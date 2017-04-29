using Logger;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreJson;

namespace Middlewares
{
	public abstract class BaseMiddleware : OwinMiddleware
	{
		protected IOwinRequest Request { get; private set; }
		protected IOwinResponse Response { get; private set; }
		protected IFormCollection Forms { get; private set; }

		protected Url Url { get; private set; }

		public BaseMiddleware(OwinMiddleware next) : base(next)
		{
		}

		public override async Task Invoke(IOwinContext context)
		{
			Request = context.Request;
			Response = context.Response;
			Url = new Url(Request.Uri.PathAndQuery);
			if (string.Equals(Request.Method, "post", StringComparison.OrdinalIgnoreCase))
			{
				var task = Request.ReadFormAsync();
				task.Wait();
				Forms = task.Result;
			}
			else
			{
				Forms = new FormCollection(new Dictionary<string, string[]>());
			}
			if (Process(context))
			{
				await Next.Invoke(context);
			}
		}

		protected abstract bool Process(IOwinContext ctx);
	}

	public class LogMiddleware : BaseMiddleware
	{
		public LogMiddleware(OwinMiddleware next) : base(next)
		{
		}

		protected override bool Process(IOwinContext ctx)
		{
			List<string> list = new List<string>();
			foreach (var i in Request.Headers)
			{
				list.Add($"{i.Key}={string.Join("\t", i.Value)}");
			}
			log.i($"[{Request.RemoteIpAddress}-{Request.Uri.AbsoluteUri}\r\n{string.Join("\r\n", list.ToArray())}]");
			return true;
		}
	}

	public class HttpMiddleware : BaseMiddleware
	{
		protected ProcessorFactory factory { get; } = new ProcessorFactory();
		protected Invoker invoker { get; } = new Invoker();
		public HttpMiddleware(OwinMiddleware next) : base(next)
		{
		}

		protected override bool Process(IOwinContext ctx)
		{
			if (Url.HasSecondary)
			{
				var processor = factory.Create<MiddleProcessor>(Url.Primary, null);
				var bytes = invoker.Invoke<byte[]>(processor, Url.Secondary, Url.Args);
				Response.Write(bytes);
			}
			return true;
		}
	}

	public class MiddleProcessor
	{
		public virtual byte[] Test()
		{
			return "Success".Bytes();
		}
	}

	public class LogProcessor : MiddleProcessor
	{
		[Method]
		public virtual byte[] info(string msg, string source, string category, string session)
		{
			log.i(msg, source, session, category);
			return "{ 'IsNoException':'true', 'method':'info' }".Bytes();
		}

		[Method]
		public virtual byte[] error(string msg, string source, string category, string session)
		{
			log.e(msg, source, session, category);
			return "{ 'IsNoException':'true', 'method':'error' }".Bytes();
		}
	}
}
