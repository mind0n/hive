using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Routing;
using Joy.Core;
using Joy.Core.Encode;

namespace Joy.Users.Web
{
    public class WebTicketService  : TicketService
    {
        private RequestContext rc;

        private HttpSessionStateBase Session
        {
            get
            {
                return rc.HttpContext.Session;
            }
        }

        private HttpCookieCollection CookiesQ
        {
            get
            {
                return rc.HttpContext.Request.Cookies;
            }
        }

        private HttpCookieCollection Cookies
        {
            get
            {
                return rc.HttpContext.Response.Cookies;
            }
        }


        public WebTicketService(RequestContext requestcontext)
            : base(GetUserPassword)
        {
            rc = requestcontext;
        }

        protected override Ticket GetTicket()
        {
            var ppl = PPLService.ThreadPPL;
            if (ppl == null) return null;
            var u = ppl.GetUserIdentity();
            return u != null ? u.Ticket : null;
        }

        public override bool IsAuthenticated
        {
            get
            {
                var ticket = GetTicket();
                if (ticket == null)
                {
                    return false;
                }
                return ticket.PreAuthCompleted && ticket.AuthCompleted;
            }
        }

        public override bool AuthTicket(string uname, Ticket tk = null)
        {
            var ticket = tk ?? GetTicket();
            var ParCTK = rc.HttpContext.Request.Cookies.GetValue(UserConstants.ClientChecksum);
            return ticket.Checksum(ParCTK);
        }

        public override Ticket PreAuthTicket(string uname, string clientToken = null)
        {
            try
            {
                string tk = CookiesQ.GetValue(UserConstants.TKClient);
                Ticket r = base.PreAuthTicket(uname, tk.SecClean());
                if (r != null)
                {
                    if (r.PreAuthCompleted)
                    {
                        Cookies.SetValue(UserConstants.Checksum, r.SecPar);
                        var svc = new WebPPLService(rc);
                        svc.SetPrincipal(PPLService.ThreadPPL);
                    }
                    else
                    {
                        Cookies.SetValue(UserConstants.TKServer, string.Empty);
                        Cookies.SetValue(UserConstants.Checksum, string.Empty);
                    }
                }
                return r;
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex);
                return null;
            }
        }
        public static string GetUserPassword(string uname)
        {
            return "pwd";
        }

        public override void UnAuthTicket()
        {
            Cookies.Clear();
            Cookies.SetValue(UserConstants.UID, UserIdentity.GenerateId());

            base.UnAuthTicket();
        }
    }
}
