using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptInterpreter.DataSchema
{
	class Expression : Node
	{
		public new Expression this[string key]
		{
			get
			{
				return base[key] as Expression;
			}
			set
			{
				base[key] = value;
			}
		}

	}
}
