using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Joy.Web.Mvc
{
    public class VirtualPathFilter : List<Regex>
    {
        private VirtualPathFilter self;
        public VirtualPathFilter()
        {
            self = this;
        }
        public void AddRule(string condition)
        {
            self.Add(new Regex(condition, RegexOptions.Singleline | RegexOptions.IgnoreCase));
        }
        public Match Match(string virtualPath)
        {
            foreach (var i in self)
            {
                var m = i.Match(virtualPath);
                if (m.Success)
                {
                    return m;
                }
            }
            return null;
        }
    }
}
