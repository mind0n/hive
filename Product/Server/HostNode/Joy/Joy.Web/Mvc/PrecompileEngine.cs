using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Joy.Web.Mvc
{
    public class PrecompileEngine : VirtualPathProviderViewEngine, IVirtualPathFactory
    {
        private Dictionary<string, Type> types = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
        public PrecompileEngine()
        {
            this.ViewLocationCache = new ViewLocationCache(this.ViewLocationCache);
            base.AreaViewLocationFormats = new[] {
                "~/Areas/{2}/Views/{1}/{0}.cshtml", 
                "~/Areas/{2}/Views/Shared/{0}.cshtml", 
            };

            base.AreaMasterLocationFormats = new[] {
                "~/Areas/{2}/Views/{1}/{0}.cshtml", 
                "~/Areas/{2}/Views/Shared/{0}.cshtml", 
            };

            base.AreaPartialViewLocationFormats = new[] {
                "~/Areas/{2}/Views/{1}/{0}.cshtml", 
                "~/Areas/{2}/Views/Shared/{0}.cshtml", 
            };
            base.ViewLocationFormats = new[] {
                "~/Views/{1}/{0}.cshtml", 
                "~/Views/Shared/{0}.cshtml", 
            };
            base.MasterLocationFormats = new[] {
                "~/Views/{1}/{0}.cshtml", 
                "~/Views/Shared/{0}.cshtml", 
            };
            base.PartialViewLocationFormats = new[] {
                "~/Views/{1}/{0}.cshtml", 
                "~/Views/Shared/{0}.cshtml", 
            };
            base.FileExtensions = new[] {
                "cshtml", 
            };

        }

        public static PrecompileEngine CreateEngine(ViewEngineCollection engines)
        {
            var en = new PrecompileEngine();
            engines.Insert(0, en);
            VirtualPathFactoryManager.RegisterVirtualPathFactory(en);
            return en;
        }

        public void AddTypes(params Type[] typelist)
        {
            foreach (var i in typelist)
            {
                var attr = i.GetCustomAttribute<PageVirtualPathAttribute>(true);
                if (attr != null && !string.IsNullOrEmpty(attr.VirtualPath))
                {
                    types[attr.VirtualPath] = i;
                }
                else
                {
                    types[i.Name] = i;
                }
            }
        }

        public void AddTypes(Assembly asm)
        {
            var types = asm.GetTypes();
            AddTypes(types);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            var v = new PrecompileView(FetchUrlType(viewPath), viewPath);
            return v;
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            var v = new PrecompileView(FetchUrlType(partialPath), partialPath);
            return v;
        }

        public object CreateInstance(string virtualPath)
        {
            var t = FetchUrlType(virtualPath);
            var o = DependencyResolver.Current.GetService(t);
            return o;
        }

        private VirtualPathFilter filter = new VirtualPathFilter();
        private Type FetchUrlType(string virtualPath)
        {
            //var r = new Regex("/(?<vasm>\\w*)[-]?(?<vname>\\w*).cshtml", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            filter.Clear();
            filter.AddRule("~/(?<vasm>[\\w|.]*)[-](?<vname>\\w+[/|\\w]*).cshtml");
            filter.AddRule("~/(?<vname>\\w+[/|\\w]*).cshtml");
            var m = filter.Match(virtualPath);
            if (m != null && m.Success)
            {
                var g = m.Groups["vname"];
                var ga = m.Groups["vasm"];
                var v = string.Format("~/{0}.cshtml", string.IsNullOrEmpty(g.Value) ? ga.Value : g.Value);
                if (types.ContainsKey(v))
                {
                    var t = types[v];
                    return t;
                }
            }
            return null;
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            return Exists(virtualPath);
        }

        public bool Exists(string virtualPath)
        {
            return FetchUrlType(virtualPath) != null;
        }
    }
}
