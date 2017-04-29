using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Infrastructure
{
	public abstract class ContextAdapter
	{
		public RequestAdapter Request { get; set; }
		public ResponseAdapter Response { get; set; }

		public ContextAdapter(object context)
		{
			ProcessContext(context);
		}

		protected abstract void ProcessContext(object context);
	}

	public class OwinContextAdapter : ContextAdapter
	{
		public OwinContextAdapter(IOwinContext context) : base(context) { }

		protected override void ProcessContext(object context)
		{
			var ctx = (IOwinContext) context;
			Request = new OwinRequestAdapter(ctx);
			Response = new OwinResponseAdapter(ctx);
		}
	}
}
