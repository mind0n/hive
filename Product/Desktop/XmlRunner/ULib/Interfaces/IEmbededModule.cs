using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ULib
{
	public interface IEmbededModule
	{
		void EmbedInto(Control parent);
	}
}
