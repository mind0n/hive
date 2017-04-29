using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wcf.Interface.DataSchema;
using System.IO;
using Platform.Core;
using System.Xml.Linq;

using XElements = System.Collections.Generic.IEnumerable<System.Xml.Linq.XElement>;
namespace Wcf.Service.Executors
{
	class FavUrlExecutor : ActionExecutor
	{
		string file = FileSystem.MapPath("favurls", "urls.txt");
		public void AddUrl(string url)
		{
			XDocument doc = GetDocument();
			if (doc != null)
			{
				XElement el = new XElement("url");
				el.Value = url;
				foreach (XElement e in doc.Element("urls").Elements("url"))
				{
					if (e != null && string.Equals(url, e.Value, StringComparison.Ordinal))
					{
						return;
					}
				}
				doc.Element("urls").Add(el);
				doc.Save(file);
			}
		}

		public string GetUrls()
		{
			string urls = File.ReadAllText(file);
			return urls;
		}

		private XDocument GetDocument()
		{
			try
			{
				XDocument doc = XDocument.Parse(File.ReadAllText(file));
				return doc;
			}
			catch
			{
				try
				{
					XDocument doc = XDocument.Parse(
						File.ReadAllText(FileSystem.MapPath("Templates", "FavUrls.xml", false))
					);
					doc.Save(file);
					return doc;
				}
				catch
				{
					return null;
				}
			}
		}
	}
}
