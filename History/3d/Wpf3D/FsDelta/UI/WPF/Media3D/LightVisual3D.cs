using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace FsDelta.UI.WPF.Media3D
{
	public class LightVisual3D : ModelVisual3D
	{
		public LightVisual3D() { }
		public LightVisual3D(object modelVisual3D)
		{
			ModelVisual3D mv = modelVisual3D as ModelVisual3D;
			Children.Add(mv);
		}
		public void AddLight(Color directColor, Vector3D direction, Color ambientColor)
		{
			AmbientLight al = new AmbientLight(ambientColor);
			DirectionalLight dl = new DirectionalLight(directColor, direction);
			ModelVisual3D ma = new ModelVisual3D();
			ModelVisual3D md = new ModelVisual3D();
			ma.Content = al;
			md.Content = dl;
			Children.Add(ma);
			Children.Add(md);
		}
	}
}
