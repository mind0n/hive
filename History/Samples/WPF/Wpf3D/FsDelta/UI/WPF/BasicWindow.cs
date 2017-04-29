using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace FsDelta.UI.WPF
{
	public class BasicWindow : Window
	{
		public BasicWindow()
			: base()
		{
			Init(true);
		}
		public BasicWindow(bool isMainWindow)
			: base()
		{
			Init(isMainWindow);
		}
		protected virtual void Init(bool isMainWindow)
		{
			if (isMainWindow)
			{
				Closed += new EventHandler(BasicWindow_Closed);
			}
			WindowStyle = System.Windows.WindowStyle.None;
			ResizeMode = System.Windows.ResizeMode.CanResize;
		}
		void BasicWindow_Closed(object sender, EventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}
