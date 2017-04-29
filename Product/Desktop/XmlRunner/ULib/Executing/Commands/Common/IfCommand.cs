using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib;
using ULib.DataSchema;

namespace ULib.Executing.Commands.Common
{
	[Command(IsPreviewRun = true, IsConfigurable = true)]
	public class IfCommand : CommandNode
    {
		[ParameterAttribute(IsRequired = true)]
		public string IsTrue;

		public bool IsNot
		{
			get
			{
				if (IsTrue != null)
				{
					return IsTrue.StartsWith("!");
				}
				else
				{
					return false;
				}
			}
		}
        private ExecuteParameter Condition
        {
            get
            {
				string name = IsTrue;
				if (name == null)
				{
					return null;
				}
				if (IsNot)
				{
					name = name.Substring(1);
				}
				return Executor.Instance.GetVar(name);
            }
        }

        public override string Name
        {
            get
            {
				return string.Format("If condition {0} {1} true", Condition == null ? string.Empty : Condition.Name, IsNot ? "is not" : "is");
            }
        }
		public override void Run(CommandResult rlt, bool isPreview = false)
        {
            if (Condition == null)
            {
				rlt.BoolResult = IsNot ? true : false;
            }
            else
            {
				rlt.BoolResult = IsNot ? !Condition.IsTrue : Condition.IsTrue;
            }
        }
        public override CommandNode Clone()
        {
            return new IfCommand { IsTrue = IsTrue, Id=Id };
        }
    }
}
