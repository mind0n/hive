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
using System.Windows.Input;
using Fs.Native.Windows.API;

namespace FsDelta.UI.WPF.Media3D
{

	public class CameraAgent
	{
		public delegate void CameraTransformHandler(CameraAgent agent);
		public event CameraTransformHandler OnCameraStatusChange;
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
		public Point MouseLockPos = new Point(201, 201);
		public Point MouseLockRelPos = new Point();
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
			
			tmr.DictParams["step"] = new Vector3D(targetPos.X*ratio, targetPos.Y*ratio * 0, targetPos.Z*ratio);
			tmr.DictParams["timer"] = tmr;
			tmr.DictParams["timeremains"] = millisecond;
			tmr.DictParams["lookat"] = lookAt;
			tmr.DictParams["callback"] = callback;
			tmr.DictParams["stepcallback"] = stepCallback;
			tmr.DictParams["$proceed"] = new ThreadViewport3D.VoidDictInvokes(ProceedOffset);
			tmr.Elapsed += new ElapsedEventHandler(tmr_Elapsed);
			tmr.Start();
		}
		public void RotateAround(double angleZ, double angleY, double miliseconds, CameraTransformHandler callback, CameraTransformHandler stepCallback)
		{
			ParamTimer tmrRot = new ParamTimer(Interval);
			tmrRot.DictParams["timer"] = tmrRot;
			tmrRot.DictParams["timeremains"] = miliseconds;
			tmrRot.DictParams["stepy"] = angleY / miliseconds * Interval;
			tmrRot.DictParams["stepz"] = angleZ / miliseconds * Interval;
			tmrRot.DictParams["stepcallback"] = stepCallback;
			tmrRot.DictParams["callback"] = callback;
			tmrRot.DictParams["$proceed"] = new ThreadViewport3D.VoidDictInvokes(ProceedRotate);
			tmrRot.Elapsed += new ElapsedEventHandler(tmr_Elapsed);
			tmrRot.Start();
		}
		public void RotateAround(double angleZ, double angleY)
		{
			RotateAround(angleZ, angleY, Interval, null, null);
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
			if (OnCameraStatusChange != null)
			{
				OnCameraStatusChange(this);
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
				Matrix3D m = MatrixHelper.CalcRotationMatrix(-stepX, stepY, 0, cmr.Position, new Vector3D(0, 1, 0), cmr.LookDirection, cmr.Transform, MatrixHelper.RotationType.LockAxisY);
				parlist["timeremains"] = remains - interval;
				//m.RotateAt(delta, cmr.Position);
				//m.RotateAtPrepend(delta, cmr.Position);
				MatrixTransform3D o = (MatrixTransform3D)cmr.Transform;
				cmr.Transform = new MatrixTransform3D(Matrix3D.Multiply(o.Matrix, m));
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
				if (OnCameraStatusChange != null)
				{
					OnCameraStatusChange(this);
				}
			}
			catch (Exception err)
			{
				Exceptions.LogOnly(err);
			}
		}
		protected void tmr_Elapsed(object sender, ElapsedEventArgs e)
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
	}
	public class EventCameraAgent : CameraAgent
	{
		public delegate void DlgMouseDownEventHandler(object sender, MouseButtonEventArgs e);
		public delegate void DlgMouseMoveEventHandler(object sender, Point [] pos);
		public DlgMouseDownEventHandler OnMouseDown;
		public DlgMouseMoveEventHandler OnMouseMove;
		public ThreadViewport3D Host
		{
			get
			{
				return host;
			}
		}
		public bool IsMouseLocked
		{
			get
			{
				return vIsMouseLocked;
			}
		}
		public Point MousePos = new Point();
		protected bool ignoreMouseMove = true;
		protected bool vIsMouseLocked = false;
		protected Timer keyTmr;
		protected Vector3D posOffset;
		protected Point? originMousePos = null;
		public EventCameraAgent(ThreadViewport3D host)
			: base(host)
		{
			keyTmr = new Timer();
			keyTmr.Interval = Interval;
			keyTmr.Elapsed += new ElapsedEventHandler(keyTmr_Elapsed);
		}
		void host_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Up || e.Key == Key.W)
			{
				posOffset.Z = 0;
			}
			if (e.Key == Key.Down || e.Key == Key.S)
			{
				posOffset.Z = 0;
			}
			if (e.Key == Key.Left || e.Key == Key.A)
			{
				posOffset.X = 0;
			}
			if (e.Key == Key.Right || e.Key == Key.D)
			{
				posOffset.X = 0;
			}
		}

		void host_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Up || e.Key == Key.W)
			{
				posOffset.Z = -10;
			}
			if (e.Key == Key.Down || e.Key == Key.S)
			{
				posOffset.Z = 10;
			}
			if (e.Key == Key.Left || e.Key == Key.A)
			{
				posOffset.X = -10;
			}
			if (e.Key == Key.Right || e.Key == Key.D)
			{
				posOffset.X = 10;
			}
		}

		void host_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.RightButton == MouseButtonState.Pressed)
			{
				LockMouse();
			}
			if (OnMouseDown != null)
			{
				OnMouseDown(sender, e);
			}
		}

		void host_MouseMove(object sender, MouseEventArgs e)
		{
			Vector d = new Vector();
			Point p = e.GetPosition(host);
			if (!vIsMouseLocked)
			{
				MousePos = p;
			}
			if (!ignoreMouseMove)
			{
				//originMousePos = new Point((int)(host.ActualWidth / 2), (int)(host.ActualHeight / 2));
				originMousePos = new Point(MouseLockPos.X, MouseLockPos.Y);
				d = p - (Point)originMousePos;
				double az = (int)-d.Y;
				double ay = (int)-d.X;
				az += MouseLockRelPos.X;
				ay += MouseLockRelPos.Y;
				RotateAround(az / 10, ay / 10, 320, null, null);
				ignoreMouseMove = true;
				
				if (OnMouseMove != null)
				{
					OnMouseMove(sender, new Point[] { (Point)originMousePos, p });
				}
				//WinAPI.SetCursorPos(host.ActualWidth / 2, host.ActualHeight / 2);
				WinAPI.SetCursorPos(MouseLockPos.X, MouseLockPos.Y);
			}
			else
			{
				if (vIsMouseLocked)
				{
					ignoreMouseMove = false;
				}
			}
		}
		void hostWindow_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (e.RightButton == MouseButtonState.Released)
			{
				ReleaseMouse();
			}
		}
		public void ReleaseMouse()
		{
			vIsMouseLocked = false;
			ignoreMouseMove = true;
			Mouse.OverrideCursor = null;
			WinAPI.SetCursorPos(MousePos.X, MousePos.Y);
		}
		public void LockMouse()
		{
			vIsMouseLocked = true;
			ignoreMouseMove = false;
			Mouse.OverrideCursor = Cursors.None;
			//WinAPI.SetCursorPos(host.ActualWidth / 2, host.ActualHeight / 2);
			WinAPI.SetCursorPos(MouseLockPos.X, MouseLockPos.Y);
		}
		public void BindEventTo(UIElement hostWindow)
		{
			if (hostWindow != null)
			{
				//WinAPI.SetCursorPos(host.ActualWidth / 2, host.ActualHeight / 2);
				WinAPI.SetCursorPos(MouseLockPos.X, MouseLockPos.Y);
				MousePos.X = host.ActualWidth / 2;
				MousePos.Y = host.ActualHeight / 2;
				hostWindow.MouseUp += new MouseButtonEventHandler(hostWindow_MouseUp);
				hostWindow.MouseMove += new MouseEventHandler(host_MouseMove);
				hostWindow.MouseDown += new MouseButtonEventHandler(host_MouseDown);
				hostWindow.KeyDown += new KeyEventHandler(host_KeyDown);
				hostWindow.KeyUp += new KeyEventHandler(host_KeyUp);
				keyTmr.Enabled = true;
				keyTmr.Start();
			}
			else
			{
				ReleaseEventHost();
			}
		}

		public void ReleaseEventHost()
		{
			keyTmr.Enabled = false;
			keyTmr.Stop();
			if (host != null)
			{
				host.MouseMove -= new MouseEventHandler(host_MouseMove);
				host.MouseDown -= new MouseButtonEventHandler(host_MouseDown);
				host.KeyDown -= new KeyEventHandler(host_KeyDown);
				host.KeyUp -= new KeyEventHandler(host_KeyUp);
			}
		}
		void keyTmr_Elapsed(object sender, ElapsedEventArgs e)
		{
			keyTmr.Stop();
			if (posOffset.X != 0 || posOffset.Y != 0 || posOffset.Z != 0)
			{
				Offset(posOffset, null, 320, null, null);
			}
			keyTmr.Start();
		}
	}
}
