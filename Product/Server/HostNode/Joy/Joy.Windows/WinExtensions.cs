using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Joy.Windows
{
    public static class WinExtensions
    {
        public static object Msg(this Control c, Action opt, bool async = false)
        {
            if (opt == null || c == null)
            {
                return null;
            }
            if (!async)
            {
                return c.Invoke(opt);
            }
            else
            {
                return c.BeginInvoke(opt);
            }
        }
    }
}
