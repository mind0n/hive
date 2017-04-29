using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Joy.Web.Mvc
{
    public class SimpleCache : Dictionary<string, object>
    {
        public static SimpleCache Instance = new SimpleCache();
        private SimpleCache self{get { return this; }}
        public object Get(string key)
        {
            if (self.ContainsKey(key))
            {
                return self[key];
            }
            return null;
        }

    }
}