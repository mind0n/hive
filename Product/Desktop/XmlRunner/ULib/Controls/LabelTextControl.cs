using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using ULib.Forms;
using System.Collections;

namespace ULib.Controls
{
    public partial class LabelTextControl : UserControl
    {
        public delegate void ContentChangeDelegate();
        public event ContentChangeDelegate OnContentChanged;
		public ConfigMode Mode;
        protected object TargetObject;
        protected FieldInfo info;
		protected string id;
		protected Type contentType;

        public LabelTextControl(object target, string fieldOrId, Type type = null, ConfigMode mode = ConfigMode.Object)
        {
			Mode = mode;
            TargetObject = target;
			contentType = type;
			if (mode == ConfigMode.Object)
			{
				FieldInfo[] list = target.GetType().GetFields();
				foreach (FieldInfo i in list)
				{
					if (string.Equals(i.Name, fieldOrId, StringComparison.Ordinal))
					{
						info = i;
						break;
					}
				}
			}
			else if (mode == ConfigMode.Dictionary)
			{
				id = fieldOrId;
			}

            InitializeComponent();

            txt.TextChanged += new EventHandler(txt_TextChanged);

        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            NotifyContentChange(txt.Text);
        }

        private void NotifyContentChange(string content)
        {
            if (info != null && TargetObject != null)
            {
				try
				{
					if (Mode == ConfigMode.Object)
					{
						info.SetValue(TargetObject, Convert.ChangeType(content, info.FieldType));
					}
					else
					{
						IDictionary d = (IDictionary)TargetObject;
						if (contentType != null)
						{
							d[id] = Convert.ChangeType(content, contentType);
						}
						else
						{
							d[id] = content;
						}
					}
				}
				catch { }
            }
            if (OnContentChanged != null)
            {
                OnContentChanged();
            }
        }

        public void SetContent(string label, string value, bool isPassword = false)
        {
            lab.Text = label;
            if (value == null && TargetObject != null)
            {
				if (Mode == ConfigMode.Object)
				{
					object v = info.GetValue(TargetObject);
					if (v != null)
					{
						value = v.ToString();
					}
				}
				else if (Mode == ConfigMode.Dictionary)
				{
					IDictionary d = (IDictionary)TargetObject;
					object o = d[id];
					Type type = o.GetType();
					PropertyInfo info = type.GetProperty("Value");
					if (o != null && info != null)
					{
						object realValue = info.GetValue(o, null);
						if (realValue != null)
						{
							value = realValue.ToString();
						}
					}
				}
            }
            txt.Text = value;
            if (isPassword)
            {
                txt.PasswordChar = '*';
            }
        }
    }
}
