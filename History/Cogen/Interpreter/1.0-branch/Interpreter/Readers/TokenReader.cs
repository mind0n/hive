using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptInterpreter.DataSchema;

namespace ScriptInterpreter
{
	class StringTokenReader
	{
		public TokenCollection Tokens = new TokenCollection();
		public int LineNumber = 1;

		protected string rawScript;
		protected char[] cache;
		protected int index;
		protected int cacheIndex = -1;


		public StringTokenReader(string script)
		{
			rawScript = script;
			index = 0;
			cache = new char[3];
		}

		public Token ReadNextToken()
		{
			if (!string.IsNullOrEmpty(rawScript) && index >= 0 && index < rawScript.Length)
			{
				Token rlt = new Token(Tokens);

				for (; index <= rawScript.Length; index++)
				{
					if (cacheIndex < 1)
					{
						cacheIndex++;
					}
					if (index == rawScript.Length)
					{
						cache[cacheIndex] = '\0';
					}
					else
					{
						cache[cacheIndex] = rawScript[index];
						if (cache[0] == '\r' && cache[1] == '\n')
						{
							LineNumber++;
						}
						if (cacheIndex == 1 && index + 1 < rawScript.Length)
						{
							cache[cacheIndex + 1] = rawScript[index + 1];
							cacheIndex++;
							index++;
						}
					}
					if (rlt.Read(cache, index < rawScript.Length))
					{
						if ((cache[1] == '"' && rlt.TokenType == TokenType.String)
							|| (cacheIndex == 2 && (cache[1] == ' ' && rlt.TokenType != TokenType.String && rlt.TokenType != TokenType.Comment))
							|| (rlt.TokenType == TokenType.Comment && "/*".Equals(rlt.StartOpCode)))
						{
							index++;
							cache[0] = cache[1];
							cache[1] = cache[2];
						}
						break;
					}
					if (cacheIndex == 2)
					{
						cache[0] = cache[1];
						cache[1] = cache[2];
					}
				}

				if (Keywords.Contains(rlt.Content))
				{
					rlt.TokenType = TokenType.Keyword;
				}
				else if (rlt.TokenType == TokenType.NewLine)
				{
					rlt.TokenType = TokenType.Ignore;
				}
				AppendToken(rlt);
				return rlt;
			}
			else
			{
				Token rlt = new Token(null);
				if (index == rawScript.Length && cache[1] != '\0')
				{
					cache[2] = '\0';
					rlt.Read(cache, false);
					index++;
					AppendToken(rlt);
					return rlt;
				}
				else
				{
					return null;
				}
			}
		}

		private void AppendToken(Token rlt)
		{
			if (rlt.TokenType != TokenType.Ignore)
			{
				Tokens.Add(rlt);
			}
		}
	}
}
