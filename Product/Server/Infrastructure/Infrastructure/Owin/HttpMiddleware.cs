using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Logger;
using Microsoft.Owin;

namespace Infrastructure
{
	public abstract class HttpMiddleware : OwinMiddleware
	{
		public HttpMiddleware(OwinMiddleware next) : base(next) { }

		public override async Task Invoke(IOwinContext context)
		{
			if (!Process(context))
			{
				context.Response.StatusCode = 404;
				if (Next != null)
				{
					await Next.Invoke(context);
				}
			}
		}

		protected abstract bool Process(IOwinContext ctx);
	}

	public class ExecuteMiddleware : HttpMiddleware
	{
		public ExecuteMiddleware(OwinMiddleware next) : base(next) { }

		protected override bool Process(IOwinContext ctx)
		{
			var cx = new OwinContextAdapter(ctx);
			var manager = new ContextManager(cx);
			var rlt = manager.Process();
			return rlt;
		}
	}
}
