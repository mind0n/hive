using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using ULib.Output;

namespace ULib.Executing.Commands.Common
{
	[Command(IsPreviewRun = true, IsConfigurable = true)]
	public class SetCommand : CommandNode
    {
		[ParameterAttribute(IsRequired = true)]
		public string Content;
		public string Argument;
		public string ContentId;
		public string TargetId;
		public bool IsReference;

		public override string Name
		{
			get
			{
				return string.Format("Set {0} {1} with value {2}", IsCondition ? "condition" : "parameter", Id, Content);
			}
			set
			{
				base.Name = value;
			}
		}
		public bool IsCondition
		{
			get
			{
				if (string.Equals(Content, "true", StringComparison.OrdinalIgnoreCase) || string.Equals(Content, "false", StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
				else if (string.IsNullOrEmpty(Content)
					&& string.IsNullOrEmpty(ContentId)
					&& string.IsNullOrEmpty(TargetId))
				{
					Content = "true";
					return true;
				}
				else
				{
					return false;
				}
			}
		}

        public SetCommand()
            : base()
        {
        }
        public bool BoolValue
        {
            get
            {
                if (IsCondition)
                {
                    return string.Equals(Content, "true", StringComparison.OrdinalIgnoreCase);
                }
                return false;
            }
        }
		public override void Run(CommandResult rlt, bool isPreview=false)
		{

			if (!string.IsNullOrEmpty(Id))
			{
				if (!IsReference)
				{
					Executor.Instance.SetVar(Id, IsCondition, this.Clone());
				}
				else
				{
					Executor.Instance.SetVar(Id, IsCondition, this);
				}
			}
			if (!string.IsNullOrEmpty(ContentId))
			{
				string targetId = TargetId;
				if (string.IsNullOrEmpty(targetId))
				{
					targetId = this.Id;
				}
				if (!string.IsNullOrEmpty(targetId))
				{
					object o = Executor.Instance.GetObject(ContentId);
					if (o is string)
					{
						string s = (string)o;
						Executor.Instance.SetVar(targetId, IsCondition, s);
					}
					else if (o is CommandNode)
					{
						CommandNode c = (CommandNode)o;
						if (c != null)
						{
							c.Visible = false;
							if (!IsReference)
							{
								CommandNode cloned = c.Clone();
								cloned.Id = null;
								cloned.Visible = this.Visible;
								Executor.Instance.SetVar(TargetId, IsCondition, cloned);
							}
							else
							{
								c.Visible = Visible;
								Executor.Instance.SetVar(TargetId, IsCondition, c);
							}
						}
					}
				}
			}
		}
    }
}
