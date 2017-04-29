namespace Joy.Windows.Controls
{
    partial class Deskform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Deskform));
            this.txt = new System.Windows.Forms.TextBox();
            this.bsw = new System.Windows.Forms.Button();
            this.fpn = new System.Windows.Forms.FlowLayoutPanel();
            this.tray = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // txt
            // 
            this.txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt.Location = new System.Drawing.Point(93, 11);
            this.txt.Name = "txt";
            this.txt.Size = new System.Drawing.Size(135, 20);
            this.txt.TabIndex = 0;
            this.txt.Text = "12";
            // 
            // bsw
            // 
            this.bsw.Location = new System.Drawing.Point(12, 9);
            this.bsw.Name = "bsw";
            this.bsw.Size = new System.Drawing.Size(75, 23);
            this.bsw.TabIndex = 1;
            this.bsw.Text = "Switch";
            this.bsw.UseVisualStyleBackColor = true;
            this.bsw.Click += new System.EventHandler(this.bsw_Click);
            // 
            // fpn
            // 
            this.fpn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fpn.Location = new System.Drawing.Point(12, 38);
            this.fpn.Name = "fpn";
            this.fpn.Size = new System.Drawing.Size(216, 228);
            this.fpn.TabIndex = 2;
            // 
            // tray
            // 
            this.tray.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.tray.Icon = ((System.Drawing.Icon)(resources.GetObject("tray.Icon")));
            this.tray.Text = "Switch Desktop";
            this.tray.Visible = true;
            this.tray.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tray_MouseClick);
            // 
            // Deskform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 278);
            this.Controls.Add(this.fpn);
            this.Controls.Add(this.bsw);
            this.Controls.Add(this.txt);
            this.Name = "Deskform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt;
        private System.Windows.Forms.Button bsw;
        private System.Windows.Forms.FlowLayoutPanel fpn;
        private System.Windows.Forms.NotifyIcon tray;
    }
}