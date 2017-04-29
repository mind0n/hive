using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using System.IO;
using ULib.Exceptions;

namespace ULib.Configs
{
	public class AppConfig
	{
		private readonly static AppConfig instance = new AppConfig();
		public readonly static string BaseDir = AppDomain.CurrentDomain.BaseDirectory + "config\\";

		public static AppConfig Instance
		{
			get
			{
				return instance;
			}
		}

		public Dict<string, AppConfigItem> settings = new Dict<string, AppConfigItem>();
		public void Initialize(string key, Type type)
		{
			settings[key] = new AppConfigItem { Name = key, InstanceType = type };
		}
		public T Load<T>(string key, bool forceLoadFile = false) where T:class
		{
			AppConfigItem i = settings[key];
			if (i != null)
			{
				if (i.Instance != null && !forceLoadFile)
				{
					return i.Instance as T;
				}
				else if (File.Exists(i.ConfigFile))
				{
					string xml = File.ReadAllText(i.ConfigFile);
					T rlt = xml.FromXml<T>(i.InstanceType);
					i.Instance = rlt;
					settings[key] = i;
					return (T)i.Instance;
				}
				else
				{
					i.Instance = Activator.CreateInstance(i.InstanceType);
					settings[key] = i;
					Save(key);
					return i.Instance as T;
				}
			}
			return default(T);
		}
		public void Update(string key, object instance)
		{
			if (settings.ContainsKey(key))
			{
				AppConfigItem i = settings[key];
				if (i != null)
				{
					i.Instance = instance;
				}
			}
		}
		public void Save(string key = null)
		{
			try
			{
				if (!Directory.Exists(BaseDir))
				{
					Directory.CreateDirectory(BaseDir);
				}
				if (!string.IsNullOrEmpty(key))
				{
					SaveByKey(key);
				}
				else
				{
					foreach (string i in settings.Keys)
					{
						SaveByKey(i);
					}
				}
			}
			catch (Exception ex)
			{
				ExceptionHandler.Handle(ex);
			}
		}

		private void SaveByKey(string key)
		{
			if (!string.IsNullOrEmpty(key) && settings.ContainsKey(key))
			{
				AppConfigItem i = settings[key];
				if (i != null && i.Instance != null)
				{
					File.WriteAllText(i.ConfigFile, i.Instance.ToXml(i.InstanceType));
				}
			}
		}
	}
	public class AppConfigItem
	{
		public Type InstanceType;
		public object Instance;
		public string Name;
		public string ConfigFile
		{
			get
			{
				return AppConfig.BaseDir + Name + ".xml";
			}
		}
	}

}
