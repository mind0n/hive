using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ULib.Winform.Controls;
using System.Diagnostics;

namespace Utility.Startup.Forms
{
	public partial class PIDForm : ScreenRegion
	{
		public PIDForm()
		{
			InitializeComponent();
			Load += new EventHandler(PIDForm_Load);
		}

		void PIDForm_Load(object sender, EventArgs e)
		{
			tpid.Text = Process.GetCurrentProcess().Id.ToString();
		}

		private void bSearch_Click(object sender, EventArgs e)
		{
			int pid;
			if (int.TryParse(tpid.Text, out pid))
			{
				try
				{
					Process p = Process.GetProcessById(pid);
					while (p != null)
					{
						Output(p.Id.ToString(), " : ", p.MainModule.FileName);
						p = p.Parent();
					}
				}
				catch (Exception error)
				{
					Output(error.Message);
				}
			}
		}

		private void Output(params string[] msg)
		{
			box.Text = string.Concat(msg) + "\r\n" + box.Text;
		}
	}

	public static class ProcessExtensions
	{
		private static string FindIndexedProcessName(int pid)
		{
			var processName = Process.GetProcessById(pid).ProcessName;
			var processesByName = Process.GetProcessesByName(processName);
			string processIndexdName = null;

			for (var index = 0; index < processesByName.Length; index++)
			{
				processIndexdName = index == 0 ? processName : processName + "#" + index;
				var processId = new PerformanceCounter("Process", "ID Process", processIndexdName);
				if ((int)processId.NextValue() == pid)
				{
					return processIndexdName;
				}
			}

			return processIndexdName;
		}

		private static Process FindPidFromIndexedProcessName(string indexedProcessName)
		{
			var parentId = new PerformanceCounter("Process", "Creating Process ID", indexedProcessName);
			return Process.GetProcessById((int)parentId.NextValue());
		}

		public static Process Parent(this Process process)
		{
			return FindPidFromIndexedProcessName(FindIndexedProcessName(process.Id));
		}
	}
}
