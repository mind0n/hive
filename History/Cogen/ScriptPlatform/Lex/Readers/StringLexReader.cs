
using Lex.DataSchema;
using Lex.Roles;
namespace Lex
{
	public class StringLexReader : Reader<string, char>
	{
		public override Element<char> CreateElement()
		{
			Word rlt = new Word();
			rlt.Cache = (Word[]) cache;
			return rlt;
		}
		public override void CreateCache()
		{
			cache = new Word[3];
			for (int i = 0; i < 3; i++)
			{
				cache[i] = CreateElement();
			}
		}
		public override Element<char> ReadNext()
		{
			var rlt = base.ReadNext();
			LexRoles.Format((Word[])cache);
			return rlt;
		}
	}
}
