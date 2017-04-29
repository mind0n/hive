using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Media3D;
using Fs;
using System.Windows;
using System.Windows.Media;

namespace FsDelta.UI.WPF.Media3D
{

	public class MatrixHelper
	{
		public enum RotationType : byte
		{
			Free,
			LockAxisX,
			LockAxisY,
			LockAxisZ
		}
		public static Point3D O = new Point3D();
		public static Vector3D XAxis
		{
			get
			{
				return new Vector3D(1, 0, 0);
			}
		}
		public static Vector3D YAxis
		{
			get
			{
				return new Vector3D(0, 1, 0);
			}
		}
		public static Vector3D ZAxis
		{
			get
			{
				return new Vector3D(0, 0, 1);
			}
		}
		public static Matrix3D CalcRotationMatrix(double x, double y, double z)
		{
			return CalcRotationMatrix(x, y, z, new Point3D(), new Vector3D(0, 1, 0), new Vector3D(0, 0, -1), new MatrixTransform3D(), RotationType.Free);
		}
		public static Matrix3D CalcRotationMatrix(double x, double y, double z, Point3D center, Vector3D up, Vector3D look, Transform3D transform, RotationType type)
		{
			//Transform3DGroup trm = new Transform3DGroup();
			//trm.Children.Add(transform);
			Vector3D realup = transform.Transform(up);
			if (type != RotationType.LockAxisY)
			{
				up = realup;
			}
			if (type != RotationType.LockAxisZ)
			{
				look = transform.Transform(look);
			}
			center = transform.Transform(center);
			Vector3D axisX = Vector3D.CrossProduct(up, look);
			Matrix3D matrix = new Matrix3D();
			//Quaternion q = new Quaternion();
			//q.
			double ang = AngleBetween(realup, YAxis) + x;
			if (ang >= 90)
			{
				x = 90 - ang;
			}
			matrix.RotateAt(new Quaternion(axisX, x), center);
			matrix.RotateAt(new Quaternion(up, y), center);
			matrix.RotateAt(new Quaternion(look, z), center);
			return matrix;
		}
		public static MatrixTransform3D Rotate3D(Transform3D transform, Vector3D look, Vector3D dir, Point3D center)
		{
			Matrix3D m = new Matrix3D();
			Vector3D realook = transform.Transform(look);
			Vector3D axis = Vector3D.CrossProduct(realook, dir);
			double angle = Math.Acos(Vector3D.DotProduct(realook, dir));
			Quaternion q = new Quaternion(axis, angle);
			m.RotateAt(q, center);
			MatrixTransform3D rlt = transform as MatrixTransform3D;
			return new MatrixTransform3D(Matrix3D.Multiply(rlt.Matrix, m));
		}
		public static MatrixTransform3D Rotate3D(Transform3D transform, double x, double y, double z, Point3D center, Vector3D up, Vector3D look, RotationType type)
		{
			if (type != RotationType.LockAxisY)
			{
				up = transform.Transform(up);
			}
			if (type != RotationType.LockAxisZ)
			{
				look = transform.Transform(look);
			}
			center = transform.Transform(center);
			Vector3D axisX = Vector3D.CrossProduct(up, look);
			Matrix3D matrix = new Matrix3D();
			matrix.RotateAt(new Quaternion(axisX, x), center);
			matrix.RotateAt(new Quaternion(up, y), center);
			matrix.RotateAt(new Quaternion(look, z), center);
			MatrixTransform3D mOriginTransform = transform as MatrixTransform3D;
			//mOriginTransform.Matrix.RotateAt(
			try
			{
				return new MatrixTransform3D(Matrix3D.Multiply(mOriginTransform.Matrix, matrix));
			}
			catch (Exception err)
			{
				Exceptions.LogOnly(err);
				return null;
			}
		}
		public static MatrixTransform3D Offset(Transform3D transform, double x, double y, double z)
		{
			//Model3DGroup rlt = new Model3DGroup();
			Matrix3D m = new Matrix3D();
			MatrixTransform3D origin = transform as MatrixTransform3D;
			TranslateTransform3D tns = new TranslateTransform3D(x, y, z);
			m.Translate(new Vector3D(x, y, z));
			return new MatrixTransform3D(Matrix3D.Multiply(origin.Matrix, m));
			//Transform3DGroup grp = new Transform3DGroup();
			//grp.Children.Add(origin);
			//grp.Children.Add(new TranslateTransform3D(x, y, z));
			//return grp;
		}
		public static double AngleBetween(Vector3D a, Vector3D b)
		{
			a.Normalize();
			b.Normalize();
			return Math.Acos(Vector3D.DotProduct(a, b)) / Math.PI * 180;
		}
		public static Point? From3Dto2D(Visual3D space, Visual surface, Camera perspectiveCamera, Point3D target)
		{
			PerspectiveCamera cmr = perspectiveCamera as PerspectiveCamera;
			Point? rlt = new Point();
			Vector3D look = cmr.Transform.Transform(cmr.LookDirection);
			Vector3D d = target - cmr.Transform.Transform(cmr.Position);
			double angle = AngleBetween(d, look);
			GeneralTransform3DTo2D gt = space.TransformToAncestor(surface);
			rlt = gt.Transform(target);
			if (Math.Abs(angle) < cmr.FieldOfView / 2)
			{
				return rlt;
			}
			return null;
		}
	}
}
