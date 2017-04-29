using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptInterpreter
{
	public class Segment
	{
		public bool Completed = false;
		public string StartOpCode;
		public string Content
		{
			get
			{
				return builder.ToString();
			}
		}
		public SegmentMode SegmentType = SegmentMode.NotSpecified;
		public Segment PrevSegment
		{
			get { return prevSegment; }
			set { prevSegment = value; }
		}
		public Segment NextSegment
		{
			get { return nextSegment; }
			set { nextSegment = value; }
		}
		protected StringBuilder builder = new StringBuilder();
		private Segment nextSegment;
		private Segment prevSegment;

		public Segment() { }
		public Segment(List<Segment> segments)
		{
			if (segments != null && segments.Count >= 1)
			{
				Segment prev = segments[segments.Count - 1];
				prevSegment = prev;
				prev.nextSegment = this;
			}
		}

		public bool Read(char[] cache, bool toBeContinued)
		{
			SegmentMode prevMode = SegmentType;
			if (cache == null || cache.Length < 1)
			{
				SegmentType = SegmentMode.NotSpecified;
				return true;
			}
			else if (cache.Length < 2 || (cache[1] == 0 && !toBeContinued))
			{
				builder.Append(cache[0]);
				SegmentType = JudgeMode(cache[0]);
				return true;
			}
			else if (cache[1] == 0 && toBeContinued)
			{
				builder.Append(cache[0]);
				SegmentType = JudgeMode(cache[0]);
				return false;
			}
			if (!JudgeCurrentMode(prevMode, cache))
			{
				AppendBuilder(cache, false);
				return true;
			}
			if (SegmentType != SegmentMode.NotSpecified)
			{
				AppendBuilder(cache, true);
			}
			return false;
		}
		protected void AppendBuilder(char[] cache, bool isContinue)
		{
			if (SegmentType != SegmentMode.String && SegmentType != SegmentMode.Comment && (cache[1] == '\r' || cache[1] == '\n'))
			{
				//CurrentMode = SegmentMode.Ignore;
				return;
			}
			if (isContinue)
			{
				if (cache[1] != ' ' || SegmentType == SegmentMode.String || SegmentType == SegmentMode.Comment)
				{
					if (cache[0] != '\\' || cache[1] != '\\')
					{
						builder.Append(cache[1]);
					}
				}
			}
			else
			{
				if (SegmentType == SegmentMode.String)
				{
					if (cache[0] != '\\' || cache[1] != '\\')
					{
						builder.Append(cache[1]);
					}
				}
				else if (SegmentType == SegmentMode.Comment && "/*".Equals(StartOpCode))
				{
					builder.Append(cache[1]);
				}
				else if (cache[2] == '\0' && cache[1] != '\0')
				{
					builder.Append(cache[1]);
				}
			}
		}
		protected bool JudgeCurrentMode(SegmentMode prevMode, char[] cache)
		{
			char ch = cache[1];
			SegmentMode curtMode = JudgeMode(ch);

			// Mode Begin OpCode detected.  (eg.:["][//][/*])
			if (ch == '"' && prevMode != SegmentMode.String && prevMode != SegmentMode.Comment)
			{
				if (prevMode != SegmentMode.OpCode)
				{
					StartOpCode = "\"";
					SegmentType = SegmentMode.String;
					return true;
				}
				else
				{
					return false;
				}
			}
			else if (ch == '\\' && cache[0] == '\\' && prevMode == SegmentMode.String)
			{
				return true;
			}
			else if (ch == '/' && cache[0] == '/' && prevMode != SegmentMode.Comment && prevMode != SegmentMode.String)
			{
				if (prevMode != SegmentMode.OpCode)
				{
					StartOpCode = "//";
					SegmentType = SegmentMode.Comment;
					return true;
				}
				else
				{
					return false;
				}
			}
			else if (ch == '*' && cache[0] == '/' && prevMode != SegmentMode.Comment && prevMode != SegmentMode.String)
			{
				StartOpCode = "/*";
				SegmentType = SegmentMode.Comment;
				return true;
			}
			else if (ch == '(' && prevMode == SegmentMode.Variable)
			{
				if (prevSegment != null && !"function".Equals(prevSegment.Content))
				{
					SegmentType = SegmentMode.FuncCall;
				}
				else
				{
					SegmentType = SegmentMode.FuncDef;
				}
				return false;
			}

			// Mode End OpCode detected.  (eg.:["][\r][\n][*/])
			if (curtMode == prevMode && curtMode != SegmentMode.String && curtMode != SegmentMode.Comment)
			{
				if (ch != '(' && ch != ')' && ch != '{' && ch != '}')
				{
					if (prevMode == SegmentMode.OpCode && curtMode == SegmentMode.OpCode)
					{
						if (cache[0] == '/' && ch == '/' || cache[0] == '/' && ch == '*')
						{
							SegmentType = SegmentMode.Comment;
						}
					}
					if (SegmentType == SegmentMode.NotSpecified && curtMode != SegmentMode.NotSpecified)
					{
						SegmentType = curtMode;
					}
					return true;
				}
				else
				{
					return false;
				}
			}
			else if (prevMode == SegmentMode.Variable && curtMode == SegmentMode.Number)
			{
				SegmentType = prevMode;
				return true;
			}
			else if (prevMode == SegmentMode.Number && ch == '.')
			{
				SegmentType = SegmentMode.Number;
				return true;
			}
			else if (prevMode == SegmentMode.String)
			{
				SegmentType = prevMode;
				if ("\"".Equals(StartOpCode) && (ch != '"' || cache[0] == '\\'))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else if (prevMode == SegmentMode.Comment)
			{
				if (("//".Equals(StartOpCode) && ch != '\n') || ("/*".Equals(StartOpCode) && (cache[0] != '*' || cache[1] != '/')))
				{
					SegmentType = prevMode;
					return true;
				}
				else
				{
					return false;
				}
			}
			else if ((prevMode == SegmentMode.String || prevMode == SegmentMode.Comment) && curtMode == SegmentMode.Space)
			{
				SegmentType = prevMode;
				return true;
			}
			else if (prevMode == SegmentMode.NotSpecified && curtMode != SegmentMode.NotSpecified)
			{
				SegmentType = curtMode;
				return true;
			}
			return false;
		}
		protected SegmentMode JudgeMode(char ch)
		{
			string symbols = "`-+/*^~!@#$%&()=[]\\|':\",.<>?";
			if (char.IsLetter(ch) || ch == '_')
			{
				return SegmentMode.Variable;
			}
			else if (char.IsDigit(ch))
			{
				return SegmentMode.Number;
			}
			else if (symbols.IndexOf(ch) >= 0)
			{
				if (ch == 0)
				{
					return SegmentMode.NotSpecified;
				}
				return SegmentMode.OpCode;
			}
			else if (ch == ';')
			{
				return SegmentMode.Statement;
			}
			else if (ch == ' ')
			{
				return SegmentMode.Space;
			}
			else if (ch == '\r' || ch == '\n')
			{
				return SegmentMode.NewLine;
			}
			else if (ch == '{')
			{
				return SegmentMode.BlockBegin;
			}
			else if (ch == '}')
			{
				return SegmentMode.BlockEnd;
			}
			else
			{
				return SegmentMode.NotSpecified;
			}
		}
	}
}
