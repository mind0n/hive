using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ULib.Winform.Interface
{
	public class ActionEventArgs : EventArgs
	{
	}
	public delegate void ActionCallback(IActionControl sender, ActionEventArgs args);
	public interface IActionControl
	{
		event ActionCallback ActionCallback;
		void NotifyAction(IActionControl sender, ActionEventArgs argument);
	}
}
