using ULib.Controls;
namespace ULib.Forms
{
    partial class ConfigScreen
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
			this.lbCaption = new System.Windows.Forms.Label();
			this.sp = new System.Windows.Forms.Panel();
			this.pn = new System.Windows.Forms.Panel();
			this.bClose = new System.Windows.Forms.LinkLabel();
			this.pnRect = new System.Windows.Forms.Panel();
			this.sp.SuspendLayout();
			this.SuspendLayout();
			// 
			// lbCaption
			// 
			this.lbCaption.AutoSize = true;
			this.lbCaption.Location = new System.Drawing.Point(2, 3);
			this.lbCaption.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbCaption.Name = "lbCaption";
			this.lbCaption.Size = new System.Drawing.Size(16, 13);
			this.lbCaption.TabIndex = 1;
			this.lbCaption.Text = "   ";
			// 
			// sp
			// 
			this.sp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sp.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.sp.Controls.Add(this.pn);
			this.sp.Location = new System.Drawing.Point(2, 23);
			this.sp.Name = "sp";
			this.sp.Size = new System.Drawing.Size(588, 90);
			this.sp.TabIndex = 3;
			// 
			// pn
			// 
			this.pn.AutoScroll = true;
			this.pn.BackColor = System.Drawing.SystemColors.HighlightText;
			this.pn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pn.Location = new System.Drawing.Point(0, 0);
			this.pn.Name = "pn";
			this.pn.Size = new System.Drawing.Size(588, 90);
			this.pn.TabIndex = 0;
			// 
			// bClose
			// 
			this.bClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bClose.AutoSize = true;
			this.bClose.LinkArea = new System.Windows.Forms.LinkArea(0, 2);
			this.bClose.LinkColor = System.Drawing.Color.Black;
			this.bClose.Location = new System.Drawing.Point(578, 3);
			this.bClose.Name = "bClose";
			this.bClose.Size = new System.Drawing.Size(10, 17);
			this.bClose.TabIndex = 4;
			this.bClose.TabStop = true;
			this.bClose.Text = "x";
			this.bClose.UseCompatibleTextRendering = true;
			this.bClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.bClose_LinkClicked);
			// 
			// pnRect
			// 
			this.pnRect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnRect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.pnRect.Location = new System.Drawing.Point(5, 7);
			this.pnRect.Name = "pnRect";
			this.pnRect.Size = new System.Drawing.Size(583, 6);
			this.pnRect.TabIndex = 5;
			// 
			// ConfigScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.HighlightText;
			this.ClientSize = new System.Drawing.Size(592, 116);
			this.Controls.Add(this.bClose);
			this.Controls.Add(this.lbCaption);
			this.Controls.Add(this.sp);
			this.Controls.Add(this.pnRect);
			this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.Name = "ConfigScreen";
			this.Text = "ConfigScreen";
			this.sp.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbCaption;
        private System.Windows.Forms.Panel sp;
        private System.Windows.Forms.LinkLabel bClose;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.Panel pn;
        private System.Windows.Forms.Panel pnRect;


    }
}