using System;
using System.Drawing;
using System.Windows.Forms;
using ULib;
using ULib.Controls;
using ULib.Exceptions;
using Utilities.Components;
using Utilities.Components.Deployment;
using Utilities.Components.HA;
using Utilities.Components.Launchers;
using Utilities.Settings;
using Utilities.Components.DBPartitioning;

namespace Utilities
{
    public partial class MainForm : Form
    {
        private ActionTreeViewNode prevSelectedNode;
        public MainForm()
        {
            InitializeComponent();
            Load += new EventHandler(MainForm_Load);
            KeyUp += new KeyEventHandler(MainForm_KeyUp);
            FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
            AppSettings.Instance.Initialize(AppSettings.KeyDeploySettings);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
        }

        void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (ActionTreeViewNode node in tv.Nodes)
            {
                node.Unload();
            }
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            tv.Nodes.Clear();

            tabs.SelectedIndexChanged += new EventHandler(tabs_SelectedIndexChanged);

            ActionTreeViewNode node;

			node = new ActionTreeViewNode
			{
				Name = "dbp",
				Text = "DB Partitioning",
				ActivateCallback = delegate(ActionTreeViewNode n)
				{
					ClearModules();
					return true;
				}
			};
			tv.Nodes.Add(node);

			//node = new ActionTreeViewNode
			//{
			//    Name = "ham",
			//    Text = "Plan Designer",
			//    ActivateCallback = delegate(ActionTreeViewNode n)
			//    {
			//        ClearModules();
			//        AddModule(typeof(PlanForm), n);
			//        return true;
			//    }
			//};
			//tv.Nodes.Add(node);

            //node = new ActionTreeViewNode
            //{
            //    Name = "utl",
            //    Text = "Utilities",
            //    ActivateCallback = delegate(ActionTreeViewNode n)
            //    {
            //        ClearModules();
            //        AddModule(typeof(PID), n);
            //        return true;
            //    }
            //};
            //tv.Nodes.Add(node);

            //node = new ActionTreeViewNode
            //{
            //    Name = "lch",
            //    Text = "Launcher",
            //    ActivateCallback = delegate(ActionTreeViewNode n)
            //    {
            //        ClearModules();
            //        AddModule(typeof(P1Launcher), n);
            //        return true;
            //    }
            //};
            //tv.Nodes.Add(node);

            //node = new ActionTreeViewNode
            //{
            //    Name = "dpl",
            //    Text = "Deployment",
            //    ActivateCallback = delegate(ActionTreeViewNode n)
            //    {
            //        ClearModules();
            //        AddModule(typeof(MsiSeekerForm), n);
            //        return true;
            //    }
            //};
            //tv.Nodes.Add(node);

            tv.NodeMouseClick += new TreeNodeMouseClickEventHandler(tv_NodeMouseClick);

            tabs.TabPages.Clear();
            ActivateNode(node);
            tv.SelectedNode = node;
            ExceptionHandler.Handlers.Add(HandleException);
        }

        void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void tv_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ActionTreeViewNode node = e.Node as ActionTreeViewNode;
            if (prevSelectedNode != node)
            {
                ActivateNode(node);
                prevSelectedNode = node;
            }
        }

        private void ActivateNode(ActionTreeViewNode node)
        {
            if (node != null && node.ActivateCallback != null)
            {
                node.ActivateCallback(node);
                prevSelectedNode = node;
            }
        }

        private bool HandleException(Exception error)
        {
            MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return true;
        }

        protected void ClearModules()
        {
            tabs.TabPages.Clear();
        }

        protected IEmbededModule AddModule(Type moduleType, ActionTreeViewNode node = null)
        {
            IEmbededModule instance = moduleType.GetConstructor(Type.EmptyTypes).Invoke(null) as IEmbededModule;
            TabPage page = CreateTab();
            instance.EmbedInto(page);
            if (node != null)
            {
                node.AddModule(instance);
            }
            return instance;
        }

        protected TabPage CreateTab()
        {
            TabPage page = new TabPage();
            tabs.TabPages.Add(page);
            return page;
        }

        private ActionTreeView tv;

        #region UI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabs = new System.Windows.Forms.TabControl();
            this.tbModule = new System.Windows.Forms.TabPage();
            this.tv = new ULib.Controls.ActionTreeView();
            this.tabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            resources.ApplyResources(this.tabs, "tabs");
            this.tabs.Controls.Add(this.tbModule);
            this.tabs.Multiline = true;
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            // 
            // tbModule
            // 
            this.tbModule.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            resources.ApplyResources(this.tbModule, "tbModule");
            this.tbModule.Name = "tbModule";
            // 
            // tv
            // 
            resources.ApplyResources(this.tv, "tv");
            this.tv.Name = "tv";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tv);
            this.Controls.Add(this.tabs);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tbModule;

        #endregion
    }
}
