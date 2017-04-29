using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using Fs.Reflection;

namespace Fs.Drawing
{
	public class RenderForm : Form
	{
		public RenderForm()
		{
			int num = Math.Max(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			BufferSize = new Size(num, num);
			Portion = new Rectangle(0, 0, Width, Height);
			BufferImage = OffsetImage.New(new Bitmap(BufferSize.Width, BufferSize.Height));
			UpdatePortion(00, 00, Width, Height);
			Load += new EventHandler(MainForm_Load);
			Paint += new PaintEventHandler(MainForm_Paint);
			Resize += new EventHandler(MainForm_Resize);

			KeyDown += new KeyEventHandler(RenderForm_KeyDown);
			KeyUp += new KeyEventHandler(RenderForm_KeyUp);
			MouseMove += new MouseEventHandler(RenderForm_MouseMove);
			MouseDown += new MouseEventHandler(RenderForm_MouseDown);
			MouseUp += new MouseEventHandler(RenderForm_MouseUp);
			MouseDoubleClick += new MouseEventHandler(RenderForm_MouseDoubleClick);
		}
		void EnumDelegateList(Delegate[] list, ClassHelper.DelegateInvoker mi)
		{
			foreach (Delegate d in list)
			{
				mi.Invoke(d);
			}
		}
		void RenderForm_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			long index = 0;
			EventImage receiver = null;
			EnumDelegateList(EventMouseDblClick.GetInvocationList(), delegate(Delegate par)
			{
				EventImage ei = (EventImage)par.Target;
				if (ei.IsWithinImage(e.Location))
				{
					if (ei.zIndex >= index)
					{
						index = ei.zIndex;
						receiver = ei;
					}
				}
				return true;
			});
			if (receiver != null)
			{
				receiver.OnMouseDblClick(sender, e);
			}
		}

		void RenderForm_MouseUp(object sender, MouseEventArgs e)
		{
			if (EventMouseUp != null)
			{
				EventMouseUp(sender, e);
			}
		}

		void RenderForm_MouseDown(object sender, MouseEventArgs e)
		{
			if (EventMouseDown != null)
			{
				EventMouseDown(sender, e);
			}
		}

		void RenderForm_MouseMove(object sender, MouseEventArgs e)
		{
			if (EventMouseMove != null)
			{
				EventMouseMove(sender, e);
			}
		}

		void RenderForm_KeyUp(object sender, KeyEventArgs e)
		{
			if (EventKeyUp != null)
			{
				EventKeyUp(sender, e);
			}
		}

		void RenderForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (EventKeyDown != null)
			{
				EventKeyDown(sender, e);
			}
		}

		public PointF Offset
		{
			get
			{
				return Portion.Location;
			}
			set
			{
				UpdatePortion(value.X, value.Y, Portion.Width, Portion.Height);
			}
		}
		public RectangleF Portion;
		public delegate void MouseActionDelegate(object sender, MouseEventArgs e);
		public delegate void KeyboardActionDelegate(object sender, KeyEventArgs e);
		public MouseActionDelegate EventMouseMove;
		public MouseActionDelegate EventMouseDown;
		public MouseActionDelegate EventMouseUp;
		public MouseActionDelegate EventMouseDblClick;
		public KeyboardActionDelegate EventKeyDown;
		public KeyboardActionDelegate EventKeyUp;

		protected System.Windows.Forms.Timer updTmr;
		protected System.Windows.Forms.Timer drwTmr;
		protected long RenderIndex
		{
			get
			{
				return _renderIndex++;
			}
			set
			{
				_renderIndex = value;
			}
		}protected long _renderIndex = 1;
		protected bool IsPainted = false;
		protected bool Resized
		{
			set
			{
				IsPainted = false;
			}
		}

		internal object BufferImageLock = new object();
		internal OffsetImage BufferImage;
		internal Size BufferSize;

