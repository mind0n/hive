using System;
using System.Collections.Generic;
using System.Text;
using Dw.Module;
using Native.Desktop;

namespace Dw.Plugins
{
	public class RemindModule : FunctionModule
	{
		public static List<SavableReminder> Reminders;
		private static new RemindModule _instance;
		private RemindModule()
		{
			IsSingleton = true;
		}
		public static RemindModule GetInstance()
		{
			return _instance;
		}
		static RemindModule()
		{
			Reminders = new List<SavableReminder>();
			_instance = new RemindModule();
		}
	}
}
