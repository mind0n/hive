using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Joy.Core.Encode
{
	public static class OWHash
	{
		public static string MD5(this string str)
		{
			string cl1 = str;
			string pwd = "";
			System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();// 加密后是一个字节类型的数组 
			byte[] s = md5.ComputeHash(Encoding.Unicode.GetBytes(cl1));
			for (int i = 0; i < s.Length; i++)
			{// 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得 
				pwd = pwd + s[i].ToString("x");// 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
			}
			return pwd;
		}
		public static string MD5CSP(this string encypStr)
		{
			string retStr;
			MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

			//创建md5对象
			byte[] inputBye;
			byte[] outputBye;

			//使用GB2312编码方式把字符串转化为字节数组．
			inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);

			outputBye = m5.ComputeHash(inputBye);

			retStr = System.BitConverter.ToString(outputBye);
			retStr = retStr.Replace("-", "").ToLower();
			return retStr;
		}
		public static string SHA256(this string str)
		{
			byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
			SHA256Managed Sha256 = new SHA256Managed();
			byte[] Result = Sha256.ComputeHash(SHA256Data);
			return Convert.ToBase64String(Result);  //返回长度为44字节的字符串
		}
	}
}
