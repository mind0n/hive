namespace Wcf.Client.Startup
{
	partial class Main
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
			this.txt = new System.Windows.Forms.TextBox();
			this.turl = new System.Windows.Forms.TextBox();
			this.bAddUrl = new System.Windows.Forms.Button();
			this.bListUrls = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txt
			// 
			this.txt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txt.Location = new System.Drawing.Point(12, 39);
			this.txt.Multiline = true;
			this.txt.Name = "txt";
			this.txt.ReadOnly = true;
			this.txt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txt.Size = new System.Drawing.Size(428, 252);
			this.txt.TabIndex = 0;
			this.txt.TabStop = false;
			this.txt.WordWrap = false;
			// 
			// turl
			// 
			this.turl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.turl.Location = new System.Drawing.Point(12, 12);
			this.turl.Name = "turl";
			this.turl.Size = new System.Drawing.Size(489, 21);
			this.turl.TabIndex = 0;
			this.turl.Text = "http://www.google.com";
			// 
			// bAddUrl
			// 
			this.bAddUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bAddUrl.Location = new System.Drawing.Point(446, 38);
			this.bAddUrl.Name = "bAddUrl";
			this.bAddUrl.Size = new System.Drawing.Size(55, 21);
			this.bAddUrl.TabIndex = 1;
			this.bAddUrl.Text = "Add";
			this.bAddUrl.UseVisualStyleBackColor = true;
			this.bAddUrl.Click += new System.EventHandler(this.bAddUrl_Click);
			// 
			// bListUrls
			// 
			this.bListUrls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bListUrls.Location = new System.Drawing.Point(446, 65);
			this.bListUrls.Name = "bListUrls";
			this.bListUrls.Size = new System.Drawing.Size(55, 21);
			this.bListUrls.TabIndex = 2;
			this.bListUrls.Text = "List";
			this.bListUrls.UseVisualStyleBackColor = true;
			this.bListUrls.Click += new System.EventHandler(this.bListUrls_Click);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(514, 303);
			this.Controls.Add(this.bListUrls);
			this.Controls.Add(this.bAddUrl);
			this.Controls.Add(this.turl);
			this.Controls.Add(this.txt);
			this.Name = "Main";
			this.Text = "WCF Client";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txt;
		private System.Windows.Forms.TextBox turl;
		private System.Windows.Forms.Button bAddUrl;
		private System.Windows.Forms.Button bListUrls;
	}
}

