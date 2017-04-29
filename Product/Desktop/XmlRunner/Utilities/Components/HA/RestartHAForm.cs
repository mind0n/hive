using System;
using System.IO;
using System.Management;
using System.Windows.Forms;
using ULib;
using ULib.Controls;
using ULib.DataSchema;
using ULib.Forms;
using Utilities.Commands;
using System.Threading;

namespace Utilities.Components.HA
{
    public partial class RestartHAForm : EmbededForm
    {
		string openedFile;
        ConfigScreen CfgCommand;
        public RestartHAForm()
        {
            InitializeComponent();
			CommandSet.Register(
				typeof(ChangeServerCommand), 
				typeof(PortStatusCommand), 
				typeof(RemoteExecuteCommand), 
				typeof(RepeatCommand), 
				typeof(WaitTimeCommand),
				typeof(ServiceManipulateCommand)
			);
            Load += new EventHandler(RestartHAForm_Load);
            InitDockTag(pnPlan);
        }

        private void InitDockTag(Panel target)
        {
            DockTag dTag = new DockTag();
            dTag.ContentRegion = target;
            target.Tag = dTag;
        }

        private void RestartHAForm_Load(object sender, EventArgs e)
        {
			UpdatePlanName();
            ShowCommandConfigScreen();
        }

		#region Command Configuration

		private void ShowCommandConfigScreen()
        {
            if (CfgCommand == null)
            {
                CfgCommand = new ConfigScreen();
                CfgCommand.Title = "Command Settings";
                CfgCommand.FormDetached += new Action(CfgCommand_FormDetached);
                CfgCommand.OnConfigChanged += new Action<object>(CfgCommand_OnConfigChanged);

                CfgCommand.EmbedInto(pnPlan, DockType.Bottom);
                CfgCommand.Show();
            }
            else if (!CfgCommand.IsLoaded)
            {
                CfgCommand.EmbedInto(pnPlan, DockType.Bottom);
            }
        }

        private void CfgCommand_OnConfigChanged(object obj)
        {
            CommandTreeViewNode cmdNode = tvPlan.SelectedNode as CommandTreeViewNode;
            if (cmdNode != null)
            {
                cmdNode.Update();
            }
        }

        private void CfgCommand_FormDetached() { }

		#endregion

		#region Command tree manipulation

		private void bRemove_Click(object sender, EventArgs e)
		{
			if (tvPlan.Nodes.Count > 0)
			{
				tvPlan.Nodes.Remove(tvPlan.SelectedNode);
			}
			tvPlan.Select();
		}

		private void bRemoveAll_Click(object sender, EventArgs e)
		{
			RemoveAllCommands();
		}

