using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptInterpreter.Parser;
using ScriptInterpreter.DataSchema;

namespace ScriptInterpreter.Opcodes
{
	abstract class Keyword
	{
		public string Name;
		public abstract Keyword CreateInstance();
		public abstract Expressions Process(Token tokens);
	}
}
