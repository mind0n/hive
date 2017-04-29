using Joy.Core.Configuration;
using Joy.Core.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Joy.Portal.Testing
{
	public partial class TestSettings : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			JSettings.Instance.Load();
			txt.Value = JSettings.Get<Setting>().Server;
		}
	}
	public class Setting
	{
		public string Server;
		public DatabaseCollection Databases;
	}
	public class DatabaseCollection
	{
		protected List<DatabaseItem> list = new List<DatabaseItem>();
		public void Add(string key, string value)
		{
			list.Add(new DatabaseItem { Name = key, Value = value });
		}
	}
	public class DatabaseItem
	{
		public string Name;
		public string Value;
	}
}