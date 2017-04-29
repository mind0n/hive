namespace ULib.Controls
{
    partial class PathBrowser
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
			this.bBrowse = new System.Windows.Forms.Button();
			this.tPath = new System.Windows.Forms.ComboBox();
			this.folderDlg = new System.Windows.Forms.FolderBrowserDialog();
			this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
			this.spMain = new System.Windows.Forms.SplitContainer();
			this.spMain.Panel1.SuspendLayout();
			this.spMain.Panel2.SuspendLayout();
			this.spMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// bBrowse
			// 
			this.bBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bBrowse.Location = new System.Drawing.Point(7, 1);
			this.bBrowse.Margin = new System.Windows.Forms.Padding(0);
			this.bBrowse.Name = "bBrowse";
			this.bBrowse.Size = new System.Drawing.Size(24, 18);
			this.bBrowse.TabIndex = 0;
			this.bBrowse.TabStop = false;
			this.bBrowse.Text = "...";
			this.bBrowse.UseVisualStyleBackColor = true;
			this.bBrowse.Click += new System.EventHandler(this.bBrowse_Click);
			// 
			// tPath
			// 
			this.tPath.AllowDrop = true;
			this.tPath.Dock = System.Windows.Forms.DockStyle.Top;
			this.tPath.FormattingEnabled = true;
			this.tPath.Location = new System.Drawing.Point(0, 0);
			this.tPath.Name = "tPath";
			this.tPath.Size = new System.Drawing.Size(140, 20);
			this.tPath.TabIndex = 0;
			this.tPath.SelectedIndexChanged += new System.EventHandler(this.tPath_SelectedIndexChanged);
			this.tPath.TextChanged += new System.EventHandler(this.tPath_TextChanged);
			// 
			// spMain
			// 
			this.spMain.AllowDrop = true;
			this.spMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.spMain.IsSplitterFixed = true;
			this.spMain.Location = new System.Drawing.Point(0, 0);
			this.spMain.Margin = new System.Windows.Forms.Padding(0);
			this.spMain.Name = "spMain";
			// 
			// spMain.Panel1
			// 
			this.spMain.Panel1.AllowDrop = true;
			this.spMain.Panel1.Controls.Add(this.tPath);
			// 
			// spMain.Panel2
			// 
			this.spMain.Panel2.Controls.Add(this.bBrowse);
			this.spMain.Size = new System.Drawing.Size(175, 23);
			this.spMain.SplitterDistance = 140;
			this.spMain.TabIndex = 2;
			// 
			// PathBrowser
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.spMain);
			this.Name = "PathBrowser";
			this.Size = new System.Drawing.Size(175, 23);
			this.spMain.Panel1.ResumeLayout(false);
			this.spMain.Panel2.ResumeLayout(false);
			this.spMain.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bBrowse;
        private System.Windows.Forms.ComboBox tPath;
        private System.Windows.Forms.FolderBrowserDialog folderDlg;
        private System.Windows.Forms.OpenFileDialog openFileDlg;
        private System.Windows.Forms.SplitContainer spMain;
    }
}
