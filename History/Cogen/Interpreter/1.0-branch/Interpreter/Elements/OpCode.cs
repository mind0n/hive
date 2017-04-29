using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptInterpreter.Parser;

namespace ScriptInterpreter.Elements
{

	abstract class OpCode : Expression
	{
		public static Dictionary<string, OpCode> OpCodes = new Dictionary<string, OpCode>();
		public override Expression CreateInstance(Token opCode)
		{
			if (OpCodes.ContainsKey(opCode.Content))
			{
				return OpCodes[opCode.Content].CreateInstance();
			}
			throw Interpreter.Raise(Messages.ProcessingInvalidToken, opCode.Content, "creating keyword expression", opCode.LineNumber);
		}
		public abstract OpCode CreateInstance();
	}
}
