using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptInterpreter
{
	class Keywords
	{
		static List<string> keywords = new List<string>();
		private Interpreter interpreter;
		private Dictionary<string, Func<Token, Token>> processors = new Dictionary<string, Func<Token, Token>>();

		public static bool Contains(string keyword)
		{
			return keywords.Contains(keyword);
		}
		public Keywords(Interpreter curtInterpreter)
		{
			interpreter = curtInterpreter;
			keywords.Add("var");
			keywords.Add("if");
			keywords.Add("for");
			keywords.Add("function");
			keywords.Add("return");
			keywords.Add("while");
			processors["var"] = ProcessVar;
			processors["if"] = null;
			processors["for"] = null;
			processors["function"] = null;
			processors["return"] = null;
			processors["while"] = null;
		}
		public Token Process(Token segment)
		{
			if (segment != null)
			{
				if (processors.ContainsKey(segment.Content))
				{
					try
					{
						return processors[segment.Content](segment.NextSegment);
					}
					catch (ScriptException error)
					{
						throw error;
					}
				}
				throw new ScriptException("No such keyword ({0}).", segment.Content);
			}
			throw new ScriptException("Keyword cannot be empty.");
		}
		private Token ProcessVar(Token segment)
		{
			if (segment != null && segment.SegmentType == TokenType.Variable)
			{
				interpreter.RegisterVariable(segment.Content);
			}
			return segment;
		}
	}
}
