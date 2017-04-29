using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace Joy.Common.DataSchema
{
	public class XReader : NodeReaderBase, IEnumerator, IEnumerable, IDisposable
	{
		protected XmlNode offsetRoot;
		protected XmlDocument rootDoc;
		protected int enumIndex;
		protected string fullPath;
		public string Name
		{
			get
			{
				return NodeContent<XmlNode>().Name;
			}
		}
		public new string Value
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
		public new XReader this[string key]
		{
			get
			{
				return (XReader)SwitchToChild(key);
			}
		}
		public XReader()
		{
			rootDoc = new XmlDocument();
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
		public XReader(object content, XmlDocument doc)
		{
			if (content is XmlNode)
			{
				Load(content, NodeReaderType.Node, AllowAutoExtend);
			}
			else
			{
				Load(content, NodeReaderType.Value, AllowAutoExtend);
			}
			rootDoc = doc;
		}
		public XReader(string xmlfile)
		{
			rootDoc = new XmlDocument();
			if (!File.Exists(xmlfile))
			{
				StreamWriter sw = File.CreateText(xmlfile);
				sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
				sw.WriteLine("<root></root>");
				sw.Close();
			}
			rootDoc.Load(xmlfile);
			fullPath = xmlfile;
			nodePtr = new XReader();
			Load(rootDoc, NodeReaderType.Node, true);
		}
		public XReader(XmlDocument xdoc)
		{
			rootDoc = xdoc;
			fullPath = null;
			nodePtr = new XReader();
			Load(rootDoc, NodeReaderType.Node, true);
		}
		public XReader Restore()
		{
			Load(offsetRoot, NodeReaderType.Node, AllowAutoExtend);
			return this;
		}
		public XReader Reset()
		{
			Load(rootDoc, NodeReaderType.Node, AllowAutoExtend);
			return this;
		}
		public void MakeRoot()
		{
			//this.root = new XmlDocument();
			this.offsetRoot = this.NodeContent<XmlNode>();
		}
		public void Load(string xmlfile)
		{
			if (rootDoc == null)
			{
				rootDoc = new XmlDocument();
			}
			if (File.Exists(xmlfile))
			{
				fullPath = xmlfile;
				rootDoc.Load(xmlfile);
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
				rootDoc.Save(xmlfile);
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
					XmlElement el = NodeContent<XmlElement>();
					if (!el.HasAttribute(key))
					{
						NodeContent<XmlElement>().Attributes.Append(rootDoc.CreateAttribute(key));
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
					XmlAttribute attr = rootDoc.CreateAttribute(key);
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
					XmlNode el = rootDoc.CreateElement(key);
					el.AppendChild(rootDoc.CreateTextNode((string)value));
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
					XmlText node = rootDoc.CreateTextNode((string)value);
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
					XmlNodeList l = el.GetElementsByTagName(key);

					for (int i = 0; i < l.Count; )
					{
						el.RemoveChild(l[i]);
					}
					
					//foreach (XmlNode n in el.ChildNodes)
					//{
					//    if (n.Name == key)
					//    {
					//        el.RemoveChild(key);
					//    }
					//}
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
		public override object AddValue(string key, object value)
		{
			object o = base.AddValue(key, value);
			XReader rlt = new XReader(o, rootDoc);
			return rlt;
		}

		#region IEnumerator Members

		public object Current
		{
			get {
				if (!IsInvalid)
				{
					if (nodePtr == null)
					{
						XReader xr = new XReader();
						nodePtr = xr;
						xr.offsetRoot = NodeContent<XmlNode>().ChildNodes[enumIndex];
					}
					nodePtr.Load(NodeContent<XmlNode>().ChildNodes[enumIndex], NodeReaderType.Node, AllowAutoExtend);
					
					(nodePtr as XReader).offsetRoot = NodeContent<XmlNode>().ChildNodes[enumIndex];
					(nodePtr as XReader).rootDoc = rootDoc;
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

		#region IDisposable Members

		void IDisposable.Dispose()
		{
			if (rootDoc != null)
			{
				rootDoc = null;
			}
		}

		#endregion
	}
}
