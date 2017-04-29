using System.Collections.Generic;
using System.Text;

namespace ScriptEngine.Core
{
	public class Lex
	{
		public char Content;
		public ElementType Type;
	}


	public class Token
	{
		StringBuilder builder = new StringBuilder();
		public ElementType Type = ElementType.Unknown;
		public string Content
		{
			get
			{
				return builder.ToString();
			}
		}
		public bool Add(Lex item)
		{
			if (item != null)
			{
				if (item.Type == ElementType.Ignore)
				{
					return false;
				}
				if (Type == ElementType.Unknown || (item.Type == Type && item.Type != ElementType.ContextBegin && item.Type != ElementType.ContextEnd && item.Type != ElementType.RightCycle))
				{
					builder.Append(item.Content);
					Type = item.Type;
					return true;
				}
			}
			return false;
		}
	}
}
