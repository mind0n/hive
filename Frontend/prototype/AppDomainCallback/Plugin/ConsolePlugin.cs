using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDomainContracts;

namespace Plugin
{
    public class ConsolePlugin : AppDomainComponent
    {
	    public override void Start(object arg = null)
	    {
			Console.WriteLine($"Console plugin:{AppDomain.CurrentDomain.FriendlyName}");
		}
	}
}
