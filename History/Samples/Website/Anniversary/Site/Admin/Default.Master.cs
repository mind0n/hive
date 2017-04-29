using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Entities;
using Site.Admin.AdminDAL;
using Joy.Server.Core;

namespace Site.Admin
{
	public partial class Default : System.Web.UI.MasterPage
	{
		public string RootUrl
		{
			get
			{
				return JoyConfig.Instance.RootUrl;
			}
		}
		void logoutbtn_Click(object sender, EventArgs e)
		{
			DalHandler.Logout();
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			string curtName = Session["UserName"] as string;
			logoutbtn.Click += new EventHandler(logoutbtn_Click);
			if (curtName != null)
			{
				curtuser.InnerHtml = curtName;
			}
		}
	}
}