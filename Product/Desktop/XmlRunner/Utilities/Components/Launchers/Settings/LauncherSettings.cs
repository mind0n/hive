using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ULib;
using System.IO;
using ULib.Exceptions;

namespace Utilities.Components.Launchers.Settings
{
    public class LauncherSettings
    {
        private static LauncherSettings instance;
        static LauncherSettings()
        {
            instance = new LauncherSettings();
            Load();
        }
        protected LauncherSettings()
        {
            Modules.Clear();
            Modules.Add(new ModuleSetting { Name = "prov", Path = @"Product\CommonServices\Development\Provisioning\Software\Build\", Filename = "ProvisioningConsole.sln" });
            Modules.Add(new ModuleSetting { Name = "cc", Path = @"Product\CAD\Development\Client\Software\Build\", Filename = "PSWGS.Client.sln" });
            Modules.Add(new ModuleSetting { Name = "cs", Path = @"Product\CommonServices\Development\Enterprise\Software\Build\", Filename = "PSWGS.Enterprise.sln" });
        }
        public static LauncherSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LauncherSettings();
                    instance.ProvSolutionPath = Get("prov").Path;
                    instance.CCSolutionPath = Get("cc").Path;
                    instance.ProvFileName = Get("prov").Filename;
                    instance.CCFileName = Get("cc").Filename;
                }
                return LauncherSettings.instance;
            }
        }

        public static void Reset()
        {
            instance = new LauncherSettings();
            if (File.Exists(CfgFile))
            {
                File.Delete(CfgFile);
            }
            Update();
        }

        public static void Update(string xml = null)
        {
            if (xml != null)
            {
                try
                {
                    instance.Modules.Clear();
                    instance = xml.FromXml<LauncherSettings>();
                }
                catch (Exception e)
                {
                    ExceptionHandler.Handle(e);
                    if (instance == null)
                    {
                        instance = new LauncherSettings();
                    }
                    if (File.Exists(CfgFile))
                    {
                        File.Delete(CfgFile);
                    }
                    return;
                }
            }
        }

        public static void Save(string xml)
        {
            File.WriteAllText(CfgFile, xml == null ? instance.ToXml() : xml);
        }

        public static void Load()
        {
            if (File.Exists(CfgFile))
            {
                string xml = File.ReadAllText(CfgFile);
                Update(xml);
            }
        }
        public static string ModuleFilename(string moduleName)
        {
            ModuleSetting m = Get(moduleName);
            return string.Concat(instance.TFSRoot, "{0}\\", m.Path, m.Filename);
        }
        public static ModuleSetting Get(string moduleName)
        {
            foreach (ModuleSetting i in Instance.Modules)
            {
                if (string.Equals(moduleName, i.Name, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return null;
        }

        private static string CfgFile
        {
            get
            {
                return string.Concat(AppDomain.CurrentDomain.BaseDirectory, CfgFileName);
            }
        }

        private static string CfgFileName = "Launcher.config";
        public List<string> Branches = new List<string>();
        public List<ModuleSetting> Modules = new List<ModuleSetting>();
        public string ProvSolutionPath;
        public string CCSolutionPath;
        public string ProvFileName;
        public string CCFileName;
        public string TFSRoot;
        public string ServerRoot;
        public string ServerNamePattern = "{0}server";
        public string VsRoot;
    }
    public class ModuleSetting
    {
        public string Name;
        public string Path;
        public string Filename;
    }
    public class IconSetting
    {
        public enum IconType
        {
            Command
        }
        public int ImgIndex = 0;
        public string Text = string.Empty;
        public string Command = string.Empty;
        public string Arguments = string.Empty;
        public IconType Type = IconType.Command;
        public static void Add(ListView view, IconSetting item)
        {
            if (view == null)
            {
                return;
            }
            ListViewItem i = new ListViewItem();
            i.Text = item.Text;
            i.ToolTipText = item.Command;
            i.ImageIndex = item.ImgIndex;
            i.Tag = item.Arguments;
            view.Items.Add(i);
        }

    }
}
