
using ULib.Winform.Controls;
namespace ScriptPlatform.Screens
{
	public partial class ScannerScreen : ScreenRegion
	{
		TextTreeScreen tts = new TextTreeScreen();
		public ScannerScreen()
		{
			InitializeComponent();
			Load += new System.EventHandler(ScannerScreen_Load);
		}

		void ScannerScreen_Load(object sender, System.EventArgs e)
		{
			tts.EmbedInto(this);
		}

		private void bScan_Click(object sender, System.EventArgs e)
		{

		}
	}
}
