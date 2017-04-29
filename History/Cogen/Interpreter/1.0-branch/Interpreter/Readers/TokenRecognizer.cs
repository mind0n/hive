using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptInterpreter.Elements;

namespace ScriptInterpreter.Readers
{
	class RuleItem
	{
		public TokenType Type = TokenType.NotSpecified;
		public string Content;
		public string StackContent;
		public TokenType StackType = TokenType.NotSpecified;
		public bool IsEffective
		{
			get
			{
				return !(Type == TokenType.NotSpecified && string.IsNullOrEmpty(Content));
			}
		}
		public bool IsMatch(Token token, Group<Token> stack)
		{
			if (token == null)
			{
				return false;
			}
			bool[] rlt = new bool[4];
			if (Type == TokenType.NotSpecified || token.TokenType == Type)
			{
				rlt[0] = true;
			}
			if (string.IsNullOrEmpty(Content) || Content.Equals(token.Content, StringComparison.Ordinal))
			{
				rlt[1] = true;
			}
			Token viewed = stack.View();
			if (string.IsNullOrEmpty(StackContent) || (viewed != null && viewed.Content == StackContent))
			{
				rlt[2] = true;
			}
			if (StackType == TokenType.NotSpecified || (viewed != null && StackType == viewed.TokenType))
			{
				rlt[3] = true;
			}
			bool result = true;
			foreach (bool r in rlt)
			{
				result &= r;
			}
			return result;
		}
	}
	class Rule
	{
		public MatchResult Result = new MatchResult { Index = -1, Type = TokenType.NotSpecified, IsMatch = false };
		public int Offset;
		protected List<RuleItem> list = new List<RuleItem>();
		public Rule(params RuleItem[] items)
		{
			list.AddRange(items);
		}
		public bool IsMatch(Token[] tokens, Group<Token> stack, int startPos = 0)
		{
			if (Offset + list.Count > tokens.Length)
			{
				return false;
			}
			bool rlt;
			RuleItem ri = list[0];
			for (int i = startPos; i < tokens.Length - list.Count; i++)
			{
				Token token = tokens[i];
				if (ri.IsMatch(token, stack))
				{
					rlt = true;
					for (int j = 1; j < list.Count; j++)
					{
						ri = list[j];
						if (!ri.IsMatch(tokens[i + j], stack))
						{
							rlt = false;
							break;
						}
					}
					if (rlt)
					{
						Result.Index = i + Offset;
						Result.IsMatch = true;
						return true;
					}
				}

			}
			return false;
		}
	}

	class Rules : List<Rule>
	{

		public void Add(TokenType result, int offset, params RuleItem[] items)
		{
			Rule rule = new Rule(items);
			rule.Offset = offset;
			rule.Result.Type = result;
			Add(rule);
		}
		public void Format(Token[] tokens, Group<Token> stack)
		{
			foreach (Rule rule in this)
			{
				if (rule.IsMatch(tokens, stack))
				{
					tokens[rule.Result.Index].TokenType = rule.Result.Type;
				}
			}
		}
	}
	class TokenRecognizer
	{
		public Rules Rules = new Rules();
		protected Group<Token> stack = new Group<Token>();
		public TokenRecognizer()
		{
			Rules.Add(TokenType.ParamStart, 1, new RuleItem { Type = TokenType.FuncCall }, new RuleItem { Content = "(" });
			Rules.Add(TokenType.ParamStart, 1, new RuleItem { Type = TokenType.FuncDef }, new RuleItem { Content = "(" });
			Rules.Add(TokenType.ParamEnd, 0, new RuleItem { StackType = TokenType.ParamStart, Content = ")" });
			Rules.Add(TokenType.BlockEnd, 0, new RuleItem { StackType = TokenType.ParamEnd, Content = "}" });
			Rules.Add(TokenType.Comment, 0, new RuleItem { Content = "/" }, new RuleItem { Type = TokenType.Comment });
		}
		protected Token[] cache = new Token[3];
		protected byte pos = 2;

		public void Recognize(Token token)
		{
			cache[0] = cache[1];
			cache[1] = cache[2];
			cache[2] = token;
			int p = 1;
			if (cache[p] != null)
			{
				if (cache[p].Content == "(")
				{
					stack.Push(cache[p]);
				}
				else if (cache[p].Content == "{")
				{
					stack.Push(cache[p]);
				}
			}
			Rules.Format(cache, stack);
			if (cache[p] != null)
			{
				Token poped;
				if (cache[p].Content == ")")
				{
					poped = stack.Pop();
					if (poped == null || poped.Content != "(")
					{
						throw Interpreter.Raise("Missing '('");
					}
				}
				else if (cache[p].Content == "}")
				{
					poped = stack.Pop();
					if (poped == null || poped.Content != "{")
					{
						throw Interpreter.Raise("Missing '{'");
					}
				}
			}
		}
	}
	class MatchResult
	{
		public int Index;
		public TokenType Type;
		public bool IsMatch = false;
		public static MatchResult NotMatch = new MatchResult { IsMatch = false };
	}
}
