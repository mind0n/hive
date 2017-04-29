using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Joy.Core;
using Joy.Core.Encode;
using System.Web.Routing;
using Joy.Web.Mvc.IO;
using System.Threading;

namespace Joy.Web.Mvc.Security
{
    public class WebIdentifier : Identifier
    {
        private HttpContext cx;

        public static WebSessionTicket Ticket
        {
            get
            {
                var t = proxy.Instance;
                //t.Hash = WebIdentifier.Hash;
                return t;
            }
        }

        protected static HttpContextProxy<WebSessionTicket> proxy;

        public WebIdentifier()
        {
            proxy = new HttpContextProxy<WebSessionTicket>();
        }

        public override void ParseRequest(object context)
        {
            cx = context as HttpContext;
            try
            {
                var hash = Ticket.Hash;
                var token = Ticket.ServerToken;
                if (string.IsNullOrEmpty(hash) || string.IsNullOrEmpty(token))
                {
                    Ticket.DecHash = null;
                    Error.Handle(new ArgumentNullException("Empty hash or token"));
                    return;
                }
                var id = OpenSSL.OpenSSLDecrypt(hash, token);
                Ticket.DecHash = id;

                var cc = Ticket.ClientConfirm;
                var sat = Ticket.ServerAuthToken;
                if (!string.IsNullOrEmpty(cc) && !string.IsNullOrEmpty(sat))
                {
                    var dcc = OpenSSL.OpenSSLDecrypt(cc, sat);
                    Ticket.DecClientConfirm = dcc;
                }
                else
                {
                    Ticket.DecClientConfirm = null;
                }
                var c = cx.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (c != null)
                {
                    var ft = FormsAuthentication.Decrypt(c.Value);
                    Ticket.DecFACookie = ft;
                    Ticket.DecFAUserData = ft.UserData.FromJson<UserData>();
                }
            }
            catch (Exception ex)
            {
                Error.Handle(ex);
            }
        }

        public override void Identify()
        {
            if (!IsValidIdentity)
            {
                FreshIdentity(cx);
            }
        }

        public void FreshIdentity(dynamic context)
        {
            FormsAuthentication.SignOut();
            if (context.Session != null)
            {
                context.Session.Clear();
            }
            context.Request.Cookies.Clear();
            context.Response.Cookies.Clear();

            var i = MakeId("id");
            var t = MakeId("st");
            var st = MakeId("stt");
            var h = OpenSSL.OpenSSLEncrypt(i, t);

            Ticket.Hash = h;
            Ticket.Id = i;
            Ticket.ServerToken = t;
            Ticket.ServerAuthToken = st;
            Ticket.ClientToken = null;
        }

        private static int sid;

        private static int SimpleId
        {
            get
            {
                return ++sid;
            }
        }

        public override bool IsAuthenticated
        {
            get
            {
                try
                {
                    var cst = Ticket.ClientSecToken;
                    var stt = Ticket.ServerAuthToken;
                    var ct = Ticket.ClientToken;
                    if (string.IsNullOrEmpty(cst) || string.IsNullOrEmpty(stt))
                    {
                        return false;
                    }
                    var bct = OpenSSL.OpenSSLDecrypt(cst, stt);
                    return string.Equals(bct, ct);
                }
                catch
                {
                    return false;
                }
            }
        }

        public override bool IsValidIdentity
        {
            get
            {
                try
                {
                    var id = Ticket.Id;
                    var cid = Ticket.DecHash;
                    //var ft = Ticket.DecFACookie;
                    UserData ud = Ticket.DecFAUserData; //ft == null ? null : ft.UserData.FromJson<UserData>();
                    var rlt = !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(cid) && string.Equals(id, cid) &&
                              (ud == null || string.Equals(ud.Username, Ticket.Username));
                    return rlt;
                }
                catch
                {
                    return false;
                }
            }
        }

    }

}
