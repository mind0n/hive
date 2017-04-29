using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using Joy.Core;

namespace Joy.Server.Drawing
{
	public static class RenderHelper
	{
		public static bool DrawActualBounds = false;
		public static bool IsRectOverRect(RectangleF rectUp, RectangleF rectDown)
		{
			PointF ul, ur, bl, br;
			ul = new PointF(rectUp.X, rectUp.Y);
			ur = new PointF(rectUp.X + rectUp.Width, rectUp.Y);
			bl = new PointF(rectUp.X, rectUp.Y + rectUp.Height);
			br = new PointF(rectUp.X + rectUp.Width, rectUp.Y + rectUp.Height);
			if (RenderHelper.IsPointOverRect(ul, rectDown)
				|| RenderHelper.IsPointOverRect(ur, rectDown)
				|| RenderHelper.IsPointOverRect(bl, rectDown)
				|| RenderHelper.IsPointOverRect(br, rectDown))
			{
				return true;
			}
			return false;

		}
		public static bool IsPointOverRect(PointF p, RectangleF r)
		{
			if (p.X >= r.X && p.Y >= r.Y && p.X <= r.X + r.Width && p.Y <= r.Y + r.Height)
			{
				return true;
			}
			return false;
		}
		public static void ClearRegion(Graphics g, OffsetImage oi)
		{
			ClearRegion(g, oi, Brushes.Blue);
		}
		public static void ClearRegion(Graphics g, OffsetImage oi, Color c)
		{
			SolidBrush sbrush = new SolidBrush(c);
			ClearRegion(g, oi, sbrush);
		}
		public static void ClearRegion(Graphics g, OffsetImage oi, Brush b)
		{
			g.FillRectangle(b, oi.ActualBounds);
		}
		public static void DrawString(ref OffsetImage target, string text, Font font, Brush brush, RotateMatrix rm)
		{
			SizeF size = new SizeF();
			Bitmap img = new Bitmap(1, 1);
			Graphics g = Graphics.FromImage(img);
			size = g.MeasureString(text, font);
			DrawString(ref target, text, size, font, brush, rm);
		}
		public static void DrawString(ref OffsetImage target, string text, SizeF stringSize, Font font, Brush brush, RotateMatrix rm)
		{
			//RotateMatrix rm = RenderHelper.Rotate(angle, stringSize.Width / 2, stringSize.Height / 2, stringSize.Width, stringSize.Height, 1, 1, 0, 0, drawOffsetX, drawOffsetY);
			Bitmap imgstr = new Bitmap((int)stringSize.Width, (int)stringSize.Height);
			Graphics gstr = Graphics.FromImage(imgstr);
			gstr.DrawString(text, font, brush, new PointF(0, 0));
			DrawImage(ref target, imgstr, rm);
		}
		public static void DrawString(Graphics g, OffsetImage target, string text, Font font, Brush brush, RotateMatrix matrix, PointF location)
		{
			if (g != null)
			{
				g = Graphics.FromImage(new Bitmap(1, 1));
			}
			SizeF sz = g.MeasureString(text, font);
			Bitmap iTemp = new Bitmap((int)sz.Width, (int)sz.Height);
			Graphics gTemp = Graphics.FromImage(iTemp);
			gTemp.DrawString(text, font, brush, 0, 0);
			gTemp.Dispose();
			DrawImage(ref target, iTemp, matrix);
			DrawImage(g, target, location);
		}
		public static void DrawImage(Graphics g, OffsetImage oi)
		{
			DrawImage(g, oi, new PointF(0, 0));
		}
		public static void DrawImage(Graphics g, OffsetImage oi, PointF location)
		{
			float x = 0, y = 0;
			x = location.X + oi.Offset.X;
			y = location.Y + oi.Offset.Y;
			oi.Position = new PointF(x, y);
			oi.IsDrawn = true;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.CompositingQuality = CompositingQuality.HighQuality;
			//Matrix m = new Matrix();
			//m.RotateAt(-90, new PointF(portion.Width/2, portion.Height/2));
			//g.Transform = m;
			g.DrawImage(oi.Source, new PointF(0, 0));
		}
		public static void DrawImage(ref OffsetImage oi, OffsetImage content, RotateMatrix matrix)
		{
			//oi.Matrix = matrix;
			content.Matrix = matrix;
			content.RenderedSource = DrawImage(ref oi.Source, content.Source, matrix);
		}
		public static void DrawImage(ref OffsetImage oi, Bitmap content, RotateMatrix matrix)
		{
			DrawImage(ref oi.Source, content, matrix);
		}
		public static Bitmap DrawImage(ref Bitmap target, Bitmap content, RotateMatrix matrix)
		{
			Bitmap temp = null;
			try
			{
				if (matrix.IsBiggerEqualTo(target.Size))
				{
					target.Dispose();
					target = matrix.GetNewImage();
				}
				temp = new Bitmap((int)matrix.ActualSize.Width, (int)matrix.ActualSize.Height);
				PointF offsetPoint = new PointF(matrix.AbsoluteOffset.X + matrix.RelativeOffset.X, matrix.AbsoluteOffset.Y + matrix.RelativeOffset.Y);
				Graphics g = Graphics.FromImage(temp);
				g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				g.Transform = matrix.Matrix;
				g.DrawImage(content, new Point(0, 0));
				if (DrawActualBounds)
				{
					g.DrawRectangle(Pens.Red, 0, 0, content.Width, content.Height);
				}
				g.Dispose();
				g = Graphics.FromImage(target);
				g.DrawImage(temp, offsetPoint);
				if (DrawActualBounds)
				{
					g.DrawRectangle(Pens.Maroon, offsetPoint.X, offsetPoint.Y, matrix.ActualSize.Width, matrix.ActualSize.Height);
				}
				g.Dispose();
			}
			catch (Exception e)
			{
				Exceptions.LogOnly(e);
			}
			return temp;
		}
		public static Bitmap LoadImage(int width, int height)
		{
			Bitmap img = new Bitmap(width, height);
			Graphics g = Graphics.FromImage(img);
			g.FillRectangle(Brushes.SkyBlue, new Rectangle(0, 0, img.Width, img.Height));
			g.Dispose();
			return img;
		}
		public static OffsetImage LoadImage(
			string fullFileName
			, int startx, int starty
			, int width, int height
		)
		{
			float scale = 1;
			Image raw = new Bitmap(fullFileName);
			if (width == 0)
			{
				width = raw.Width;
			}
			if (height == 0)
			{
				height = raw.Height;
			}
			int rltwidth = Convert.ToInt32(Math.Ceiling(width * scale));
			int rltheight = Convert.ToInt32(Math.Ceiling(height * scale));
			Bitmap rlt = new Bitmap(rltwidth, rltheight);

			Graphics g = Graphics.FromImage(rlt);
			PointF ul = new PointF(0, 0);
			PointF ur = new PointF(rltwidth, 0);
			PointF ll = new PointF(0, rltheight);
			PointF[] imgscale = new PointF[] { ul, ur, ll };
			Rectangle portion = new Rectangle(startx, starty, width, height);
			g.DrawImage(raw, imgscale, portion, GraphicsUnit.Pixel);
			g.Dispose();
			OffsetImage oi = OffsetImage.New(rlt);
			return oi;
		}
		public static PointF[] Rotate(
			SizeF size,			// the image to draw
			PointF sourceAxle,	// pivot point passing through image.
			PointF destAxle,	// pivot point's position on destination surface
			float degrees,		// degrees through which the image is rotated clockwise
			//float scale,		// size multiplier
			SizeF skew			// the slanting effect size, applies BEFORE scaling or rotation
		)
		{
			// give this array temporary coords that will be overwritten in the loop below
			// the skewing is done here orthogonally, before any trigonometry is applied
			PointF[] temp = new PointF[] {
                new PointF(skew.Width, -skew.Height),
				new PointF((size.Width - 1) + skew.Width, skew.Height),
				new PointF(-skew.Width,(size.Height - 1) - skew.Height),
				//new PointF(0, 0),
				//new PointF(0, 0)
			};
			float scale = 1;
			float ang, dist;
			float radians = degrees * ((float)Math.PI / 180);
			// convert the images corner points into scaled, rotated, skewed and translated points
			for (int i = 0; i < 3; i++)
			{
				// measure the angle to the image's corner and then add the rotation value to it
				ang = GetBearingRadians(sourceAxle, temp[i], out dist) + radians;
				dist *= scale; // scale
				temp[i] = new PointF((Single)((Math.Cos(ang) * dist) + destAxle.X),
				(Single)((Math.Sin(ang) * dist) + destAxle.Y));
			}
			return temp;
		}
		public static GraphicsContainer Rotate(Graphics g, float angle, float x, float y)
		{
			PointF p = new PointF(x, y);
			return Rotate(g, angle, p);
		}
		public static GraphicsContainer Rotate(Graphics g, float angle, PointF center)
		{
			GraphicsContainer gcr = g.BeginContainer();
			RotateMatrix m = Rotate(angle, center.X, center.Y);
			g.Transform = m.Matrix;
			return gcr;
		}
		public static void Rotate(GraphicsPath gp, float angle, PointF center)
		{
			RotateMatrix m = Rotate(angle, center.X, center.Y);
			gp.Transform(m.Matrix);
		}
		public static RotateMatrix Rotate(float angle, float centerX, float centerY)
		{
			return Rotate(angle, centerX, centerY, centerX * 2, centerY * 2, 1, 1, 0, 0, 0, 0);
		}
		public static RotateMatrix Rotate(float angle, float centerX, float centerY, float drawOffsetX, float drawOffsetY)
		{
			return Rotate(angle, centerX, centerY, centerX * 2, centerY * 2, 1, 1, 0, 0, drawOffsetX, drawOffsetY);
		}
		public static RotateMatrix Rotate(
			double angle
			, double centerX
			, double centerY
			, double width
			, double height
			, double scaleX
			, double scaleY
			, double shearX
			, double shearY
			, double drawOffsetX
			, double drawOffsetY
		)
		{
			float angleF = (float)angle;
			float centerXF = (float)centerX;
			float centerYF = (float)centerY;
			float widthF = (float)width;
			float heightF = (float)height;
			float scaleXF = (float)scaleX;
			float scaleYF = (float)scaleY;
			float shearXF = (float)shearX;
			float shearYF = (float)shearY;
			float drawOffsetXF = (float)drawOffsetX;
			float drawOffsetYF = (float)drawOffsetY;

			PointF offset = new PointF();
			PointF ur = new PointF();
			Matrix matrix = new Matrix();
			PointF center = new PointF(centerXF, centerYF);
			angleF = angleF % 360;
			if (angleF < 0)
			{
				angleF += 360;
			}
			if (angleF % 90 == 0)
			{
				angleF += 0.01f;
			}

			widthF = centerXF * 2 * scaleXF;
			heightF = centerYF * 2 * scaleYF;
			matrix.Scale(scaleXF, scaleYF);
			matrix.Shear(shearXF, shearYF);
			matrix.RotateAt(angleF, center);

			offset.X = matrix.OffsetX;
			offset.Y = matrix.OffsetY;
			float rs, rc;
			rs = (float)Math.Sin(angleF * Math.PI / 180);
			rc = (float)Math.Cos(angleF * Math.PI / 180);
			ur.X = rc * widthF;
			ur.Y = rs * widthF;
			ur.X += matrix.OffsetX;
			ur.Y += matrix.OffsetY;
			angleF = angleF % 360;
			if (angleF >= 0 && angleF <= 90)
			{
				offset.X = ur.X - widthF;
				offset.Y = -matrix.OffsetY;
			}
			else if (angleF > 90 && angleF < 180)
			{
				offset.X = -widthF + matrix.OffsetX;
				offset.Y = ur.Y - heightF;
			}
			else if (angleF >= 180 && angleF <= 270)
			{
				offset.X = -ur.X;
				offset.Y = matrix.OffsetY - heightF;
			}
			else
			{
				offset.X = -matrix.OffsetX;
				offset.Y = -ur.Y;
			}

			matrix.RotateAt(-angleF, center);
			matrix.Translate(offset.X / scaleXF, offset.Y / scaleYF);
			//matrix.Translate(offset.X / scaleXF + drawOffsetXF / scaleXF, offset.Y / scaleYF + drawOffsetYF / scaleYF);
			matrix.RotateAt(angleF, center);
			RotateMatrix rm = new RotateMatrix(matrix, angleF, -offset.X, -offset.Y, widthF, heightF);
			rm.AbsoluteOffset = new PointF(drawOffsetXF, drawOffsetYF);
			return rm;
		}
		private static float GetBearingRadians(PointF reference, PointF target, out float distance)
		{
			float dx = target.X - reference.X;
			float dy = target.Y - reference.Y;
			float result = (float)Math.Atan2(dy, dx);
			distance = (float)Math.Sqrt((dx * dx) + (dy * dy));
			if (result < 0)
				result += ((float)Math.PI * 2); // add the negative number to 360 degrees
			// to correct the atan2 value
			return result;
		}
	}

}
