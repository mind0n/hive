using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.Controls;
using System.Windows.Forms;
using ULib.DataSchema;
using ULib.Executing.Commands.Common;

namespace ULib.Executing
{
	public class ParameterAttribute : Attribute
	{
		public bool IsHidden { get; set; }
		public bool IsParsable { get; set; }
		public bool IsRequired { get; set; }
		public bool IsReadonly { get; set; }
		public bool Validate(SettingTag tag)
		{
			object value = tag.Value;
			if (IsRequired)
			{
				if (value == null)
				{
					return false;
				}
				if (value is string)
				{
					if (string.IsNullOrEmpty(value.ToString()))
					{
						return false;
					}
					else
					{
						if (tag.EditorType == 0 && tag.Target != null && tag.Target is SetCommand)
						{
							SetCommand cmd = (SetCommand)tag.Target;
							if (cmd.Id.EndsWith("path", StringComparison.OrdinalIgnoreCase) || cmd.Id.EndsWith("dir", StringComparison.OrdinalIgnoreCase) || cmd.Id.EndsWith("directory", StringComparison.OrdinalIgnoreCase))
							{
								
								if (!cmd.Value.ToString().EndsWith("\\"))
								{
									cmd.Value = cmd.Value.ToString() + "\\";
								}
							}
						}
						return true;
					}
				}
			}
			return true;
		}
	}

	public class CommandAttribute : Attribute
	{
		public bool IsSkipAfterStart { get; set; }
		public bool IsPreviewRun { get; set; }
		public bool IsConfigurable { get; set; }
		public bool IsExecutable { get; set; }
        public bool IsLoadRun { get; set; }
		public bool IsConfigRun { get; set; }

		public CommandAttribute()
		{
			IsExecutable = true;
		}
	}
}
