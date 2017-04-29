using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities.Settings;
using ULib.DataSchema;

namespace Utilities.Components.Deployment
{
	public class DeploySettings : Dict<string, DeploySetting>, ISettings
	{
		public T GetValue<T>(string key) where T : Setting
		{
			return this[key] as T;
		}

		public DeploySetting GetValue(string key)
		{
			return this[key] as DeploySetting;
		}

		public void SetValue(string key, Setting value)
		{
			this[key] = (DeploySetting)value;
		}
	}
	public class DeploySetting : Setting
    {
        public string Script_Base = string.Concat(AppDomain.CurrentDomain.BaseDirectory, @"External\Scripts\");
        public string Msi_Base_Path = @"\\192.168.17.8\DailyMainBuild\";
        public string Server_Branch = "P1-main";
        public string Client_Type = "CADClient";
        public string Ip_Postfix = "17.4";
        public string Main_Version = "3.3.0.";
    }
}
