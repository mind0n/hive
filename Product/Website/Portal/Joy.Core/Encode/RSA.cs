using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Joy.Core.Encode
{
	public static class RSA
	{
		public static string RSADecrypt(this string s, string key)
		{
			string result = null;

			if (string.IsNullOrEmpty(s)) throw new ArgumentException("An empty string value cannot be encrypted.");

			if (string.IsNullOrEmpty(key)) throw new ArgumentException("Cannot decrypt using an empty key. Please supply a decryption key.");

			CspParameters cspp = new CspParameters();
			cspp.KeyContainerName = key;

			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
			rsa.PersistKeyInCsp = true;

			string[] decryptArray = s.Split(new string[] { "-" }, StringSplitOptions.None);
			byte[] decryptByteArray = Array.ConvertAll<string, byte>(decryptArray, (a => Convert.ToByte(byte.Parse(a, System.Globalization.NumberStyles.HexNumber))));

			byte[] bytes = rsa.Decrypt(decryptByteArray, true);

			result = System.Text.UTF8Encoding.UTF8.GetString(bytes);

			return result;
		}
		public static string RSAEncrypt(this string s, string key)
		{
			if (string.IsNullOrEmpty(s)) throw new ArgumentException("An empty string value cannot be encrypted.");

			if (string.IsNullOrEmpty(key)) throw new ArgumentException("Cannot encrypt using an empty key. Please supply an encryption key.");

			CspParameters cspp = new CspParameters();
			cspp.KeyContainerName = key;

			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
			rsa.PersistKeyInCsp = true;

			byte[] bytes = rsa.Encrypt(System.Text.UTF8Encoding.UTF8.GetBytes(s), true);

			return BitConverter.ToString(bytes);
		}
	}
}
