using System.Web.Security;
using Joy.Core.Encode;
using Joy.Web.Mvc.IO;
using System;

namespace Joy.Web.Mvc.Security
{
    public class WebSessionTicket : MarshalByRefObject
    {
        #region Identification

        /// <summary>
        /// Auto generated
        /// </summary>
        [Cookie]
        public string Hash { get; set; }

        [Session]
        public string DecHash { get; set; }

        [Session]
        public FormsAuthenticationTicket DecFACookie { get; set; }

        [Session]
        public UserData DecFAUserData { get; set; }

        [Session]
        public string ReturnUrl { get; set; }

        [Session]
        public string Username { get; set; }

        [Session]
        public string Id { get; set; }

        [Session]
        public string ServerToken { get; set; }

        #endregion

        #region Authentication

        /// <summary>
        /// Browser provided user pwd encrypted tmp token. BPETT
        /// </summary>
        [Cookie]
        public string ClientSecToken { get; set; }

        /// <summary>
        /// Decrypted BPETT saved in session.
        /// </summary>
        [Session]
        public string ClientToken { get; set; }

        /// <summary>
        /// Serverside auth token used by authentication logic.  
        /// Different from token used by identification logic.
        /// </summary>
        [Session]
        public string ServerAuthToken { get; set; }

        /// <summary>
        /// Server auth token encrypted by client token.
        /// Send to browser from server.
        /// </summary>
        [Cookie]
        public string ClientExToken { get; set; }

        /// <summary>
        /// Client confirm of the ClientExToken, client token encrypted by server auth token.
        /// </summary>
        [Cookie]
        public string ClientConfirm { get; set; }

        [Session]
        public string DecClientConfirm { get; set; }

        #endregion

    }

    public class WebCacheTicket : MarshalByRefObject
    {
        [Cookie]
        public string Hash { get; set; }

        [Cache(Postfix = "Hash")]
        public string Id { get; set; }

        [Cache(Postfix = "Hash")]
        public string Token { get; set; }

        [Cookie]
        public string ClientSecToken { get; set; }

        [Cache(Postfix = "Hash")]
        public string ClientToken { get; set; }

        [Cache(Postfix = "Hash")]
        public string ServerTmpToken { get; set; }

        [Cache(Postfix = "Hash")]
        public JoyPrincipal CurrentPrincipal { get; set; }

        [Cookie]
        public string ClientExToken { get; set; }

        [Cookie]
        public string ClientConfirm { get; set; }

        public bool IsAuthenticated
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(ClientSecToken) || string.IsNullOrEmpty(ServerTmpToken))
                    {
                        return false;
                    }
                    var ct = OpenSSL.OpenSSLDecrypt(ClientSecToken, ServerTmpToken);
                    return string.Equals(ct, ClientToken);
                }
                catch
                {
                    return false;
                }
            }
        }
    }

}