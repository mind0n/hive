using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joy.Web.Mvc
{
    public class ActionAttribute : Attribute
    {
        public string Url { get; set; }
        public bool IsNoSplit { get; set; }
    }
}
