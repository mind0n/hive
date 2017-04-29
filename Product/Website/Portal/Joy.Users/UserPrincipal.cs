using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Joy.Core;

namespace Joy.Users
{
    public class UserPrincipal : IPrincipal
    {
        internal DateTime TimeCreated;
        protected UserIdentity uid;
        protected List<URole> uroles = new List<URole>();
        protected string clientUniqueIdentifier;

        public string ClientId
        {
            get
            {
                return clientUniqueIdentifier;
            }
        }
        
        public UserPrincipal(UserIdentity identity = null, int maxlevel = 0, string clientId = null)
        {
            TimeCreated = DateTime.UtcNow;
            uid = identity ?? new UserIdentity(clientId);
            clientUniqueIdentifier = clientId;
            uroles.Add(URoles.AnonymousRole);
            foreach (var i in URoles.Instance.Roles)
            {
                if (i.Level <= maxlevel)
                {
                    uroles.Add(i);
                }
            }
        }

        public string Id
        {
            get
            {
                return uid.Id;
            }
        }

        public UserIdentity GetUserIdentity()
        {
            return uid;
        }

        public IIdentity Identity
        {
            get { return uid; }
        }

        internal void SetClientId(string id)
        {
            clientUniqueIdentifier = id;
        }

        public void Authorize(URole[] roles)
        {
            uroles.AddRoles(roles);
        }

        public void UnAuthorize()
        {
            var id = this.GetUserIdentity();
            var tk = id.Ticket;
            tk.Reset();
            uroles.Clear();
            uroles.Add(URoles.AnonymousRole);
        }

        public void Authorize(string[] rols)
        {
            uroles.Clear();
            URoles.Instance.EnumRoles(delegate(URole role)
            {
                //if (string.Equals(role.Name, ))
                foreach (var i in rols)
                {
                    if (string.Equals(i, role.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        uroles.Add(role);
                    }
                }
                return true;
            });
        }

        public virtual bool IsInRoles(params string[] roles)
        {
            foreach (var i in uroles)
            {
                if (i.IsQualified(roles))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsInRole(string role)
        {
            return uroles.Any(i => role.IsQualified(i));
        }
    }
}
