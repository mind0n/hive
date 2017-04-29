using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using System.Reflection;
using Logger;

namespace Infrastructure
{
	[Serializable]
	public abstract class Loader
	{
		protected AppDomain domain { get; set; }

		public Loader(string name = null)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				name = Guid.NewGuid().ToString();
			}
			var dir = AppDomain.CurrentDomain.BaseDirectory;
			var sdir = dir + "shadow\\";
			domain = AppDomain.CreateDomain(name, AppDomain.CurrentDomain.Evidence, sdir, sdir, false); // AppDomain.CreateDomain(name, AppDomain.CurrentDomain.Evidence, new AppDomainSetup { ApplicationBase = AppDomain.CurrentDomain.BaseDirectory }, new PermissionSet(PermissionState.Unrestricted));
		}

		public virtual void Load()
		{
			preloadCallback();
			domain.DoCallBack(new CrossAppDomainDelegate(loadCallback));
		}

		public virtual void Unload()
		{
			if (domain != null)
			{
				AppDomain.Unload(domain);
			}
		}

		protected static object Invoke(object o, string action)
		{
			try
			{
				var type = o.GetType();
				var mi = type.GetMethod(action);
				if (mi != null)
				{
					var rlt = mi.Invoke(o, null);
					return rlt;
				}
			}
			catch (Exception ex)
			{
				if (Debugger.IsAttached)
				{
					Debug.WriteLine(ex.ToString());
					Debugger.Break();
				}
			}
			return null;
		}

		protected static object CreateInstance(Type type)
		{
			if (type != null)
			{
				var o = Activator.CreateInstance(type);
				return o;
			}
			return null;
		}

		protected static Type FindType(Assembly asm, string target)
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

		protected abstract void loadCallback();
		protected abstract void preloadCallback();
	}

	[Serializable]
	public class FolderLoader : Loader
	{
		protected string basedir = AppDomain.CurrentDomain.BaseDirectory;
		protected string loadfoldername = "shadow";
		protected string monitorfoldername = "addins";

		public FolderLoader(string name = null) : base(name)
		{
		}

		protected string LoadFolder
		{
			get
			{
				var rlt = $"{basedir}{loadfoldername}\\";
				if (!Directory.Exists(rlt))
				{
					Directory.CreateDirectory(rlt);
				}
                return rlt;
			}
		}

		protected string MonitorFolder
		{
			get
			{
				var rlt = $"{basedir}{monitorfoldername}\\";
				if (!Directory.Exists(rlt))
				{
					Directory.CreateDirectory(rlt);
				}
				return rlt;
			}
		}

		protected override void loadCallback()
		{
			Startup();
		}

		protected override void preloadCallback()
		{
			Preload();
		}

		protected virtual void Preload()
		{
			var dlls = Directory.GetFiles(MonitorFolder);
			foreach (var i in dlls)
			{
				var name = i.PathLastName();
				var fsrc = MonitorFolder + name;
				var fdes = LoadFolder + name;
				if (File.Exists(fdes))
				{
					File.Delete(fdes);
				}
                File.Move(fsrc, fdes);
			}
		}

		protected virtual void Startup()
		{
			var dlls = Directory.GetFiles(LoadFolder, "*.entry.dll");
			foreach (var i in dlls)
			{
				var asm = Assembly.LoadFile(i);
				if (asm != null)
				{
					var type = FindType(asm, "Entry");
					if (type != null)
					{
						var o = CreateInstance(type);
						if (o != null)
						{
							Invoke(o, "Start");
						}
					}
				}
			}
		}
	}
}
