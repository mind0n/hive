namespace Utilities.Components
{
    partial class PID
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
            this.label1 = new System.Windows.Forms.Label();
            this.tCmd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.history = new System.Windows.Forms.WebBrowser();
            this.bAnalyze = new System.Windows.Forms.Button();
            this.txt = new System.Windows.Forms.TextBox();
            this.pnMain = new System.Windows.Forms.Panel();
            this.bClear = new System.Windows.Forms.Button();
            this.pnMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Process ID";
            // 
            // tCmd
            // 
            this.tCmd.Location = new System.Drawing.Point(95, 6);
            this.tCmd.Name = "tCmd";
            this.tCmd.Size = new System.Drawing.Size(100, 22);
            this.tCmd.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "History";
            // 
            // history
            // 
            this.history.AllowNavigation = false;
            this.history.Dock = System.Windows.Forms.DockStyle.Fill;
            this.history.Location = new System.Drawing.Point(0, 0);
            this.history.MinimumSize = new System.Drawing.Size(20, 20);
            this.history.Name = "history";
            this.history.ScriptErrorsSuppressed = true;
            this.history.Size = new System.Drawing.Size(600, 382);
            this.history.TabIndex = 3;
            this.history.Url = new System.Uri("about:blank", System.UriKind.Absolute);
            this.history.WebBrowserShortcutsEnabled = false;
            // 
            // bAnalyze
            // 
            this.bAnalyze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bAnalyze.Location = new System.Drawing.Point(459, 5);
            this.bAnalyze.Name = "bAnalyze";
            this.bAnalyze.Size = new System.Drawing.Size(75, 23);
            this.bAnalyze.TabIndex = 4;
            this.bAnalyze.Text = "Analyze";
            this.bAnalyze.UseVisualStyleBackColor = true;
            this.bAnalyze.Click += new System.EventHandler(this.bAnalyze_Click);
            // 
            // txt
            // 
            this.txt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt.Location = new System.Drawing.Point(0, 0);
            this.txt.Multiline = true;
            this.txt.Name = "txt";
            this.txt.Size = new System.Drawing.Size(600, 382);
            this.txt.TabIndex = 5;
            this.txt.Visible = false;
            // 
            // pnMain
            // 
            this.pnMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnMain.Controls.Add(this.history);
            this.pnMain.Controls.Add(this.txt);
            this.pnMain.Location = new System.Drawing.Point(15, 48);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(600, 382);
            this.pnMain.TabIndex = 6;
            // 
            // bClear
            // 
            this.bClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bClear.Location = new System.Drawing.Point(540, 5);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(75, 23);
            this.bClear.TabIndex = 7;
            this.bClear.Text = "Clear";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // PID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 442);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.bAnalyze);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tCmd);
            this.Controls.Add(this.label1);
            this.Name = "PID";
            this.Text = "PID";
            this.pnMain.ResumeLayout(false);
            this.pnMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tCmd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.WebBrowser history;
        private System.Windows.Forms.Button bAnalyze;
        private System.Windows.Forms.TextBox txt;
        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Button bClear;
    }
}