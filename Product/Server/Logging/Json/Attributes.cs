using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreJson
{
	public class JsonIgnoreAttribute : Attribute
	{

	}

	public class MethodAttribute : Attribute
	{
		public virtual object[] OnInvoke(object instance, ParameterInfo[] pars, object[] args)
		{
			var opars = new object[pars.Length];
			if (args != null && args.Length > 0)
			{
				for (int i = 0; i < opars.Length; i++)
				{
					if (i < args.Length)
					{
						var v = Convert.ChangeType(args[i], pars[i].ParameterType);
						opars[i] = v;
					}
					else
					{
						break;
					}
				}
			}
			return opars;
		}

		public virtual object[] OnInvoke(object instance, ParameterInfo[] pars, Hashtable args)
		{
			var opars = new object[pars.Length];
			if (args != null)
			{
				for (int i = 0; i < pars.Length; i++)
				{
					var par = pars[i];
					if (args.ContainsKey(par.Name.ToLower()))
					{
						var arg = args[par.Name];
						var v = Convert.ChangeType(arg, pars[i].ParameterType);
						opars[i] = v;
					}

				}
			}
			return opars;
		}
	}
}
