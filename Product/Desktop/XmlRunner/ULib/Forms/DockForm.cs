using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ULib.DataSchema;
using ULib.Exceptions;

namespace ULib.Forms
{
    public partial class DockForm : EmbededForm
    {
        public event Action FormEmbeded;
        public event Action FormDetached;
        protected bool isShown;
        protected bool isBeginEmbed;

        public bool IsLoaded
        {
            get { return isShown; }
        }
        public IDockableTag DockTag
        {
            get
            {
                IDockableTag tag = Tag as IDockableTag;
                if (Tag == null)
                {
                    tag = InitializeTag();
                }
                else if (tag == null)
                {
                    return null;
                }
                return tag;
            }
        }

        public DockForm()
        {
            InitializeComponent();
            Load += new EventHandler(DockForm_Load);
            Shown += new EventHandler(DockForm_Shown);
        }


        #region Event Handlers

        private void DockForm_Shown(object sender, EventArgs e)
        {
        }
        private void DockForm_Load(object sender, EventArgs e)
        {
            isShown = true;
        }

        #endregion

        protected virtual IDockableTag InitializeTag()
        {
            return new DockTag { ContentRegion = this };
        }

        public virtual void BeginEmbed()
        {
            isBeginEmbed = true;
            if (DockTag != null)
            {
                DockTag.NewlyEmbededForms.Clear();
            }
        }

        public virtual void EndEmbed()
        {
            if (DockTag != null && DockTag.NewlyEmbededForms.Count > 0)
            {
                DockTag.EmbededForms.AddRange(DockTag.NewlyEmbededForms);
                bool notified = false;
                while (true)
                {
                    bool allShown = true;
                    foreach (DockForm form in DockTag.NewlyEmbededForms)
                    {
                        try
                        {
                            if (!form.IsLoaded)
                            {
                                allShown = false;
                                break;
                            }
                            if (allShown && form.FormEmbeded != null)
                            {
                                form.FormEmbeded();
                            }
                            if (allShown && !notified)
                            {
                                notified = true;
                            }
                        }
                        catch (Exception e)
                        {
                            ExceptionHandler.Handle(e);
                            continue;
                        }
                    }
                    if (allShown && notified)
                    {
                        break;
                    }
                }
            }
            isBeginEmbed = false;
        }
        public SplitContainer Embed(Control childControl, DockType dock, bool isFixedSize = false)
        {
            if (!isBeginEmbed)
            {
                return null;
            }
            return DockForm.Embed(this, childControl, dock, isFixedSize);
        }
        public static Control Detach(Control childControl, Control parentControl = null)
        {
            SplitterPanel panel = childControl.Parent as SplitterPanel;
            if (panel == null)
            {
                return null;
            }
            SplitContainer splitter = panel.GetContainerControl() as SplitContainer;
            if (splitter == null)
            {
                return null;
            }
            Control container = splitter.Parent;
            if (container == null)
            {
                return null;
            }
            SplitterPanel contentRegion = splitter.Panel1;
            if (contentRegion == panel)
            {
                contentRegion = splitter.Panel2;
            }
            if (contentRegion.Controls.Count > 0)
            {
                for (int l = contentRegion.Controls.Count, i = l - 1; i >= 0; i--)
                {
                    Control c = contentRegion.Controls[i];
                    container.Controls.Add(c);
                }
            }
            panel.Controls.Remove(childControl);
            if (childControl is DockForm)
            {
                DockForm df = (DockForm)childControl;
                df.isShown = false;
                if (df.FormDetached != null)
                {
                    df.FormDetached();
                }
            }
            splitter.Dispose();
            if (parentControl == null && childControl is DockForm)
            {
                parentControl = ((DockForm)childControl).originalParent;
            }
            if (parentControl != null)
            {
                IDockableTag tag = parentControl.Tag as IDockableTag;
                if (tag != null)
                {
                    tag.SetContentRegion(container);
                }
            }
            return container;
        }
        public static SplitContainer Embed(Control parentControl, Control childControl, DockType dock, bool isFixedSize = false)
        {
            if (parentControl == null || childControl == null)
            {
                return null;
            }
			if (!(childControl is EmbededForm) || EmbededForm.IsEmbeded(childControl))
			{
				return null;
			}
            IDockableTag tag = parentControl.Tag as IDockableTag;
            if (tag == null)
            {
                tag = new DockTag { ContentRegion = parentControl };
                parentControl.Tag = tag;
            }
            tag.AddForm(childControl);
            Control container = tag.GetContentRegion();
            if (container == null)
            {
                return null;
            }
            SplitContainer splitter = new SplitContainer();
            splitter.IsSplitterFixed = isFixedSize;
            Control originContainer = null;
            Control parentContainer = AdjustSplitter(childControl, dock, splitter, ref originContainer);
            if (parentContainer == null)
            {
                parentContainer = container;
                originContainer = container;
            }
            tag.SetContentRegion(originContainer);
            if (container.Controls.Count > 0)
            {
                if (originContainer == null)
                {
                    return null;
                }
                foreach (Control c in container.Controls)
                {
                    container.Controls.Remove(c);
                    originContainer.Controls.Add(c);
                }
            }

            container.Controls.Add(splitter);
            splitter.Dock = DockStyle.Fill;
            parentControl.Tag = tag;

            EmbededForm.Embed(childControl, parentContainer, true, true);
            return splitter;
        }
        protected Control originalParent;
        public virtual void EmbedInto(Control targetControl, DockType dockType, bool isFixedSize = false)
        {
            IDockableTag target = null;
            if (!(targetControl.Tag is IDockableTag))
            {
                return;
            }
            target = targetControl.Tag as IDockableTag;
            if (target == null)
            {
                target = new DockTag { ContentRegion = targetControl };
                targetControl.Tag = target;
            }
            target.AddForm(this);
            Control container = target.GetContentRegion();
            if (container == null)
            {
                return;
            }
            originalParent = targetControl;
            SplitContainer splitter = new SplitContainer();
            splitter.IsSplitterFixed = isFixedSize;
            Control originContainer = null;
            Control parentContainer = PickSplitter(dockType, splitter, ref originContainer);
            if (parentContainer == null)
            {
                parentContainer = container;
                originContainer = container;
            }
            target.SetContentRegion(originContainer);

            if (container.Controls.Count > 0)
            {
                if (originContainer == null)
                {
                    return;
                }
                for (int l = container.Controls.Count - 1, i=l; i >= 0; i--)
                {
                    Control c = container.Controls[i];
                    container.Controls.Remove(c);
                    originContainer.Controls.Add(c);
                }
            }

            container.Controls.Add(splitter);
            splitter.Dock = DockStyle.Fill;
            targetControl.Tag = target;

            EmbedInto(parentContainer, true);
        }

