using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Joy.Core.Encode
{
    public static class Base64
    {
        public static string Base64Encode(this string oritin)
        {
            UTF8Encoding ue = new UTF8Encoding();
            return Encode(ue.GetBytes(oritin));
        }
        public static string Base64Decode(this string encoded)
        {
            Encoding en = new UTF8Encoding();
            return en.GetString(Decode(encoded));
        }
		public static string Encode(byte[] origin)
		{
			return Convert.ToBase64String(origin);
		}
		public static byte[] Decode(string encoded)
		{
		    return Convert.FromBase64String(encoded.Replace(' ', '+'));
		}
	}
}
