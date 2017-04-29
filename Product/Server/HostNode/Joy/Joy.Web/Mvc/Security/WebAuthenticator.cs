using System.Web;
using System.Web.Security;
using Joy.Core;
using Joy.Core.Encode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Joy.Web.Mvc.Security
{
    public class WebAuthenticator : IAuthenticator
    {
        private const string AnonymousUser = "$";
        

        public void DeAuth()
        {
            FormsAuthentication.SignOut();
        }
        public bool PreAuth(string user, string returl, Func<string, string> getpwd)
        {
            try
            {
                var pwd = getpwd(user);
                if (!string.IsNullOrEmpty(pwd))
                {
                    var tk = WebIdentifier.Ticket;
                    tk.Username = AnonymousUser;
                    tk.ReturnUrl = returl;
                    var clientSecToken = tk.ClientSecToken;
                    var clientTmpToken = OpenSSL.OpenSSLDecrypt(clientSecToken, pwd);
                    tk.ClientToken = clientTmpToken;
                    var authToken = tk.ServerAuthToken;
                    var extoken = OpenSSL.OpenSSLEncrypt(authToken, clientTmpToken);
                    tk.ClientExToken = extoken;
                    tk.Username = user;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Error.Handle(ex);
                return false;
            }
        }

        public bool Auth(Func<string, string> getrole)
        {
            var tk = WebIdentifier.Ticket;
            try
            {
                var dcc = tk.DecClientConfirm;
                var ct = tk.ClientToken;
                var rlt = string.Equals(dcc, ct);
                if (!rlt)
                {
                    tk.Username = AnonymousUser;
                }
                else
                {
                    var user = tk.Username;
                    if (string.IsNullOrEmpty(user))
                    {
                        user = AnonymousUser;
                    }

                    var d = new UserData { Id = tk.Id, Role = getrole != null ? getrole(user) : "Guest", Username = tk.Username };
                    GenFormCookie(user, d);

                }
                return rlt;
            }
            catch (Exception ex)
            {
                Error.Handle(ex);
                tk.Username = null;
                return false;
            }
        }

        private static void GenFormCookie(string user, UserData d)
        {
            var authTicket = new FormsAuthenticationTicket(1, user, DateTime.Now, DateTime.Now.AddMinutes(30),
                true, d.ToJson());

            string cookieContents = FormsAuthentication.Encrypt(authTicket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieContents)
            {
                Expires = authTicket.Expiration,
                Path = FormsAuthentication.FormsCookiePath
            };
            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        public string FormatRetUrl(string returl)
        {
            return returl;
        }
    }
}
