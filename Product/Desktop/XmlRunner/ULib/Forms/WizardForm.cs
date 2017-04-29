using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ULib.Exceptions;
using ULib.DataSchema;

namespace ULib.Forms
{
	public partial class WizardForm : DockForm
	{
		public Dict<string, string> Storage = new Dict<string, string>();
		public delegate void OnStartHandler(DockForm step);
		public delegate void OnLoadHandler();

		public event OnStartHandler OnStart;
		public event OnStartHandler OnStop;
		public event OnLoadHandler OnLoad;

		public bool BackToFirst = false;
		public bool IsStarted;
		protected List<string> status = new List<string>();
		public WizardForm()
		{
			InitializeComponent();
			StepPanel.Left = 10;
			StepPanel.Top = 10;
			StepPanel.Width = pnLeft.Width - 20;
			StepPanel.StepHeight = 25;
			StepPanel.StepMargin = 3;
			StepPanel.FormRegion = pnMain;
			StepPanel.Wizard = this;
            Load += new EventHandler(WizardForm_Load);
		}
		public void SetStatus(params string [] btnstatus)
		{
			//status.Clear();
			//for (int i = 0; i < btnstatus.Length; i++)
			//{
			//    string s = btnstatus[i];
			//    status.Add(s);
			//}
			//UpdateStatus();
		}
        void WizardForm_Load(object sender, EventArgs e)
        {
			try
			{
				if (OnLoad != null)
				{
					OnLoad();
				}
			}
			catch (Exception ex)
			{
				ExceptionHandler.Handle(ex);
			}
        }
		public void NotifyClose()
		{
			if (IsStarted)
			{
				bStop_Click(this, null);
			}
		}
		public void UpdateStatus()
		{
			int i = StepPanel.CurtIndex;
			if (i < 0 || i >=StepPanel.Count)
			{
				return;
			}
			if (i >= status.Count)
			{
				return;
			}
			string s = status[i];
			UpdateStatus(s);
		}
		public void Alert(string msg, params string[] args)
		{
			MessageBox.Show(string.Format(msg, args), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		public void UpdateStatus(string s)
		{
			this.Invoke((MethodInvoker)delegate()
			{
				if (s.Length == 4)
				{
					bBack.Enabled = s[0] == '1';
					bNext.Enabled = s[1] == '1';
					bStart.Enabled = s[2] == '1';
					bStop.Enabled = s[3] == '1';
				}
			});
		}
		private void bNext_Click(object sender, EventArgs e)
		{
			if (StepPanel.CurtStep.Form.PreviewNext())
			{
				StepPanel.Wizard = this;
				StepPanel.Next();
			}
		}

		private void bBack_Click(object sender, EventArgs e)
		{
			StepPanel.Wizard = this;
			StepPanel.Back(BackToFirst);
			BackToFirst = false;
		}

		private void bStop_Click(object sender, EventArgs e)
		{
			UpdateStatus("0000");
			if (OnStop != null)
			{
				OnStop(StepPanel.CurtStep.Form);
			}
		}

		private void bStart_Click(object sender, EventArgs e)
		{
			IsStarted = true;
			UpdateStatus("0001");
			if (OnStart != null)
			{
				OnStart(StepPanel.CurtStep.Form);
			}
		}

	}
}
