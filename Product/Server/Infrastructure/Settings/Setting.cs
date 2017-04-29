using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Logger;

namespace Settings
{
    public abstract class Setting
    {
	    protected Dobj cache;

	    protected dynamic setup;

	    public dynamic Instance
	    {
		    get
		    {
			    if (cache == null)
			    {
				    Load();
			    }
			    return cache;
		    }
	    }

	    public Setting(object setup)
	    {
		    this.setup = new Dobj(setup);
	    }

	    public abstract void Load();
    }

	public class FileSetting : Setting
	{
		protected FileSystemWatcher watcher;
		protected string file;
		public FileSetting(object args) : base(args)
		{
			var name = setup.name;
			var files = new string[]
			{
				$"{AppDomain.CurrentDomain.BaseDirectory}settings\\{Environment.MachineName}\\{name}",
				$"{AppDomain.CurrentDomain.BaseDirectory}settings\\{name}",
				$"{AppDomain.CurrentDomain.BaseDirectory}{name}"
			};
			file = files[0];
            foreach (var i in files)
			{
				if (File.Exists(i))
				{
					file = i;
					break;
				}
			}
			watcher = new FileSystemWatcher(file.PathWithoutFilename(), file.PathLastName());
			watcher.Changed += Watcher_Changed;
			watcher.EnableRaisingEvents = true;
		}

		private void Watcher_Changed(object sender, FileSystemEventArgs e)
		{
			watcher.EnableRaisingEvents = false;
			Load();
			watcher.EnableRaisingEvents = true;
		}

		public override void Load()
		{
			if (File.Exists(file))
			{
				var json = File.ReadAllText(file);
				if (!string.IsNullOrWhiteSpace(json))
				{
					cache = Dobj.FromJson(json);
				}
			}
			else
			{
				cache = new Dobj();
			}
		}
	}
}
