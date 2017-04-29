using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XnaEngine.Scene.Rendering
{
    public static class Extensions
    {
        #region Color
        public static Color ChangeAlpha(this Color color, byte alpha)
        {
            color.A = alpha;
            return color;
        }
        #endregion
        #region Vector2
        public static Vector2 Half(this Vector2 vector)
        {
            return new Vector2(vector.X * 0.5f, vector.Y * 0.5f);
        }

        public static readonly Vector2 Vector2Unit = new Vector2(1f, 1f);
        #endregion
        #region Vector3
        public static Vector3 Half(this Vector3 vector)
        {
            return new Vector3(vector.X * 0.5f, vector.Y * 0.5f, vector.Z * 0.5f);
        }

        public static readonly Vector3 Vector3Unit = new Vector3(1f, 1f, 1f);
        #endregion
        #region Rectangle
        public static readonly Rectangle RectangleUnit =  NewRectangle(Vector2.Zero, Vector2Unit);

        public static Rectangle NewRectangle(Vector2 v1, Vector2 v2)
        {
            return new Rectangle((int)v1.X, (int)v1.Y, (int)v2.X, (int)v2.Y);
        }

        public static Vector2 LeftTop(this Rectangle rectangle)
        {
            return new Vector2(rectangle.X, rectangle.Y);
        }

        public static Vector2 Size(this Rectangle rectangle)
        {
            return new Vector2(rectangle.Width, rectangle.Height);
        }
        #endregion
        #region Texture2D

        #endregion
    }
}
