using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using ULib.Winform.Exceptions;
using ULib.Winform.Controls;

namespace ULib.Winform
{
	public enum EmbedType
	{
		Top = 8,
		Right = 6,
		Bottom = 2,
		Left = 4,
		Fill = 5,
		None = 0
	}
	public class EmbedableTag : Dictionary<string, object>
	{
		public Control Parent;
		public Control ContentRegion;
		public List<Control> EmbededControls = new List<Control>();
		public List<Control> NewlyEmbededControls = new List<Control>();
		public object Value
		{
			get
			{
				if (ContainsKey("value"))
				{
					return this["value"];
				}
				return null;
			}
		}
		public EmbedableTag() { }
		public static EmbedableTag GetInstance(Control parent)
		{
			EmbedableTag tag;
			if (parent.Tag == null)
			{
				tag = new EmbedableTag();
				tag.ContentRegion = parent;
			}
			else if (parent.Tag is EmbedableTag)
			{
				tag = parent.Tag as EmbedableTag;
			}
			else
			{
				tag = new EmbedableTag();
				tag["value"] = parent.Tag;
				tag.ContentRegion = parent;
			}
			parent.Tag = tag;
			return tag;
		}
	}
	public delegate void EmbededCallback(Control control);
	public static class Extensions
	{
		public static void BeginEmbed(this Control control)
		{
			EmbedableTag tag = EmbedableTag.GetInstance(control);
			if (tag.NewlyEmbededControls.Count > 0)
			{
				tag.EmbededControls.AddRange(tag.NewlyEmbededControls);
				tag.NewlyEmbededControls.Clear();
			}
		}
		public static void EndEmbed(this Control control)
		{
			EmbedableTag tag = EmbedableTag.GetInstance(control);
			if (tag == null)
			{
				return;
			}
			foreach (Control item in tag.NewlyEmbededControls)
			{
				try
				{
					MethodInfo info = item.GetType().GetMethod("Embeded");
					if (info != null)
					{
						info.Invoke(item, new object[] { tag });
					}
				}
				catch (Exception e)
				{
					ExceptionHandler.Handle(e);
				}
			}
		}
		public static void Embed(this Control parent, Control child, EmbedType type, EmbededCallback embededCallback = null)
		{
			EmbedInto(child, parent, type, embededCallback);
		}
		public static void EmbedInto(this Control child, Control parent, bool isFillContainer)
		{
			if (child != null && parent != null)
			{
				parent.Controls.Clear();
				parent.Controls.Add(child);
				if (isFillContainer)
				{
					child.Dock = DockStyle.Fill;
				}
				if (child is Form)
				{
					child.Show();
				}
			}
		}
		public static void EmbedInto(this Control child, Control parent, EmbedType type, EmbededCallback embededCallback, bool isFixed = false, bool isSizable = false)
		{
			if (child != null && parent != null)
			{
				EmbedableTag tag = EmbedableTag.GetInstance(parent);
				tag.NewlyEmbededControls.Add(child);
				tag.Parent = parent;
				Control container = tag.ContentRegion;

				if (type == EmbedType.Fill || type == EmbedType.None)
				{
					child.EmbedInto(container, type == EmbedType.Fill);
					return;
				}


				#region Generate SplitContainer according to the EmbedType
				SplitContainer splitter;
				SplitterPanel fillPanel;
				SplitterPanel contentPanel;
				GenerateSplitter(parent, type, tag, out splitter, out fillPanel, out contentPanel, isSizable);
				#endregion
				if (splitter != null)
				{
					splitter.IsSplitterFixed = isFixed;
				}
				fillPanel.Controls.Add(child);
				container.Controls.Add(splitter);
				splitter.Dock = DockStyle.Fill;
				tag.ContentRegion = contentPanel;

				#region Adjust fill panel size
				AdjustFillPanel(child, type, splitter);
				#endregion
				if (child is ScreenRegion)
				{
					child.Show();
				}
				if (embededCallback != null)
				{
					embededCallback(child);
				}
			}
		}

		private static void AdjustFillPanel(Control child, EmbedType type, SplitContainer splitter)
		{
			if (type == EmbedType.Left)
			{
				splitter.SplitterDistance = child.Width;
				child.Height = splitter.Height;
			}
			else if (type == EmbedType.Right)
			{
				splitter.SplitterDistance = splitter.Width - child.Width;
				child.Height = splitter.Height;
			}
			else if (type == EmbedType.Top)
			{
				splitter.SplitterDistance = child.Height;
				child.Width = splitter.Width;
			}
			else if (type == EmbedType.Bottom)
			{
				splitter.SplitterDistance = splitter.Height - child.Height;
				child.Width = splitter.Width;
			}
			if (type != EmbedType.None)
			{
				child.Dock = DockStyle.Fill;
			}
		}

		private static void GenerateSplitter(Control target, EmbedType type, EmbedableTag tag, out SplitContainer splitter, out SplitterPanel fillPanel, out SplitterPanel contentPanel, bool isSizable)
		{
			splitter = new SplitContainer();
			fillPanel = null;

			if (type == EmbedType.Left || type == EmbedType.Right)
			{
				splitter.Orientation = Orientation.Vertical;
				if (type == EmbedType.Left)
				{
					fillPanel = splitter.Panel1;
					contentPanel = splitter.Panel2;
					if (!isSizable)
						splitter.FixedPanel = FixedPanel.Panel1;
				}
				else
				{
					fillPanel = splitter.Panel2;
					contentPanel = splitter.Panel1;
					if (!isSizable)
						splitter.FixedPanel = FixedPanel.Panel2;
				}
			}
			else
			{
				splitter.Orientation = Orientation.Horizontal;
				if (type == EmbedType.Top)
				{
					fillPanel = splitter.Panel1;
					contentPanel = splitter.Panel2;
					if (!isSizable)
						splitter.FixedPanel = FixedPanel.Panel1;
				}
				else
				{
					fillPanel = splitter.Panel2;
					contentPanel = splitter.Panel1;
					if (!isSizable)
						splitter.FixedPanel = FixedPanel.Panel2;
				}
			}
			if (tag != null && tag.ContentRegion != null && tag.ContentRegion.Controls.Count > 0)// target.Controls.Count > 0)
			{
				foreach (Control item in target.Controls)
				{
					target.Controls.Remove(item);
					
					contentPanel.Controls.Add(item);
				}
			}
			tag.ContentRegion = contentPanel;
		}
	}
}
