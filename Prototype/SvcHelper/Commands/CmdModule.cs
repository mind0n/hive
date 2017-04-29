using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public class CmdModule
    {
        public CmdModule(dynamic settings)
        {
            var pi = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + "input.bat");
            pi.RedirectStandardOutput = true;
            pi.RedirectStandardError = true;
            pi.UseShellExecute = false;
            var p = Process.Start(pi);
            if (p != null)
            {
                p.WaitForExit();
                var f = AppDomain.CurrentDomain.BaseDirectory + "output.txt";
                var s = p.StandardOutput.ReadToEnd();
                var e = p.StandardError.ReadToEnd();
                File.WriteAllText(f, s);
                File.AppendAllText(f, e);
            }
        }
    }
}
