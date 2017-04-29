using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.SessionState;
using DAL.DataEntity;
using Joy.Core;
using Joy.Core.Encode;
using Joy.Server.Web;
using Joy.Users;
using Joy.Users.Web;
using System;
using System.Web;

namespace Portal.Testing
{
	public class TestService : ServiceHandler, IRequiresSessionState
	{
		[Method]
		public string Calc(int a, int b)
		{
			return (a + b).ToString();
		}
		[Method]
		protected string xxx(int a, int b)
		{
			return (a - b).ToString();
		}
		[Method]
		public string Test()
		{
			return DateTime.UtcNow.ToString();
		}
		[Method]
		public string TestJsonRequest(string arg)
		{
			
			return arg;
		}

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
	            s.GetCurrent().Authorize(new []{new URole("admins", 100) });
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

		protected override string Get(HttpContext context)
		{
			
			return "getok";
		}
		protected override string Post(HttpContext context)
		{
			return "postok";
		}
		protected override string Put(HttpContext context)
		{
			return "putok";
		}
		protected override string Delete(HttpContext context)
		{
			return "deleteok";
		}
	}
}