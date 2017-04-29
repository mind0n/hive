using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class CoreExtensions
    {
	    public static void D(this object o)
	    {
		    if (Debugger.IsAttached)
		    {
			    Debug.WriteLine(o.ToString());
			    Debugger.Break();
		    }
		    else
		    {
			    Console.WriteLine(Assembly.GetExecutingAssembly().FullName);
		    }
	    }
    }
}
