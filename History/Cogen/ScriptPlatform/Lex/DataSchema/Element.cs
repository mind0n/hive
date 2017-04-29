using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lex
{
	public enum Types : int
	{
		NotSpecified,
		Letter,
		Number,
		Symbol
	}
	public struct ProcessResult
	{
		public bool IsCompleted;
		public static ProcessResult NoResult
		{
			get
			{
				return new ProcessResult { IsCompleted = false };
			}
		}
	}
	public class Element<Tc>
	{
		public virtual bool IsEmpty
		{
			get
			{
				return true;
			}
		}
		public virtual bool IsCacheEmpty
		{
			get
			{
				if (Cache == null)
				{
					return true;
				}
				else
				{
					bool rlt = true;
					foreach (Element<Tc> el in Cache)
					{
						rlt &= el.IsEmpty;
					}
					return rlt;
				}
			}
		}
		public Types Type;
		public Element<Tc>[] Cache;
		
		public virtual ProcessResult Read()
		{
			return new ProcessResult { IsCompleted = false };
		}
		public virtual void Load(Tc ch) { }
	}
}
