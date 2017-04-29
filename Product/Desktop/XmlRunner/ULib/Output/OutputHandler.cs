using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing;

namespace ULib.Output
{
	public class OutputHandler
	{
		public static OutputHandleList Handlers = new OutputHandleList();
		public static void Handle(string msg, int msgType = 0, params string[] args)
		{
			string content = string.Format(msg, args);
			foreach (Action<string, int> handler in Handlers)
			{
				if (handler != null)
				{
					handler(string.Format(msg, args), msgType);
				}
			}
			Console.WriteLine(content);
			Debug.WriteLine(content);
		}
		public static OutputHandleList BackupAndClear()
		{
			OutputHandleList rlt = Handlers;
			Handlers = new OutputHandleList();
			return rlt;
		}
		public static void Restore(OutputHandleList list)
		{
			Handlers.Clear();
			if (list != null)
			{
				Handlers = list;
			}
		}
	}
	public class OutputHandleList : List<Action<string, int>>
	{
	}
}
