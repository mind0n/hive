using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using ULib.Configs;
using ULib.Controls;
using ULib.DataSchema;
using ULib.Exceptions;
using ULib.Executing;
using ULib.Executing.Commands.Common;
using ULib.Forms;
using ULib.Output;
using ULib.Executing.Commands.Data;
using System.IO;
using ULib;
using ULib.Executing.Commands.OS;
using System.Data;
using System.Drawing;

namespace Utilities.Components.DBPartitioning
{
	public partial class ExecuteForm : WizardStepForm
	{

		public ExecuteFormSettings Settings
		{
			get
			{
				return AppConfig.Instance.Load<ExecuteFormSettings>(Constants.KeyExecForm);
			}
		}
		List<CommandNode> cmds = new List<CommandNode>();
		public ExecuteForm()
		{
			InitializeComponent();
			OnEmbeded += new OnEmbededHandler(ExecuteForm_OnEmbeded);
			AppConfig.Instance.Initialize(Constants.KeyExecForm, typeof(ExecuteFormSettings));
			pnParam.Controls.Add(asp);
			if (!Settings.IsAllowDebug)
			{
				tabs.TabPages.Remove(tbParam);
			}
		}

		void ExecuteForm_OnEmbeded(EmbededForm sender)
		{
		    tVersion.Text = "Executor Version: " + Application.ProductVersion;
			gDebug.Enabled = Settings.IsAllowDebug;
			gDebug.Visible = Settings.IsAllowDebug;
			OutputHandler.Handle("Execute form loaded");
			Wizard.UpdateStatus("1010");
			Wizard.OnStart -= new WizardForm.OnStartHandler(Wizard_OnStart);
			Wizard.OnStop -= new WizardForm.OnStartHandler(Wizard_OnStop);
			Wizard.OnStart += new WizardForm.OnStartHandler(Wizard_OnStart);
			Wizard.OnStop += new WizardForm.OnStartHandler(Wizard_OnStop);
			Wizard.BackToFirst = true;
			ResetEvents();
			lvCmd.DataSource = null;
			lvCmd.AutoGenerateColumns = false;
			lvCmd.Columns[0].DataPropertyName = "Index";
			lvCmd.Columns[0].Width = 50;
			lvCmd.Columns[2].DataPropertyName = "ExecStatus";
			lvCmd.Columns[2].Width = 100;
			lvCmd.Columns[1].DataPropertyName = "Name";
			lvCmd.Columns[1].Width = lvCmd.Width - 200;
			lvCmd.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			cmds.Clear();
			InitTabsStatus();
			Executor.Instance.Preview(null, PreviewCmdCallback);
			InitCmdStatus();
			lvCmd.DataSource = cmds;
		}

		private void InitTabsStatus()
		{
			if (!Settings.IsAllowDebug)
			{
				if (tabsStatus.TabPages.Count > 1)
				{
					tabsStatus.TabPages.RemoveAt(1);
				}
				tabsStatus.Left = 10;
				tabsStatus.Width = this.Width - 20;
			}
			else
			{
				if (tabsStatus.TabPages.Count <= 1)
				{
					tabsStatus.TabPages.Add(tbParam);
				}
				tabsStatus.Left = 154;
				tabsStatus.Width = this.Width - 154 - 20;
			}
		}

		private void InitCmdStatus()
		{
			foreach (CommandNode i in cmds)
			{
				i.ExecStatus = CommandStatus.Wait;
				i.Error = null;
			}
		}

		void Wizard_OnStop(DockForm step)
		{
			Executor.Instance.Stop();
		}

		void Output(string msg, int type)
		{
			try
			{
				this.Invoke((MethodInvoker)delegate()
				{
					obox.WriteMsg(msg);
				});
			}
			catch { }
		}

		bool OutputError(Exception e)
		{
			try
			{
				this.Invoke((MethodInvoker)delegate()
				{
					obox.WriteErrorMsg(e.ToString());
				});
			}
			catch { }
			return false;
		}

		void PreviewCmdCallback(CommandNode cmd, bool isComplete, CommandResult rlt)
		{
			if (!(cmd is IfCommand) && !(cmd is DescriptionsCommand) && !(cmd is SetCommand))
			{
				cmds.Add(cmd);
			}
		}

