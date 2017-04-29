namespace TestRoom
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
            this.bTestVirtualDesktop = new System.Windows.Forms.Button();
            this.bTestLoaderService = new System.Windows.Forms.Button();
            this.bWcf = new System.Windows.Forms.Button();
            this.bTestCfg = new System.Windows.Forms.Button();
            this.bShowTaskbar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bTestVirtualDesktop
            // 
            this.bTestVirtualDesktop.Location = new System.Drawing.Point(12, 12);
            this.bTestVirtualDesktop.Name = "bTestVirtualDesktop";
            this.bTestVirtualDesktop.Size = new System.Drawing.Size(151, 23);
            this.bTestVirtualDesktop.TabIndex = 0;
            this.bTestVirtualDesktop.Text = "Test Virtual Desktop";
            this.bTestVirtualDesktop.UseVisualStyleBackColor = true;
            this.bTestVirtualDesktop.Click += new System.EventHandler(this.bTestVirtualDesktop_Click);
            // 
            // bTestLoaderService
            // 
            this.bTestLoaderService.Location = new System.Drawing.Point(12, 41);
            this.bTestLoaderService.Name = "bTestLoaderService";
            this.bTestLoaderService.Size = new System.Drawing.Size(151, 23);
            this.bTestLoaderService.TabIndex = 1;
            this.bTestLoaderService.Text = "Test Loader Service";
            this.bTestLoaderService.UseVisualStyleBackColor = true;
            this.bTestLoaderService.Click += new System.EventHandler(this.bTestLoaderService_Click);
            // 
            // bWcf
            // 
            this.bWcf.Location = new System.Drawing.Point(12, 70);
            this.bWcf.Name = "bWcf";
            this.bWcf.Size = new System.Drawing.Size(151, 23);
            this.bWcf.TabIndex = 1;
            this.bWcf.Text = "Test Wcf";
            this.bWcf.UseVisualStyleBackColor = true;
            this.bWcf.Click += new System.EventHandler(this.bWcf_Click);
            // 
            // bTestCfg
            // 
            this.bTestCfg.Location = new System.Drawing.Point(12, 99);
            this.bTestCfg.Name = "bTestCfg";
            this.bTestCfg.Size = new System.Drawing.Size(151, 23);
            this.bTestCfg.TabIndex = 2;
            this.bTestCfg.Text = "Test Cfg";
            this.bTestCfg.UseVisualStyleBackColor = true;
            this.bTestCfg.Click += new System.EventHandler(this.bTestCfg_Click);
            // 
            // bShowTaskbar
            // 
            this.bShowTaskbar.Location = new System.Drawing.Point(12, 128);
            this.bShowTaskbar.Name = "bShowTaskbar";
            this.bShowTaskbar.Size = new System.Drawing.Size(151, 23);
            this.bShowTaskbar.TabIndex = 3;
            this.bShowTaskbar.Text = "Show Taskbar";
            this.bShowTaskbar.UseVisualStyleBackColor = true;
            this.bShowTaskbar.Click += new System.EventHandler(this.bShowTaskbar_Click);
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 629);
            this.Controls.Add(this.bShowTaskbar);
            this.Controls.Add(this.bTestCfg);
            this.Controls.Add(this.bWcf);
            this.Controls.Add(this.bTestLoaderService);
            this.Controls.Add(this.bTestVirtualDesktop);
            this.Name = "Mainform";
            this.Text = "Test Room";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bTestVirtualDesktop;
        private System.Windows.Forms.Button bTestLoaderService;
        private System.Windows.Forms.Button bWcf;
        private System.Windows.Forms.Button bTestCfg;
        private System.Windows.Forms.Button bShowTaskbar;
    }
}

