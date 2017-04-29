using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Reflection;

namespace Codgen
{
	public class XmlReaderHelper
	{
		public delegate void EnumAttributeHandler(string key, string val, string nodeName);
		public static void EnumAttributes(XmlReader xr, EnumAttributeHandler callback)
		{
			if (xr.NodeType == XmlNodeType.Element)
			{
				string elName = xr.Name.ToLower();
				for (int i = 0; i < xr.AttributeCount; i++)
				{
					xr.MoveToAttribute(i);
					callback(xr.Name, xr.Value, elName);
				}
				xr.MoveToElement();
				if (!xr.IsEmptyElement)
				{
					xr.MoveToContent();
					callback("$", xr.Value, elName);
				}
			}
		}
		public static string ReadContent(XmlReader xr)
		{
			string rlt;
			if (xr.NodeType != XmlNodeType.Element)
			{
				xr.MoveToElement();
			}
			rlt = xr.ReadString();
			xr.MoveToElement();
			return rlt;
		}
		public static string ReadAttribute(XmlReader xr, string key)
		{
			string rlt;
			if (xr.NodeType == XmlNodeType.Element)
			{
				xr.MoveToAttribute(key);
				rlt = xr.Value;
				xr.MoveToElement();
				return rlt;
			}
			return null;
		}
		public static XmlReader ReadToNode(XmlReader xr, string nodeName)
		{
			xr.ReadToNextSibling(nodeName);
			return xr;
		}
	}
	public enum CodgenParamScope
	{
		Local = 0,
		Global = 1
	}
	public class CodgenParam
	{
		public string ParamType;
		public string Key;
		public string Value;
		public CodgenParamScope Scope;
		public CodgenParam(string key, string value, string type)
		{
			Key = key;
			Value = value;
			ParamType = type;
			Scope = CodgenParamScope.Local;
		}
	}
	public class CodgenXml
	{
		public string Result;
		protected Assembly CallingAssembly;
		protected string BaseDir = AppDomain.CurrentDomain.BaseDirectory;

		protected List<CodgenParam> Params = new List<CodgenParam>();

