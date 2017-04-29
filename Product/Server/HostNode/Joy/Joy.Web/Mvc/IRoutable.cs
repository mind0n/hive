using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

namespace Joy.Web.Mvc
{
    public interface IRoutable
    {
        string ActionName { get; set; }
        string Args { get; set; }
        RouteItem RouteItem { get; set; }
        GroupCollection Groups { get; set; }
    }
}