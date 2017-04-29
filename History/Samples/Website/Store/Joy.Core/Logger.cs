using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

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

		public static void Log(string content)
		{
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
		static Logger()
		{
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
