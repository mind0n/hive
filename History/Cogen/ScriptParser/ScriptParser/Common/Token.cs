using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScriptParser.Common
{
	public class TokenStack : List<Legend>
	{
		public void Push(Legend token)
		{
			if (token != null)
			{
				Insert(0, token);
			}
		}
		public Legend Pop()
		{
			if (Count > 0)
			{
				Legend r = this[0];
				Remove(r);
				return r;
			}
			return null;
		}
		public Legend Read()
		{
			if (Count > 0)
			{
				Legend r = this[0];
				return r;
			}
			return null;
		}
	}
	public class Token
	{
		public TokenType Type{get;set;}
		public string Content
		{
			get
			{
				return storage.ToString();
			}
			set
			{
				storage = new StringBuilder(value);
			}
		}
		public Token()
		{
			Type = TokenType.Unknown;
		}
		public bool Add(Legend legend)
		{
			if (legend.Type == Type || storage.Length < 1)
			{
				storage.Append(legend.Content);
				return true;
			}
			return false;
		}
		protected StringBuilder storage = new StringBuilder();
	}
	
	public class Tokens
	{
		public Token[] Content
		{
			get
			{
				return storage.ToArray();
			}
		}
		protected JsLegendCache cache = new JsLegendCache();
		protected List<Token> storage = new List<Token>();
		public Tokens()
		{
			cache.OnShiftLeft += cache_OnShiftLeft;
		}

		protected virtual bool cache_OnShiftLeft(Legend item)
		{
			Add(item);
			return true;
		}
		public string PrintCache()
		{
            string dir = "C:\\$$$$$\\temp\\output\\";
			string rlt = cache.Print();
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
			File.WriteAllText(dir + "output.txt", rlt);
			return rlt;
		}
		public void Flush()
		{
			cache.Flush();
		}
		public void Add(char ch)
		{
			cache.Add(ch);
		}
		public void Add(Legend legend)
		{
			Token curt = null;
			if (storage.Count < 1)
			{
				curt = new Token();
				curt.Type = legend.Type;
				storage.Add(curt);
			}
			else
			{
				curt = storage[storage.Count - 1];
			}
			if (!curt.Add(legend))
			{
				Token token = new Token();
				token.Type = legend.Type;
				token.Add(legend);
				storage.Add(token);
			}
		}
	}
}
