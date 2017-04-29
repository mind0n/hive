namespace OAWidgets
{
	partial class MailGenerator
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
			this.htmlBox = new Fs.UI.Controls.HtmlBox();
			this.fopenXL = new TestOffice.FileOpener();
			this.btnGenerate = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// htmlBox
			// 
			this.htmlBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.htmlBox.Location = new System.Drawing.Point(15, 44);
			this.htmlBox.Name = "htmlBox";
			this.htmlBox.Size = new System.Drawing.Size(604, 477);
			this.htmlBox.TabIndex = 11;
			this.htmlBox.UrlString = "about:blank";
			// 
			// fopenXL
			// 
			this.fopenXL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.fopenXL.FullPath = "";
			this.fopenXL.Location = new System.Drawing.Point(15, 12);
			this.fopenXL.Name = "fopenXL";
			this.fopenXL.Size = new System.Drawing.Size(604, 26);
			this.fopenXL.TabIndex = 10;
			// 
			// btnOpen
			// 
			this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnGenerate.Location = new System.Drawing.Point(15, 533);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(75, 23);
			this.btnGenerate.TabIndex = 9;
			this.btnGenerate.Text = "Generate";
			this.btnGenerate.UseVisualStyleBackColor = true;
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			// 
			// MailGenerator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(631, 572);
			this.Controls.Add(this.htmlBox);
			this.Controls.Add(this.fopenXL);
			this.Controls.Add(this.btnGenerate);
			this.Name = "MailGenerator";
			this.Text = "Outlook Mail Generator";
			this.ResumeLayout(false);

		}

		#endregion

		private Fs.UI.Controls.HtmlBox htmlBox;
		private TestOffice.FileOpener fopenXL;
		private System.Windows.Forms.Button btnGenerate;

	}
}