using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

namespace XnaEngine.Scene.Rendering
{
    /// <summary>
    /// Transformation in 2D space. Internally uses 3x2 matrix logic
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    // TODO: Matrix 
    public struct Transform2D : IEquatable<Transform2D>
    {
        public float M11;
        public float M12;
        public float TX;
        public float M21;
        public float M22;
        public float TY;

        private static Transform2D _empty;


        /// <summary>
        /// Gets a value indicating whether this transformation is empty.
        /// </summary>
        /// <value><c>true</c> if this transformation is empty; otherwise, <c>false</c>.</value>
        public bool IsEmpty
        {
            get
            {
                return M11 == 1 && M22 == 1 && TX == 0 && TY == 0 && M12 == 0 && M21 == 0;
            }
        }

        /// <summary>
        /// Initializes the <see cref="Transform2D"/> struct static member.
        /// </summary>
        static Transform2D()
        {
            Transform2D.CreateEmpty(out _empty);
        }

        /// <summary>
        /// Gets the empty transformation.
        /// </summary>
        /// <value>The empty.</value>
        public static Transform2D Empty
        {
            get { return _empty; }
        }

        #region Operations

        /// <summary>
        /// Combines (multiplies) specified transformations.
        /// </summary>
        /// <param name="t1">Left transform.</param>
        /// <param name="t2">Right transform</param>
        /// <param name="result">The result of combining.</param>
        public static void Multiply(ref Transform2D t1, ref Transform2D t2, out Transform2D result)
        {
            result.M11 = t1.M11 * t2.M11 + t1.M21 * t2.M12;
            result.M12 = t1.M12 * t2.M11 + t1.M22 * t2.M12;

            result.M21 = t1.M11 * t2.M21 + t1.M21 * t2.M22;
            result.M22 = t1.M12 * t2.M21 + t1.M22 * t2.M22;

            result.TX = t1.TX * t2.M11 + t1.TY * t2.M12 + t2.TX;
            result.TY = t1.TX * t2.M21 + t1.TY * t2.M22 + t2.TY;
        }

        /// <summary>
        /// Multiplies (transforms) the specified transform and vector.
        /// </summary>
        /// <param name="transform">The transform.</param>
        /// <param name="vector">The vector.</param>
        /// <param name="result">The result of transformation.</param>
        public static void Multiply(ref Transform2D transform, ref Vector2 vector, out Vector2 result)
        {
            result = new Vector2(
                transform.M11 * vector.X + transform.M12 * vector.Y + transform.TX,
                transform.M21 * vector.X + transform.M22 * vector.Y + transform.TY
            );
        }

        /// <summary>
        /// Restores transformed vector into original value
        /// </summary>
        /// <param name="vector">The transformed vector.</param>
        /// <param name="transform">The transform.</param>
        /// <param name="result">The result vector.</param>
        public static void ReverseMultiply(ref Transform2D transform, ref Vector2 vector, out Vector2 result)
        {
            float cx = vector.X - transform.TX, cy = vector.Y - transform.TY;
            float div = 1 / (transform.M12 * transform.M21 - transform.M22 * transform.M11);

            result = new Vector2(
                (transform.M12 * cy - transform.M22 * cx) * div,
                (transform.M11 * cy - transform.M21 * cx) * -div
            );
        }

        /// <summary>
        /// Adds specified transformations
        /// </summary>
        /// <param name="t1">Left transform.</param>
        /// <param name="t2">Right transform.</param>
        /// <param name="result">The result of addition.</param>
        public static void Add(ref Transform2D t1, ref Transform2D t2, out Transform2D result)
        {
            result.M11 = t1.M11 + t2.M11;
            result.M12 = t1.M12 + t2.M12;
            result.M21 = t1.M21 + t2.M21;
            result.M22 = t1.M22 + t2.M22;
            result.TX = t1.TX + t2.TX;
            result.TY = t1.TY + t2.TY;
        }

