using System;
using System.Collections.Generic;
using System.Text;

namespace Joy.Server.Entities
{
	public class DictParams : Dictionary<string, object>
	{
		public object[] ToObjectArray()
		{
			object[] rlt = null;
			if (this.Count > 0)
			{
				rlt = new object[this.Count];
				int i = 0;
				foreach (object val in Values)
				{
					rlt[i] = val;
					i++;
				}
			}
			return rlt;
		}
		protected T GetValue<T>(string key, T defVal)
		{
			if (ContainsKey(key))
			{
				return (T)this[key];
			}
			return defVal;
		}
	}
}
