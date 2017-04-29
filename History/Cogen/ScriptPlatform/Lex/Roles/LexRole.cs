using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lex.DataSchema;

namespace Lex.Roles
{
	public class LexRoleUnit : RoleUnit<Word>
	{
		public string Content;
		public Types Type = Types.NotSpecified;
		public override bool Match(Word input)
		{
			return false;	
		}
	}

	public class LexRole : Role<IEnumerable<Word>, Word>
	{
		public LexRole(Action<MatchResult> callback, int offset, params RoleUnit<Word> [] units)
			: base(callback, offset, units)
		{
		}
	}

	public class LexRoles 
	{
		protected static List<LexRole> roles = new List<LexRole>();
		static LexRoles()
		{
			roles.Add(new LexRole(CommentsHandler, 0, new LexRoleUnit { Content = "/" }, new LexRoleUnit { Content = "/" }));
			roles.Add(new LexRole(CommentsHandler, 0, new LexRoleUnit { Content = "/" }, new LexRoleUnit { Content = "*" }));
		}
		private static void CommentsHandler(MatchResult result)
		{
			List<Word> input = result.Input as List<Word>;
		}
		public static void Format(Word[] tokens)
		{
			foreach (LexRole role in roles)
			{
				role.Match(tokens);
			}
		}

	}
	public class TokenRoleUnit:RoleUnit<Token>
	{
		public char Content;
		//public Types Type = Types.NotSpecified;
		public override bool Match(Token input)
		{
			bool rlt = false;
			if (Content == 0 || input.Content == Content)
			{
				rlt = true;
			}
			else
			{
				rlt = false;
			}
			//if (Type == Types.NotSpecified || Type == input.Type)
			//{
			//    rlt = true;
			//}
			//else
			//{
			//    rlt = false;
			//}
			return rlt;

		}
	}
	public class TokenRole : Role<IEnumerable<Token>, Token>
	{
		public TokenRole(Action<MatchResult> callback, int offset, params TokenRoleUnit[] units)
			: base(callback, offset, units)
		{
		}
	}
	public class TokenRoles
	{
		protected static List<TokenRole> roles = new List<TokenRole>();
		static TokenRoles()
		{
			roles.Add(new TokenRole(TokenHandler, 0, new TokenRoleUnit ()));
		}
		private static void TokenHandler(MatchResult result)
		{
			Token[] tokens = result.Input as Token[];
			Token ch = tokens[result.Index + result.Offset];
			if (ch.Content == 0)
			{
			}
		}
		public static void Format(Token[] tokens)
		{
			foreach (TokenRole role in roles)
			{
				role.Match(tokens);
			}
		}

	}
}
