using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.Controls;
using System.Diagnostics;

namespace ULib.Debugging
{
    public class D
    {
        public static void WriteMsg(string msg, params string[] args)
        {
            Debug.WriteLine(string.Format(msg, args));
        }
    }
}