		protected void MainForm_Resize(object sender, EventArgs e)
		{
			Resized = true;
			Portion.Width = Width;
			Portion.Height = Height;
		}
		protected void MainForm_Paint(object sender, PaintEventArgs e)
		{
			//e.Graphics.FillRectangle(Brushes.White, BufferImage.ClearBounds);
			lock (BufferImageLock)
			{
				RenderHelper.DrawImage(e.Graphics, BufferImage, Portion.Location);
			}
		}
		protected void MainForm_Load(object sender, EventArgs e)
		{
			ControlBox = false;
			FormBorderStyle = FormBorderStyle.None;
			//WindowState = FormWindowState.Maximized;
			TopMost = false;
			drwTmr = new System.Windows.Forms.Timer();
			drwTmr.Interval = 31;
			drwTmr.Tick += new EventHandler(drwTmr_Tick);
			drwTmr.Enabled = true;
			drwTmr.Start();
			updTmr = new System.Windows.Forms.Timer();
			updTmr.Interval = 11;
			updTmr.Tick += new EventHandler(tmr_Tick);
			updTmr.Enabled = true;
			updTmr.Start();
			Run();
		}

		protected void drwTmr_Tick(object sender, EventArgs e)
		{
			Invalidate();
		}

		protected void tmr_Tick(object sender, EventArgs e)
		{
			updTmr.Stop();
			updTmr.Enabled = false;
			ClearBufferImage();
			OnRenderFrameUpdate();
			updTmr.Enabled = true;
			updTmr.Start();
		}
		protected void tmr_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			tmr_Tick(sender, e);
		}
		protected void ClearBufferImage()
		{
			Graphics g = Graphics.FromImage(BufferImage.Source);
			g.Clear(BackColor);
			g.Dispose();
		}
		protected void DrawImage(OffsetImage img, double angle, double x, double y)
		{
			DrawImage(img, angle, img.Width / 2, img.Height / 2, 1, 1, 0, 0, x, y);
		}
		protected void DrawImage(OffsetImage img, double angle, double centerX, double centerY, double scaleX, double scaleY, double shearX, double shearY, double drawOffsetX, double drawOffsetY)
		{
			//Graphics g = Graphics.FromImage(BufferImage.Source);
			if (RenderHelper.IsRectOverRect(new RectangleF((float)drawOffsetX, (float)drawOffsetY, img.Width, img.Height), Portion))
			{
				RotateMatrix rm = RenderHelper.Rotate(angle, centerX, centerY, img.Width, img.Height, scaleX, scaleY, shearX, shearY, drawOffsetX, drawOffsetY);
				lock (BufferImageLock)
				{
					img.zIndex = RenderIndex;
					RenderHelper.DrawImage(ref BufferImage, img, rm);
				}
			}
		}
		protected void DrawString(string text, Font font, Brush brush, double angle, double drawOffsetX, double drawOffsetY)
		{
			Graphics g = Graphics.FromImage(BufferImage.Source);
			SizeF sz = g.MeasureString(text, font);
			if (RenderHelper.IsRectOverRect(new RectangleF((float)drawOffsetX, (float)drawOffsetY, sz.Width, sz.Height), Portion))
			{
				RotateMatrix rm = RenderHelper.Rotate(angle, sz.Width / 2, sz.Height / 2, sz.Width, sz.Height, 1, 1, 0, 0, drawOffsetX, drawOffsetY);
				lock (BufferImageLock)
				{
					RenderHelper.DrawString(ref BufferImage, text, sz, font, brush, rm);
					//Image imgstr = new Bitmap((int)sz.Width, (int)sz.Height);
					//Graphics gstr = Graphics.FromImage(imgstr);
					//gstr.DrawString(text, font, brush, new PointF(0, 0));
					//RenderHelper.DrawImage(ref BufferImage, imgstr, rm);
				}
			}
			g.Dispose();
		}
		protected void UpdatePortion()
		{
			UpdatePortion(Portion.X, Portion.Y, Width, Height);
		}
		protected void UpdatePortion(float x, float y, float width, float height)
		{
			if (Portion == null)
			{
				Portion = new RectangleF(x, y, width, height);
			}
			else
			{
				Portion.Location = new PointF(x, y);
				Portion.Size = new SizeF(width, height);
			}
		}
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			if (!IsPainted)
			{
				//e.Graphics.Clear(BackColor);
				IsPainted = true;
			}
		}
		protected virtual void Run() { }
		protected virtual void OnRenderFrameUpdate() { }
	}
}
