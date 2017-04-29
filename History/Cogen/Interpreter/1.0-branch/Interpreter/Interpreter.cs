using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptInterpreter.Elements;
using ScriptInterpreter.DataSchema;
using ScriptInterpreter.Parser;

namespace ScriptInterpreter
{
	public class Interpreter
	{
		//public static List<string> Keywords = new List<string>();
		public static Interpreter CurrentInterpreter;
		private TokenCollection tokens;

		#region Constructors
		public static Interpreter CreateInstance()
		{
			Interpreter result = new Interpreter();
            Keyword.Keywords["var"] = new KwVar();
            return result;
		}
		static Interpreter()
		{
		}
		protected Interpreter() {
			CurrentInterpreter = this;
		}
		#endregion

		public static Exception Raise(string message, params string[] args)
		{
			if (args != null)
			{
				return new Exception(string.Format(message, args));
			}
			else
			{
				return new Exception(message);
			}
		}
		public TokenCollection Read(string script)
		{
			if (CurrentInterpreter != null)
			{
				StringTokenReader reader = new StringTokenReader(script);
				List<string> rlt = new List<string>();
				while (true)
				{
					Token token = reader.ReadNextToken();
					if (token == null)
					{
						break;
					}
					else
					{
						token.LineNumber = reader.LineNumber;
					}
				}
				CurrentInterpreter.tokens = reader.Tokens;
				return reader.Tokens;
			}
			return null;
		}
		public void Parse(TokenCollection tokens)
		{
			Token token = null;
			if (tokens == null)
			{
				return;
			}
			int i = 0;
			token = tokens[i];
			try
			{
                Expression tree = new Expression();
                Expression curtExp = null;
				while (token != null)
				{
					if (curtExp == null)
					{
						curtExp = tree.CreateInstance(token);
					}
					else
					{
						curtExp.Process(token);
					}
					token = curtExp.LastToken.NextToken;
					if (curtExp.IsComplete)
					{
						tree.Add(curtExp);
						curtExp = null;
					}
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message + "\r\nAt line " + token.LineNumber);
			}

		}
	}
}
