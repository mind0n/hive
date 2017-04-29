using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace Joy.Core
{
	public class Logger
	{
		public delegate void OnLogHandler(string content);
		public static event OnLogHandler OnLog;
		public static long LineNumber = 1;
		private static object WriterLock = new object();

		public static string LogFileName = "Log (" + DateTime.Now.ToString().Replace(':', '#').Replace('/', '_') + ").txt";
		public static string LogFilePath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "logs\\");
		public static string DefaultLogFileFullPath
		{
			get
			{
				if (!Directory.Exists(LogFilePath))
				{
					try
					{
						Directory.CreateDirectory(LogFilePath);
					}
					catch
					{
						LogFilePath = AppDomain.CurrentDomain.BaseDirectory;
					}
				}
				return string.Concat(LogFilePath, LogFileName);
			}
		}

	    public static void Clear()
	    {
	        if (!string.IsNullOrEmpty(el.Source))
	        {
	            el.Clear();
	        }
	    }
        public static void Log(string content, EventLogEntryType type = EventLogEntryType.Information, string source = null, string category = null, params string[] args)
        {
            LogEvent(content, type, source, category, args);
            if (OnLog != null)
			{
				DateTime d = DateTime.Now;
				lock (WriterLock)
				{
					OnLog(LineNumber + ":\t[" + d.Year + '.' + d.Month + '.' + d.Day + '|' + d.Hour + ':' + d.Minute + ':' + d.Second + '|' + d.Millisecond + "]\t" + content);
					LineNumber++;
				}
			}
        }

	    private static void LogEvent(string content, EventLogEntryType type, string source, string category, string[] args)
	    {
	        if (source == null)
	        {
	            source = Assembly.GetCallingAssembly().FullName;
	        }
	        if (!EventLog.SourceExists(source))
	        {
	            EventLog.CreateEventSource(source, category);
	        }

	        el.Source = source;
	        el.WriteEntry(string.Format(content, args), type);
	    }

	    private static EventLog el;
		static Logger()
		{
            el = new EventLog();
            
            if (File.Exists(DefaultLogFileFullPath))
			{
				File.Delete(DefaultLogFileFullPath);
				WriteLogFile("-[" + DateTime.Now + "]------------------------------------------------------------------------\r\n"
					+ "<<< Program Started >>>");
			}
			OnLog += delegate(string cnt)
			{
				WriteLogFile(cnt);
			};
		}

		static void WriteLogFile(string cnt, params string[] args)
		{
			try
			{
				FileStream fs = null;
				Exception err = null;
				for (int i = 1; i < 10; i++)
				{
					try
					{
						fs = new FileStream(DefaultLogFileFullPath, FileMode.Append, FileAccess.Write);
						err = null;
						break;
					}
					catch (Exception e)
					{
						err = e;
						Thread.Sleep(200);
						continue;
					}
				}
				StreamWriter sw = new StreamWriter(fs);
				sw.WriteLine(string.Format(cnt, args));
				sw.Close();
				fs.Close();
			}
			catch { }
		}
	}
}
