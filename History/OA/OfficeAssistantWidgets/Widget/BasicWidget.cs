using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fs.UI.Controls;
using System.Reflection;

namespace OAWidgets.Widget
{
	
	public class BasicWidget : BasicForm
	{
		public virtual string Id { get { return null; } }
		public string BaseDir { get; set; }
		protected BasicWidget() : base()
		{
		}
		public virtual void Launch(PlatformAgent agent)
		{
			
		}
	}
}
