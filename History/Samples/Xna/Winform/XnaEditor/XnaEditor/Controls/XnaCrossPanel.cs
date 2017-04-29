using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XnaPanel;

namespace XnaEditor.Controls
{
	public partial class XnaCrossPanel : UserControl
	{
		public IntPtr Lt
		{
			get
			{
				return this.spLeft.Panel1.IsHandleCreated ? this.spLeft.Panel1.Handle : IntPtr.Zero;
			}
		}
		public IntPtr Lb
		{
			get
			{
				return this.spLeft.Panel2.IsHandleCreated ? this.spLeft.Panel2.Handle : IntPtr.Zero;
			}
		}
		public IntPtr Rt
		{
			get
			{
				return this.spRight.Panel1.IsHandleCreated ? this.spRight.Panel1.Handle : IntPtr.Zero;
			}
		}
		public IntPtr Rb
		{
			get
			{
				return this.spRight.Panel2.IsHandleCreated ? this.spRight.Panel2.Handle : IntPtr.Zero;
			}
		}
		protected GameBase Glt, Glb, Grt, Grb;

		public XnaCrossPanel()
		{
			InitializeComponent();
			Load += new EventHandler(XnaCrossPanel_Load);
		}

		void XnaCrossPanel_Load(object sender, EventArgs e)
		{
			Glt = new GameBase(Lt);
			Glb = new GameBase(Lb);
			Grt = new GameBase(Rt);
			Grb = new GameBase(Rb);
			Glt.Launch();
			Glb.Launch();
			Grt.Launch();
			Grb.Launch();
		}

	}
}
