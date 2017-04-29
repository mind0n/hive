using ScriptParser.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptParser
{
    public class JsLegendCache : Cache<Legend>
    {
        public JsLegendCache()
            : base(32)
        {
        }

		public string Print()
		{
			StringBuilder b = new StringBuilder();
			for (int index = 0; index < storage.Length; index++)
			{
				Legend i = this[index];
				if (i != null)
				{
					b.Append(i.Content).Append("\t").Append(i.Type).Append("\r\n");
				}
			}
			return b.ToString();
		}
		public void Flush()
		{
			while (this[0] != null)
			{
				Add(null);
			}
		}
        public void Add(char ch)
        {
            Legend token = new Legend();
            token.Content = ch;
			if (char.IsNumber(ch))
			{
				token.Type = TokenType.Number;
			}
			else if (char.IsLetter(ch))
			{
				token.Type = TokenType.Letter;
			}
			else
			{
				token.Type = TokenType.Operator;
			}
            Add(token);
        }
    }

}
