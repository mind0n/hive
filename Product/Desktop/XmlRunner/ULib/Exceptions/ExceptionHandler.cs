using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ULib.Exceptions
{
    public class ExceptionHandler
    {
		public static ExceptionHandleList nHandlers = new ExceptionHandleList();
		public static ExceptionHandleList Handlers = new ExceptionHandleList();
        public static void Raise(string msg, params string[] args)
        {
            throw new Exception(string.Format(msg, args));
        }
		public static bool IsFullHandling = true;
        public static void Handle(string error, string source, params string[] args)
        {
            Exception e = new Exception(string.Format(error, args));
            e.Source = source;
            Handle(e);
        }
		public static void AddHandler(Func<Exception, bool> handler, bool isNoisy = false)
		{
			if (isNoisy)
			{
				nHandlers.Add(handler);
			}
			else
			{
				Handlers.Add(handler);
			}
		}
        public static void Handle(Exception error)
        {
            bool handled = false;
			Exception e = error;
			while (e.InnerException != null)
			{
				e = e.InnerException;
			}
			error = e;
            foreach (Func<Exception, bool> handler in Handlers)
            {
                if (handler != null)
                {
                    handled = handled || handler(error);
                }
            }
			if (IsFullHandling)
			{
				foreach (Func<Exception, bool> handler in nHandlers)
				{
					if (handler != null)
					{
						handled = handled || handler(error);
					}
				}
			}
			if (!handled)
            {
                Console.WriteLine(error.ToString());
            }
        }
		public static ExceptionHandleList BackupAndClear()
		{
			ExceptionHandleList rlt = Handlers;
			Handlers = new ExceptionHandleList();
			return rlt;
		}
		public static void Restore(ExceptionHandleList list)
		{
			Handlers.Clear();
			if (list != null)
			{
				Handlers = list;
			}
		}
    }
	public class ExceptionHandleList : List<Func<Exception, bool>>
	{
	}
}
