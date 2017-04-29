using System;
using System.Collections.Generic;
using ScriptEngine.Frontend;
using System.Diagnostics;
using ScriptEngine.Debug;

namespace ScriptEngine.Core
{
	public class LexReader : BufferedReader<Lex, char>
	{
		public event Action<LexReader, Lex> ReadCallback;

		public LexReader()
			: base(ScriptScanner.RealBufferSize)
		{
			BufferReadCallback += new Action<BufferedReader<Lex, char>, Lex[]>(OnBufferRead);
			
		}

		protected virtual void OnBufferRead(BufferedReader<Lex, char> sender, Lex[] buffer)
		{
			if (ReadCallback != null)
			{
				ReadCallback(this, buffer[0]);
			}
		}

		public override Lex CreateBufferUnit(char content)
		{
			Lex token = new Lex { Content = content, Type = ElementType.Unknown };
			if (char.IsLetter(content))
			{
				token.Type = ElementType.Variable;
			}
			else if (char.IsDigit(content))
			{
				token.Type = ElementType.Integer;
			}
			else if (char.IsSymbol(content) || char.IsPunctuation(content))
			{
				token.Type = ElementType.Symbol;
			}
			else if (content == ' ' || content == '\t' || content == '\r' || content == '\n')
			{
				token.Type = ElementType.Space;
			}
			return token;
		}
	}
	
	public class RoleBasedTokenReader : LexReader
	{
		List<LexRole> roles = new List<LexRole>();
		public LexRole AddRole(string name, int startIndex, params LexCondition[] conditions)
		{
			LexRole role = null;
			if (conditions != null)
			{
				role = new LexRole(name, startIndex, conditions);
			}
			roles.Add(role);
			return role;
		}
		public LexCondition AddSingleConditionRole(string name, int startIndex = 0, char? lex = null, ElementType? type = null, Condition<Lex>.ValidateCallbackHandler callback = null)
		{
			LexCondition condition = new LexCondition { Content = lex, Type = type };
			if (callback != null)
			{
				condition.ValidateCallback += callback;
			}
			LexRole role = new LexRole(name, startIndex, condition);
			roles.Add(role);
			return condition;
		}
		protected override void OnBufferRead(BufferedReader<Lex, char> sender, Lex[] buffer)
		{
			TraceBuffer(buffer);
			for (int ii = 0; ii < roles.Count; ii++)
			{
				LexRole role = roles[ii];
				TraceRole(role);
				if (role.Validate(buffer))
				{
					break;
				}
			}
			base.OnBufferRead(sender, buffer);
		}

		private static void TraceRole(LexRole role)
		{
#if DEBUG
			D.WriteLine("-[" + role.Name + "]-----------");
			D.str1 = role.Name;
#endif
		}

		private static void TraceBuffer(Lex[] buffer)
		{
#if DEBUG
			string s = string.Empty;
			int p = -1;
			for (int i = 0; i < buffer.Length; i++)
			{
				Lex l = buffer[i] as Lex;
				if (l != null)
				{
					s += "[" + l.Content + "," + l.Type + "]";
					p++;
				}
			}
			D.Write(p.ToString(), "<<", s);
			D.num1 = p;
#endif
		}
	}
}