        /// <summary>
        /// Subtracts specified transformations
        /// </summary>
        /// <param name="t1">Left transform.</param>
        /// <param name="t2">Right transform.</param>
        /// <param name="result">The result of subtraction.</param>
        public static void Subtract(ref Transform2D t1, ref Transform2D t2, out Transform2D result)
        {
            result.M11 = t1.M11 - t2.M11;
            result.M12 = t1.M12 - t2.M12;
            result.M21 = t1.M21 - t2.M21;
            result.M22 = t1.M22 - t2.M22;
            result.TX = t1.TX - t2.TX;
            result.TY = t1.TY - t2.TY;
        }

        /// <summary>
        /// Combine translate with transform
        /// </summary>
        /// <param name="t">The source transform.</param>
        /// <param name="offset">The offset to translate.</param>
        /// <param name="result">The result of translation.</param>
        public static void MultiplyTranslate(ref Transform2D t, ref Vector2 offset, out Transform2D result)
        {
            result.M11 = t.M11;
            result.M12 = t.M12;

            result.M21 = t.M11;
            result.M22 = t.M12;

            result.TX = t.M11 * offset.X + t.M21 * offset.Y + t.TX;
            result.TY = t.M12 * offset.X + t.M22 * offset.Y + t.TY;
        }

        /// <summary>
        /// Transforms the specified vector.
        /// </summary>
        /// <param name="vector">The vector to transform.</param>
        /// <returns>Transformed vector</returns>
        public Vector2 Multiply(Vector2 vector)
        {
            Vector2 result;
            Multiply(ref this, ref vector, out result);
            return result;
        }

        /// <summary>
        /// Make reverse transform of the specified vector.
        /// </summary>
        /// <param name="vector">The vector to transform.</param>
        /// <returns>Transformed vector</returns>
        public Vector2 ReverseMultiply(Vector2 vector)
        {
            Vector2 result;
            ReverseMultiply(ref this, ref vector, out result);
            return result;
        }

        #endregion

        #region Instance Operations

        /// <summary>
        /// Adds the specified translation offset.
        /// </summary>
        /// <param name="x">The x offset to translate.</param>
        /// <param name="y">The y offset to translate.</param>
        /// <returns>Result transform</returns>
        public Transform2D Translate(float x, float y)
        {
            Vector2 offset = new Vector2(x, y);
            Transform2D result, temp;
            Transform2D.CreateTranslation(ref offset, out temp);
            Transform2D.Multiply(ref this, ref temp, out result);
            return result;
        }

        /// <summary>
        /// Adds the specified translation offset.
        /// </summary>
        /// <param name="offset">The offset to translate.</param>
        /// <returns>Result transform</returns>
        public Transform2D Translate(Vector2 offset)
        {
            Transform2D result, temp;
            Transform2D.CreateTranslation(ref offset, out temp);
            Transform2D.Multiply(ref this, ref temp, out result);
            return result;
        }

        /// <summary>
        /// Adds rotation around (0, 0) point.
        /// </summary>
        /// <param name="angleRad">The angle in radians.</param>
        /// <returns>Result transform</returns>
        public Transform2D Rotate(float angleRad)
        {
            Transform2D result, temp;
            Transform2D.CreateRotation(angleRad, out temp);
            Transform2D.Multiply(ref this, ref temp, out result);
            return result;
        }

        /// <summary>
        /// Adds rotation around origin point.
        /// </summary>
        /// <param name="angleRad">The angle in radians.</param>
        /// <param name="origin">The origin point.</param>
        /// <returns>Result transform</returns>
        public Transform2D Rotate(float angleRad, Vector2 origin)
        {
            Transform2D result, temp;
            Transform2D.CreateRotation(angleRad, ref origin, out temp);
            Transform2D.Multiply(ref this, ref temp, out result);
            return result;
        }

        /// <summary>
        /// Adds scaling against (0, 0) point
        /// </summary>
        /// <param name="factor">The scaling factor.</param>
        /// <returns>Result transform</returns>
        public Transform2D Scale(Vector2 factor)
        {
            Transform2D result, temp;
            Transform2D.CreateScaling(ref factor, out temp);
            Transform2D.Multiply(ref this, ref temp, out result);
            return result;
        }

        /// <summary>
        /// Adds scaling against the origin point
        /// </summary>
        /// <param name="factor">The scaling factor.</param>
        /// <param name="origin">The origin point.</param>
        /// <returns>Result transform</returns>
        public Transform2D Scale(Vector2 factor, Vector2 origin)
        {
            Transform2D result, temp;
            Transform2D.CreateScaling(ref factor, ref origin, out temp);
            Transform2D.Multiply(ref this, ref temp, out result);
            return result;
        }

