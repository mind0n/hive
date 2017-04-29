using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using XnaEngine.XnaGame;

namespace XnaEngine.Threads
{
	public class ThreadParameter
	{
		private object argument;
		public virtual T Argument<T>(int index)
		{
			try
			{
				if (index < 0)
				{
					T result = (T)argument;
					return result;
				}
				else
				{
					object[] list = argument as object[];
					if (list != null && list.Length > index)
					{
						T result = (T)list[index];
						return result;
					}
					return default(T);
				}
			}
			catch
			{
				return default(T);
			}
		}
		public void Argument(object value)
		{
			if (value != null)
			{
				argument = value;
			}
		}
		public ThreadParameter(object arg)
		{
			argument = arg;
		}
	}
	public class ThreadResult
	{
	}
	public class ThreadController
	{
		public Dictionary<string, List<Thread>> Threads = new Dictionary<string, List<Thread>>();
		public Func<ThreadParameter, ThreadResult> Worker;
		public Action<ThreadResult> ThreadComplete;

		public void StartNew(string name, params object[] args)
		{
			Create(name);
			Start(name, args);
		}

		public void Start(string name, params object[] args)
		{
			EnumThread(name, delegate(Thread thread)
			{
				if (thread.ThreadState == ThreadState.Unstarted)
				{
					thread.Start(args);
					return true;
				}
				return false;
			});
		}

		public void Create(string name)
		{
			if (Worker != null)
			{
				ParameterizedThreadStart pts = new ParameterizedThreadStart(WorkerThread);
				Thread thread = new Thread(pts);
				if (!Threads.ContainsKey(name) || Threads[name] == null)
				{
					Threads[name] = new List<Thread>();
				}
				Threads[name].Add(thread);
			}
		}

		public void Stop(string name)
		{
			if (!string.IsNullOrEmpty(name) && Threads.ContainsKey(name))
			{
				StopThreads(Threads[name]);
				Threads[name] = null;
			}
		}

		public void StopAll()
		{
			foreach (KeyValuePair<string, List<Thread>> pair in Threads)
			{
				StopThreads(pair.Value);
			}
		}

		protected void WorkerThread(object args)
		{
			if (Worker != null)
			{
				ThreadParameter par = new ThreadParameter(args);
				ThreadResult result = Worker(par);
				if (ThreadComplete != null)
				{
					ThreadComplete(result);
				}
			}
		}
	
		protected void StopThreads(List<Thread> threads)
		{
			foreach (Thread thread in threads)
			{
				thread.Abort();
			}
			threads.Clear();
		}

		private void EnumThread(string name, Func<Thread, bool> callback)
		{
			if (!string.IsNullOrEmpty(name) && Threads.ContainsKey(name))
			{
				for (int i = Threads[name].Count - 1; i >= 0; i--)
				{
					Thread thread = Threads[name][i];
					if (callback != null)
					{
						if (!callback(thread))
						{
							Threads[name].RemoveAt(i);
						}
					}
				}
			}
		}
	}
}
