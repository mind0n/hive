using Joy.Core;
using Joy.Web.Controllers;
using Joy.Web.Mvc.IO;
using Joy.Web.Mvc.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace Joy.Web.Mvc
{
    public class Router : IRouteHandler, IRequiresSessionState
    {

        private static Router instance;
        public static Router Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Router();
                }
                return instance;
            }
        }
        public static FileCfg<RouterConfig> Config;
        private NotFoundHandler NotFoundHandler = new NotFoundHandler();
        private BypassHandler BypassHandler = new BypassHandler();
        private ForbiddenHandler ForbiddenHandler = new ForbiddenHandler();

        public Router()
        {
        }

        public static void OnBeginRequest(object sender, EventArgs e)
        {
            var p = HttpContext.Current.Request.Url.AbsolutePath;
            if (string.Equals(p, "/", StringComparison.Ordinal) && !string.IsNullOrEmpty(Config.Instance.DefaultPath))
            {
                HttpContext.Current.RewritePath(Config.Instance.DefaultPath);
            }
        }

        public static void OnAppStart(params Assembly[] asms)
        {
            ControllerBuilder.Current.SetControllerFactory(new JoyControllerFactory());
            Identifier.CreateInstance(new FileCfg<AuthConfig>("/configs/auth.json".MapPath()));
            Config = new FileCfg<RouterConfig>("/configs/route.json".MapPath());
            Config.Instance.Init();
            AreaRegistration.RegisterAllAreas();
            BindRoutes("{controller}/{action}/{*args}", "{controller}/{*args}", "{*args}");
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
            GlobalFilters.Filters.Add(new AuthorizeAttribute());
            ViewEngines.Engines.Clear();
            BindViewEngine(asms);
#if DEBUG
            var list = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            foreach (var i in list)
            {
                Logger.Log(i);
            }
#endif
        }

        private static void BindViewEngine(Assembly[] asms)
        {
            var en = PrecompileEngine.CreateEngine(ViewEngines.Engines);
            foreach (var i in asms)
            {
                en.AddTypes(i);
            }
        }

        private static void BindRoutes(params string [] items)
        {
            List<string> defaults = new List<string>();
            defaults.AddRange(items);
            //BindRoutes("{controller}/{action}/{*args}", "{resource}.axd/{*pathInfo}");
            var routes = RouteTable.Routes;
            foreach (var i in defaults)
            {
                var r = new Route(i, Router.Instance);
                routes.Add(r);
            }
        }

        public System.Web.IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            //requestContext.HttpContext.Response.AddHeader("p3p", "CP=\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\"");
            var controller = requestContext.RouteData.Values[WebConstants.const_controller].AsString(true);
            if (Config.Instance.EnableHandlers && Config.Instance.RouteDict != null && Config.Instance.RouteDict.ContainsKey(controller))
            {
                //requestContext.RouteData.Values[const_controller] = defaultHandler[0];
                //requestContext.RouteData.Values[const_action] = defaultHandler[1];
                //var t = typeof(ResHandler).AssemblyQualifiedName;
                var item = Config.Instance.RouteDict[controller];
                if (item != null)
                {
                    var h = item.Handler.CreateInstance() as IRoutable;
                    if (h != null)
                    {
                        item.ParseContext(requestContext, h);
                        return h as IHttpHandler;
                    }
                }
                return NotFoundHandler;
            }
            else
            {
                var mh = new JoyMvcHandler(requestContext);
                return mh;
            }
        }
    }

    [Serializable]
    public class RouterConfig
    {
        public List<string> IgnoreRoutes { get; set; }
        public List<RouteItem> Handlers { get; set; }
        public string Identifier { get; set; }
        public string DefaultPath { get; set; }
        public bool EnableHandlers { get; set; }
        public RouterConfig()
        {
            Identifier = typeof(WebIdentifier).AssemblyQualifiedName;
        }

        [ScriptIgnore]
        public Dictionary<string, RouteItem> RouteDict = new Dictionary<string, RouteItem>(StringComparer.OrdinalIgnoreCase);
        public void Init()
        {
            RouteDict.Clear();
            if (Handlers != null && Handlers.Count > 0)
            {
                foreach (var i in Handlers)
                {
                    RouteDict[i.Name] = i;
                }
            }
        }
    }

    [Serializable]
    public class RouteItem
    {
        public string Name { get; set; }
        public bool IsNoSplit { get; set; }
        public string Handler { get; set; }
        public string RegExp { get; set; }
        public string Action { get; set; }
        public void ParseContext(RequestContext cxt, IRoutable h)
        {
            if (h != null)
            {
                if (string.IsNullOrEmpty(RegExp))
                {
                    h.ActionName = cxt.RouteData.Values[WebConstants.const_action].AsString(true);
                    h.Args = cxt.RouteData.Values[WebConstants.const_params].AsString(true);
                    if (!string.IsNullOrEmpty(Action))
                    {
                        if (!string.IsNullOrEmpty(h.Args))
                        {
                            h.Args = h.ActionName + '/' + h.Args;
                        }
                        else
                        {
                            h.Args = h.ActionName;
                        }
                        h.ActionName = Action;
                    }
                }
                else
                {
                    var m = new Regex(RegExp, RegexOptions.IgnoreCase).Match(cxt.HttpContext.Request.Url.AbsolutePath);
                    if (m.Index >= 0)
                    {
                        h.Groups = m.Groups;
                        var act = m.Groups[WebConstants.const_action];
                        var arg = m.Groups[WebConstants.const_params];
                        if (act != null)
                        {
                            if (string.IsNullOrEmpty(Action))
                            {
                                h.ActionName = act.Value;
                            }
                            else
                            {
                                h.ActionName = Action;
                                h.Args = act.Value + "/" + h.Args;
                            }
                        }
                        if (arg != null)
                        {
                            h.Args = arg.Value;
                        }
                    }
                }
                h.RouteItem = this;
            }
        }
    }

    public class BypassHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }
        public void ProcessRequest(HttpContext context)
        {

        }
    }

    public class ForbiddenHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.StatusCode = 403;
            context.Response.Write("403 Unauthorized access");
        }
    }
    public class NotFoundHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.StatusCode = 404;
            context.Response.Write("404 Page not found");
        }
    }	

}
