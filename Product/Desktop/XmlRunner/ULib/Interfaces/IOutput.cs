using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ULib.Controls
{
    public interface IOutput
    {
        void WriteMsg(string msg, bool hideTimestamp = false, string color = "black", bool breakLine = true, params string[] args);
    }
}
