using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Fs.Drawing
{
	public class OffsetImage
	{
		public RotateMatrix Matrix
		{
			set
			{
				_matrix = value;
				Offset = value.RelativeOffset;
				Position = new PointF(value.AbsoluteOffset.X + value.RelativeOffset.X, value.AbsoluteOffset.Y + value.RelativeOffset.Y);
				ScaleX = value.ActualSize.Width / Width;
				ScaleY = value.ActualSize.Height / Height;
				//Width = value.ActualSize.Width;
				//Height = value.ActualSize.Height;
			}
			get
			{
				return _matrix;
			}
		}protected RotateMatrix _matrix;
		public float ScaleX;
		public float ScaleY;
		public int Width
		{
			get
			{
				if (Source != null)
				{
					return Source.Width;
				}
				else
				{
					return 0;
				}
			}
		}
		public int Height
		{
			get
			{
				if (Source != null)
				{
					return Source.Height;
				}
				else
				{
					return 0;
				}
			}
		}
		public long zIndex = 0;
		public SizeF OriginSize
		{
			get
			{
				return new SizeF(Width, Height);
			}
		}
		public RectangleF ActualBounds
		{
			get
			{
				return new RectangleF(Position.X, Position.Y, OriginSize.Width * ScaleX, OriginSize.Height * ScaleY);
			}
		}
		public Rectangle ClearBounds
		{
			get
			{
				return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height);
			}
		}
		public PointF Position
		{
			get
			{
				return new PointF(_pos.X, _pos.Y);
			}
			set
			{
				_pos = value;
			}
		}protected PointF _pos;
		public PointF Offset = new PointF(0, 0);
		public Bitmap Source;
		public Bitmap RenderedSource;
		internal bool IsDrawn = false;
		public static OffsetImage New(Bitmap img)
		{
			OffsetImage oi = new OffsetImage();
			oi.Source = img;
			return oi;
		}
		public void Dispose()
		{
			if (Source != null)
			{
				Source.Dispose();
			}
			if (RenderedSource != null)
			{
				RenderedSource.Dispose();
			}
		}
		internal OffsetImage()
		{
		}
	}
	public class EventImage : OffsetImage
	{
		public static EventImage New(RenderForm container)
		{
			EventImage ei = new EventImage();
			ei.Container = container;
			return ei;
		}
		protected RenderForm Container
		{
			set
			{
				_container = value;
				value.EventKeyDown += OnKeyDown;
				value.EventKeyUp += OnKeyUp;
				value.EventMouseMove += OnMouseMove;
				value.EventMouseUp += OnMouseUp;
				value.EventMouseDown += OnMouseDown;
				value.EventMouseDblClick += OnMouseDblClick;
			}
			get
			{
				return _container;
			}
		}protected RenderForm _container;
		protected bool IsActive = false;
		public bool IsWithinImage(PointF screenPoint)
		{
			if (!RenderHelper.IsPointOverRect(screenPoint, ActualBounds))
			{
				return false;
			}
			PointF d = new PointF(screenPoint.X - Position.X, screenPoint.Y - Position.Y);
			Color c = RenderedSource.GetPixel((int)d.X, (int)d.Y);
			
			if (c.ToArgb() == Color.Empty.ToArgb())
			{
				return false;
			}
			return true;
		}
		internal virtual void OnMouseDown(object sender, MouseEventArgs e)
		{
			
		}
		internal virtual void OnMouseUp(object sender, MouseEventArgs e)
		{
			
		}
		internal virtual void OnMouseMove(object sender, MouseEventArgs e)
		{
			
		}
		internal virtual void OnMouseDblClick(object sender, MouseEventArgs e)
		{
			MessageBox.Show("DblClick");
		}
		internal virtual void OnKeyDown(object sender, KeyEventArgs e)
		{
			
		}
		internal virtual void OnKeyUp(object sender, KeyEventArgs e)
		{
			
		}
		
		internal EventImage()
		{
		}
	}
}
