namespace Platform.Screens
{
	partial class SplitTestScreen
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
			this.sp1 = new System.Windows.Forms.SplitContainer();
			this.tScript = new System.Windows.Forms.TextBox();
			this.sp2 = new System.Windows.Forms.SplitContainer();
			this.tResult = new System.Windows.Forms.TextBox();
			this.sp1.Panel1.SuspendLayout();
			this.sp1.Panel2.SuspendLayout();
			this.sp1.SuspendLayout();
			this.sp2.Panel2.SuspendLayout();
			this.sp2.SuspendLayout();
			this.SuspendLayout();
			// 
			// sp1
			// 
			this.sp1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sp1.Location = new System.Drawing.Point(12, 12);
			this.sp1.Name = "sp1";
			// 
			// sp1.Panel1
			// 
			this.sp1.Panel1.Controls.Add(this.tScript);
			// 
			// sp1.Panel2
			// 
			this.sp1.Panel2.Controls.Add(this.sp2);
			this.sp1.Size = new System.Drawing.Size(815, 460);
			this.sp1.SplitterDistance = 330;
			this.sp1.TabIndex = 0;
			// 
			// tScript
			// 
			this.tScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tScript.Location = new System.Drawing.Point(3, 3);
			this.tScript.Multiline = true;
			this.tScript.Name = "tScript";
			this.tScript.Size = new System.Drawing.Size(324, 454);
			this.tScript.TabIndex = 0;
			// 
			// sp2
			// 
			this.sp2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sp2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.sp2.Location = new System.Drawing.Point(0, 0);
			this.sp2.Name = "sp2";
			// 
			// sp2.Panel2
			// 
			this.sp2.Panel2.Controls.Add(this.tResult);
			this.sp2.Size = new System.Drawing.Size(481, 460);
			this.sp2.SplitterDistance = 86;
			this.sp2.TabIndex = 0;
			// 
			// tResult
			// 
			this.tResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tResult.Location = new System.Drawing.Point(3, 3);
			this.tResult.Multiline = true;
			this.tResult.Name = "tResult";
			this.tResult.Size = new System.Drawing.Size(385, 454);
			this.tResult.TabIndex = 0;
			// 
			// SplitTestScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(839, 484);
			this.Controls.Add(this.sp1);
			this.Name = "SplitTestScreen";
			this.Text = "LexScreen";
			this.sp1.Panel1.ResumeLayout(false);
			this.sp1.Panel1.PerformLayout();
			this.sp1.Panel2.ResumeLayout(false);
			this.sp1.ResumeLayout(false);
			this.sp2.Panel2.ResumeLayout(false);
			this.sp2.Panel2.PerformLayout();
			this.sp2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer sp1;
		private System.Windows.Forms.SplitContainer sp2;
		private System.Windows.Forms.TextBox tScript;
		private System.Windows.Forms.TextBox tResult;

	}
}