using Joy.Common.Implement;
using Joy.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Joy.Common
{
	public class PcPlatform : J
	{
		public PcPlatform()
		{

		}
		public override JFileSystem FileSystem
		{
			get
			{
				if (fileSystem == null)
				{
					fileSystem = new FileSystem();
				}
				return fileSystem;
			}
		}
		public override object CreateThread(J.ThreadWorker worker, object argument = null, bool isBackground = true, bool isStart = true)
		{
			ParameterizedThreadStart ts = new ParameterizedThreadStart(worker);
			Thread th = new Thread(ts);
			th.IsBackground = isBackground;
			th.Start(argument);
			return th;
		}

		public override JLock CreateLock()
		{
			return new RwLock();
		}

		public override JLogger CreateLogger()
		{
			return new PcLogger();
		}

		public override void Sleep(int ms)
		{
			Thread.Sleep(ms);
		}
	}
}
