namespace Dw.Plugins
{
	partial class CoderForm
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
			this.btnOpen = new System.Windows.Forms.Button();
			this.btnRun = new System.Windows.Forms.Button();
			this.txtFileName = new System.Windows.Forms.TextBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.dlgOf = new System.Windows.Forms.OpenFileDialog();
			this.dlgSf = new System.Windows.Forms.SaveFileDialog();
			this.txtEncoded = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// btnOpen
			// 
			this.btnOpen.Location = new System.Drawing.Point(461, 12);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(29, 23);
			this.btnOpen.TabIndex = 0;
			this.btnOpen.Text = "...";
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// btnRun
			// 
			this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRun.Location = new System.Drawing.Point(415, 41);
			this.btnRun.Name = "btnRun";
			this.btnRun.Size = new System.Drawing.Size(75, 23);
			this.btnRun.TabIndex = 1;
			this.btnRun.Text = "Run";
			this.btnRun.UseVisualStyleBackColor = true;
			this.btnRun.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRun_MouseClick);
			// 
			// txtFileName
			// 
			this.txtFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.txtFileName.Location = new System.Drawing.Point(12, 12);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.Size = new System.Drawing.Size(443, 23);
			this.txtFileName.TabIndex = 2;
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(415, 70);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 3;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(0, 13);
			this.label1.TabIndex = 4;
			// 
			// dlgOf
			// 
			this.dlgOf.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
			this.dlgOf.FilterIndex = 2;
			// 
			// dlgSf
			// 
			this.dlgSf.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
			// 
			// txtEncoded
			// 
			this.txtEncoded.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtEncoded.Location = new System.Drawing.Point(12, 43);
			this.txtEncoded.Margin = new System.Windows.Forms.Padding(0);
			this.txtEncoded.Name = "txtEncoded";
			this.txtEncoded.Size = new System.Drawing.Size(397, 248);
			this.txtEncoded.TabIndex = 5;
			this.txtEncoded.Text = "";
			this.txtEncoded.TextChanged += new System.EventHandler(this.txtEncoded_TextChanged);
			// 
			// CoderForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(502, 303);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.txtFileName);
			this.Controls.Add(this.btnRun);
			this.Controls.Add(this.btnOpen);
			this.Controls.Add(this.txtEncoded);
			this.Name = "CoderForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Foder";
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.CoderForm_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.CoderForm_DragEnter);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.Button btnRun;
		private System.Windows.Forms.TextBox txtFileName;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.OpenFileDialog dlgOf;
		private System.Windows.Forms.SaveFileDialog dlgSf;
		private System.Windows.Forms.RichTextBox txtEncoded;
	}
}