namespace Utilities.Components.WCF
{
    partial class WcfClient
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
            this.thistory = new System.Windows.Forms.TextBox();
            this.bsend = new System.Windows.Forms.Button();
            this.tchat = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // thistory
            // 
            this.thistory.Location = new System.Drawing.Point(12, 12);
            this.thistory.Multiline = true;
            this.thistory.Name = "thistory";
            this.thistory.Size = new System.Drawing.Size(410, 382);
            this.thistory.TabIndex = 0;
            this.thistory.TabStop = false;
            // 
            // bsend
            // 
            this.bsend.Location = new System.Drawing.Point(347, 400);
            this.bsend.Name = "bsend";
            this.bsend.Size = new System.Drawing.Size(75, 47);
            this.bsend.TabIndex = 1;
            this.bsend.Text = "Send";
            this.bsend.UseVisualStyleBackColor = true;
            this.bsend.Click += new System.EventHandler(this.bsend_Click);
            // 
            // tchat
            // 
            this.tchat.Location = new System.Drawing.Point(12, 400);
            this.tchat.Multiline = true;
            this.tchat.Name = "tchat";
            this.tchat.Size = new System.Drawing.Size(329, 47);
            this.tchat.TabIndex = 2;
            // 
            // WcfClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 459);
            this.Controls.Add(this.tchat);
            this.Controls.Add(this.bsend);
            this.Controls.Add(this.thistory);
            this.Name = "WcfClient";
            this.Text = "WcfClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox thistory;
        private System.Windows.Forms.Button bsend;
        private System.Windows.Forms.TextBox tchat;
    }
}