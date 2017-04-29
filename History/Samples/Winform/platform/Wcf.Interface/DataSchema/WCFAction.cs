using System;
using System.Collections.Generic;
using ULib.Core;

namespace Wcf.Interface.DataSchema
{
	public class WcfActions
	{
		public readonly List<WcfAction> Actions = new List<WcfAction>();

		public WcfActions() { }
		public WcfActions(params WcfAction[] actions)
		{
			Actions.AddRange(actions);
		}

		public static string CreateXml(params WcfAction[] actions)
		{
			WcfActions r = new WcfActions(actions);
			return r.ToXml();
		}

		public void Add(string name, params object[] args)
		{
			WcfAction act = new WcfAction();
			act.Name = name;
			act.Parameters = args;
			Actions.Add(act);
		}

		public void Remove(string name = null)
		{
			if (string.IsNullOrEmpty(name))
			{
				Actions.Clear();
			}
			else
			{
				for (int i = 0; i < Actions.Count; i++)
				{
					if (string.Equals(Actions[i].Name, name))
					{
						Actions.RemoveAt(i);
						i--;
					}
				}
			}
		}
	}

	public class WcfAction
	{
		public string Name;
		public object[] Parameters;
		public static WcfActions ParseEmpty(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			WcfActions rlt = new WcfActions();
			rlt.Add(name);
			return rlt;
		}
		public static WcfActions Parse(string xml)
		{
			if (string.IsNullOrEmpty(xml))
			{
				return null;
			}
			try
			{
				WcfActions result = xml.FromXml<WcfActions>();
				return result;
			}
			catch
			{
				return null;
			}
		}
		public static WcfAction BuildAction(string name, params object[] args)
		{
			return new WcfAction { Name = name, Parameters = args };
		}
		public bool IsAction(string act)
		{
			return string.Equals(act, Name, StringComparison.OrdinalIgnoreCase);
		}
	}

	public class WCFActionParam
	{
		public object Value;
		public T GetValue<T>()
		{
			if (Value != null)
			{
				return (T)Value;
			}
			else
			{
				return default(T);
			}
		}
	}
	
}
