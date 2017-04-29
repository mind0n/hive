using Joy.Core;
using Joy.Users;
using Joy.Users.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;

namespace Joy.Server.Web
{
	[Serializable]
	public class Dispatcher : IRouteHandler, IRequiresSessionState
	{
	    private static string configpath = "/Configs".MapPath();
	    private static string filename = "routes.json";
	    private static string configfile = string.Format("{0}\\{1}", configpath, filename);
        private static object locker = new object();
	    private static Dispatcher instance;
		public static Dispatcher Instance
		{
			get { return instance ?? (instance = LoadRoutes()); }
		}

		public char Splitter = '/';
	    public bool EnableAnonymousAccess;
	    public bool EnableAutoAddRole;
	    public bool EnableAuthService;

		public Dictionary<string, RouteInfo> Mappings = new Dictionary<string, RouteInfo>();
		protected IHttpHandler NotFoundHandler;
		protected IHttpHandler BypassHandler;
	    protected IHttpHandler ForbiddenHandler;

	    static Dispatcher()
	    {
	        instance = LoadRoutes();
            FileSystemWatcher w = new FileSystemWatcher(configpath);
            w.Changed += w_Changed;
	        w.NotifyFilter = NotifyFilters.LastWrite;
	        w.EnableRaisingEvents = true;
	    }

        static void w_Changed(object sender, FileSystemEventArgs e)
        {
            FileSystemWatcher w = sender as FileSystemWatcher;
            if (w != null)
            {
                w.EnableRaisingEvents = false;
                LoadRoutes();
                MapHandler();
                w.NotifyFilter = NotifyFilters.LastWrite;
                w.EnableRaisingEvents = true;
            }
        }
		public Dispatcher()
		{
			NotFoundHandler = new NotFoundHandler();
			BypassHandler = new BypassHandler();
            ForbiddenHandler = new ForbiddenHandler();
		}
		public static Dispatcher LoadRoutes()
		{
		    lock (locker)
		    {
		        try
		        {
		            string file = configfile;
		            Dispatcher rlt = null;
		            if (File.Exists(file))
		            {
		                string json = File.ReadAllText(file);
		                if (!string.IsNullOrEmpty(json))
		                {
		                    rlt = json.FromJson<Dispatcher>();
		                }
		            }
		            else
		            {
		                rlt = new Dispatcher();
		                File.WriteAllText(file, rlt.ToJson());
		            }
		            if (rlt != null)
		            {
		                if (rlt.EnableAuthService)
		                {
                            rlt.Mappings.Add("auth", new RouteInfo { TypeName = typeof(AuthService).FullName });
                        }
		                foreach (KeyValuePair<string, RouteInfo> i in rlt.Mappings)
		                {
		                    RouteInfo info = i.Value;
		                    info.LoadType();
		                }
		            }
		            else
		            {
		                rlt = new Dispatcher();
		            }
		            instance = rlt;
	                URoles.Instance.EnableAnonymousAccess = rlt.EnableAnonymousAccess;
		            URoles.Instance.EnableAutoAddRole = rlt.EnableAutoAddRole;
		            return rlt;
		        }
		        catch
		        {
                    instance = new Dispatcher();
		            return instance;
		        }
		    }
		}
		public static void MapHandler()
		{
            RouteTable.Routes.Clear();
			RouteTable.Routes.Add(new Route("{handler}/{action}/{*Params}", Dispatcher.Instance));
			RouteTable.Routes.Add(new Route("{handler}/{*Params}", Dispatcher.Instance));
			RouteTable.Routes.Add(new Route("{*Params}", Dispatcher.Instance));
		}

		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			string handler = requestContext.RouteData.Values["handler"].AsString(true);

		    if (!string.IsNullOrEmpty(handler))
			{
				if (Mappings.ContainsKey(handler))
				{
					RouteInfo info = Mappings[handler];
					if (info != null)
					{
                        var wis = new WebPPLService(requestContext);
					    var p = wis.GetCurrent();
					    if (info.Authenticate(p))
					    {
					        return CreateHandler(requestContext, info);
					    }
					    else
					    {
					        return ForbiddenHandler;
					    }
					}
				}
			}
			return NotFoundHandler;
		}

        private IHttpHandler CreateHandler(RequestContext requestContext, RouteInfo info)
		{
            //if (!Authenticate(requestContext))
            //{
            //    return NotFoundHandler;
            //}
			string[] par = null;
			object p = requestContext.RouteData.Values[ServerConst.Params];
			if (p != null)
			{
				if (info.IsNoSplit)
				{
					par = new string[] {p.ToString()};
				}
				else
				{
					par = p.ToString().Split(Splitter);
				}
			}
			Type type = info.Type;
			if (type == null)
			{
				info.LoadType();
			}
			if (type != null)
			{
				IRoutable h = Activator.CreateInstance(type) as IRoutable;
				if (h != null)
				{
					string action = requestContext.RouteData.Values["action"].AsString();
					h.Params = par;
					h.RouteInfo = info;
					h.RequestContext = requestContext;
					h.Intent = action == null ? null : type.GetMethod(action);
					return h;
				}
			}
			return NotFoundHandler;
		}

	}

	public class BypassHandler :IHttpHandler
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