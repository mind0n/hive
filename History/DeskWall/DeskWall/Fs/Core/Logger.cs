using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace Fs.Core
{
	public class Logger
	{
		public delegate void OnLogHandler(string content);
		public static OnLogHandler OnLog;
		
		static string DefaultLogFileFullPath = "Logger.txt";
		static List<string> items = new List<string>();
		
		public static void Log(string content)
		{
			items.Add(content);
			if (OnLog != null)
			{
				OnLog(content);
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

		static void WriteLogFile(string cnt)
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
					Thread.Sleep(100);
					continue;
				}
			}
			if (err != null)
			{
				throw new Exception("Cannot access log file:" + DefaultLogFileFullPath);
			}
			StreamWriter sw = new StreamWriter(fs);
			sw.WriteLine(cnt + "\r\n");
			sw.Close();
			fs.Close();
		}
	}
}
