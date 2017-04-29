using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Joy.Core;
using Joy.Web.Mvc.Security;

namespace Joy.Web.Mvc
{
    public class JoyControllerBuilder : System.Web.Mvc.ControllerBuilder
    {
    }

    public class JoyControllerFactory : DefaultControllerFactory
    {
        public Type RetrieveControllerType(RequestContext rc, string n)
        {
            return GetControllerType(rc, n);
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            var t = this.GetControllerType(requestContext, controllerName);
            var r = base.CreateController(requestContext, controllerName);
            return r;
        }
    }
}