using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class CoreExtensions
    {
	    public static void D(this object o)
	    {
		    if (Debugger.IsAttached)
		    {
			    Debug.WriteLine(o.ToString());
			    Debugger.Break();
		    }
	    }

		public static string PathLastName(this string target, bool includeExt = true, params char[] splitter)
		{
			if (!string.IsNullOrEmpty(target) && !string.IsNullOrEmpty(target.Trim()))
			{
				if (splitter == null || splitter.Length < 1)
				{
					splitter = new[] { '\\' };
				}
				string[] list = target.Split(splitter);
				string result = list[list.Length - 1];
				if (!includeExt)
				{
					int pos = result.LastIndexOf('.');
					if (pos <= 0)
					{
						result = string.Empty;
					}
					else if (pos > 0)
					{
						result = result.Substring(0, pos - 1);
					}
				}
				return result;
			}
			return string.Empty;
		}

		public static string PathWithoutFilename(this string file, bool upfolder = false)
		{
			var list = new List<string>(file.Split('\\'));
			list.RemoveAt(list.Count - 1);
			if (upfolder)
			{
				return list[list.Count - 1];
			}
			else
			{
				return string.Join("\\", list.ToArray());
			}
		}

		public static string PathAppendFilename(this string path, string filename)
		{
			if (!string.IsNullOrEmpty(path))
			{
				if (!path.EndsWith("\\"))
				{
					path += "\\";
				}
				path += filename;
			}
			return path;
		}

		public static string PathAbs(this string path, char splitter = '/')
		{
			if (path.IndexOf(":\\") < 1)
			{
				path = AppDomain.CurrentDomain.BaseDirectory + string.Join("\\", path.Split(splitter));
				path = path.Replace("\\\\", "\\");
			}
			return path;
		}

		public static string MakeAbsolutePath(this string path)
		{
			if (!path.IsAbsolutePath())
			{
				return AppDomain.CurrentDomain.BaseDirectory + path;
			}
			return path;
		}

		public static bool IsAbsolutePath(this string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return false;
			}
			if (path.IndexOf(':') >= 0 || path.StartsWith("\\\\"))
			{
				return true;
			}
			return false;
		}

	    public static Result BufferedRead(this Stream s, Action<byte[], int> callback)
	    {
		    var rlt = new Result();
		    if (callback != null)
		    {
			    try
			    {
				    var buf = new byte[4096];
				    int offset = 0;
				    while (true)
				    {
					    var n = s.Read(buf, offset, buf.Length);
					    callback(buf, n);
					    if (n == 0 || n < buf.Length)
					    {
						    break;
					    }
				    }
				    rlt.Set();
			    }
			    catch (Exception ex)
			    {
				    rlt.Error(ex);
			    }
		    }
		    return rlt;
	    }

	    public static T FromCache<T>(this string key, IDictionary dict, Func<string, IDictionary, T> misshandler = null)
	    {
		    try
		    {
			    if (dict.Contains(key))
			    {
				    return (T) dict[key];
			    }
			    if (misshandler != null)
			    {
				    return misshandler(key, dict);
			    }
			    return default(T);
		    }
		    catch (Exception ex)
		    {
			    if (Debugger.IsAttached)
			    {
					Debug.WriteLine(ex.ToString());
				    Debugger.Break();
			    }
				return default(T);
		    }
	    }
    }

	public class ReadResult : Result
	{
		public int Offset { get; set; }
		public int Size { get; set; }
		public byte[] Buffer { get; set; }
	}
}
