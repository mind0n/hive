using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Infrastructure;
using Core;

namespace PluginA
{
    public class Entry
    {
	    public static string Name { get; } = "Addin A";

	    static PassiveNode node;

		public static void Start()
		{
			node = new PassiveNode();
			Dobj d = new Dobj(new { Port = 20000, Threads = 1, Handler = "Infrastructure.DomainSocketHandler,Infrastructure" });
			node.Listen(d);
		}

		public static void Stop()
		{
			node.Dispose();
		}
	}
}
