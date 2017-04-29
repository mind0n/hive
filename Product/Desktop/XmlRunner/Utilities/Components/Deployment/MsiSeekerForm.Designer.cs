namespace Utilities.Components.Deployment
{
    partial class MsiSeekerForm
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
            this.pnOutter = new System.Windows.Forms.Panel();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tbInstall = new System.Windows.Forms.TabPage();
            this.pnInstall = new System.Windows.Forms.Panel();
            this.bInstall = new System.Windows.Forms.Button();
            this.tbOutput = new System.Windows.Forms.TabPage();
            this.output = new ULib.Controls.OutputBox();
            this.bFind = new System.Windows.Forms.Button();
            this.lst = new System.Windows.Forms.ListBox();
            this.hcFind = new ULib.Controls.HistoryCombox();
            this.pnOutter.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tbInstall.SuspendLayout();
            this.tbOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnOutter
            // 
            this.pnOutter.Controls.Add(this.tabs);
            this.pnOutter.Controls.Add(this.bFind);
            this.pnOutter.Controls.Add(this.lst);
            this.pnOutter.Controls.Add(this.hcFind);
            this.pnOutter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnOutter.Location = new System.Drawing.Point(0, 0);
            this.pnOutter.Name = "pnOutter";
            this.pnOutter.Size = new System.Drawing.Size(715, 551);
            this.pnOutter.TabIndex = 0;
            // 
            // tabs
            // 
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Controls.Add(this.tbInstall);
            this.tabs.Controls.Add(this.tbOutput);
            this.tabs.Location = new System.Drawing.Point(296, 12);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(407, 517);
            this.tabs.TabIndex = 3;
            // 
            // tbInstall
            // 
            this.tbInstall.Controls.Add(this.pnInstall);
            this.tbInstall.Controls.Add(this.bInstall);
            this.tbInstall.Location = new System.Drawing.Point(4, 22);
            this.tbInstall.Name = "tbInstall";
            this.tbInstall.Padding = new System.Windows.Forms.Padding(3);
            this.tbInstall.Size = new System.Drawing.Size(399, 491);
            this.tbInstall.TabIndex = 0;
            this.tbInstall.Text = "Install";
            this.tbInstall.UseVisualStyleBackColor = true;
            // 
            // pnInstall
            // 
            this.pnInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnInstall.Location = new System.Drawing.Point(6, 6);
            this.pnInstall.Name = "pnInstall";
            this.pnInstall.Size = new System.Drawing.Size(387, 436);
            this.pnInstall.TabIndex = 2;
            // 
            // bInstall
            // 
            this.bInstall.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bInstall.Location = new System.Drawing.Point(3, 448);
            this.bInstall.Name = "bInstall";
            this.bInstall.Size = new System.Drawing.Size(393, 40);
            this.bInstall.TabIndex = 1;
            this.bInstall.Text = "Install";
            this.bInstall.UseVisualStyleBackColor = true;
            this.bInstall.Click += new System.EventHandler(this.bInstall_Click);
            // 
            // tbOutput
            // 
            this.tbOutput.Controls.Add(this.output);
            this.tbOutput.Location = new System.Drawing.Point(4, 22);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tbOutput.Size = new System.Drawing.Size(399, 491);
            this.tbOutput.TabIndex = 1;
            this.tbOutput.Text = "Output";
            this.tbOutput.UseVisualStyleBackColor = true;
            // 
            // output
            // 
            this.output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.output.HideCmdBar = false;
            this.output.Location = new System.Drawing.Point(3, 3);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(393, 485);
            this.output.TabIndex = 0;
            // 
            // bFind
            // 
            this.bFind.Location = new System.Drawing.Point(239, 12);
            this.bFind.Name = "bFind";
            this.bFind.Size = new System.Drawing.Size(51, 21);
            this.bFind.TabIndex = 2;
            this.bFind.Text = "Find";
            this.bFind.UseVisualStyleBackColor = true;
            this.bFind.Click += new System.EventHandler(this.bFind_Click);
            // 
            // lst
            // 
            this.lst.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lst.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst.FormattingEnabled = true;
            this.lst.ItemHeight = 18;
            this.lst.Location = new System.Drawing.Point(12, 39);
            this.lst.Name = "lst";
            this.lst.Size = new System.Drawing.Size(278, 490);
            this.lst.TabIndex = 1;
            this.lst.SelectedIndexChanged += new System.EventHandler(this.lst_SelectedIndexChanged);
            // 
            // hcFind
            // 
            this.hcFind.CacheFile = "C:\\hcFind.dat";
            this.hcFind.FormattingEnabled = true;
            this.hcFind.Items.AddRange(new object[] {
            "3.1.2-main_Installers",
            "3.1.2_ps_release",
            "3.1.7_ps_release",
            "3.1.8_ps_release",
            "3.2-main_Installers",
            "3.2.1-release",
            "p1-main_Installers"});
            this.hcFind.Location = new System.Drawing.Point(12, 12);
            this.hcFind.Name = "hcFind";
            this.hcFind.Size = new System.Drawing.Size(221, 21);
            this.hcFind.TabIndex = 0;
            this.hcFind.SelectedIndexChanged += new System.EventHandler(this.hcFind_SelectedIndexChanged);
            // 
            // MsiSeekerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 551);
            this.Controls.Add(this.pnOutter);
            this.Name = "MsiSeekerForm";
            this.Text = "MsiSeekerForm";
            this.pnOutter.ResumeLayout(false);
            this.tabs.ResumeLayout(false);
            this.tbInstall.ResumeLayout(false);
            this.tbOutput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnOutter;
        private ULib.Controls.HistoryCombox hcFind;
        private System.Windows.Forms.ListBox lst;
        private System.Windows.Forms.Button bFind;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tbInstall;
        private System.Windows.Forms.TabPage tbOutput;
        private System.Windows.Forms.Button bInstall;
        private System.Windows.Forms.Panel pnInstall;
        private ULib.Controls.OutputBox output;

    }
}