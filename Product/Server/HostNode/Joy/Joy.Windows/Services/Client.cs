using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Joy.Windows.Services
{
    public class Client
    {
        public string Address;
        public int Port;
        public override bool Equals(object obj)
        {
            var c = obj as Client;
            if (c != null)
            {
                return c.Port == Port && string.Equals(c.Address, Address, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
    public class Clients : List<Client>
    {
        public bool Contains(string addr)
        {
            var self = this;
            foreach (var c in self)
            {
                if (string.Equals(c.Address, addr, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
