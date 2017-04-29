namespace Utility.Startup.Forms
{
	partial class UrlToolForm
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
			this.tPath = new System.Windows.Forms.TextBox();
			this.tUrl = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// tPath
			// 
			this.tPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tPath.Location = new System.Drawing.Point(12, 12);
			this.tPath.Name = "tPath";
			this.tPath.Size = new System.Drawing.Size(276, 20);
			this.tPath.TabIndex = 0;
			this.tPath.TextChanged += new System.EventHandler(this.tPath_TextChanged);
			// 
			// tUrl
			// 
			this.tUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tUrl.Location = new System.Drawing.Point(12, 38);
			this.tUrl.Name = "tUrl";
			this.tUrl.ReadOnly = true;
			this.tUrl.Size = new System.Drawing.Size(276, 20);
			this.tUrl.TabIndex = 0;
			// 
			// UrlToolForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(300, 300);
			this.Controls.Add(this.tUrl);
			this.Controls.Add(this.tPath);
			this.Name = "UrlToolForm";
			this.Text = "UrlToolForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tPath;
		private System.Windows.Forms.TextBox tUrl;
	}
}