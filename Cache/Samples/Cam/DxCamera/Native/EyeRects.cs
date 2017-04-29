
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using OpenCvSharp;

namespace Eyes
{
    public class EyeRects
    {
        protected List<CvRect> xs = new List<CvRect>();
        protected List<CvRect> ys = new List<CvRect>(); 
        public void AddRect(CvRect r)
        {
            if (xs.Count < 1)
            {
                xs.Add(r);
            }
            else
            {
                for (int i = 0; i < xs.Count; i++)
                {
                    var c = xs[i];
                    if (r.X <= c.X)
                    {
                        xs.Insert(i, r);
                        break;
                    }
                }
            }
            if (ys.Count < 1)
            {
                ys.Add(r);
            }
            else
            {
                for (int i = 0; i < ys.Count; i++)
                {
                    var c = ys[i];
                    if (r.Y <= c.Y)
                    {
                        ys.Insert(i, c);
                        break;
                    }
                }
            }
        }

        public Point Pos(int padding = 0)
        {
            var z = new Point(padding, padding);
            if (xs.Count < 1)
            {
                return z;
            }
            var r = xs[0];
            var x = r.X + r.Width/2 - padding;
            var y = r.Y + r.Height/2 - padding;
            return new Point (x >= 0 ? x : 0, y >= 0 ? y : 0);
        } 
    }
}
