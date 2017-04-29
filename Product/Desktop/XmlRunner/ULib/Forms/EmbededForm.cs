using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ULib.Exceptions;

namespace ULib.Forms
{
    public partial class EmbededForm : FormBase, IEmbededModule
    {
        public const string MsgErrorLoadModule = "Exception occurred when initializing the content.\r\nType: {1}\r\nDetails: {0}\r\n";

		public delegate void OnEmbededHandler(EmbededForm sender);
		public event OnEmbededHandler OnEmbeded;

		public EmbededForm()
        {
            InitializeComponent();
        }

		public static bool IsEmbeded(Control target)
		{
			return target.Parent != null;
		}

		public static bool Embed(Control target, Control container, bool isFullDock, bool isClearContainer)
        {
            Form child = null;
            try
            {
                if (target != null && container != null)
                {
                    child = (target as Form);
                    if (target is Form)
                    {
                        child.TopLevel = false;
                        child.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                        child.ControlBox = false;
                        child.MaximizeBox = false;
                        child.MinimizeBox = false;
                        AdjustContainer(container, child, isClearContainer, isFullDock);
                        child.Show();
                        container.Text = child.Text;
                    }
                    else if (target is Control)
                    {
                        AdjustContainer(container, target, isClearContainer, isFullDock);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                if (child != null)
                {
                    child.Close();
                }
                ExceptionHandler.Handle(MsgErrorLoadModule, e.Source, e.ToString(), child.GetType().FullName);
                return false;
            }
        }

        private static void AdjustContainer(Control container, Control child, bool isClearContainer, bool isFullDock)
        {
            if (isClearContainer)
            {
                for (int i = 0; i < container.Controls.Count; i++)
                {
                    container.Controls[i].Dispose();
                }
                container.Controls.Clear();
            }
            container.Controls.Add(child);
            if (isFullDock)
            {
                child.Dock = DockStyle.Fill;
            }
        }
        
        public virtual void EmbedInto(Control container, bool isFullDock)
        {
            try
            {
				if (container.Controls.Contains(this))
				{
					return;
				}
				for (int i = 0; i < container.Controls.Count; i++)
				{
					Control c = container.Controls[i];
					if (c is Form)
					{
						c.Hide();
					}
				}
                container.Controls.Clear();
                container.Controls.Add(this);
                if (isFullDock)
                {
                    this.Dock = DockStyle.Fill;
                }
                this.Show();
				if (OnEmbeded != null)
				{
					OnEmbeded(this);
				}
                container.Text = this.Text;
            }
            catch (Exception e)
            {
                ExceptionHandler.Handle(e);
            }
        }

        public virtual void EmbedInto(Control parent)
        {
            EmbedInto(parent, true);
        }
    }
}
