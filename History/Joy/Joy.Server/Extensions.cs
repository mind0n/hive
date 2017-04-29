using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;

namespace Joy.Server
{
	public static class Extensions
	{
		public static bool Exists(this Cache cache, string id)
		{
			if (cache != null)
			{
				return cache[id] != null;
			}
			return false;
		}
		public static string ToJson(this object o)
		{
			JsonSerializer s = new JsonSerializer();
			return s.Serialize(o);
		}
	}
}
