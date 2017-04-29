using System;
using Joy.Data;
namespace Joy.WebApp.WebApp.Tests
{
	public partial class TestFileDb : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			using (FileDB db = new FileDB("c:\\data.fdb", System.IO.FileAccess.ReadWrite))
			{
				db.Save("success.txt", Guid.NewGuid().ToString());
				EntryInfo[] list = db.ListFiles();
				for (int i = 0; i < list.Length; i++)
				{
					EntryInfo info = list[i];
					Response.Write(db.Load(info));
					Response.Write("<br />");
				}
			}
			
		}
	}
}