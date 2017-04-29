using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;


namespace XNAWinForms
{

    public partial class XNAWinForm : Form
    {
		//public enum RegionRefreshMode
		//{
		//    Always,
		//    OnPanelPaint,        
		//}
		//public GraphicsDevice Device
		//{
		//    get { return mDevice; }
		//}
		//public RegionRefreshMode RefreshMode
		//{
		//    get { return mRefreshMode; }
		//    set
		//    {
		//        mRefreshMode = value;
		//    }
		//}

		private bool isLoaded;
		private bool isExiting;

		//private GraphicsDevice mDevice;
		//private RegionRefreshMode mRefreshMode = RegionRefreshMode.Always;
		//private Microsoft.Xna.Framework.Color mBackColor = Microsoft.Xna.Framework.Color.AliceBlue;

		//public delegate void GraphicsDeviceDelegate(GraphicsDevice pDevice);
		//public delegate void EmptyEventHandler();
		//public event GraphicsDeviceDelegate OnFrameRender = null;
		//public event GraphicsDeviceDelegate OnFrameMove = null;
		//public event EmptyEventHandler DeviceResetting = null;
		//public event GraphicsDeviceDelegate DeviceReset = null;
        
        public XNAWinForm()
        {
            InitializeComponent();

            // Foce color resfresh. (if panel has default backcolor, BackColorChanged won´t be called)
            this.panelViewport_BackColorChanged(null, EventArgs.Empty);
        }

        protected override void OnLoad(EventArgs e)
        {
			isLoaded = true;
            base.OnLoad(e);

			//CreateGraphicsDevice();

			//ResetGraphicsDevice();
        }

		protected override void OnShown(EventArgs e)
		{
			//LaunchRenderThread();
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			Thread.Sleep(100);
			isExiting = true;
			base.OnFormClosing(e);
			isLoaded = false;
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

		//private void CreateGraphicsDevice()
		//{
		//    // Create Presentation Parameters
		//    PresentationParameters pp = new PresentationParameters();
		//    pp.IsFullScreen = false;
		//    pp.BackBufferWidth = panelViewport.Width;
		//    pp.BackBufferHeight = panelViewport.Height;
		//    pp.PresentationInterval = PresentInterval.Default;
		//    pp.DeviceWindowHandle = this.panelViewport.Handle;

		//    // Create device
		//    //mDevice = new GraphicsDevice(
		//    //    GraphicsAdapter.DefaultAdapter,
		//    //    GraphicsProfile.Reach,
		//    //    pp);
		//}

		///// <summary>
		///// Resets the graphics device and calls the disposing and re-creating events
		///// </summary>
		//private void ResetGraphicsDevice()
		//{       
		//    // Avoid entering until panelViewport is setup and device created
		//    if (mDevice== null || panelViewport.Width == 0 || panelViewport.Height == 0)
		//        return;

		//    if (this.DeviceResetting != null)
		//        this.DeviceResetting();

		//    // Reset device
		//    mDevice.PresentationParameters.BackBufferWidth = panelViewport.Width;
		//    mDevice.PresentationParameters.BackBufferHeight = panelViewport.Height;
		//    mDevice.Reset();

		//    if (this.DeviceReset != null)
		//        this.DeviceReset(this.mDevice);
		//}  

		//public void Render()
		//{
		//    if (this.OnFrameMove != null)
		//        this.OnFrameMove(this.mDevice);

		//    mDevice.Clear(this.mBackColor);

		//    if (this.OnFrameRender != null)
		//        this.OnFrameRender(this.mDevice);
          
		//    mDevice.Present();

		//}	

		//private void OnViewportResize(object sender, EventArgs e)
		//{
		//    //ResetGraphicsDevice();
		//}

		//private void OnVieweportPaint(object sender, PaintEventArgs e)
		//{
		//    //if (this.mRefreshMode != RegionRefreshMode.Always)
		//    //{
		//    //    this.Render();
		//    //}
		//}

		//private void panelViewport_BackColorChanged(object sender, EventArgs e)
		//{
		//    //this.mBackColor = new Microsoft.Xna.Framework.Color(panelViewport.BackColor.R, panelViewport.BackColor.G, panelViewport.BackColor.B);
		//}
    }

}