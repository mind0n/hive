using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ULib;
using ULib.Forms;
using Utilities.Components.Launchers.Settings;
using System.Threading;
using System.Diagnostics;

namespace Utilities.Components.Launchers
{
    public partial class P1Launcher : EmbededForm
    {
        private string CCSolutionFile
        {
            get
            {
                return string.Concat(LauncherSettings.Instance.TFSRoot, cmBranch.Text, "\\", LauncherSettings.Instance.ProvSolutionPath, LauncherSettings.Instance.ProvFileName);
            }
        }
        private string ProvSolutionFile
        {
            get
            {
                return string.Concat(LauncherSettings.Instance.TFSRoot, cmBranch.Text, "\\", LauncherSettings.Instance.ProvSolutionPath, LauncherSettings.Instance.ProvFileName);
            }
        }
        private string VsCmd
        {
            get
            {
                return string.Concat(LauncherSettings.Instance.VsRoot, "devenv.exe ");
            }
        }
       
        public P1Launcher()
        {
            InitializeComponent();
            Text = "CAD Clients Launcher";
            InitializeConfig();
            Load += new EventHandler(CADClientsLauncher_Load);
            FormClosing += new FormClosingEventHandler(P1Launcher_FormClosing);
        }

        private static void InitializeConfig()
        {
            LauncherSettings.Instance.TFSRoot = @"C:\__TFSCode\";
            LauncherSettings.Instance.ServerRoot = @"C:\Servers";
            LauncherSettings.Instance.VsRoot = @"C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\";
            LauncherSettings.Instance.Branches = new List<string> { "P1-main", "3.2-main", "3.1.2-main", "3.1.2_ps_release" };
        }

        void P1Launcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            LauncherSettings.Update();
        }

        void CADClientsLauncher_Load(object sender, EventArgs e)
        {
            cmBranch.DataSource = LauncherSettings.Instance.Branches;
            cmBranch.SelectedIndex = 1;
            lvMain.LargeImageList = imgs;
            lvMain.SmallImageList = imgs;
            lvMain.DoubleClick+=new EventHandler(lvMain_DoubleClick);
            IconSetting.Add(lvMain, new IconSetting { ImgIndex = 2, Text = "Provisioning Console Solution", Command = VsCmd, Arguments = LauncherSettings.ModuleFilename("prov") });
            IconSetting.Add(lvMain, new IconSetting { ImgIndex = 2, Text = "CAD Client Solution", Command = VsCmd, Arguments = LauncherSettings.ModuleFilename("cc") });
            IconSetting.Add(lvMain, new IconSetting { ImgIndex = 2, Text = "Common Service Solution", Command = VsCmd, Arguments = LauncherSettings.ModuleFilename("cs") });
        }

        void lvMain_DoubleClick(object sender, EventArgs e)
        {
            Execute();
        }

        private void Execute()
        {
            if (lvMain.SelectedItems.Count < 1)
            {
                return;
            }
            ListViewItem i = lvMain.SelectedItems[0];
            if (i != null && !string.IsNullOrEmpty(i.ToolTipText))
            {
                string cmd = i.ToolTipText;
                string arg = i.Tag != null ? string.Format(i.Tag.ToString(), cmBranch.Text) : string.Empty;
                ExecuteCmd(cmd, arg);
            }
        }

        private static void ExecuteCmd(string cmd, string arg)
        {
            Thread th = new Thread(delegate()
            {
                Process.Start(cmd, arg);
            });
            th.IsBackground = true;
            th.Start();
        }

        private void bConfig_Click(object sender, EventArgs e)
        {
            EditForm.Popup(LauncherSettings.Instance.ToXml(), null, new Action<string>(delegate(string s)
            {
                LauncherSettings.Save(s);
            }));
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            LauncherSettings.Reset();
            InitializeConfig();
        }
    }
}
