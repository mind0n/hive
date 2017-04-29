using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Joy.Core
{
    public abstract class Cfg<T> where T:class, new()
    {
        public virtual T Instance
        {
            get
            {
                return instance;
            }
        }
        protected T instance;
        
        protected string Target;
        protected Action OnTargetChanged;
        public Cfg(string target = null)
        {
            Target = target;
            OnTargetChanged += Configer_OnTargetChanged;
            if (!string.IsNullOrEmpty(target))
            {
                instance = Load();
            }
        }

        void Configer_OnTargetChanged()
        {
            Load();
        }
        public virtual T Load()
        {
            if (!Exists())
            {
                instance = Activator.CreateInstance<T>();
                Save();
                return instance;
            }
            var s = LoadContent(Target);
            if (!string.IsNullOrEmpty(s))
            {
                var r = Deserialize(s);
                instance = r;
                return r;
            }
            return default(T);
        }
        public void Save(object t = null)
        {
            if (t != null)
            {
                instance = t as T;
            }
            if (instance == null)
            {
                instance = new T();
            }
            if (Target != null)
            {
                var s = Serialize();
                WriteContent(s);
            }
        }
        protected abstract void WriteContent(string content);
        protected abstract bool Exists();
        protected abstract string Serialize();
        protected abstract string LoadContent(string target);
        protected abstract T Deserialize(string s);
    }

    public class FileCfg<T> : Cfg<T> where T:class,new()
    {
        protected JavaScriptSerializer serializer = new JavaScriptSerializer();
        
        protected FileSystemWatcher watcher;
        
        public FileCfg(string target = null) : base(target)
        {
            try
            {
                if (target == null)
                {
                    target = string.Concat(AppDomain.CurrentDomain.BaseDirectory, typeof (T).Name, ".json");
                }
                else if (!target.IsAbsolutePath())
                {
                    target = AppDomain.CurrentDomain.BaseDirectory + target;
                }
                Target = target;
                Load();
                if (File.Exists(target))
                {
                    watcher = new FileSystemWatcher(target.PathWithoutFilename(), "*.json");
                    watcher.Changed += watcher_Changed;
                }
            }
            catch (Exception ex)
            {
                Error.Handle(ex);
            }
        }
        
        object locker = new object();
        
        void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            var w = sender as FileSystemWatcher;
            w.EnableRaisingEvents = false;
            lock (locker)
            {
                if (string.Equals(e.Name, Target.PathLastName()))
                {
                    OnTargetChanged();
                }
            }
            w.EnableRaisingEvents = true;
        }

        protected override bool Exists()
        {
            return File.Exists(Target);
        }

        protected override string Serialize()
        {
            var s = serializer.Serialize(instance);
            return s;
        }

        protected override void WriteContent(string content)
        {
            File.WriteAllText(Target, content);
        }

        protected override T Deserialize(string s)
        {
            var js = serializer;
            var r = js.Deserialize(s, typeof(T));
            return r as T;
        }

        protected override string LoadContent(string target)
        {
            return File.ReadAllText(target);
        }
    }
}
