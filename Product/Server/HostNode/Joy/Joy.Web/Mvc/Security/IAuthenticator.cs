using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joy.Web.Mvc.Security
{
    interface IAuthenticator
    {
        bool PreAuth(string user, string returl, Func<string, string> getpwd);
        bool Auth(Func<string, string> getrole );
    }
}
