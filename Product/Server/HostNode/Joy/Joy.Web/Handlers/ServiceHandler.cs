using Joy.Web.Mvc;
using Joy.Web.Mvc.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using ActionAttribute = Joy.Web.Mvc.IO.ActionAttribute;

namespace Joy.Web.Handlers
{
    public class ServiceHandler : IHttpHandler, IRoutable, IRequiresSessionState
    {
        public GroupCollection Groups { get; set; }
        List<MethodInfo> methods = new List<MethodInfo>();
        protected HttpResponse Response
        {
            get
            {
                return HttpContext.Current.Response;
            }
        }
        protected HttpRequest Request
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (!Router.Config.Instance.EnableHandlers)
            {
                Response.Clear();
                Response.StatusCode = 403;
                return;
            }
            var mi = _GetMethodInfo();
            if (mi != null)
            {
                var atts = mi.GetCustomAttributes(typeof(ActionAttribute), true);
                ActionAttribute at = null;
                if (atts != null && atts.Length > 0)
                {
                    at = atts[0] as ActionAttribute;
                }
                if (at == null)
                {
                    at = new ActionAttribute { ContentType = "text/plain", OutputType = ActionOutputType.Raw };
                }
                var args = new List<string>();
                if (Args != null)
                {
                    if (RouteItem.IsNoSplit)
                    {
                        args.Add(Args);
                    }
                    else
                    {
                        args.AddRange(Args.Split('/'));
                    }
                }
                var pl = mi.GetParameters();
                var os = args.Count > 0 ? new object[args.Count] : null;

                for (int i = 0; i < pl.Length; i++)
                {
                    if (i < args.Count)
                    {
                        var o = Convert.ChangeType(args[i], pl[i].ParameterType);
                        os[i] = o;
                    }
                }
                context.Response.Clear();
                context.Response.ContentType = at.ContentType;
                if (mi.ReturnType != null)
                {
                    var r = mi.Invoke(this, os);
                    if (at.OutputType == ActionOutputType.Raw)
                    {
                        context.Response.Write(r);
                    }
                    else if (at.OutputType == ActionOutputType.Json)
                    {
                        var js = new JavaScriptSerializer();
                        var s = js.Serialize(r);
                        context.Response.Write(s);
                    }
                }
                else
                {
                    mi.Invoke(this, os);
                }
            }
        }

        private MethodInfo _GetMethodInfo()
        {
            if (methods.Count < 1)
            {
                var mis = this.GetType().GetMethods();
                methods.AddRange(mis);
            }
            foreach (var i in methods)
            {
                if (i.IsPublic && string.Equals(i.Name, ActionName, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return null;
        }

        public string ActionName { get; set; }

        public string Args { get; set; }

        public RouteItem RouteItem { get; set; }
    }
}