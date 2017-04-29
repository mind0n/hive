using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Joy.Web.Mvc.Security
{
    public class JoyPrincipal : IPrincipal
    {
        protected string _hash;

        public string Hash
        {
            get
            {
                return _hash;
            }
        }
        protected List<string> Roles = new List<string>();

        public JoyPrincipal(string hash)
        {
            _hash = hash;
        }

        public bool IsAnonymous
        {
            get
            {
                return Roles.Count < 1;
            }
        }

        public IIdentity Identity
        {
            get { return Thread.CurrentPrincipal.Identity; }
        }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}
