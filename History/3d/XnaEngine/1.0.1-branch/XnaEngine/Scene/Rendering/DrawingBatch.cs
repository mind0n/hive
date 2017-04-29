using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XnaEngine.Scene.Rendering
{
    public class DrawingBatch
    {
        private DrawingRender render;
        public DrawingRender Render
        {
            get
            {
                return this.render;
            }
        }

        public GraphicsDevice GraphicsDevice
        {
            get;
            private set;
        }

        public string Name { get; set; }
        public object Tag { get; set; }


        #region Constructor
        public DrawingBatch(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
            this.render = new DrawingRender(graphicsDevice);
        }

        public DrawingBatch(RenderTarget2D renderTarget)
        {
            GraphicsDevice = renderTarget.GraphicsDevice;
            this.render = new DrawingRender(renderTarget);
        }

        public DrawingBatch(RenderTarget2D renderTarget, Texture2D background)
        {
            GraphicsDevice = renderTarget.GraphicsDevice;
            this.render = new DrawingRender(renderTarget, background);
        }
        #endregion

        private const int MAX_ATOMIC_POLYGON_LENGTH = DrawingRender.MAX_VERTEX_COUNT;

        private float _Z = 0.0f;
        public DrawingSortMode SortMode
        {
            get;
            private set;
        }


        #region VertexBuffer for LineList
        private VertexPositionColor[] _lineVertexData = new VertexPositionColor[DrawingRender.MAX_VERTEX_COUNT];
        private int _lineVertexCount;

        private short[] _lineIndexData = new short[DrawingRender.MAX_INDEX_COUNT];
        private int _lineIndexCount;
        #endregion

        #region VertexBuffer for TriangleList
        private VertexPositionColor[] _triangleVertexData = new VertexPositionColor[DrawingRender.MAX_VERTEX_COUNT];
        private VertexPositionNormalTexture[] _triangleVertexTextures = new VertexPositionNormalTexture[DrawingRender.MAX_VERTEX_COUNT];
        private int _triangleVertexCount;

        private short[] _triangleIndexData = new short[DrawingRender.MAX_INDEX_COUNT];
        private int _triangleIndexCount;
        #endregion

        #region StartDrawing & FinishDrawing
        private unsafe void StartDrawing(VertexFragment vertexFragment, out VertexPositionColor* vertices, out short* indices, out short baseIndex)
        {
            VertexPositionNormalTexture* textures;
            StartDrawing(vertexFragment, out vertices, out textures, out indices, out baseIndex);
        }
        private unsafe void StartDrawing(VertexFragment vertexFragment, out VertexPositionColor* vertices, out VertexPositionNormalTexture* textures, out short* indices, out short baseIndex)
        {
            textures = null;
            if (vertexFragment.PrimitiveType == PrimitiveType.LineList)
            {
                if ((this.SortMode == DrawingSortMode.Order) && (this._triangleVertexCount > 0))
                {
                    Flush();
                    StartLineDrawing(vertexFragment, out vertices, out indices, out baseIndex);
                }
                else
                {
                    StartLineDrawing(vertexFragment, out vertices, out indices, out baseIndex);
                }
            }
            else if (vertexFragment.PrimitiveType == PrimitiveType.TriangleList)
            {
                if ((this.SortMode == DrawingSortMode.Order) && (this._lineVertexCount > 0))
                {
                    Flush();
                    StartTriangleDrawing(vertexFragment, out vertices, out textures, out indices, out baseIndex);
                }
                else
                {
                    StartTriangleDrawing(vertexFragment, out vertices, out textures, out indices, out baseIndex);
                }
            }
            else
            {
                throw new NotSupportedException(string.Format("PrimitiveType: {0} is not supported.", vertexFragment.PrimitiveType));
            }
        }

        private unsafe void StartLineDrawing(VertexFragment vertexFragment, out VertexPositionColor* vertices, out short* indices, out short baseIndex)
        {
            int vertexCount = vertexFragment.VertexCount;
            int indexCount = vertexFragment.IndexCount;
            StartLineDrawing(vertexCount, indexCount, out vertices, out indices, out baseIndex);
        }
        unsafe private void StartLineDrawing(int vertexCount, int indexCount, out VertexPositionColor* vertices, out short* indices, out short baseIndex)
        {
            if ((vertexCount + _lineVertexCount < _lineVertexData.Length) && (indexCount + _lineIndexCount < _lineIndexData.Length))
            {
                fixed (VertexPositionColor* pVertexData = _lineVertexData)
                {
                    vertices = pVertexData + _lineVertexCount;
                }
                fixed (short* pIndexData = _lineIndexData)
                {
                    indices = pIndexData + _lineIndexCount;
                }
                baseIndex = (short)_lineVertexCount;

                //update internal counters
                _lineVertexCount += vertexCount;
                _lineIndexCount += indexCount;
            }
            else
            {
                if (vertexCount > _lineVertexData.Length)
                {
                    throw new ArgumentOutOfRangeException(string.Format("Vertex count {0} is greater than maximum allowed {1}", vertexCount, _lineVertexData.Length));
                }
                if (indexCount > _lineIndexData.Length)
                {
                    throw new ArgumentOutOfRangeException(string.Format("Index count {0} is greater than maximum allowed {1}", indexCount, _lineIndexData.Length));
                }

                Flush();
                StartLineDrawing(vertexCount, indexCount, out vertices, out indices, out baseIndex);
            }
        }

        private unsafe void StartTriangleDrawing(VertexFragment vertexFragment, out VertexPositionColor* vertices, out VertexPositionNormalTexture* textures, out short* indices, out short baseIndex)
        {
            int vertexCount = vertexFragment.VertexCount;
            int indexCount = vertexFragment.IndexCount;
            StartTriangleDrawing(vertexCount, indexCount, out vertices, out textures, out indices, out baseIndex);
        }
        unsafe private void StartTriangleDrawing(int vertexCount, int indexCount, out VertexPositionColor* vertices, out VertexPositionNormalTexture* textures, out short* indices, out short baseIndex)
        {
            if ((vertexCount + _triangleVertexCount < _triangleVertexData.Length) && (indexCount + _triangleIndexCount < _triangleIndexData.Length))
            {
                fixed (VertexPositionColor* pVertexData = _triangleVertexData)
                {
                    vertices = pVertexData + _triangleVertexCount;
                }
                fixed (VertexPositionNormalTexture* pVertexTextures = _triangleVertexTextures)
                {
                    textures = pVertexTextures + _triangleVertexCount;
                }
                fixed (short* pIndexData = _triangleIndexData)
                {
                    indices = pIndexData + _triangleIndexCount;
                }
                baseIndex = (short)_triangleVertexCount;

                //update internal counters
                _triangleVertexCount += vertexCount;
                _triangleIndexCount += indexCount;
            }
            else
            {
                if (vertexCount > _triangleVertexData.Length)
                {
                    throw new ArgumentOutOfRangeException(string.Format("Vertex count {0} is greater than maximum allowed {1}", vertexCount, _triangleVertexData.Length));
                }
                if (indexCount > _triangleIndexData.Length)
                {
                    throw new ArgumentOutOfRangeException(string.Format("Index count {0} is greater than maximum allowed {1}", indexCount, _triangleIndexData.Length));
                }

                Flush();
                StartTriangleDrawing(vertexCount, indexCount, out vertices, out textures, out indices, out baseIndex);
            }
        }

        private void FinishDrawing(VertexFragment vertexFragment)
        {
        }
        #endregion

        #region Draw
        #region DrawLine
        /// <summary>
        /// Draws the simple 1 px line.
        /// </summary>
        /// <param name="startX">The start X coord of line.</param>
        /// <param name="startY">The start Y coord of line.</param>
        /// <param name="endX">The end X coord of line.</param>
        /// <param name="endY">The end Y coord of line.</param>
        /// <param name="color">The color tint.</param>
        public virtual void DrawLine(float startX, float startY, float endX, float endY, Color color)
        {
            DrawLine(new Vector2(startX, startY), new Vector2(endX, endY), color);
        }

        /// <summary>
        /// Draws the simple 1px line.
        /// </summary>
        /// <param name="start">The start point.</param>
        /// <param name="end">The end point.</param>
        /// <param name="color">The color tint.</param>
        public virtual void DrawLine(Vector2 start, Vector2 end, Color color)
        {
            ProcessDrawLine(start, end, color);
        }

		public virtual void Draw2DLines(Color color, bool isCycling, params Vector3[] points)
		{
			if (points != null)
			{
				if (!isCycling && points.Length % 2 == 0)
				{
					for (int i = 0; i < points.Length - 1; i += 2)
					{
						Vector2 start = new Vector2(points[i].X, points[i].Y);
						Vector2 end = new Vector2(points[i + 1].X, points[i + 1].Y);
						ProcessDrawLine(start, end, color);
					}
				}
				else
				{
					for (int i = 0; i < points.Length - 1; i++)
					{
						Vector2 start = new Vector2(points[i].X, points[i].Y);
						Vector2 end = new Vector2(points[i + 1].X, points[i + 1].Y);
						ProcessDrawLine(start, end, color);
					}
				}
			}
		}

        /// <summary>
        /// Draws the simple 1px line.
        /// </summary>
        /// <param name="start">The start point.</param>
        /// <param name="end">The end point.</param>
        /// <param name="color">The color tint.</param>
        protected virtual void ProcessDrawLine(Vector2 start, Vector2 end, Color color)
        {
            unsafe
            {
                VertexPositionColor* vertices;
                short* indices;
                short baseIndex;
                StartDrawing(VertexFragment.Line, out vertices, out indices, out baseIndex);
                //---------------------------------------------------
                vertices[0].Position = new Vector3(
                    _transform.M11 * start.X + _transform.M12 * start.Y + _transform.TX - 0.5f,
                    _transform.M21 * start.X + _transform.M22 * start.Y + _transform.TY,
                    _Z
                );
                vertices[0].Color = color;
                //---------------------------------------------------
                vertices[1].Position = new Vector3(
                    _transform.M11 * end.X + _transform.M12 * end.Y + _transform.TX - 0.5f,
                    _transform.M21 * end.X + _transform.M22 * end.Y + _transform.TY,
                    _Z
                );
                vertices[1].Color = color;
                //---------------------------------------------------

                //populate indices
                indices[0] = baseIndex;
                indices[1] = (short)(baseIndex + 1);
            }
            FinishDrawing(VertexFragment.Line);
        }
        #endregion

        #region DrawRectangle
        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="rect">The rectangle.</param>
        /// <param name="color">The color tint.</param>
        public virtual void DrawRectangle(Rectangle rectangle, Color color)
        {
            DrawRectangle(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height, color);
        }

        public virtual void DrawRectangle(float x, float y, float width, float height, Color color)
        {
            DrawRectangle(new Vector2(x, y), new Vector2(width, height), color);
        }

        public virtual void DrawRectangle(Vector2 location, Vector2 size, Color color)
        {
            ProcessDrawRectangle(location, size, color);
        }

		

        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="x">The x of left top rect corner.</param>
        /// <param name="y">The y of left top rect cornder.</param>
        /// <param name="width">The width of rect.</param>
        /// <param name="height">The height of rect.</param>
        /// <param name="color">The color tint.</param>
        protected virtual void ProcessDrawRectangle(Vector2 location, Vector2 size, Color color)
        {
            float x = location.X;
            float y = location.Y;
            float width = size.X;
            float height = size.Y;
            float right = x + width - 0.5f;
            float bottom = y + height - 0.5f;
            unsafe
            {
                VertexPositionColor* vertices;
                short* indices;
                short baseIndex;
                StartDrawing(VertexFragment.Rectangle, out vertices, out indices, out baseIndex);
                //---------------------------------------------------
                vertices[0].Position = new Vector3(
                    _transform.M11 * x + _transform.M12 * y + _transform.TX - 0.5f,
                    _transform.M21 * x + _transform.M22 * y + _transform.TY - 0.5f,
                    _Z
                );
                vertices[0].Color = color;
                //---------------------------------------------------
                vertices[1].Position = new Vector3(
                    _transform.M11 * right + _transform.M12 * y + _transform.TX,
                    _transform.M21 * right + _transform.M22 * y + _transform.TY,
                    _Z
                );
                vertices[1].Color = color;
                //---------------------------------------------------
                vertices[2].Position = new Vector3(
                    _transform.M11 * right + _transform.M12 * bottom + _transform.TX,
                    _transform.M21 * right + _transform.M22 * bottom + _transform.TY,
                    _Z
                );
                vertices[2].Color = color;
                //---------------------------------------------------
                vertices[3].Position = new Vector3(
                    _transform.M11 * x + _transform.M12 * bottom + _transform.TX,
                    _transform.M21 * x + _transform.M22 * bottom + _transform.TY,
                    _Z
                );
                vertices[3].Color = color;
                //---------------------------------------------------

                //populate indices
                indices[0] = baseIndex;
                indices[1] = (short)(baseIndex + 1);
                indices[2] = (short)(baseIndex + 1);
                indices[3] = (short)(baseIndex + 2);
                indices[4] = (short)(baseIndex + 2);
                indices[5] = (short)(baseIndex + 3);
                indices[6] = (short)(baseIndex + 3);
                indices[7] = (short)(baseIndex + 0);
            }
            FinishDrawing(VertexFragment.Rectangle);
        }
        #endregion

        #region DrawTriangle
        /// <summary>
        /// Draws the triangle from specified vertices.
        /// </summary>
        /// <param name="x1">The vertex1 X coord.</param>
        /// <param name="y1">The vertex1 Y coord.</param>
        /// <param name="x2">The vertex2 X coord.</param>
        /// <param name="y2">The vertex2 Y coord.</param>
        /// <param name="x3">The vertex3 X coord.</param>
        /// <param name="y3">The vertex3 Y coord.</param>
        /// <param name="color">The color tint.</param>
        public virtual void DrawTriangle(float x1, float y1, float x2, float y2, float x3, float y3, Color color)
        {
            DrawTriangle(new Vector2(x1, y1), new Vector2(x2, y2), new Vector2(x3, y3), color);
        }
        public virtual void DrawTriangle(Vector2 vertex1, Vector2 vertex2, Vector2 vertex3, Color color)
        {
            ProcessDrawTriangle(vertex1, vertex2, vertex3, color);
        }
        /// <summary>
        /// Draws the triangle from specified vertices.
        /// </summary>
        /// <param name="vertex1">The vertex1 reference.</param>
        /// <param name="vertex2">The vertex2 reference.</param>
        /// <param name="vertex3">The vertex3 reference.</param>
        /// <param name="color">The color tint.</param>
        protected virtual void ProcessDrawTriangle(Vector2 vertex1, Vector2 vertex2, Vector2 vertex3, Color color)
        {
            unsafe
            {
                VertexPositionColor* vertices;
                short* indices;
                short baseIndex;
                StartDrawing(VertexFragment.Triangle, out vertices, out indices, out baseIndex);
                //---------------------------------------------------
                vertices[0].Position = new Vector3(
                _transform.M11 * vertex1.X + _transform.M12 * vertex1.Y + _transform.TX - 0.5f,
                _transform.M21 * vertex1.X + _transform.M22 * vertex1.Y + _transform.TY - 0.5f,
                _Z
                );
                vertices[0].Color = color;
                //---------------------------------------------------
                vertices[1].Position = new Vector3(
                _transform.M11 * vertex2.X + _transform.M12 * vertex2.Y + _transform.TX - 0.5f,
                _transform.M21 * vertex2.X + _transform.M22 * vertex2.Y + _transform.TY - 0.5f,
                _Z
                );
                vertices[1].Color = color;
                //---------------------------------------------------

                vertices[2].Position = new Vector3(
                _transform.M11 * vertex3.X + _transform.M12 * vertex3.Y + _transform.TX - 0.5f,
                _transform.M21 * vertex3.X + _transform.M22 * vertex3.Y + _transform.TY - 0.5f,
                _Z
                );
                vertices[2].Color = color;
                //---------------------------------------------------

                //add indices
                indices[0] = baseIndex;
                indices[1] = (short)(baseIndex + 1);
                indices[2] = (short)(baseIndex + 1);
                indices[3] = (short)(baseIndex + 2);
                indices[4] = (short)(baseIndex + 2);
                indices[5] = (short)(baseIndex + 0);
            }
            FinishDrawing(VertexFragment.Triangle);
        }
        #endregion

        #region DrawEllipse
        /// <summary>
        /// Draws the ellipse.
        /// </summary>
        /// <param name="region">The region to draw ellipse.</param>
        /// <param name="color">The color tint.</param>
        public virtual void DrawEllipse(Rectangle region, Color color)
        {
            DrawEllipse(region.Left, region.Top, region.Width, region.Height, color);
        }

        /// <summary>
        /// Draws the ellipse.
        /// </summary>
        /// <param name="x">The x of left top point of rectangular region.</param>
        /// <param name="y">The y of left top point of rectangular region..</param>
        /// <param name="width">The width of rectangular region.</param>
        /// <param name="height">The height of rectangular region.</param>
        /// <param name="color">The color tint.</param>
        public virtual void DrawEllipse(float x, float y, float width, float height, Color color)
        {
            DrawEllipse(new Vector2(x, y), new Vector2(width, height), color);
        }

        public virtual void DrawEllipse(Vector2 location, Vector2 size, Color color)
        {
            ProcessDrawEllipse(location, size, color);
        }

        protected virtual void ProcessDrawEllipse(Vector2 location, Vector2 size, Color color)
        {
            unsafe
            {
                VertexPositionColor* vertices;
                short* indices;
                short baseIndex;
                StartDrawing(VertexFragment.Ellipse, out vertices, out indices, out baseIndex);
                short arcBaseIndex = baseIndex;
                Vector2[] offsets = VertexFragment.EllipseSegments;
                int lastOffset = offsets.Length - 1;

                size -= new Vector2(1f, 1f);
                for (int n = 0; n < offsets.Length; ++n)
                {
                    vertices->Position = new Vector3(offsets[n] * size + location, _Z);
                    vertices->Color = color;
                    vertices++;

                    indices[0] = (short)(arcBaseIndex + n);
                    indices[1] = (short)(arcBaseIndex + ((n == lastOffset) ? 0 : (n + 1)));
                    indices += 2;
                }

                //transform everything
                TransformVertices(ref _transform, offsets.Length, vertices);
            }
            FinishDrawing(VertexFragment.Ellipse);
        }
        #endregion

        #region DrawPolyline
        /// <summary>
        /// Draws the polyline.
        /// </summary>
        /// <param name="points">The array with points of polyline.</param>
        /// <param name="color">The color tint.</param>
        public virtual void DrawPolyline(Vector2[] points, Color color)
        {
            DrawPolyline(points, 0, -1, false, color);
        }

        /// <summary>
        /// Draws the polyline.
        /// </summary>
        /// <param name="points">The array with points of polyline.</param>
        /// <param name="closed">if set to <c>true</c> to connect last point with the first one.</param>
        /// <param name="color">The color tint.</param>
        public virtual void DrawPolyline(Vector2[] points, bool closed, Color color)
        {
            DrawPolyline(points, 0, -1, closed, color);
        }

        public virtual void DrawPolyline(Vector2[] points, int start, int count, bool closed, Color color)
        {
            ProcessDrawPolyline(points, start, count, closed, color);
        }

        /// <summary>
        /// Draws the polyline.
        /// </summary>
        /// <param name="points">The array with points of polyline.</param>
        /// <param name="start">The start element in array to use.</param>
        /// <param name="count">The element count to use.</param>
        /// <param name="closed">if set to <c>true</c> to connect last point with the first one.</param>
        /// <param name="color">The color tint.</param>
        protected virtual void ProcessDrawPolyline(Vector2[] points, int start, int count, bool closed, Color color)
        {
            count = CheckPoints(points, start, count);
            if (count > 1)
            {
                if (count <= MAX_ATOMIC_POLYGON_LENGTH)
                {
                    int vertexCount = count, lineCount = count - (closed ? 0 : 1);
                    VertexFragment vertexFragment = new VertexFragment(vertexCount, lineCount * 2, PrimitiveType.LineList);
                    unsafe
                    {
                        VertexPositionColor* vertices;
                        short* indices;
                        short baseIndex;
                        StartDrawing(vertexFragment, out vertices, out indices, out baseIndex);
                        short index = baseIndex;
                        //populate core line vertices
                        for (int n = 0; n < vertexCount; ++n)
                        {
                            //---------------------------------------------------
                            Vector2 vector = points[n + start];
                            vertices->Position = new Vector3(
                            _transform.M11 * vector.X + _transform.M12 * vector.Y + _transform.TX - 0.5f, //fix line alignment issue
                            _transform.M21 * vector.X + _transform.M22 * vector.Y + _transform.TY - 0.5f, //fix line alignment issue
                            _Z
                            );
                            vertices->Color = color;
                            //---------------------------------------------------
                            if (n < vertexCount - 1)
                            {
                                indices[0] = (short)index;
                                indices[1] = (short)(index + 1);
                                indices += 2;
                                index++;
                            }
                            else
                            {
                                //if need to connect last and first - lets do it
                                if (closed)
                                {
                                    indices[0] = index;
                                    indices[1] = baseIndex;
                                }
                            }
                            vertices++;
                        }
                    }
                    FinishDrawing(vertexFragment);
                }
                else
                {
                    int step = MAX_ATOMIC_POLYGON_LENGTH - 1;
                    //split polygon/polyline into less parts
                    for (int n = 0; n < count; n += step)
                    {
                        ProcessDrawPolyline(points, start + n - (n != 0 ? 1 : 0), Math.Min(step, count - n) + (n != 0 ? 1 : 0), false, color);
                    }
                    if (closed)
                    {
                        ProcessDrawPolyline(new Vector2[] { points[start + count - 1], points[start] }, 0, 2, false, color);
                    }
                }
            }
            else
            {
                throw new ArgumentException("Drawing polyline/polygon required at least 2 points");
            }
        }
        #endregion


        #region DrawFilledRectangle
        /// <summary>
        /// Draws the solid simple rectangle.
        /// </summary>
        /// <param name="rect">The rectangle.</param>
        /// <param name="color">The color tint.</param>
        public virtual void DrawFilledRectangle(Rectangle rectangle, Color color)
        {
            DrawFilledRectangle(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height, color);
        }

        public virtual void DrawFilledRectangle(float x, float y, float width, float height, Color color)
        {
            DrawFilledRectangle(new Vector2(x, y), new Vector2(width, height), color);
        }
        public virtual void DrawFilledRectangle(Vector2 location, Vector2 size, Color color)
        {
            ProcessDrawFilledRectangle(location, size, color);

        }
        /// <summary>
        /// Draws the solid simple rectangle.
        /// </summary>
        /// <param name="x">The x of left top rect corner.</param>
        /// <param name="y">The y of left top rect cornder.</param>
        /// <param name="width">The width of rect.</param>
        /// <param name="height">The height of rect.</param>
        /// <param name="color">The color tint.</param>
        protected virtual void ProcessDrawFilledRectangle(Vector2 location, Vector2 size, Color color)
        {
            float x = location.X;
            float y = location.Y;
            float width = size.X;
            float height = size.Y;
            unsafe
            {
                VertexPositionColor* vertices;
                VertexPositionNormalTexture* textures;
                short* indices;
                short baseIndex;
                StartDrawing(VertexFragment.FilledRectangle, out vertices, out textures, out indices, out baseIndex);
                //---------------------------------------------------
                vertices[0].Position = new Vector3(x, y, _Z);
                vertices[0].Color = color;
                //---------------------------------------------------
                vertices[1].Position = new Vector3(x + width, y, _Z);
                vertices[1].Color = color;
                //---------------------------------------------------
                vertices[2].Position = new Vector3(x + width, y + height, _Z);
                vertices[2].Color = color;
                //---------------------------------------------------
                vertices[3].Position = new Vector3(x, y + height, _Z);
                vertices[3].Color = color;
                //---------------------------------------------------

                //apply transform
                Transform4Vertices(ref _transform, vertices);

                //populate indices
                indices[0] = baseIndex;
                indices[1] = (short)(baseIndex + 1);
                indices[2] = (short)(baseIndex + 2);
                indices[3] = baseIndex;
                indices[4] = (short)(baseIndex + 2);
                indices[5] = (short)(baseIndex + 3);
            }
            FinishDrawing(VertexFragment.FilledRectangle);
        }
        #endregion

        #region DrawFilledTriangle
        /// <summary>
        /// Draws the triangle from specified vertices.
        /// </summary>
        /// <param name="x1">The vertex1 X coord.</param>
        /// <param name="y1">The vertex1 Y coord.</param>
        /// <param name="x2">The vertex2 X coord.</param>
        /// <param name="y2">The vertex2 Y coord.</param>
        /// <param name="x3">The vertex3 X coord.</param>
        /// <param name="y3">The vertex3 Y coord.</param>
        /// <param name="color">The color tint.</param>
        public virtual void DrawFilledTriangle(float x1, float y1, float x2, float y2, float x3, float y3, Color color)
        {
            DrawFilledTriangle(new Vector2(x1, y1), new Vector2(x2, y2), new Vector2(x3, y3), color);
        }
        public virtual void DrawFilledTriangle(Vector2 vertex1, Vector2 vertex2, Vector2 vertex3, Color color)
        {
            ProcessDrawFilledTriangle(vertex1, vertex2, vertex3, color);
        }
        /// <summary>
        /// Draws the triangle from specified vertices.
        /// </summary>
        /// <param name="vertex1">The vertex1 reference.</param>
        /// <param name="vertex2">The vertex2 reference.</param>
        /// <param name="vertex3">The vertex3 reference.</param>
        /// <param name="color">The color tint.</param>
        protected virtual void ProcessDrawFilledTriangle(Vector2 vertex1, Vector2 vertex2, Vector2 vertex3, Color color)
        {
            unsafe
            {
                VertexPositionColor* vertices;
                VertexPositionNormalTexture* textures;
                short* indices;
                short baseIndex;
                StartDrawing(VertexFragment.FilledTriangle, out vertices, out textures, out indices, out baseIndex);
                //---------------------------------------------------
                vertices[0].Position = new Vector3(
                _transform.M11 * vertex1.X + _transform.M12 * vertex1.Y + _transform.TX,
                _transform.M21 * vertex1.X + _transform.M22 * vertex1.Y + _transform.TY,
                _Z
                );
                vertices[0].Color = color;
                //---------------------------------------------------
                vertices[1].Position = new Vector3(
                _transform.M11 * vertex2.X + _transform.M12 * vertex2.Y + _transform.TX,
                _transform.M21 * vertex2.X + _transform.M22 * vertex2.Y + _transform.TY,
                _Z
                );
                vertices[1].Color = color;
                //---------------------------------------------------
                vertices[2].Position = new Vector3(
                _transform.M11 * vertex3.X + _transform.M12 * vertex3.Y + _transform.TX,
                _transform.M21 * vertex3.X + _transform.M22 * vertex3.Y + _transform.TY,
                _Z
                );
                vertices[2].Color = color;
                //---------------------------------------------------

                //populate indices
                indices[0] = baseIndex;
                if (CheckClockwise(vertex1, vertex2, vertex3))
                {
                    indices[1] = (short)(baseIndex + 1);
                    indices[2] = (short)(baseIndex + 2);
                }
                else
                {
                    indices[1] = (short)(baseIndex + 2);
                    indices[2] = (short)(baseIndex + 1);
                }
            }
            FinishDrawing(VertexFragment.FilledTriangle);
        }
        #endregion

        #region DrawFilledEllipse
        /// <summary>
        /// Draws the ellipse.
        /// </summary>
        /// <param name="region">The region to draw ellipse.</param>
        /// <param name="color">The color tint.</param>
        public virtual void DrawFilledEllipse(Rectangle region, Color color)
        {
            DrawFilledEllipse(region.Left, region.Top, region.Width, region.Height, color);
        }

        public virtual void DrawFilledEllipse(float x, float y, float width, float height, Color color)
        {
            DrawFilledEllipse(new Vector2(x, y), new Vector2(width, height), color);
        }
        public virtual void DrawFilledEllipse(Vector2 location, Vector2 size, Color color)
        {
            ProcessDrawFilledEllipse(location, size, color);
        }
        /// <summary>
        /// Draws the ellipse.
        /// </summary>
        /// <param name="x">The x of left top point of rectangular region.</param>
        /// <param name="y">The y of left top point of rectangular region..</param>
        /// <param name="width">The width of rectangular region.</param>
        /// <param name="height">The height of rectangular region.</param>
        /// <param name="color">The color tint.</param>
        protected virtual void ProcessDrawFilledEllipse(Vector2 location, Vector2 size, Color color)
        {
            unsafe
            {
                VertexPositionColor* vertices;
                VertexPositionNormalTexture* textures;
                short* indices;
                short baseIndex;
                StartDrawing(VertexFragment.FilledEllipse, out vertices, out textures, out indices, out baseIndex);
                short arcBaseIndex = (short)(baseIndex + 1);
                Vector2[] offsets = VertexFragment.EllipseSegments;
                location -= new Vector2(1f, 1f);
                int lastOffset = offsets.Length - 1;

                Rectangle coordRegion = Extensions.RectangleUnit;
                Vector2 coordsOffset = coordRegion.LeftTop();
                Vector2 coordsSize = coordRegion.Size();

                //---------------------------------------------------
                //setup central point
                vertices[0].Position = new Vector3(location.X + size.X * 0.5f, location.Y + size.Y * 0.5f, _Z);
                vertices[0].Color = color;
                vertices++;
                //---------------------------------------------------
                for (int n = 0; n < offsets.Length; ++n)
                {
                    vertices[n].Position = new Vector3(offsets[n] * size + location, _Z);
                    vertices[n].Color = color;

                    indices[0] = (short)(arcBaseIndex + n);
                    indices[1] = (short)(arcBaseIndex + ((n == lastOffset) ? 0 : (n + 1)));
                    indices[2] = baseIndex;
                    indices += 3;
                }

                //transform everything
                TransformVertices(ref _transform, (int)VertexFragment.FilledEllipse.VertexCount, vertices, new Vector2(0.5f, 0.5f));
            }
            FinishDrawing(VertexFragment.FilledEllipse);
        }
        #endregion
        #endregion

        #region Helper Methods
        /// <summary>
        /// Calculates the primitive count based on index count primitive type.
        /// </summary>
        /// <param name="primitiveType">Type of the primitive.</param>
        /// <param name="indexCount">The index count.</param>
        /// <returns></returns>
        public static int CalculatePrimitiveCount(PrimitiveType primitiveType, int indexCount)
        {
            switch (primitiveType)
            {
                case PrimitiveType.LineList:
                    return indexCount / 2;
                case PrimitiveType.TriangleList:
                    return indexCount / 3;
                case PrimitiveType.TriangleStrip:
                    return indexCount - 1;
                default:
                    return 0;
            }
        }

        public static bool CheckClockwise(Vector2 v1, Vector2 v2, Vector2 v3)
        {
            return ((v2.X - v1.X) * (v3.Y - v1.Y) - (v2.Y - v1.Y) * (v3.X - v1.X)) > 0;
        }

        protected virtual int CheckPoints(Vector2[] points, int start, int count)
        {
            if (points == null) throw new ArgumentNullException("points");

            if (start < 0 || start >= points.Length)
            {
                throw new ArgumentException("start", "Start element is out of array index");
            }

            if (count == -1)
            {
                count = points.Length - start;
            }
            else
            {
                if (start + count > points.Length)
                {
                    throw new ArgumentException("count", "End element is out of array index");
                }
            }
            return count;
        }

        /// <summary>
        /// Transforms 2 vertices at one call
        /// </summary>
        /// <param name="t">The _transform.</param>
        /// <param name="v">The vertices.</param>
        private static unsafe void Transform2Vertices(ref Transform2D t, VertexPositionColor* v)
        {
            v[0].Position = new Vector3(
                t.M11 * v[0].Position.X + t.M12 * v[0].Position.Y + t.TX,
                t.M21 * v[0].Position.X + t.M22 * v[0].Position.Y + t.TY,
                0
            );

            v[1].Position = new Vector3(
                t.M11 * v[1].Position.X + t.M12 * v[1].Position.Y + t.TX,
                t.M21 * v[1].Position.X + t.M22 * v[1].Position.Y + t.TY,
                0
            );
        }

        /// <summary>
        /// Transforms 4 vertices at one call
        /// </summary>
        /// <param name="t">The _transform.</param>
        /// <param name="v">The vertices.</param>
        private static unsafe void Transform4Vertices(ref Transform2D t, VertexPositionColor* v)
        {
            //+3% of performance
            v[0].Position = new Vector3(
                t.M11 * v[0].Position.X + t.M12 * v[0].Position.Y + t.TX,
                t.M21 * v[0].Position.X + t.M22 * v[0].Position.Y + t.TY,
                0
            );

            v[1].Position = new Vector3(
                t.M11 * v[1].Position.X + t.M12 * v[1].Position.Y + t.TX,
                t.M21 * v[1].Position.X + t.M22 * v[1].Position.Y + t.TY,
                0
            );

            v[2].Position = new Vector3(
                t.M11 * v[2].Position.X + t.M12 * v[2].Position.Y + t.TX,
                t.M21 * v[2].Position.X + t.M22 * v[2].Position.Y + t.TY,
                0
            );

            v[3].Position = new Vector3(
                t.M11 * v[3].Position.X + t.M12 * v[3].Position.Y + t.TX,
                t.M21 * v[3].Position.X + t.M22 * v[3].Position.Y + t.TY,
                0
            );
        }

        /// <summary>
        /// Transforms any number of vertices at one call
        /// </summary>
        /// <param name="t">The _transform.</param>
        /// <param name="count">The count of vertices to transform.</param>
        /// <param name="v">The vertices.</param>
        private static unsafe void TransformVertices(ref Transform2D t, int count, VertexPositionColor* v)
        {
            for (int n = 0; n < count; ++n)
            {
                v[0].Position = new Vector3(
                    t.M11 * v->Position.X + t.M12 * v->Position.Y + t.TX,
                    t.M21 * v->Position.X + t.M22 * v->Position.Y + t.TY,
                    0
                );
                v++;
            }
        }

        /// <summary>
        /// Transforms any number of vertices at one call
        /// </summary>
        /// <param name="t">The _transform.</param>
        /// <param name="count">The count of vertices to transform.</param>
        /// <param name="v">The vertices.</param>
        /// <param name="offset">Additional offset to apply after transformation.</param>
        private static unsafe void TransformVertices(ref Transform2D t, int count, VertexPositionColor* v, Vector2 offset)
        {
            for (int n = 0; n < count; ++n)
            {
                v[0].Position = new Vector3(
                    t.M11 * v->Position.X + t.M12 * v->Position.Y + t.TX + offset.X,
                    t.M21 * v->Position.X + t.M22 * v->Position.Y + t.TY + offset.Y,
                    0
                );
                v++;
            }
        }
        #endregion

        #region Transform
        ///<summary>Maximal number of pushed transforms for canvas</summary>
        private const int TRANSFORM_STACK_MAX_SIZE = 64;

        ///<summary>Current canvas transformation</summary>
        private Transform2D _transform = Transform2D.Empty;
        ///<summary>Canvas transformation stack</summary>
        private Transform2D[] _transformStack = new Transform2D[TRANSFORM_STACK_MAX_SIZE];
        ///<summary>Size of transformation stack</summary>
        private int _transformStackSize = 0;

        #region Public Transform Interface

        /// <summary>
        /// Gets or sets canvas transformation. This transformation applies for everything
        /// </summary>
        /// <value>The transform.</value>
        public Transform2D Transform
        {
            get { return _transform; }
            set { _transform = value; }
        }

        /// <summary>
        /// Resets the transform to initial state (stack is untouched).
        /// </summary>
        public void ResetTransform()
        {
            _transform = Transform2D.Empty;
        }

        /// <summary>
        /// Sets (overrides) the specified transform for canvas.
        /// </summary>
        /// <param name="transform">The transform for override.</param>
        public void SetTransform(ref Transform2D transform)
        {
            _transform = transform;
        }

        /// <summary>
        /// Applies the specified transform after current canvas transform.
        /// </summary>
        /// <param name="transform">The transform to apply.</param>
        public void ApplyTransform(ref Transform2D transform)
        {
            Transform2D current = _transform;
            Transform2D.Multiply(ref current, ref transform, out _transform);
        }

        /// <summary>
        /// Applies the specified transform before current canvas transform.
        /// </summary>
        /// <param name="transform">The transform to apply.</param>
        public void ApplyTransformBefore(ref Transform2D transform)
        {
            Transform2D current = _transform;
            Transform2D.Multiply(ref transform, ref current, out _transform);
        }

        /// <summary>
        /// Sets (overrides) the specified transform for canvas.
        /// </summary>
        /// <param name="transform">The transform for override.</param>
        public void SetTransform(ref ITransform transform)
        {
            transform.GetTransform(out _transform);
        }

        /// <summary>
        /// Applies the specified transform after current canvas transform.
        /// </summary>
        /// <param name="transform">The transform to apply.</param>
        public void ApplyTransform(ref ITransform transform)
        {
            Transform2D current = _transform, temp;
            transform.GetTransform(out temp);
            Transform2D.Multiply(ref current, ref temp, out _transform);
        }

        /// <summary>
        /// Applies the specified transform before current canvas transform.
        /// </summary>
        /// <param name="transform">The transform to apply.</param>
        public void ApplyTransformBefore(ref ITransform transform)
        {
            Transform2D current = _transform, temp;
            transform.GetTransform(out temp);
            Transform2D.Multiply(ref temp, ref current, out _transform);
        }

        /// <summary>
        /// Pushes the current transform onto transformation stack.
        /// </summary>
        public void PushTransform()
        {
            if (_transformStackSize >= TRANSFORM_STACK_MAX_SIZE)
            {
                throw new Exception("Transformation stack overflow");
            }
            _transformStack[_transformStackSize++] = _transform;
        }

        /// <summary>
        /// Pops the transform. Retrieves it from stack
        /// </summary>
        public void PopTransform()
        {
            if (_transformStackSize > 0)
            {
                _transformStackSize -= 1;
                _transform = _transformStack[_transformStackSize];
            }
            else
            {
                throw new Exception("Can't pop. Transformation stack is empty");
            }
        }

        /// <summary>
        /// Translates the canvas on specified offset.
        /// </summary>
        /// <param name="offsetX">The offset by X.</param>
        /// <param name="offsetY">The offset by Y.</param>
        public void Translate(float offsetX, float offsetY)
        {
            Translate(new Vector2(offsetX, offsetY));
        }

        /// <summary>
        /// Translates the canvas on specified offset.
        /// </summary>
        /// <param name="offset">The offset vector.</param>
        public void Translate(Vector2 offset)
        {
            Transform2D temp;
            Transform2D.CreateTranslation(ref offset, out temp);
            ApplyTransform(ref temp);
        }

        /// <summary>
        /// Rotates the canvas on specified angle in radians.
        /// </summary>
        /// <param name="angleRad">The angle in radians.</param>
        public void Rotate(float angleRad)
        {
            Transform2D temp;
            Transform2D.CreateRotation(angleRad, out temp);
            ApplyTransform(ref temp);
        }

        /// <summary>
        /// Rotates the canvas on specified angle in radians around origin.
        /// </summary>
        /// <param name="angleRad">The angle in radians.</param>
        /// <param name="origin">The origin of rotation.</param>
        public void Rotate(float angleRad, Vector2 origin)
        {
            Transform2D temp;
            Transform2D.CreateRotation(angleRad, ref origin, out temp);
            ApplyTransform(ref temp);
        }

        /// <summary>
        /// Rotates the canvas on specified angle in radians around origin.
        /// </summary>
        /// <param name="angleRad">The angle in radians.</param>
        /// <param name="originX">The origin of rotation : X coord.</param>
        /// <param name="originY">The origin Y of rotation : Y coord.</param>
        public void Rotate(float angleRad, float originX, float originY)
        {
            Rotate(angleRad, new Vector2(originX, originY));
        }

        /// <summary>
        /// Scales canvas for the specified factor agains (0,0) point.
        /// </summary>
        /// <param name="factor">The factor.</param>
        public void Scale(float factor)
        {
            Scale(new Vector2(factor));
        }

        /// <summary>
        /// Scales canvas against the origin point
        /// </summary>
        /// <param name="factor">The scale factor.</param>
        /// <param name="origin">The origin point.</param>
        public void Scale(float factor, Vector2 origin)
        {
            Scale(new Vector2(factor), origin);
        }

        /// <summary>
        /// Scales canvas for the specified factor agains the (0,0) point.
        /// </summary>
        /// <param name="factor">The factor.</param>
        public void Scale(Vector2 factor)
        {
            Transform2D temp;
            Transform2D.CreateScaling(ref factor, out temp);
            ApplyTransform(ref temp);
        }

        /// <summary>
        /// Scales canvas against the origin point
        /// </summary>
        /// <param name="factor">The scale factor.</param>
        /// <param name="origin">The origin point.</param>
        public void Scale(Vector2 factor, Vector2 origin)
        {
            Transform2D temp;
            Transform2D.CreateScaling(ref factor, ref origin, out temp);
            ApplyTransform(ref temp);
        }

        /// <summary>
        /// Scales canvas against the origin point
        /// </summary>
        /// <param name="factor">The scale factor.</param>
        /// <param name="originX">The origin point X.</param>
        /// <param name="originY">The origin point Y.</param>
        public void Scale(float factor, float originX, float originY)
        {
            Scale(factor, new Vector2(originX, originY));
        }


        /// <summary>
        /// Scales canvas against the origin point
        /// </summary>
        /// <param name="factor">The factor.</param>
        /// <param name="originX">The origin point X.</param>
        /// <param name="originY">The origin point Y.</param>
        public void Scale(Vector2 factor, float originX, float originY)
        {
            Scale(factor, new Vector2(originX, originY));
        }

        #endregion

        #endregion

        #region Begin & End
        public virtual void Begin(DrawingSortMode sortMode)
        {
            this.SortMode = sortMode;
            Begin();
        }

        public virtual void Begin()
        {
			this.Render.UpdateProjection();
        }

        public virtual void End()
        {
            Flush();
        }
        #endregion

        #region Flush
        protected virtual void Flush()
        {
            if (SortMode == DrawingSortMode.Order)
            {
                RenderTriangle();
                RenderLine();
            }
            else if (SortMode.HasFlag(DrawingSortMode.Line))
            {
                RenderLine();
                RenderTriangle();
            }
            else
            {
                RenderTriangle();
                RenderLine();
            }
        }

        private void RenderTriangle()
        {
            #region update triangle
            if (this._triangleVertexCount > 0)
            {
                PrimitiveType primitiveType = PrimitiveType.TriangleList;
                int primitiveCount = CalculatePrimitiveCount(primitiveType, _triangleIndexCount);
                this.render.DrawIndexedPrimitives<VertexPositionColor>(primitiveType, _triangleVertexData, 0, _triangleVertexCount, _triangleIndexData, 0, primitiveCount);

                _triangleVertexCount = 0;
                _triangleIndexCount = 0;
            }
            #endregion
        }
        private void RenderLine()
        {
            #region update line
            if (this._lineVertexCount > 0)
            {
                PrimitiveType primitiveType = PrimitiveType.LineList;
                int primitiveCount = CalculatePrimitiveCount(primitiveType, _lineIndexCount);
                this.render.DrawIndexedPrimitives<VertexPositionColor>(primitiveType, _lineVertexData, 0, _lineVertexCount, _lineIndexData, 0, primitiveCount);

                _lineVertexCount = 0;
                _lineIndexCount = 0;
            }
            #endregion
        }
        #endregion
    }
}

