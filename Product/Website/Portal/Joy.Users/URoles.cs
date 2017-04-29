using System;
using System.Collections.Generic;
using System.Reflection;
using Joy.Core;

namespace Joy.Users
{
    public class URoles
    {
        public delegate bool EnumRoleDelegate(URole role);
        public static URoles Instance = new URoles();
        public List<URole> Roles { get; set; }

        public bool EnableAnonymousAccess
        {
            get { return enableAnonymousAccess; }
            set
            {
                enableAnonymousAccess = value;
                if (enableAnonymousAccess)
                {
                    RegisterRole(URoles.AnonymousRole);
                }
                else
                {
                    UnregisterRole(URoles.AnonymousRole);
                }
            }
        }

        public bool EnableAutoAddRole;

        private bool enableAnonymousAccess;
        public URoles()
        {
            Roles = new List<URole>();
        }

        public URole EnumRoles(EnumRoleDelegate callback)
        {
            if (callback != null)
            {
                foreach (var i in Roles)
                {
                    if (!callback(i))
                    {
                        return i;
                    }
                }
            }
            return null;
        }
        
        public void RegisterRole(URole role)
        {
            UnregisterRole(role);
            Roles.Add(role);
        }

        public void UnregisterRole(URole role)
        {
            for (int i = 0, l = Roles.Count; i < l; i++)
            {
                var r = Roles[i];
                if (string.Equals(role.Name, r.Name, StringComparison.OrdinalIgnoreCase))
                {
                    Roles.Remove(r);
                    l--;
                }
            }
        }

        public URole GetRole(string name)
        {
            foreach (var i in Roles)
            {
                if (string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return null;
        }

        public static readonly URole AnonymousRole = new URole(UserConstants.Anonymous, 0);
    }

    public class URole
    {
        private string name;
        private int level;

        public string Name
        {
            get { return name; }
        }

        public int Level
        {
            get { return level; }
        }

        public URole(string name, int level)
        {
            this.name = name;
            this.level = level;
        }

    }
}
