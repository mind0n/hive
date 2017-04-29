using System;
using ScriptEngine.Core;
using System.Diagnostics;
using System.Collections.Generic;
using ScriptEngine.Debug;

namespace ScriptEngine.Frontend
{
	public class ScriptScanner
	{
		public const int realBufferSize = 32;
		const int bufferSize = 4;
		const int bufferOffset = realBufferSize - bufferSize;
		public LexCollection Lexes = new LexCollection();
		protected RoleBasedTokenReader reader = new RoleBasedTokenReader();

		public ScriptScanner()
		{

			LexCondition stringStartEnd = reader.AddSingleConditionRole("stringSE", bufferOffset + 2, '\"', ElementType.Symbol);
			stringStartEnd.ValidateCallback += new Condition<Lex>.ValidateCallbackHandler(stringStartEnd_ValidateCallback);

			LexRole commentStartEnd = reader.AddRole("commentSE1", bufferOffset + 2,
				new LexCondition { Content = '/', Type = ElementType.Symbol, ResolveType = ElementType.SingleLineCommentStart },
				new LexCondition { Content = '/', ResolveType = ElementType.SingleLineCommentStart }
			);
			commentStartEnd.MatchCallback += new Role<Lex>.MatchCallbackHandler(commentStartEnd_MatchCallback);
			commentStartEnd = reader.AddRole("commentSE2", bufferOffset + 2,
				new LexCondition { Content = '/', Type = ElementType.Symbol, ResolveType = ElementType.MultiLineCommentStart },
				new LexCondition { Content = '*', ResolveType = ElementType.MultiLineCommentStart }
			);
			commentStartEnd.MatchCallback += new Role<Lex>.MatchCallbackHandler(commentStartEnd_MatchCallback);
			commentStartEnd = reader.AddRole("commentSE3", bufferOffset + 1,
				new LexCondition { Content = '*', Type = ElementType.MultiLineComment, ResolveType = ElementType.MultiLineCommentEnd },
				new LexCondition { Content = '/', ResolveType = ElementType.MultiLineCommentEnd }
			);
			commentStartEnd.MatchCallback += new Role<Lex>.MatchCallbackHandler(commentStartEnd_MatchCallback);
			commentStartEnd = reader.AddRole("commentSE4", bufferOffset + 1,
				new LexCondition { Content = '\r', Type = ElementType.SingleLineComment, ResolveType = ElementType.SingleLineCommentEnd },
				new LexCondition { Content = '\n', ResolveType = ElementType.SingleLineCommentEnd }
			);
			commentStartEnd.MatchCallback += new Role<Lex>.MatchCallbackHandler(commentStartEnd_MatchCallback);

			LexCondition stringCommentContent = reader.AddSingleConditionRole("content", bufferOffset + 2);
			stringCommentContent.ValidateCallback += new Condition<Lex>.ValidateCallbackHandler(stringCommentContent_ValidateCallback);


			LexCondition ignore = reader.AddSingleConditionRole("ignore", bufferOffset + 2, null, ElementType.Space);
			ignore.ValidateCallback += new Condition<Lex>.ValidateCallbackHandler(ignore_ValidateCallback);

			reader.BufferFullCallback += new Action<BufferedReader<Lex, char>, Lex[]>(reader_BufferFullCallback);
		}

