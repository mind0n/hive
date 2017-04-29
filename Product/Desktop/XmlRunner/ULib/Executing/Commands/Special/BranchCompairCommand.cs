using System;
using System.Collections.Generic;
using ULib.DataSchema;
using ULib.Output;
using System.Diagnostics;
using ULib.NativeSystem;
using System.IO;

namespace ULib.Executing.Commands.Special
{
	public class BranchCompareCommand : CommandNode
	{
		public string Tool;
		public string TfsRoot;
		public string SrcBranch;
		public string DesBranch;
		public string SrcFile;
        public string SrcDir;
		public string[] SrcFiles
		{
			get
			{
                if (!string.IsNullOrEmpty(SrcDir))
                {
                    List<string> files = new List<string>();
                    string srcdir = Executor.Instance.ParseIdString(SrcDir);
                    Native.EnumFiles(srcdir, new Action<string>((file) =>
                    {
                        if (!string.IsNullOrEmpty(file))
                        {
                            files.Add(file);
                        }
                    }));
                    return files.ToArray();
                }
                else if (!string.IsNullOrEmpty(SrcFile))
                {
                    string srcfile = Executor.Instance.ParseIdString(SrcFile);
                    if (!string.IsNullOrEmpty(srcfile))
                    {
                        return srcfile.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    }
                }
				return new string[0];
			}
		}
		public override string Name
		{
			get
			{
				return string.Format("Compare between branch {0} and {1}:\r\n {2}", SrcBranch, DesBranch, SrcFile);
			}
		}

        string tfsroot;
        string srcbranch;
        string desbranch;
	    string tool;

	    public override void Run(CommandResult rlt, bool isPreview = false)
	    {
	        tfsroot = Executor.Instance.ParseIdString(TfsRoot);
	        srcbranch = Executor.Instance.ParseIdString(SrcBranch);
	        desbranch = Executor.Instance.ParseIdString(DesBranch);
	        tool = Executor.Instance.ParseIdString(Tool);
	        AppendSymbol(ref tfsroot);
	        foreach (string i in SrcFiles)
	        {
	            string src = i;
	            if (src.StartsWith("###"))
	            {
	                src = src.Replace("###", tfsroot);
	            }
	            else if (src.StartsWith("$/Root") || src.StartsWith("$\\Root"))
	            {
	                src = src.Replace("$/Root", tfsroot);
	                src = src.Replace("$\\Root", tfsroot);
	                src = src.Replace("/", "\\");
	                src = src.Replace("\\\\", "\\");
	            }
	            if (!src.EndsWith("\\"))
	            {
	                Compare(src);
	            }
	            else
	            {
	                string[] files = Directory.GetFiles(src, "*.*", SearchOption.AllDirectories);
	                foreach (string file in files)
	                {
	                    Compare(file);
	                }
	            }
	        }
	    }

	    private void Compare(string src)
	    {
	        string t = src.SubPath(tfsroot, srcbranch);
	        string p = string.Concat(tfsroot, desbranch, "\\", t);
	        string a = string.Join(" ", new string[] {src, p});
	        OutputHandler.Handle("Comparing {0}\r\nWith {1}", 0, src, p);
	        Process.Start(tool, a);
	        OutputHandler.Handle(tool + " " + a);
	    }


	    private static void AppendSymbol(ref string tfsroot)
        {
            if (!tfsroot.EndsWith("\\"))
            {
                tfsroot += "\\";
            }
        }
	}
}
