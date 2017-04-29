namespace TestRoom.VirtualDesktop
{
    partial class ServiceForm
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
            this.bCreate = new System.Windows.Forms.Button();
            this.bStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bCreate
            // 
            this.bCreate.Location = new System.Drawing.Point(12, 12);
            this.bCreate.Name = "bCreate";
            this.bCreate.Size = new System.Drawing.Size(126, 23);
            this.bCreate.TabIndex = 0;
            this.bCreate.Text = "Create Service";
            this.bCreate.UseVisualStyleBackColor = true;
            this.bCreate.Click += new System.EventHandler(this.bCreate_Click);
            // 
            // bStart
            // 
            this.bStart.Location = new System.Drawing.Point(144, 12);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(126, 23);
            this.bStart.TabIndex = 0;
            this.bStart.Text = "Start Service";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // ServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 47);
            this.Controls.Add(this.bStart);
            this.Controls.Add(this.bCreate);
            this.Name = "ServiceForm";
            this.Text = "ServiceForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bCreate;
        private System.Windows.Forms.Button bStart;
    }
}