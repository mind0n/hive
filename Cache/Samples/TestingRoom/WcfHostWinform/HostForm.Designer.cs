namespace WcfHostWinform
{
    partial class HostForm
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
            this.bNetTcpBasic = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            this.bBasicHttp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bNetTcpBasic
            // 
            this.bNetTcpBasic.Location = new System.Drawing.Point(12, 12);
            this.bNetTcpBasic.Name = "bNetTcpBasic";
            this.bNetTcpBasic.Size = new System.Drawing.Size(156, 23);
            this.bNetTcpBasic.TabIndex = 0;
            this.bNetTcpBasic.Text = "Net Tcp Host Basic";
            this.bNetTcpBasic.UseVisualStyleBackColor = true;
            this.bNetTcpBasic.Click += new System.EventHandler(this.bNetTcpBasic_Click);
            // 
            // bClose
            // 
            this.bClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bClose.Location = new System.Drawing.Point(1008, 12);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(75, 23);
            this.bClose.TabIndex = 1;
            this.bClose.Text = "Close All";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // bBasicHttp
            // 
            this.bBasicHttp.Location = new System.Drawing.Point(12, 41);
            this.bBasicHttp.Name = "bBasicHttp";
            this.bBasicHttp.Size = new System.Drawing.Size(156, 23);
            this.bBasicHttp.TabIndex = 2;
            this.bBasicHttp.Text = "Basic Http";
            this.bBasicHttp.UseVisualStyleBackColor = true;
            this.bBasicHttp.Click += new System.EventHandler(this.bBasicHttp_Click);
            // 
            // HostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 657);
            this.Controls.Add(this.bBasicHttp);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.bNetTcpBasic);
            this.Name = "HostForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wcf Host";
            this.Load += new System.EventHandler(this.HostForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bNetTcpBasic;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.Button bBasicHttp;
    }
}

