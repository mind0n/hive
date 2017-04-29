using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ULib.Forms;
using ULib.Executing;
using ULib.Output;
using ULib.Configs;
using ULib;
using System.IO;
using System.Collections;
using ULib.DataSchema;
using ULib.Executing.Commands.Common;

namespace Utilities.Components.DBPartitioning
{
	public partial class PlanSettingsForm : WizardStepForm
	{
		public ExecuteFormSettings Settings
		{
			get
			{
				return AppConfig.Instance.Load<ExecuteFormSettings>(Constants.KeyExecForm);
			}
		}

		public PlanSettingsForm()
		{
			InitializeComponent();
			OnEmbeded += new OnEmbededHandler(PlanSettingsForm_OnEmbeded);
		}

		void PlanSettingsForm_OnEmbeded(EmbededForm sender)
		{
			OutputHandler.Handle("Plan Settings");
			Executor.Instance.Reset();
			Executor.Instance.SetVar("PlanSettingsForm", false, this);
			Executor.Instance.RunOnLoad();
			asp.Initialize();
			asp.Visible = false;
			asp.Generate(Executor.Instance.Parameters);
			asp.Visible = true;

			if (tabs.TabPages.Contains(tbPartition))
			{
				tabs.TabPages.Remove(tbPartition);
			}
			Executor.Instance.RunOnConfig();
			Wizard.UpdateStatus("1100");
		}
		
		public override bool PreviewNext()
		{
			bool rlt = asp.Validate();
			bool rltPartition = IsPartitionTabShown ? aspPartition.Validate() : true;
			OutputHandler.Handle("Verify Plan Settings {0}", 0, rlt ? "successful" : "failed");
			Executor.Instance.Cache();
			if (!rltPartition)
			{
				if (rlt)
				{
					tabs.SelectedIndex = 1;
				}
				else
				{
					tabs.SelectedIndex = 0;
				}
			}
			else
			{
				tabs.SelectedIndex = 0;
			}
			if (!rlt || !rltPartition)
			{
				MessageBox.Show("Required parameter missing. Please check the value in the enabled item(s).", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else if (IsPartitionTabShown)
			{
				BatchReplace();
			}
			return rlt && rltPartition;
		}

		private void BatchReplace()
		{
			string targetDir = AppDomain.CurrentDomain.BaseDirectory + "sql\\";
			string baseDir = targetDir + "original\\" + curtFolder + "\\";
			foreach (Node file in partitionSettings.Children)
			{
				string sqlfile = baseDir + file.Name;
				string content = File.ReadAllText(sqlfile);
				foreach (Node i in file.Children)
				{
					if (i is SetCommand)
					{
						SetCommand cmd = (SetCommand)i;
						if (!string.IsNullOrEmpty(cmd.Argument))
						{
							content = content.Replace(cmd.Argument, cmd.Value == null ? string.Empty : cmd.Value.ToString());
						}
					}
				}
				File.WriteAllText(targetDir + file.Name, content);
			}
		}

		Node partitionSettings = new Node();

		public void LoadParametersFromBackup(string file)
		{
			if (!file.IsAbsolute())
			{
				file = AppDomain.CurrentDomain.BaseDirectory + file;
			}
			Executor.Instance.LoadParameters(file);
		}

		bool IsPartitionTabShown
		{
			get
			{
				return tabs.Contains(tbPartition);
			}
		}
		string curtFolder;
		public void ConfigPartitionsData(string folder)
		{
			string sqldir = AppDomain.CurrentDomain.BaseDirectory + "sql\\original\\" + folder + "\\";
			string[] sqls = Directory.GetFiles(sqldir);
			curtFolder = folder;
			tabs.TabPages.Add(tbPartition);
			aspPartition.Initialize();
			aspPartition.LabelWidth = 250;
			aspPartition.Visible = false;
			partitionSettings.Clear();
			foreach (string file in sqls)
			{
				aspPartition.Generate(partitionSettings, file);
			}
			aspPartition.Visible = true;
		}
		
	}
}
