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
using FsDelta.UI.WPF.Media3D;
using System.Windows.Media.Media3D;
using Fs.Entities;

namespace Wpf3D
{
	/// <summary>
	/// Interaction logic for Motion.xaml
	/// </summary>
	public partial class MotionWindow : FullScreenWindow
	{
		public static Point3D CmrPos = new Point3D();
		protected UnitVisual3D uBase = new UnitVisual3D();
		protected UnitVisual3D uPlatform = new UnitVisual3D();
		protected UnitVisual3D upper = new UnitVisual3D();
		protected EventCameraAgent ca;
		public MotionWindow()
			: base()
		{
			InitializeComponent();
			Loaded += new RoutedEventHandler(MotionWindow_Loaded);
			Init(true);
			vpMain.Width = Width;
			vpMain.Height = Height;
		}
		protected override void Init(bool isMainWindow)
		{
			base.Init(isMainWindow);
		}
		void MotionWindow_Loaded(object sender, RoutedEventArgs e)
		{
			Transform3DGroup gp = new Transform3DGroup();
			uBase.Load(@"3DModules\upper.xaml");
			ScaleTransform3D sc = new ScaleTransform3D(12, 12, 1, 0, 0, 0);
			TranslateTransform3D tt = new TranslateTransform3D(0, 0, -15.5);
			gp.Children.Add(sc);
			gp.Children.Add(tt);
			uBase.Transform = gp;
			uPlatform.Load(@"3DModules\platform.xaml");
			upper.Load(@"3DModules\upper.xaml");
			AbsoluteVisual.AddLight(Colors.Wheat, new Vector3D(-1, -1, -1), Colors.Gray);
			vpMain.PlaceCamera(
				new Point3D(0, 0, 500),
				new Vector3D(0, 0, -1),
				new Vector3D(0, 1, 0),
				45
			);
			AbsoluteVisual.Children.Add(uBase);
			AbsoluteVisual.Children.Add(uPlatform);
			AbsoluteVisual.Children.Add(upper);
			ca = new EventCameraAgent(vpMain);
			ca.MouseLockRelPos = new Point(1, 1);
			ca.BindEventTo(this);
			ca.OnCameraStatusChange += new CameraAgent.CameraTransformHandler(ca_OnCameraStatusChange);
		}

		void ca_OnCameraStatusChange(CameraAgent agent)
		{
			CmrPos = (vpMain.Camera as ProjectionCamera).Position;
			Point? p = MatrixHelper.From3Dto2D(AbsoluteVisual, vpMain, vpMain.Camera, new Point3D());
			if (p != null && !double.IsNaN(p.Value.X))
			{
				btnTest.Visibility = System.Windows.Visibility.Visible;
				btnTest.Content = p.Value.X + "," + p.Value.Y;
				Canvas.SetLeft(btnTest, p.Value.X);
				Canvas.SetTop(btnTest, p.Value.Y);
			}
			else
			{
				btnTest.Visibility = System.Windows.Visibility.Hidden;
			}
		}

		private void btnTest_Click(object sender, RoutedEventArgs e)
		{
			FrequentMessage message = uPlatform.Controller.Rotate(0, 0, 3600, 60000, false);
			message.Process();
		}

	}
}
