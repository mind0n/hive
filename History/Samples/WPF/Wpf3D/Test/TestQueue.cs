using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fs.Entities;
using System.Timers;
using Fs.Native.Windows;

namespace Test
{
	public class TestQueue : MessageQueue
	{
		public TestQueue()
		{
			
		}
		public void Run(int steps, bool IsParallel)
		{
			DictParams parlist = new DictParams();
			parlist["stepsleft"] = steps;
			//Add(ProceedRun, parlist, IsParallel);
			Add(tmrRun_Elapsed, parlist, 0, IsParallel);
		}
		public void LookRight()
		{
			Add(ProceedLookRight, null, true);
		}
		void tmrRun_Elapsed(object sender, ElapsedEventArgs e)
		{
			ParamTimer tmr = (ParamTimer)sender;
			tmr.Stop();
			int sleft = (int)tmr.DictParams["stepsleft"];
			Console.WriteLine("Running - " + sleft);
			tmr.DictParams["stepsleft"] = sleft - 1;
			if (sleft - 1 > 0)
			{
				tmr.Start();
			}
			else
			{
				Message msg = tmr.Param as Message;
				msg.Release();
			}
		}
		protected void ProceedLookRight(MessageQueue queue, Message msg)
		{
			Console.WriteLine("Look right");
		}
	}
}
