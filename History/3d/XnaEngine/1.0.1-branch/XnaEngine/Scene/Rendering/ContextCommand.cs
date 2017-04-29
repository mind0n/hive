using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaEngine.Scene.Rendering
{
    public class ContextCommand
    {
        public CommandType CommandType
        {
            get;
            set;
        }

        public Vector2[] Vectors
        {
            get;
            set;
        }

        public Color Color
        {
            get;
            set;
        }

        public object[] Addtions
        {
            get;
            set;
        }
    }
}
