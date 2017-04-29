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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.IO;
using System.Windows.Media.Media3D;
using System.Timers;
using Fs;
using Fs.Native.Windows;
using Fs.Native.Windows.API;
using FsDelta.UI.WPF;
using FsDelta.UI.WPF.Media3D;

namespace Wpf3D
{
	/// <summary>
	/// Interaction logic for Main Window
	/// </summary>
	public partial class MainWindow : BasicWindow, System.Windows.Markup.IComponentConnector 
	{
		public delegate void VoidDictInvokes(Dictionary<string, object> parlist);
		protected ParamTimer tmr;
		protected Model3DGroup grpMain;
		protected LightVisual3D lvMain;
		protected EventCameraAgent ca;
		public MainWindow(bool isMainWindow)
			: base()
		{
			InitializeComponent();
			WindowStyle = System.Windows.WindowStyle.None;
			WindowState = WindowState.Maximized;
			Width = SystemParameters.PrimaryScreenWidth;
			Height = SystemParameters.PrimaryScreenHeight;
			vpMain.Width = Width;
			vpMain.Height = Height;
			try
			{
				BitmapImage bi = new BitmapImage();
				bi.BeginInit();
				bi.CacheOption = BitmapCacheOption.OnLoad;
				bi.UriSource = new Uri("bg.png", UriKind.Relative);
				bi.EndInit();
				Background = new ImageBrush(bi);
			}
			catch (Exception err)
			{
				Exceptions.LogOnly(err);
			}
			Loaded += new RoutedEventHandler(MainWindow_Loaded);
		}
		void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			object oVisual = XamlReader.Load
			(new FileStream
			(@"..\..\3DModules\Fighter.xaml", FileMode.Open));
			vpMain.Camera = new PerspectiveCamera(
				new Point3D(-500, 0, 0),
				new Vector3D(0, 0, -1),
				new Vector3D(0, 1, 0),
				45
			);
			
			lvMain = new LightVisual3D();
			grpMain = new Model3DGroup();
			lvMain.Content = grpMain;
			grpMain.Children.Add(oVisual as Model3DGroup);
			lvMain.AddLight(Colors.Wheat, new Vector3D(-1, -1, -1), Colors.Gray);
			vpMain.Children.Add(lvMain);
			for (int i = 0; i <= 200; i++)
			{
				
				LightVisual3D lv = new LightVisual3D();
				Model3DGroup grp = new Model3DGroup();
				lv.Content = grp;
				grp.Children.Add(oVisual as Model3DGroup);
				lv.Transform = new TranslateTransform3D(0, 0, -100 * i);
				lvMain.Children.Add(lv);
			}
			ca = new EventCameraAgent(vpMain);
			ca.BindEventTo(this);
			ca.OnMouseDown += MainWindow_MouseDown;
			ca.OnMouseMove += MainWindow_MouseMove;
			//WinAPI.SetCursorPos(ActualWidth / 2, ActualHeight / 2);
			WinAPI.SetCursorPos(200, 200);
			MouseDown += new MouseButtonEventHandler(MainWindow_MouseDown);
			Activated += new EventHandler(MainWindow_Activated);
			
		}
		void MainWindow_Activated(object sender, EventArgs e)
		{
			//Visibility = Visibility.Visible;
			WinAPI.SetCursorPos(ActualWidth / 2, ActualHeight / 2);
		}
		void MainWindow_MouseMove(object sender, Point[] pos)
		{
			btnTest.Content = pos[1].ToString() + "; " + pos[0].ToString() + "; " + ca.MousePos.ToString();
			
		}

		void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				//Visibility = Visibility.Hidden;
				WindowState = WindowState.Minimized;
			}
		}
		private void RelocateBtn(CameraAgent cmragent)
		{
			ResetBtnLocation(new Point3D(0, 0, 50));
		}
		public void GetTransform3DGroup(Model3D model)
		{
		}
		private void ResetBtnLocation(Point3D point)
		{
			Model3D m = (Model3D)(((Model3DGroup)grpMain.Children[0]).Children[0]);

			Transform3DGroup tgp = new Transform3DGroup();
			tgp.Children.Add(grpMain.Transform);
			tgp.Children.Add(m.Transform);

			GeneralTransform3DTo2D gt = lvMain.TransformToAncestor(vpMain);//vpmain.TransformToDescendant(...);
			Point p = gt.Transform(tgp.Transform(point));
			Canvas.SetLeft(btnTest, p.X);
			Canvas.SetTop(btnTest, p.Y);
			btnTest.Content = p.X + "," + p.Y;
		}
		Model3DGroup Copy(Model3DGroup origin)
		{
			Model3DGroup target = new Model3DGroup();
			foreach (Model3D m in origin.Children)
			{
				target.Children.Add(m);
			}
			return target;
		}
		void offsetCallbackLeftUp(CameraAgent ca)
		{
			ca.Offset(new Vector3D(1000, 0, 0), new Point3D(0, 0, 50), 6000, offsetCallbackRight, RelocateBtn);
		}
		void offsetCallbackRight(CameraAgent ca)
		{
			ca.Offset(new Vector3D(0, 0, 2000), new Point3D(0, 0, 50), 6000, offsetCallbackDown, RelocateBtn);
		}
		void offsetCallbackDown(CameraAgent ca)
		{
			ca.Offset(new Vector3D(-1000, 0, 0), new Point3D(0, 0, 50), 6000, offsetCallback, RelocateBtn);
		}
		void offsetCallback(CameraAgent ca)
		{
			ca.Offset(new Vector3D(0, 0, -2000), new Point3D(0, 0, 50), 6000, offsetCallbackLeftUp, RelocateBtn);
		}
	}
}
