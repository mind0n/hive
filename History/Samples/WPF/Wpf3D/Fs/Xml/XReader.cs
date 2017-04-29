using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Fs.Collections;
using System.IO;
using System.Collections;

namespace Fs.Xml
{
	public class XReader : NodeReaderBase, IEnumerator, IEnumerable
	{
		protected XmlDocument root;
		protected int enumIndex;
		protected string fullPath;
		public string Name
		{
			get
			{
				return NodeContent<XmlNode>().Name;
			}
		}
		public string Value
		{
			get
			{
				return (string)base.Value;
			}
			set
			{
				base.Value = value;
			}
		}
		public XReader this[string key]
		{
			get
			{
				return (XReader)SwitchToChild(key);
			}
		}
		public XReader()
		{
			root = new XmlDocument();
			fullPath = null;
		}
		public XReader(object content)
		{
			if (content is XmlNode)
			{
				Load(content, NodeReaderType.Node, AllowAutoExtend);
			}
			else
			{
				Load(content, NodeReaderType.Value, AllowAutoExtend);
			}
		}
		public XReader(string xmlfile)
		{
			root = new XmlDocument();
			if (!File.Exists(xmlfile))
			{
				StreamWriter sw = File.CreateText(xmlfile);
				sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
				sw.WriteLine("<root></root>");
				sw.Close();
			}
			root.Load(xmlfile);
			fullPath = xmlfile;
			nodePtr = new XReader();
			Load(root, NodeReaderType.Node, true);
		}
		public XReader(XmlDocument xdoc)
		{
			root = xdoc;
			fullPath = null;
			nodePtr = new XReader();
			Load(root, NodeReaderType.Node, true);
		}
		public XReader Reset()
		{
			Load(root, NodeReaderType.Node, AllowAutoExtend);
			return this;
		}
		public void Load(string xmlfile)
		{
			if (root == null)
			{
				root = new XmlDocument();
			}
			if (File.Exists(xmlfile))
			{
				fullPath = xmlfile;
				root.Load(xmlfile);
			}
		}
		public bool Save()
		{
			return Save(fullPath);
		}
		public bool Save(string xmlfile)
		{
			if (!string.IsNullOrEmpty(xmlfile))
			{
				root.Save(xmlfile);
				return true;
			}
			return false;
		}
		protected override object GetChildContent(string key)
		{
			if (!string.IsNullOrEmpty(key))
			{
				if (NodeContent<object>() == null)
				{
					SetChildContent(key, null);
				}
				if (key.IndexOf('$') == 0)
				{
					key = key.Substring(1);
					if (!NodeContent<XmlElement>().HasAttribute(key))
					{
						NodeContent<XmlElement>().Attributes.Append(root.CreateAttribute(key));
					}
					return NodeContent<XmlElement>().GetAttribute(key);
				}
				else
				{
					return GetChildContentNodeByName(key);
					//return NodeContent<XmlElement>().GetElementsByTagName(key);
				}
			}
			else
			{
				if (vNodeType == NodeReaderType.Node)
				{
					XmlNode rlt = NodeContent<XmlNode>();
					if (rlt != null && rlt.FirstChild != null && rlt.FirstChild.NodeType == XmlNodeType.Text)
					{
						return rlt.FirstChild.Value;
					}
					return null;
				}
				else if (vNodeType == NodeReaderType.Value)
				{
					return NodeContent<string>();
				}
				return NodeContent<object>().ToString();

			}
		}
		protected override object SetChildContent(string key, object value, bool overwrite)
		{
			if (!string.IsNullOrEmpty(key))
			{
				if (key.IndexOf('$') == 0)
				{
					key = key.Substring(1);
					XmlAttribute attr = root.CreateAttribute(key);
					attr.Value = (string)value;
					if (NodeContent<object>() != null)
					{
						XmlAttribute old = NodeContent<XmlNode>().Attributes[key];
						if (old == null)
						{
							NodeContent<XmlNode>().Attributes.Remove(old);
						}
					}
					NodeContent<XmlNode>().Attributes.Append(attr);

				}
				else
				{
					XmlNode el = root.CreateElement(key);
					el.AppendChild(root.CreateTextNode((string)value));
					XmlElement old = (XmlElement)GetChildContentNodeByName(key);
					if (old == null)
					{
						NodeContent<XmlNode>().AppendChild(el);
					}
					else
					{
						if (overwrite)
						{
							NodeContent<XmlNode>().ReplaceChild(el, old);
						}
						else
						{
							NodeContent<XmlNode>().AppendChild(el);
						}
					}
					return el;
				}
			}
			else
			{
				if (NodeContent<XmlNode>().FirstChild.NodeType == XmlNodeType.Text)
				{
					NodeContent<XmlNode>().FirstChild.Value = (string)value;
				}
				else
				{
					XmlText node = root.CreateTextNode((string)value);
					NodeContent<XmlNode>().PrependChild(node);
				}
			}
			return NodeContent<XmlNode>();
		}
		protected override object AddChildContent(string key, object value)
		{
			return SetChildContent(key, value, false);
		}
		public XReader EnumChilds(NodeReaderBase.EnumChildContentHandler callback)
		{
			return (XReader)EnumChild(callback);
		}
		protected override object EnumChild(NodeReaderBase.EnumChildContentHandler callback)
		{
			IEnumerator en = GetEnumerator();
			en.Reset();
			while (en.MoveNext())
			{
				if (callback != null)
				{
					if (!callback(en.Current))
					{
						return en.Current;
					}
				}
			}
			return null;
		}
		protected override bool RemoveChildContent(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				NodeContent<XmlNode>().Value = "";
			}
			else
			{
				if (key.IndexOf('$') == 0)
				{
					key = key.Substring(1);
					NodeContent<XmlElement>().RemoveAttribute(key);
				}
				else
				{
					XmlElement el = NodeContent<XmlElement>();
					el.RemoveChild(el.GetElementsByTagName(key)[0]);
				}
			}
			return true;
		}
		protected XmlNode GetChildContentNodeByName(string key)
		{
			foreach (XmlNode node in NodeContent<XmlNode>().ChildNodes)
			{
				if (node.Name == key)
				{
					return node;
				}
			}
			return null;
		}
		protected XmlNode GetChildContentAttributeByName(string key)
		{
			foreach (XmlNode node in NodeContent<XmlNode>().ChildNodes)
			{
				if (node.Name == key)
				{
					return node;
				}
			}
			return null;
		}
		protected override NodeReaderType DetectNodeTypeFromKeyName(string key)
		{
			if (key.IndexOf('$') == 0)
			{
				return NodeReaderType.Value;
			}
			else
			{
				return NodeReaderType.Node;
			}
		}

