using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ULib.Executing;
using ULib.Output;
using ULib.Exceptions;
using ULib.Forms;
using Utilities.Components.DBPartitioning;

namespace Utilities
{
	public partial class StartupForm : Form
	{
		WizardForm wiz = new WizardForm();
		public StartupForm()
		{
			InitializeComponent();
			Logger.Init();
			OutputHandler.Handlers.Add(this.PopupMsg);
			ExceptionHandler.AddHandler(this.PopupErrorMsg, true);
            Load += new EventHandler(StartupForm_Load);
			FormClosing += new FormClosingEventHandler(StartupForm_FormClosing);
		}

		void StartupForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			wiz.NotifyClose();
		}
		void PopupMsg(string msg, int type)
		{
		}
		bool PopupErrorMsg(Exception e)
		{
			MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return false;
		}
		void StartupForm_Load(object sender, EventArgs e)
        {
			wiz.OnLoad += new WizardForm.OnLoadHandler(wiz_OnLoad);
			wiz.EmbedInto(this);
        }

		void wiz_OnLoad()
		{
			wiz.StepPanel.AddStep("Choose Plan", new ChoosePlanForm());
			wiz.StepPanel.AddStep("Plan Settings", new PlanSettingsForm());
			wiz.StepPanel.AddStep("Execute", new ExecuteForm());
			wiz.UpdateStatus("0000");
			wiz.StepPanel.UpdateStatus(true);
			wiz.Text = this.Text = "Command Execute Wizard";
		}

	}
}