		bool isFullHandling;

		private CommandNode GetSelectedCommand()
		{
			if (lvCmd.SelectedRows.Count > 0)
			{
				CommandNode cmd = lvCmd.SelectedRows[0].DataBoundItem as CommandNode;
				return cmd;
			}
			return null;
		}

		void Instance_OnRun(CommandNode run2Cmd, CommandNode runFromCmd)
		{
			if (run2Cmd == null && runFromCmd == null)
			{
				InitCmdStatus();
			}
			isFullHandling = ExceptionHandler.IsFullHandling;
			ExceptionHandler.IsFullHandling = false;
			ExceptionHandler.Handlers.Add(OutputError);
			OutputHandler.Handlers.Add(Output);
		}

		void Instance_OnRunCompleted(CommandNode run2Cmd, CommandNode runFromCmd, bool isStopOnError = false)
		{
			ExceptionHandler.IsFullHandling = isFullHandling;
			ExceptionHandler.Handlers.Remove(OutputError);
			OutputHandler.Handlers.Remove(Output);
			RemoveEvents();
            if (isStopOnError)
            {
                MessageBox.Show("Execution stopped by an error.  Please select the item with error status to view the details.", "Stop on Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
			Wizard.UpdateStatus("1000");
		}

		void Wizard_OnStart(DockForm step)
		{
			obox.Clear();
			//Executor.Instance.Cache();

			ResetEvents();
		    Executor.Instance.FailAndRun = !ckStopOnError.Checked;
			Executor.Instance.RunAsync();
		}

		private void ResetEvents()
		{
			RemoveEvents();

			SetEvents();
		}

		private void SetEvents()
		{
			Executor.Instance.OnExecutionStopped += new Executor.StopRunHandler(Instance_OnExecutionStopped);
			Executor.Instance.OnRun += new Executor.RunHandler(Instance_OnRun);
			Executor.Instance.OnRunCompleted += new Executor.RunCompletedHandler(Instance_OnRunCompleted);
			Executor.Instance.ExecState.OnExecCmd += new ExecuteState.RunCmdHandler(State_OnExecCmd);
		}

		private void RemoveEvents()
		{
			Executor.Instance.ExecState.OnExecCmd -= new ExecuteState.RunCmdHandler(State_OnExecCmd);
			Executor.Instance.OnRun -= new Executor.RunHandler(Instance_OnRun);
			Executor.Instance.OnRunCompleted -= new Executor.RunCompletedHandler(Instance_OnRunCompleted);
			Executor.Instance.OnExecutionStopped -= new Executor.StopRunHandler(Instance_OnExecutionStopped);
		}

		void State_OnExecCmd(CommandNode cmd, bool isCompleted = false, CommandResult rlt = null)
		{
			this.Invoke((MethodInvoker)delegate
			{
				if (isCompleted && !rlt.IsSuccessful)
				{
					cmd.ExecStatus = CommandStatus.Error;
					cmd.Error = rlt.LastError;
				}
				else if (isCompleted && rlt.IsSuccessful)
				{
					cmd.ExecStatus = CommandStatus.Success;
				}
				lvCmd.Update();
				lvCmd.Invalidate();
			});
		}

		AutoSettingPanel asp = new AutoSettingPanel();

		private void lvCmd_SelectionChanged(object sender, EventArgs e)
		{
			CommandNode cmd = GetSelectedCommand();
			if (cmd != null)
			{
				pnParam.Enabled = Settings.IsAllowDebug;
				asp.Dock = DockStyle.Fill;
				asp.Initialize();
				asp.Generate(cmd);
				if (cmd.Error != null)
				{
					errbox.Text = cmd.Error.ToString();
				}
				else
				{
					errbox.Text = cmd.ExecStatus.ToString();
				}
			}
		}

		private void lvCmd_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			CommandNode cmd = GetSelectedCommand();
			if (cmd != null)
			{
				if (cmd is ExecuteSqlCommand)
				{
					ExecuteSqlCommand sql = (ExecuteSqlCommand)cmd;
					string file = AppDomain.CurrentDomain.BaseDirectory + sql.SqlFile;
					file.Open();
				}
				else if (cmd is SetCommand)
				{
					errbox.Text = Executor.Instance.GetString(cmd.Id);
				}
				else if (cmd is ArrayCommand)
				{
					errbox.Text = ((ArrayCommand)cmd).ToString();
				}
				else if (cmd is RunCommand)
				{
					RunCommand rc = (RunCommand)cmd;
					errbox.Text = rc.CmdLine;
				}
			}
		}

		private void bViewLog_Click(object sender, EventArgs e)
		{
			Process.Start("notepad.exe", AppDomain.CurrentDomain.BaseDirectory + "log.txt");
		}

		private void bRunCurt_Click(object sender, EventArgs e)
		{
			CommandNode cmd = GetSelectedCommand();
			if (cmd != null)
			{
                CommandResult rlt = cmd.Execute();
				State_OnExecCmd(cmd, true, rlt);
			}
		}

		private void bRun2Curt_Click(object sender, EventArgs e)
		{
			CommandNode cmd = GetSelectedCommand();
			if (cmd != null)
			{
				ResetEvents();
				Executor.Instance.RunAsync(cmd);
			}

		}

		bool Instance_OnExecutionStopped(CommandNode cmd, bool isCompleted = false, CommandResult rlt = null)
		{
			if (cmd is ValidateDbCommand)
			{
				ValidateDbCommand v = (ValidateDbCommand)cmd;
                if (v.IsNoException)
                {
                    if (MessageBox.Show(v.ErrorMsg, "Validation failed", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    {
                        rlt.ShouldStop = false;
                        return false;
                    }
                }
			}
			RemoveEvents();
			Wizard.UpdateStatus("1000");
			return true;
		}

		private void bRunFrom_Click(object sender, EventArgs e)
		{
			CommandNode cmd = GetSelectedCommand();
			if (cmd != null)
			{
				ResetEvents();
			    Executor.Instance.FailAndRun = !ckStopOnError.Checked;
				Executor.Instance.RunAsync(null, cmd);
			}
		}

		private void bSkip2Curt_Click(object sender, EventArgs e)
		{
			CommandNode cmd = GetSelectedCommand();
			if (cmd != null)
			{
				ResetEvents();
				if (Executor.Instance.Preview(cmd))
				{
					MessageBox.Show("Jumped to the command successfully", "Jump Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show("Specified command not reached, jumped to the first command.", "Jump Completed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
		}

		private void lvCmd_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
		}

		private void lvCmd_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
		{

		}

		private void lvCmd_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
		{

		}

		private void lvCmd_BindingContextChanged(object sender, EventArgs e)
		{

		}

		private void lvCmd_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.ColumnIndex >= 0 && e.ColumnIndex < lvCmd.Columns.Count && e.RowIndex >= 0 && e.RowIndex < lvCmd.Rows.Count)
			{
				DataGridViewRow row = lvCmd.Rows[e.RowIndex];
				DataGridViewCell cell = row.Cells[e.ColumnIndex];
				object v = cell.Value;
				if (v != null)
				{
					string value = v.ToString();
					if (string.Equals(value, "success", StringComparison.OrdinalIgnoreCase))
					{
						cell.Style.ForeColor = Color.Green;
					}
					else if (string.Equals(value, "error", StringComparison.OrdinalIgnoreCase))
					{
						cell.Style.ForeColor = Color.Red;
					}
					else if (string.Equals(value, "wait", StringComparison.OrdinalIgnoreCase) || string.Equals(value, "ready", StringComparison.OrdinalIgnoreCase))
					{
						cell.Style.ForeColor = Color.Gray;
					}
					else
					{
						cell.Style.ForeColor = Color.Black;
					}
				}
			}
		}

        private void ckErrStop_CheckedChanged(object sender, EventArgs e)
        {
            Executor.Instance.FailAndRun = !ckStopOnError.Checked;
        }
	}

	public class ExecuteFormSettings
	{
		public bool IsAllowDebug { get; set; }
		private List<string> plans;
		public List<string> Plans
		{
			get {
				if (plans == null)
				{
					plans = new List<string>();
				}
				return plans; 
			}
			set { plans = value; }
		}
		public string LastPlan { get; set; }
	}

}
