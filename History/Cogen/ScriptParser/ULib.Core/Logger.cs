using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ULib.Core
{
	public class Logger
	{
		private static Handlers<string> logHandlers = new Handlers<string>();

		public static Handlers<string> LogHandlers
		{
			get { return Logger.logHandlers; }
		}
		public static void Log(string msg, params string[] args)
		{
			msg = string.Format(msg, args);
			if (logHandlers != null)
			{
				foreach (Action<string> i in logHandlers)
				{
					if (i != null)
					{
						i(msg);
					}
				}
			}
			else
			{
				logHandlers = new Handlers<string>();
			}
		}
	}
}
