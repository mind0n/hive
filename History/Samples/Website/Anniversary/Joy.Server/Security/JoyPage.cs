using System;
using System.Web;
using System.Web.UI;
using System.Diagnostics;
using Joy.Core;

namespace Joy.Server
{
	public class JoyPage : Page
	{
		const string STR_Time = "time";
		const string KeyClientCache = "ccx";
		const string KeyClientCacheJson = "ccj";
		const string STR_Dashs = "-";
		const string KeyUserInstanceId = "userid";
		const string HTTP_FORWARDED_FOR = "HTTP_X_FORWARDED_FOR";
		const string UnderLine = "_";

		public delegate void AuthenticateHandler();
		public string NewId(string uid = null)
		{
			if (uid == null)
			{
				uid = Guid.NewGuid().ToString();
			}
			return string.Concat(uid.Replace(STR_Dashs, string.Empty), UnderLine, PageId);
		}

		protected ValidationMode vmode;
		protected string PageId;
		protected ClientCredential clientCredential;
		protected Credential credential;
		protected event AuthenticateHandler OnAuthenticate;
		protected string password;
		protected bool IsAthenticated;

		public JoyPage()
		{
			vmode = ValidationMode.None;
			password = string.Empty;
			IsAthenticated = false;
		}
		public void GeneratePassword(bool forceNewPassword = true)
		{
			if (forceNewPassword || string.IsNullOrEmpty(credential.ServerPassword))
			{
				clientCredential.ClientPassword = credential.ServerPassword;
				credential.GeneratePassword(vmode >= ValidationMode.Standard);
			}
		}
		protected override void OnInit(EventArgs e)
		{
			if (string.IsNullOrEmpty(PageId))
			{
				PageId = this.GetType().Name;
			}

			InitializeClientCredential();

			ValidateRequest();

			base.OnInit(e);
		}
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
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
		private void ValidateRequest()
		{
			credential = Credential.CreateInstance(NewId(clientCredential.Uid), Cache);
			if (vmode != ValidationMode.None)
			{
				if (!credential.Validate(clientCredential))
				{
					credential = Credential.CreateInstance(NewId(), Cache);
				}
				if (OnAuthenticate != null)
				{
					OnAuthenticate();
				}
				GeneratePassword();
			}
			else
			{
				credential.Validate(clientCredential, false);
				GeneratePassword(false);
			}
			credential.GeneratePid(clientCredential, vmode != ValidationMode.None);
			clientCredential.ClientPassword = credential.ServerPassword;
			clientCredential.Page = PageId;
			clientCredential.Uid = credential.ClientUid;
			Cookie(KeyClientCacheJson, HttpUtility.UrlEncode(clientCredential.ToJson()));
			Cookie(KeyClientCache, HttpUtility.UrlEncode(clientCredential.ToXml()));
		}
		private void InitializeClientCredential()
		{
			Session[STR_Time] = DateTime.UtcNow;
			string xml = HttpUtility.UrlDecode(Cookie(KeyClientCache));
			if (!string.IsNullOrEmpty(xml))
			{
				clientCredential = xml.FromXml<ClientCredential>();
			}
			else
			{
				clientCredential = new ClientCredential();
			}
		}
	}
	public enum ValidationMode : int
	{
		Restrict = 3,
		Standard = 2,
		Basic = 1,
		None = 0
	}
}