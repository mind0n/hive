using System;
using System.Collections.Generic;

using System.Text;
using Joy.Server.Web.Services;
using System.Web;
using System.IO;

namespace Joy.Server.Authentication
{

	public class PassportBase : System.Web.UI.Page
	{
		protected static new HttpResponse Response
		{
			get
			{
				return HttpContext.Current.Response;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			string rlt = HttpMethodInvoker.Invoke(Request, this);
			Response.Write(rlt);
		}
		protected static HashManager _hash;
		public static string Encode(string uname, string upwd, string ticket)
		{
			return uname + upwd + ticket;
		}
		public static void UpdateHash(HttpCookie hcUserName, string upwd)
		{
			string uname = "noname", hash = AuthManager.LastHash;
			if (hcUserName != null)
			{
				uname = hcUserName.Value;
			}
			SetLastHash(uname, hash);
			//AuthManager.CookiesResponse.Remove("Remember");
			//AuthManager.CookiesResponse.Remove("RememberUserName");
			//AuthManager.CookiesRequest.Remove("Remember");
			//AuthManager.CookiesRequest.Remove("RememberUserName");

			//AuthManager.CookiesResponse.Add(new HttpCookie("Remember", Encode(uname, upwd, hash)));
			//AuthManager.CookiesResponse.Add(new HttpCookie("RememberUserName", uname));
			//AuthManager.Response.SetCookie(new HttpCookie("Remember", ""));
			//AuthManager.Response.SetCookie((new HttpCookie("RememberUserName", "")));

			AuthManager.Response.SetCookie(new HttpCookie("Remember", Encode(uname, upwd, hash)));
			AuthManager.Response.SetCookie((new HttpCookie("RememberUserName", uname)));
		}
		public static bool RememberedAuth(HttpCookie remembername, string upwd, HttpCookie rememberhash)
		{
			string lh = "";
			if (rememberhash == null || remembername == null)
			{
				return false;
			}
			string uname = remembername.Value;
			string hash = rememberhash.Value;
			lh = GetLastHash(uname);
			if (Encode(uname, upwd, lh) == hash)
			{
				return true;
			}
			return false;
		}
		public static bool Authenticate(string uname, string upwd)
		{
			if (AuthManager.RequestCookies.Get("Cooked") == null)
			{
				return false;
			}
			string lh = AuthManager.LastHash;
			string ck = AuthManager.RequestCookies["Cooked"].Value;
			SetLastHash(uname, lh);
			if (Encode(uname, upwd, lh) == ck)
			{
				return true;
			}
			return false;
		}
		public static void Deauthenticate(HttpResponse Response)
		{
			//Response.Cookies.Remove("Remember");
			//Response.Cookies.Remove("RememberUserName");
			//Response.Cookies.Remove("Cooked");

			//Response.Cookies.Set(new HttpCookie("Remember", ""));
			//Response.Cookies.Set(new HttpCookie("RememberUserName", ""));
			//Response.Cookies.Set(new HttpCookie("Cooked", ""));
			Response.SetCookie(new HttpCookie("Remember", ""));
			Response.SetCookie(new HttpCookie("RememberUserName", ""));
			Response.SetCookie(new HttpCookie("Cooked", ""));
			ClearLastHash();
		}
		protected static string GetLastHash(string uname)
		{
			if (_hash != null)
			{
				return _hash.GetLastTicket(uname);
			}
			return null;
		}
		protected static void SetLastHash(string uname, string hash)
		{
			if (_hash != null)
			{
				_hash.UpdateTicket(uname, hash);
			}
		}
		protected static void ClearLastHash()
		{
			if (_hash != null)
			{
				_hash.ClearLastHash();
			}
		}
		[ScriptMethod(ParamType = ScriptMethodParamType.Explicit)]
		public virtual string Authenticate()
		{
			return "{error: 'Authentication method not implemented.'}";
		}
	}

}
