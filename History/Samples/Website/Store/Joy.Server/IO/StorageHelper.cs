using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Joy.Server.IO
{
	public class PathInfo
	{
		public string Drive
		{
			get
			{
				return GetInfo(0);
			}
		}
		public string CurrentDirName
		{
			get
			{
				return GetInfo(-2);
			}
		}
		public string ParentDirName
		{
			get
			{
				return GetInfo(-3);
			}
		}
		public string FullPath
		{
			get
			{
				return vFullPath;
			}
		}
		protected string vFullPath;
		protected string[] vPath;
		public PathInfo(string path)
		{
			if (!string.IsNullOrEmpty(path))
			{
				vFullPath = path;
				if (path[path.Length - 1] != '\\')
				{
					path += '\\';
				}
				vPath = path.Split('\\');
			}
		}
		protected string GetInfo(int index)
		{
			if (vPath != null)
			{
				if (index >= 0)
				{
					if (vPath.Length > index)
					{
						return vPath[index];
					}
				}
				else
				{
					if (vPath.Length + index >= 0)
					{
						return vPath[vPath.Length + index];
					}
				}
			}
			return null;
		}
	}
	public class DiskHelper
	{
		public enum DirectoryItemType : int
		{
			File,
			Directory
		}
		public delegate bool BoolDlgStringType(string path, DirectoryItemType type);
		public delegate bool BoolDlgString(FileInfo fileInfo);
		public static string MapPath(string uri)
		{
			string rlt = null;
			if (!string.IsNullOrEmpty(uri))
			{
				rlt = uri.Replace('/', '\\');
			}
			if (rlt[0] == '\\')
			{
				rlt = rlt.Substring(1);
				rlt = AppDomain.CurrentDomain.BaseDirectory + rlt;
			}
			else
			{
				rlt = Directory.GetCurrentDirectory() + rlt;
			}
			return rlt;
		}
		public static string MapUrl(string path, string relativeDir)
		{
			string rlt = null;
			string baseDir = AppDomain.CurrentDomain.BaseDirectory;
			if (!string.IsNullOrEmpty(relativeDir))
			{
				baseDir = relativeDir;
			}
			if (!string.IsNullOrEmpty(path))
			{
				int n = path.IndexOf(baseDir);
				if (n == 0)
				{
					rlt = path.Substring(baseDir.Length);

					rlt = rlt.Replace('\\', '/');
				}
			}
			return '/' + rlt;
		}
		public static bool EnumDirectory(string path, BoolDlgStringType callback)
		{
			DirectoryInfo[] pathInfo = new DirectoryInfo(path).GetDirectories();
			foreach (DirectoryInfo dir in pathInfo)
			{
				if (!callback(dir.FullName, DirectoryItemType.Directory))
				{
					return false;
				}
			}
			FileInfo[] fileInfo = new DirectoryInfo(path).GetFiles();
			foreach (FileInfo file in fileInfo)
			{
				if (!callback(file.FullName, DirectoryItemType.File))
				{
					return false;
				}
			}
			return true;
		}
		public static bool EnumFile(string path, BoolDlgString callback)
		{
			//string path
			FileInfo[] fileInfo = new DirectoryInfo(path).GetFiles();
			foreach (FileInfo file in fileInfo)
			{
				if (!callback(file))
				{
					return false;
				}
			}
			DirectoryInfo[] pathInfo = new DirectoryInfo(path).GetDirectories();
			bool rlt = false;
			foreach (DirectoryInfo dir in pathInfo)
			{
				rlt = EnumFile(dir.FullName, callback);
				if (!rlt)
				{
					return false;
				}
			}
			return true;
		}
	}
}
