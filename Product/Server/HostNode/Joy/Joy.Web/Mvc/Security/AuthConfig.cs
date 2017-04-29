using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Joy.Web.Mvc.Security
{
    public class AuthConfig
    {
        public string UserField = "uname";
        public string Identifier = typeof(WebIdentifier).AssemblyQualifiedName;
    }

    public class AuthResult
    {

    }
}