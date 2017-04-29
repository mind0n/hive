using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;

namespace ULib.Executing.Commands.Text
{
    [Command(IsPreviewRun=true, IsConfigurable=false)]
    public class JoinTextCommand : CommandNode
    {
        public string Splitter;
        public string Join;
        public string Target;
        public string SaveId;
        public override string Name
        {
            get
            {
                return string.Concat("Join ", Target, " together", string.IsNullOrEmpty(Splitter) ? " with splitter " : string.Empty, Splitter);
            }
        }
        public override void Run(CommandResult rlt, bool isPreview = false)
        {
            if (!string.IsNullOrEmpty(SaveId))
            {
                string splitter = Splitter;
                string target = Executor.Instance.ParseIdString(Target);
                if (!string.IsNullOrEmpty(target))
                {
                    List<string> list = new List<string>();
                    string[] s = target.Split(new string[] { splitter }, StringSplitOptions.None);
                    if (Join == null)
                    {
                        Join = string.Empty;
                    }
                    foreach (string i in s)
                    {
                        list.Add(Executor.Instance.ParseIdString(i));
                    }
                    Executor.Instance.SetVar(SaveId, false, string.Join(Join, list.ToArray()));
                }
            }
        }
    }
}
