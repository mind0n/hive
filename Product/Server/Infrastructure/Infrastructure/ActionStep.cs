using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
	public abstract class ActionStep
	{
		public abstract void Execute(ContextAdapter context);
	}

	public class RedirectStep : ActionStep
	{
		public string Location { get; set; }
		public override void Execute(ContextAdapter context)
		{
			context.Response.Header("Location", Location);
			context.Response.Status(302);
		}
	}
}
