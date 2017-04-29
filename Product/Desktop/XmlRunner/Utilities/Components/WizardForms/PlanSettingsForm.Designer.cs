namespace Utilities.Components.DBPartitioning
{
	partial class PlanSettingsForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnCfgPar = new System.Windows.Forms.Panel();
			this.tabs = new System.Windows.Forms.TabControl();
			this.tbPlan = new System.Windows.Forms.TabPage();
			this.asp = new ULib.Controls.AutoSettingPanel();
			this.tbPartition = new System.Windows.Forms.TabPage();
			this.aspPartition = new ULib.Controls.AutoSettingPanel();
			this.pnCfgPar.SuspendLayout();
			this.tabs.SuspendLayout();
			this.tbPlan.SuspendLayout();
			this.tbPartition.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnCfgPar
			// 
			this.pnCfgPar.Controls.Add(this.tabs);
			this.pnCfgPar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnCfgPar.Location = new System.Drawing.Point(0, 0);
			this.pnCfgPar.Name = "pnCfgPar";
			this.pnCfgPar.Size = new System.Drawing.Size(693, 631);
			this.pnCfgPar.TabIndex = 0;
			// 
			// tabs
			// 
			this.tabs.Controls.Add(this.tbPlan);
			this.tabs.Controls.Add(this.tbPartition);
			this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabs.Location = new System.Drawing.Point(0, 0);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(693, 631);
			this.tabs.TabIndex = 1;
			// 
			// tbPlan
			// 
			this.tbPlan.Controls.Add(this.asp);
			this.tbPlan.Location = new System.Drawing.Point(4, 22);
			this.tbPlan.Name = "tbPlan";
			this.tbPlan.Padding = new System.Windows.Forms.Padding(3);
			this.tbPlan.Size = new System.Drawing.Size(685, 605);
			this.tbPlan.TabIndex = 0;
			this.tbPlan.Text = "Plan Settings";
			this.tbPlan.UseVisualStyleBackColor = true;
			// 
			// asp
			// 
			this.asp.AllowDrop = true;
			this.asp.AutoScroll = true;
			this.asp.AutoScrollMinSize = new System.Drawing.Size(0, 20);
			this.asp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.asp.Font = new System.Drawing.Font("Consolas", 8F);
			this.asp.ItemHeight = 24;
			this.asp.ItemLeft = 10;
			this.asp.ItemMarginLeft = 4;
			this.asp.ItemMarginRight = 20;
			this.asp.ItemMarginTop = 4;
			this.asp.LabelWidth = 120;
			this.asp.Location = new System.Drawing.Point(3, 3);
			this.asp.Name = "asp";
			this.asp.Size = new System.Drawing.Size(679, 599);
			this.asp.TabIndex = 0;
			// 
			// tbPartition
			// 
			this.tbPartition.Controls.Add(this.aspPartition);
			this.tbPartition.Location = new System.Drawing.Point(4, 22);
			this.tbPartition.Name = "tbPartition";
			this.tbPartition.Padding = new System.Windows.Forms.Padding(3);
			this.tbPartition.Size = new System.Drawing.Size(685, 605);
			this.tbPartition.TabIndex = 1;
			this.tbPartition.Text = "Partition Settings";
			this.tbPartition.UseVisualStyleBackColor = true;
			// 
			// aspPartition
			// 
			this.aspPartition.AllowDrop = true;
			this.aspPartition.AutoScroll = true;
			this.aspPartition.AutoScrollMinSize = new System.Drawing.Size(0, 24);
			this.aspPartition.Dock = System.Windows.Forms.DockStyle.Fill;
			this.aspPartition.Font = new System.Drawing.Font("Consolas", 8F);
			this.aspPartition.ItemHeight = 0;
			this.aspPartition.ItemLeft = 0;
			this.aspPartition.ItemMarginLeft = 0;
			this.aspPartition.ItemMarginRight = 0;
			this.aspPartition.ItemMarginTop = 0;
			this.aspPartition.LabelWidth = 0;
			this.aspPartition.Location = new System.Drawing.Point(3, 3);
			this.aspPartition.Name = "aspPartition";
			this.aspPartition.Size = new System.Drawing.Size(679, 599);
			this.aspPartition.TabIndex = 0;
			// 
			// PlanSettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(693, 631);
			this.Controls.Add(this.pnCfgPar);
			this.Name = "PlanSettingsForm";
			this.Text = "PlanSettingsForm";
			this.pnCfgPar.ResumeLayout(false);
			this.tabs.ResumeLayout(false);
			this.tbPlan.ResumeLayout(false);
			this.tbPartition.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnCfgPar;
		private ULib.Controls.AutoSettingPanel asp;
		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.TabPage tbPlan;
		private System.Windows.Forms.TabPage tbPartition;
		private ULib.Controls.AutoSettingPanel aspPartition;
	}
}