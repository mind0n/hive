using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;

namespace FsDelta.UI.WPF
{
	public class ThreadViewport3D : Viewport3D
	{
		public delegate void VoidDictInvokes(Dictionary<string, object> parlist);
		public delegate void VoidListInvokes(List<object> parlist);
		public delegate object ParamInvokes(params object[] parlist);
		public delegate void VoidInvokes();
		public DispatcherOperation Invoke(Delegate method, params object[] parameters)
		{
			return Dispatcher.BeginInvoke(method, parameters);
		}
		public ThreadViewport3D() : base()
		{
		}

	}
}
