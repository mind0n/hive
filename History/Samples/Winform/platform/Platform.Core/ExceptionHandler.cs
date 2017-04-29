using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Platform.Core;

namespace ULib.Winform.Exceptions
{
	public class ExceptionHandler
	{
		public static Handlers<Exception> ExceptionHandlers = new Handlers<Exception>();

		public static void Handle(string msg, string source, params string[] args)
		{
			Handle(CreateException(msg, source, args));
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

		public static void Raise(string msg, string source, params string[] args)
		{
			Exception e = CreateException(msg, source, args);
			throw e;
		}

		private static Exception CreateException(string msg, string source, string[] args)
		{
			Exception e = new Exception(string.Format(msg, args));
			if (!string.IsNullOrEmpty(source))
			{
				e.Source = source;
			}
			return e;
		}
	}

}
