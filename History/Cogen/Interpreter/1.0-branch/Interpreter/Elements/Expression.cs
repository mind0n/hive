using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptInterpreter.DataSchema;
using ScriptInterpreter.Elements;

namespace ScriptInterpreter.Elements
{
	abstract class Expression : List<Expression>
	{
		public static Dictionary<TokenType, Expression> ExpTypes = new Dictionary<TokenType, Expression>();
		public string Name;
		private bool isComplete;

		public bool IsComplete
		{
			get { return isComplete; }
		}

		private int tokenCount;

		public int TokenCount
		{
			get { return tokenCount; }
		}
		private Token lastToken;

		public Token LastToken
		{
			get { return lastToken; }
		}

		public abstract Expression Process(Token tokens);
		public Expression CreateInstance(Token token)
		{
			if (ExpTypes.ContainsKey(token.TokenType))
			{
				lastToken = token;
				Expression rlt = ExpTypes[token.TokenType].CreateInstance(token);
				rlt.isComplete = false;
				return rlt;
			}
			throw Interpreter.Raise(Messages.ProcessingInvalidToken, token.Content, "creating expression", token.LineNumber);
		}
		
	}
}
