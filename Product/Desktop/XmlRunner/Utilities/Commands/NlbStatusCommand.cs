using System;
using System.Diagnostics;
using System.Management;
using ULib.Controls;
using ULib.DataSchema;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace Utilities.Commands
{
	[Serializable]
	public class NlbStatusCommand : CommandNode
    {
        public override string Name
        {
            get
            {
                return string.Format(base.Name, Computer_Name, Correct_Status);
            }
        }
        //public string Vb_Script = AppDomain.CurrentDomain.BaseDirectory + "External\\Scripts\\nbl.vbs";
        public string Computer_Name = ".";
        public string Correct_Status = "Normal";
        public NlbStatusCommand()
        {
            base.Name = "Name: {0} | Assert Status: {1}";
        }
        public override CommandResult Execute(CommandTreeViewNode node = null)
        {
			CommandResult rlt = new CommandResult();
			try
			{
                string targetName = null;
                ManagementObjectCollection machines = GetMgmtObjects("\\\\.\\root\\MicrosoftNLB", "Select * from MicrosoftNLB_Node");
                if (machines != null)
                {
                    foreach (ManagementObject m in machines)
                    {
                        string cname = (string)m.GetPropertyValue("ComputerName");
                        uint status = (uint)m.GetPropertyValue("StatusCode");
                        if (".".Equals(Computer_Name))
                        {
                            targetName = GetLocalName();
                        }
                        else
                        {
                            targetName = Computer_Name;
                        }
                        if (string.Equals(targetName, cname, StringComparison.OrdinalIgnoreCase))
                        {
                            string curtStatus = "Error";
                            if (status == 1008 || status == 1007)
                            {
                                curtStatus = "Normal";
                            }
                            if (status != 0 && !string.Equals(curtStatus, Correct_Status))
                            {
                                rlt.LastError = new Exception("Incorrect status detected: " + cname + "," + status);
                            }
                        }
                        else
                        {
                            Output(string.Join(",", new string[] {"Skipped",  cname, status.ToString() }));
                        }
                    }
                }
                //object resultValue = new CommandResult();
                //ProcessStartInfo info = new ProcessStartInfo();
                //info.FileName = "cscript.exe";
                //info.Arguments = Vb_Script;
                //info.StandardOutputEncoding = Encoding.ASCII;
                //info.UseShellExecute = false;
                //info.RedirectStandardOutput = true;
                //info.RedirectStandardError = true;
                //info.CreateNoWindow = true;
                //Process p = new Process();
                //p.StartInfo = info;
                //p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
                //p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);
                //p.Start();
                //p.BeginOutputReadLine();
                //p.BeginErrorReadLine();
                //p.WaitForExit();
			}
			catch (Exception e)
			{
				rlt.LastError = e;
			}
			return rlt;
        }

        public static string GetLocalName()
        {
            string domainName = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
            string hostName = Dns.GetHostName();
            string fqdn = "";
            if (!hostName.Contains(domainName))
                fqdn = hostName + "." + domainName;
            else
                fqdn = hostName;

            return fqdn;
        }

        void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            
        }

        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            
        }

		public override CommandNode Clone()
		{
			NlbStatusCommand rlt = new NlbStatusCommand { Computer_Name = this.Computer_Name, Correct_Status = this.Correct_Status };
			base.CopyChildren(rlt);
			return rlt;
		}

        public ManagementObjectCollection GetMgmtObjects(string path, string query)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(path, query);
            ManagementObjectCollection objs = searcher.Get();
            return objs;
        }
        public ManagementObject GetMgmtObject(string path, string query, string property, object value)
        {
            ManagementObject rlt = null;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(path, query);
            ManagementObjectCollection objs = searcher.Get();
            foreach (ManagementObject o in objs)
            {
                object v = o.GetPropertyValue(property);
                if (v == value)
                {
                    return o;
                }
            }
            return null;
        }
        public ManagementObject GetMgmtObject(string path, string query, int index)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(path, query);
            ManagementObjectCollection objs = searcher.Get();
            
            int i = 0;
            foreach (ManagementObject o in objs)
            {
                if (i == index)
                {
                    return o;
                }
                i++;
            }
            return null;
        }

		public override void Reset()
		{
			base.Reset();
		}
	}
}
