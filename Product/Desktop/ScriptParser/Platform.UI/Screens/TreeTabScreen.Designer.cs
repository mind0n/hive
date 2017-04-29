using ULib.Winform.Controls;
namespace ScriptPlatform.Screens
{
	partial class TreeTabScreen
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
			this.Splitter = new System.Windows.Forms.SplitContainer();
			this.ActionTreeView = new ULib.Winform.Controls.ActionTreeView<ScreenTreeNode>();
			this.Splitter.Panel1.SuspendLayout();
			this.Splitter.SuspendLayout();
			this.SuspendLayout();
			// 
			// Splitter
			// 
			this.Splitter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Splitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.Splitter.Location = new System.Drawing.Point(0, 0);
			this.Splitter.Name = "Splitter";
			// 
			// Splitter.Panel1
			// 
			this.Splitter.Panel1.Controls.Add(this.ActionTreeView);
			this.Splitter.Size = new System.Drawing.Size(673, 535);
			this.Splitter.SplitterDistance = 168;
			this.Splitter.TabIndex = 0;
			// 
			// ActionTreeView
			// 
			this.ActionTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ActionTreeView.Location = new System.Drawing.Point(3, 3);
			this.ActionTreeView.Name = "ActionTreeView";
			this.ActionTreeView.Size = new System.Drawing.Size(162, 529);
			this.ActionTreeView.TabIndex = 0;
			// 
			// TreeTabScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(673, 535);
			this.Controls.Add(this.Splitter);
			this.Name = "TreeTabScreen";
			this.Text = "TreeTabScreen";
			this.Splitter.Panel1.ResumeLayout(false);
			this.Splitter.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.SplitContainer Splitter;
		public ULib.Winform.Controls.ActionTreeView<ScreenTreeNode> ActionTreeView;
	}
}