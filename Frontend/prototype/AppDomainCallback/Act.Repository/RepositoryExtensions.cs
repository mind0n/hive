using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Act.Repository
{
	public static class RepositoryExtensions
	{
		public static void Delete(this ConcurrentDictionary<string, object> o, string key)
		{
			object item = null;
			while (!o.TryRemove(key, out item))
			{
				Thread.Sleep(100);
			}
		}
	}
}
