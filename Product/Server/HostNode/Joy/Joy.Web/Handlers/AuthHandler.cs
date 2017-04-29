using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Joy.Web.Handlers;
using Joy.Web.Mvc.Security;

namespace Joy.Web.Handlers
{
    public class AuthHandler : ServiceHandler
    {
        protected WebAuthenticator author;
        public AuthHandler()
        {
            author = new WebAuthenticator();
        }
        public virtual int PreAuth(string returnUrl)
        {
            var uname = Request["uname"];
            return author.PreAuth(uname, returnUrl, user => "abc") ? 0 : 1;
        }
        public virtual int Auth()
        {
            return author.Auth(user => "adminHandler") ? 0 : 1;
        }
    }
}