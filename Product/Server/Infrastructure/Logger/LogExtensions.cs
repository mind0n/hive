using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
	public static class LogExtensions
	{
		public static string Uid(this string prefix)
		{
			var d = DateTime.Now;
			var s =$"{prefix}_{d.ToString("yyyyMMdd_HHmmss_fff")}";
			return s;
		}

		public static string ShiftFile(this string file, string basedir = null, string ext = null)
		{
			if (ext == null)
			{
				ext = "txt";
			}
			if (basedir == null)
			{
				basedir = AppDomain.CurrentDomain.BaseDirectory;
			}
			else if (basedir.IndexOf(":") < 0)
			{
				basedir = AppDomain.CurrentDomain.BaseDirectory + basedir;
			}
			if (!Directory.Exists(basedir))
			{
				Directory.CreateDirectory(basedir);
			}
			var oldfile = $"{basedir}\\{file}.{ext}";
			var newfile = $"{basedir}\\{Uid(file)}.{ext}";
            if (File.Exists(oldfile))
			{
				File.Move(oldfile, newfile);
			}
			return oldfile;
		}

		public static bool IsAbsPath(this string path)
		{
			return path.IndexOf(":") >= 0;
		}

		public static string PathExt(this string target, char splitter = '\\')
		{
			if (string.IsNullOrEmpty(target))
			{
				return string.Empty;
			}
			var list = target.Split(splitter);
			var s = list[list.Length - 1];
			var n = s.LastIndexOf('.');
			return s.Substring(n + 1, s.Length - n - 1);
		}

		public static string PathNormalize(this string path, char splitter = '\\', char joiner = '\\', bool appendTailJoiner = true)
		{
			var list = path.Split(splitter);
			var rlt = new List<string>();
			foreach (var i in list)
			{
				if ("..".Equals(i) && rlt.Count > 0)
				{
					rlt.RemoveAt(rlt.Count - 1);
					continue;
				}
				else if (".".Equals(i) || string.IsNullOrEmpty(i.Trim()))
				{
					continue;
				}
				rlt.Add(i);
			}
			return string.Join(joiner.ToString(), rlt.ToArray()) + (appendTailJoiner ? joiner.ToString() : string.Empty);
		}

		public static string PathMove(this string file, string des, string folder)
		{
			var rlt = new List<string>();
			var plist = file.Split('\\');
			int i = plist.Length - 1;
			for (; i >= 0; i--)
			{
				var curt = plist[i];
				if (string.Equals(curt, folder, StringComparison.OrdinalIgnoreCase))
				{
					break;
				}
				rlt.Insert(0, curt);
			}
			rlt.InsertRange(0, des.Split('\\'));
			var path = string.Join("\\", rlt.ToArray());
			return path;
		}

		public static string PathMap(this string url, string bas = null, char splitter = '/', char joiner = '\\')
		{
			string rlt = url;
			if (bas == null)
			{
				bas = AppDomain.CurrentDomain.BaseDirectory;
			}
			else
			{
				bas = bas.PathNormalize();
			}
			if (rlt.StartsWith(splitter.ToString()))
			{
				rlt = rlt.Substring(1);
			}
			if (rlt.EndsWith(splitter.ToString()))
			{
				rlt = rlt.Substring(0, rlt.Length - 1);
			}
			rlt = rlt.Replace(splitter, joiner);
			if (rlt.IndexOf("{0}") >= 0)
			{
				rlt = string.Format(rlt,bas);
			}
			else
			{
				rlt = bas + rlt;
			}
			rlt = rlt.PathNormalize(joiner, joiner, false);
			rlt = rlt.Replace("\\\\", "\\");
			if (rlt.IndexOf(bas) < 0)
			{
				throw new SecurityException("Invalid path mapping");
			}
			return rlt;
		}

		public static Hashtable UrlArgs(this string urlstr)
		{
			Hashtable rlt = new Hashtable();
			if (string.IsNullOrWhiteSpace(urlstr) || urlstr.IndexOf('=') < 0)
			{
				return rlt;
			}

			var temp = urlstr.Split('?');
			urlstr = temp[temp.Length - 1];
			var list = urlstr.Split('&');

			foreach (var i in list)
			{
				var kv = i.Split('=');
				if (kv.Length > 1)
				{
					rlt[kv[0]] = kv[1];
				}
				else if (kv.Length > 0)
				{
					rlt[kv[0]] = string.Empty;
				}
			}
			return rlt;
		}
	}
}
