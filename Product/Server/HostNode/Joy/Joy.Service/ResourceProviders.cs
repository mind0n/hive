using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Joy.Core;
using System.Reflection;

namespace Joy.Service
{
    public abstract class ResourceProvider
    {
        public virtual string Joiner
        {
            get
            {
                return "/";
            }
        }
        protected InstanceSettings settings;
        
        public ResourceProvider(InstanceSettings insSettings)
        {
            settings = insSettings;
        }

        public abstract Stream LoadStream(string path, string type = null);

        public virtual string LoadContent(string path, string type = null)
        {
            using (var s = LoadStream(path, type))
            {
                if (s == null)
                {
                    return string.Empty;
                }
                using (var sr = new StreamReader(s))
                {
                    var r = sr.ReadToEnd();
                    return r;
                }

            }
        }
    }

    public class AssemblyResourceProvider : ResourceProvider
    {
        public override string Joiner
        {
            get
            {
                return ".";
            }
        }
        public AssemblyResourceProvider(InstanceSettings settings) : base(settings) { }
        public override Stream LoadStream(string path, string type = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty.Stream();
            }
            var asm = Assembly.GetExecutingAssembly();
            if (!string.IsNullOrEmpty(type))
            {
                var t = type.RetrieveType();
                if (t != null)
                {
                    asm = Assembly.GetAssembly(t);
                }
            }
            return asm.GetManifestResourceStream(path);
        }
    }

    public class FileContentProvider : ResourceProvider
    {
        public override string Joiner
        {
            get
            {
                return "\\";
            }
        }
        public FileContentProvider(InstanceSettings settings) : base(settings) { }
        public override Stream LoadStream(string path, string type = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                return Stream.Null;
            }
            if (path.IndexOf("..") >= 0 || path.IndexOf(":") >= 0)
            {
                return Stream.Null;
            }

            var vf = path.PathMap(settings.BaseDir);
            var cnt = File.OpenRead(vf);
            return cnt;
        }

    }
}
