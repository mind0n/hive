using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptInterpreter
{
	enum VariableType : int
	{
		Value,
		Variable,
		NotSpecified
	}
	class Variable : Node
	{
		public VariableType Type = VariableType.NotSpecified;
		public new Variable this[string key]
		{
			get
			{
				return base[key] as Variable;
			}
			set
			{
				base[key] = value;
			}
		}
		public string Name = null;
	}
}
