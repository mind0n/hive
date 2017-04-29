using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptInterpreter
{
	class ScriptException : Exception
	{
		public ScriptException() : base() { }
		public ScriptException(string message) : base(message) { }
		public ScriptException(string message, params string[] args) : base(string.Format(message, args)) { }
	}
}
