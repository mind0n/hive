using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Infrastructure
{
	public class BizUnit
	{
		protected ContextAdapter cx { get; }

		public BizUnit(ContextAdapter context)
		{
			cx = context;
		}

		public virtual object Execute(string action, string[] args)
		{
			return ReflectionExtensions.Invoke(GetType(), action, this, args);
		}
	}

	public class TestBizUnit : BizUnit
	{
		public TestBizUnit(ContextAdapter context) : base(context) { }

		public string Default(int arg)
		{
			return "Parameter: " + arg;
		}
	}

	public class LhBizUnit : BizUnit
	{
		public LhBizUnit(ContextAdapter context) : base(context) { }

		public string Domains()
		{
			List<string> list = new List<string>();
			foreach (var i in DomainHelper.Instance.Keys)
			{
				list.Add(i);
			}
			return string.Join("<br />", list.ToArray());
		}

		public string Restart()
		{
			Launcher.Instance.Reset();
			return "Restarted";
		}
	}

}
