using Joy.Core.DataSchema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Joy.Core.Xml
{
	public class XReader
	{
		public delegate void ReadHandler(XmlNode node);
		public event ReadHandler OnReadStartElement;
		public event ReadHandler OnReadTextElement;
		public event ReadHandler OnReadEndElement;
		protected XmlNode root;
		protected Stack<XmlNode> parents = new Stack<XmlNode>();
		public XmlNode LoadXml(string xml)
		{
			return LoadXmlString(xml);
		}
		protected XmlNode LoadXmlString(string xml, string rootName = "root")
		{
			XmlReader xr = XmlReader.Create(new StringReader(xml));
			root = null;
			parents.Clear();
			while (xr.Read())
			{
				if (xr.NodeType == XmlNodeType.Element && (xr.IsStartElement() || xr.IsEmptyElement))
				{
					XmlNode node = parents.Count < 1 ? new XmlNode { Name = rootName } : root.New<XmlNode>();
					node.Text = xr.Value;
					node.Name = xr.Name;
					node.NodeType = xr.NodeType;
					node.ValueType = xr.ValueType.FullName;
					if (xr.HasAttributes)
					{
						for (int i = 0; i < xr.AttributeCount; i++)
						{
							XmlAttribute att = new XmlAttribute();
							xr.MoveToAttribute(i);
							att.Name = xr.Name;
							xr.GetAttribute(i);
							att.Text = xr.Value;
							node.Attributes.Add(att);
						}
						xr.MoveToElement();
					}
					if (parents.Count < 1)
					{
						root = node;
					}
					else
					{
						parents.Peek().Add(node);
					}
					if (!xr.IsEmptyElement)
					{
						parents.Push(node);
					}
					if (OnReadStartElement != null)
					{
						OnReadStartElement(node);
					}
				}
				else if (xr.NodeType == XmlNodeType.Text || xr.NodeType == XmlNodeType.CDATA)
				{
					parents.Peek().Text = xr.Value;
					if (OnReadTextElement != null)
					{
						OnReadTextElement(parents.Peek());
					}
				}
				else if (xr.NodeType == XmlNodeType.EndElement)
				{
					if (OnReadEndElement != null)
					{
						OnReadEndElement(parents.Peek());
					}
					parents.Pop();
				}
			}
			return root;
		}
	}
	public class XmlNode : TextNode
	{
		public virtual string this[string key]
		{
			get
			{
				if (string.IsNullOrEmpty(key))
				{
					return null;
				}
				if ("$".Equals(key))
				{
					return Text;
				}
				else if (key.StartsWith("$"))
				{
					string name = key.Substring(1);
					foreach (XmlAttribute a in Attributes)
					{
						if (string.Equals(a.Name, name, StringComparison.OrdinalIgnoreCase))
						{
							return a.Text;
						}
					}
				}
				else
				{
					string name = key.Substring(1);
					foreach (Node n in Children)
					{
						XmlNode xn = n as XmlNode;
						if (xn != null)
						{
							return xn.Text;
						}
					}
				}
				return null;
			}
		}
		public bool IsSelfClosed
		{
			get
			{
				return Children.Count < 1 && string.IsNullOrEmpty(Text);
			}
		}
		public XmlNodeType NodeType;
		public string ValueType;
		public List<XmlAttribute> Attributes = new List<XmlAttribute>();
		public object[] AttributeArray
		{
			get
			{
				if (Attributes.Count > 0)
				{
					object[] rlt = new object[Attributes.Count];
					for (int i = 0; i < rlt.Length; i++)
					{
						rlt[i] = Attributes[i].Text;
					}
					return rlt;
				}
				return null;
			}
		}
		public XmlNode()
		{
			childCreateInstance -= TextNodeCreateInstance;
			childCreateInstance += XmlNodeCreateInstance;			
		}
		public string ToXml(bool addHeader = true)
		{
			const string header = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
			string template = "<{0} {1}>{2}</{0}>";
			string templateSc = "<{0} {1} />";

			List<string> alist = new List<string>();
			List<string> clist = new List<string>();

			if (Attributes.Count > 0)
			{
				foreach (XmlAttribute att in Attributes)
				{
					alist.Add(string.Concat(att.Name, "=\"", att.Text, '"'));
				}
			}
			string a = string.Join(" ", alist.ToArray());
			if (Children.Count > 0)
			{
				foreach (Node n in Children)
				{
					XmlNode child = n as XmlNode;
					if (child != null)
					{
						clist.Add(child.ToXml(false));
					}
				}
			}
			string c = string.Join("\r\n", clist.ToArray());
			string rlt = string.Empty;
			if (IsSelfClosed)
			{
				rlt = string.Format(templateSc, Name, a);
			}
			else
			{
				rlt = string.Format(template, Name, a, Text + c);
			}
			if (addHeader)
			{
				rlt = string.Concat(header, "\r\n", rlt);
			}
			return rlt;
		}
		public override string ToString()
		{
			return ToXml(true);
		}
		protected Node XmlNodeCreateInstance()
		{
			return new XmlNode();
		}
	}
	public class XmlAttribute
	{
		public string Name;
		public string Text;
	}
}
