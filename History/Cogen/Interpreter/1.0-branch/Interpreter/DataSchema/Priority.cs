using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptInterpreter.DataSchema
{
	class Priority
	{
		private static string[] symbols = ",;_;=;_;==;>=;<=;_;+;-;_;*;/;_;^;_;.".Split(';');
		public int this[string opcode]
		{
			get
			{
				int priority = -1;
				foreach (string op in symbols)
				{
					if (op.Equals(opcode))
					{
						return priority;
					}
					if ("_".Equals(op))
					{
						priority++;
					}
				}
				throw new ScriptException("Invalid operator ({0})", opcode);
			}
		}
		public int this[char opcode]
		{
			get
			{
				string strOpCode = new String(new char[] { opcode });
				return this[strOpCode];
			}
		}
	}
}
