using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Joy.Core;
using System.IO;

namespace Joy.Server.Core
{
	public class JoyConfig
	{
		public string RootUrl;
		public static JoyConfig Instance
		{
			get
			{
				if (instance == null)
				{
					instance = CreateInstance();
				}
				return instance;
			}
		}
		protected static string baseDir = AppDomain.CurrentDomain.BaseDirectory;
		protected static string filename = "Joy.Config";
		protected static string fullpath
		{
			get
			{
				return string.Concat(baseDir, filename);
			}
		}
		protected static JoyConfig instance;

		public static JoyConfig CreateInstance(string rooturl = null)
		{
			if (File.Exists(fullpath))
			{
				string xml = File.ReadAllText(fullpath);
				JoyConfig c = xml.FromXml<JoyConfig>();
				return c;
			}
			if (string.IsNullOrEmpty(rooturl))
			{
				rooturl = "/";
			}
			return new JoyConfig { RootUrl = rooturl };
		}

		public static string UrlFromRoot(string url)
		{
			return string.Concat(Instance.RootUrl, url);
		}
	}
}