        #endregion

        #region Operators

        public static Transform2D operator *(Transform2D left, Transform2D right)
        {
            Transform2D result;
            Multiply(ref left, ref right, out result);
            return result;
        }

        public static Vector2 operator *(Transform2D left, Vector2 right)
        {
            Vector2 result;
            Multiply(ref left, ref right, out result);
            return result;
        }

        public static Vector2 operator /(Transform2D left, Vector2 right)
        {
            Vector2 result;
            ReverseMultiply(ref left, ref right, out result);
            return result;
        }

        public static Transform2D operator +(Transform2D left, Transform2D right)
        {
            Transform2D result;
            Add(ref left, ref right, out result);
            return result;
        }

        public static Transform2D operator -(Transform2D left, Transform2D right)
        {
            Transform2D result;
            Add(ref left, ref right, out result);
            return result;
        }

        #endregion

        #region Create methods

        /// <summary>
        /// Creates the empty transform (identity).
        /// </summary>
        /// <returns>Empty transform</returns>
        public static Transform2D CreateEmpty()
        {
            Transform2D temp;
            temp.M11 = temp.M22 = 1.0f;
            temp.M12 = temp.M21 = 0.0f;
            temp.TX = temp.TY = 0.0f;
            return temp;
        }

        /// <summary>
        /// Creates the translation transform.
        /// </summary>
        /// <param name="offsetX">The offset X.</param>
        /// <param name="offsetY">The offset Y.</param>
        /// <returns>Translation transform</returns>
        public static Transform2D CreateTranslation(float offsetX, float offsetY)
        {
            Transform2D temp;
            temp.M11 = temp.M22 = 1.0f;
            temp.M12 = temp.M21 = 0.0f;
            temp.TX = offsetX;
            temp.TY = offsetY;
            return temp;
        }

        /// <summary>
        /// Creates the translation transform.
        /// </summary>
        /// <param name="translation">The translation.</param>
        /// <returns>Translation transform</returns>
        public static Transform2D CreateTranslation(Vector2 translation)
        {
            Transform2D temp;
            temp.M11 = temp.M22 = 1.0f;
            temp.M12 = temp.M21 = 0.0f;
            temp.TX = translation.X;
            temp.TY = translation.Y;
            return temp;
        }

        /// <summary>
        /// Creates the rotation transform.
        /// </summary>
        /// <param name="angleRad">The angle in radians.</param>
        /// <returns>Rotation transform</returns>
        public static Transform2D CreateRotation(float angleRad)
        {
            Transform2D temp;
            CreateRotation(angleRad, out temp);
            return temp;
        }

        /// <summary>
        /// Creates the rotation transform around origin point.
        /// </summary>
        /// <param name="angleRad">The angle in radians.</param>
        /// <param name="origin">The rotation origin.</param>
        /// <returns>Rotation transform</returns>
        public static Transform2D CreateRotation(float angleRad, Vector2 origin)
        {
            Transform2D temp;
            CreateRotation(angleRad, ref origin, out temp);
            return temp;
        }

        /// <summary>
        /// Creates the scaling transform.
        /// </summary>
        /// <param name="scaleFactor">The scale factor.</param>
        /// <returns>Scaling transform</returns>
        public static Transform2D CreateScaling(float scaleFactor)
        {
            Transform2D temp;
            Vector2 factor = new Vector2(scaleFactor);
            CreateScaling(ref factor, out temp);
            return temp;
        }

        /// <summary>
        /// Creates the scaling transform.
        /// </summary>
        /// <param name="scaleFactor">The scale factor.</param>
        /// <returns>Scaling transform</returns>
        public static Transform2D CreateScaling(Vector2 scaleFactor)
        {
            Transform2D temp;
            CreateScaling(ref scaleFactor, out temp);
            return temp;
        }

        /// <summary>
        /// Creates the scaling transform relative the origin point.
        /// </summary>
        /// <param name="scaleFactor">The scale factor.</param>
        /// <param name="origin">The origin point.</param>
        /// <returns>Scaling transform</returns>
        public static Transform2D CreateScaling(Vector2 scaleFactor, Vector2 origin)
        {
            Transform2D temp;
            CreateScaling(ref scaleFactor, ref origin, out temp);
            return temp;
        }

