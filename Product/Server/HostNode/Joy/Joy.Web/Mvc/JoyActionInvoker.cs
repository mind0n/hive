using System.Diagnostics;
using System.Web.Mvc.Async;
using Joy.Core;
using Joy.Web.Mvc.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.Mvc;

namespace Joy.Web.Mvc
{
    public class JoyActionInvoker : AsyncControllerActionInvoker
    {
        protected MethodInfo defAct;

        public JoyActionInvoker(string defaultAction = null)
        {
            if (!string.IsNullOrEmpty(defaultAction))
            {
                var st = new StackTrace();
                var sf = st.GetFrame(2);
                var type = sf.GetMethod().DeclaringType;
                defAct = type.GetMethod(defaultAction);
            }
        }

        protected override object GetParameterValue(ControllerContext cc, ParameterDescriptor pd)
        {
            var r = base.GetParameterValue(cc, pd);
            return r;
        }

        protected override IDictionary<string, object> GetParameterValues(ControllerContext cc, ActionDescriptor ad)
        {
            var ps = ad.GetParameters();
            var r = base.GetParameterValues(cc, ad);
            var a = cc.RouteData.Values["args"];
            var arg = (string) (defAct != null ? cc.RouteData.Values["action"] : a);
            var args = !string.IsNullOrEmpty(arg) ? arg.Split('/') : null;
            var rr = new Dictionary<string, object>();
            if (r.Count == 0)
            {
                return rr;
            }
            if (r.Count == 1)
            {
                if (!string.IsNullOrEmpty(arg))
                {
                    foreach (var o in r)
                    {
                        rr[o.Key] = Convert.ChangeType(arg, ps[0].ParameterType);
                    }
                }
                else
                {
                    ParseParameters(cc, r, rr);
                }
            }
            else
            {
                if (args != null && args.Length > 0)
                {
                    int p = 0;
                    foreach (var i in r)
                    {
                        if (p >= args.Length)
                        {
                            break;
                        }
                        var par = ps[p];
                        rr[i.Key] = Convert.ChangeType(args[p], par.ParameterType);
                        p++;
                    }
                }
                else
                {
                    ParseParameters(cc, r, rr);
                }
            }
            return rr;
        }

        private static void ParseParameters(ControllerContext cc, IDictionary<string, object> r, Dictionary<string, object> rr)
        {
            foreach (var o in r)
            {
                var rv = (cc.RouteData.Values[o.Key] ?? cc.HttpContext.Request.QueryString[o.Key]) ??
                         string.Empty;
                rr[o.Key] = rv;
            }
        }

        protected override ActionResult InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor ad,
            IDictionary<string, object> parameters)
        {
            return base.InvokeActionMethod(controllerContext, ad, parameters);
        }

        protected override ActionDescriptor FindAction(ControllerContext cc, ControllerDescriptor cd,
            string actionName)
        {
            if (defAct != null)
            {
                var ad = new ReflectedActionDescriptor(defAct, defAct.Name, cd);
                return ad;
            }
            else
            {
                var c = cc.Controller;
                var mi = c.GetType()
                    .GetMethod(actionName,
                        BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.Public |
                        BindingFlags.IgnoreCase);
                if (mi != null)
                {
                    var ad = new ReflectedActionDescriptor(mi, actionName, cd);
                    return ad;
                }
            }
            return base.FindAction(cc, cd, actionName);
        }
    }
}