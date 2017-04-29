namespace Joy.Windows.Controls
{
    partial class ConfirmBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfirmBox));
            this.pn = new System.Windows.Forms.FlowLayoutPanel();
            this.lbhint = new System.Windows.Forms.Label();
            this.tinput = new System.Windows.Forms.TextBox();
            this.bClose = new System.Windows.Forms.Button();
            this.bConfirm = new System.Windows.Forms.Button();
            this.pn.SuspendLayout();
            this.SuspendLayout();
            // 
            // pn
            // 
            resources.ApplyResources(this.pn, "pn");
            this.pn.Controls.Add(this.lbhint);
            this.pn.Controls.Add(this.tinput);
            this.pn.Name = "pn";
            // 
            // lbhint
            // 
            resources.ApplyResources(this.lbhint, "lbhint");
            this.lbhint.Name = "lbhint";
            // 
            // tinput
            // 
            resources.ApplyResources(this.tinput, "tinput");
            this.tinput.Name = "tinput";
            // 
            // bClose
            // 
            resources.ApplyResources(this.bClose, "bClose");
            this.bClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bClose.Name = "bClose";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // bConfirm
            // 
            resources.ApplyResources(this.bConfirm, "bConfirm");
            this.bConfirm.Name = "bConfirm";
            this.bConfirm.UseVisualStyleBackColor = true;
            this.bConfirm.Click += new System.EventHandler(this.bConfirm_Click);
            // 
            // ConfirmBox
            // 
            this.AcceptButton = this.bConfirm;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bClose;
            this.ControlBox = false;
            this.Controls.Add(this.bConfirm);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.pn);
            this.MaximizeBox = false;
            this.Name = "ConfirmBox";
            this.pn.ResumeLayout(false);
            this.pn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pn;
        private System.Windows.Forms.Label lbhint;
        private System.Windows.Forms.TextBox tinput;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.Button bConfirm;

    }
}