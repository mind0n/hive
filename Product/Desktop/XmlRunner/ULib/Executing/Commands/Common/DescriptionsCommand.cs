using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ULib.Executing.Commands.Common
{
	[Command(IsPreviewRun = false, IsConfigurable = false, IsExecutable = false, IsLoadRun = false)]
	public class DescriptionsCommand : UICommandNode
	{
		private string content { get; set; }
		public string Read()
		{
			return content;
		}
		public void Set(string descriptions)
		{
			content = descriptions;
		}
		protected override string GenerateXml()
		{
			return content;
		}
	}
}
