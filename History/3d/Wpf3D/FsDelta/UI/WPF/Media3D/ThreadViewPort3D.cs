using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media.Media3D;

namespace FsDelta.UI.WPF.Media3D
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
		public void PlaceCamera(Point3D pos, Vector3D look, Vector3D up, double viewrange)
		{
			PerspectiveCamera cmr = new PerspectiveCamera(pos, look, up, viewrange);
			Camera = cmr;
		}
		public ThreadViewport3D() : base()
		{
		}

	}
}
