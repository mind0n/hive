namespace WebOperator
{
	partial class DebugForm
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
			this.tScripts = new System.Windows.Forms.TextBox();
			this.bRun = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tScripts
			// 
			this.tScripts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tScripts.Location = new System.Drawing.Point(12, 12);
			this.tScripts.Multiline = true;
			this.tScripts.Name = "tScripts";
			this.tScripts.Size = new System.Drawing.Size(607, 341);
			this.tScripts.TabIndex = 0;
			// 
			// bRun
			// 
			this.bRun.Location = new System.Drawing.Point(12, 359);
			this.bRun.Name = "bRun";
			this.bRun.Size = new System.Drawing.Size(75, 23);
			this.bRun.TabIndex = 1;
			this.bRun.Text = "Run";
			this.bRun.UseVisualStyleBackColor = true;
			this.bRun.Click += new System.EventHandler(this.bRun_Click);
			// 
			// DebugForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(631, 403);
			this.Controls.Add(this.bRun);
			this.Controls.Add(this.tScripts);
			this.Name = "DebugForm";
			this.Text = "DebugForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tScripts;
		private System.Windows.Forms.Button bRun;
	}
}