		#region IEnumerator Members

		public object Current
		{
			get {
				if (!IsInvalid)
				{
					if (nodePtr == null)
					{
						nodePtr = new XReader();
					}
					nodePtr.Load(NodeContent<XmlNode>().ChildNodes[enumIndex], NodeReaderType.Node, AllowAutoExtend);
					return nodePtr;
				}
				return null;
			}
		}

		public bool MoveNext()
		{
			enumIndex++;
			//nodePtr.Load(NodeContent<XmlNode>().ChildNodes[enumIndex], NodeReaderType.Node, AllowAutoExtend);
			return enumIndex < NodeContent<XmlNode>().ChildNodes.Count;
		}

		void IEnumerator.Reset()
		{
			enumIndex = -1;
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			((IEnumerator)this).Reset();
			return this;
		}

		#endregion


		protected override bool ValueEquals(object value)
		{
			if (!IsEmpty)
			{
				return Value.Equals((string)value);
			}
			return value == null;
		}

		protected override string GetContentKeyString()
		{
			if (!IsEmpty)
			{
				return NodeContent<XmlNode>().Name;
			}
			return null;
		}

		protected override int GetChildCount()
		{
			if (!IsEmpty && vNodeType == NodeReaderType.Node)
			{
				XmlNode n = NodeContent<XmlNode>();
				return n.ChildNodes.Count;
			}
			return 0;
		}

		protected override object GetChildContent(int index)
		{
			if (!IsEmpty && vNodeType == NodeReaderType.Node)
			{
				XmlNode n = NodeContent<XmlNode>();
				return n.ChildNodes[index];
			}
			return null;
		}

		protected override NodeReaderBase CreateInstance(object content)
		{
			return new XReader(content);
		}
	}
}