        public override void EmbedInto(Control target, bool isFullDock)
        {
            //if (!isBeginEmbed)
            //{
            //    return;
            //}
            base.EmbedInto(target, isFullDock);
        }

        private static Control AdjustSplitter(Control child, DockType dockType, SplitContainer splitter, ref Control originContainer)
        {
            Control parentContainer = null;
            splitter.Width = child.Size.Width * 2;
            if (dockType == DockType.Left || dockType == DockType.Right)
            {
                splitter.Orientation = Orientation.Vertical;
                if (dockType == DockType.Left)
                {
                    parentContainer = splitter.Panel1;
                    originContainer = splitter.Panel2;
                    splitter.SplitterDistance = child.Size.Width;
                    splitter.FixedPanel = FixedPanel.Panel1;
                }
                else
                {
                    parentContainer = splitter.Panel2;
                    originContainer = splitter.Panel1;
                    splitter.SplitterDistance = splitter.Width - child.Size.Width;
                    splitter.FixedPanel = FixedPanel.Panel2;
                }
            }
            else if (dockType != DockType.Fill)
            {
                splitter.Orientation = Orientation.Horizontal;
                if (dockType == DockType.Top)
                {
                    parentContainer = splitter.Panel1;
                    originContainer = splitter.Panel2;
                    splitter.SplitterDistance = child.Size.Height;
                    splitter.FixedPanel = FixedPanel.Panel1;
                }
                else
                {
                    parentContainer = splitter.Panel2;
                    originContainer = splitter.Panel1;
                    splitter.SplitterDistance = splitter.Height - child.Size.Height;
                    splitter.FixedPanel = FixedPanel.Panel2;
                }
            }
            return parentContainer;
        }

        private Control PickSplitter(DockType dockType, SplitContainer splitter, ref Control originContainer)
        {
            Control parentContainer = null;
            if (dockType == DockType.Left || dockType == DockType.Right)
            {
                splitter.Width = this.Size.Width * 2;
                splitter.Orientation = Orientation.Vertical;
                if (dockType == DockType.Left)
                {
                    parentContainer = splitter.Panel1;
                    originContainer = splitter.Panel2;
                    splitter.SplitterDistance = this.Size.Width;
                    splitter.FixedPanel = FixedPanel.Panel1;
                }
                else
                {
                    parentContainer = splitter.Panel2;
                    originContainer = splitter.Panel1;
                    splitter.SplitterDistance = splitter.Width - this.Size.Width;
                    splitter.FixedPanel = FixedPanel.Panel2;
                }
            }
            else if (dockType != DockType.Fill)
            {
                splitter.Height = this.Size.Height * 2;
                splitter.Orientation = Orientation.Horizontal;
                if (dockType == DockType.Top)
                {
                    parentContainer = splitter.Panel1;
                    originContainer = splitter.Panel2;
                    splitter.SplitterDistance = this.Size.Height;
                    splitter.FixedPanel = FixedPanel.Panel1;
                }
                else
                {
                    parentContainer = splitter.Panel2;
                    originContainer = splitter.Panel1;
                    splitter.SplitterDistance = splitter.Height - this.Size.Height;
                    splitter.FixedPanel = FixedPanel.Panel2;
                }
            }
            return parentContainer;
        }
        private void AdjustSplitter(DockType dockType, SplitContainer splitter, ref Control originContainer)
        {
            Control parentContainer = null;
            splitter.Width = this.Size.Width * 2;
            if (dockType == DockType.Left || dockType == DockType.Right)
            {
                splitter.Orientation = Orientation.Vertical;
                if (dockType == DockType.Left)
                {
                    parentContainer = splitter.Panel1;
                    originContainer = splitter.Panel2;
                    splitter.SplitterDistance = this.Size.Width;
                    splitter.FixedPanel = FixedPanel.Panel1;
                }
                else
                {
                    parentContainer = splitter.Panel2;
                    originContainer = splitter.Panel1;
                    splitter.SplitterDistance = splitter.Width - this.Size.Width;
                    splitter.FixedPanel = FixedPanel.Panel2;
                }
            }
            else if (dockType != DockType.Fill)
            {
                splitter.Orientation = Orientation.Horizontal;
                if (dockType == DockType.Top)
                {
                    parentContainer = splitter.Panel1;
                    originContainer = splitter.Panel2;
                    splitter.SplitterDistance = this.Size.Height;
                    splitter.FixedPanel = FixedPanel.Panel1;
                }
                else
                {
                    parentContainer = splitter.Panel2;
                    originContainer = splitter.Panel1;
                    splitter.SplitterDistance = splitter.Height - this.Size.Height;
                    splitter.FixedPanel = FixedPanel.Panel2;
                }
            }
        }
    }
}
