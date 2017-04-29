namespace TestRoom.Wcf
{
    partial class WcfForm
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
            this.bStartWcfHost = new System.Windows.Forms.Button();
            this.bPipe = new System.Windows.Forms.Button();
            this.box = new System.Windows.Forms.TextBox();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tbScreenshot = new System.Windows.Forms.TabPage();
            this.pic = new System.Windows.Forms.PictureBox();
            this.tbTxt = new System.Windows.Forms.TabPage();
            this.bWebHttp = new System.Windows.Forms.Button();
            this.bViewer = new System.Windows.Forms.Button();
            this.bStopSvc = new System.Windows.Forms.Button();
            this.tabs.SuspendLayout();
            this.tbScreenshot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.tbTxt.SuspendLayout();
            this.SuspendLayout();
            // 
            // bStartWcfHost
            // 
            this.bStartWcfHost.Location = new System.Drawing.Point(12, 12);
            this.bStartWcfHost.Name = "bStartWcfHost";
            this.bStartWcfHost.Size = new System.Drawing.Size(126, 23);
            this.bStartWcfHost.TabIndex = 0;
            this.bStartWcfHost.Text = "Start Wcf Host";
            this.bStartWcfHost.UseVisualStyleBackColor = true;
            this.bStartWcfHost.Click += new System.EventHandler(this.bStartWcfHost_Click);
            // 
            // bPipe
            // 
            this.bPipe.Location = new System.Drawing.Point(12, 41);
            this.bPipe.Name = "bPipe";
            this.bPipe.Size = new System.Drawing.Size(126, 23);
            this.bPipe.TabIndex = 1;
            this.bPipe.Text = "Named Pipe Bind";
            this.bPipe.UseVisualStyleBackColor = true;
            this.bPipe.Click += new System.EventHandler(this.bPipe_Click);
            // 
            // box
            // 
            this.box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.box.Location = new System.Drawing.Point(3, 3);
            this.box.Multiline = true;
            this.box.Name = "box";
            this.box.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.box.Size = new System.Drawing.Size(652, 472);
            this.box.TabIndex = 2;
            // 
            // tabs
            // 
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Controls.Add(this.tbScreenshot);
            this.tabs.Controls.Add(this.tbTxt);
            this.tabs.Location = new System.Drawing.Point(144, 12);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(666, 508);
            this.tabs.TabIndex = 3;
            // 
            // tbScreenshot
            // 
            this.tbScreenshot.Controls.Add(this.pic);
            this.tbScreenshot.Location = new System.Drawing.Point(4, 22);
            this.tbScreenshot.Name = "tbScreenshot";
            this.tbScreenshot.Padding = new System.Windows.Forms.Padding(3);
            this.tbScreenshot.Size = new System.Drawing.Size(658, 482);
            this.tbScreenshot.TabIndex = 0;
            this.tbScreenshot.Text = "Screenshot";
            this.tbScreenshot.UseVisualStyleBackColor = true;
            // 
            // pic
            // 
            this.pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic.Location = new System.Drawing.Point(3, 3);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(652, 476);
            this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            // 
            // tbTxt
            // 
            this.tbTxt.Controls.Add(this.box);
            this.tbTxt.Location = new System.Drawing.Point(4, 22);
            this.tbTxt.Name = "tbTxt";
            this.tbTxt.Padding = new System.Windows.Forms.Padding(3);
            this.tbTxt.Size = new System.Drawing.Size(658, 478);
            this.tbTxt.TabIndex = 1;
            this.tbTxt.Text = "Output";
            this.tbTxt.UseVisualStyleBackColor = true;
            // 
            // bWebHttp
            // 
            this.bWebHttp.Location = new System.Drawing.Point(12, 70);
            this.bWebHttp.Name = "bWebHttp";
            this.bWebHttp.Size = new System.Drawing.Size(126, 23);
            this.bWebHttp.TabIndex = 4;
            this.bWebHttp.Text = "Web Http Bind";
            this.bWebHttp.UseVisualStyleBackColor = true;
            this.bWebHttp.Click += new System.EventHandler(this.bWebHttp_Click);
            // 
            // bViewer
            // 
            this.bViewer.Location = new System.Drawing.Point(12, 99);
            this.bViewer.Name = "bViewer";
            this.bViewer.Size = new System.Drawing.Size(126, 23);
            this.bViewer.TabIndex = 5;
            this.bViewer.Text = "Desk Viewer";
            this.bViewer.UseVisualStyleBackColor = true;
            this.bViewer.Click += new System.EventHandler(this.bViewer_Click);
            // 
            // bStopSvc
            // 
            this.bStopSvc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bStopSvc.Location = new System.Drawing.Point(12, 497);
            this.bStopSvc.Name = "bStopSvc";
            this.bStopSvc.Size = new System.Drawing.Size(126, 23);
            this.bStopSvc.TabIndex = 6;
            this.bStopSvc.Text = "Stop Service";
            this.bStopSvc.UseVisualStyleBackColor = true;
            this.bStopSvc.Click += new System.EventHandler(this.bStopSvc_Click);
            // 
            // WcfForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 532);
            this.Controls.Add(this.bStopSvc);
            this.Controls.Add(this.bViewer);
            this.Controls.Add(this.bWebHttp);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.bPipe);
            this.Controls.Add(this.bStartWcfHost);
            this.Name = "WcfForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WcfForm";
            this.tabs.ResumeLayout(false);
            this.tbScreenshot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.tbTxt.ResumeLayout(false);
            this.tbTxt.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bStartWcfHost;
        private System.Windows.Forms.Button bPipe;
        private System.Windows.Forms.TextBox box;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tbScreenshot;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.TabPage tbTxt;
        private System.Windows.Forms.Button bWebHttp;
        private System.Windows.Forms.Button bViewer;
        private System.Windows.Forms.Button bStopSvc;
    }
}