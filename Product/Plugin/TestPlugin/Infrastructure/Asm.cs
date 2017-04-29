using System;
using System.Collections.Generic;
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

		public static AppDomain Domain(string name)
		{
			var dir = AppDomain.CurrentDomain.BaseDirectory;
			var sdir = dir + "addins\\";
			var domain = AppDomain.CreateDomain(name, AppDomain.CurrentDomain.Evidence, dir, sdir, false); // AppDomain.CreateDomain(name, AppDomain.CurrentDomain.Evidence, new AppDomainSetup { ApplicationBase = AppDomain.CurrentDomain.BaseDirectory }, new PermissionSet(PermissionState.Unrestricted));
			return domain;
		}

		public static void Load(AppDomain domain, string file, Action<Assembly> callback)
		{
			Asm asm = new Asm() { asmfile = file, handler = callback };
			try
			{
				domain.DoCallBack(new CrossAppDomainDelegate(asm.loadAsmFile));
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Failed to load one of the assembly into domain {domain.FriendlyName}: {ex.Message}");
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
