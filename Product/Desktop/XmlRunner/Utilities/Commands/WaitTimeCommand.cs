using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using System.Threading;
using ULib.Controls;

namespace Utilities.Commands
{
	public class WaitTimeCommand : CommandNode
	{
		public int Time_In_Second = 30;
		public override string Name
		{
			get
			{
				return string.Format("Wait for {0} second(s).", this.Time_In_Second);
			}
		}
		public override CommandResult Execute(CommandTreeViewNode node = null)
		{
			Thread.Sleep(Time_In_Second * 1000);
			return new CommandResult();
		}

		public override CommandNode Clone()
		{
			WaitTimeCommand rlt = new WaitTimeCommand { Time_In_Second = this.Time_In_Second };
			base.CopyChildren(rlt);
			return rlt;
		}

		public override void Reset()
		{
			base.Reset();
		}
	}
}
