using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Joy.Server.Entities
{
	public class DataObjectAttribute : Attribute
	{
		private bool visible = true;

		public bool Visible
		{
			get { return visible; }
			set { visible = value; }
		}
		private string caption = string.Empty;

		public string Caption
		{
			get { return caption; }
			set { caption = value; }
		}
		private int? order;

		public int? Order
		{
			get { return order; }
			set { order = value; }
		}

	}
}
