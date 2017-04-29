using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using Joy.Core;
using Joy.Server.Data;

namespace Joy.Server
{
	public class WebPage : Page
	{
		const string STR_Time = "time";
		const string KeyClientCache = "ccx";
		const string KeyClientCacheJson = "ccj";
		const string STR_Dashs = "-";
		const string KeyUserInstanceId = "userid";
		const string HTTP_FORWARDED_FOR = "HTTP_X_FORWARDED_FOR";
		const string UnderLine = "_";


		protected string PageId
		{
			get
			{
				return this.GetType().Name;
			}
		}

		protected bool isEnableJoy = true;
		protected Storage storage;
		protected List<string> clientScripts = new List<string>();

		protected override void OnInit(EventArgs e)
		{
			Session[STR_Time] = DateTime.UtcNow;
			storage = Storage.GetInstance<Storage>(Session.SessionID);
			base.OnInit(e);
		}
		public string NewId(string uid = null)
		{
			if (uid == null)
			{
				uid = Guid.NewGuid().ToString();
			}
			return string.Concat(uid.Replace(STR_Dashs, string.Empty), UnderLine, PageId);
		}
		protected string Cookie(string key, string value = null)
		{
			if (value == null)
			{
				HttpCookie c = Request.Cookies[key];
				if (c != null)
				{
					return c.Value;
				}
			}
			else
			{
				HttpCookie rlt = new HttpCookie(key, value);
				Response.SetCookie(rlt);
			}
			return null;
		}
		protected override void OnLoadComplete(EventArgs e)
		{
			const string template = "<script>\r\n{0}\r\n</script>";
			const string joyTemplate = "<script>\r\nJoy.onready(function(){{\r\n {0} \r\n}});\r\n</script>";
			base.OnLoadComplete(e);
			ClientScript.RegisterClientScriptBlock(this.GetType(), "PageController", "<script>var page = {};</script>");
			ClientScript.RegisterStartupScript(this.GetType(), "PageStartup", "<script>isPageReady=true;</script>");
			if (clientScripts.Count > 0)
			{
				if (isEnableJoy)
				{
					ClientScript.RegisterClientScriptBlock(this.GetType(), "ClientScripts", string.Format(joyTemplate, string.Join(";", clientScripts.ToArray())));
				}
				else
				{
					ClientScript.RegisterClientScriptBlock(this.GetType(), "ClientScripts", string.Format(template, string.Join(";", clientScripts.ToArray())));
				}
			}
		}
		public static void RegistScript(WebPage page, string script, string format = null)
		{
			if (!string.IsNullOrEmpty(format))
			{
				script = string.Format(format, script);
			}
			if (!string.IsNullOrEmpty(script))
			{
				page.clientScripts.Add(script);
			}
		}
		public static void RegistVariable(WebPage page, string name, string content)
		{
			if (content.EndsWith(";"))
			{
				content = content.Left(-1);
			}
			RegistScript(page, string.Concat("page.", name, " = ", content));
		}
	}
}
