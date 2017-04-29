using Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
	[Serializable]
	public class Asm
	{
		protected byte[] asmbytes;

		protected string asmfile;

		protected AppDomain domain;

		protected Action<Assembly> handler;

		protected void loadAsmBytes()
		{
			if (handler != null)
			{
				var asm = Assembly.Load(asmbytes);
				if (asm != null)
				{
					handler(asm);
				}
			}
		}

		protected void unloadDomain()
		{
			if (domain != null)
			{
				var dlls = domain.GetAssemblies();
				foreach (var i in dlls)
				{
					try
					{
						if (i.FullName.IndexOf(".entry,", StringComparison.OrdinalIgnoreCase) >= 0)
						{
							log.i($"Unloading entry {i.FullName}");
							handler(i);
						}
					}
					catch (Exception ex)
					{
						log.e(ex);
					}
				}
			}
		}

		protected void loadAsmFile()
		{
			if (handler != null)
			{
				var asm = Assembly.LoadFile(asmfile);
				if (asm != null)
				{
					handler(asm);
				}
			}
		}

		public static AppDomain Domain(string name, string probedir)
		{
			if (!Directory.Exists(probedir))
			{
				Directory.CreateDirectory(probedir);
			}
			if (!probedir.EndsWith("\\"))
			{
				probedir += "\\";
			}
			log.i($"Creating app domain {name} with base: {probedir}");

			var domain = AppDomain.CreateDomain(name, AppDomain.CurrentDomain.Evidence, probedir, probedir, false);
			return domain;
		}

		public static void Unload(AppDomain domain, Action<Assembly> callback)
		{
			if (callback != null)
			{
				Asm asm = new Asm() {domain = domain, handler = callback};
				try
				{
					log.i($"Unloading {domain.FriendlyName}: {domain.BaseDirectory}");
					domain.DoCallBack(new CrossAppDomainDelegate(asm.unloadDomain));
				}
				catch (Exception ex)
				{
					log.i($"Failed to unload domain {domain.FriendlyName}: {ex.Message}");
				}
			}
		}

		public static void Load(AppDomain domain, string file, Action<Assembly> callback)
		{
			Asm asm = new Asm() { asmfile = file, handler = callback };
			try
			{
				domain.DoCallBack(new CrossAppDomainDelegate(asm.loadAsmFile));
				log.i($"Domain {domain.FriendlyName} loaded: {file}");
			}
			catch (Exception ex)
			{
				log.e($"Failed to load one of the assembly into domain {domain.FriendlyName}: {ex.Message}");
			}
		}

		public static void Load(AppDomain domain, byte[] assemblyBytes, Action<Assembly> callback)
		{
			Asm asm = new Asm() { asmbytes = assemblyBytes, handler = callback };
			try
			{
				domain.DoCallBack(new CrossAppDomainDelegate(asm.loadAsmBytes));
			}
			catch (Exception ex)
			{
				var iex = ex;
				while (iex.InnerException != null)
				{
					iex = iex.InnerException;
				}
				Console.WriteLine($"Failed to load one of the assembly into domain {domain.FriendlyName}: {iex.Message}");
			}
		}
	}
}
