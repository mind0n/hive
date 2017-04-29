using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using System.Xml.Linq;
using System.IO;
using ULib.Output;

namespace ULib.Executing.Commands.Common
{

    [Command(IsExecutable = false, IsPreviewRun = false, IsConfigurable = false, IsLoadRun = true)]
    public class ImportCommand : CommandNode
    {
        public string Src;
        public override XElement XmlLoaded()
        {
            string src = Src;
            if (!string.IsNullOrEmpty(Src))
            {
                if (!src.IsAbsolute())
                {
                    src = Src.PathMakeAbsolute();
                }
                if (!File.Exists(src))
                {
                    throw new ArgumentException(string.Format("File missing error - Import {0}", true, src));
                }
                string content = File.ReadAllText(src);
                XDocument doc = XDocument.Parse(content);
                XElement el = doc.Root;
                return el;
            }
            return null;
        }
    }
}
