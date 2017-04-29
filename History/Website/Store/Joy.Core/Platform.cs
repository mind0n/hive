using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Joy.Core;
using System.Threading;
using Joy.Core.Logging;

namespace Joy.Core
{
	public abstract class J
	{
		#region Static Region

		public static bool IsFunctional
		{
			get
			{
				return instance == null;
			}
		}
		private static J instance;

		public static J Instance
		{
			get
			{
				return J.instance;
			}
		}	   

		public static void Init(Type type, bool isOverride = false)
		{
			if (type != null)
			{
				if (instance == null || isOverride)
				{
					instance = type.Create<J>();
				}
			}
		}

		#endregion Static Region

		protected JFileSystem fileSystem;
		public virtual JFileSystem FileSystem
		{
			get
			{
				return fileSystem;
			}
		}

		public delegate void ThreadWorker(object argument);
		public abstract object CreateThread(ThreadWorker worker, object argument = null, bool isBackground = true, bool isStart = true);
		public abstract JLock CreateLock();
		public abstract JLogger CreateLogger();
		public abstract void Sleep(int ms);
	}
	public abstract class JLock
	{
		public delegate void LockCallback();
		public abstract void EnterReadLock();
		public abstract void EnterWriteLock();
		public abstract void ExitReadLock();
		public abstract void ExitWriteLock();
	}
	public abstract class JFileSystem
	{
		public abstract string BaseDir { get; }
		public abstract bool Exists(string fullname, bool isFile = true);
		public abstract string Read(string fullname);
		public abstract void Copy(string src, string des, bool isMove = false);
	}
	public class JLogger
	{

		public delegate void LogMethod(string msg);
		public event LogMethod LogMethods;
		protected string logFile = J.Instance.FileSystem.BaseDir + "logs\\log.txt";
		protected static long lineNumber = 1;

		public string LogFile
		{
			get { return logFile; }
		}
		public class JLogItem
		{
			public string Content;
		}
		private static JLogger instance = J.Instance.CreateLogger();

		public static JLogger Instance
		{
			get
			{
				if (instance == null)
				{
					instance = J.Instance.CreateLogger();
				}
				return instance;
			}
		}
		protected List<JLogItem> inputCache = new List<JLogItem>();
		protected List<JLogItem> outputCache = new List<JLogItem>();
		protected static bool isExiting;
		protected JLock switchLock = J.Instance.CreateLock();
		protected object outputLock = new object();
		protected object outputThread;

		public JLogger()
		{
			outputThread = J.Instance.CreateThread(delegate(object argument)
			{
				while (!isExiting)
				{
					try
					{
						if (outputCache != null && outputCache.Count > 0)
						{
							lock (outputLock)
							{
								while (outputCache.Count > 0)
								{
									JLogItem item = outputCache[0];
									LogImmediately(item.Content);
									outputCache.RemoveAt(0);
								}
							}
							outputCache = null;
						}
						else if (inputCache != null && inputCache.Count > 0)
						{
							switchLock.EnterWriteLock();
							outputCache = null;
							outputCache = inputCache;
							inputCache = new List<JLogItem>();
							switchLock.ExitWriteLock();
						}
						J.Instance.Sleep(1000);
					}
					catch (Exception ex)
					{
						LogImmediately(ex.ToString());
					}

				}
			});
		}

		protected virtual void LogImmediately(string msg)
		{
			try
			{
				if (LogMethods != null)
				{
					LogMethods(msg);
				}
			}
			catch { }
		}

		public static void Log(string msg, params string[] args)
		{
			Instance.switchLock.EnterReadLock();
			Instance.inputCache.Add(new JLogItem { Content = string.Concat(lineNumber, "\t", DateTime.Now.ToString(), "\t", string.Format(msg, args), "\r\n") });
			lineNumber++;
			Instance.switchLock.ExitReadLock();
		}
	}
}
