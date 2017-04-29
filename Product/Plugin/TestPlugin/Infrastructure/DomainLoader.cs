using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
	public class DomainLoader
	{
		protected AppDomain domain { get; }
		public DomainLoader(string name)
		{
			domain = Asm.Domain(name);
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
			if (!string.IsNullOrWhiteSpace(dll) && callback != null)
			{
				Asm.Load(domain, dll, callback);
			}
		}

		public void Unload()
		{
			AppDomain.Unload(domain);
		}
	}
}