		bool commentStartEnd_MatchCallback(Role<Lex> sender, Lex[] buffer, int index)
		{
			Lex curt = buffer[index];
			Lex prev = buffer[index - 1];
			Lex next = buffer[index + 1];

			if (curt.Content == '/' && next.Content == '/')
			{
				if (prev != null && (prev.Type == ElementType.MultiLineComment || prev.Type == ElementType.MultiLineCommentStart))
				{
					curt.Type = ElementType.MultiLineComment;
					return false;
				}
				return true;
			}
			else if (next.Content == '*')
			{
				if (prev != null)
				{
					if (prev.Type == ElementType.SingleLineComment || prev.Type == ElementType.SingleLineCommentStart)
					{
						curt.Type = ElementType.SingleLineComment;
						return false;
					}
					else if (prev.Type == ElementType.MultiLineComment)
					{
						curt.Type = ElementType.MultiLineComment;
						return false;
					}
				}
				return true;
			}
			else if (curt.Content == '\r')
			{
				if (prev != null && (prev.Type == ElementType.MultiLineComment || prev.Type == ElementType.MultiLineCommentStart))
				{
					curt.Type = ElementType.MultiLineComment;
					return false;
				}
				return true;
			}
			else if (curt.Content == '*')
			{
				if (prev != null && (prev.Type == ElementType.SingleLineComment || prev.Type == ElementType.SingleLineCommentStart))
				{
					curt.Type = ElementType.SingleLineComment;
					return false;
				}
				return true;
			}
			else
			{
				return false;
			}
		}

		bool ignore_ValidateCallback(Lex[] buffer, Lex prev, int index, bool ismatch)
		{
			if (prev == null
				|| (prev.Type != ElementType.String
					&& prev.Type != ElementType.StringStart
					&& prev.Type != ElementType.MultiLineCommentStart
					&& prev.Type != ElementType.SingleLineCommentStart
					&& prev.Type != ElementType.SingleLineComment
					&& prev.Type != ElementType.MultiLineComment))
			{
				buffer[index].Type = ElementType.Ignore;
				return true;
			}
			return false;
		}

		void stringRole_MatchCallback(Role<Lex> sender, Lex[] buffer, int index)
		{
			for (int i = index + 1; i < buffer.Length; i++)
			{
				buffer[i].Type = ElementType.String;
			}
		}

		bool stringCommentContent_ValidateCallback(Lex[] buffer, Lex prev, int index, bool ismatch)
		{
			Lex curt = buffer[index];
			if (prev != null && curt != null
				&& prev.Type != ElementType.StringEnd
				&& prev.Type != ElementType.SingleLineCommentEnd
				&& prev.Type != ElementType.MultiLineCommentEnd
				&& curt.Type != ElementType.SingleLineCommentStart
				&& curt.Type != ElementType.MultiLineCommentStart
			)
			{
				if (prev.Type == ElementType.StringStart || prev.Type == ElementType.String)
				{
					buffer[index].Type = ElementType.String;
				}
				else if (prev.Type == ElementType.SingleLineCommentStart || prev.Type == ElementType.SingleLineComment)
				{
					buffer[index].Type = ElementType.SingleLineComment;
				}
				else if (prev.Type == ElementType.MultiLineCommentStart || prev.Type == ElementType.MultiLineComment)
				{
					buffer[index].Type = ElementType.MultiLineComment;
				}
				else
				{
					return false;
				}
				return true;
			}
			return false;
		}

		bool stringStartEnd_ValidateCallback(Lex[] buffer, Lex prev, int index, bool ismatch)
		{
			Lex curt = buffer[index];
			if (index < 1 && curt.Type != ElementType.SingleLineComment && curt.Type != ElementType.MultiLineComment)
			{
				curt.Type = ElementType.StringStart;
			}
			else
			{
				prev = buffer[index - 1];
			}
			if (prev != null && prev.Type != ElementType.SingleLineCommentStart && prev.Type != ElementType.SingleLineComment && prev.Type != ElementType.MultiLineCommentStart && prev.Type != ElementType.MultiLineComment)
			{
				if (prev.Content == '\\')
				{
					curt.Type = prev.Type;
				}
				else if (prev.Type == ElementType.StringStart || prev.Type == ElementType.String)
				{
					curt.Type = ElementType.StringEnd;
				}
				else
				{
					curt.Type = ElementType.StringStart;
				}
				return true;
			}
			return false;
		}

		public void Load(string scripts)
		{
			for (int i = 0; i < scripts.Length; i++)
			{
				reader.Read(scripts[i]);
			}
			reader.Flush();
		}

		protected void reader_BufferFullCallback(BufferedReader<Lex, char> sender, Lex[] buffer)
		{
			Lexes.Add(buffer[0]);
		}

	}
}
