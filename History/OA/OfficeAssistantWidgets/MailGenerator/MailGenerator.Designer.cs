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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MailGenerator));
			this.btnGenerate = new System.Windows.Forms.Button();
			this.grpMail = new System.Windows.Forms.GroupBox();
			this.htmlBox = new Fs.UI.Controls.HtmlBox();
			this.txtSubject = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.listAttaches = new System.Windows.Forms.ListBox();
			this.txtCC = new System.Windows.Forms.TextBox();
			this.txtSender = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnSendMail = new System.Windows.Forms.Button();
			this.grpXL = new System.Windows.Forms.GroupBox();
			this.btnOpenBook = new System.Windows.Forms.Button();
			this.btnLoadTemplate = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.cbSheets = new System.Windows.Forms.ComboBox();
			this.fopenXL = new TestOffice.FileOpener();
			this.grpMail.SuspendLayout();
			this.grpXL.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnGenerate
			// 
			this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnGenerate.Location = new System.Drawing.Point(15, 551);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(75, 23);
			this.btnGenerate.TabIndex = 9;
			this.btnGenerate.Text = "Re-generate";
			this.btnGenerate.UseVisualStyleBackColor = true;
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			// 
			// grpMail
			// 
			this.grpMail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.grpMail.Controls.Add(this.htmlBox);
			this.grpMail.Controls.Add(this.txtSubject);
			this.grpMail.Controls.Add(this.label5);
			this.grpMail.Controls.Add(this.label4);
			this.grpMail.Controls.Add(this.listAttaches);
			this.grpMail.Controls.Add(this.txtCC);
			this.grpMail.Controls.Add(this.txtSender);
			this.grpMail.Controls.Add(this.label3);
			this.grpMail.Controls.Add(this.label2);
			this.grpMail.Controls.Add(this.label1);
			this.grpMail.Location = new System.Drawing.Point(15, 102);
			this.grpMail.Name = "grpMail";
			this.grpMail.Size = new System.Drawing.Size(494, 443);
			this.grpMail.TabIndex = 12;
			this.grpMail.TabStop = false;
			this.grpMail.Text = "Mail Item";
			// 
			// htmlBox
			// 
			this.htmlBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.htmlBox.HtmlText = resources.GetString("htmlBox.HtmlText");
			this.htmlBox.Location = new System.Drawing.Point(9, 121);
			this.htmlBox.Name = "htmlBox";
			this.htmlBox.Size = new System.Drawing.Size(479, 254);
			this.htmlBox.TabIndex = 22;
			this.htmlBox.UrlString = "about:blank";
			// 
			// txtSubject
			// 
			this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSubject.BackColor = System.Drawing.SystemColors.Info;
			this.txtSubject.Location = new System.Drawing.Point(67, 76);
			this.txtSubject.Name = "txtSubject";
			this.txtSubject.Size = new System.Drawing.Size(421, 20);
			this.txtSubject.TabIndex = 21;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(15, 79);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(46, 13);
			this.label5.TabIndex = 20;
			this.label5.Text = "Subject:";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 378);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(69, 13);
			this.label4.TabIndex = 19;
			this.label4.Text = "Attachments:";
			// 
			// listAttaches
			// 
			this.listAttaches.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listAttaches.BackColor = System.Drawing.SystemColors.InactiveBorder;
			this.listAttaches.FormattingEnabled = true;
			this.listAttaches.Location = new System.Drawing.Point(9, 394);
			this.listAttaches.Name = "listAttaches";
			this.listAttaches.Size = new System.Drawing.Size(479, 43);
			this.listAttaches.TabIndex = 18;
			// 
			// txtCC
			// 
			this.txtCC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtCC.Location = new System.Drawing.Point(67, 50);
			this.txtCC.Name = "txtCC";
			this.txtCC.Size = new System.Drawing.Size(421, 20);
			this.txtCC.TabIndex = 17;
			// 
			// txtSender
			// 
			this.txtSender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSender.Location = new System.Drawing.Point(67, 23);
			this.txtSender.Name = "txtSender";
			this.txtSender.Size = new System.Drawing.Size(421, 20);
			this.txtSender.TabIndex = 16;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(37, 53);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(24, 13);
			this.label3.TabIndex = 15;
			this.label3.Text = "CC:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(17, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 13);
			this.label2.TabIndex = 14;
			this.label2.Text = "Sender:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 105);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(69, 13);
			this.label1.TabIndex = 13;
			this.label1.Text = "Mail Content:";
			// 
			// btnSendMail
			// 
			this.btnSendMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSendMail.Location = new System.Drawing.Point(434, 551);
			this.btnSendMail.Name = "btnSendMail";
			this.btnSendMail.Size = new System.Drawing.Size(75, 23);
			this.btnSendMail.TabIndex = 13;
			this.btnSendMail.Text = "Send";
			this.btnSendMail.UseVisualStyleBackColor = true;
			this.btnSendMail.Click += new System.EventHandler(this.btnSendMail_Click);
			// 
			// grpXL
			// 
			this.grpXL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.grpXL.Controls.Add(this.btnOpenBook);
			this.grpXL.Controls.Add(this.btnLoadTemplate);
			this.grpXL.Controls.Add(this.label6);
			this.grpXL.Controls.Add(this.cbSheets);
			this.grpXL.Controls.Add(this.fopenXL);
			this.grpXL.Location = new System.Drawing.Point(15, 10);
			this.grpXL.Name = "grpXL";
			this.grpXL.Size = new System.Drawing.Size(494, 86);
			this.grpXL.TabIndex = 14;
			this.grpXL.TabStop = false;
			this.grpXL.Text = "Template Information";
			// 
			// btnOpenBook
			// 
			this.btnOpenBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpenBook.Location = new System.Drawing.Point(419, 20);
			this.btnOpenBook.Name = "btnOpenBook";
			this.btnOpenBook.Size = new System.Drawing.Size(61, 23);
			this.btnOpenBook.TabIndex = 24;
			this.btnOpenBook.Text = "Open";
			this.btnOpenBook.UseVisualStyleBackColor = true;
			this.btnOpenBook.Click += new System.EventHandler(this.btnOpenBook_Click);
			// 
			// btnLoadTemplate
			// 
			this.btnLoadTemplate.Location = new System.Drawing.Point(198, 49);
			this.btnLoadTemplate.Name = "btnLoadTemplate";
			this.btnLoadTemplate.Size = new System.Drawing.Size(119, 23);
			this.btnLoadTemplate.TabIndex = 23;
			this.btnLoadTemplate.Text = "Load Template";
			this.btnLoadTemplate.UseVisualStyleBackColor = true;
			this.btnLoadTemplate.Click += new System.EventHandler(this.btnLoadTemplate_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(17, 54);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(44, 13);
			this.label6.TabIndex = 22;
			this.label6.Text = "Sender:";
			// 
			// cbSheets
			// 
			this.cbSheets.FormattingEnabled = true;
			this.cbSheets.Location = new System.Drawing.Point(67, 51);
			this.cbSheets.Name = "cbSheets";
			this.cbSheets.Size = new System.Drawing.Size(125, 21);
			this.cbSheets.TabIndex = 12;
			// 
			// fopenXL
			// 
			this.fopenXL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.fopenXL.FullPath = "";
			this.fopenXL.Location = new System.Drawing.Point(6, 19);
			this.fopenXL.Name = "fopenXL";
			this.fopenXL.Size = new System.Drawing.Size(415, 26);
			this.fopenXL.TabIndex = 11;
			// 
			// MailGenerator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(521, 590);
			this.Controls.Add(this.grpXL);
			this.Controls.Add(this.btnSendMail);
			this.Controls.Add(this.grpMail);
			this.Controls.Add(this.btnGenerate);
			this.Name = "MailGenerator";
			this.Text = "Outlook Mail Generator";
			this.grpMail.ResumeLayout(false);
			this.grpMail.PerformLayout();
			this.grpXL.ResumeLayout(false);
			this.grpXL.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnGenerate;
		private System.Windows.Forms.GroupBox grpMail;
		private System.Windows.Forms.TextBox txtCC;
		private System.Windows.Forms.TextBox txtSender;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListBox listAttaches;
		private System.Windows.Forms.Button btnSendMail;
		private System.Windows.Forms.TextBox txtSubject;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox grpXL;
		private TestOffice.FileOpener fopenXL;
		private System.Windows.Forms.Button btnLoadTemplate;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cbSheets;
		private Fs.UI.Controls.HtmlBox htmlBox;
		private System.Windows.Forms.Button btnOpenBook;

	}
}