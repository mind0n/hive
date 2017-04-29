using System.Windows.Forms;
using ULib.Winform.Controls;
using ULib.Winform;
namespace ScriptPlatform.Screens
{
	public partial class TextTreeScreen : ULib.Winform.Controls.ScreenRegion
	{
		public TextBox Textbox
		{
			get
			{
				return txt;
			}
		}
		public TreeView TreeView
		{
			get
			{
				return tv;
			}
		}
		public SplitterPanel CommandRegion
		{
			get
			{
				return spRight.Panel1;
			}
		}
		public TextTreeScreen()
		{
			InitializeComponent();
		}
		public void EmbedInto(Control control)
		{
			int count = control.Controls.Count;
			for (int i = 0; i < count; i++)
			{
				Control ctrl = control.Controls[0];
				control.Controls.RemoveAt(0);
				CommandRegion.Embed(ctrl, EmbedType.Top);
			}
				
			control.Controls.Clear();
			control.Controls.Add(this);
			this.Dock = DockStyle.Fill;
			this.Show();
		}
	}
}
