using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace XnaEngine.Winform
{
	public partial class XnaPanel : UserControl
	{
		private bool isExiting;
		public enum RegionRefreshMode
		{
			Always,
			OnPanelPaint,
		}
		public GraphicsDevice Device
		{
			get { return mDevice; }
		}
		public RegionRefreshMode RefreshMode
		{
			get { return mRefreshMode; }
			set
			{
				mRefreshMode = value;
			}
		}
		private GraphicsDevice mDevice;
		private RegionRefreshMode mRefreshMode = RegionRefreshMode.Always;
		private Microsoft.Xna.Framework.Color mBackColor = Microsoft.Xna.Framework.Color.AliceBlue;

		public delegate void GraphicsDeviceDelegate(GraphicsDevice pDevice);
		public delegate void EmptyEventHandler();
		public event GraphicsDeviceDelegate OnFrameRender = null;
		public event GraphicsDeviceDelegate OnFrameMove = null;
		public event EmptyEventHandler DeviceResetting = null;
		public event GraphicsDeviceDelegate DeviceReset = null;

		public XnaPanel()
		{
			InitializeComponent();
			panelViewport.Paint += new PaintEventHandler(xpanel_Paint);
			panelViewport.Resize += new EventHandler(xpanel_Resize);
			panelViewport.BackColorChanged += new EventHandler(xpanel_BackColorChanged);
		}

		void xpanel_BackColorChanged(object sender, EventArgs e)
		{
			this.mBackColor = new Microsoft.Xna.Framework.Color(panelViewport.BackColor.R, panelViewport.BackColor.G, panelViewport.BackColor.B);
		}

		void xpanel_Resize(object sender, EventArgs e)
		{
			ResetGraphicsDevice();
		}

		void xpanel_Paint(object sender, PaintEventArgs e)
		{
			if (this.mRefreshMode != RegionRefreshMode.Always)
			{
				this.Render();
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			CreateGraphicsDevice();
			ResetGraphicsDevice();
			LaunchRenderThread();
		}
		private void LaunchRenderThread()
		{
			ThreadStart ts = new ThreadStart(delegate()
			{
				while (!isExiting)
				{
					try
					{
						Invoke((MethodInvoker)delegate()
						{
							try
							{
								Render();
							}
							catch { }
						});
						Thread.Sleep(10);
					}
					catch
					{
						break;
					}
				}
			});
			Thread th = new Thread(ts);
			th.IsBackground = true;
			th.Start();
		}

		private void CreateGraphicsDevice()
		{
			// Create Presentation Parameters
			PresentationParameters pp = new PresentationParameters();
			pp.IsFullScreen = false;
			pp.BackBufferWidth = panelViewport.Width;
			pp.BackBufferHeight = panelViewport.Height;
			pp.PresentationInterval = PresentInterval.Default;
			pp.DeviceWindowHandle = this.panelViewport.Handle;

			// Create device
			mDevice = new GraphicsDevice(
				GraphicsAdapter.DefaultAdapter,
				GraphicsProfile.Reach,
				pp);
		}

		/// <summary>
		/// Resets the graphics device and calls the disposing and re-creating events
		/// </summary>
		private void ResetGraphicsDevice()
		{
			// Avoid entering until panelViewport is setup and device created
			if (mDevice == null || panelViewport.Width == 0 || panelViewport.Height == 0)
				return;

			if (this.DeviceResetting != null)
				this.DeviceResetting();

			// Reset device
			mDevice.PresentationParameters.BackBufferWidth = panelViewport.Width;
			mDevice.PresentationParameters.BackBufferHeight = panelViewport.Height;
			mDevice.Reset();

			if (this.DeviceReset != null)
				this.DeviceReset(this.mDevice);
		}

		public void Render()
		{
			if (this.OnFrameMove != null)
				this.OnFrameMove(this.mDevice);

			mDevice.Clear(this.mBackColor);

			if (this.OnFrameRender != null)
				this.OnFrameRender(this.mDevice);

			mDevice.Present();

		}
	}
}
