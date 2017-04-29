using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDomainContracts;

namespace Addin
{
    public class ConsoleAddin : AppDomainComponent
    {
	    public override void Start(object arg = null)
	    {
		    Console.WriteLine($"Console addin: {AppDomain.CurrentDomain.FriendlyName}, {(arg != null?arg.ToString(): "(null)")}");
	    }
    }
}
