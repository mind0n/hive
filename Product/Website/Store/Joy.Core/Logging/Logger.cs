using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Joy.Core.Logging
{
	public abstract class Logger
	{
		public static void Log(string msg, params string[] args)
		{
			JLogger.Log(msg, args);
		}
	}
}
