using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ULib.Controls
{
	public class AutoLabelPanel : Panel
	{
		public int TopMargin { get; set; }
		public int LeftMargin { get; set; }
		List<Label> list = new List<Label>();
		public AutoLabelPanel()
		{
			TopMargin = LeftMargin = 2;
			AutoScroll = true;
			AutoScrollMinSize = new System.Drawing.Size(1, 1);
		}
		public void Clear()
		{
			list.Clear();
			Controls.Clear();
		}
		public void AddRange(List<AutoLabelItem> list)
		{
			foreach (AutoLabelItem i in list)
			{
				Add(i);
			}
		}
		public void Add(string text)
		{
			Add(new AutoLabelItem { Text = text });
		}
		public void Add(AutoLabelItem item = null)
		{
			Label lb = new Label();
			lb.AutoSize = true;
			lb.Text = item.Text;
			if (item == null)
			{
				item = new AutoLabelItem();
			}
			if (item.EmSize <= 0)
			{
				item.EmSize = 11;
			}
			if (string.IsNullOrEmpty(item.FamilyName))
			{
				item.FamilyName = "Microsoft Sans Serif";
			}
			lb.Font = new System.Drawing.Font(item.FamilyName, item.EmSize, item.DisplayStyle);
			lb.ForeColor = Color.FromName(item.Color);
			list.Add(lb);
		}
		public void Display()
		{
			int top = TopMargin;
			int left = LeftMargin;
			Controls.Clear();
			foreach (Label i in list)
			{
				Controls.Add(i);
				i.Left = left;
				i.Top = top;
				top += i.Height + TopMargin;
			}
		}
	}

	public class AutoLabelItem
	{
		public string Text;
		public string FamilyName = "Microsoft Sans Serif";
		public string Color = "Black";
		public float EmSize = 11;
		public System.Drawing.FontStyle DisplayStyle= System.Drawing.FontStyle.Regular;
	}
}
