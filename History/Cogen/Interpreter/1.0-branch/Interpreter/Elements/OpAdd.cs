using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptInterpreter.Elements
{
	class OpAdd:OpCode
	{
		public OpAdd()
		{
			Name = "+";
		}
		public override OpCode CreateInstance()
		{
			return new OpAdd();
		}
		public override Expression Process(Token tokens)
		{
			return null;
		}
	}
}
