using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Joy.Web.Mvc
{
    public class ViewLocationCache : IViewLocationCache
    {
        private IViewLocationCache cache;
        public ViewLocationCache(IViewLocationCache locache)
        {
            cache = locache;
        }
        public string GetViewLocation(System.Web.HttpContextBase httpContext, string key)
        {
            var r = cache.GetViewLocation(httpContext, key);
            return r;
        }

        public void InsertViewLocation(System.Web.HttpContextBase httpContext, string key, string virtualPath)
        {
            cache.InsertViewLocation(httpContext, key, virtualPath);
        }
    }
}
