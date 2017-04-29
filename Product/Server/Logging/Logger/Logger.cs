using Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class Logger : IDisposable
    {
		protected LogQueue queue = new LogQueue();

	    protected dynamic settings;

	    public Logger() : this(new Dobj())
	    {
		    
	    }

	    public Logger(dynamic config)
	    {
		    settings = config;
	    }

	    public virtual void Log(dynamic entry)
	    {
		    queue.Enqueue(entry);
	    }

	    public virtual void Log(string msg, string src = null, string category = null, string session = null, Level lvl = Level.Info)
	    {
		    dynamic entry = new Entry();
		    Dobj.Settings(entry).AutoCreate = true;
		    entry.Level = lvl;
		    entry.Message = msg;
		    entry.Category = category ?? "Log";
		    entry.Source = src ?? Assembly.GetCallingAssembly().FullName;
		    entry.Session = session;
			Log(entry);
	    }

	    public void Dispose()
	    {
		    queue.Dispose();
	    }
    }

}
