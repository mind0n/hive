namespace TestOffice
{
	partial class FileOpener
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnOpen = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.txtFilenameOld = new System.Windows.Forms.TextBox();
			this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
			this.txtFilename = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// btnOpen
			// 
			this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpen.Location = new System.Drawing.Point(451, 1);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(23, 23);
			this.btnOpen.TabIndex = 5;
			this.btnOpen.Text = "...";
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 6);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(52, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Filename:";
			// 
			// txtFilenameOld
			// 
			this.txtFilenameOld.Location = new System.Drawing.Point(0, 0);
			this.txtFilenameOld.Name = "txtFilenameOld";
			this.txtFilenameOld.Size = new System.Drawing.Size(100, 20);
			this.txtFilenameOld.TabIndex = 0;
			// 
			// dlgOpen
			// 
			this.dlgOpen.FileName = "utilmacros.xlsm";
			this.dlgOpen.Filter = "Excel 2007 with Macros (*.xlsm)| *.xlsm|All files (*.*)|*.*";
			// 
			// txtFilename
			// 
			this.txtFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtFilename.FormattingEnabled = true;
			this.txtFilename.Location = new System.Drawing.Point(61, 3);
			this.txtFilename.Name = "txtFilename";
			this.txtFilename.Size = new System.Drawing.Size(384, 21);
			this.txtFilename.TabIndex = 6;
			// 
			// FileOpener
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnOpen);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtFilename);
			this.Name = "FileOpener";
			this.Size = new System.Drawing.Size(477, 27);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtFilenameOld;
		private System.Windows.Forms.OpenFileDialog dlgOpen;
		private System.Windows.Forms.ComboBox txtFilename;
	}
}
