using Joy.Common.DataSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Joy.Common
{
	public class Configuration
	{
		public static string BaseDir
		{
			get
			{
				string baseDir = AppDomain.CurrentDomain.BaseDirectory;
				if (baseDir[baseDir.Length - 1] != '\\')
				{
					baseDir += '\\';
				}
				return baseDir;
			}
		}
		public static string UiBaseUrl
		{
			get
			{
				XReader xr = UIConfig;
				if (xr != null)
				{
					return xr.Reset()["configuration"]["$BaseUrl"].Value;
				}
				else
				{
					return null;
				}
			}
		}
		public static XReader DbConfig
		{
			get
			{
				return GetConfigFileReader("DB");
			}
		}
		public static XReader UIConfig
		{
			get
			{
				return GetConfigFileReader("UI");
			}
		}
		public static XReader GetConfigFileReader(string name)
		{
			string fullConfigFilename = BaseDir + name + ".Config";
			XReader xr = new XReader(fullConfigFilename);
			return xr;
		}
	}
	public class FsConfig : Configuration { }

}
