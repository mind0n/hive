using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Fs.Xml
{
	public class XReader //: IDisposable
	{
		public enum QueryMode : int
		{
			ReadOnly,
			ReadWrite
		}
		int NodeType;	//0:null;1:attr;2:node;
		public QueryMode Mode = QueryMode.ReadWrite;
		public string Value
		{
			get
			{
				//return _value;
				if (curt.NodeType == XmlNodeType.Attribute || curt.NodeType == XmlNodeType.CDATA || curt.NodeType == XmlNodeType.Text)
				{
					return curt.Value;
				}
				else if (curt.NodeType == XmlNodeType.Element)
				{
					return curt.InnerText;
				}
				else
				{
					return curt.InnerXml;
				}
			}
			set
			{
				try
				{
					if (curt.NodeType == XmlNodeType.Attribute)
					{
						curt.Value = value;
					}
					else if (curt.NodeType == XmlNodeType.Element)
					{
						curt.InnerText = value;
					}
				}
				catch (Exception err)
				{
				}
			}
		}
		public string Name
		{
			get
			{
				if (curt != null)
				{
					return curt.Name;
				}
				return null;
			}
		}
		public XmlAttributeCollection Attributes
		{
			get
			{
				if (curt != null && curt.NodeType == XmlNodeType.Attribute)
				{
					return curt.Attributes;
				}
				return null;
			}
		}
		public XmlNodeList ChildNodes
		{
			get
			{
				if (curt != null && curt.NodeType == XmlNodeType.Element)
				{
					return curt.ChildNodes;
				}
				return null;
			}
		}
		public List<XReader> Children
		{
			get
			{
				List<XReader> rlt;
				if (curt != null && curt.NodeType == XmlNodeType.Element)
				{
					rlt = new List<XReader>();
					foreach (XmlNode n in curt.ChildNodes)
					{
						rlt.Add(new XReader(n));
					}
					return rlt;
				}
				return null;
			}
		}
		XmlNode curt;
		XmlAttribute attr;
		XmlDocument curtDoc;
		protected string FullFilename;
		public XReader this[string nodeName]
		{
			get
			{
				return this[nodeName, null, null];
			}
		}
		public XReader this[string nodeName, string nameAttrValue]
		{
			get
			{
				return this[nodeName, nameAttrValue, null];
			}
		}
		public XReader this[string nodeName, string attrName, string attrValue]
		{
			get
			{
				XReader rlt;
				string key;
				if (NodeType == 0)
				{
					return GetInstance();
				}
				if (!string.IsNullOrEmpty(nodeName))
				{
					XReader x;
					if (nodeName.IndexOf('$') == 0)
					{
						key = nodeName.Substring(1, nodeName.Length - 1);
						foreach (XmlAttribute xa in curt.Attributes)
						{
							if (key.Equals(xa.Name))
							{
								if (attrName == null || (xa.Attributes[attrName] != null && xa.Attributes[attrName].InnerText.Equals(attrValue)))
								{
									x = GetInstance(xa);
									return x;
								}
							}
						}
						if (Mode == QueryMode.ReadOnly)
						{
							x = GetInstance();
						}
						else
						{
							x = Extend(nodeName);
						}
					}
					else
					{
						foreach (XmlNode xn in curt.ChildNodes)
						{
							if (nodeName.Equals(xn.Name))
							{
								if (attrName == null || (xn.Attributes[attrName] != null && xn.Attributes[attrName].InnerText.Equals(attrValue)))
								{
									x = GetInstance(xn);
									return x;
								}
							}
						}
						if (Mode == QueryMode.ReadOnly)
						{
							x = GetInstance();
						}
						else
						{
							x = Extend(nodeName);
						}
					}
					return x;
				}
				else
				{
					return GetInstance();
				}
			}
		}
		public XReader Extend(string name)
		{
			string key;
			XmlDocument xd = new XmlDocument();
			XmlNode xn;
			if (!string.IsNullOrEmpty(name) && name[0] == '$')
			{
				key = name.Substring(1);
				XmlAttribute xa = curtDoc.CreateAttribute(key);
				curt.Attributes.Append(xa);
				return GetInstance(xa);
			}
			else
			{
				key = name;
				xn = curtDoc.CreateElement(key);
				curt.AppendChild(xn);
				return GetInstance(xn);
			}
		}

		public void Load(string fullFilename)
		{
			if (!string.IsNullOrEmpty(fullFilename) && File.Exists(fullFilename))
			{
				XmlDocument xd = new XmlDocument();
				xd.Load(fullFilename);
				curtDoc = xd;
				FullFilename = fullFilename;
				init(xd);
			}
		}
		public void Save()
		{
			if (!string.IsNullOrEmpty(FullFilename))
			{
				Save(FullFilename);
			}
		}
		public void Save(string fullFilename)
		{
			try
			{
				XmlDocument xd = (XmlDocument)curt;
				xd.Save(fullFilename);
			}
			catch (Exception err)
			{
				Exceptions.LogOnly(err);
			}

		}
		public void SetValue(string val)
		{
			if (curt != null)
			{
				curt.InnerText = val;
			}
		}
		public void SetAttr(string val)
		{
			if (attr != null)
			{
				attr.InnerText = val;
			}
		}
		public XReader()
		{
			NodeType = 0;
			//Name = null;
			Value = null;
		}
		public XReader(string FileName)
		{
			Load(FileName);
		}
		public XReader(XmlDocument xdoc)
		{
			curtDoc = xdoc;
			//Name = xdoc.Name;
			init(xdoc);
		}
		public XReader(XmlNode xnode)
		{
			init(xnode);
		}
		public XReader(XmlAttribute xattr)
		{
			attr = xattr;
			NodeType = 1;
			//Name = xattr.Name;
			Value = xattr.InnerText;
			curt = xattr;
		}
		protected void init(XmlNode xnode)
		{
			curt = xnode;
			NodeType = 2;
			//Name = xnode.Name;
			//Value = xnode.InnerText;
		}
		protected void InitInstance(XReader xr)
		{
			xr.Mode = Mode;
			xr.curtDoc = curtDoc;
		}
		protected XReader GetInstance()
		{
			XReader xr = new XReader();
			InitInstance(xr);
			return xr;
		}
		protected XReader GetInstance(XmlAttribute xa)
		{
			XReader xr = new XReader(xa);
			InitInstance(xr);
			return xr;
		}
		protected XReader GetInstance(XmlNode xn)
		{
			XReader xr = new XReader(xn);
			InitInstance(xr);
			return xr;
		}

		//#region IDisposable Members

		//public void Dispose()
		//{

		//}

		//#endregion
	}
}
