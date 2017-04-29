using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ULib.Controls
{
    public class ActionTreeView : System.Windows.Forms.TreeView
    {
        //protected ActionTreeViewNode prevSelectedNode;
        public ActionTreeView()
        {

        }
        protected override void OnNodeMouseDoubleClick(System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            ActionTreeViewNode node = e.Node as ActionTreeViewNode;
            if (e.Button == System.Windows.Forms.MouseButtons.Left && node != null)
            {
                if (node.DoubleClickCallback != null)
                {
                    node.DoubleClickCallback(node);
                    //prevSelectedNode = node;
                }
            }
            base.OnNodeMouseDoubleClick(e);
        }
        protected override void OnNodeMouseClick(System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            ActionTreeViewNode node = e.Node as ActionTreeViewNode;
            if (e.Button == System.Windows.Forms.MouseButtons.Left && node != null)
            {
                if (node.ActivateCallback != null)
                {
                    node.ActivateCallback(node);
                    //prevSelectedNode = node;
                }
            }
            base.OnNodeMouseClick(e);
        }
    }
}
