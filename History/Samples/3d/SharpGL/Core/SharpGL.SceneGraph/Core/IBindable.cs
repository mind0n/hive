using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneGraph.Core
{
    /// <summary>
    /// An object that is Bindable is able to set itself into
    /// the current OpenGL instance. This can be lights, materials,
    /// attributes and so on.
    /// </summary>
    public interface IBindable
    {
        /// <summary>
        /// Bind to the specified OpenGL instance.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        void Bind(OpenGL gl);
    }
}
