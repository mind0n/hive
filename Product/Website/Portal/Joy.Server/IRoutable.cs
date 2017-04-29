using Joy.Server.Web;
using System.Reflection;
using System.Web;
using System.Web.Routing;

namespace Joy.Server
{
	interface IRoutable : IHttpHandler
	{
		RouteInfo RouteInfo { get; set; }
		MethodInfo Intent { get; set; }
		string[] Params { get; set; }
		RequestContext RequestContext { get; set; }
	}
}
