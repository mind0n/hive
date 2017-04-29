using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptInterpreter.DataSchema;

namespace ScriptInterpreter
{
	public class Token
	{
		public bool Completed = false;
		public int LineNumber = 0;
		public string StartOpCode;
		public string Content
		{
			get
			{
				return builder.ToString();
			}
		}
		public TokenType TokenType = TokenType.NotSpecified;
		public Token PrevToken
		{
			get { return prevToken; }
			set { prevToken = value; }
		}
		public Token NextToken
		{
			get { return nextToken; }
			set { nextToken = value; }
		}
		protected StringBuilder builder = new StringBuilder();
		private Token nextToken;
		private Token prevToken;

		public Token() { }
		public Token(TokenCollection tokens)
		{
			if (tokens != null && tokens.Count >= 1)
			{
				Token prev = tokens[tokens.Count - 1];
				prevToken = prev;
				prev.nextToken = this;
			}
		}

		public bool Read(char[] cache, bool toBeContinued)
		{
			TokenType prevMode = TokenType;
			if (cache == null || cache.Length < 1)
			{
				TokenType = TokenType.NotSpecified;
				return true;
			}
			else if (cache.Length < 2 || (cache[1] == 0 && !toBeContinued))
			{
				builder.Append(cache[0]);
				TokenType = JudgeMode(cache[0]);
				return true;
			}
			else if (cache[1] == 0 && toBeContinued)
			{
				builder.Append(cache[0]);
				TokenType = JudgeMode(cache[0]);
				return false;
			}
			if (!JudgeCurrentMode(prevMode, cache))
			{
				AppendBuilder(cache, false);
				return true;
			}
			if (TokenType != TokenType.NotSpecified)
			{
				AppendBuilder(cache, true);
			}
			return false;
		}
		protected void AppendBuilder(char[] cache, bool isContinue)
		{
			if (TokenType != TokenType.String && TokenType != TokenType.Comment && (cache[1] == '\r' || cache[1] == '\n'))
			{
				//CurrentMode = SegmentMode.Ignore;
				return;
			}
			if (isContinue)
			{
				if (cache[1] != ' ' || TokenType == TokenType.String || TokenType == TokenType.Comment)
				{
					if (cache[0] != '\\' || cache[1] != '\\')
					{
						builder.Append(cache[1]);
					}
				}
			}
			else
			{
				if (TokenType == TokenType.String)
				{
					if (cache[0] != '\\' || cache[1] != '\\')
					{
						builder.Append(cache[1]);
					}
				}
				else if (TokenType == TokenType.Comment && "/*".Equals(StartOpCode))
				{
					builder.Append(cache[1]);
				}
				else if (cache[2] == '\0' && cache[1] != '\0')
				{
					builder.Append(cache[1]);
				}
			}
		}
		protected bool JudgeCurrentMode(TokenType prevMode, char[] cache)
		{
			char ch = cache[1];
			TokenType curtMode = JudgeMode(ch);

			// Mode Begin OpCode detected.  (eg.:["][//][/*])
			if (ch == '"' && prevMode != TokenType.String && prevMode != TokenType.Comment)
			{
				if (prevMode != TokenType.OpCode)
				{
					StartOpCode = "\"";
					TokenType = TokenType.String;
					return true;
				}
				else
				{
					return false;
				}
			}
			else if (ch == '\\' && cache[0] == '\\' && prevMode == TokenType.String)
			{
				return true;
			}
			else if (ch == '/' && cache[0] == '/' && prevMode != TokenType.Comment && prevMode != TokenType.String)
			{
				if (prevMode != TokenType.OpCode)
				{
					StartOpCode = "//";
					TokenType = TokenType.Comment;
					return true;
				}
				else
				{
					return false;
				}
			}
			else if (ch == '*' && cache[0] == '/' && prevMode != TokenType.Comment && prevMode != TokenType.String)
			{
				StartOpCode = "/*";
				TokenType = TokenType.Comment;
				return true;
			}
			else if (ch == '(' && prevMode == TokenType.Variable)
			{
				if (prevToken != null && !"function".Equals(prevToken.Content))
				{
					TokenType = TokenType.FuncCall;
				}
				else
				{
					TokenType = TokenType.FuncDef;
				}
				return false;
			}

			// Mode End OpCode detected.  (eg.:["][\r][\n][*/])
			if (curtMode == prevMode && curtMode != TokenType.String && curtMode != TokenType.Comment)
			{
				if (ch != '(' && ch != ')' && ch != '{' && ch != '}')
				{
					if (prevMode == TokenType.OpCode && curtMode == TokenType.OpCode)
					{
						if (cache[0] == '/' && ch == '/' || cache[0] == '/' && ch == '*')
						{
							TokenType = TokenType.Comment;
						}
					}
					if (TokenType == TokenType.NotSpecified && curtMode != TokenType.NotSpecified)
					{
						TokenType = curtMode;
					}
					return true;
				}
				else
				{
					return false;
				}
			}
			else if (prevMode == TokenType.Variable && curtMode == TokenType.Number)
			{
				TokenType = prevMode;
				return true;
			}
			else if (prevMode == TokenType.Number && ch == '.')
			{
				TokenType = TokenType.Number;
				return true;
			}
			else if (prevMode == TokenType.String)
			{
				TokenType = prevMode;
				if ("\"".Equals(StartOpCode) && (ch != '"' || cache[0] == '\\'))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else if (prevMode == TokenType.Comment)
			{
				if (("//".Equals(StartOpCode) && ch != '\n') || ("/*".Equals(StartOpCode) && (cache[0] != '*' || cache[1] != '/')))
				{
					TokenType = prevMode;
					return true;
				}
				else
				{
					return false;
				}
			}
			else if ((prevMode == TokenType.String || prevMode == TokenType.Comment) && curtMode == TokenType.Space)
			{
				TokenType = prevMode;
				return true;
			}
			else if (prevMode == TokenType.NotSpecified && curtMode != TokenType.NotSpecified)
			{
				TokenType = curtMode;
				return true;
			}
			return false;
		}
		protected TokenType JudgeMode(char ch)
		{
			string symbols = "`-+/*^~!@#$%&()=[]\\|':\",.<>?";
			if (char.IsLetter(ch) || ch == '_')
			{
				return TokenType.Variable;
			}
			else if (char.IsDigit(ch))
			{
				return TokenType.Number;
			}
			else if (symbols.IndexOf(ch) >= 0)
			{
				if (ch == 0)
				{
					return TokenType.NotSpecified;
				}
				return TokenType.OpCode;
			}
			else if (ch == ';')
			{
				return TokenType.Statement;
			}
			else if (ch == ' ')
			{
				return TokenType.Space;
			}
			else if (ch == '\r' || ch == '\n')
			{
				return TokenType.NewLine;
			}
			else if (ch == '{')
			{
				return TokenType.BlockBegin;
			}
			else if (ch == '}')
			{
				return TokenType.BlockEnd;
			}
			else
			{
				return TokenType.NotSpecified;
			}
		}
	}
}
