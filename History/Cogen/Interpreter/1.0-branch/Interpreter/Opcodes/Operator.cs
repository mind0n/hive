using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptInterpreter
{

	abstract class Operator
	{
		public string Name = null;
		public abstract object Resolve(params object[] targets);
	}
}
