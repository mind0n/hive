using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptInterpreter.Opcodes;

namespace ScriptInterpreter
{
	class Keywords
	{
		static Dictionary<string, Keyword> keywords = new Dictionary<string, Keyword>();
		private Interpreter interpreter;
		static Keywords()
		{
			keywords["var"] = new KwVar();
		}
		public static bool Contains(string keyword)
		{
			return keywords.ContainsKey(keyword);
		}
		public static void Add(Keyword keyword)
		{
			keywords[keyword.Name] = keyword;
		}
		public static Keyword CreateInstance(string keyword)
		{
			if (keywords.ContainsKey(keyword)){
				return keywords[keyword].CreateInstance();	
			}
			throw new Exception("Invalid keyword: " + keyword);
		}
		public Keywords(Interpreter curtInterpreter)
		{
			interpreter = curtInterpreter;
		}
	}
}
