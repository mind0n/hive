using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Fs.UI.Controls;
using OAWidgets.Widget;
using Fs.IO;
using System.IO;
using Fs.Xml;
using OAWidgets;
namespace OA
{
	public partial class MainForm : BasicWidget, PlatformAgent
	{
		public override string Id
		{
			get
			{
				return "c09466a4-fba3-495b-a180-0185ada58981";
			}
		}
		public MainForm()
		{
			BaseDir = AppDomain.CurrentDomain.BaseDirectory;
			InitializeComponent();
			ShowInTaskbar = true;
			BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "..\\Resources\\bg.gif");
			BackgroundImageLayout = ImageLayout.Tile;
			tvCaption.BackColor = Color.Transparent;
			
			pnTop.BackColor = Color.Transparent;
			pnTopContainer.BackColor = Color.Transparent;
			pnMiddle.BackColor = Color.Transparent;
			pnBottom.BackColor = Color.Transparent;

			cboView.ItemOnUpdate += new EventCombo.UpdateEventHandler(delegate(object i)
			{
				if (((string)i).Equals("Details"))
				{
					lvMain.View = View.Details;
				}
				else
				{
					lvMain.View = View.LargeIcon;
				}
			});
			cboView.Select(0);
			EventHandler(pnTop);
			DiskHelper.EnumDirectory(AppDomain.CurrentDomain.BaseDirectory, new DiskHelper.BoolDlgStringType(WidgetDirEnumCallback));
		}
		protected void InitWidget(string baseDir)
		{
			string IconName, IconSrc;
			XReader xr = new XReader(baseDir + "Widget.Config");
			IconName = xr["Widget"]["$Text"].Value;
			IconSrc = xr.Reset()["Widget"]["Img"]["$Src"].Value;

			EventListViewItem item = new EventListViewItem(IconName);
			item.OnItemLaunch += new EventListViewItem.MouseActionHandler(delegate(EventListViewItem oSender, MouseEventArgs oEvtArgs)
			{
				LaunchMailGenerator();
			});
			item.ImageIndex = 0;
			item.SubItems.Add("None");
			imgIcons.Images.Add(Image.FromFile(baseDir + IconSrc));
			lvMain.LargeImageList = imgIcons;
			lvMain.Columns.Add("Application Name", 150);
			lvMain.Columns.Add("Shortcut Key", 90);
			lvMain.Items.Add(item);

			EventTreeNode tn = new EventTreeNode();
			tn.MouseOnClick += new EventTreeNode.MouseActionHandler(delegate(EventTreeNode node, MouseEventArgs mEvt)
			{
				LaunchMailGenerator();
			});
			tn.Text = "Outlook Mail Generator";

			tvMain.Nodes.Add(tn);
			tvMain.NodeMouseClick += new TreeNodeMouseClickEventHandler(tvMain_NodeMouseClick);
		}
		protected bool WidgetDirEnumCallback(string path, DiskHelper.DirectoryItemType type)
		{
			string widgetConfigFile;
			if (type == DiskHelper.DirectoryItemType.Directory)
			{
				widgetConfigFile = path + "\\Widget.Config";
				if (File.Exists(widgetConfigFile))
				{
					InitWidget(path + "\\");
				}
			}
			return true;
		}
		void tvMain_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			((EventTreeNode)e.Node).MouseOnClick((EventTreeNode)e.Node, e);
		}

		void LaunchMailGenerator()
		{
			MailGenerator mg = new MailGenerator(AppDomain.CurrentDomain.BaseDirectory);
			mg.Launch(this);
		}

		private void btnCloseForm_Click(object sender, EventArgs e)
		{
			Close();
		}


		#region PlatformAgent Members

		public EventTabPage CreateNewTab(string tbName)
		{
			EventTabPage page = new EventTabPage(tbName);
			page.AddTo(tbMain);
			return page;
		}
		public ImageList GetListViewImageList()
		{
			return imgIcons;
		}
		#endregion
	}
}
