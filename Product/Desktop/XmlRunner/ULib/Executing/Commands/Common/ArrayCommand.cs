using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using ULib.Exceptions;

namespace ULib.Executing.Commands.Common
{
	[Command(IsPreviewRun=true, IsConfigurable=true)]
	public class ArrayCommand : CommandNode
	{
		private List<string> list = new List<string>();

		public int SelectedIndex;

		public string Content;
		
		public bool IsDistinct;

		public string Splitter;

		//public string Add;

		public string SelectedContent
		{
			get
			{
				if (SelectedIndex < 0 || SelectedIndex >= list.Count)
				{
					Run(new CommandResult());
				}
				if (SelectedIndex >= 0 && SelectedIndex < list.Count)
				{
					return list[SelectedIndex];
				}
				return string.Empty;
			}
		}
		
		public ArrayCommand()
		{
			Splitter = string.Empty;
			SelectedIndex = 0;
		}

		public override string Name
		{
			get
			{
				return string.Format("Initialize list {0}", Content);
			}
		}

        public override void Run(CommandResult rlt, bool isPreview = false)
        {
            if ((SelectedIndex < 0 || SelectedIndex > list.Count) && !string.IsNullOrEmpty(Content))
            {
                SelectedIndex = list.Count > 0 ? 0 : -1;
            }
            if (!string.IsNullOrEmpty(Content))
            {
                string[] splitted = Content.Split(new string[] { Splitter }, StringSplitOptions.None);
                foreach (string i in splitted)
                {
                    if (!IsDistinct || !list.Contains(i))
                    {
                        string real = Executor.Instance.ParseIdString(i);
                        list.Add(real);
                    }
                }
            }
        }

		public override string ToString()
		{
			if (list != null)
			{
				return string.Join(Splitter, list.ToArray());
			}
			return string.Empty;
		}

		public string[] GetList()
		{
			if (list.Count < 1 && !string.IsNullOrEmpty(Content))
			{
				list.AddRange(Content.Split(new string[] { Splitter }, StringSplitOptions.None));
			}
			return list.ToArray();
		}
	}
}
