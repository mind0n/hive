using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;
using System.IO;
using Settings;

namespace Infrastructure
{
	public class ContextManager
	{
		protected List<ContextHandler> handlers = new List<ContextHandler>();
		protected ContextAdapter ctx { get; }

		public ContextManager(ContextAdapter adapter)
		{
			ctx = adapter;
			handlers.Add(new InstanceHandler());
			handlers.Add(new StaticFileHandler() {BaseFolder = "/../../../../../../QCloud/qode/src/assets", Name = "assets"});
			handlers.Add(new StaticFileHandler() {BaseFolder = "res"});
		}

		public bool Process()
		{
			bool handled = false;
			foreach (var i in handlers)
			{
				var rlt = i.Handle(ctx);
                if (rlt.Accomplished)
                {
	                handled = true;
					break;
				}
			}
			return handled;
		}	
	}

	public class HandleResult : Result
	{
		public bool Accomplished { get; protected set; }

		public override void Set(object result = null)
		{
			if (result == null)
			{
				Accomplished = false;
			}
			else if (result is bool)
			{
				Accomplished = (bool) result;
			}
			else
			{
				Accomplished = false;
			}
			base.Set(result);
		}

		public override void Error(Exception ex = null)
		{
			Accomplished = false;
			base.Error(ex);
		}
	}

	public abstract class ContextHandler
	{
		protected FileSetting settings = new FileSetting(new {name="ContextSettings.json"});

		public HandleResult Handle(ContextAdapter context)
		{
			var rlt = new HandleResult();
			try
			{
				Handle(context, rlt);
			}
			catch (Exception ex)
			{
				rlt.Error(ex);
			}
			return rlt;
		}

		protected abstract void Handle(ContextAdapter context, HandleResult result);
	}

	public class StaticFileHandler : ContextHandler
	{
		public string BaseFolder { get; set; }
		public string Name { get; set; }
		public StaticFileHandler()
		{
		}
		protected override void Handle(ContextAdapter context, HandleResult rlt)
		{
			if (!string.IsNullOrWhiteSpace(Name) &&
			    !string.Equals(context.Request.Url.Instance, Name, StringComparison.OrdinalIgnoreCase))
			{
				return;
			}
			var route = context.Request.Url.Route;
			if (!string.IsNullOrWhiteSpace(Name))
			{
				route = route.Replace(Name, string.Empty);
			}
			//var path = route.PathMap(AppDomain.CurrentDomain.BaseDirectory + "res");
			var path = route.PathMap(BaseFolder.PathAbs());

			var ext = path.PathExt();
			var dtype = Dobj.Get<string>(settings.Instance.mime, "*");
			var etype = Dobj.Get<string>(settings.Instance.mime, ext);
			if (!string.IsNullOrWhiteSpace(etype))
			{
				context.Response.Type(etype);
			}
			else
			{
				context.Response.Type(dtype);
			}

			if (File.Exists(path))
			{
				var s = File.Open(path, FileMode.Open);
				context.Response.Write(s);
				rlt.Set(true);
			}
			else
			{
				context.Response.Type("text/html");
			}
		}
	}

	public class InstanceHandler : ContextHandler
	{
		protected override void Handle(ContextAdapter context, HandleResult rlt)
		{
			var iname = context.Request.Url.Instance;
			var aname = context.Request.Url.Action;

			var typename = Dobj.Get<string>(settings.Instance.mapping, iname);
			if (typename != null)
			{
				var o = ReflectionExtensions.CreateInstance(typename, null, context);
				if (o != null)
				{
					try
					{
						context.Response.Type("text/html");
						var result = (o is BizUnit)
							? ((BizUnit) o).Execute(aname, context.Request.Url.RouteArgs)
							: ReflectionExtensions.Invoke(o.GetType(), aname, o, context.Request.Url.RouteArgs);
						if (result == null)
						{
							// Do nothing (void);
						}
						else if (result is ActionStep)
						{
							var ar = (ActionStep) result;
							ar.Execute(context);
						}
						else if (result is Stream)
						{
							context.Response.Write((Stream) result);
						}
						else
						{
							context.Response.Write(result.ToString());
						}
						rlt.Set(true);
					}
					catch (Exception ex)
					{
						context.Response.Status(403);
						context.Response.Type("text/html");
						log.e(ex);
					}
				}
			}
		}
	}
}
