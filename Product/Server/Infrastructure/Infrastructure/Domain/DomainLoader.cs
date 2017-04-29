using Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Infrastructure
{
	public class DomainLoader
	{
		protected AppDomain domain { get; set; }
		protected string name { get; set; }
		protected string probedir { get; set; }

		public DomainLoader(string name, string probedir = "shadow")
		{
			this.name = name;
			this.probedir = probedir;
			domain = Asm.Domain(name, probedir);
		}

		public void LoadBytes(string dll, Action<Assembly> callback)
		{
			if (!string.IsNullOrWhiteSpace(dll) && callback != null)
			{
				var bytes = File.ReadAllBytes(dll);
				File.Delete(dll);
				Asm.Load(domain, bytes, callback);
			}
		}

		public void LoadFile(string dll, Action<Assembly> callback)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(dll) && callback != null)
				{
					Asm.Load(domain, dll, callback);
				}
			}
			catch (Exception ex)
			{
				log.e(ex);
			}
		}

		public void Unload(Action<Assembly> callback = null)
		{
			Asm.Unload(domain, callback);
			AppDomain.Unload(domain);
			domain = Asm.Domain(name, probedir);
		}
	}

	public class DomainsLoader
	{
		protected static Dictionary<string, AppDomain> cache { get; private set; } = new Dictionary<string, AppDomain>();
		protected string watchdir;
		protected string probedir;

		public DomainsLoader(string watchdir = "addins", string probedir = "shadow")
		{
			this.watchdir = watchdir;
			this.probedir = probedir;
		}

		public void LoadFile(string dll, Action<Assembly> callback)
		{
			try
			{
				var name = dll.PathWithoutFilename().PathLastName();
				if (!string.IsNullOrWhiteSpace(dll) && callback != null)
				{
					var path = $"{AppDomain.CurrentDomain.BaseDirectory}{probedir}\\{dll.PathWithoutFilename().PathLastName()}";
					var domain = UseDomain(name, path);
					if (domain != null)
					{
						Asm.Load(domain, dll, callback);
					}
				}
			}
			catch (Exception ex)
			{
				log.e(ex);
			}
		}

		public static AppDomain UseDomain(string name,string path = null)
		{
			AppDomain domain = null;
			if (!string.IsNullOrWhiteSpace(name))
			{
				if (cache.ContainsKey(name))
				{
					domain = cache[name];
				}
				else if (path != null)
				{
					domain = Asm.Domain(name, path);
					cache[name] = domain;
				}
			}
			return domain;
		}

		public void Execute(string name, Action<object[]> callback, params object[] args)
		{
			var domain = UseDomain(name);
			if (domain != null)
			{
				
			}
		}

		public void Unload(Action<Assembly> callback = null)
		{
			var tmp = cache;
			cache = new Dictionary<string, AppDomain>();
			foreach (var i in tmp.Keys)
			{
				var domain = tmp[i];
				var path = domain.BaseDirectory;
				Asm.Unload(domain, callback);
				AppDomain.Unload(domain);
				cache[i] = Asm.Domain(i, path);
			}
		}
	}
}
