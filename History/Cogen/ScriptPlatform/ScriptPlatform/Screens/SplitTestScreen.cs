
using System.Windows.Forms;
namespace Platform.Screens
{
	public partial class SplitTestScreen : Screen
	{
		public TextBox ScriptBox
		{
			get
			{
				return tScript;
			}
		}
		public TextBox ResultBox
		{
			get
			{
				return tResult;
			}
		}
		public Panel MidPanel
		{
			get
			{
				return sp2.Panel1;
			}
		}
		public SplitTestScreen()
		{
			InitializeComponent();
			Load += new System.EventHandler(SplitTestScreen_Load);
		}

		void SplitTestScreen_Load(object sender, System.EventArgs e)
		{
			
		}
		public override void EmbedInto(System.Windows.Forms.Control target)
		{
			foreach (Control control in target.Controls)
			{
				target.Controls.Remove(control);
				MidPanel.Controls.Add(control);
				control.Dock = DockStyle.Top;
			}
			base.EmbedInto(target);
		}
	}
}
