using Logger;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core;

namespace Infrastructure
{
	public static class DomainHelper
	{
		private static Dictionary<string, DomainAdapter> cache = new Dictionary<string, DomainAdapter>();
			 
		public static DomainAdapter Use(string name, string path = null, bool isreset = false)
		{
			if (cache.ContainsKey(name))
			{
				var rlt = cache[name];
				if (isreset)
				{
					rlt.Reset();
				}
				return rlt;
			}
			if (path == null)
			{
				path = AppDomain.CurrentDomain.BaseDirectory;
			}
			var domain = CreateDomain(name, path);
			var adapter = new DomainAdapter(domain);
			cache[name] = adapter;
			return adapter;
		}

		public static Dictionary<string, DomainAdapter> Instance
		{
			get { return cache; }
		}

		public static AppDomain CreateDomain(string name, string path, bool iscache = false)
		{
			if (!path.EndsWith("\\"))
			{
				path += "\\";
			}
			var domain = AppDomain.CreateDomain(name, AppDomain.CurrentDomain.Evidence, path, path, false);
			if (iscache)
			{
				cache[name] = new DomainAdapter(domain);
			}
			return domain;
		}

		public static void Execute(string name, Action<object[]> callback, params object[] args)
		{
			var d = Use(name);
			Execute(d, callback, args);
		}
        public static void Execute(DomainAdapter adapter, Action<object[]> callback, params object[] args)
		{
			if (adapter != null && callback != null)
			{
				adapter.Execute(callback, args);
			}
		}


		public static void Load(string dll, Action<Assembly> callback, DomainAdapter adapter = null)
		{
			if (!File.Exists(dll))
			{
				log.e($"Unable to find dll: {dll}");
				return;
			}
			if (adapter == null)
			{
				var dir = dll.PathWithoutFilename();
				var name = dll.PathWithoutFilename(true);
				adapter = Use(name, dir);
			}
			Execute(adapter, (args) =>
			{
				var func = (Action<Assembly>) args[0];
				var file = (string)args[1];
				var asm = Assembly.LoadFile(file);
				func(asm);
			}, callback, dll);
		}

		public static void Unload(string name = null)
		{
			if (name != null)
			{
				if (cache.ContainsKey(name))
				{
					var adapter = cache[name];
					adapter.Unload();
					cache.Remove(name);
				}
			}
			else
			{
				foreach (var i in cache)
				{
					var adapter = i.Value;
					adapter.Unload();
				}
				cache.Clear();
			}
		}
	}

	public class DomainAdapter
	{
		public string Name
		{
			get
			{
				if (domain != null)
				{
					return domain.FriendlyName;
				}
				return string.Empty;
			}
		}

		protected AppDomain domain { get; private set; }

		protected ConcurrentDictionary<string, DomainTask> queue { get; } = new ConcurrentDictionary<string, DomainTask>();

		public List<Assembly> GetAssembly(string pattern = null)
		{
			var rlt = new List<Assembly>();
			var asms = domain.GetAssemblies();
			if (pattern == null)
			{
				rlt.AddRange(asms);
			}
			else
			{
				foreach (var i in asms)
				{
					if (i.FullName.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) >= 0)
					{
						rlt.Add(i);
					}
				}
			}
			return rlt;
		}

		public DomainAdapter(AppDomain appdomain)
		{
			var n = appdomain.FriendlyName;
			var d = appdomain.BaseDirectory;
			log.i($"Creating domain adapter for {n}: {d}");
			domain = appdomain;
		}

		public void Execute(Action<object[]> callback, params object[] args)
		{
			if (domain == null)
			{
				log.e($"Domain missing during execution");
				return;
			}
			var n = domain.FriendlyName;
			var d = domain.BaseDirectory;
            log.i($"Executing tasks in domain {n}: {d}");

			var helper = new DomainTask(callback, args);
			while (!queue.TryAdd(helper.Id, helper))
			{
				Thread.Sleep(100);
			}
			domain.DoCallBack(helper.ExecuteCallback);
			DomainTask tmp = null;
			while (!queue.TryRemove(helper.Id, out tmp))
			{
				Thread.Sleep(100);
			}
		}

		public void Unload()
		{
			if (domain != null)
			{
				Execute((args) =>
				{
					var name = AppDomain.CurrentDomain.FriendlyName;
					log.i($"Unloading app domain: {name}");
					var asms = AppDomain.CurrentDomain.GetAssemblies();
					foreach (var i in asms)
					{
						if (i.FullName.IndexOf(".entry,", StringComparison.OrdinalIgnoreCase) >= 0)
						{
							var type = i.FindType(".entry");
							type.DirectInvoke("Stop");
						}
					}
				}, null);
				AppDomain.Unload(domain);
				domain = null;
			}
		}

		public void Reset()
		{
			if (domain == null)
			{
				return;
			}
			var name = domain.FriendlyName;
			var dir = domain.BaseDirectory;
			Unload();
			AppDomain.Unload(domain);
			domain = DomainHelper.CreateDomain(name, dir);
		}
	}

	[Serializable]
	public class DomainTask
	{
		public string Id { get; } = Guid.NewGuid().ToString();

		protected object[] parameter;

		protected Action<object[]> handler;

		protected bool isdone;

		public bool IsDone { get { return isdone; } }

		public void Done()
		{
			isdone = true;
		}

		public DomainTask(Action<object[]> callback, params object[] args)
		{
			handler = callback;
			parameter = args;
		}

		internal void ExecuteCallback()
		{
			try
			{
				if (handler != null)
				{
					handler(parameter);
				}
			}
			catch (Exception ex)
			{
				log.e(ex);
			}
		}
	}
}
