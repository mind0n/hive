using System.Security.Cryptography;
using System.Web;
using Joy.Core.Encode;
using System;
using System.Text;
using System.Threading;

namespace Joy.Users
{
    public abstract class TicketService
    {
        public delegate string GetPasswordCallback(string input);

        protected GetPasswordCallback GetPassword;
        public TicketService(GetPasswordCallback getpwd)
        {
            if (getpwd == null)
            {
                throw new ArgumentNullException();
            }
            GetPassword = getpwd;
        }

        protected virtual Ticket GetTicket()
        {
            var id = PPLService.ThreadIdentity;
            if (id != null)
            {
                return id.Ticket;
            }
            return null;
        }
        public abstract bool IsAuthenticated { get; }

        //protected virtual bool ValidateTicket(string username, Ticket ticket, string clientsalt)
        //{
        //    if (GetPassword == null || ticket == null || clientsalt == null)
        //    {
        //        return false;
        //    }
        //    string pwd = GetPassword(username);
        //    ticket.SecSalt = AES.AESEncrypt(ticket.Salt, pwd);
        //    ticket.SecToken = AES.AESEncrypt(ticket.Token, pwd);
        //    return string.Equals(clientsalt, ticket.SecSalt);
        //}
        public virtual Ticket PreAuthTicket(string uname, string clientToken = null)
        {
            var ppl = PPLService.ThreadPPL;
            var id = ppl.GetUserIdentity();
            if (id != null)
            {
                var ticket = id.Ticket;
                string pwd = GetPassword(uname);
                string ctoken = OpenSSL.OpenSSLDecrypt(clientToken, pwd);
                ticket.ClientToken = ctoken;
                ticket.SecPar = OpenSSL.OpenSSLEncrypt(ticket.Par, ctoken);
                ticket.PreAuthCompleted = true;
                return ticket;
            }
            return null;
        }

        public virtual bool AuthTicket(string cs, Ticket tk = null)
        {
            var ticket = tk ?? GetTicket();
            return ticket.Checksum(cs);
        }

        public virtual void UnAuthTicket()
        {
            var tk = GetTicket();
            tk.Reset();
        }
    }

    public class Ticket
    {
        public Ticket()
        {
            Token = UserIdentity.GenerateId(); 
            Par = UserIdentity.GenerateId();
        }

        public string ClientToken;
        public string SecToken;
        public string SecPar;
        public bool PreAuthCompleted;
        public bool AuthCompleted;
        public string Par;
        public string Token;

        public bool Checksum(string target)
        {
            var ctoken = OpenSSL.OpenSSLDecrypt(target, Par);
            AuthCompleted = string.Equals(ctoken, ClientToken);
            return AuthCompleted;
        }

        public void Reset()
        {
            ClientToken = string.Empty;
            SecToken = string.Empty;
            SecPar = string.Empty;
            PreAuthCompleted = false;
            AuthCompleted = false;
            Token = UserIdentity.GenerateId();
            Par = UserIdentity.GenerateId();
        }

        public static Ticket NewTicket(bool isBase64 = true)
        {
            var t = new Ticket();
            if (!isBase64) return t;
            t.Token = t.Token.Base64Encode();
            t.Par = t.Par.Base64Encode();
            return t;
        }

    }
}
