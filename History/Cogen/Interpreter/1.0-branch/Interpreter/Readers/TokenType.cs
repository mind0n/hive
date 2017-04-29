using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptInterpreter
{
	public enum TokenType
	{
		Space,
		String,
		Number,
		OpCode,
		Ignore,
		Keyword,
		Comment,
		NewLine,
		Variable,
		FuncDef,
		FuncCall,
		ParamStart,
		ParamEnd,
		Statement,
		BlockBegin,
		BlockEnd,
		NotSpecified
	}
}
