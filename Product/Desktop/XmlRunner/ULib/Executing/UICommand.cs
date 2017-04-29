using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using System.Windows.Forms;
using ULib.Controls;

namespace ULib.Executing
{
	public class UICommandNode : CommandNode
	{
		protected Dict<string, Control> controls = new Dict<string, Control>();
		public virtual void OnGenerate(Label label, Control editor)
		{
			if (label != null && editor != null)
			{
				controls[label.Text.ToLower()] = editor;
			}
		}
		public virtual void OnGenerateCompleted(AutoSettingPanel sender)
		{
		}
		protected T Get<T>(string key) where T : class
		{
			if (controls.ContainsKey(key))
			{
				return controls[key] as T;
			}
			return default(T);
		}
	}
}
