using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptInterpreter.Elements
{
	class OpEqual:OpCode
	{
		public OpEqual()
		{
			Name = "=";
		}
		public override OpCode CreateInstance()
		{
			return new OpEqual();
		}
		public override Expression AddToken(Token tokens)
		{
			return null;
		}
	}
}
