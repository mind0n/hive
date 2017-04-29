using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using System.Reflection;

namespace ULib.Executing.Commands.Common
{
	public class InvokeCommand : CommandNode
	{
		public string Object;
		public string Method;
		public string Arguments;
		public override string Name
		{
			get
			{
				return string.Format("Invoke {0}", Method);
			}
		}
		public override void Run(CommandResult rlt, bool isPreview = false)
		{
			ExecuteParameter par = Executor.Instance.GetVar(Object);
			if (par != null)
			{
				object o = par.Value;
				MethodInfo method = o.GetType().GetMethod(Method);
				if (method != null)
				{
					string[] args = null;
					if (!string.IsNullOrEmpty(Arguments))
					{
						args = Arguments.Split(',');
					}
					object r = method.Invoke(o, args);
				}
			}
		}
	}
}
