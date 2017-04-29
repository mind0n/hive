using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public static class ReflectionExtensions
	{
		static Dictionary<string, Type> TypeCache = new Dictionary<string, Type>();

		public static object CreateInstance(this string typename, AppDomain domain = null, params object[] args)
		{
			object o = null;
			Type type = null;
			var key = $"{typename}";
			if (TypeCache.ContainsKey(key))
			{
				type = TypeCache[key];
			}
			else
			{
				type = Type.GetType(typename, false, true);
			}
			if (type != null)
			{
				o = CreateInstanceNoCache(type, domain ?? AppDomain.CurrentDomain, args);
				if (o != null)
				{
					TypeCache[key] = o.GetType();
				}
				return o;
			}
			return null;
		}

		static object CreateInstanceNoCache(Type type, AppDomain domain, params object[] args)
		{
			try
			{
				if (args != null && args.Length < 1)
				{
					args = null;
				}
				var obj = Activator.CreateInstance(domain, type.Assembly.FullName, type.FullName, true, BindingFlags.Default, null, args,
					CultureInfo.InvariantCulture, null);
				return obj.Unwrap();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				if (Debugger.IsAttached)
				{
					Debugger.Break();
				}
			}
			return null;
		}

		public static Type FindType(this Assembly asm, string name)
		{
			if (asm != null)
			{
				var types = asm.GetTypes();
				foreach (var i in types)
				{
					if (i.FullName.EndsWith(name, StringComparison.OrdinalIgnoreCase))
					{
						return i;
					}
				}
			}
			return null;
		}

		static Dictionary<string, MethodInfo> methodcache = new Dictionary<string, MethodInfo>(); 
		public static object GetAction(this Type type, string name, Func<MethodInfo, object> callback)
		{
			if (callback == null)
			{
				return null;
			}
			if (type != null && !string.IsNullOrWhiteSpace(name))
			{
                var mi = $"{type.Assembly.FullName}#{type.FullName}#{name}".FromCache<MethodInfo>(methodcache, (k, d) =>
                {
					var m = type.GetMethod(k);
	                if (m == null)
	                {
						var ms = type.GetMethods();
						foreach (var i in ms)
						{
							if (string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase))
							{
								return i;
							}
						}
					}
					return m;
                });
				if (mi != null)
				{
					return callback(mi);
				}
			}
			return null;
		}

		public static object DirectInvoke(this Type type, string method, object instance = null, params object[] args)
		{
			var result = GetAction(type, method, (mi) =>
			{
				var rlt = mi.Invoke(instance, args);
				return rlt;
			});
			return result;
		}

		public static object Invoke(this Type type, string method, object instance = null, params object[] args)
		{
			var result = GetAction(type, method, (mi) =>
			{
				var arguments = ConvertArgs(args, mi);
				var rlt = mi.Invoke(instance, arguments.ToArray());
				return rlt;

			});
			return result;
		}

		public static object Invoke(this Type type, string method, object instance = null, IDictionary dict = null)
		{
			var result = GetAction(type, method, (mi) =>
			{
				var arguments = ConvertArgs(dict, mi);
				var rlt = mi.Invoke(instance, arguments.ToArray());
				return rlt;

			});
			return result;
		}

		private static List<object> ConvertArgs(IDictionary args, MethodInfo mi)
		{
			var arguments = new List<object>();
			if (args != null && args.Count > 0)
			{
				var pars = mi.GetParameters();
				for (int i = 0; i < pars.Length; i++)
				{
					var pitem = pars[i];
					object value = null;
					if (args.Contains(pitem.Name))
					{
						value = args[pitem.Name];
					}
					else if (args.Contains(pitem.Name.ToLower()))
					{
						value = args[pitem.Name.ToLower()];
					}
					else
					{
						return null;
					}
					value = Convert.ChangeType(value, pitem.ParameterType);
					arguments.Add(value);
				}
			}
			return arguments;
		}

		private static List<object> ConvertArgs(object[] args, MethodInfo mi)
		{
			var arguments = new List<object>();
			if (args != null && args.Length > 0)
			{
				var pars = mi.GetParameters();
				for (int i = 0; i < pars.Length; i++)
				{
					if (i < args.Length)
					{
						object item = null;
						var rawarg = args[i];
						if (rawarg != null)
						{
							item = Convert.ChangeType(rawarg, pars[i].ParameterType);
						}
						arguments.Add(item);
					}
				}
			}
			return arguments;
		}

		public static MethodInfo GetMethodByName(this Type type, string name)
		{
			MethodInfo[] list = type.GetMethods();
			foreach (var i in list)
			{
				if (string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			return null;
		}

		public static object Call(this MethodInfo info, object instance, bool forceAttr = false, Hashtable args = null)
		{
			object r = null;
			if (info is MethodInfo)
			{
				var mi = (MethodInfo)info;
				try
				{
					var pars = mi.GetParameters();
					object[] opars = null;
					MethodAttribute attr = null;
					if (!mi.HasAttr<MethodAttribute>(out attr))
					{
						if (!forceAttr)
						{
							attr = new MethodAttribute();
						}
						else
						{
							throw new MissingMethodException(mi.ReflectedType.Name, mi.Name);
						}
					}
					opars = attr.OnInvoke(instance, pars, args);
					if (mi.ReturnType != typeof(void))
					{
						r = mi.Invoke(instance, opars);
						if (r == null)
						{
							return mi.ReturnType.DefaultVal();
						}
						return r;
					}
					else
					{
						mi.Invoke(instance, opars);
						return mi.ReturnType.DefaultVal();
					}
				}
				catch (Exception ex)
				{
					Handle(ex);
					return mi.ReturnType.DefaultVal();
				}
			}
			return null;
		}

		public static object Call(this MemberInfo info, object instance, bool forceAttr = false, params object[] args)
		{
			object r = null;
			if (info is MethodInfo)
			{
				var mi = (MethodInfo)info;
				try
				{
					var pars = mi.GetParameters();
					object[] opars = null;
					MethodAttribute attr = null;
					if (!mi.HasAttr<MethodAttribute>(out attr))
					{
						if (!forceAttr)
						{
							attr = new MethodAttribute();
						}
						else
						{
							throw new MissingMethodException(mi.ReflectedType.Name, mi.Name);
						}
					}
					opars = attr.OnInvoke(instance, pars, args);
					if (mi.ReturnType != typeof(void))
					{
						r = mi.Invoke(instance, opars);
						if (r == null)
						{
							return mi.ReturnType.DefaultVal();
						}
						return r;
					}
					else
					{
						mi.Invoke(instance, opars);
						return mi.ReturnType.DefaultVal();
					}
				}
				catch (Exception ex)
				{
					Handle(ex);
					return mi.ReturnType.DefaultVal();
				}
			}
			else if (info is PropertyInfo)
			{
				var pi = (PropertyInfo)info;
				try
				{
					if (pi.CanRead && (args == null || args.Length < 1))
					{
						r = pi.GetValue(instance, null);
						if (r == null)
						{
							return pi.PropertyType.DefaultVal();
						}
						return r;
					}
					if (pi.CanWrite)
					{
						pi.SetValue(instance, args[0], null);
					}
				}
				catch
				{
					return pi.PropertyType.DefaultVal();
				}
			}
			else if (info is FieldInfo)
			{
				var fi = (FieldInfo)info;
				try
				{
					if (args == null || args.Length < 1)
					{
						r = fi.GetValue(instance);
						if (r == null)
						{
							return fi.FieldType.DefaultVal();
						}
						return r;
					}
					fi.SetValue(instance, args[0]);
					return fi.FieldType.DefaultVal();
				}
				catch
				{
					return fi.FieldType.DefaultVal();
				}
			}
			return null;
		}

		public static object DefaultVal(this Type t)
		{
			if (t != null && t.IsValueType)
			{
				return Activator.CreateInstance(t);
			}
			return null;
		}

		public static bool HasAttr<T>(this MemberInfo info, out T rlt) where T : class
		{
			if (info == null)
			{
				rlt = default(T);
				return false;
			}
			T[] o;
			var r = info.HasAttr(out o);
			if (o != null)
			{
				rlt = o[0];
			}
			else
			{
				rlt = default(T);
			}
			return r;
		}

		public static bool HasAttr<T>(this MemberInfo info, out T[] rlt) where T : class
		{
			var atts = info.GetCustomAttributes(typeof(T), true);
			if (atts != null && atts.Length > 0)
			{
				rlt = new T[atts.Length];
				for (int i = 0; i < rlt.Length; i++)
				{
					rlt[i] = atts[i] as T;
				}
				return true;
			}
			rlt = null;
			return false;
		}

		public static void Handle(this Exception ex)
		{
			Debug.WriteLine(ex.ToString());
			if (Debugger.IsAttached)
			{
				Debugger.Break();
			}
		}

		public static Dictionary<string, object> ToDict(this object o)
		{
			var rlt = new Dictionary<string, object>();
			if (o != null)
			{
				ToDictInternal(o, rlt);
			}
			return rlt;
		}

		private static void ToDictInternal(object o, Dictionary<string, object> dict)
		{
			var ps = o.GetType().GetProperties();
			foreach (var i in ps)
			{
				if (i.CanRead)
				{
					var v = i.GetValue(o);
					if (v == null || v.GetType().IsValueType || v is string)
					{
						dict[i.Name] = v;
					}
					else
					{
						var d = new Dictionary<string, object>();
						dict[i.Name] = d;
						ToDictInternal(v, d);
					}
				}
			}
			var fs = o.GetType().GetFields();
			foreach (var i in fs)
			{
				var v = i.GetValue(o);
				if (v == null || v.GetType().IsValueType || v is string)
				{
					dict[i.Name] = v;
				}
				else
				{
					var d = new Dictionary<string, object>();
					dict[i.Name] = d;
					ToDictInternal(v, d);
				}
			}
		}
	}
}