        //-----------------------------------

        /// <summary>
        /// Creates the empty transform (identity).
        /// </summary>
        /// <param name="result">The result transform.</param>
        public static void CreateEmpty(out Transform2D result)
        {
            result.M11 = result.M22 = 1.0f;
            result.M12 = result.M21 = 0.0f;
            result.TX = result.TY = 0.0f;
        }

        /// <summary>
        /// Creates the translation transform.
        /// </summary>
        /// <param name="result">The result transform.</param>
        public static void CreateTranslation(ref Vector2 translation, out Transform2D result)
        {
            result.M11 = result.M22 = 1.0f;
            result.M12 = result.M21 = 0.0f;
            result.TX = translation.X;
            result.TY = translation.Y;
        }

        /// <summary>
        /// Creates the rotation transform.
        /// </summary>
        /// <param name="angleRad">The angle in radians.</param>
        /// <param name="result">The result transform.</param>
        public static void CreateRotation(float angleRad, out Transform2D result)
        {
            result.M11 = (float)Math.Cos(angleRad);
            result.M21 = (float)Math.Sin(angleRad);
            result.M12 = -result.M21;
            result.M22 = result.M11;
            result.TX = result.TY = 0.0f;
        }

        /// <summary>
        /// Creates the rotation transform around origin point.
        /// </summary>
        /// <param name="angleRad">The angle in radians.</param>
        /// <param name="origin">The rotation origin.</param>
        /// <param name="result">The result transform.</param>
        public static void CreateRotation(float angleRad, ref Vector2 origin, out Transform2D result)
        {
            result.M11 = (float)Math.Cos(angleRad);
            result.M21 = (float)Math.Sin(angleRad);
            result.M12 = -result.M21;
            result.M22 = result.M11;

            result.TX = result.M11 * (-origin.X) + result.M21 * origin.Y + origin.X;
            result.TY = result.M12 * origin.X + result.M22 * (-origin.Y) + origin.Y;
        }

        /// <summary>
        /// Creates the scaling transform.
        /// </summary>
        /// <param name="angleRad">The angle in radians.</param>
        /// <param name="result">The result transform.</param>
        public static void CreateScaling(ref Vector2 scaleFactor, out Transform2D result)
        {
            result.M11 = scaleFactor.X;
            result.M22 = scaleFactor.Y;
            result.M12 = 0.0f;
            result.M21 = 0.0f;

            result.TX = result.TY = 0.0f;
        }

        /// <summary>
        /// Creates the scaling transform relative the origin point.
        /// </summary>
        /// <param name="scaleFactor">The scale factor.</param>
        /// <param name="origin">The origin point.</param>
        /// <param name="result">The result transform.</param>
        public static void CreateScaling(ref Vector2 scaleFactor, ref Vector2 origin, out Transform2D result)
        {
            result.M11 = scaleFactor.X;
            result.M12 = 0.0f;
            result.M22 = scaleFactor.Y;
            result.M21 = 0.0f;

            result.TX = result.M11 * (-origin.X) + result.M21 * origin.Y + origin.X;
            result.TY = result.M12 * origin.X + result.M22 * (-origin.Y) + origin.Y;
        }

        #endregion


        #region IEquatable<Transform> Members

        /// <summary>
        /// Indicates whether the current transform is equal to another transform.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current transform is equal to the <param ref name="other"/>; otherwise, false.
        /// </returns>
        public bool Equals(Transform2D other)
        {
            return
                M11 == other.M11 &&
                M12 == other.M12 &&
                TX == other.TX &&
                M21 == other.M21 &&
                M22 == other.M22 &&
                TY == other.TY;
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this transform.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this transform.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{{M11={0}, M12={1}, M21={2}, M22={3}; TX={4}, TY={5}}}", M11, M12, M21, M22, TX, TY);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this transform instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this transform instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this transform instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj is Transform2D ? Equals((Transform2D)obj) : false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return (int)(M11 + M12 + M21 + M22 + TX + TY);
        }
    }
}
