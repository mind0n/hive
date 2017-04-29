using ULib.Forms;
using Utilities.Settings;
using System.IO;
using System;
using System.Collections.Generic;
using ULib;
using ULib.Exceptions;
using System.Diagnostics;
using ULib.DataSchema;

namespace Utilities.Components.Deployment
{
    public partial class MsiSeekerForm : DockForm
    {
        private DeploySettings settings = new DeploySettings();
        private DeploySetting setting
        {
            get
            {
                if (settings.ContainsKey(hcFind.Text))
                {
                    return settings[hcFind.Text];
                }
                else
                {
                    foreach (KeyValuePair<string, DeploySetting> i in settings)
                    {
                        if (hcFind.Text.IndexOf(i.Key, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            return i.Value;
                        }
                    }
                }
                return null;
            }
        }
        public MsiSeekerForm()
        {
            InitializeComponent();
            Load += new System.EventHandler(MsiSeekerFormLoad);
            KeyUp += new System.Windows.Forms.KeyEventHandler(MsiSeekerForm_KeyUp);
            FormClosing += new System.Windows.Forms.FormClosingEventHandler(MsiSeekerForm_FormClosing);
            if (!AppSettings.Instance.TryLoad<DeploySettings>(AppSettings.KeyDeploySettings))
            {
                settings["p1-main"] = new DeploySetting();
                settings["3.2-main"] = new DeploySetting { Ip_Postfix = "17.6", Main_Version = "3.2.2." };
                settings["3.2-release"] = settings["3.2-main"];
                settings["3.2.1-release"] = new DeploySetting { Ip_Postfix = "17.6", Main_Version = "3.2.1." };
                settings["3.1.2-main"] = new DeploySetting { Ip_Postfix = "17.8", Server_Branch = "3.1-main", Main_Version = "3.1" };
                settings["3.1.2_ps_release"] = settings["3.1.2-main"];
                settings["3.1.7_ps_release"] = settings["3.1.2-main"];
                settings["3.1.8_ps_release"] = settings["3.1.2-main"];
                AppSettings.Instance.Initialize(AppSettings.KeyDeploySettings, settings);
                AppSettings.Instance.Save(AppSettings.KeyDeploySettings);
            }
            else
            {
                settings = AppSettings.Instance.Read<DeploySettings>(AppSettings.KeyDeploySettings);
            }
        }

        void MsiSeekerForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F12)
            {
                AppSettings.Instance.Erase(AppSettings.KeyDeploySettings);
            }
        }

        void MsiSeekerForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            AppSettings.Instance.Save(AppSettings.KeyDeploySettings);
        }

        private ConfigScreen cfg;
        private void MsiSeekerFormLoad(object sender, System.EventArgs e)
        {
            hcFind.SelectedIndex = hcFind.Items.Count - 1;
            cfg = new ConfigScreen();
            cfg.Title = "Deployment Settings";
            cfg.Generate(setting);
            cfg.EmbedInto(pnInstall);
        }

        private void bFind_Click(object sender, System.EventArgs e)
        {
            List<ListItem> list = ListDirectories();
            lst.DisplayMember = "Name";
            lst.ScrollAlwaysVisible = true;
            lst.DataSource = list;
        }

        private List<ListItem> ListDirectories()
        {
            string[] dirs = Directory.GetDirectories(setting.Msi_Base_Path);
            DateTime latest = DateTime.Now - TimeSpan.FromDays(20);
            List<ListItem> list = new List<ListItem>();
            foreach (string dir in dirs)
            {
                DirectoryInfo info = new DirectoryInfo(dir);
                DateTime time = info.CreationTime;
                if (dir.IndexOf(setting.Main_Version, StringComparison.OrdinalIgnoreCase) >= 0 && time > latest)
                {
                    string[] files = Directory.GetFiles(dir, "*.msi");
                    string subdir;
                    if (files == null || files.Length < 1)
                    {
                        string[] subdirs = Directory.GetDirectories(dir);
                        if (subdirs != null && subdirs.Length > 0)
                        {
                            subdir = subdirs[0];
                        }
                        else
                        {
							continue;
                        }
                    }
                    else
                    {
                        subdir = dir;
                    }
                    ListItem newItem = new ListItem { FullPath = subdir };
                    list.Insert(0, newItem);
                    latest = time;
                    files = Directory.GetFiles(subdir);
                    foreach (string file in files)
                    {
                        string name = file.PathLastName();
                        if (name.IndexOf("cadclient", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            newItem.MsiName["cadclient"] = file;
                        }
                        else if (name.IndexOf("mobileclient", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            newItem.MsiName["mobileclient"] = file;
                        }
                    }
                }
            }
            return list;
        }

        private void bInstall_Click(object sender, EventArgs e)
        {
            try
            {
                ListItem i = GetSelectedItem();
                if (i != null)
                {
                    tabs.SelectedIndex = 1;
                    string cmd = string.Concat(setting.Script_Base, "DeployClient.bat ");
                    string arg = string.Concat(setting.Server_Branch, " ", setting.Ip_Postfix, " ", setting.Client_Type, " ", i.MsiName[setting.Client_Type.ToLower()], " ", i.MsiName[setting.Client_Type.ToLower()].PathLastName());
                    string fullargs = string.Concat("/c ", cmd, " ", arg);
                    output.WriteMsg("cmd " + fullargs);
                    Process.Start(new ProcessStartInfo("cmd.exe", fullargs));
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        private ListItem GetSelectedItem()
        {
            if (lst.SelectedIndex >= 0)
            {
                return lst.Items[lst.SelectedIndex] as ListItem;
            }
            else if (lst.Items.Count > 0)
            {
                return lst.Items[0] as ListItem;
            }
            return null;
        }

        private void lst_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void hcFind_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cfg != null && setting != null)
            {
                cfg.Generate(setting);
            }
        }
    }

    public class ListItem
    {
        public Dict<string, string> MsiName = new Dict<string, string>();
        public string GetFullname(string client)
        {
            return string.Concat(FullPath, MsiName[client.ToLower()]);
        }
        public string FullPath;
        public string Path
        {
            get
            {
                if (!string.IsNullOrEmpty(FullPath))
                {
                    string[] list = FullPath.Split('\\');
                    if (list.Length > 0)
                    {
                        string rlt = string.Join("\\", list, 0, list.Length - 1);
                        if (!rlt.EndsWith("\\"))
                        {
                            rlt += "\\";
                        }
                        return rlt;
                    }
                }
                return null;
            }
        }
        public string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(FullPath))
                {
                    string[] list = FullPath.Split('\\');
                    if (list.Length > 0)
                    {
                        return list[list.Length - 1];
                    }
                }
                return null;
            }
        }
    }
}
