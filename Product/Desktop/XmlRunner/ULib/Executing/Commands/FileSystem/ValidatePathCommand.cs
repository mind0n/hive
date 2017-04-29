using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using System.IO;

namespace ULib.Executing.Commands.FileSystem
{
    public class ValidatePathCommand : CommandNode
    {
        public bool AutoCreate = true;
        public string Path;
        public override string Name
        {
            get
            {
                return string.Format("Validate path {0}", Path);
            }
        }

        public override void Run(CommandResult rlt, bool isPreview = false)
        {
            try
            {
                string path = Executor.Instance.ParseIdString(Path);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception e)
            {
                rlt.LastError = e;
            }
        }
    }
}
