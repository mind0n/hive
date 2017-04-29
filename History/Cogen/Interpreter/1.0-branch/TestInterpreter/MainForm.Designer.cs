namespace TestInterpreter
{
	partial class MainForm
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
			this.tScript = new System.Windows.Forms.TextBox();
			this.bParse = new System.Windows.Forms.Button();
			this.tResult = new System.Windows.Forms.TextBox();
			this.bLoad = new System.Windows.Forms.Button();
			this.bExec = new System.Windows.Forms.Button();
			this.pnMain = new System.Windows.Forms.Panel();
			this.pnMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// tScript
			// 
			this.tScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.tScript.Location = new System.Drawing.Point(3, 3);
			this.tScript.Multiline = true;
			this.tScript.Name = "tScript";
			this.tScript.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tScript.Size = new System.Drawing.Size(333, 519);
			this.tScript.TabIndex = 0;
			this.tScript.WordWrap = false;
			// 
			// bParse
			// 
			this.bParse.Location = new System.Drawing.Point(342, 33);
			this.bParse.Name = "bParse";
			this.bParse.Size = new System.Drawing.Size(116, 27);
			this.bParse.TabIndex = 1;
			this.bParse.Text = "Parse";
			this.bParse.UseVisualStyleBackColor = true;
			this.bParse.Click += new System.EventHandler(this.bParse_Click);
			// 
			// tResult
			// 
			this.tResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tResult.Location = new System.Drawing.Point(464, 4);
			this.tResult.Multiline = true;
			this.tResult.Name = "tResult";
			this.tResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tResult.Size = new System.Drawing.Size(330, 518);
			this.tResult.TabIndex = 2;
			this.tResult.WordWrap = false;
			// 
			// bLoad
			// 
			this.bLoad.Location = new System.Drawing.Point(342, 0);
			this.bLoad.Name = "bLoad";
			this.bLoad.Size = new System.Drawing.Size(116, 27);
			this.bLoad.TabIndex = 3;
			this.bLoad.Text = "Load Script";
			this.bLoad.UseVisualStyleBackColor = true;
			this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
			// 
			// bExec
			// 
			this.bExec.Location = new System.Drawing.Point(342, 67);
			this.bExec.Name = "bExec";
			this.bExec.Size = new System.Drawing.Size(116, 27);
			this.bExec.TabIndex = 4;
			this.bExec.Text = "Execute";
			this.bExec.UseVisualStyleBackColor = true;
			this.bExec.Click += new System.EventHandler(this.bExec_Click);
			// 
			// pnMain
			// 
			this.pnMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnMain.Controls.Add(this.tScript);
			this.pnMain.Controls.Add(this.tResult);
			this.pnMain.Controls.Add(this.bExec);
			this.pnMain.Controls.Add(this.bLoad);
			this.pnMain.Controls.Add(this.bParse);
			this.pnMain.Location = new System.Drawing.Point(12, 12);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(793, 525);
			this.pnMain.TabIndex = 5;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(820, 549);
			this.Controls.Add(this.pnMain);
			this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Interpreter";
			this.pnMain.ResumeLayout(false);
			this.pnMain.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox tScript;
		private System.Windows.Forms.Button bParse;
		private System.Windows.Forms.TextBox tResult;
		private System.Windows.Forms.Button bLoad;
		private System.Windows.Forms.Button bExec;
		private System.Windows.Forms.Panel pnMain;
	}
}

