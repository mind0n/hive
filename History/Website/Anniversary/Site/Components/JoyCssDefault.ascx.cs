﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Joy.Server.Core;

namespace Site.Components
{
	public partial class JoyCssDefault : System.Web.UI.UserControl
	{
		public string RootUrl
		{
			get
			{
				return JoyConfig.Instance.RootUrl;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}