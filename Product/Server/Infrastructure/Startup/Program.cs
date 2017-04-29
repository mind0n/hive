using Infrastructure;
using Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		public static void Main(string [] args)
		{
			Commandline.Handle(args);
		}
	}
}
