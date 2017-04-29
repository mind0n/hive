namespace WebOperator
{
	partial class MasterForm
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
			this.tAddress = new System.Windows.Forms.ComboBox();
			this.bGo = new System.Windows.Forms.Button();
			this.spnMain = new System.Windows.Forms.SplitContainer();
			this.browser = new System.Windows.Forms.WebBrowser();
			this.bFillQuery = new System.Windows.Forms.Button();
			this.bAttach = new System.Windows.Forms.Button();
			this.bFillMark = new System.Windows.Forms.Button();
			this.status = new System.Windows.Forms.ComboBox();
			this.lDebug = new System.Windows.Forms.Label();
			this.bHome = new System.Windows.Forms.Button();
			this.bQuery = new System.Windows.Forms.Button();
			this.bBook = new System.Windows.Forms.Button();
			this.bConfirm = new System.Windows.Forms.Button();
			this.spnMain.Panel1.SuspendLayout();
			this.spnMain.Panel2.SuspendLayout();
			this.spnMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// tAddress
			// 
			this.tAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tAddress.FormattingEnabled = true;
			this.tAddress.Location = new System.Drawing.Point(12, 12);
			this.tAddress.Name = "tAddress";
			this.tAddress.Size = new System.Drawing.Size(461, 21);
			this.tAddress.TabIndex = 0;
			// 
			// bGo
			// 
			this.bGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bGo.Location = new System.Drawing.Point(479, 10);
			this.bGo.Name = "bGo";
			this.bGo.Size = new System.Drawing.Size(29, 23);
			this.bGo.TabIndex = 1;
			this.bGo.Text = "Go";
			this.bGo.UseVisualStyleBackColor = true;
			this.bGo.Click += new System.EventHandler(this.bGo_Click);
			// 
			// spnMain
			// 
			this.spnMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.spnMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.spnMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.spnMain.Location = new System.Drawing.Point(12, 39);
			this.spnMain.Name = "spnMain";
			// 
			// spnMain.Panel1
			// 
			this.spnMain.Panel1.Controls.Add(this.browser);
			// 
			// spnMain.Panel2
			// 
			this.spnMain.Panel2.Controls.Add(this.bFillQuery);
			this.spnMain.Panel2.Controls.Add(this.bAttach);
			this.spnMain.Panel2.Controls.Add(this.bFillMark);
			this.spnMain.Size = new System.Drawing.Size(658, 418);
			this.spnMain.SplitterDistance = 544;
			this.spnMain.TabIndex = 2;
			// 
			// browser
			// 
			this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.browser.Location = new System.Drawing.Point(0, 0);
			this.browser.MinimumSize = new System.Drawing.Size(20, 20);
			this.browser.Name = "browser";
			this.browser.Size = new System.Drawing.Size(542, 416);
			this.browser.TabIndex = 0;
			// 
			// bFillQuery
			// 
			this.bFillQuery.Location = new System.Drawing.Point(16, 32);
			this.bFillQuery.Name = "bFillQuery";
			this.bFillQuery.Size = new System.Drawing.Size(75, 23);
			this.bFillQuery.TabIndex = 2;
			this.bFillQuery.Text = "Query";
			this.bFillQuery.UseVisualStyleBackColor = true;
			this.bFillQuery.Click += new System.EventHandler(this.bFillQuery_Click);
			// 
			// bAttach
			// 
			this.bAttach.Location = new System.Drawing.Point(16, 61);
			this.bAttach.Name = "bAttach";
			this.bAttach.Size = new System.Drawing.Size(75, 23);
			this.bAttach.TabIndex = 1;
			this.bAttach.Text = "Book";
			this.bAttach.UseVisualStyleBackColor = true;
			this.bAttach.Click += new System.EventHandler(this.bBookForm_Click);
			// 
			// bFillMark
			// 
			this.bFillMark.Location = new System.Drawing.Point(16, 3);
			this.bFillMark.Name = "bFillMark";
			this.bFillMark.Size = new System.Drawing.Size(75, 23);
			this.bFillMark.TabIndex = 0;
			this.bFillMark.Text = "Login";
			this.bFillMark.UseVisualStyleBackColor = true;
			this.bFillMark.Click += new System.EventHandler(this.bFillMark_Click);
			// 
			// status
			// 
			this.status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.status.FormattingEnabled = true;
			this.status.Location = new System.Drawing.Point(12, 463);
			this.status.Name = "status";
			this.status.Size = new System.Drawing.Size(595, 21);
			this.status.TabIndex = 3;
			// 
			// lDebug
			// 
			this.lDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lDebug.AutoSize = true;
			this.lDebug.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lDebug.Location = new System.Drawing.Point(613, 466);
			this.lDebug.MinimumSize = new System.Drawing.Size(54, 2);
			this.lDebug.Name = "lDebug";
			this.lDebug.Size = new System.Drawing.Size(54, 15);
			this.lDebug.TabIndex = 4;
			this.lDebug.Text = " Debug";
			this.lDebug.Click += new System.EventHandler(this.lDebug_Click);
			// 
			// bHome
			// 
			this.bHome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bHome.Location = new System.Drawing.Point(514, 10);
			this.bHome.Name = "bHome";
			this.bHome.Size = new System.Drawing.Size(25, 23);
			this.bHome.TabIndex = 5;
			this.bHome.Text = "H";
			this.bHome.UseVisualStyleBackColor = true;
			this.bHome.Click += new System.EventHandler(this.bHome_Click);
			// 
			// bQuery
			// 
			this.bQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bQuery.Location = new System.Drawing.Point(545, 10);
			this.bQuery.Name = "bQuery";
			this.bQuery.Size = new System.Drawing.Size(43, 23);
			this.bQuery.TabIndex = 6;
			this.bQuery.Text = "Q";
			this.bQuery.UseVisualStyleBackColor = true;
			this.bQuery.Click += new System.EventHandler(this.bQuery_Click);
			// 
			// bBook
			// 
			this.bBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bBook.Location = new System.Drawing.Point(594, 10);
			this.bBook.Name = "bBook";
			this.bBook.Size = new System.Drawing.Size(38, 23);
			this.bBook.TabIndex = 7;
			this.bBook.Text = "B";
			this.bBook.UseVisualStyleBackColor = true;
			this.bBook.Click += new System.EventHandler(this.bBook_Click);
			// 
			// bConfirm
			// 
			this.bConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bConfirm.Location = new System.Drawing.Point(638, 10);
			this.bConfirm.Name = "bConfirm";
			this.bConfirm.Size = new System.Drawing.Size(32, 23);
			this.bConfirm.TabIndex = 8;
			this.bConfirm.Text = "C";
			this.bConfirm.UseVisualStyleBackColor = true;
			// 
			// MasterForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(682, 496);
			this.Controls.Add(this.bConfirm);
			this.Controls.Add(this.bBook);
			this.Controls.Add(this.bQuery);
			this.Controls.Add(this.bHome);
			this.Controls.Add(this.lDebug);
			this.Controls.Add(this.status);
			this.Controls.Add(this.spnMain);
			this.Controls.Add(this.bGo);
			this.Controls.Add(this.tAddress);
			this.Name = "MasterForm";
			this.Text = "MasterForm";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.spnMain.Panel1.ResumeLayout(false);
			this.spnMain.Panel2.ResumeLayout(false);
			this.spnMain.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox tAddress;
		private System.Windows.Forms.Button bGo;
		private System.Windows.Forms.SplitContainer spnMain;
		private System.Windows.Forms.ComboBox status;
		private System.Windows.Forms.Label lDebug;
		private System.Windows.Forms.WebBrowser browser;
		private System.Windows.Forms.Button bFillQuery;
		private System.Windows.Forms.Button bAttach;
		private System.Windows.Forms.Button bHome;
		private System.Windows.Forms.Button bFillMark;
		private System.Windows.Forms.Button bQuery;
		private System.Windows.Forms.Button bBook;
		private System.Windows.Forms.Button bConfirm;
	}
}