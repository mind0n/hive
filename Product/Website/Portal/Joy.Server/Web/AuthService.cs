using Joy.Core;
using Joy.Users;
using Joy.Users.Web;
using System;

namespace Joy.Server.Web
{
    public class AuthService : ServiceHandler
    {
        [Method]
        public ResultBase PreAuth(string uname)
        {
            ResultBase r = new ResultBase();
            WebTicketService wts = new WebTicketService(RequestContext);
            Ticket t = wts.PreAuthTicket(uname);
            if (t == null || !t.PreAuthCompleted)
            {
                r.LastError = new Exception("Pre-authentication failed");
            }
            return r;
        }

        [Method]
        public ResultBase Auth()
        {
            var r = new ResultBase();
            var wts = new WebTicketService(RequestContext);
            if (!wts.AuthTicket(PPLService.ThreadIdentity.Name))
            {
                r.LastError = new Exception("Authentication failed");
            }
            else
            {
                var s = new WebPPLService(RequestContext);
                s.GetCurrent().Authorize(new[] { new URole("admins", 100) });
            }
            return r;
        }

        [Method]
        public void UnAuth()
        {
            var wts = new WebTicketService(RequestContext);
            wts.UnAuthTicket();
            var wps = new WebPPLService(RequestContext);
            wps.Clear();
        }

    }
}