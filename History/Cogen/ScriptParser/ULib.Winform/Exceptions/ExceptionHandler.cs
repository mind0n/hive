using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ULib.Winform.Exceptions
{
	public class ExceptionHandler
	{
		public static List<Action<Exception>> ExceptionHandlers = new List<Action<Exception>>();
		public static void Handle(string msg, params string[] args)
		{
			Handle(new Exception(string.Format(msg, args)));
		}
		public static void Handle(Exception e)
		{
			string name = AppDomain.CurrentDomain.FriendlyName;
			if (!EventLog.SourceExists(name))
			{
				EventLog.CreateEventSource(name, "Application");
			}
			EventLog.WriteEntry(name, e.ToString(), EventLogEntryType.Error);

			foreach (Action<Exception> item in ExceptionHandlers)
			{
				item(e);
			}
		}
	}
}
