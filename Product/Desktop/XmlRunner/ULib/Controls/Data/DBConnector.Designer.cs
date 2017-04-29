namespace ULib.Controls.Data
{
	partial class DBConnector
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pn = new System.Windows.Forms.Panel();
			this.bTest = new System.Windows.Forms.Button();
			this.ckTrusted = new System.Windows.Forms.CheckBox();
			this.ltPwd = new ULib.Controls.LabelTextBox();
			this.ltUser = new ULib.Controls.LabelTextBox();
			this.ltServer = new ULib.Controls.LabelTextBox();
			this.pn.SuspendLayout();
			this.SuspendLayout();
			// 
			// pn
			// 
			this.pn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pn.Controls.Add(this.bTest);
			this.pn.Controls.Add(this.ckTrusted);
			this.pn.Controls.Add(this.ltPwd);
			this.pn.Controls.Add(this.ltUser);
			this.pn.Controls.Add(this.ltServer);
			this.pn.Location = new System.Drawing.Point(3, 3);
			this.pn.Name = "pn";
			this.pn.Size = new System.Drawing.Size(252, 146);
			this.pn.TabIndex = 0;
			// 
			// bTest
			// 
			this.bTest.Location = new System.Drawing.Point(67, 108);
			this.bTest.Name = "bTest";
			this.bTest.Size = new System.Drawing.Size(135, 27);
			this.bTest.TabIndex = 4;
			this.bTest.Text = "Test Connection";
			this.bTest.UseVisualStyleBackColor = true;
			// 
			// ckTrusted
			// 
			this.ckTrusted.AutoSize = true;
			this.ckTrusted.Location = new System.Drawing.Point(67, 33);
			this.ckTrusted.Name = "ckTrusted";
			this.ckTrusted.Size = new System.Drawing.Size(150, 16);
			this.ckTrusted.TabIndex = 1;
			this.ckTrusted.Text = "Is Trusted Connection";
			this.ckTrusted.UseVisualStyleBackColor = true;
			// 
			// ltPwd
			// 
			this.ltPwd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ltPwd.ContentFormatControl = null;
			this.ltPwd.LabelText = "Password:";
			this.ltPwd.Location = new System.Drawing.Point(3, 82);
			this.ltPwd.Name = "ltPwd";
			this.ltPwd.PasswordChar = '\0';
			this.ltPwd.ReadOnly = false;
			this.ltPwd.Size = new System.Drawing.Size(246, 22);
			this.ltPwd.SplitDistance = 61;
			this.ltPwd.TabIndex = 0;
			// 
			// ltUser
			// 
			this.ltUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ltUser.ContentFormatControl = null;
			this.ltUser.LabelText = "Username:";
			this.ltUser.Location = new System.Drawing.Point(3, 56);
			this.ltUser.Name = "ltUser";
			this.ltUser.PasswordChar = '\0';
			this.ltUser.ReadOnly = false;
			this.ltUser.Size = new System.Drawing.Size(246, 22);
			this.ltUser.SplitDistance = 61;
			this.ltUser.TabIndex = 0;
			// 
			// ltServer
			// 
			this.ltServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ltServer.ContentFormatControl = null;
			this.ltServer.LabelText = "Server:";
			this.ltServer.Location = new System.Drawing.Point(3, 3);
			this.ltServer.Name = "ltServer";
			this.ltServer.PasswordChar = '\0';
			this.ltServer.ReadOnly = false;
			this.ltServer.Size = new System.Drawing.Size(246, 22);
			this.ltServer.SplitDistance = 61;
			this.ltServer.TabIndex = 0;
			// 
			// DBConnector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pn);
			this.Name = "DBConnector";
			this.Size = new System.Drawing.Size(258, 152);
			this.pn.ResumeLayout(false);
			this.pn.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pn;
		private System.Windows.Forms.CheckBox ckTrusted;
		private LabelTextBox ltPwd;
		private LabelTextBox ltUser;
		private LabelTextBox ltServer;
		private System.Windows.Forms.Button bTest;

	}
}
