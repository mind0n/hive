using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lex.DataSchema
{
	public class Token:Element<char>
	{
		public new Token[] Cache
		{
			get
			{
				return base.Cache as Token[];
			}
			set
			{
				base.Cache = value;
			}
		}
		public char Content = '\0';
		public Types Type = Types.NotSpecified;
		public bool IsEmpty
		{
			get
			{
				return Content == '\0';
			}
		}
		public override ProcessResult Read()
		{
			if (IsCacheEmpty)
			{
				return new ProcessResult { IsCompleted = true };
			}
			if (Cache[1].IsEmpty)
			{
				return ProcessResult.NoResult;
			}
			else
			{
				Content = Cache[1].Content;
				return ProcessResult.NoResult;
			}
		}
	}
}
