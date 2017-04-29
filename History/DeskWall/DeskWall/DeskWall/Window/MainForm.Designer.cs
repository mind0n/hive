using System;
using Fs;
namespace Dw
{
	partial class MainForm
	{
		protected new DelegateDisposeCallback Dispose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected void DisposeWindow(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
		protected void Dispose_Delegate(bool disposing)
		{
			DelegateDisposeCallback sw = new DelegateDisposeCallback(DisposeWindow);
			try
			{
				if (Handle != IntPtr.Zero)
				{
					Invoke(sw, disposing);
				}
			}
			catch (Exception err)
			{
				Exceptions.LogOnly(err);
			}
		}
		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.BgPic = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.BgPic)).BeginInit();
			this.SuspendLayout();
			// 
			// BgPic
			// 
			this.BgPic.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BgPic.Location = new System.Drawing.Point(0, 0);
			this.BgPic.Name = "BgPic";
			this.BgPic.Size = new System.Drawing.Size(292, 266);
			this.BgPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.BgPic.TabIndex = 1;
			this.BgPic.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.ControlBox = false;
			this.Controls.Add(this.BgPic);
			this.Name = "MainForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "MainForm";
			((System.ComponentModel.ISupportInitialize)(this.BgPic)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox BgPic;
	}
}