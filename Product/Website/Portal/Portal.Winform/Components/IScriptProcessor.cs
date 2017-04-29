using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portal.Winform.Components
{
    public interface IScriptProcessor
    {
        void OnLoad();
        void OnClose();
    }
}
