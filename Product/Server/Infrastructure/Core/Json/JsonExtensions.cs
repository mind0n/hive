using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public static class JsonExtensions
	{
		public static string Jsonf(this string raw)
		{
			return raw.Replace("\"", "\\\"").Replace("\r", "\\\r").Replace("\n", "\\\n");
		}

		public static bool Has(this string s, params string[] sub)
		{
			if (string.IsNullOrEmpty(s) || sub == null || sub.Length < 1)
			{
				return false;
			}
			foreach (var i in sub)
			{
				if (s.IndexOf(i, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					return true;
				}
			}
			return false;
		}

		public static bool Belong(this char ch, params string[] targets)
		{
			if (targets == null || targets.Length < 1)
			{
				return false;
			}
			foreach (var i in targets)
			{
				if (i.Contains(ch))
				{
					return true;
				}
			}
			return false;
		}

		public static byte[] Bytes(this string cnt)
		{
			var en = new UTF8Encoding();
			return en.GetBytes(cnt);
		}

		public static Stream Stream(this string cnt, bool dispose = false)
		{
			if (string.IsNullOrEmpty(cnt))
			{
				if (dispose)
				{
					using (var ms = new MemoryStream())
					{
						return ms;
					}
				}
				return new MemoryStream();
			}
			var en = new UTF8Encoding();
			var bs = en.GetBytes(cnt);
			if (dispose)
			{
				using (var mms = new MemoryStream(bs))
				{
					return mms;
				}
			}
			return new MemoryStream(bs);
		}
	}
}
