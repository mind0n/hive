using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Joy.Core.Logging;
using Joy.Core;

namespace Joy.Common
{
	public class FileSystem : JFileSystem
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

		public override string BaseDir
		{
			get
			{
				return AppDomain.CurrentDomain.BaseDirectory;
			}
		}

		public override bool Exists(string fullname, bool isFile = true)
		{
			return isFile ? File.Exists(fullname) : Directory.Exists(fullname);
		}

		public override string Read(string fullname)
		{
			try
			{
				return File.ReadAllText(fullname);
			}
			catch (Exception ex)
			{
				ErrorHandler.Handle(ex);
				return null;
			}
		}

		public override void Copy(string src, string des, bool isMove = false)
		{
			try
			{
				if (isMove)
				{
					File.Move(src, des);
				}
				else
				{
					byte[] bytes = File.ReadAllBytes(src);
					File.WriteAllBytes(des, bytes);
				}
			}
			catch (Exception ex)
			{
				ErrorHandler.Handle(ex);
			}
		}
	}
}
