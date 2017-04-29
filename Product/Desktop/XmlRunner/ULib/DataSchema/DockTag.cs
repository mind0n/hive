using System.Collections.Generic;
using System.Windows.Forms;
using ULib.Forms;

namespace ULib.DataSchema
{
    public enum DockType
    {
        Left = 4,
        Bottom = 2,
        Right = 6,
        Fill = 5,
        Top = 8
    }
    public class DockTag : IDockableTag
    {
        public Control ContentRegion;

        public List<Control> EmbededForms
        {
            get { return embededForms; }
        }
        protected List<Control> embededForms = new List<Control>();



        public Control GetContentRegion()
        {
            return ContentRegion;
        }

        public void SetContentRegion(Control region)
        {
            ContentRegion = region;
        }

        public static void AddForm(IDockableTag target, Control form)
        {
            if (target.NewlyEmbededForms != null && !target.NewlyEmbededForms.Contains(form))
            {
                target.NewlyEmbededForms.Add(form);
            }
        }


        public void AddForm(Control form)
        {
            DockTag.AddForm(this, form);
        }


        public List<Control> NewlyEmbededForms
        {
            get
            {
                return newlyEmbededForms;
            }
            set
            {
                newlyEmbededForms = value;
            }
        }
        protected List<Control> newlyEmbededForms = new List<Control>();
    }
}
