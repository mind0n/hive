using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Joy.Web.Mvc.IO
{
    public class ActionAttribute : Attribute
    {
        public ActionOutputType OutputType = ActionOutputType.Raw;
        public string ContentType = "text/html";
        public bool IsNoSplit;
    }
    public enum ActionOutputType
    {
        Raw,
        Json
    }
}