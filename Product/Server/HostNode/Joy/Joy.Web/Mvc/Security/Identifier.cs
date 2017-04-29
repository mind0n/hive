using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using Joy.Core;
using Joy.Web.Controllers;

namespace Joy.Web.Mvc.Security
{
    public abstract class Identifier : IIdentity
    {
        #region Initialize Identifier
        private static Identifier instance;

        public static Identifier Instance
        {
            get
            {
                return instance;
            }
        }
        public static Identifier CreateInstance(Cfg<AuthConfig> cfg)
        {
            instance = cfg.Instance.Identifier.CreateInstance<Identifier>();
            return instance;
        }
        #endregion

        public abstract void Identify();

        public abstract void ParseRequest(object context);

        protected string name { get; set; }

        private static int sid;

        protected static int SimpleId
        {
            get
            {
                return ++sid;
            }
        }

        public static string MakeId(string prefix = "")
        {
#if DEBUG
            return prefix + "_" + SimpleId;
#else
            return prefix + "_" + Guid.NewGuid().ToString();
#endif
        }

        #region Identity Interface

        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public abstract bool IsAuthenticated { get; }

        public string Name
        {
            get { return name; }
        }

        #endregion

        public abstract bool IsValidIdentity { get; }
        //public bool IsValidIdentity
        //{
        //    get
        //    {
        //        try
        //        {
        //            var hash = Ticket.Hash;
        //            var token = Token;
        //            if (string.IsNullOrEmpty(hash) || string.IsNullOrEmpty(token))
        //            {
        //                return false;
        //            }
        //            var id = OpenSSL.OpenSSLDecrypt(hash, token);
        //            var sid = Id;
        //            return string.Equals(id, sid);
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }
        //}
    }
}
