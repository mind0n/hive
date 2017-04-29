namespace TestRoom.Testing
{
    partial class Testform
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
            this.bCfg = new System.Windows.Forms.Button();
            this.bStartProxy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bCfg
            // 
            this.bCfg.Location = new System.Drawing.Point(12, 41);
            this.bCfg.Name = "bCfg";
            this.bCfg.Size = new System.Drawing.Size(75, 23);
            this.bCfg.TabIndex = 0;
            this.bCfg.Text = "Test Cfg";
            this.bCfg.UseVisualStyleBackColor = true;
            this.bCfg.Click += new System.EventHandler(this.bCfg_Click);
            // 
            // bStartProxy
            // 
            this.bStartProxy.Location = new System.Drawing.Point(12, 12);
            this.bStartProxy.Name = "bStartProxy";
            this.bStartProxy.Size = new System.Drawing.Size(75, 23);
            this.bStartProxy.TabIndex = 1;
            this.bStartProxy.Text = "Start Proxy";
            this.bStartProxy.UseVisualStyleBackColor = true;
            this.bStartProxy.Click += new System.EventHandler(this.bStartProxy_Click);
            // 
            // Testform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.bStartProxy);
            this.Controls.Add(this.bCfg);
            this.Name = "Testform";
            this.Text = "Testform";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bCfg;
        private System.Windows.Forms.Button bStartProxy;
    }
}