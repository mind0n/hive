using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XnaEngine.Scene.Rendering
{
    [Flags]
    public enum DrawingSortMode
    {
        Order,
        Line,
        Triangle,
        Sprite
    }
}
