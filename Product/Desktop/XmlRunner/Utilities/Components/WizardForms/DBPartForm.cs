using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ULib.Forms;
using ULib.Encoders;
using Utilities.Settings;

namespace Utilities.Components.DBPartitioning
{
	public partial class DBPartForm : DockForm
	{
		protected DBPartSettings settings = new DBPartSettings();
		protected SqlFileSettings fileSettings = new SqlFileSettings();
		public DBPartForm()
		{
			InitializeComponent();
			Load += new EventHandler(DBPartForm_Load);
		}

		private void DBPartForm_Load(object sender, EventArgs e)
		{
			string file = AppDomain.CurrentDomain.BaseDirectory + "test.xml";
			fileSettings = new SqlFileSettings();
			settings.SqlSettings.Add(new SqlFileSetting { Fullname = FullSqlName("dep1.sql"), Sequence = 1 });
			settings.SqlSettings.Add(new SqlFileSetting { Fullname = FullSqlName("dep2.sql"), Sequence = 2 });
			settings.SqlSettings.Add(new SqlFileSetting { Fullname = FullSqlName("dep3.sql"), Sequence = 3 });
			settings.SqlSettings.Add(new SqlFileSetting { Fullname = FullSqlName("dep4.sql"), Sequence = 4 });
			//settings
			//fileSettings["deploy1.sql"] = new SqlFileSetting { Fullname = FullSqlName("dep1.sql") };
			
			string content = settings.Obj2Xml(file);
			box.Text = content;
		}
		public string FullSqlName(string name)
		{
			return settings.Sql_Directory + name;
		}
		public string FullCacheName(string name)
		{
			return settings.Cache_Directory + name;
		}
	}
}
