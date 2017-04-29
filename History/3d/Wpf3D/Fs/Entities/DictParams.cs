using System;
using System.Collections.Generic;
using System.Text;

namespace Fs.Entities
{
	public class DictParams : Dictionary<string, object>
	{
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
