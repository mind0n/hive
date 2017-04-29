using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace PluginA
{
    public class Entry
    {
	    public string Name { get; } = "Addin A";

		public void Start()
		{
			this.D();
		}
	}
}
