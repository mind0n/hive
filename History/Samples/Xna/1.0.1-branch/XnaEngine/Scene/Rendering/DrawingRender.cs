using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XnaEngine.Scene.Rendering
{
    public class DrawingRender
    {
        public const int MAX_VERTEX_COUNT = 32768;
        public const int MAX_INDEX_COUNT = 65536;

        VertexBuffer _vertexDataBuffer;
        VertexBuffer _vertexTextureBuffer;
        IndexBuffer _indexBuffer;

        // a basic effect, which contains the shaders that we will use to draw our
        // primitives.
        BasicEffect basicEffect;

        public GraphicsDevice GraphicsDevice
        {
            get;
            private set;
        }

        public RenderTarget2D RenderTarget
        {
            get;
            private set;
        }

        public Texture2D Background
        {
            get;
            private set;
        }

        private SpriteBatch spriteBatch;

        #region Events
        public event EventHandler BeforeRender;
        public event EventHandler AfterRender;
        #endregion

        #region Constructor
        public DrawingRender(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
            this.Initialize();
        }

        public DrawingRender(RenderTarget2D renderTarget)
        {
            this.RenderTarget = renderTarget;
            this.GraphicsDevice = renderTarget.GraphicsDevice;
            this.Initialize();
        }

        public DrawingRender(RenderTarget2D renderTarget, Texture2D background)
            : this(renderTarget)
        {
            this.RenderTarget = renderTarget;
            this.GraphicsDevice = renderTarget.GraphicsDevice;
            this.Background = background;
            this.Initialize();
        }
        #endregion

        public void Initialize()
        {
            if (_vertexDataBuffer == null)
            {
                _vertexDataBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), MAX_VERTEX_COUNT, BufferUsage.WriteOnly);
            }
            if (_vertexTextureBuffer == null)
            {
                _vertexTextureBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionNormalTexture), MAX_VERTEX_COUNT, BufferUsage.WriteOnly);
            }
            if (_indexBuffer == null)
            {
                _indexBuffer = new IndexBuffer(GraphicsDevice, typeof(short), MAX_INDEX_COUNT, BufferUsage.WriteOnly);
            }

            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.VertexColorEnabled = true;
            // projection uses CreateOrthographicOffCenter to create 2d projection
            // matrix with 0,0 in the upper left.
			UpdateProjection();

            if (this.Background != null)
            {
                this.spriteBatch = new SpriteBatch(GraphicsDevice);
            }
        }

		public void UpdateProjection()
		{
			basicEffect.Projection = Matrix.CreateOrthographicOffCenter(0, GraphicsDevice.Viewport.Width,
				GraphicsDevice.Viewport.Height, 0,
				0, 1);
		}

        public void DrawIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices, short[] indexData, int indexOffset, int primitiveCount) where T : struct, IVertexType
        {
            if (this.BeforeRender != null)
            {
                this.BeforeRender(this, EventArgs.Empty);
            }

            var renderTagets = GraphicsDevice.GetRenderTargets();
            GraphicsDevice.SetRenderTarget(this.RenderTarget);

            if ((this.spriteBatch != null) && (this.Background != null))
            {
                this.spriteBatch.Begin();
                this.spriteBatch.Draw(this.Background, this.Background.Bounds, Color.White);
                this.spriteBatch.End();

                if (GraphicsDevice.Textures[0] == this.Background)
                {
                    GraphicsDevice.Textures[0] = null;
                }
            }

            _vertexDataBuffer.SetData<T>(vertexData);
            _indexBuffer.SetData<short>(indexData);
            // prepare the graphics device for drawing by setting the vertex declaration and the vertices            
            GraphicsDevice.SetVertexBuffer(_vertexDataBuffer);
            // prepare the graphics device for drawing by setting the indices
            GraphicsDevice.Indices = _indexBuffer;
            // tell our basic effect to begin.
            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawIndexedPrimitives(primitiveType, 0, 0, numVertices, 0, primitiveCount);
            }

            GraphicsDevice.SetVertexBuffer(null);
            GraphicsDevice.Indices = null;
            GraphicsDevice.SetRenderTargets(renderTagets);

            if (this.AfterRender != null)
            {
                this.AfterRender(this, EventArgs.Empty);
            }
        }

        internal void Begin(SpriteBatch spriteBatch, SpriteSortMode spriteSortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect, Matrix matrix, out RenderTargetBinding[] renderTagets)
        {
            if (this.BeforeRender != null)
            {
                this.BeforeRender(this, EventArgs.Empty);
            }

            renderTagets = GraphicsDevice.GetRenderTargets();
            GraphicsDevice.SetRenderTarget(this.RenderTarget);

            if ((this.spriteBatch != null) && (this.Background != null))
            {
                this.spriteBatch.Begin();
                this.spriteBatch.Draw(this.Background, this.Background.Bounds, Color.White);
                this.spriteBatch.End();

                if (GraphicsDevice.Textures[0] == this.Background)
                {
                    GraphicsDevice.Textures[0] = null;
                }
            }

            spriteBatch.Begin();
        }

        internal void End(SpriteBatch spriteBatch, RenderTargetBinding[] renderTagets)
        {
            spriteBatch.End();

            GraphicsDevice.SetRenderTargets(renderTagets);

            if (this.AfterRender != null)
            {
                this.AfterRender(this, EventArgs.Empty);
            }
        }
    }
}
