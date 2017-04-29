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

namespace Game.Wpf
{
	/// <summary>
	/// Interaction logic for RenderRegion.xaml
	/// </summary>
	public partial class RenderRegion : UserControl
	{
		public RenderRegion()
		{
			InitializeComponent();
		}
		public void AddImage(string path, TileMode tile = TileMode.None, Rect? targetArea = null, Stretch stretch = Stretch.Fill, Rectangle rect = null, UriKind kind = UriKind.Relative)
		{
			BitmapImage bm = new BitmapImage(new Uri(path, kind));
			UIElement el = null;
			if (tile != TileMode.None && rect == null)
			{
				Rectangle r = new Rectangle();
				if (targetArea.HasValue)
				{
					r.Width = targetArea.Value.Width;
					r.Height = targetArea.Value.Height;
				}
				else
				{
					r.Width = this.ActualWidth;
					r.Height = this.ActualHeight;
				}
				Rect rr = new Rect();
				rr.Width = bm.PixelWidth;
				rr.Height = bm.PixelHeight;
				ImageDrawing d = new ImageDrawing(bm, rr);
				DrawingBrush brush = new DrawingBrush(d);
				brush.Viewport = rr;
				brush.ViewportUnits = BrushMappingMode.Absolute;
				brush.TileMode = tile;
				brush.Stretch = Stretch.Fill;
				r.Fill = brush;
				el = r;
			}
			else
			{
				Image img = new Image();
				img.Width = bm.Width;
				img.Height = bm.Height;
				img.Source = bm;
				el = img;
			}
			RenderCanvas.Children.Add(el);
			if (targetArea.HasValue)
			{
				Canvas.SetLeft(el, targetArea.Value.X);
				Canvas.SetTop(el, targetArea.Value.Y);
			}
			else
			{
				Canvas.SetLeft(el, 0);
				Canvas.SetTop(el, 0);
			}
		}
	}
}
