using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PluginB
{
    public class Entry
    {
	    public string Name { get; } = "Addin B";

	    public void Start()
	    {
		    if (Debugger.IsAttached)
		    {
			    Debugger.Break();
		    }
		    else
		    {
				Console.WriteLine(Assembly.GetExecutingAssembly().FullName);
			}
		}
    }
}
