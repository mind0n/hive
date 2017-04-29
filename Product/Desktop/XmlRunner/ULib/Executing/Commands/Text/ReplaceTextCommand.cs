using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using ULib.Output;
using System.IO;

namespace ULib.Executing.Commands.Text
{
    public class ReplaceTextCommand : CommandNode
    {
		[ParameterAttribute(IsRequired = true)]
		public string TargetString;
		[ParameterAttribute(IsRequired = true)]
		public string ResultString;
		public string ResultId;
		public string SaveId;
		public string ReadId;
		public string TargetId;
		public string TargetFile;

        public override string Name
        {
            get
            {
				return string.Format("Replace text {0} {1} in {2}", TargetString, string.IsNullOrEmpty(ResultString) ? string.Empty : "for" + ResultString, TargetFile);
            }
        }

        public override void Run(CommandResult rlt, bool isPreview = false)
        {
            OutputHandler.Handle(Name);
            string content = string.Empty;
            if (!string.IsNullOrEmpty(TargetFile))
            {
                content = File.ReadAllText(TargetFile);
            }
            else if (!string.IsNullOrEmpty(ReadId))
            {
                content = Executor.Instance.GetString(ReadId);
            }
            else
            {
                content = Executor.Instance.GetString(SaveId);
            }
            if (!string.IsNullOrEmpty(TargetId))
            {
                TargetString = Executor.Instance.GetString(TargetId);
            }
            if (!string.IsNullOrEmpty(ResultId))
            {
                content = content.Replace(TargetString, Executor.Instance.GetString(ResultId));
            }
            else if (!string.IsNullOrEmpty(ResultString))
            {
                content = content.Replace(TargetString, ResultString);
            }
            if (!string.IsNullOrEmpty(TargetFile))
            {
                File.WriteAllText(TargetFile, content);
            }
            if (!string.IsNullOrEmpty(SaveId))
            {
                Executor.Instance.SetVar(SaveId, false, content);
            }
        }

        public override CommandNode Clone()
        {
			return new ReplaceTextCommand
			{
				Id = Id,
				TargetString = TargetString,
				ResultString = ResultString,
				ResultId = ResultId,
				SaveId = SaveId,
				ReadId = ReadId,
				TargetFile = TargetFile
			};
        }
    }
}
