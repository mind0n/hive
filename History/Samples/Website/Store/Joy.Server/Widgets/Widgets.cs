using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Joy.Server.Widgets
{
	public class Widget : Dictionary<string, object>
	{
		public T Use<T>(string key, object value = null, bool autoCreate = true) where T : class, new()
		{
			T rlt = default(T);
			if (this.ContainsKey(key))
			{
				rlt = this[key] as T;
			}
			else if (autoCreate || value != null)
			{
				if (value == null)
				{
					rlt = new T();
					this[key] = rlt;
				}
				else
				{
					rlt = this as T;
					this[key] = value;
				}
			}
			return rlt;
		}
	}
	public class WidgetCollection : List<Widget>
	{
		public Widget this[string key, string property]
		{
			get
			{
				foreach (Widget col in this)
				{
					string field = col[property] as string;
					if (!string.IsNullOrEmpty(field) && string.Equals(key, field))
					{
						return col;
					}
				}
				return null;
			}
		}
	}
}
