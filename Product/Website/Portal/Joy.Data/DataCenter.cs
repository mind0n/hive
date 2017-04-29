using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Joy.Core;
using Joy.Core.Reflect;

namespace Joy.Data
{
	public class DataCenter
	{
		private static DataCenter instance = Load();

		public static DataCenter Instance
		{
			get
			{
				if (instance == null)
				{
					instance = Load();
				}
				return DataCenter.instance;
			}
		}

		public List<DbConfig> DbConfigs = new List<DbConfig>();
		private Dictionary<string, Db> databases = new Dictionary<string, Db>();

		public static T Use<T>(string name) where T:Db,new()
		{
			try
			{
				if (DataCenter.Instance == null)
				{
					return null;
				}
				if (!DataCenter.Instance.databases.ContainsKey(name) || DataCenter.Instance.databases[name] == null)
				{
					bool found = false;
					foreach (DbConfig i in DataCenter.Instance.DbConfigs)
					{
						if (string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase))
						{
							Db db = i.TypeName.CreateInstance() as Db;
							if (db != null)
							{
								db.GetReady(i);
								DataCenter.Instance.databases[name] = db;
								found = true;
							}
							break;
						}
					}
					if (!found)
					{
						return default(T);
					}
				}
				return DataCenter.Instance.databases[name] as T;
			}
			catch
			{
				return default(T);
			}
		}

		private static DataCenter Load()
		{
			try
			{
				string file = AppDomain.CurrentDomain.BaseDirectory + "db.config";
				if (File.Exists(file))
				{
					string content = File.ReadAllText(file);
					if (!string.IsNullOrEmpty(content))
					{
						DataCenter rlt = content.FromJson<DataCenter>();
						return rlt;
					}
				}
				return null;
			}
			catch
			{
				return null;
			}
		}
	}
	public class DbConfig
	{
		public string Name;
		public string Url;
		public string Description;
		public string Connector;
		public string Encoder;
		public string TypeName;
		public string Assembly;
	}

}
