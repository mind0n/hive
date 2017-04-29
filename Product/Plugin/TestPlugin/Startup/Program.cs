using Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Startup
{
	class Program
	{
		static DomainLoader loader = new DomainLoader("test");
		static void Main(string[] args)
		{
			string path = AppDomain.CurrentDomain.BaseDirectory + "addins\\";
			var di = new DirectoryInfo(path);
			var addins = di.GetFiles("*Plugin*.dll");
			Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
			foreach (var i in addins)
            {
				loader.LoadFile(i.FullName, (asm) =>
				{
					var target = "Entry";
					var type = FindType(asm, target);
					if (type != null)
					{
						var o = CreateInstance(type);
						Invoke(o, "Start");
					}
				});

			}
			Console.ReadLine();
			loader.Unload();
			Console.WriteLine("Domain unloaded");
			Console.ReadLine();
		}

		private static void BytesLoad(FileInfo i)
		{
			loader.LoadBytes(i.FullName, (asm) =>
			{
				var target = "Entry";
				var type = FindType(asm, target);
				if (type != null)
				{
					var o = CreateInstance(type);
					Invoke(o, "Start");
				}
			});
		}

		private static object Invoke(object o, string action)
		{
			var type = o.GetType();
			var mi = type.GetMethod(action);
			if (mi != null)
			{
				var rlt = mi.Invoke(o, null);
				return rlt;
			}
			return null;
		}

		private static object CreateInstance(Type type)
		{
			if (type != null)
			{
				var o = Activator.CreateInstance(type);
				return o;
			}
			return null;
		}

		private static Type FindType(Assembly asm, string target)
		{
			var type = asm.GetType(target, false, true);
			if (type == null)
			{
				var types = asm.GetTypes();
				if (types != null)
				{
					foreach (var i in types)
					{
						if (i.Name.EndsWith(target, StringComparison.OrdinalIgnoreCase))
						{
							return i;
						}
					}
				}
			}
			return type;
		}

	}



}
