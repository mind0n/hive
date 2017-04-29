using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XnaEngine.Scene.Rendering
{
    /// <summary>
    /// Interface to implement for objects which represents some kind of transformation like Transform2D, Translation, Rotation, 
    /// </summary>
    public interface ITransform
    {
        /// <summary>
        /// Gets the transform which represents this object.
        /// </summary>
        /// <param name="transform">The transform represented by this object.</param>
        void GetTransform(out Transform2D transform);
    }
}
