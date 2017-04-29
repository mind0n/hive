using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Joy.Web
{
    public class JRegion : System.Web.UI.UserControl
    {
		protected JRegion LoadRegion(string virtualPath, string id = null, bool isClear = false)
		{
			if (isClear)
			{
				Controls.Clear();
			}
			Control container = FindControl(id) ?? this;
			if (container != null)
			{
				Control rlt = LoadControl(virtualPath);
				if (rlt != null)
				{
					container.Controls.Add(rlt);
				}
				return rlt as JRegion;
			}
			return null;
		}
    }
}
