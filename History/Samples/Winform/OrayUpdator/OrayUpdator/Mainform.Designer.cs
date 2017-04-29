namespace OrayUpdator
{
	partial class Mainform
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
			this.web = new System.Windows.Forms.WebBrowser();
			this.tUrl = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// web
			// 
			this.web.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.web.Location = new System.Drawing.Point(12, 36);
			this.web.MinimumSize = new System.Drawing.Size(20, 20);
			this.web.Name = "web";
			this.web.Size = new System.Drawing.Size(260, 213);
			this.web.TabIndex = 0;
			// 
			// tUrl
			// 
			this.tUrl.Location = new System.Drawing.Point(12, 10);
			this.tUrl.Name = "tUrl";
			this.tUrl.Size = new System.Drawing.Size(260, 20);
			this.tUrl.TabIndex = 1;
			// 
			// Mainform
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.tUrl);
			this.Controls.Add(this.web);
			this.Name = "Mainform";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.WebBrowser web;
		private System.Windows.Forms.TextBox tUrl;
	}
}

