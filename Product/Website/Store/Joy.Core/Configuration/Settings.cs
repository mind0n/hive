using Joy.Core.Logging;
using Joy.Core.Xml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Joy.Core.Configuration
{
	public class JSettings : Dictionary<string, object>
	{
		protected string filename;
		protected Stack<object> parents = new Stack<object>();
		public static JSettings Instance = new JSettings();
		public JSettings()
		{
			filename = J.Instance.FileSystem.BaseDir + "Joy.Config";
		}
		public static T Get<T>(string key = "$") where T : class
		{
			if (Instance.ContainsKey(key))
			{
				T rlt = Instance[key] as T;
				return rlt;
			} return default(T);
		}
		public void Load()
		{
			parents.Clear();
			if (J.Instance.FileSystem.Exists(filename))
			{
				string content = J.Instance.FileSystem.Read(filename);
				if (!string.IsNullOrEmpty(content))
				{
					XReader xr = new XReader();
					xr.OnReadStartElement += xr_OnReadStartElement;
					xr.OnReadTextElement += xr_OnReadTextElement;
					xr.OnReadEndElement += xr_OnReadEndElement;
					xr.LoadXml(content);
				}
			}
		}

		void xr_OnReadEndElement(XmlNode node)
		{
			object o = parents.Peek();
			if (o != null)
			{
				SettingInfo info = o as SettingInfo;
				if (info != null && info.IsMethod)
				{
					XmlNode n = info.Node;
					if (n != null)
					{
						if (n.IsSelfClosed)
						{
						}
						else
						{
						}
					}
				}	
			}
			parents.Pop();			
		}

		void xr_OnReadTextElement(XmlNode node)
		{
			object o = parents.Peek();
			if (o != null)
			{
				SettingInfo info = o as SettingInfo;
				if (info != null && !info.IsMethod)
				{
					Type t = info.Target.GetType();
					if (t != null)
					{
						FieldInfo f = t.GetField(node.Name);
						if (f != null)
						{
							f.SetValue(info.Target, Convert.ChangeType(node.Text, f.FieldType, CultureInfo.InvariantCulture));
						}
					}
				}
			}
		}

		void xr_OnReadStartElement(XmlNode node)
		{
			if (parents.Count < 1)
			{
				string asmName = node["$Assembly"];
				if (!string.IsNullOrEmpty(node.Name))
				{
					object o = node.Name.CreateObject(asmName);
					parents.Push(o);
					this["$"] = o;
				}
				else
				{
					ErrorHandler.Raise("Invalid assembly name {0}", asmName);
				}
			}
			else
			{
				object o = parents.Peek();
				Type t = o.GetType();
				FieldInfo f = t.GetField(node.Name);
				if (f != null)
				{
					object v = f.GetValue(o);
					if (v == null)
					{
						if (f.FieldType.IsValueType || "String".Equals(f.FieldType.Name))
						{
							parents.Push(new SettingInfo { Target = o, Node = node });
						}
						else
						{
							v = f.FieldType.CreateObject();
							if (v != null)
							{
								f.SetValue(o, v);
								parents.Push(v);
							}
							else
							{
								ErrorHandler.Raise("Invalid field type {0}", f.FieldType.FullName);
							}
						}
					}
				}
				else
				{
					MethodInfo m = t.GetMethod(node.Name);
					if (node.IsSelfClosed && m != null)
					{
						m.Invoke(o, node.AttributeArray);
					}
				}
			}
		}
	}
	public class SettingInfo
	{
		public Type Type;
		public object Target;
		public string FieldName;
		public MethodInfo Method;
		public XmlNode Node;
		public bool IsMethod
		{
			get
			{
				return Method != null;
			}
		}
	}
}
