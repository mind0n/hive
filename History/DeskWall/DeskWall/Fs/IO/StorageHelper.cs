using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Fs.IO
{
	public class DiskHelper
	{
		public enum DirectoryItemType : int
		{
			File,
			Directory
		}
		public delegate bool BoolDlgStringType(string path, DirectoryItemType type);
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
	}
}
