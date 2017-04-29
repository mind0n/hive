using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreJson
{
	public class Invoker
	{
		//static Dictionary<string, string> cache = new Dictionary<string, string>();
		static Dictionary<string, MethodInfo> mcache = new Dictionary<string, MethodInfo>();

		public Ta Invoke<Ta>(object o, string aname, Hashtable args) where Ta : class
		{
			try
			{
				if (o == null)
				{
					return default(Ta);
				}
				var key = $"{o.GetType().FullName}_{aname}";
				var mi = GetMethodInfo<Ta>(o.GetType(), aname);
				if (mi == null)
				{
					return default(Ta);
				}
				var v = mi.Call(o, false, args);
				return (Ta)v;
			}
			catch (Exception ex)
			{
				ex.Handle();
				return default(Ta);
			}
		}

		public Ta Invoke<Ta>(object o, string aname, params object[] args) where Ta : class
		{
			try
			{
				if (o == null)
				{
					return default(Ta);
				}
				var key = $"{o.GetType().FullName}_{aname}";
				var mi = GetMethodInfo<Ta>(o.GetType(), aname);
				if (mi == null)
				{
					return default(Ta);
				}
				var v = mi.Call(o, false, args);
				return (Ta)v;
			}
			catch (Exception ex)
			{
				ex.Handle();
				return default(Ta);
			}
		}

		private static MethodInfo GetMethodInfo<Ta>(Type type, string aname) where Ta : class
		{
			MethodInfo mi = null;
			if (mcache.ContainsKey(aname))
			{
				mi = mcache[aname];
			}
			else
			{
				mi = type.GetMethodByName(aname);
				if (mi != null)
				{
					mcache[aname] = mi;
				}
			}
			return mi;
		}
	}
}
