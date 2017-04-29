using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fs.Web;

namespace FSUI.Lib.Server
{
	public class TracerKeys
	{
		public static readonly string PreviousUrl = "_tk_previous_url_";
	}
	public partial class TracerPage : System.Web.UI.Page
	{
		public static string PreviousUrl
		{
			get
			{
				return ServerHelper.GetSession(TracerKeys.PreviousUrl);
			}
			set
			{
				ServerHelper.SetSession(TracerKeys.PreviousUrl, value);
			}
		}

		protected override void OnInit(EventArgs e)
		{
			PreviousUrl = Request.Url.ToString();
			base.OnInit(e);
		}
	}
}
