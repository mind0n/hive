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

namespace Wpf.Startup
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Loaded += MainWindow_Loaded;
		}

		void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			WindowState = System.Windows.WindowState.Maximized;
			WindowStyle = System.Windows.WindowStyle.None;
			this.RenderRegion.AddImage("resources/images/ground.jpg", TileMode.Tile);
			this.RenderRegion.Width = this.MainGrid.Width;
			this.RenderRegion.Height = this.MainGrid.Height;
			this.RenderRegion.MouseDown += RenderRegion_MouseDown;
			this.RenderRegion.MouseDoubleClick += RenderRegion_MouseDoubleClick;
		}

		void RenderRegion_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			
		}

		void RenderRegion_MouseDown(object sender, MouseButtonEventArgs e)
		{
			
		}
	}
}
