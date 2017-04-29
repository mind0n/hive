using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptInterpreter.Parser;
using ScriptInterpreter.DataSchema;

namespace ScriptInterpreter.Elements
{
	class Keyword : Expression
	{
        public static Dictionary<string, Keyword> Keywords = new Dictionary<string, Keyword>();
        public override Expression CreateInstance(Token keyword)
        {
            if (Keywords.ContainsKey(keyword.Content))
            {
                return Keywords[keyword.Content].CreateInstance();
            }
			throw Interpreter.Raise(Messages.ProcessingInvalidToken, keyword.Content, "creating keyword expression", keyword.LineNumber);
        }
        public abstract Keyword CreateInstance();
	}
}
