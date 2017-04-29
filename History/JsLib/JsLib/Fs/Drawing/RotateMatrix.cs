using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Fs.Drawing
{
	public class RotateMatrix
	{
		public float Angle;
		public Matrix Matrix;
		public PointF RelativeOffset;
		public PointF AbsoluteOffset;
		public Size IntSize
		{
			get { return ActualSize.ToSize(); }
			set { _size = new SizeF(value.Width, value.Height); }
		}
		public SizeF ActualSize
		{
			get { return new SizeF(_size.Width - RelativeOffset.X*2, _size.Height - RelativeOffset.Y*2); }
			set { _size = value; }
		}protected SizeF _size;
		public bool IsBiggerEqualTo(SizeF size)
		{
			if (size.Width < _size.Width && size.Height < _size.Height )
			{
				return true;
			} 
			return false;
		}
		public bool IsInside(RectangleF rf)
		{
			//if ((AbsoluteOffset.X > (rf.X + rf.Width) || (AbsoluteOffset.X + ActualSize.Width) < rf.X) && (AbsoluteOffset.Y > (rf.Y + rf.Height) || (AbsoluteOffset.Y + ActualSize.Height) < rf.Y))
			//{
			//    return false;
			//}
			PointF ul, ur, bl, br;
			
			ul = new PointF(AbsoluteOffset.X, AbsoluteOffset.Y);
			ur = new PointF(AbsoluteOffset.X + ActualSize.Width, AbsoluteOffset.Y);
			bl = new PointF(AbsoluteOffset.X, AbsoluteOffset.Y + ActualSize.Height);
			br = new PointF(AbsoluteOffset.X + ActualSize.Width, AbsoluteOffset.Y + ActualSize.Height);
			if (RenderHelper.IsPointOverRect(ul, rf)
				|| RenderHelper.IsPointOverRect(ur, rf)
				|| RenderHelper.IsPointOverRect(bl, rf)
				|| RenderHelper.IsPointOverRect(br, rf))
			{
				return true;
			}
			return false;
		}
		public bool IsBiggerEqualTo(Size size)
		{
			if (IntSize.Width >= size.Width && IntSize.Height >= size.Height)
			{
				return true;
			}
			return false;
		}
		public Bitmap GetNewImage()
		{
			Size s = ActualSize.ToSize();
			Bitmap img = new Bitmap(s.Width, s.Height);
			return img;
		}
		public RotateMatrix(Matrix m, float angle, PointF center, SizeF size)
		{
			Init(m, angle, center.X, center.Y, size.Width, size.Height);
		}
		public RotateMatrix(Matrix m, float angle, float x, float y, float width, float height)
		{
			Init(m, angle, x, y, width, height);
		}
		protected void Init(Matrix m, float angle, float x, float y, float width, float height)
		{
			Matrix = m;
			Angle = angle;
			RelativeOffset = new PointF(x, y);
			ActualSize = new SizeF(width, height);
		}
	}
}
