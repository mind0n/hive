using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lex.DataSchema
{
	public class Word : Element<char>
	{
		public new Word[] Cache
		{
			get
			{
				return base.Cache as Word[];
			}
			set
			{
				base.Cache = value;
			}
		}
		public string Content
		{
			get
			{
				return b.ToString();
			}
		}
		public override bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(Content);
			}
		}
		protected StringBuilder b = new StringBuilder();

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
				b.Append(Cache[1].Content);
				return ProcessResult.NoResult;
			}
		}

		public override void Load(char ch)
		{
			b = new StringBuilder();
			b.Append(ch);
		}
	}
}
