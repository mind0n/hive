using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using System.IO;

namespace ULib.Executing.Commands.Text
{
    [Command(IsPreviewRun = false)]
    public class DupTextCommand : CommandNode
    {
        [ParameterAttribute(IsRequired = true)]
        public string Template;
        [ParameterAttribute(IsRequired = true)]
        public string Repeat;
        public string Startup;
        public string Splitter = "\r\n";
        public string PlaceHolder = "%i%";
        public string Length;
        public string Filename;
        public string IsAppend;
        public override string Name
        {
            get
            {
                return string.Format("Duplicate text {0} times", Repeat);
            }
        }
        public override void Run(CommandResult rlt, bool isPreview = false)
        {
            bool isappend = string.IsNullOrEmpty(IsAppend) ? false : Convert.ToBoolean(Executor.Instance.ParseIdString(IsAppend));
            string txt = Executor.Instance.ParseIdString(Template);
            int count = Convert.ToInt32(Executor.Instance.ParseIdString(Repeat));
            string place = Executor.Instance.ParseIdString(PlaceHolder);
            int length = Convert.ToInt32(Executor.Instance.ParseIdString(Length));
            int startup = Convert.ToInt32(Executor.Instance.ParseIdString(Startup));
            if (startup < 0)
            {
                startup = 0;
            }
            StringBuilder b = new StringBuilder();
            if (count > 0)
            {
                for (int i = startup; i < count; i++)
                {
                    string unit = txt + Splitter;
                    if (unit.IndexOf(place) >= 0)
                    {
                        unit = ReplaceIndex(place, length, i, unit);
                    }
                    b.Append(unit);
                }
            }
            OutputId = b.ToString();
            string file = Executor.Instance.ParseIdString(Filename);
            if (!string.IsNullOrEmpty(file))
            {
                if (isappend)
                {
                    File.AppendAllText(file, OutputId);
                }
                else
                {
                    File.WriteAllText(file, OutputId);
                }
            }
        }

        private static string ReplaceIndex(string place, int length, int i, string unit)
        {
            string s = i.ToString();
            string idx = s;
            for (int j = 0; j < length - s.Length; j++)
            {
                idx = "0" + idx;
            }
            unit = unit.Replace(place, idx);
            return unit;
        }
    }
}
