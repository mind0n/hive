using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptInterpreter.Opcodes
{
	class KwVar : Keyword
	{
		public KwVar()
		{
			Name = "var";
		}
		public override Keyword CreateInstance()
		{
			return new KwVar();
		}

		public override Parser.Expressions Process(Token tokens)
		{
			//throw new NotImplementedException();
			return null;
		}
	}
}
