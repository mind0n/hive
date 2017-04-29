using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FsDelta.UI.WPF;
using System.Windows.Media.Media3D;
using FsDelta.UI.WPF.Media3D;
using Fs;
using Fs.Native.Windows.API;
using System.Timers;
using Fs.Entities;

namespace Wpf3D
{
	/// <summary>
	/// Interaction logic for Test.xaml
	/// </summary>
	public partial class Test : FullScreenWindow
	{
		Timer t;
		public Test()
		{
			InitializeComponent();
			Loaded += new RoutedEventHandler(Test_Loaded);
		}

		void Test_Loaded(object sender, RoutedEventArgs e)
		{
			//ModelVisual3D mv = vpMain.Children[0] as ModelVisual3D;
			//GeneralTransform3DTo2D gt = mv.TransformToAncestor(cvMain);
			//Point p = gt.Transform(new Point3D(1, 1, 1));
			//Canvas.SetLeft(btnTest, p.X);
			//Canvas.SetTop(btnTest, p.Y);
			this.WindowState = WindowState.Maximized;
			this.WindowStyle = WindowStyle.None;
			Width = SystemParameters.PrimaryScreenWidth;
			Height = SystemParameters.PrimaryScreenHeight;
			vpMain.Width = ActualWidth;
			vpMain.Height = ActualHeight;
			
			for (int i = 1; i <= 100; i++)
			{
				ModelVisual3D mv = new ModelVisual3D();
				mv.Content = mCube;
				mv.Transform = new TranslateTransform3D(0, 20 * i, 0);
				uMain.Children.Add(mv);
			}

			EventCameraAgent ca = new EventCameraAgent(vpMain);
			ca.MouseLockRelPos = new Point(1, 1);
			ca.BindEventTo(this);
			ca.OnMouseDown += new EventCameraAgent.DlgMouseDownEventHandler(delegate(object sdr, MouseButtonEventArgs evt)
			{
				if (evt.LeftButton == MouseButtonState.Pressed)
				{
					WindowState = WindowState.Minimized;
				}
			});
			ca.OnCameraStatusChange += CSC;
			t = new Timer(32);
			t.Elapsed += new ElapsedEventHandler(t_Elapsed);
			t.Enabled = true;
			t.Start();
			uMain.OriginPosition = new Point3D(5, 5, 5);
			uMain.Controller.Start();
			Activated += new EventHandler(Test_Activated);
		}

		void t_Elapsed(object sender, ElapsedEventArgs e)
		{
			t.Stop();
			FrequentMessage fmOffset = uMain.Controller.Offset(10, 10, 0, 10000, true);
			FrequentMessage fmRotate = uMain.Controller.Rotate(0, 360, 0, 6000, false);
			uMain.Controller.Wait(fmOffset);
			uMain.Controller.Wait(fmRotate);
			t.Start();
		}
		void CSC(CameraAgent agt)
		{
			RelocateBtn();
			//btnInf.Content = agt.;
			object oup = vpMain.FindName("mup");
		}
		void RelocateBtn()
		{
			
			GeneralTransform3DTo2D gt = vAbsolute.TransformToAncestor(vpMain);
			Point? ro = MatrixHelper.From3Dto2D(vAbsolute, vpMain, vpMain.Camera, new Point3D());
			Point? rx = MatrixHelper.From3Dto2D(vAbsolute, vpMain, vpMain.Camera,new Point3D(10, 0, 0));
			Point? ry = MatrixHelper.From3Dto2D(vAbsolute, vpMain, vpMain.Camera,new Point3D(0, 10, 0));
			Point? rz = MatrixHelper.From3Dto2D(vAbsolute, vpMain, vpMain.Camera,new Point3D(0, 0, 10));
			if (ro == null)
			{
				btnO.Visibility = Visibility.Hidden;
			}
			else
			{
				btnO.Visibility = Visibility.Visible;
				Canvas.SetLeft(btnO, ro.Value.X);
				Canvas.SetTop(btnO, ro.Value.Y);
				btnO.Content = "O | " + ro.Value.X + "," + ro.Value.Y;
			}
			if (rx == null)
			{
				btnX.Visibility = Visibility.Hidden;
			}
			else
			{
				btnX.Visibility = Visibility.Visible;
				Canvas.SetLeft(btnX, rx.Value.X);
				Canvas.SetTop(btnX, rx.Value.Y);
				btnX.Content = "X | " + rx.Value.X + "," + rx.Value.Y;
			}
			if (ry == null)
			{
				btnY.Visibility = Visibility.Hidden;
			}
			else
			{
				btnY.Visibility = Visibility.Visible;
				Canvas.SetLeft(btnY, ry.Value.X);
				Canvas.SetTop(btnY, ry.Value.Y);
				btnY.Content = "Y | " + ry.Value.X + "," + ry.Value.Y;
			}
			if (rz == null)
			{
				btnZ.Visibility = Visibility.Hidden;
			}
			else
			{
				btnZ.Visibility = Visibility.Visible;
				Canvas.SetLeft(btnZ, rz.Value.X);
				Canvas.SetTop(btnZ, rz.Value.Y);
				btnZ.Content = "Z | " + rz.Value.X + "," + rz.Value.Y;
			}
		}
		void Test_Activated(object sender, EventArgs e)
		{
			//WinAPI.SetCursorPos(ActualWidth / 2, ActualHeight / 2);
			WinAPI.SetCursorPos(200, 200);
		}
	}
}
