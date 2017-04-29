using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Fs.UI.Controls
{
	public partial class BasicForm : Form
	{

		public bool IsDragging { get; set; }
		public bool Resizable { get; set; }
		public Point ClickedPoint { get; set; }
		public int BorderWidth { get; set; }
		public int TitlebarHeight { get; set; }
		
		private const int WM_NCHITTEST = 0x84;
		private const int HTCLIENT = 0x1;
		private const int HTCAPTION = 0x2;
		
		public BasicForm()
		{
			Version v = System.Environment.Version;

			if (v.Major < 2)
			{
				this.SetStyle(ControlStyles.DoubleBuffer, true);
			}
			else
			{
				this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			}

			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.DoubleBuffered = true;
			InitializeComponent();
			
			Text = "";
			Resizable = true;
			ControlBox = false;
			StartPosition = FormStartPosition.CenterScreen;
			FormBorderStyle = FormBorderStyle.None;
			Paint += new PaintEventHandler(BasicForm_Paint);
			ParamUpdate();
		}
		public void EventHandler(Control ctrl)
		{
			ctrl.MouseMove += new MouseEventHandler(OnMouseMove);
			ctrl.MouseDown += new MouseEventHandler(OnMouseDown);
			ctrl.MouseUp +=new MouseEventHandler(OnMouseUp);
			ctrl.MouseDoubleClick += new MouseEventHandler(OnMouseDoubleClick);
		}
		public void EmbedInto(Panel target, DockStyle dock)
		{
			Resizable = false;
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			TopLevel = false;
			target.Controls.Add(this);
			Dock = dock;
			Show();
		}
		protected void ParamUpdate()
		{
			BorderWidth = (this.Width - this.ClientSize.Width) / 2;
			TitlebarHeight = this.Height - this.ClientSize.Height - BorderWidth * 2;
		}
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			
			#region Move without Form Border
			if (m.Msg == WM_NCHITTEST)
			{
				this.DefWndProc(ref m);
				if (m.Result.ToInt32() == HTCLIENT)
					m.Result = new IntPtr(HTCAPTION);
			}

			#endregion

			#region Resize without Form Border

			if (m.Msg == WM_NCHITTEST && this.FormBorderStyle == FormBorderStyle.None)
			{
				Point mPosRel = Control.MousePosition;
				if (mPosRel != null)
				{
					mPosRel.X -= Location.X;
					mPosRel.Y -= Location.Y;
				}

				Padding resizePadding = new Padding(8);
				int result = 0;
				if (mPosRel.X < (resizePadding.Left)) result = 10;
				else if (mPosRel.X > (Width - resizePadding.Right)) result = 11;

				if (mPosRel.Y < (resizePadding.Top))
				{
					switch (result)
					{
						case 0:
							result = 12;
							break;
						case 10:
							result = 13;
							break;
						case 11:
							result = 14;
							break;
						default:
							break;
					}
				}
				else if (mPosRel.Y > (Height - resizePadding.Bottom))
				{
					switch (result)
					{
						case 0:
							result = 15;
							break;
						case 10:
							result = 16;
							break;
						case 11:
							result = 17;
							break;
						default:
							break;
					}
				}

				if (result != 0) m.Result = (IntPtr)result;
			}

			#endregion

		}
		protected void OnMouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (Resizable)
			{
				WindowState = (WindowState == FormWindowState.Maximized) ? FormWindowState.Normal : FormWindowState.Maximized;
			}
		}
		protected void OnMouseDown(object sender, MouseEventArgs e)
		{
			this.IsDragging = true;
			this.ClickedPoint = new Point(e.X, e.Y);
			base.OnMouseDown(e);
		}
		protected void OnMouseUp(object sender, MouseEventArgs e)
		{
			this.IsDragging = false;
			base.OnMouseUp(e);
		}
		protected void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (this.IsDragging)
			{
				Point NewPoint;
				NewPoint = this.PointToScreen(new Point(e.X, e.Y));
				//The new point is relative to the original point
				NewPoint.Offset((this.ClickedPoint.X + BorderWidth) * -1,
							(this.ClickedPoint.Y + BorderWidth) * -1);
				//Finally, assign the form's location to the              
				//determined new point
				this.Location = NewPoint;
			}
			base.OnMouseMove(e);
		}
		void BasicForm_Paint(object sender, PaintEventArgs e)
		{
			ParamUpdate();
			//Rectangle borderRectangle = this.ClientRectangle;
			//borderRectangle.Inflate(-10, -10);
			//ControlPaint.DrawBorder3D(e.Graphics, borderRectangle, Border3DStyle.Flat);
		}

		//protected void UpdateRegion()
		//{
		//    Region = new Region(new Rectangle(0, Height - ClientSize.Height, ClientSize.Width, ClientSize.Height));
		//}


	}
}
