using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ULib.Core.Security;

namespace ULib.Core
{
    public static class CoreExtensions
    {
        public static string AESEncrypt(this string text, string key)
        {
            string rlt = text.EncryptStringAES(key);
            return rlt;
        }

        public static string AESDecrypt(this string data, string key)
        {
            string rlt = data.DecryptStringAES(key);
            return rlt;
        }

        public static string Base64Decode(this string data)
        {
            ASCIIEncoding en = new ASCIIEncoding();
            byte[] bytes = Convert.FromBase64String(data);
            return en.GetString(bytes);
        }

        public static byte[] Base64DecodeByteArray(this string data)
        {
            byte[] bytes = Convert.FromBase64String(data);
            return bytes;
        }

        public static string Base64Encode(this string text)
        {
            ASCIIEncoding en = new ASCIIEncoding();
            byte[] bytes = en.GetBytes(text);
            return Convert.ToBase64String(bytes);
        }

    }
}
