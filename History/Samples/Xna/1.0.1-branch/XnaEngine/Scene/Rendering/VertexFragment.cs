using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XnaEngine.Scene.Rendering
{
    public class VertexFragment
    {
        public int VertexCount { get; set; }
        public int IndexCount { get; set; }
        public PrimitiveType PrimitiveType { get; set; }
        
        public const int ELLIPSE_SEGMENTS = 64;
        private static Vector2[] _ellipseSegments;
        /// <summary>
        /// Gets segments for building ellipses.
        /// </summary>
        /// <value>The ellipse segments.</value>
        public static Vector2[] EllipseSegments
        {
            get { return _ellipseSegments; }
        }

        static VertexFragment()
        {
            _ellipseSegments = new Vector2[ELLIPSE_SEGMENTS];
            float angle = 0.0f, angleStep = (float)(MathHelper.Pi * 2 / (double)ELLIPSE_SEGMENTS);
            for (int n = 0; n < ELLIPSE_SEGMENTS; ++n)
            {
                _ellipseSegments[n] = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * Vector2.One.Half() + Vector2.One.Half();
                angle += angleStep;
            }
        }

        public VertexFragment(int vertexCount, int indexCount, PrimitiveType primitiveType)
        {
            VertexCount = vertexCount;
            IndexCount = indexCount;
            PrimitiveType = primitiveType;
        }

        private static readonly VertexFragment _line = new VertexFragment(2, 2, PrimitiveType.LineList);
        public static VertexFragment Line
        {
            get
            {
                return _line;
            }
        }
        private static readonly VertexFragment _styledRectangle = new VertexFragment(2 * (4 + 1), 3 * 4 * 2, PrimitiveType.TriangleList);
        public static VertexFragment StyledRectangle
        {
            get
            {
                return _styledRectangle;
            }
        }
   
        private static readonly VertexFragment _rectangle = new VertexFragment(4, 8, PrimitiveType.LineList);
        public static VertexFragment Rectangle
        {
            get
            {
                return _rectangle;
            }
        }
        private static readonly VertexFragment _filledRectangle = new VertexFragment(4, 6, PrimitiveType.TriangleList);
        public static VertexFragment FilledRectangle
        {
            get
            {
                return _filledRectangle;
            }
        }
    
        private static readonly VertexFragment _triangle = new VertexFragment(3, 6, PrimitiveType.LineList);
        public static VertexFragment Triangle
        {
            get
            {
                return _triangle;
            }
        }
        private static readonly VertexFragment _filledTriangle = new VertexFragment(3, 3, PrimitiveType.TriangleList);
        public static VertexFragment FilledTriangle
        {
            get
            {
                return _filledTriangle;
            }
        }
        
        private static readonly VertexFragment _ellipse = new VertexFragment(ELLIPSE_SEGMENTS, ELLIPSE_SEGMENTS * 2, PrimitiveType.LineList);
        /// <summary>
        /// Gets the ellipse fragment to used for request vertices.
        /// </summary>
        /// <value>The ellipse fragment.</value>
        public static VertexFragment Ellipse
        {
            get { return _ellipse; }
        }
        private static readonly VertexFragment _filledEllipse = new VertexFragment(ELLIPSE_SEGMENTS + 1, ELLIPSE_SEGMENTS * 3, PrimitiveType.TriangleList);
        /// <summary>
        /// Gets the filled ellipse fragment to used for request vertices.
        /// </summary>
        /// <value>The filled ellipse fragment.</value>
        public static VertexFragment FilledEllipse
        {
            get { return _filledEllipse; }
        }
    }
}
