using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fs.Web.Authentication;
using Fs.Xml;
using Fs;
using FSUI.Lib.Server;
using System.Threading;

namespace FSUI.Lib.Server.Ajax
{
	public class LoginModuleParam
	{
		public string PreviousUrl;
		public string SignoutRedirect;
		public string SigninProcessor;
		public string SigninFormUrl;
		public string SuccessRedirect;
		public string AuthCodeUrl;
		public LoginModuleParam(string authCodeUrl, string signinProcessor, string successRedirect, string signoutRedirect, string signinFormUrl)
		{
			AuthCodeUrl = authCodeUrl;
			SignoutRedirect = signoutRedirect;
			SigninProcessor = signinProcessor;
			SigninFormUrl = signinFormUrl;
			SuccessRedirect = successRedirect;
		}
		public void Redirect(string url)
		{
			HttpResponse response = HttpContext.Current.Response;
			string prevUrl = PreviousUrl;
			if (string.IsNullOrEmpty(url) || url.Equals("*"))
			{
				if (!string.IsNullOrEmpty(prevUrl))
				{
					response.Redirect(prevUrl);
				}
			}
			else
			{
				response.Redirect(url);
			}
		}
	}
	public static class LoginModuleParamNames
	{
		public static readonly string Action = "lmact";
		public static readonly string AuthCodeUrl = "acu";
		public static readonly string PreviousUrl = "url";
		public static readonly string SuccessRedirect = "sr";
		public static readonly string SignoutRedirect = "or";
		public static readonly string SigninProcessor = "sp";
		public static readonly string SigninFormUrl = "fu";
		public static readonly string Name = "name";
	}
	public partial class LoginModule : System.Web.UI.UserControl, IConfigurable
	{
		public static string DefaultName;
		public string Name;
		public string Act;
		public string PreviousUrl;
		protected static Dictionary<string, LoginModuleParam> List;
		protected LoginModuleParam CurtParam;
		public static string SignoutRedirect(string name)
		{
			if (List.ContainsKey(name))
			{
				return List[name].SignoutRedirect;
			}
			return null;
		}
		static LoginModule()
		{
			ReadConfigFile();
		}
		protected void InitCurtParam()
		{
			Act = Request.QueryString[LoginModuleParamNames.Action];
			Name = Request.QueryString[LoginModuleParamNames.Name];
			PreviousUrl = Request.QueryString[LoginModuleParamNames.PreviousUrl];
			if (string.IsNullOrEmpty(PreviousUrl))
			{
				PreviousUrl = TracerPage.PreviousUrl; 
			}
			if (!string.IsNullOrEmpty(Name))
			{
				if (List.ContainsKey(Name))
				{
					CurtParam = List[Name];
				}
				else
				{
					CurtParam = List[DefaultName];
				}
			}
			else
			{
				string acu = Request.QueryString[LoginModuleParamNames.AuthCodeUrl];
				string sp = Request.QueryString[LoginModuleParamNames.SigninProcessor];
				string sr = Request.QueryString[LoginModuleParamNames.SuccessRedirect];
				string or = Request.QueryString[LoginModuleParamNames.SignoutRedirect];
				string fu = Request.QueryString[LoginModuleParamNames.SigninFormUrl];
				CurtParam = new LoginModuleParam(acu, sp, sr, or, fu);

			}
			CurtParam.PreviousUrl = PreviousUrl;
		}
		protected static void ReadConfigFile()
		{
			string name, authPath, proxyPage;//, authCodePage;
			XReader xr = Fmk.UIConfig;
			xr = xr["configuration"]["authentication"];
			DefaultName = xr["$DefaultItem"].Value;
			if (List != null)
			{
				List.Clear();
			}
			else
			{
				List = new Dictionary<string, LoginModuleParam>();
			}
			foreach (XReader child in xr.Children)
			{
				authPath = Fmk.UiBaseUrl + child["$RelativeUrl"].Value;
				proxyPage = authPath + child["$ProxyPage"].Value;
				//authCodePage = authPath + xr["$AuthCodePage"].Value;
				name = child["$Name"].Value;
				LoginModuleParam lmp = new LoginModuleParam(proxyPage, child["$Processor"].Value, child["$SuccessRedirect"].Value, child["$SignoutRedirect"].Value, child["$SigninFormUrl"].Value);
				List.Add(name, lmp);
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			InitCurtParam();
			if (!string.IsNullOrEmpty(Act) && Act.Equals("signout"))
			{
				Passport.Logout();
				CurtParam.Redirect(CurtParam.SignoutRedirect);
			}
			else
			{
				Authenticator.MakeTicket();
				if (Passport.RememberAuthenticate())
				{
					Authenticator.Login(Authenticator.RememberedName);
					CurtParam.Redirect(CurtParam.SuccessRedirect);
				}
			}
		}

		#region IConfigurable Members

		public object RereadConfigFile()
		{
			ReadConfigFile();
			return null;
		}

		#endregion
	}
}