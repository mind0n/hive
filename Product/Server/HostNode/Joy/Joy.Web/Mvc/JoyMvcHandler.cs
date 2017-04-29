using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using Joy.Web.Mvc.Security;

namespace Joy.Web.Mvc
{
    public class JoyMvcHandler : MvcHandler, IRequiresSessionState
    {
        public JoyMvcHandler(RequestContext rc) : base(rc)
        {
            
        }

        protected override IAsyncResult BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, object state)
        {
            return base.BeginProcessRequest(httpContext, callback, state);
        }

        protected override IAsyncResult BeginProcessRequest(HttpContextBase httpContext, AsyncCallback callback, object state)
        {
            return base.BeginProcessRequest(httpContext, callback, state);
        }

        protected override void EndProcessRequest(IAsyncResult asyncResult)
        {
            base.EndProcessRequest(asyncResult);
        }

        protected override void ProcessRequest(HttpContextBase httpContext)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
            base.ProcessRequest(httpContext);
        }

        protected override void ProcessRequest(HttpContext httpContext)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
            Identifier.Instance.ParseRequest(httpContext);
            Identifier.Instance.Identify();
            base.ProcessRequest(httpContext);
        }

        private bool VerifyController()
        {
            var cb = System.Web.Mvc.ControllerBuilder.Current;
            string requiredString = this.RequestContext.RouteData.GetRequiredString("controller");
            var factory = (JoyControllerFactory)cb.GetControllerFactory();
            //this.RequestContext.RouteData.Values["controller"] = 
            var type = factory.RetrieveControllerType(RequestContext, requiredString);
            return false;
        }
    }
}