using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ULib.Controls
{
    public class ActionTabPage : TabPage
    {
        public event Func<ActionTabPage, bool> ActivateCallback;
		public bool NotifyActivated(ActionTabPage page)
		{
			if (ActivateCallback != null)
			{
				return ActivateCallback(page);
			}
			return false;
		}
    }
}
