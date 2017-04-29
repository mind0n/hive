using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SharpGL
{
   	/// <summary>
	/// This is the basic OpenGL control object, it gives all of the basic OpenGL functionality.
	/// </summary>
    [System.Drawing.ToolboxBitmap(typeof(OpenGLControl), "SharpGL.png")]
    public partial class OpenGLControl : UserControl, ISupportInitialize
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGLControl"/> class.
        /// </summary>
        public OpenGLControl()
        {
            InitializeComponent();

            //  Set the user draw styles.
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            Load += OpenGLControl_Load;
            timerDrawing.Interval = 1000 / FrameRate;
            timerDrawing.Enabled = true;
            this.DoubleBuffered = true;
        }

        Timer t = new Timer();
        void OpenGLControl_Load(object sender, EventArgs e)
        {
            t.Interval = 100;
            t.Tick += t_Tick;
            t.Enabled = true;
            t.Start();
        }

        void t_Tick(object sender, EventArgs e)
        {
            t.Stop();
//            t.Start();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //if (RenderContextType == SharpGL.RenderContextType.NativeWindow)
            //    return;
            //base.OnPaintBackground(e);
            g = e.Graphics;
            Render();
        }

        /// <summary>
        /// Initialises OpenGL.
        /// </summary>
        protected void InitialiseOpenGL()
        {
            object parameter = null;
           
            //  Native render context providers need a little bit more attention.
            if(RenderContextType == SharpGL.RenderContextType.NativeWindow)
            {
                SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
                parameter = (object)Handle;
            }

            //  Create the render context.
            gl.Create(RenderContextType, Width, Height, 32, parameter);

            //  Set the most basic OpenGL styles.
            gl.ShadeModel(OpenGL.GL_SMOOTH);
            gl.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            gl.ClearDepth(1.0f);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.DepthFunc(OpenGL.GL_LEQUAL);
            gl.Hint(OpenGL.GL_PERSPECTIVE_CORRECTION_HINT, OpenGL.GL_NICEST);

            //  Fire the OpenGL initialized event.
            DoOpenGLInitialized();

            //  Set the draw timer.
            timerDrawing.Tick += new EventHandler(timerDrawing_Tick);
        }

        Graphics g;

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
        }

        private void Render()
        {
            if (g == null)
            {
                return;
            }
            //g = this.CreateGraphics();
            //  Start the stopwatch so that we can time the rendering.
            stopwatch.Restart();

            //	Make sure it's our instance of openSharpGL that's active.
            OpenGL.MakeCurrent();

            //	If there is a draw handler, then call it.
            DoOpenGLDraw();

            //  Draw the FPS.
            if (DrawFPS)
            {
                OpenGL.DrawText(5, 5, 1.0f, 0.0f, 0.0f, "Courier New", 12.0f,
                    string.Format("Draw Time: {0:0.0000} ms ~ {1:0.0} FPS", frameTime, 1000.0 / frameTime));
                OpenGL.Flush();
            }

            //	Blit our offscreen bitmap.
            IntPtr handleDeviceContext = g.GetHdc();
            OpenGL.Blit(handleDeviceContext);
            g.ReleaseHdc(handleDeviceContext);

            //  Stop the stopwatch.
            stopwatch.Stop();

            //  Store the frame time.
            frameTime = stopwatch.Elapsed.TotalMilliseconds;
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            //  If we do not have a render context there is nothing more
            //  we can do.
            if (OpenGL.RenderContextProvider == null)
                return;

            //	Resize the DIB Surface.
            OpenGL.SetDimensions(Width, Height);

            //	Set the viewport.
            gl.Viewport(0, 0, Width, Height);

            //  If we have a project handler, call it...
            if (Width != -1 && Height != -1)
            {
                var handler = Resized;
                if (handler != null)
                    handler(this, e);
                else
                {                    
                    //  Otherwise we do our own projection.
                    gl.MatrixMode(OpenGL.GL_PROJECTION);
                    gl.LoadIdentity();

                    // Calculate The Aspect Ratio Of The Window
                    gl.Perspective(45.0f, (float)Width / (float)Height, 0.1f, 100.0f);

                    gl.MatrixMode(OpenGL.GL_MODELVIEW);
                    gl.LoadIdentity();
                }
            }

            Invalidate();
        }

        /// <summary>
        /// Calls the OpenGL initialized function.
        /// </summary>
        protected virtual void DoOpenGLInitialized()
        {
            var handler = OpenGLInitialized;
            if (handler != null)
                handler(this, null);
        }

        /// <summary>
        /// Call this function in derived classes to do the OpenGL Draw event.
        /// </summary>
        protected virtual void DoOpenGLDraw()
        {
            //var handler = OpenGLDraw;
            //if (handler != null)
            //    handler(this, e);
            if (OnGLDraw != null)
            {
                OnGLDraw();
            }
        }

        /// <summary>
        /// Call this function in derived classes to do the GDI Draw event.
        /// </summary>
        protected virtual void DoGDIDraw(PaintEventArgs e)
        {
            var handler = GDIDraw;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Handles the Tick event of the timerDrawing control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void timerDrawing_Tick(object sender, EventArgs e)
        {
            //  The timer for drawing simply invalidates the control at a regular interval.
            //Invalidate();
        }

        /// <summary>
        /// Signals the object that initialization is starting.
        /// </summary>
        void ISupportInitialize.BeginInit()
        {
        }

        /// <summary>
        /// Signals the object that initialization is complete.
        /// </summary>
        void ISupportInitialize.EndInit()
        {
            InitialiseOpenGL();
            OnSizeChanged(null);
        }

        [Description("Called when OpenGL has been initialized occur."), Category("SharpGL")]
        public event EventHandler OpenGLInitialized;

        [Description("Called whenever OpenGL drawing can should occur."), Category("SharpGL")]
        public event PaintEventHandler OpenGLDraw;

        public event Action OnGLDraw;

        [Description("Called at the point in the render cycle when GDI drawing can occur."), Category("SharpGL")]
        public event PaintEventHandler GDIDraw;

        [Description("Called when the control is resized - you can use this to do custom viewport projections."), Category("SharpGL")]
        public event EventHandler Resized;

        /// <summary>
        /// When set to true, the draw time will be displayed in the render.
        /// </summary>
        protected bool drawFPS = false;

        /// <summary>
        /// The timer used for drawing the control.
        /// </summary>
        protected Timer timerDrawing = new Timer();

        /// <summary>
        /// The OpenGL object for the control.
        /// </summary>
        protected OpenGL gl = new OpenGL();

        /// <summary>
        /// A stopwatch used for timing rendering.
        /// </summary>
        protected Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// The render context type.
        /// </summary>
        protected RenderContextType renderContextType = RenderContextType.DIBSection;

        /// <summary>
        /// The bit depth.
        /// </summary>
        protected int bitDepth = 24;

        /// <summary>
        /// The last frame time in milliseconds.
        /// </summary>
        protected double frameTime = 0;

        /// <summary>
        /// Gets the OpenGL object.
        /// </summary>
        /// <value>The OpenGL.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OpenGL OpenGL
        {
            get { return gl; }
        }

        [Description("Should the draw time be shown?"), Category("SharpGL")]
        public bool DrawFPS
        {
            get { return drawFPS; }
            set { drawFPS = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int frameRate = 20;

        [Description("The rate at which the control should be re-drawn, in Hertz."), Category("SharpGL")]
        public int FrameRate
        {
            get { return frameRate; }
            set
            {
                frameRate = value;
                timerDrawing.Interval = 1000 / 20;
            }
        }

        [Description("The render context type."), Category("SharpGL")]
        public RenderContextType RenderContextType
        {
            get { return renderContextType; }
            set { renderContextType = value; }
        }

        [Description("The bit depth."), Category("SharpGL")]
        public int BitDepth
        {
            get { return bitDepth; }
            set { bitDepth = value; }
        }
    }
}
