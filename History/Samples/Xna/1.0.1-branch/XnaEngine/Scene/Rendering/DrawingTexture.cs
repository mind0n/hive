using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XnaEngine.Scene.Rendering
{
    public class DrawingTexture : RenderTarget2D
    {
        #region Constructor
        // Summary:
        //     Creates an instance of this object.
        //
        // Parameters:
        //   graphicsDevice:
        //     The graphics device to associate with this render target resource.
        //
        //   width:
        //     Width, in pixels, of the render target. You can use graphicsDevice.PresentationParameters.BackBufferWidth
        //     to get the current screen width.
        //
        //   height:
        //     Height, in pixels, of the render target. You can use graphicsDevice.PresentationParameters.BackBufferHeight
        //     to get the current screen height.
        public DrawingTexture(GraphicsDevice graphicsDevice, int width, int height)
            : this(graphicsDevice, width, height, true)
        {
        }

        public DrawingTexture(GraphicsDevice graphicsDevice, int width, int height, bool keepDrawing)
            : base(graphicsDevice, width, height)
        {
            this.KeepDrawing = keepDrawing;
            this.Initialize();
        }


        //
        // Summary:
        //     Creates an instance of this object.
        //
        // Parameters:
        //   graphicsDevice:
        //     The graphics device to associate with this render target resource.
        //
        //   width:
        //     Width, in pixels, of the render target. You can use graphicsDevice.PresentationParameters.BackBufferWidth
        //     to get the current screen width.
        //
        //   height:
        //     Height, in pixels, of the render target. You can use graphicsDevice.PresentationParameters.BackBufferHeight
        //     to get the current screen height.
        //
        //   mipMap:
        //     [MarshalAsAttribute(U1)] True to enable a full mipmap chain to be generated,
        //     false otherwise.
        //
        //   preferredFormat:
        //     Preferred format for the surface data. This is the format preferred by the
        //     application, which may or may not be available from the hardware.
        //
        //   preferredDepthFormat:
        //     Preferred format for the depth buffer. This is the format preferred by the
        //     application, which may or may not be available from the hardware.
        public DrawingTexture(GraphicsDevice graphicsDevice, int width, int height, bool mipMap, SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat)
            : this(graphicsDevice, width, height, mipMap, preferredFormat, preferredDepthFormat, true)
        {
        }

        public DrawingTexture(GraphicsDevice graphicsDevice, int width, int height, bool mipMap, SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat, bool keepDrawing)
            : base(graphicsDevice, width, height, mipMap, preferredFormat, preferredDepthFormat)
        {
            this.KeepDrawing = keepDrawing;
            this.Initialize();
        }
        //
        // Summary:
        //     Creates an instance of this object.
        //
        // Parameters:
        //   graphicsDevice:
        //     The graphics device to associate with this render target resource.
        //
        //   width:
        //     Width, in pixels, of the render target. You can use graphicsDevice.PresentationParameters.BackBufferWidth
        //     to get the current screen width.
        //
        //   height:
        //     Height, in pixels, of the render target. You can use graphicsDevice.PresentationParameters.BackBufferHeight
        //     to get the current screen height.
        //
        //   mipMap:
        //     [MarshalAsAttribute(U1)] True to enable a full mipmap chain to be generated,
        //     false otherwise.
        //
        //   preferredFormat:
        //     Preferred format for the surface data. This is the format preferred by the
        //     application, which may or may not be available from the hardware.
        //
        //   preferredDepthFormat:
        //     Preferred format for the depth buffer. This is the format preferred by the
        //     application, which may or may not be available from the hardware.
        //
        //   preferredMultiSampleCount:
        //     The preferred number of multisample locations.
        //
        //   usage:
        //     Behavior options.
        public DrawingTexture(GraphicsDevice graphicsDevice, int width, int height, bool mipMap, SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat, int preferredMultiSampleCount, RenderTargetUsage usage)
            : this(graphicsDevice, width, height, mipMap, preferredFormat, preferredDepthFormat, preferredMultiSampleCount, usage, true)
        {
        }

        public DrawingTexture(GraphicsDevice graphicsDevice, int width, int height, bool mipMap, SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat, int preferredMultiSampleCount, RenderTargetUsage usage, bool keepDrawing)
            : base(graphicsDevice, width, height, mipMap, preferredFormat, preferredDepthFormat, preferredMultiSampleCount, usage)
        {
            this.KeepDrawing = keepDrawing;
            this.Initialize();
        }
        #endregion

        public DrawingContext DrawingContext
        {
            get;
            private set;
        }

        public DrawingRender Render
        {
            get
            {
                return this.DrawingContext.Render;
            }
        }

        private Texture2D background;
        public Texture2D Background
        {
            get
            {
                return this.background;
            }
        }

        protected virtual void Initialize()
        {
            if (this.KeepDrawing)
            {
                this.background = new Texture2D(GraphicsDevice, Width, Height);
                this.DrawingContext = new DrawingContext(this, this.background);
            }
            else
            {
                this.DrawingContext = new DrawingContext(this);
            }
            this.Render.BeforeRender += new EventHandler(Render_BeforeRender);
            this.Render.AfterRender += new EventHandler(Render_AfterRender);
        }

        #region KeepDrawing
        public Color[] data;        
        public bool KeepDrawing
        {
            get;
            set;
        }

        protected virtual void RestoreData()
        {
            if (KeepDrawing)
            {
                if (this.data != null)
                {
                    this.background.SetData<Color>(this.data);
                }
                else
                {
                    this.data = new Color[Width * Height];
                }
            }
        }
        protected virtual void BackupData()
        {
            if (this.KeepDrawing)
            {
                if (this.data != null)
                {
                    GetData<Color>(this.data);
                }
                else
                {
                    this.data = new Color[Width * Height];
                }
            }
        }
        #endregion

        public virtual void Clear(Color color)
        {
            if (this.data == null)
            {
                this.data = new Color[Width * Height];
            }
            for (int i = 0; i < this.data.Length; i++)
            {
                this.data[i] = color;
            }
            this.background.SetData<Color>(this.data);
        }

        public virtual void Begin(DrawingSortMode sortMode)
        {
            DrawingContext.Begin(sortMode);
        }

        public virtual void Begin()
        {
            DrawingContext.Begin();
        }

        public virtual void End()
        {
            DrawingContext.End();
        }

        void Render_BeforeRender(object sender, EventArgs e)
        {
            RestoreData();
        }

        void Render_AfterRender(object sender, EventArgs e)
        {
            BackupData();
        }
    }
}
