using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Joy.Users;
using Joy.Users.Web;

namespace Joy.Server.Web
{
	public class JoyPage : System.Web.UI.Page, IRoutable
    {

	    public RouteInfo RouteInfo { get; set; }

		public string[] Params { get; set; }

		public System.Reflection.MethodInfo Intent { get; set; }

		public System.Web.Routing.RequestContext RequestContext { get; set; }
	}
}