using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using ULib.Output;
using System.IO;

namespace ULib.Executing.Commands.FileSystem
{
    public class FileCopyCommand : CommandNode
    {
        public string OriginalFile;
        public string TargetFile;
        public bool IsMove;
		public bool IsAbsolute;
        public override string Name
        {
            get
            {
                return string.Format("{0} file {1} to {2}", IsMove ? "Move" : "Copy", OriginalFile, TargetFile);
            }
            set
            {
                base.Name = value;
            }
        }
		public override void Run(CommandResult rlt, bool isPreview = false)
		{
			string originalFile = Executor.Instance.ParseIdString(OriginalFile), targetFile = Executor.Instance.ParseIdString(TargetFile);
			if (!IsAbsolute)
			{
				originalFile = AppDomain.CurrentDomain.BaseDirectory + originalFile;
				targetFile = AppDomain.CurrentDomain.BaseDirectory + targetFile;
			}
            if (originalFile.IndexOf("\r\n") > 0)
            {
                string[] os = originalFile.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                string[] ts = targetFile.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                if (os.Length == ts.Length)
                {
                    for (int i = 0; i < os.Length; i++)
                    {
                        if (i >= 0 && i < os.Length && i < ts.Length)
                        {
                            CopySingFile(os[i], ts[i]);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else if (ts.Length > 0)
                {
                    for (int i = 0; i < os.Length; i++)
                    {
                        if (i >= 0 && i < os.Length && i < ts.Length)
                        {
                            CopySingFile(os[i], ts[0], true);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                CopySingFile(originalFile, targetFile);
            }
		}
        private void CopySingFile(string originalFile, string targetFileOrDir, bool targetIsDir = false)
        {
            if (File.Exists(originalFile))
            {
                string targetFile = targetFileOrDir;
                if (targetIsDir)
                {
                    string name = originalFile.PathLastName();
                    if (!targetFile.EndsWith("\\"))
                    {
                        targetFile += "\\";
                    }
                    targetFile += name;
                }
                byte[] bytes = File.ReadAllBytes(originalFile);
                File.WriteAllBytes(targetFile, bytes);
                if (IsMove)
                {
                    File.Delete(originalFile);
                }
            }
        }
        public override CommandNode Clone()
        {
			return new FileCopyCommand { Id = Id, OriginalFile = OriginalFile, TargetFile = TargetFile, IsMove = IsMove, IsAbsolute = IsAbsolute };
        }
    }
}
