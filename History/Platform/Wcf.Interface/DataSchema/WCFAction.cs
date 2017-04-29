using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using XElements = System.Collections.Generic.IEnumerable<System.Xml.Linq.XElement>;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;

namespace Wcf.Interface.DataSchema
{
	public class WcfAction
	{
		public string Name;
		public object[] Parameters;
		public static Collection<WcfAction> ParseEmpty(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			string path = AppDomain.CurrentDomain.BaseDirectory + "DataSchema\\EmptyAction.xml";
			string xml = File.ReadAllText(path);
			xml = xml.Replace("%ActionName%", name);
			return Parse(xml);
		}
		public static Collection<WcfAction> Parse(string xml)
		{
			if (string.IsNullOrEmpty(xml))
			{
				return null;
			}
			Collection<WcfAction> actions = new Collection<WcfAction>();
			XDocument doc = XDocument.Parse(xml);
			XElements actionEls = doc.Element("actions").Elements("action");
			foreach (XElement actionEl in actionEls)
			{
				XElements paramEls = actionEl.Elements("param");
				WcfAction action = new WcfAction();
				action.Name = actionEl.Attribute("name").Value;
				int count = paramEls.Count<XElement>();
				action.Parameters = new object[count];
				for (int i = 0; i < count; i++)
				{
					XElement el = paramEls.ElementAt<XElement>(i);
					if (el != null)
					{
						action.Parameters[i] = el.Value;
					}
				}
				actions.Add(action);
			}
			return actions;
		}
		public static string Build(string name, params string[] args)
		{
			string path = AppDomain.CurrentDomain.BaseDirectory + "Templates\\ActionTemplate.xml";
			string xml = File.ReadAllText(path);
			string paramTemplate = "<param>{0}</param>\r\n";
			string p = string.Empty;
			foreach (string i in args)
			{
				p += string.Format(paramTemplate, i);
			}
			xml = string.Format(xml, name, "\r\n" + p);
			return xml;
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
