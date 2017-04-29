using Joy.Core.Encode;
using System;

namespace Portal.Testing.Crypto
{
    public partial class TestCrypto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string s = Request.Form["s"];
            string p = Request.Form["p"];
            string a = Request.Form["a"];
            if (!string.IsNullOrEmpty(a))
            {
                s = "Success!";
                s = OpenSSL.OpenSSLEncrypt(s, p);
                Response.Clear();
                Response.Write(s);
                Response.End();
            }
            else if (!string.IsNullOrEmpty(s))
            {
                Response.Clear();
                string t = OpenSSL.OpenSSLDecrypt(s, p);
                Response.Write(t);
                Response.End();
            }
        }
    }
}