using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace Startup
{
	class Program
	{
		static ManualResetEvent mre = new ManualResetEvent(false);
		static void Main(string[] args)
		{
			var config = new HttpSelfHostConfiguration("http://localhost:80");
			config.Routes.MapHttpRoute("ApiDefault", "api/{controller}/{id}", new {id = RouteParameter.Optional});
			using (HttpSelfHostServer server = new HttpSelfHostServer(config))
			{
				server.OpenAsync().Wait();
				mre.WaitOne();
			}
		}
	}
}
