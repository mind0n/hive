using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Joy.Core
{
	public class FileSystem
	{
		public static string MapPath(string path, string filename = null, bool autoCreate = true)
		{
			string rlt = string.Empty;
			if (!path.EndsWith("\\"))
			{
				rlt = string.Concat(AppDomain.CurrentDomain.BaseDirectory, path, "\\");
			}
			else
			{
				rlt = string.Concat(AppDomain.CurrentDomain.BaseDirectory, path);
			}
			if (autoCreate && !Directory.Exists(rlt))
			{
				try
				{
					Directory.CreateDirectory(rlt);
				}
				catch (Exception e)
				{
					ErrorHandler.Handle(e);
				}
			}
			if (!string.IsNullOrEmpty(filename))
			{
				rlt += filename;
				if (autoCreate && !File.Exists(rlt))
				{
					try
					{
						File.WriteAllText(rlt, string.Empty);
					}
					catch (Exception e)
					{
						ErrorHandler.Handle(e);
					}
				}
			}
			return rlt;
		}
	}
}
