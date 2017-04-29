using ULib.Core;
using Wcf.Interface.DataSchema;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using Wcf.Interface;

namespace Wcf.Service.Executors
{
	abstract class ActionExecutor
	{
		static ActionExecutor()
		{
			AddExecutor(ActionNames.AddUrlAction, typeof(FavUrlExecutor).FullName, ActionNames.AddUrlAction);
			AddExecutor(ActionNames.GetUrlsAction, typeof(FavUrlExecutor).FullName, ActionNames.GetUrlsAction);
		}
		static Dictionary<string, ActionExecutor> execmappings = new Dictionary<string, ActionExecutor>();
		static Dictionary<string, MethodInfo> methodmappings = new Dictionary<string, MethodInfo>();
		public static void AddExecutor(string key, string typeName, string methodName)
		{
			Type type = Type.GetType(typeName, false);
			if (!string.IsNullOrEmpty(key) && type != null)
			{
				ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
				if (constructor != null)
				{
					ActionExecutor executor = constructor.Invoke(null) as ActionExecutor;
					if (executor != null)
					{
						MethodInfo[] list = type.GetMethods();
						if (list != null)
						{
							execmappings[key] = executor;
							for (int i = 0; i < list.Length; i++)
							{
								MethodInfo mi = list[i];
								if (string.Equals(methodName, mi.Name))
								{
									methodmappings[key] = mi;
								}
							}
						}
					}
				}
			}
		}
		public static ExecuteResultSet ExecuteActions(WcfActions actions)
		{
			ExecuteResultSet rlt = new ExecuteResultSet();
			if (actions != null)
			{
				foreach (WcfAction action in actions.Actions)
				{
					ExecuteAction(rlt, action);
				}
			}
			return rlt;
		}

		public static void ExecuteAction(ExecuteResultSet rlt, WcfAction action)
		{
			ActionExecutor exe = GetExecutor(action);
			if (exe != null)
			{
				rlt.Add(exe.Execute(action));
			}
		}

		public static ActionExecutor GetExecutor(WcfAction action)
		{
			if (action != null)
			{
				return GetExecutor(action.Name);
			}
			return null;
		}
		public static ActionExecutor GetExecutor(string key)
		{
			if (execmappings.ContainsKey(key))
			{
				return execmappings[key];
			}
			return null;
		}
		public virtual ExecuteResult Execute(WcfAction action)
		{
			ExecuteResult result = new ExecuteResult();
			if (action != null && execmappings.ContainsKey(action.Name) && methodmappings.ContainsKey(action.Name))
			{
				MethodInfo mi = methodmappings[action.Name];
				ActionExecutor instance = execmappings[action.Name];
				Logger.Log("Action {0} begin -------------------------\r\nParameter: \r\n{1}", action.Name, action.Parameters.ToXml());
				try
				{
					result.ActionName = action.Name;
					if (mi.ReturnType != null)
					{
						result.Result = mi.Invoke(instance, action.Parameters);
					}
					else
					{
						result.Result = null;
					}
				}
				catch (Exception e)
				{
					result.LastError = e;
					Logger.Log(e.Message);
				}
				Logger.Log("Action {0} end -------------------------\r\n", action.Name);
			}
			return result;
		}
	}
}
