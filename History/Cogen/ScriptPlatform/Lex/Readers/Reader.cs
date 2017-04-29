using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Lex
{
	public class Reader<Ts, Tc>
		where Ts:IEnumerable<Tc>
	{
		public List<Element<Tc>> Elements = new List<Element<Tc>>();
		public Element<Tc> Curt;
		protected Element<Tc>[] cache;
		protected int pos;
		protected Ts input;

		public Reader()
		{
			CreateCache();
		}
		public virtual Element<Tc> CreateElement()
		{
			return null;
		}
		public virtual void CreateCache() { }
		public void Load(Ts input)
		{
			this.input = input;
		}
		public T ReadNext<T>() where T : Element<Tc>
		{
			return ReadNext() as T;
		}
		public virtual Element<Tc> ReadNext()
		{
			if (input == null)
			{
				return null;
			}
			if (Curt == null)
			{
				Curt = CreateElement();
				Curt.Cache = cache;
			}
			for (int i = pos; i < input.Count<Tc>(); i++)
			{
				Tc ch;
				if (i < input.Count<Tc>())
				{
					ch = (Tc)input.ElementAt<Tc>(i);
				}
				else
				{
					ch = default(Tc);
				}
				cache[0] = cache[1];
				cache[1] = cache[2];
				cache[2].Load(ch);
				ProcessResult result = Curt.Read();
				if (result.IsCompleted)
				{
					Elements.Add(Curt);
					Curt = null;
					pos = i;
					return Curt;
				}
			}
			return Curt;
		}
	}
}
