using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using System.Diagnostics;

namespace ULib.Executing.Commands.OS
{
	public class RunCommand : CommandNode
	{
		public string Cmd;

		public string Args;

		public bool IsBypass;

		public string CmdLine
		{
			get
			{
				return string.Concat(Executor.Instance.ParseIdString(Cmd), " ", Executor.Instance.ParseIdString(Args));
			}
		}
		public override string Name
		{
			get
			{
				return string.Format("Run command {0} with arguments {1}", Cmd, Executor.Instance.ParseIdString(Args));
			}
		}
		public override void Run(CommandResult rlt, bool isPreview = false)
		{
			if (!IsBypass)
			{
				Process.Start(Executor.Instance.ParseIdString(Cmd), Executor.Instance.ParseIdString(Args));
			}
		}
	}
}
