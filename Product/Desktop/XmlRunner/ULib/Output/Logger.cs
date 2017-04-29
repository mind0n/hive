using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ULib.Exceptions;

namespace ULib.Output
{
	public class Logger
	{
		private static string baseDir = AppDomain.CurrentDomain.BaseDirectory;
		private static string logfile
		{
			get
			{
				return baseDir + "log.txt";
			}
		}
		public static string bakfile
		{
			get
			{
				DateTime n = DateTime.Now;
				return baseDir + string.Format(template, n.Year, n.Month, n.Day, n.Hour, n.Minute, n.Second, n.Millisecond);
			}
		}
		private static string template = "Log_{0}_{1}_{2}-{3}_{4}_{5}-{6}.txt";

		public static void Init()
		{
			OutputHandler.Handlers.Add(LogMsg);
			ExceptionHandler.Handlers.Add(LogError);
			if (File.Exists(logfile))
			{
				File.Move(logfile, bakfile);
				Log(string.Empty);
			}
		}
		public static void Log2File(string sql, string file = null)
		{
			try
			{
				if (string.IsNullOrEmpty(file))
				{
					//DateTime t = DateTime.Now;
					//file = string.Format("temp_{0}_{1}_{2}-{3}_{4}_{5}.txt", t.Year, t.Month, t.Day, t.Hour, t.Minute, t.Second);
					file = AppDomain.CurrentDomain.BaseDirectory + "temp.txt";
				}
				//File.AppendAllText(file, sql + "\r\n");
			}
			catch { }
		}
		public static void Log(string msg, bool addTimestamp = true, params string[] args)
		{
			try
			{
				string s = string.Empty;
				if (addTimestamp)
				{
					s += string.Concat("[", DateTime.Now.ToString(), "] - ");
				}
				File.AppendAllText(logfile, string.Concat(s, string.Format(msg, args), "\r\n"));
			}
			catch { }
		}
		static bool LogError(Exception e)
		{
			Log(e.ToString());
			return false;
		}
		static void LogMsg(string msg, int type)
		{
			Logger.Log(string.Format("[{0}]:\r\n{1}", type, msg));
			//MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
