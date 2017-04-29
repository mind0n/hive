using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using ULib.Native;
using Utilities.Settings;

namespace Utilities.Components.DBPartitioning
{
	public class DBPartSettings : Dict<string, DBPartSetting>, ISettings
	{
		public static string BaseDir { get { return AppDomain.CurrentDomain.BaseDirectory; } }
		public string Sql_Directory = BaseDir + "sql\\";
		public string Cache_Directory = BaseDir + "cache\\";
		public string Server = ".\\premierone";
		public bool Is_Trusted_Connection = true;
		public string Username = "sa";
		public string Password = "123456";
		public SqlFileSettings SqlSettings = new SqlFileSettings();

		public T GetValue<T>(string key) where T : Setting
		{
			return this[key] as T;
		}

		public DBPartSetting GetValue(string key)
		{
			return this[key] as DBPartSetting;
		}

		public void SetValue(string key, Setting value)
		{
			this[key] = (DBPartSetting)value;
		}
	}
	public class DBPartSetting : Setting
	{
	}
	public class SqlFileSetting : DBPartSetting
	{
		public string Fullname;
		public string Filename
		{
			get
			{
				return Native.GetFileNameFromPath(Fullname);
			}
		}
		public int Sequence;
	}
	public class SqlFileSettings : List<SqlFileSetting>
	{
		public void AddItem(string sqlfile, int sequence = -1)
		{
			SqlFileSetting item = new SqlFileSetting { Fullname = sqlfile };
			if (sequence < 0)
			{
				Add(item);
			}
			else
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this[i] != null && this[i].Sequence >= sequence)
					{
						this.Insert(i, item);
						return;
					}
				}
				Add(item);
			}
		}
	}
}
