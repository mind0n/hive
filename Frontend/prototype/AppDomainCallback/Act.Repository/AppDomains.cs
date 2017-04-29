using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Act.Repository
{
    public class AppDomains
    {
        static ConcurrentDictionary<string, AppDomainAdapter> cache { get; } = new ConcurrentDictionary<string, AppDomainAdapter>();

        public static AppDomainAdapter Use(string name)
        {
            AppDomainAdapter ad = null;
            if (!cache.ContainsKey(name))
            {
                var dm = AppDomain.CreateDomain(name, AppDomain.CurrentDomain.Evidence);
                ad = new AppDomainAdapter(dm);
                cache[name] = ad;
            }
            ad = cache[name];
            return ad;
        }

        public static AppDomainAdapter Require(string name, int timeout = 0)
        {
            var dt = DateTime.Now;
            while (!cache.ContainsKey(name))
            {
                Thread.Sleep(1000);
                if (timeout > 0)
                {
                    var ts=  DateTime.Now - dt;
                    if (ts.TotalMilliseconds > timeout)
                    {
                        return null;
                    }
                }
            }
            return cache[name];
        }

        public static void Unload(string name)
        {
            if (cache.ContainsKey(name))
            {
                AppDomainAdapter o = null;
                while (!cache.TryRemove(name, out o))
                {
                    Thread.Sleep(100);
                }
				if (o != null)
				{
					o.Unload();
				}
            }
        }
    }

	[Serializable]
    public class AppDomainAdapter
	{
		public const string KEY_EXCHANGE = "_exchange_";
        protected AppDomain dm;
        public string BaseDirectory { get { return dm.BaseDirectory; } }
        public string SearchDirectory { get { return dm.RelativeSearchPath; } }
        public string Name { get { return $"{dm.Id}_{dm.FriendlyName}"; } }
        public AppDomainAdapter(AppDomain domain)
        {
            dm = domain;
	        var ec = dm.GetData(KEY_EXCHANGE);
	        if (ec == null)
	        {
		        ec = new ConcurrentDictionary<string, object>();
				dm.SetData(KEY_EXCHANGE, ec);
	        }
        }

		public void Unload()
		{
			if (dm != null)
			{
				AppDomain.Unload(dm);
			}
		}

	    public AppDomainTaskResult Execute(Func<object[], object> callback, params object[] args)
	    {
			var task = new AppDomainTask(callback, args);
			try
			{
				dm.DoCallBack(task.Run);
				var ec = Exchanger(dm);
				if (ec != null)
				{
					return ec[task.Id] as AppDomainTaskResult;
				}
			}
			finally
			{
				Exchanger(dm).Delete(task.Id);
			}
			return null;
		}

		public static ConcurrentDictionary<string, object> Exchanger(AppDomain domain)
		{
			return domain.GetData(KEY_EXCHANGE) as ConcurrentDictionary<string, object>;
		}
	}

	[Serializable]
	public class AppDomainTaskResult
	{
		public object Result { get; protected set; }
		public Exception Error { get; protected set; }

		public bool NoError
		{
			get { return Error == null; }
		}

		public AppDomainTaskResult(object result, Exception ex = null)
		{
			Result = result;
			Error = ex;
		}
	}

	[Serializable]
	public class AppDomainTask
	{
		protected bool disposed;
		protected Func<object[], object> handler;
		protected object[] pars;

		public string Id { get; } = Guid.NewGuid().ToString();

		public object Result
		{
			get { return exchanger[Id]; }
		}

		public AppDomainTask(Func<object[], object> callback, params object[] args)
		{
			handler = callback;
			pars = args;
		}

		internal void Run()
		{
			try
			{
				if (handler != null)
				{
					var rlt = handler(pars);
					exchanger[Id] = new AppDomainTaskResult(rlt);
				}
				else
				{
					exchanger[Id] = new AppDomainTaskResult(null);
				}
			}
			catch (Exception ex)
			{
				exchanger[Id] = new AppDomainTaskResult(null, ex);
			}
		}

		protected ConcurrentDictionary<string, object> exchanger
		{
			get
			{
				return AppDomain.CurrentDomain.GetData(AppDomainAdapter.KEY_EXCHANGE) as ConcurrentDictionary<string, object>;
			}
		} 
	}
}
