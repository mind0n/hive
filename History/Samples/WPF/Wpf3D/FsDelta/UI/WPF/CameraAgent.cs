using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows;
using System.Windows.Threading;
using System.Timers;
using Fs.Native.Windows;
using Fs;

namespace FsDelta.UI.WPF
{

	public class CameraAgent
	{
		public delegate void CameraTransformHandler(CameraAgent agent);
		public bool IsValid
		{
			get
			{
				return cmr != null;
			}
		}
		public Point3D Position
		{
			get
			{
				return cmr.Position;
			}
			set
			{
				cmr.Position = value;
			}
		}
		protected double Interval = 32;
		protected ProjectionCamera cmr;
		protected ThreadViewport3D host;
		public CameraAgent(ThreadViewport3D evtHost)
		{
			host = evtHost;
			cmr = (ProjectionCamera)host.Camera;
		}
		public void PointAt(Point3D lookAt)
		{
			cmr.LookDirection = lookAt - cmr.Position;
		}
		public void RotateAround(double angleZ, double angleY, double miliseconds, CameraTransformHandler callback, CameraTransformHandler stepCallback)
		{
			ParamTimer tmr = new ParamTimer(Interval);
			tmr.DictParams["timer"] = tmr;
			tmr.DictParams["timeremains"] = miliseconds;
			tmr.DictParams["stepy"] = angleY / miliseconds * Interval;
			tmr.DictParams["stepz"] = angleZ / miliseconds * Interval;
			tmr.DictParams["stepcallback"] = stepCallback;
			tmr.DictParams["callback"] = callback;
			tmr.DictParams["$proceed"] = new ThreadViewport3D.VoidDictInvokes(ProceedRotate);
			tmr.Elapsed += new ElapsedEventHandler(tmr_Elapsed);
			tmr.Start();
		}
		//public void RotateAround(double angleZ, double angleY)
		//{
		//    Dictionary<string, object> par = new Dictionary<string, object>();
		//    par["stepy"] = angleY;
		//    par["stepz"] = angleZ;
		//    par["timer"] = null;
		//    ProceedRotate(par);
		//}
		public void RotateAround(double angleZ, double angleY)
		{
			RotateAround(angleZ, angleY, Interval, null, null);
		}
		public void Offset(Vector3D targetPos, Point3D? lookAt)
		{
			Offset(targetPos, lookAt, Interval, null, null);
			//cmr.Position = cmr.Position + targetPos;
			//if (lookAt != null)
			//{
			//    PointAt((Point3D)lookAt);
			//}
		}
		public void Offset(Vector3D targetPos, Point3D? lookAt, double millisecond, CameraTransformHandler callback, CameraTransformHandler stepCallback)
		{
			double ratio = Interval / millisecond;
			ParamTimer tmr = new ParamTimer(Interval);

			tmr.DictParams["step"] = new Vector3D(targetPos.X*ratio, targetPos.Y*ratio, targetPos.Z*ratio);
			tmr.DictParams["timer"] = tmr;
			tmr.DictParams["timeremains"] = millisecond;
			tmr.DictParams["lookat"] = lookAt;
			tmr.DictParams["callback"] = callback;
			tmr.DictParams["stepcallback"] = stepCallback;
			tmr.DictParams["$proceed"] = new ThreadViewport3D.VoidDictInvokes(ProceedOffset);
			tmr.Elapsed += new ElapsedEventHandler(tmr_Elapsed);
			tmr.Start();
		}
		void tmr_Elapsed(object sender, ElapsedEventArgs e)
		{
			ParamTimer tmr = (ParamTimer)sender;
			ThreadViewport3D.VoidDictInvokes proceed = (ThreadViewport3D.VoidDictInvokes)tmr.DictParams["$proceed"];
			try
			{
				tmr.Stop();
				host.Invoke(proceed, tmr.DictParams);
				tmr.Start();
			}
			catch (Exception err)
			{
				Exceptions.LogOnly(err);
			}
		}
		protected void ProceedOffset(Dictionary<string, object> parlist)
		{
			ParamTimer tmr = (ParamTimer)parlist["timer"];
			Point3D? lookAt = (parlist["lookat"] == null ? null : (Point3D?)parlist["lookat"]);
			CameraTransformHandler stepCallback = (CameraTransformHandler)parlist["stepcallback"];
			double remains = (double)parlist["timeremains"];
			Vector3D step = (Vector3D)parlist["step"];
			Matrix3D m = new Matrix3D();

			//cmr.Transform.

			cmr.Position += step; //new Point3D(cmr.Position.X + step.X, cmr.Position.Y + step.Y, cmr.Position.Z + step.Z);
			if (lookAt != null)
			{
				PointAt((Point3D)lookAt);
			} 
			parlist["timeremains"] = remains - tmr.Interval;
			if (remains - tmr.Interval <= 0)
			{
				tmr.Enabled = false;
				tmr.Stop();
				CameraTransformHandler callback = (CameraTransformHandler)parlist["callback"];
				if (callback != null)
				{
					callback(this);
					//callback.BeginInvoke(this, null, null);
				}
			}
			if (stepCallback != null)
			{
				stepCallback(this);
				//stepCallback.BeginInvoke(this, null, null);
			}
		}
		protected void ProceedRotate(Dictionary<string, object> parlist)
		{
			ParamTimer tmr = (ParamTimer)parlist["timer"];
			double interval = 0;
			double stepX = (double)parlist["stepz"];
			double stepY = (double)parlist["stepy"];
			CameraTransformHandler stepCallback = (CameraTransformHandler)parlist["stepcallback"];
			CameraTransformHandler callback = (CameraTransformHandler)parlist["callback"];
			double remains = 0;
			if (tmr != null)
			{
				interval = tmr.Interval;
				remains = (double)parlist["timeremains"];
			}
			//Quaternion delta = new Quaternion(Vector3D.CrossProduct(cmr.UpDirection, cmr.LookDirection), stepZ); //new Quaternion(cmr.UpDirection, stepY);
			//delta *= new Quaternion(cmr.UpDirection, stepY);
			try
			{
				Matrix3D m = MatrixHelper.CalculateRotationMatrix(-stepX, stepY, 0, cmr.Position, cmr.UpDirection, cmr.LookDirection, cmr.Transform);
				//m.RotateAt(delta, cmr.Position);
				Point3D p = cmr.Position;
				//m.RotateAtPrepend(delta, cmr.Position);
				MatrixTransform3D o = (MatrixTransform3D)cmr.Transform;
				cmr.Transform = new MatrixTransform3D(Matrix3D.Multiply(o.Matrix, m));
				parlist["timeremains"] = remains - interval;
				if (tmr != null && remains - interval <= 0)
				{
					tmr.Stop();
					if (callback != null)
					{
						callback(this);
					}
					return;
				}
				if (stepCallback != null)
				{
					stepCallback(this);
				}
			}
			catch (Exception err)
			{
				Exceptions.LogOnly(err);
			}
		}
	}
}
