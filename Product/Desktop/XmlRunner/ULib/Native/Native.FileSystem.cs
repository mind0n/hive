using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using Shell32;
using ULib.DataSchema;
using ULib.Exceptions;

namespace ULib.Native
{
    public partial class Native
    {
		public bool IsStopUnzip;
        [DllImport("kernel32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, SYMBOLIC_LINK_FLAG dwFlags);

        public enum SYMBOLIC_LINK_FLAG
        {
            File = 0,
            Directory = 1
        }

		public static void Unzip(string zipfile, string targetPath, Func<FolderItem, bool> preUnzipCallback = null, Action<string> postUnzipCallback = null)
		{
			ShellClass sc = new Shell32.ShellClass();
			Folder SrcFolder = sc.NameSpace(zipfile);
			Folder DestFolder = sc.NameSpace(targetPath);
			FolderItems items = SrcFolder.Items();
			for (int i = 0; i < items.Count; i++)
			{
				FolderItem item = items.Item(i);
				if (preUnzipCallback == null || preUnzipCallback(item))
				{
					DestFolder.CopyHere(item, 20);
					if (postUnzipCallback != null)
					{
						if (!targetPath.EndsWith("\\"))
						{
							targetPath += '\\';
						}
						string targetFile = targetPath + item.Name;
						if (File.Exists(targetFile))
						{
							postUnzipCallback(targetFile);
						}
						else
						{
							ExceptionHandler.Raise("Cannot find the unziped file.\r\n{0}", targetFile);
						}
					}
				}
			}
			
		}

        public static ProcessResult StartProcess(string workingDirectory, string cmdName, string argument, ProcessWindowStyle style = ProcessWindowStyle.Hidden)
        {
            ProcessResult rlt = new ProcessResult();
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.FileName = cmdName;
            proc.StartInfo.Arguments = argument;
            proc.StartInfo.WindowStyle = style;
            proc.StartInfo.WorkingDirectory = workingDirectory;
            proc.StartInfo.CreateNoWindow = true;
            proc.Start();
            StreamReader reader = proc.StandardOutput;
            proc.WaitForExit();
            rlt.Process = proc;
            return rlt;
        }
        public static string GetFileNameFromPath(string fileFullName)
        {
            if (string.IsNullOrEmpty(fileFullName))
            {
                return null;
            }
            int pos = fileFullName.LastIndexOf('\\');
            if (pos >= 0)
            {
                return fileFullName.Substring(pos + 1, fileFullName.Length - pos - 1);
            }
            else
            {
                return fileFullName;
            }
        }
		public static string GetFileExtFromPath(string fileFullName)
		{
			if (string.IsNullOrEmpty(fileFullName)){
				return null;
			}
			int pos = fileFullName.LastIndexOf('.');
			if (pos >= 0)
			{
				string ext = fileFullName.Substring(pos + 1, fileFullName.Length - pos - 1);
				return ext;
			}
			else
			{
				return null;
			}
		}
		public static bool IsFileOfExt(string fullname, string extName)
		{
			return string.Equals(GetFileExtFromPath(fullname), extName, StringComparison.OrdinalIgnoreCase);
		}
		
    }

    public class ProcessResult : Result
    {
        public Process Process;
        public override bool IsSuccessful
        {
            get
            {
                try
                {
                    return Process.ExitCode == 0;
                }
                catch
                {
                    return false;
                }
            }
        }
        public string Output
        {
            get
            {
                try
                {
                    return Process.StandardOutput.ReadToEnd();
                }
                catch
                {
                    return null;
                }
            }
        }
        public string Error
        {
            get
            {
                try
                {
                    return Process.StandardError.ReadToEnd();
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
