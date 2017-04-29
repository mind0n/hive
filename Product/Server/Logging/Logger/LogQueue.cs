using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logger
{
	public class LogQueue : IDisposable
	{
		public delegate void HandleDequeue(Entry entry);
		public event HandleDequeue OnDequeue;
		protected ConcurrentQueue<Entry> queue = new ConcurrentQueue<Entry>();
		protected ManualResetEvent mre = new ManualResetEvent(false);
		protected bool isexit;

		public LogQueue()
		{
			ThreadStart ts = new ThreadStart(() =>
			{
				using (mre)
				{
					while (!isexit)
					{
						if (OnDequeue != null)
						{
							var q = queue;
							queue = new ConcurrentQueue<Entry>();
							for (var i = 0; i < 10; i++)
							{
								Entry item;
								if (q.TryDequeue(out item))
								{
									if (item != null)
									{
										OnDequeue(item);
									}
								}
								else
								{
									if (q.Count > 0)
									{
										Thread.Sleep(10);
										continue;
									}
									else
									{
										break;
									}
								}
							}
						}
						mre.WaitOne();
					}
				}
			});
			Thread th = new Thread(ts);
			th.Start();
		}

		public void Enqueue(Entry item)
		{
			if (OnDequeue != null)
			{
				queue.Enqueue(item);
			}
			mre.Set();
		}

		public void Dispose()
		{
			isexit = true;
			mre.Set();
		}
	}
}
