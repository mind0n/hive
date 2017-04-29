using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fs.UI.Controls;

namespace OAWidgets.Widget
{
	public interface PlatformAgent
	{
		EventTabPage CreateNewTab(string tbName);
	}
}
