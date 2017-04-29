using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Fs.UI.Controls
{
	public partial class EventCombo : ComboBox
	{
		public delegate void UpdateEventHandler(object item);
		public UpdateEventHandler ItemOnUpdate;
		public EventCombo()
		{
			InitializeComponent();
			
		}
		public void Select(int index)
		{
			SelectedIndex = index;
			Update();
		}
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			Update();
			base.OnSelectedIndexChanged(e);
		}
		protected void Update()
		{
			if (ItemOnUpdate != null)
			{
				ItemOnUpdate(Items[SelectedIndex]);
			}
		}
	}
}
