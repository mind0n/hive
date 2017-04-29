using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ULib.Forms;
using System.IO;
using ULib;
using ULib.Executing;
using ULib.Exceptions;
using ULib.Output;
using ULib.Configs;
using ULib.NativeSystem;
using ULib.Controls;

namespace Utilities.Components.DBPartitioning
{
	public partial class ChoosePlanForm : WizardStepForm
	{
		public ExecuteFormSettings Settings
		{
			get
			{
				return AppConfig.Instance.Load<ExecuteFormSettings>(Constants.KeyExecForm);
			}
		}
		private bool isChanged
		{
			set
			{
				bSave.Enabled = value;
			}
		}
		public ChoosePlanForm()
		{
			InitializeComponent();
			AppConfig.Instance.Initialize(Constants.KeyExecForm, typeof(ExecuteFormSettings));

			ptPlan.isOpenFile = true;
			ptPlan.ComboBox.Text = Settings.LastPlan;
			ptPlan.ComboBox.TextChanged += new EventHandler(ComboBox_TextChanged);
			if (string.IsNullOrEmpty(Settings.LastPlan))
			{
                ptPlan.StartupPath = AppDomain.CurrentDomain.BaseDirectory + "plans\\InsertItem.xml";
			}

			OnEmbeded += new OnEmbededHandler(ChoosePlanForm_OnEmbeded);
			tContent.TextChanged += new EventHandler(tContent_TextChanged);
		}

		void tContent_TextChanged(object sender, EventArgs e)
		{
			isChanged = true;
		}

		void ChoosePlanForm_OnEmbeded(EmbededForm sender)
		{
			if (!Settings.IsAllowDebug)
			{
				if (tabs.TabPages.Contains(tbDetails))
				{
					tabs.TabPages.Remove(tbDetails);
				}
			}
			else
			{
				if (!tabs.TabPages.Contains(tbDetails))
				{
					tabs.TabPages.Add(tbDetails);
				}
			}
			isChanged = false;
			ptPlan.FileDialog.Filter = "Plan Files(*.xml)|*.xml|Plan Backup Files(*.xml.bak)|*.xml.bak|All Files(*.*)|*.*";
			OutputHandler.Handle("Choose Plan");
			ptPlan.ClearPaths();
			if (Settings.Plans.Count > 0)
			{
				ptPlan.LoadPaths(Settings.Plans.ToArray());
				if (!string.IsNullOrEmpty(Settings.LastPlan))
				{
					ptPlan.StartupPath = Settings.LastPlan;
				}
			}
			PreviewNext();
		}

		void ComboBox_TextChanged(object sender, EventArgs e)
		{
			alp.Clear();
			if (File.Exists(ptPlan.ComboBox.Text))
			{
				tContent.TextChanged -= new EventHandler(tContent_TextChanged);
				tContent.Text = File.ReadAllText(ptPlan.ComboBox.Text);
				tContent.TextChanged += new EventHandler(tContent_TextChanged);
				List<AutoLabelItem> list = Executor.Instance.LoadDescription(tContent.Text);
				alp.AddRange(list);
				PreviewNext();
			}
			else
			{
				alp.Add("Please select a plan file.");
			}
			alp.Display();
		}

		public override bool PreviewNext()
		{
			if (File.Exists(ptPlan.ComboBox.Text))
			{
				try
				{
					Settings.LastPlan = ptPlan.StartupPath;
					Settings.Plans.Clear();
					Settings.Plans.AddRange(ptPlan.GetPaths());
					AppConfig.Instance.Update(Constants.KeyExecForm, Settings);

					if (Wizard != null)
					{
						Wizard.Storage["planfile"] = ptPlan.ComboBox.Text;
					}
					
					Executor.Instance.Load(tContent.Text);
					Executor.Instance.SetVar("ChoosePlanForm", false, this);
					//Executor.Instance.RunOnLoad();

					OutputHandler.Handle("Plan selected: " + ptPlan.ComboBox.Text);

					if (Wizard != null)
					{
						Wizard.UpdateStatus("0100");
					}
					AppConfig.Instance.Save(Constants.KeyExecForm);
					return true;
				}
				catch (Exception e)
				{
					ExceptionHandler.Handle(e);
				}
			}
			else
			{
				Wizard.UpdateStatus("0000");
			}
			return false;
		}

		//public void LoadParametersFromBackup(string file)
		//{
		//    if (!file.IsAbsolute())
		//    {
		//        file = AppDomain.CurrentDomain.BaseDirectory + file;
		//    }
		//    Executor.Instance.LoadParameters(file);
		//}

		private void bSave_Click(object sender, EventArgs e)
		{
			try
			{
				File.WriteAllText(ptPlan.ComboBox.Text, tContent.Text);
				isChanged = false;
			}
			catch (Exception ex)
			{
				isChanged = true;
				ExceptionHandler.Handle(ex);
			}
		}
	}
}
