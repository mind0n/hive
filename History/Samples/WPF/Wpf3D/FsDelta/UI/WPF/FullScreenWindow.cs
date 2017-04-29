using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Fs;

namespace FsDelta.UI.WPF
{
	public class FullScreenWindow : BasicWindow
	{
		public FullScreenWindow() : base()
		{
			ResizeMode = ResizeMode.NoResize;
			WindowState = WindowState.Maximized;
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
			Width = SystemParameters.PrimaryScreenWidth;
			Height = SystemParameters.PrimaryScreenHeight;
			try
			{
				BitmapImage bi = new BitmapImage();
				bi.BeginInit();
				bi.CacheOption = BitmapCacheOption.OnLoad;
				bi.UriSource = new Uri("[Metal_Brass_Ceiling].jpg", UriKind.Relative);
				bi.EndInit();
				Background = new ImageBrush(bi);
			}
			catch (Exception err)
			{
				Exceptions.LogOnly(err);
			}
		}

	}
}
