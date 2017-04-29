using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fs;

namespace OAWidgets.Widget
{
	public class WidgetCollection
	{
		public static Dictionary<string, BasicWidget> Widgets;
		static WidgetCollection()
		{
			Widgets = new Dictionary<string, BasicWidget>();
		}
		public static bool Add(BasicWidget widget)
		{
			string key = widget.Id;
			if (widget != null && !string.IsNullOrEmpty(key))
			{
				try
				{
					if (Widgets.ContainsKey(key))
					{
						Widgets[key] = widget;
					}
					else
					{
						Widgets.Add(key, widget);
					}
					return true;
				}
				catch (Exception err)
				{
					Exceptions.LogOnly(err);
					return false;
				}
			}
			return false;
		}
	}
}
