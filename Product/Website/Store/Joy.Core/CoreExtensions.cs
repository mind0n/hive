
using System;
using System.Reflection;
namespace Joy.Core
{
	public static class CoreExtensions
	{
		public static T Create<T>(this Type type)
		{
			if (type != null)
			{
				ConstructorInfo c = type.GetConstructor(Constants.EmptyTypes);
				if (c != null && type.IsSubclassOf(typeof(T)))
				{
					T rlt = (T)c.Invoke(null);
					return rlt;
				}
			}
			return default(T);
		}

		public static object CreateObject(this string typeName, string asmName = null)
		{
			Assembly asm = null;
			if (string.IsNullOrEmpty(typeName))
			{
				return null;
			}
			if (string.IsNullOrEmpty(asmName))
			{
				asm = Assembly.GetExecutingAssembly();
			}
			else
			{
				asm = Assembly.Load(asmName);
			}
			if (asm != null)
			{
				Type type = asm.GetType(typeName);
				if (type == null)
				{
					Type[] types = asm.GetTypes();
					foreach (Type i in types)
					{
						if (string.Equals(i.Name, typeName, StringComparison.OrdinalIgnoreCase))
						{
							type = i;
						}
					}
				}
				if (type != null)
				{
					return type.CreateObject();
				}
			}
			return null;
		}

		public static object CreateObject(this Type type)
		{
			if (type != null)
			{
				ConstructorInfo c = type.GetConstructor(Constants.EmptyTypes);
				if (c != null)
				{
					object rlt = c.Invoke(null);
					if (rlt != null)
					{
						return rlt;
					}
				}
			}
			return null;
		}
		public static string PathLastName(this string path, bool isWithExt = true, char splitter = '\\')
		{
			if (!string.IsNullOrEmpty(path) && !path.EndsWith(splitter.ToString(), StringComparison.Ordinal))
			{
				string[] folders = path.Split(splitter);
				if (folders.Length > 0)
				{
					string rlt = folders[folders.Length - 1];
					if (isWithExt)
					{
						return rlt;
					}
					else
					{
						return rlt.Split('.')[0];
					}
				}
			}
			return string.Empty;
		}
		public static string PathWithoutName(this string path, char splitter = '\\')
		{
			string rlt = string.Empty;
			if (!string.IsNullOrEmpty(path) && !path.EndsWith(splitter.ToString(), StringComparison.Ordinal))
			{
				string[] folders = path.Split(splitter);
				if (folders.Length > 1)
				{
					folders[folders.Length - 1] = string.Empty;
					rlt = string.Join(splitter.ToString(), folders);
				}
				else
				{
					rlt = path;
				}
			}
			if (!rlt.EndsWith(splitter.ToString()))
			{
				rlt += splitter;
			}
			return rlt;
		}
		public static string Left(this string content, int count)
		{
			if (count < 0)
			{
				count = content.Length + count;
			}
			if (count <= 0)
			{
				return string.Empty;
			}
			else if (count > 0 && count < content.Length)
			{
				return content.Substring(0, count);
			}
			else if (count >= content.Length)
			{
				return content;
			}
			return content;
		}
	}
}
