using System;
using System.Security.Principal;
using System.Text;

namespace Joy.Users
{
    public class UserIdentity : IIdentity
    {

        public Ticket Ticket
        {
            get
            {
                return uticket;
            }
        }
        protected readonly string uname;
        protected readonly string utype;
        protected readonly string uid;
        protected readonly Ticket uticket;

        public UserIdentity(string id = null, Ticket tk = null, string name = UserConstants.Anonymous, string type = UserConstants.CustomType)
        {
            uname = name;
            utype = type;
            uid = id ?? GenerateId();
            uticket = tk ?? Ticket.NewTicket();
        }

        public static string GenerateId()
        {
            const string store =
                "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ-=[]',./<>?:{}+_)(*&^%$#@!~`";

            var g = Guid.NewGuid();
            var ba = g.ToByteArray();
            var b = new StringBuilder();
            var rnd = new Random(ba[2]);
            var upper = ba.Length + rnd.Next(6,10);

            for (int i = 0; i < upper; i++)
            {
                int rdx = store[rnd.Next(0, store.Length - 1)];
                int pos = i >= ba.Length ? rdx%store.Length : (ba[i%ba.Length]%store.Length);
                b.Append(store[pos]);
            }
            return b.ToString();
        }

        public UserIdentity AnonymousIdentity
        {
            get
            {
                return new UserIdentity();
            }
        }
        public virtual string Id
        {
            get
            {
                return uid;
            }
        }
        public virtual string AuthenticationType
        {
            get
            {
                return utype;
            }
        }

        public virtual string Name
        {
            get
            {
                return uname;
            }
        }


        public bool IsAuthenticated
        {
            get
            {
                return Ticket != null && Ticket.PreAuthCompleted && Ticket.AuthCompleted;
            }
        }
    }
}
