using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Media3D;

namespace FsDelta.UI.WPF
{
	public class MatrixHelper
	{
		public static Matrix3D CalculateRotationMatrix(double x, double y, double z)
		{
			return CalculateRotationMatrix(x, y, z, new Point3D(), new Vector3D(0, 1, 0), new Vector3D(0, 0, -1), new MatrixTransform3D());
		}
		public static Matrix3D CalculateRotationMatrix(double x, double y, double z, Point3D center, Vector3D up, Vector3D look, Transform3D transform)
		{
			//Transform3DGroup trm = new Transform3DGroup();
			//trm.Children.Add(transform);
			up = transform.Transform(up);
			look = transform.Transform(look);
			center = transform.Transform(center);
			Vector3D axisZ = Vector3D.CrossProduct(up, look);
			Matrix3D matrix = new Matrix3D();
			matrix.RotateAt(new Quaternion(axisZ, x), center);
			matrix.RotateAt(new Quaternion(up, y), center);
			matrix.RotateAt(new Quaternion(look, z), center);
			return matrix;
		}
	}
}
