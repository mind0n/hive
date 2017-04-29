using System.Collections.Generic;

namespace ScriptEngine.Core
{
	public class Lex
	{
		public char Content;
		public ElementType Type;
	}


	public class LexCollection : List<Lex>
	{
	}
}
