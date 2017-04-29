using Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Logger
{
	public class log
	{
		[ThreadStatic]
		protected static DateTime? prevtime;

		protected static LogQueue queue = new LogQueue();

		static dynamic settings { get; }
		static List<Logger> logger { get; } = new List<Logger>();

		static log()
		{
			var file = AppDomain.CurrentDomain.BaseDirectory + "logsettings.json";
			if (File.Exists(file))
			{
				var cnt = File.ReadAllText(file);
				var cfg = Dobj.FromJson(cnt);
				settings = cfg;
			}
			else
			{
				settings = new Dobj();
				Dobj.Settings(settings).AutoCreate = true;
				settings.logger.file.name = typeof (FileLogger).AssemblyQualifiedName;
				settings.logger.file.args.folder = "logs";
				Dobj.Settings(settings).AutoCreate = false;
				var cnt = Dobj.ToJson(settings);
				File.WriteAllText(file, cnt);
			}
			var loggers = settings.logger;
			Dobj.Enum(loggers,new Func<string, dynamic, bool>((k, v) =>
			{
				string name = v.name;
				var lg = (Logger)ReflectionExtensions.CreateInstance(name, AppDomain.CurrentDomain, v.args);
				logger.Add(lg);
				return false;
			}));
			queue.OnDequeue += Queue_OnDequeue;
		}

		private static void Queue_OnDequeue(Entry entry)
		{
			foreach (var i in logger)
			{
				i.Log(entry);
			}
		}

		public static void Shutdown()
		{
			foreach (var i in logger)
			{
				i.Dispose();
			}
			queue.Dispose();
		}

		public static void e(string msg, string source = "", string category = "", string session = "")
		{
			w(msg, source, category, Level.Error, session);
		}

		public static void e(Exception ex, string source = "", string category = "", string session = "")
		{
			w(ex.ToString(), source, category, Level.Error, session);
		}

		public static void i(string msg, string source = "", string category = "", string session = "")
		{
			w(msg, source, category, Level.Info, session);
		}

		private static void w(string msg, string source, string category, Level level, string session)
		{
			dynamic entry = new Entry();
			entry.Message = msg;
			entry.Session = session;
			entry.Category = category;
			entry.Level = level;
			entry.Source = Assembly.GetCallingAssembly().GetName().Name;
			if (!prevtime.HasValue)
			{
				entry.Duration = -1;
			}
			else
			{
				entry.Duration = (DateTime.Now - prevtime.Value).Milliseconds;
			}
			prevtime = DateTime.Now;
			queue.Enqueue(entry);
		}
	}

	public class LogSettings : Dobj
	{
	}
}
