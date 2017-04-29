using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Joy.Startup
{
    public class Starter
    {
        static Dictionary<string, Container> cache = new Dictionary<string, Container>();
        static StarterSettings cfg;
        static Starter()
        {
            cfg = new StarterSettings();
        }
        internal void Start()
        {
            Enum(cfg.Instance.Modules, new Func<string, dynamic, bool>((name, c) =>
            {
                if (!c.IsDisabled)
                {
                    var asm = c.Assembly;
                    var container = new Container { Settings = c, AsmName = asm, IsDisabled = false, Module = c.Entry };
                    container.Setup(name);
                    cache[name] = container;
                }
                return false;
            }));
        }
        internal void Stop()
        {
            foreach (var i in cache)
            {
                i.Value.Unload();
            }
        }
        internal void Enum(dynamic target, Func<string, dynamic, bool> cb)
        {
            if (cb == null)
            {
                return;
            }
            foreach (var i in target)
            {
                cb(i.Key, i.Value);
            }
        }
    }
    [Serializable]
    public class StarterSettings
    {
        static List<string> dirs = new List<string>();
        static StarterSettings()
        {
            dirs.Add(AppDomain.CurrentDomain.BaseDirectory + "settings\\" + Environment.MachineName + "\\startup.json");
            dirs.Add(AppDomain.CurrentDomain.BaseDirectory + "settings\\startup.json");
            dirs.Add(AppDomain.CurrentDomain.BaseDirectory + "startup.json");
        }
        protected StarterModules instance;
        public StarterModules Instance
        {
            get
            {
                try
                {
                    if (instance == null)
                    {
                        var file = string.Empty;
                        foreach (var i in dirs)
                        {
                            if (File.Exists(i))
                            {
                                file = i;
                                break;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(file))
                        {
                            var cnt = File.ReadAllText(file);
                            var jss = new JavaScriptSerializer();
                            var o = jss.Deserialize<StarterModules>(cnt);
                            instance = o;
                        }
                    }
                    return instance;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    return null;
                }
            }
        }
    }
    [Serializable]
    public class StarterModules
    {
        public Dictionary<string, StarterModule> Modules = new Dictionary<string, StarterModule>();
        public StarterModules()
        {
            Modules["TestModule"] = new StarterModule { Assembly = "TestModuleA", Entry = "TestModuleA.ModuleEntry", Settings = new StarterModuleSettings() };
        }
    }
    [Serializable]
    public class StarterModule
    {
        public bool IsDisabled { get; set; }
        public string Assembly { get; set; }
        public string Entry { get; set; }
        public dynamic Settings { get; set; }
    }
    [Serializable]
    public class StarterModuleSettings
    {
        public string Test { get; set; }
        public StarterModuleSettings()
        {
            Test = "Success";
        }
    }
    [Serializable]
    public class Container
    {
        public Assembly Asm;
        public AppDomain Dom;
        public dynamic Settings;
        public string AsmName;
        public string Module;
        public bool IsDisabled;
        protected ManualResetEvent mre;
        public Container()
        {
            mre = new ManualResetEvent(false);
        }
        public void Setup(string k)
        {
            Thread th = new Thread(new ThreadStart(() =>
            {
                CreateModuleInternal(k);
                mre.WaitOne();
            }));
            th.IsBackground = false;
            th.Start();
        }
        private ModuleResult CreateModuleInternal(string k)
        {
            var rlt = new ModuleResult();
            try
            {
                var setup = new AppDomainSetup { ApplicationBase = AppDomain.CurrentDomain.BaseDirectory, ShadowCopyFiles = "true", ApplicationName = "JoyStartup", CachePath = AppDomain.CurrentDomain.BaseDirectory + "shadow\\" };
                var dom = AppDomain.CreateDomain(k, new Evidence(), setup);
                var f = AppDomain.CurrentDomain.BaseDirectory + "Modules\\" + AsmName + ".dll";
                if (File.Exists(f))
                {
                    dom.CreateInstanceFrom(f, Module, true, BindingFlags.Default, null, new object[] { Settings.Settings }, CultureInfo.InvariantCulture, null);
                }
                else
                {
                    rlt.Fail();
                }
                return rlt;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                rlt.Fail();
                return rlt;
            }
        }
        public void Unload()
        {
            try
            {
                mre.Set();
                Thread.Sleep(500);
                AppDomain.Unload(Dom);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }

    public class ModuleResult
    {
        public bool IsCreated { get; private set; }
        public void Fail()
        {
            IsCreated = false;
        }
    }
}