		private void RemoveAllCommands(bool clearFile = false)
		{
			if (tvPlan.Nodes.Count > 0)
			{
				if (MessageBox.Show("All the commands will be removed, continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
				{
					tvPlan.Nodes.Clear();
					if (clearFile)
					{
						openedFile = null;
					}
				}
			}
			else
			{
				tvPlan.Nodes.Clear();
				if (clearFile)
				{
					openedFile = null;
				}
			}
			UpdatePlanName();
		}

		private void bRunCurt_Click(object sender, EventArgs e)
		{
			tabs.SelectedIndex = 1;
			CommandTreeViewNode node = tvPlan.SelectedNode as CommandTreeViewNode;
			Thread thread = new Thread(delegate()
			{
				//this.Invoke((MethodInvoker)delegate
				//{
					ExecuteCommand(node, false);
				//});
			});
			thread.IsBackground = true;
			thread.Start();
		}

		private void bRunAll_Click(object sender, EventArgs e)
		{
			obox.WriteMsg("-- Preparing to execute all the commands -------------------------------------");
			tabs.SelectedIndex = 1;
			ExecuteCommands(tvPlan.Nodes);
		}

		private void ExecuteCommands(TreeNodeCollection nodes)
		{
			Thread thread = new Thread(delegate(object callback)
			{
				ExecuteCommandInternal(nodes);
				if (callback != null)
				{
					try
					{
						((Action)callback)();
					}
					catch (Exception err)
					{
						MessageBox.Show(err.Message);
					}
				}
			});
			thread.IsBackground = true;
			thread.Start(new Action(delegate()
			{
				obox.WriteMsg("-- DONE --");
				ResetAllCommands();
			}));
		}

		private bool isCancelled;

		private CommandResult ExecuteCommandInternal(TreeNodeCollection nodes)
		{
			CommandResult result = null;
			if (nodes.Count > 0)
			{
				CommandTreeViewNode node = nodes[0] as CommandTreeViewNode;
				while (node != null && !isCancelled)
				{
					if (node != null)
					{
						result = ExecuteCommand(node, false);
					}
					if (node.Nodes.Count > 0)
					{
						result = ExecuteCommandInternal(node.Nodes);
					}
					node = result.Next;
				}
				if (isCancelled)
				{
					obox.WriteErrorMsg("Cancelled.");
					ResetAllCommands();
				}
				isCancelled = false;
			}
			return result;
		}

		private CommandResult ExecuteCommand(CommandTreeViewNode node, bool suppressSuccessMsg = true)
		{
			if (node != null)
			{
				obox.WriteMsg("{0}&nbsp;", false, "gray", false, node.Text);
				CommandResult result = node.Execute(node, obox);
				if (result.Next == null)
				{
					result.Next = node.NextNode as CommandTreeViewNode;
				}
				if (result != null)
				{
					this.Invoke((MethodInvoker)delegate
					{
						node.Update();
					});
					if (result.IsSuccessful && !suppressSuccessMsg && !isCancelled)
					{
						obox.WriteSuccessMsg("Successful");
					}
					else if (result.IsError)
					{
						obox.WriteErrorMsg("Failed <br />{0}", result.LastError.Message);
					}
					else if (result.IsCanceled)
					{
						obox.WriteMsg("Canceled");
					}
				}
				else
				{
					obox.WriteErrorMsg("Failed <br />Command result missing.", node.Text);
				}
				return result;
			}
			return new CommandResult();
		}

		#endregion Command tree manipulation

		#region Helper Methods

		private void UpdatePlanName()
		{
			this.Invoke((MethodInvoker)delegate
			{
				lbCommand.Text = string.Concat("Active Plan ", openedFile, ":");
			});
		}

		private void CacheSelectedNode(bool isCut = true, TreeView tv = null)
		{
			if (tv == null)
			{
				tv = tvPlan;
			}
			CommandTreeViewNode node = tv.SelectedNode as CommandTreeViewNode;
			if (node != null)
			{
				//CommandCache.Instance.cl
				CommandCache.Instance.ClearAndAdd(node);
				if (isCut)
				{
					tvPlan.Nodes.Remove(node);
				}
			}
		}

		private void AddCommandTreeviewNode(CommandNode cmd)
		{
			CommandTreeViewNode node = cmd.CreateTreeviewNode();
			InsertTreeViewNode(node);
		}

		private void InsertTreeViewNode(CommandTreeViewNode node)
		{
			node.ActivateCallback = NodeActivateCallback;
			if (tvPlan.SelectedNode != null)
			{
				tvPlan.Nodes.Insert(tvPlan.Nodes.IndexOf(tvPlan.SelectedNode) + 1, node);
			}
			else
			{
				tvPlan.Nodes.Add(node);
			}
			tvPlan.SelectedNode = node;
		}

		private bool NodeActivateCallback(ActionTreeViewNode sender)
		{
			CommandTreeViewNode cnode = sender as CommandTreeViewNode;
			if (cnode != null)
			{
				CfgCommand.Generate(cnode.CmdNode);
			}
			return true;
		}

		private void BuildNodes(TreeNodeCollection treeNodes, Node curt)
		{
			foreach (CommandTreeViewNode i in treeNodes)
			{
				if (i != null)
				{
					curt.Add(i.CmdNode);
					if (i.Nodes.Count > 0)
					{
						BuildNodes(i.Nodes, i.CmdNode);
					}
				}
			}
		}

		private void EnumNodes(TreeNodeCollection nodes, Action<CommandTreeViewNode> callback)
		{
			foreach (CommandTreeViewNode i in nodes)
			{
				if (callback != null)
				{
					callback(i);
				}
				if (i.Nodes.Count > 0)
				{
					EnumNodes(i.Nodes, callback);
				}
			}
		}

		private void ResetAllCommands()
		{
			EnumNodes(tvPlan.Nodes, new Action<CommandTreeViewNode>(delegate(CommandTreeViewNode n)
			{
				if (n != null)
				{
					n.Reset();
				}

			}));
		}


		#endregion Helper Methods

		#region Output Tab Click Handler

		private void bClearOutput_Click(object sender, EventArgs e)
		{
			obox.Clear();
			tabs.SelectedIndex = 0;
		}
		private void bStopExec_Click(object sender, EventArgs e)
		{
			isCancelled = true;
			obox.WriteErrorMsg("Cancelling execution, please wait ...");
			EnumNodes(tvPlan.Nodes, new Action<CommandTreeViewNode>(delegate(CommandTreeViewNode n)
			{
				if (n != null)
				{
					n.Cancel();
				}
			}));
		}

		#endregion Output Tab Click Handler

		#region Toolstrip Click Handler

		private void saveToolStripButton_Click(object sender, EventArgs e)
		{
			Thread th = new Thread(delegate(object par)
			{
				if (string.IsNullOrEmpty(openedFile))
				{
					if (savePlanDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						openedFile = savePlanDlg.FileName;
					}
				}
				UpdatePlanName();
				if (!string.IsNullOrEmpty(openedFile))
				{
					RootCommandNode root = new RootCommandNode();
					BuildNodes(tvPlan.Nodes, root);
					string xml = root.ToXml(CommandSet.SupportedCommands);
					File.WriteAllText(openedFile, xml);
				}
			});
			th.SetApartmentState(ApartmentState.STA);
			th.IsBackground = true;
			th.Start(null);
		}

		private void newToolStripButton_Click(object sender, EventArgs e)
		{
			RemoveAllCommands(true);
		}

		private void openToolStripButton_Click(object sender, EventArgs e)
		{
			if (openPlanDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				openedFile = openPlanDlg.FileName;
				UpdatePlanName();
				string xml = File.ReadAllText(openedFile);
				RootCommandNode root = xml.FromXml<RootCommandNode>(CommandSet.SupportedCommands);
				if (root != null)
				{
					RemoveAllCommands();
					root.CreateTreeview(new Action<CommandTreeViewNode, CommandNode>(delegate(CommandTreeViewNode tn, CommandNode n)
					{
						tn.ActivateCallback = NodeActivateCallback;
					}), tvPlan.Nodes, tvPlan);
				}
			}
		}

		private void cutToolStripButton_Click(object sender, EventArgs e)
		{
			CacheSelectedNode();
		}

		private void copyToolStripButton_Click(object sender, EventArgs e)
		{
			CacheSelectedNode(false);
		}

		private void pasteToolStripButton_Click(object sender, EventArgs e)
		{
			InsertTreeViewNode(CommandCache.Instance.Read());
		}

		#endregion Toolstrip Click Handler

		#region Command Click Handler

		private void bChangeServer_Click(object sender, EventArgs e)
		{
			ChangeServerCommand cmd = new ChangeServerCommand();
			AddCommandTreeviewNode(cmd);
		}

		private void bWaitTime_Click(object sender, EventArgs e)
		{
			WaitTimeCommand cmd = new WaitTimeCommand();
			AddCommandTreeviewNode(cmd);
		}

		private void bRepeatCommand_Click(object sender, EventArgs e)
		{
			RepeatCommand cmd = new RepeatCommand();
			AddCommandTreeviewNode(cmd);
			//CommandTreeViewNode node = tvPlan.SelectedNode as CommandTreeViewNode;
			//TreeNodeCollection list = node.Parent == null ? tvPlan.Nodes : node.Parent.Nodes;
			//if (list != null)
			//{
			//    CommandTreeViewNode tvNode = cmd.CreateTreeviewNode();
			//    for (int i = list.Count - 1; i >= 0; i--)
			//    {
			//        TreeNode n = list[i];
			//        list.Remove(n);
			//        tvNode.Nodes.Insert(0, n);
			//    }
			//    list.Clear();
			//    InsertTreeViewNode(tvNode);
			//}
		}

		private void bWaitStart_Click(object sender, EventArgs e)
		{
			PortStatusCommand cmd = new PortStatusCommand();
			AddCommandTreeviewNode(cmd);
		}

		private void bServiceStatus_Click(object sender, EventArgs e)
		{
			ServiceManipulateCommand cmd = new ServiceManipulateCommand();
			AddCommandTreeviewNode(cmd);
		}

		private void bExecuteCommand_Click(object sender, EventArgs e)
		{
			RemoteExecuteCommand cmd = new RemoteExecuteCommand();
			AddCommandTreeviewNode(cmd);
		}
        private void bNLB_Click(object sender, EventArgs e)
        {
            NlbStatusCommand cmd = new NlbStatusCommand();
            AddCommandTreeviewNode(cmd);
        }

		#endregion Command Click Handler

        private void tvPlan_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

	}
}
