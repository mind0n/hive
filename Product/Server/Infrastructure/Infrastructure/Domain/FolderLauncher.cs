using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Logger;
using System.Threading;
using Settings;

namespace Infrastructure
{
	public class Launcher
	{
		public static Launcher Instance { get; private set; }
		private OwinAdapter adapter { get;  }
		protected FileSetting settings { get; } = new FileSetting(new {name = "launcher.json"});

		public Launcher()
		{
			Instance = this;
			adapter = new OwinAdapter(settings.Instance.owin);
			adapter.Start();
		}

		public virtual void Launch()
		{
		}

		public virtual void Shutdown()
		{
			adapter.Stop();
			DomainHelper.Unload();
		}

		public virtual void Reset()
		{
			DomainHelper.Unload();
			Launch();
		}
	}
	public class FolderLauncher : Launcher
	{
		private string basedir { get; } = AppDomain.CurrentDomain.BaseDirectory;
		private string importfolder { get; } = "addins";
		private string loadfolder { get; } = "shadow";
		protected string ImportPath
		{
			get { return $"{basedir}{importfolder}"; }
		}
		protected string LoadPath
		{
			get { return $"{basedir}{loadfolder}"; }
		}

		public override void Launch()
		{
			InitDomain();
			LoadAddins();
		}

		private void LoadAddins()
		{
			var entries = Directory.GetFiles(LoadPath, "*.entry.dll", SearchOption.AllDirectories);
			foreach (var i in entries)
			{
				DomainHelper.Load(i, (asm) =>
				{
					var type = asm.FindType(".entry");
					if (type != null)
					{
						var mi = type.GetMethod("Start");
						if (mi != null)
						{
							mi.Invoke(null, null);
						}
					}
				});
			}
		}

		private void InitDomain()
		{
			var addins = Directory.GetDirectories(ImportPath);
			foreach (var i in addins)
			{
				PreloadFiles(i);
			}
		}

		private void PreloadFiles(string addindir)
		{
			var files = Directory.GetFiles(addindir, "*.*", SearchOption.AllDirectories);
			foreach (var file in files)
			{
				if (file.EndsWith(".pdb"))
				{
					continue;
				}
				var target = file.PathMove(LoadPath, "addins");
				var dir = target.PathWithoutFilename();
				if (!Directory.Exists(dir))
				{
					Directory.CreateDirectory(dir);
				}
				if (File.Exists(target))
				{
					File.Delete(target);
				}
				File.Move(file, target);
			}
		}
	}
}
