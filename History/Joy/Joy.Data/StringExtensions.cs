using System;
using System.Text;

namespace Joy.Data
{

    internal static class StringExtensions
    {
        public static byte[] ToBytes(this string str, int size)
        {
            if (string.IsNullOrEmpty(str))
            {
                return new byte[size];
            }
            byte[] buffer = new byte[size];
            byte[] strbytes = Encoding.UTF8.GetBytes(str);
            Array.Copy(strbytes, buffer, (size > strbytes.Length) ? strbytes.Length : size);
            return buffer;
        }
    }
}

