using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptInterpreter.Elements
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

		public override Expression Process(Token tokens)
		{
			//throw new NotImplementedException();
			return null;
		}
	}
}
