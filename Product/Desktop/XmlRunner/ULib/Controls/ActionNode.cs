using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using System.Windows.Forms;


namespace ULib.Controls
{
    public class ActionTreeViewNode : System.Windows.Forms.TreeNode
    {
        protected List<Form> forms = new List<Form>();
        public Func<ActionTreeViewNode, bool> ActivateCallback;
        public Func<ActionTreeViewNode, bool> DoubleClickCallback;
        public ActionTreeView TreeView;
        public ActionTreeViewNode()
        {

        }
        public void AddModule(IEmbededModule module, TabPage page = null)
        {
            Form frm = module as Form;
            if (frm != null)
            {
                forms.Add(frm);
            }
            if (page != null)
            {
                page.Tag = frm;
            }
        }
        public void Unload()
        {
            foreach (Form i in forms)
            {
                i.Close();
            }
        }
    }
    public class CommandTreeViewNode : ActionTreeViewNode, ICloneable
    {
        private OutputBox outputer;
        protected void Output(string msg, params string[] args)
        {
            if (outputer != null)
            {
                outputer.WriteMsg(msg, false, "black", true, args);
            }            
        }
		public string CommandName
		{
			get
			{
				return CmdNode.Name;
			}
		}
        public CommandNode CmdNode;
        public void Update()
        {
            Text = CmdNode.Name;
        }
		public void Cancel()
		{
			CmdNode.Cancel();
		}
		public void Reset()
		{
			CmdNode.Reset();
		}
		public CommandTreeViewNode Clone()
		{
			CommandNode node = CmdNode.Clone();
			return node.CreateTreeviewNode();
		}
    }
}
