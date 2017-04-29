using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptInterpreter
{
	class Node : Dictionary<string, Node>
	{
	}

	class Context : Node
	{
		public new Context this[string key]
		{
			get
			{
				return base[key] as Context;
			}
			set
			{
				base[key] = value;
			}
		}
	}
}
