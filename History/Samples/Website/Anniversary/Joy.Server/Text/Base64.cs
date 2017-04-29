using System;
using System.Collections.Generic;
using System.Text;

namespace Joy.Server.Text
{
	public class Base64
	{
		public static string Encode(string oritin)
		{
			UTF8Encoding ue = new UTF8Encoding();
			return Encode(ue.GetBytes(oritin));
		}
		public static string Encode(byte[] origin)
		{
			return Convert.ToBase64String(origin);
		}
		public static string Decode(string encoded, Encoding encoder)
		{
			string rlt = encoder.GetString(Convert.FromBase64String(encoded.Replace(' ', '+')));
			return rlt;
		}
		public static string Decode(string encoded)
		{
			return Decode(encoded, new UTF8Encoding());
		}
	}
}
