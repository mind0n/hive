using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
using System.Web;

namespace Joy.Server.Data
{
	public class Storage : Dictionary<string, object>
	{
		protected static TimeSpan DefaultDuration = TimeSpan.FromMinutes(20);
		protected Cache cache;
		public Storage()
		{
			cache = HttpContext.Current.Cache;
		}
		public static T GetInstance<T>(string id) where T : Storage, new()
		{
			Cache c = HttpContext.Current.Cache;
			if (!c.Exists(id))
			{
				T rlt = new T();
				c.Insert(id, rlt, null, DateTime.MaxValue, DefaultDuration);
				return rlt;
			}
			else
			{
				return c[id] as T;
			}
		}
	}
}
