using Joy.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HostNode.Instances
{
    public class DataInstance : Instance
    {
        public string Menu()
        {
            Thread.Sleep(1500);
            return "{ value:'Menu Data' }";
        }
    }
}
