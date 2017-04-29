using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Joy.Web
{
	public class JPage : System.Web.UI.Page
	{
		protected JRegion LoadLayout(string virtualPath, string id = null, bool isClear = false)
		{
			if (isClear)
			{
				Controls.Clear();
			}
			Control container = this;
			if (id != null)
			{
				container = FindControl(id) ?? this;
			}
			Control layout = LoadControl(virtualPath);
			if (layout != null)
			{
				container.Controls.Add(layout);
			}
			return layout as JRegion;
		}
	}
}
