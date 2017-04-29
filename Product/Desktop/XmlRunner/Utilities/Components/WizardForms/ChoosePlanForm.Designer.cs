namespace Utilities.Components.DBPartitioning
{
	partial class ChoosePlanForm
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
            this.ptPlan = new ULib.Controls.PathBrowser();
            this.label1 = new System.Windows.Forms.Label();
            this.tContent = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bSave = new System.Windows.Forms.Button();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tbPlan = new System.Windows.Forms.TabPage();
            this.alp = new ULib.Controls.AutoLabelPanel();
            this.tbDetails = new System.Windows.Forms.TabPage();
            this.tabs.SuspendLayout();
            this.tbPlan.SuspendLayout();
            this.tbDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // ptPlan
            // 
            this.ptPlan.AllowDrop = true;
            this.ptPlan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ptPlan.Location = new System.Drawing.Point(68, 12);
            this.ptPlan.Name = "ptPlan";
            this.ptPlan.Size = new System.Drawing.Size(436, 23);
            this.ptPlan.StartupPath = null;
            this.ptPlan.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Plan File:";
            // 
            // tContent
            // 
            this.tContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tContent.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tContent.Location = new System.Drawing.Point(6, 24);
            this.tContent.Multiline = true;
            this.tContent.Name = "tContent";
            this.tContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tContent.Size = new System.Drawing.Size(470, 251);
            this.tContent.TabIndex = 2;
            this.tContent.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Content:";
            // 
            // bSave
            // 
            this.bSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bSave.Location = new System.Drawing.Point(401, 281);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(75, 23);
            this.bSave.TabIndex = 4;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // tabs
            // 
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Controls.Add(this.tbPlan);
            this.tabs.Controls.Add(this.tbDetails);
            this.tabs.Location = new System.Drawing.Point(14, 41);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(490, 367);
            this.tabs.TabIndex = 5;
            // 
            // tbPlan
            // 
            this.tbPlan.Controls.Add(this.alp);
            this.tbPlan.Location = new System.Drawing.Point(4, 22);
            this.tbPlan.Name = "tbPlan";
            this.tbPlan.Padding = new System.Windows.Forms.Padding(3);
            this.tbPlan.Size = new System.Drawing.Size(482, 341);
            this.tbPlan.TabIndex = 1;
            this.tbPlan.Text = "Plan Description";
            this.tbPlan.UseVisualStyleBackColor = true;
            // 
            // alp
            // 
            this.alp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.alp.AutoScroll = true;
            this.alp.AutoScrollMinSize = new System.Drawing.Size(1, 1);
            this.alp.BackColor = System.Drawing.Color.White;
            this.alp.LeftMargin = 0;
            this.alp.Location = new System.Drawing.Point(6, 6);
            this.alp.Name = "alp";
            this.alp.Size = new System.Drawing.Size(470, 329);
            this.alp.TabIndex = 0;
            this.alp.TopMargin = 0;
            // 
            // tbDetails
            // 
            this.tbDetails.Controls.Add(this.tContent);
            this.tbDetails.Controls.Add(this.bSave);
            this.tbDetails.Controls.Add(this.label3);
            this.tbDetails.Location = new System.Drawing.Point(4, 22);
            this.tbDetails.Name = "tbDetails";
            this.tbDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tbDetails.Size = new System.Drawing.Size(482, 341);
            this.tbDetails.TabIndex = 0;
            this.tbDetails.Text = "Plan Details";
            this.tbDetails.UseVisualStyleBackColor = true;
            // 
            // ChoosePlanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 420);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ptPlan);
            this.Name = "ChoosePlanForm";
            this.Text = "ChoosePlanForm";
            this.tabs.ResumeLayout(false);
            this.tbPlan.ResumeLayout(false);
            this.tbDetails.ResumeLayout(false);
            this.tbDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private ULib.Controls.PathBrowser ptPlan;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tContent;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button bSave;
		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.TabPage tbPlan;
		private System.Windows.Forms.TabPage tbDetails;
		private ULib.Controls.AutoLabelPanel alp;
	}
}