using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using ULib.Controls;

namespace Utilities.Commands
{
	public class RepeatCommand : CommandNode
	{
		private static int times = 0;
		public int Repeat_Times = 0;
		public override string Name
		{
			get
			{
				return string.Format("Executed Time(s): {1}/{0}", Repeat_Times + 1, times);
			}
		}
		public override CommandResult Execute(CommandTreeViewNode node = null)
		{
			CommandResult rlt = new CommandResult();
			times++;
			if (node != null)
			{
				if (Repeat_Times < 0 || times <= Repeat_Times)
				{
					CommandTreeViewNode prev = node;
					while (prev.PrevNode != null && !isCancelling)
					{
						prev = prev.PrevNode as CommandTreeViewNode;
					}
					if (base.AccomplishCancellation())
					{
						rlt.Next = null;
					}
					else
					{
						rlt.Next = prev;
					}
				}
			}
			//try
			//{
			//    foreach (CommandNode i in this.Children)
			//    {
			//        if (i != null)
			//        {
			//            CommandResult r = i.Execute();
			//            if (r.IsError)
			//            {
			//                rlt.LastError = r.LastError;
			//                break;
			//            }
			//        }
			//    }
			//}
			//catch (Exception e)
			//{
			//    rlt.LastError = e;
			//}
			return rlt;
		}

		public override CommandNode Clone()
		{
			RepeatCommand rlt = new RepeatCommand();
			base.CopyChildren(rlt);
			return rlt;
		}

		public override void Reset()
		{
			times = 0;
			base.Reset();
		}

	}
}
