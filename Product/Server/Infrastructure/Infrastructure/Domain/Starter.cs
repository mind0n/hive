using Core;
using Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
	public abstract class Starter
	{
		protected static DomainsLoader loader = new DomainsLoader();

		protected static Dobj settings { get; set; }

		public abstract void Reset();

		public abstract void Start();

		public virtual void Stop()
		{
			loader.Unload((asm) =>
			{
				var target = "Entry";
				var type = FindType(asm, target);
				if (type != null)
				{
					Invoke(type, "Stop");
				}
			});
		}

		protected static object Invoke(Type type, string action)
		{
			var mi = type.GetMethod(action);
			if (mi != null)
			{
				var rlt = mi.Invoke(null, null);
				return rlt;
			}
			return null;
		}

		protected static object CreateInstance(Type type)
		{
			if (type != null)
			{
				var o = Activator.CreateInstance(type);
				return o;
			}
			return null;
		}

		protected static Type FindType(Assembly asm, string target)
		{
			var type = asm.GetType(target, false, true);
			if (type == null)
			{
				var types = asm.GetTypes();
				if (types != null)
				{
					foreach (var i in types)
					{
						if (i.Name.EndsWith(target, StringComparison.OrdinalIgnoreCase))
						{
							return i;
						}
					}
				}
			}
			return type;
		} 
	}

	public class FolderStarter : Starter
	{
		protected string basedir = AppDomain.CurrentDomain.BaseDirectory;
		protected string loadfoldername = "shadow";
		protected string monitorfoldername = "addins";

		protected string LoadFolder
		{
			get { return $"{basedir}{loadfoldername}\\"; }
		}

		protected string MonitorFolder
		{
			get { return $"{basedir}{monitorfoldername}\\"; }
		}

		public FolderStarter()
		{
			if (!Directory.Exists(LoadFolder))
			{
				Directory.CreateDirectory(LoadFolder);
			}

			if (!Directory.Exists(MonitorFolder))
			{
				Directory.CreateDirectory(MonitorFolder);
			}
		}

		public override void Reset()
		{
			Stop();
			var preloads = Directory.GetFiles(LoadFolder, "*.*", SearchOption.AllDirectories);
			if (preloads.Length > 0)
			{
				foreach (var i in preloads)
				{
					File.Delete(i);
				}
			}
		}

		public override void Start()
		{
			PrepareAddins();
			LoadAddins();
		}

		public override void Stop()
		{
			base.Stop();
		}

		protected void PrepareAddins()
		{
			var preloads = Directory.GetFiles(MonitorFolder, "*.*", SearchOption.AllDirectories);
			if (preloads.Length > 0)
			{
				Stop();
				foreach (var i in preloads)
				{
					try
					{
						var name = i.PathLastName();
						var folder = i.PathWithoutFilename().PathLastName();
						var dest = LoadFolder.PathAppendFilename(folder + "\\" + name);
						var destdir = dest.PathWithoutFilename();
						if (!Directory.Exists(destdir))
						{
							Directory.CreateDirectory(destdir);
						}
						if (File.Exists(dest))
						{
							File.Delete(dest);
						}
						File.Move(i, dest);
					}
					catch (Exception ex)
					{
						log.e(ex);
					}
				}
			}
		}

		protected void LoadAddins()
		{
			string path = LoadFolder;
			var di = new DirectoryInfo(path);
			var addins = di.GetFiles("*.Entry.dll", SearchOption.AllDirectories);
			log.i($"Loading into domain: {AppDomain.CurrentDomain.FriendlyName}", "FolderStarter", "Infrastructure");
			foreach (var i in addins)
			{
				loader.LoadFile(i.FullName, (asm) =>
				{
					var target = "Entry";
					var type = FindType(asm, target);
					if (type != null)
					{
						Invoke(type, "Start");
					}
				});
			}
		}
	}

	public class FolderMonitor : IDisposable
	{
		protected ManualResetEvent mre;
		protected FileSystemWatcher watcher;
		protected bool isexit;
		protected bool isdisposed;

		public FolderMonitor(string path, string pattern, Action<string[]> callback)
		{
			if (callback == null || string.IsNullOrWhiteSpace(path))
			{
				return;
			}

			if (string.IsNullOrWhiteSpace(pattern))
			{
				pattern = "*.*";
			}

			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			mre = new ManualResetEvent(false);
			watcher = new FileSystemWatcher(path);
			watcher.Changed += Watcher_Changed;
			watcher.EnableRaisingEvents = true;

			Thread th = new Thread(new ThreadStart(() =>
			{
				try
				{
					mre.WaitOne();
					while (!isexit)
					{
						Thread.Sleep(500);
						var files = Directory.GetFiles(path, pattern, SearchOption.AllDirectories);
						if (files.Length > 0)
						{
							callback(files);
						}
						mre.WaitOne();
					}
				}
				catch (Exception ex)
				{
					log.e(ex);
				}
				finally
				{
					mre.Dispose();
					watcher.Dispose();
					isdisposed = true;
				}
			}));
			th.Start();
		}

		private void Watcher_Changed(object sender, FileSystemEventArgs e)
		{
			watcher.EnableRaisingEvents = false;
			mre.Set();
			watcher.EnableRaisingEvents = true;
		}

		public void Dispose()
		{
			isexit = true;
			if (!isdisposed)
			{
				mre.Set();
			}
		}
	}
}
