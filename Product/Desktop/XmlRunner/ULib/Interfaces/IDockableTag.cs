using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ULib.Forms;

namespace ULib
{
    public interface IDockableTag
    {
        List<Control> EmbededForms { get; }

        List<Control> NewlyEmbededForms { get; set; }

        Control GetContentRegion();

        void AddForm(Control form);

        void SetContentRegion(Control region);
    }
}
