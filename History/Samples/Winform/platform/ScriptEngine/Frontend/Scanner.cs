using System;
using ScriptEngine.Core;
using System.Diagnostics;
using System.Collections.Generic;
using ScriptEngine.Debug;

namespace ScriptEngine.Frontend
{
	public class ScriptScanner
	{
		public delegate void OnReadCallbackHandler(ScriptScanner sender, Token lexes, Lex lex);
		public delegate void OnReadTokenCallbackHandler(ScriptScanner sender, List<Token> tokens, Token token);
		public event OnReadTokenCallbackHandler OnReadTokenCallback;
		protected event OnReadCallbackHandler OnReadCallback;

		public const int RealBufferSize = 32;
		const int bufferSize = 6;
		const int bufferOffset = RealBufferSize - bufferSize;
		public List<Token> Tokens = new List<Token>();
		protected RoleBasedTokenReader reader = new RoleBasedTokenReader();
		protected Token curtToken = new Token();
		protected List<Lex> pairqueue = new List<Lex>();

		public ScriptScanner()
		{
			OnReadCallback += new OnReadCallbackHandler(ScriptScanner_OnReadCallback);
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

			LexRole digitalPoint = reader.AddRole("digitalPt", bufferOffset + 2,
				new LexCondition { Type = ElementType.Integer, ResolveType = ElementType.Float },
				new LexCondition { Content='.', Type = ElementType.Symbol, ResolveType = ElementType.Float },
				new LexCondition { Type = ElementType.Integer, ResolveType = ElementType.Float }
			);
			digitalPoint.MatchCallback += new Role<Lex>.MatchCallbackHandler(digitalPoint_MatchCallback);

			LexRole int2float = reader.AddRole("int2float", bufferOffset + 2,
				new LexCondition { Type = ElementType.Float, ResolveType = ElementType.Float },
				new LexCondition { Type = ElementType.Integer, ResolveType = ElementType.Float }
			);

			LexCondition stringCommentContent = reader.AddSingleConditionRole("content", bufferOffset + 2);
			stringCommentContent.ValidateCallback += new Condition<Lex>.ValidateCallbackHandler(stringCommentContent_ValidateCallback);

			LexCondition symbolCondition = reader.AddSingleConditionRole("symbol", bufferOffset + 2, null, ElementType.Symbol);
			symbolCondition.ValidateCallback += new Condition<Lex>.ValidateCallbackHandler(symbolCondition_ValidateCallback);

			LexCondition ignore = reader.AddSingleConditionRole("ignore", bufferOffset + 2, null, ElementType.Space);
			ignore.ValidateCallback += new Condition<Lex>.ValidateCallbackHandler(ignore_ValidateCallback);

			reader.BufferFullCallback += new Action<BufferedReader<Lex, char>, Lex[]>(reader_BufferFullCallback);
		}


		bool digitalPoint_MatchCallback(Role<Lex> role, Lex[] buffer, int index)
		{
			for (int i = index; i >= 0; i--)
			{
				if (buffer[i].Type == ElementType.Integer)
				{
					buffer[i].Type = ElementType.Float;
				}
			}

			return true;
		}

		bool symbolCondition_ValidateCallback(Lex[] buffer, Lex prev, int index, bool ismatch)
		{
			Lex curt = buffer[index];
			if (curt.Content == ';')
			{
				curt.Type = ElementType.StatementEnd;
			}
			else if (curt.Content == '{')
			{
				curt.Type = ElementType.ContextBegin;
			}
			else if (curt.Content == '}')
			{
				curt.Type = ElementType.ContextEnd;
			}
			else if (curt.Content == '(')
			{
				if (prev != null && prev.Type == ElementType.Variable)
				{
					curt.Type = ElementType.ParamenterBegin;
				}
				else
				{
					curt.Type = ElementType.LeftCycle;
				}
				pairqueue.Insert(0, curt);
			}
			else if (curt.Content == ')')
			{
				Lex pair = null;
				if (pairqueue.Count > 0)
				{
					pair = pairqueue[0];
					pairqueue.RemoveAt(0);
				}
				if (pair != null)
				{
					if (pair.Type == ElementType.ParamenterBegin)
					{
						curt.Type = ElementType.ParameterEnd;
					}
					else if (pair.Type == ElementType.LeftCycle)
					{
						curt.Type = ElementType.RightCycle;
					}
					else
					{
						return false;
					}
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
			return true;
		}

		bool commentStartEnd_MatchCallback(Role<Lex> sender, Lex[] buffer, int index)
		{
			Lex curt = buffer[index];
			Lex prev = buffer[index - 1];
			Lex next = buffer[index + 1];

			if (curt.Content == '/' && next.Content == '/')
			{
				
				if (prev != null)
				{
					if (prev.Type == ElementType.MultiLineComment || prev.Type == ElementType.MultiLineCommentStart)
					{
						curt.Type = ElementType.MultiLineComment;
						return false;
					}
					else if (prev.Type == ElementType.String || prev.Type == ElementType.StringStart)
					{
						curt.Type = ElementType.String;
						return false;
					}
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
					else if (prev.Type == ElementType.String || prev.Type == ElementType.StringStart)
					{
						curt.Type = ElementType.String;
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
			ElementType type = ElementType.Unknown;
			if (index + 1 < buffer.Length && buffer[index + 1] != null)
			{
				type = buffer[index + 1].Type;
			}
			if (prev == null
				|| (prev.Type != ElementType.String
					&& prev.Type != ElementType.StringStart
					&& prev.Type != ElementType.MultiLineCommentStart
					&& prev.Type != ElementType.SingleLineCommentStart
					&& prev.Type != ElementType.SingleLineComment
					&& prev.Type != ElementType.MultiLineComment))
			{
				buffer[index].Type = ElementType.Ignore;
				if (type != prev.Type)
				{
					reader.RemoveIgnore(index);
				}
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
			if (OnReadCallback != null)
			{
				OnReadCallback(this, curtToken, buffer[0]);
			}
		}
		protected void ScriptScanner_OnReadCallback(ScriptScanner sender, Token lexes, Lex lex)
		{
			if (!curtToken.Add(lex))
			{
				Tokens.Add(curtToken);
				if (OnReadTokenCallback != null)
				{
					OnReadTokenCallback(this, Tokens, curtToken);
				}
				curtToken = new Token();
				curtToken.Add(lex);
			}
		}
	}
}
