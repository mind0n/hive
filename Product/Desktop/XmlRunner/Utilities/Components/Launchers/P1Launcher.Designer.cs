namespace Utilities.Components.Launchers
{
    partial class P1Launcher
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P1Launcher));
            this.lvMain = new System.Windows.Forms.ListView();
            this.imgs = new System.Windows.Forms.ImageList(this.components);
            this.pnMain = new System.Windows.Forms.Panel();
            this.bConfig = new System.Windows.Forms.Button();
            this.cmBranch = new System.Windows.Forms.ComboBox();
            this.bClear = new System.Windows.Forms.Button();
            this.pnMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMain
            // 
            this.lvMain.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMain.AutoArrange = false;
            this.lvMain.BackgroundImageTiled = true;
            this.lvMain.GridLines = true;
            this.lvMain.LargeImageList = this.imgs;
            this.lvMain.Location = new System.Drawing.Point(3, 32);
            this.lvMain.MultiSelect = false;
            this.lvMain.Name = "lvMain";
            this.lvMain.Size = new System.Drawing.Size(641, 443);
            this.lvMain.SmallImageList = this.imgs;
            this.lvMain.TabIndex = 0;
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.VirtualListSize = 64;
            // 
            // imgs
            // 
            this.imgs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgs.ImageStream")));
            this.imgs.TransparentColor = System.Drawing.Color.Transparent;
            this.imgs.Images.SetKeyName(0, "BlackM.png");
            this.imgs.Images.SetKeyName(1, "BlueE.png");
            this.imgs.Images.SetKeyName(2, "IconV.PNG");
            // 
            // pnMain
            // 
            this.pnMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnMain.Controls.Add(this.bClear);
            this.pnMain.Controls.Add(this.bConfig);
            this.pnMain.Controls.Add(this.cmBranch);
            this.pnMain.Controls.Add(this.lvMain);
            this.pnMain.Location = new System.Drawing.Point(14, 12);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(648, 478);
            this.pnMain.TabIndex = 1;
            // 
            // bConfig
            // 
            this.bConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bConfig.Location = new System.Drawing.Point(531, 3);
            this.bConfig.Name = "bConfig";
            this.bConfig.Size = new System.Drawing.Size(113, 23);
            this.bConfig.TabIndex = 5;
            this.bConfig.Text = "Config";
            this.bConfig.UseVisualStyleBackColor = true;
            this.bConfig.Click += new System.EventHandler(this.bConfig_Click);
            // 
            // cmBranch
            // 
            this.cmBranch.FormattingEnabled = true;
            this.cmBranch.Items.AddRange(new object[] {
            "P1-main",
            "3.2-main",
            "3.1.2-main",
            "3.1.2_ps_release"});
            this.cmBranch.Location = new System.Drawing.Point(3, 4);
            this.cmBranch.Name = "cmBranch";
            this.cmBranch.Size = new System.Drawing.Size(121, 22);
            this.cmBranch.TabIndex = 1;
            // 
            // bClear
            // 
            this.bClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bClear.Location = new System.Drawing.Point(450, 3);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(75, 23);
            this.bClear.TabIndex = 6;
            this.bClear.Text = "Clear";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // P1Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 502);
            this.Controls.Add(this.pnMain);
            this.Name = "P1Launcher";
            this.Text = "CADClientsLauncher";
            this.pnMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvMain;
        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.ComboBox cmBranch;
        private System.Windows.Forms.ImageList imgs;
        private System.Windows.Forms.Button bConfig;
        private System.Windows.Forms.Button bClear;

    }
}