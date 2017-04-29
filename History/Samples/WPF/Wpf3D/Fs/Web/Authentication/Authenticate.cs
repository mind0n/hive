using System;
using System.Collections.Generic;

using System.Text;
using System.Web;
using System.Web.SessionState;
using System.IO;
using Fs.Data;
using Fs.Web.Services;

namespace Fs.Web.Authentication
{
	public class UserContext
	{
		public string UserName;
		public UserContext(string uname)
		{
			init(uname);
		}
		protected void init(string uname)
		{
			UserName = uname;
		}
	}
	public class AuthenticatorKeys
	{
		public static readonly string Ticket = "_ak_ticket_";
		public static readonly string AuthCode = "_ak_auth_code_";
		public static readonly string RememberedHash = "_ak_remember_hash_";
		public static readonly string RememberedName = "_ak_remember_name_";
		public static readonly string IsLogin = "_ak_is_login_";
		public static readonly string MemberName = "_ak_member_name_";
		public static readonly string MemberContext = "_ak_member_context_";
		public static readonly string ClientHash = "_ak_client_hash_";
	}
	public class Authenticator
	{
		public static string AuthCode
		{
			get
			{
				return GetSession(AuthenticatorKeys.AuthCode);
			}
			set
			{
				SetSession(AuthenticatorKeys.AuthCode, value);
			}
		}
		public static string ClientHash
		{
			get
			{
				return GetCookieValue(AuthenticatorKeys.ClientHash);
			}
		}
		public static string RememberedName
		{
			get
			{
				return GetCookieValue(AuthenticatorKeys.RememberedName);
			}
		}
		public static string RememberedHash
		{
			get
			{
				return GetCookieValue(AuthenticatorKeys.RememberedHash);
			}
		}
		public static string Ticket
		{
			get
			{
				return GetSession(AuthenticatorKeys.Ticket);
			}
		}
		public static string MemberName
		{
			get
			{
				return GetSession(AuthenticatorKeys.MemberName);
			}
		}
		public static bool IsLogin
		{
			get
			{
				return GetSession<bool>(AuthenticatorKeys.IsLogin);
			}
		}
		public static HttpCookieCollection CookiesRequest
		{
			get
			{
				return HttpContext.Current.Request.Cookies;
			}
		}
		public static HttpCookieCollection CookiesResponse
		{
			get
			{
				return HttpContext.Current.Response.Cookies;
			}
		}
		public static HttpCookieCollection Cookies
		{
			get
			{
				return HttpContext.Current.Response.Cookies;
			}
		}
		public static HttpResponse Response
		{
			get
			{
				return HttpContext.Current.Response;
			}
		}
		public static HttpRequest Request
		{
			get
			{
				return HttpContext.Current.Request;
			}
		}
		public static HttpSessionState Session
		{
			get
			{
				return HttpContext.Current.Session;
			}
		}
		public static void Login(string uname)
		{
			UserContext uc = new UserContext(uname);
			Login(uc);
		}
		public static void Login(UserContext context)
		{
			SetSession(AuthenticatorKeys.MemberName, context.UserName);
			SetSession(AuthenticatorKeys.MemberContext, context);
			SetSession(AuthenticatorKeys.IsLogin, true);
		}
		public static void Logout()
		{
			SetSession(AuthenticatorKeys.MemberName, "");
			SetSession(AuthenticatorKeys.MemberContext, null);
			SetSession(AuthenticatorKeys.IsLogin, false);
		}
		public static string MakeAuthCode()
		{
			string rlt = Guid.NewGuid().ToString().Substring(0, 4);
			SetSession(AuthenticatorKeys.Ticket, rlt);
			return rlt;
		}
		public static string MakeTicket()
		{
			string guid = Guid.NewGuid().ToString();
			HttpCookie Cookie = new HttpCookie(AuthenticatorKeys.Ticket, guid);
			Cookie.Expires = DateTime.Now.AddDays(1);
			Response.SetCookie(Cookie);
			SetSession(AuthenticatorKeys.Ticket, guid);
			return guid;
		}
		protected static T GetSession<T>(string name)
		{
			object rlt = Session[name];
			if (rlt != null)
			{
				return (T)rlt;
			}
			else
			{
				return default(T);
			}
		}
		protected static string GetSession(string name)
		{
			return GetSession<string>(name);
		}
		protected static string GetCookieValue(string name)
		{
			HttpCookie hc = Request.Cookies.Get(name);
			if (hc != null)
			{
				return hc.Value;
			}
			return null;
		}
		protected static void SetSession(string key, object value)
		{
			Session[key] = value;
		}
		protected static void SetCookie(string key, string value)
		{
			SetCookie(key, value, null);
		}
		protected static void SetCookie(string key, string value, string expireDate)
		{
			HttpCookie hc = new HttpCookie(key, value);
			if (expireDate != null)
			{
				hc.Expires = DateTime.Parse(expireDate);
			}
			Response.SetCookie(hc);
		}
	}
	public class BasicPassport
	{
		public BasicPassport()
		{
			response = HttpContext.Current.Response;
		}
		HttpResponse response;
		protected static HashManager _ticketManager;
		public virtual void Remember(string uname, string hash)
		{
			response.SetCookie(new HttpCookie(AuthenticatorKeys.RememberedName, uname));
			response.SetCookie(new HttpCookie(AuthenticatorKeys.RememberedHash, hash));
		}
		public virtual void Logout()
		{
			Forget();
			Authenticator.Logout();
		}
		public virtual void Forget()
		{
			response.Cookies.Remove(AuthenticatorKeys.RememberedHash);
			response.Cookies.Remove(AuthenticatorKeys.RememberedName);
		}
		public virtual bool RememberAuthenticate()
		{
			string rmhash;
			string uname = Authenticator.RememberedName;
			string clientHash = Authenticator.RememberedHash;
			if (_ticketManager == null || string.IsNullOrEmpty(clientHash) || string.IsNullOrEmpty(uname))
			{
				return false;
			}
			rmhash = _ticketManager.GetLastHash(uname);
			if (!string.IsNullOrEmpty(rmhash) && rmhash.Equals(clientHash))
			{
				return true;
			}
			return false;
			//return FormAuthenticate(uname, clientHash, _ticketManager.GetLastTicket(uname));
		}
		public virtual bool FormAuthenticate(string uname, string clientHash, string lastTicket)
		{
			string secuhash, upwd = GetPwdHashByMemberName(uname);
			if (string.IsNullOrEmpty(upwd) || string.IsNullOrEmpty(clientHash) || string.IsNullOrEmpty(lastTicket))
			{
				return false;
			}
			secuhash = GetSecurityHash(uname, upwd, lastTicket);
			if (secuhash.Equals(clientHash))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		protected virtual string GetSecurityHash(string uname, string upwd, string ticket) { throw new NotImplementedException(); }
		protected virtual string GetPwdHashByMemberName(string memberName) { throw new NotImplementedException(); }
	}
	public class SimplePassport : BasicPassport
	{
		static SimplePassport()
		{
			_ticketManager = new FileHashManager();
		}
		public override void Remember(string uname, string hash)
		{
			base.Remember(uname, hash);
			_ticketManager.UpdateHash(uname, hash);
		}
		public override void Logout()
		{
			_ticketManager.ClearLastHash();
			base.Logout();
		}
		protected override string GetSecurityHash(string uname, string upwd, string ticket)
		{
			return uname + '_' + upwd + '_' + ticket;
		}
		protected override string GetPwdHashByMemberName(string memberName)
		{
			if (memberName.Equals("admin"))
			{
				return "--test--";
			}
			else
			{
				return null;
			}
		}
	}
	public class Passport : SimplePassport
	{
		public static new void Remember(string uname, string hash)
		{
			BasicPassport bp = new SimplePassport();
			bp.Remember(uname, hash);
		}
		public static new void Logout()
		{
			BasicPassport bp = new SimplePassport();
			bp.Logout();
		}
		public static new void Forget()
		{
			BasicPassport bp = new SimplePassport();
			bp.Forget();
		}
		public static new bool FormAuthenticate(string uname, string clientHash, string lastTicket)
		{
			BasicPassport bp = new SimplePassport();
			return bp.FormAuthenticate(uname, clientHash, lastTicket);
		}
		public static new bool RememberAuthenticate()
		{
			BasicPassport bp = new SimplePassport();
			return bp.RememberAuthenticate();
		}
	}
	//public class PassportPage : System.Web.UI.Page
	//{
	//    protected TicketManager _ticketManager;
	//    public static bool Authenticate(string uname, string clientHash)
	//    {
	//        PassportPage pp = new PassportPage();
	//        return (pp.RememberAuthenticate(uname, clientHash) || pp.FormAuthenticate(uname, clientHash, Authenticator.Ticket));
	//    }
	//    public virtual bool RememberAuthenticate(string uname, string clientHash)
	//    {
	//        if (_ticketManager != null)
	//        {
	//            return FormAuthenticate(uname, clientHash, _ticketManager.GetLastTicket(uname));
	//        }
	//        return false;
	//    }
	//    public virtual bool FormAuthenticate(string uname, string clientHash, string lastTicket)
	//    {
	//        string secuhash, upwd = GetPwdHashByMemberName(uname);
	//        if (string.IsNullOrEmpty(upwd) || string.IsNullOrEmpty(clientHash) || string.IsNullOrEmpty(lastTicket))
	//        {
	//            return false;
	//        }
	//        secuhash = GetSecurityHash(uname, upwd, lastTicket);
	//        if (secuhash.Equals(clientHash))
	//        {
	//            return true;
	//        }
	//        else
	//        {
	//            return false;
	//        }
	//    }
	//    protected virtual string GetSecurityHash(string uname, string upwd, string ticket) { throw new NotImplementedException(); }
	//    protected virtual string GetPwdHashByMemberName(string memberName) { throw new NotImplementedException(); }
	//    protected void Page_Load(object sender, EventArgs e)
	//    {
	//        string rlt = HttpMethodInvoker.Invoke(Request, this);
	//        Response.Write(rlt);
	//    }
	//}
	//public class SimplePassportPage : PassportPage
	//{
	//    public SimplePassportPage()
	//    {
	//        _ticketManager = new FileHashManager(AppDomain.CurrentDomain.BaseDirectory);
	//    }
	//    protected override string GetSecurityHash(string uname, string upwd, string ticket)
	//    {
	//        return uname + '_' + upwd + '_' + ticket;
	//    }
	//    protected override string GetPwdHashByMemberName(string memberName)
	//    {
	//        if (memberName.Equals("admin"))
	//        {
	//            return ">>test<<";
	//        }
	//        else
	//        {
	//            return null;
	//        }
	//    }
	//}
	public class HashManager
	{
		public virtual string GetLastHash(string uname) { return null; }
		public virtual void UpdateHash(string uname, string hash) { }
		public virtual void UpdateHash(string uname) { }
		public virtual void ClearLastHash() { }
	}
	public class FileHashManager : HashManager
	{
		protected string BaseDir;
		protected string PostFix = ".hash";
		public FileHashManager()
		{
			Init(null);
		}
		public FileHashManager(string baseDir)
		{
			Init(baseDir);
		}
		protected void Init(string baseDir)
		{
			if (string.IsNullOrEmpty(baseDir) || !Directory.Exists(baseDir))
			{
				BaseDir = AppDomain.CurrentDomain.BaseDirectory + "UserHashes\\";
				if (!Directory.Exists(BaseDir))
				{
					Directory.CreateDirectory(BaseDir);
				}
			}
			else
			{
				BaseDir = baseDir;
			}
		}
		public override string GetLastHash(string uname)
		{
			string fname = BaseDir + uname + PostFix;
			if (File.Exists(fname))
			{
				return File.ReadAllText(fname);
			}
			return null;
		}
		public override void UpdateHash(string uname, string hash)
		{
			string fname = BaseDir + uname + PostFix;
			File.WriteAllText(fname, hash);
		}
		public override void ClearLastHash()
		{
			string fname = BaseDir + Authenticator.MemberName + PostFix;
			File.Delete(fname);
		}
	}

}