		public string ParseXML(XmlReader xr)
		{
			string generator = "", method = "", rltstr;
			StringBuilder rlt = new StringBuilder();
			if (xr.NodeType == XmlNodeType.Element)
			{
				for (int i = 0; i < xr.AttributeCount; i++)
				{
					xr.MoveToAttribute(i);
					if (xr.Name.Equals("Generator"))
					{
						generator = xr.Value;
					}
					else if (xr.Name.Equals("Method"))
					{
						method = xr.Value;
					}
					else
					{
						CodgenParam cp = new CodgenParam(xr.Name, xr.Value, "template");
						Params.Add(cp);
					}
				}
			}
			while (xr.Read())
			{
				//Output(xr.Name + " - " + xr.NodeType);
				if (xr.NodeType == XmlNodeType.CDATA || xr.NodeType == XmlNodeType.Text)
				{
					string content = xr.Value;
					foreach (CodgenParam cp in Params)
					{
						if (cp.ParamType.Equals("replace") || cp.ParamType.Equals("globalreplace"))
						{
							content = content.Replace(">" + cp.Key + "<", cp.Value);
						}
					}
					rlt.Append(content);
				}
				else if (xr.NodeType == XmlNodeType.Element)
				{
					rlt.Append(ParseElement(xr));
				}
			}
			if (!string.IsNullOrEmpty(generator))
			{
				if (string.IsNullOrEmpty(method))
				{
					method = "Generate";
				}

				//Type typ = Type.GetType(generator);
				Type typ = CallingAssembly.GetType(generator);
				if (typ != null)
				{
					MethodInfo mi = typ.GetMethod(
					method, 
					System.Reflection.BindingFlags.Static
					| System.Reflection.BindingFlags.Public
					| System.Reflection.BindingFlags.NonPublic
					| System.Reflection.BindingFlags.InvokeMethod
					| System.Reflection.BindingFlags.GetProperty
					);
					if (mi != null)
					{
						try
						{
							rlt = new StringBuilder(mi.Invoke(null, new object[] { rlt.ToString() }) as string);
						}
						catch (Exception err)
						{
							throw err;
						}
					}
				}
			}
			rltstr = rlt.ToString();
			rltstr = PostRender(rltstr);
			RenderComplete(rltstr);
			return rltstr;
		}
		public string ReadFile(string filename)
		{
			string rlt = string.Empty, path = string.Empty;
			if (!string.IsNullOrEmpty(filename))
			{
				if (filename.IndexOf(':') < 0)
				{
					filename = BaseDir + filename;
				}
				int pos = filename.LastIndexOf('\\');
				if (pos > 0)
				{
					path = filename.Substring(0, pos + 1);
				}
				BaseDir = path;
				using (XmlReader xr = XmlReader.Create(filename))
				{
					xr.Read();
					xr.ReadToNextSibling("Template");
					rlt = ParseXML(xr);
				}
			}
			return rlt;
		}
		public bool WriteFile(string fullpath)
		{
			return WriteFile(fullpath, Result);
		}
		public bool WriteFile(string fullpath, string content)
		{
			FileStream fs = new FileStream(fullpath, FileMode.Create);
			StreamWriter sw = new StreamWriter(fs);
			sw.Write(content);
			sw.Close();
			return true;
		}
		protected void Init()
		{
		}
		protected string ParseElement(XmlReader xr)
		{
			string elName, rlt = "";
			elName = xr.Name.ToLower();
			if (elName.Equals("import"))
			{
				string url = XmlReaderHelper.ReadAttribute(xr, "Url");
				if (!string.IsNullOrEmpty(url) && url.IndexOf(':') < 0)
				{
					url = BaseDir + url;
				}
				CodgenXml cx = new CodgenXml(CallingAssembly);
				foreach (CodgenParam cp in Params)
				{
					if (cp.ParamType.Equals("globalreplace"))
					{
						CodgenParam ncp = new CodgenParam(cp.Key, cp.Value, cp.ParamType);
						cx.Params.Add(ncp);
					}
				}
				rlt = cx.ReadFile(url);
			}
			else if (elName.Equals("contentreplace"))
			{
				string key = XmlReaderHelper.ReadAttribute(xr, "Handler");
				string scope = XmlReaderHelper.ReadAttribute(xr, "Scope");
				string val = XmlReaderHelper.ReadContent(xr);
				if (string.IsNullOrEmpty(scope))
				{
					scope = "replace";
				}
				CodgenParam par = new CodgenParam(key, val, scope.ToLower());
				Params.Add(par);
				//xr.Read();
				//xr.Read();
			}
			else
			{
				XmlReaderHelper.EnumAttributes(xr, delegate(string k, string v, string n)
				{
					CodgenParam par;
					par = new CodgenParam(k, v, n);
					Params.Add(par);
				});
			}
			return rlt;
		}
		protected virtual void Render(string content)
		{
			Console.Write(content);
		}
		protected virtual string PostRender(string content)
		{
			foreach (CodgenParam cp in Params)
			{
				if (cp.ParamType.Equals("template"))
				{
					if (cp.Key.Equals("NoEmptyLine"))
					{
						//Console.WriteLine("******" + mc.Count);
						//content = content.Replace("\n\n", "");
						content = RemoveEmptyRows(content);
					}
				}
			}
			content = content.Replace("\r\n", "\n");
			content = RemoveEmptyRows(content);
			content = content.Replace("\n", "\r\n");
			Result = content;
			return content;
		}
		protected string RemoveEmptyRows(string content)
		{
			int matches = 1;
			while (matches > 0)
			{
				Regex r = new Regex("[\n][\t]*[\n]");
				MatchCollection mc = r.Matches(content);
				matches = mc.Count;
				foreach (Match m in mc)
				{
					content = content.Replace(m.Value, "\n");
				}
			}
			return content;
		}
		protected virtual void RenderComplete(string content)
		{
			foreach (CodgenParam cp in Params)
			{
				if (cp.ParamType.Equals("output"))
				{
					if (cp.Key.Equals("Path") && !string.IsNullOrEmpty(cp.Value))
					{
						WriteFile(cp.Value, content);
					}
				}
			}
		}
		public CodgenXml()
		{
			CallingAssembly = Assembly.GetCallingAssembly();
		}
		public CodgenXml(Assembly callingAssembly)
		{
			CallingAssembly = callingAssembly;
		}
	}
	public class Generator
	{
		public static string Run(string fullFileName)
		{
			string rlt;
			CodgenXml cx = new CodgenXml(Assembly.GetCallingAssembly());
			rlt = cx.ReadFile(fullFileName);
			return rlt;
		}
	}
}
