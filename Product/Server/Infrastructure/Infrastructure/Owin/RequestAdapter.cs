using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Microsoft.Owin;

namespace Infrastructure
{
	[Serializable]
	public abstract class RequestAdapter
	{
		public Dobj Headers { get; protected set; }
		public Stream Body { get; protected set; }
		public string ContentType { get; protected set; }
		public RequestUrl Url { get; protected set; }
		public Dobj Forms { get; protected set; } = new Dobj();
		public Dobj Cookies { get; protected set; } = new Dobj();
		public RequestAdapter(object ctx)
		{
			ProcessContext(ctx);
		}

		protected abstract void ProcessContext(object ctx);
	}

	public class OwinRequestAdapter : RequestAdapter
	{
		public OwinRequestAdapter(IOwinContext context) : base(context) { }
		protected override void ProcessContext(object context)
		{
			var ctx = (IOwinContext) context;
			
			IFormCollection forms = null;
			if (string.Equals(ctx.Request.Method, "post", StringComparison.OrdinalIgnoreCase))
			{
				var task = ctx.Request.ReadFormAsync();
				task.Wait();
				forms = task.Result;
			}
			else
			{
				forms = new FormCollection(new Dictionary<string, string[]>());
			}
			foreach (var i in forms)
			{
				Dobj.Set(Forms, i.Key, i.Value);
			}

			foreach (var i in ctx.Request.Cookies)
			{
				Dobj.Set(Cookies, i.Key, i.Value);
			}

			Headers = new Dobj();
			foreach (var i in ctx.Request.Headers.Keys)
			{
				Dobj.Set(Headers, i, ctx.Request.Headers[i]);
			}
			Body = ctx.Request.Body;
			ContentType = ctx.Request.ContentType;
			Url = new RequestUrl(ctx.Request.Uri);
		}
	}

	[Serializable]
	public class RequestUrl
	{
		public string Raw { get; }
		public string Scheme { get; }
		public string Domain { get; }
		public string Route { get; }
		public dynamic QueryArgs { get; } = new Dobj();
		public string Instance { get; } = "Default";
		public string Action { get; } = "Default";
		public string[] RouteArgs { get; }

		protected string qstring { get; }

		public RequestUrl(Uri uri)
		{
			Raw = uri.AbsoluteUri;
			Scheme = uri.Scheme.ToLower();
			var schemepos = Raw.IndexOf("://");
            if (schemepos> 0)
            {
	            var dpos = schemepos + 3;
				var pos = Raw.IndexOf("/", schemepos + 3);
				Domain = Raw.Substring(dpos, pos - dpos);

				Route = Raw.Substring(pos);
	            var list = Route.Split('?');
	            Route = list[0];
	            var ulist = Route.Split('/');
	            if (ulist.Length > 1)
	            {
		            Instance = ulist[1];
	            }
	            if (ulist.Length > 2)
	            {
		            Action = ulist[2];
	            }
	            if (ulist.Length > 3)
	            {
		            RouteArgs = new string[ulist.Length - 3];
		            for (int i = 3; i < ulist.Length; i++)
		            {
			            RouteArgs[i - 3] = ulist[i];
		            }
	            }

	            qstring = null;
	            if (list.Length > 1)
	            {
					qstring = list[1];
				}
	            if (!string.IsNullOrWhiteSpace(qstring))
	            {
		            var qlist = qstring.Split('&');
		            foreach (var i in qlist)
		            {
			            var kv = i.Split('=');
			            var val = string.Empty;
			            if (kv.Length > 1)
			            {
				            val = kv[1];
			            }
			            Dobj.Set(QueryArgs, kv[0], val);
		            }
	            }
            }
		}
	}
}
