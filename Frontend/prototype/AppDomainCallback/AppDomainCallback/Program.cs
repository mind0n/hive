using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Act.Repository;
using AppDomainContracts;
using System.Threading;
using Act.Core;

namespace Startup
{
	partial class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
			dynamic settings = new Dobj(new DobjSettings { AutoCreate = true });
			settings.name = "TestContainer";
			settings.basedir = AppDomain.CurrentDomain.BaseDirectory;
			settings.pattern = "addin.dll";
			settings.entry = ".ConsoleAddin";
			settings.settings.port = 80;
			var rp = new FolderRepository(settings);
			rp.Load();
			Console.WriteLine("Press any key to unload ...");
			Console.ReadKey();
			rp.Unload();
			Console.WriteLine("Press any key to exit ...");
			Console.ReadKey();

			//HappyPath();
			//Test();
		}

	}
}
