using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Collections;

namespace ULib.Controls
{
    /// <summary>
    /// Interaction logic for LabelTextControl.xaml
    /// </summary>
    public partial class LabelTextControl : UserControl
    {
        public delegate void ContentChangeDelegate();
        public event ContentChangeDelegate OnContentChanged;
        protected object TargetObject;
        protected FieldInfo info;

        public LabelTextControl(object target, string propertyName)
        {

            TargetObject = target;
            FieldInfo[] list = target.GetType().GetFields();
            foreach (FieldInfo i in list)
            {
                if (string.Equals(i.Name, propertyName, StringComparison.Ordinal))
                {
                    info = i;
                    break;
                }
            }

            txt.TextChanged += new TextChangedEventHandler(txt_TextChanged);
            pwd.PasswordChanged += new RoutedEventHandler(pwd_PasswordChanged);

        }

        private void txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            NotifyContentChange(txt.Text);
        }

        private void pwd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            NotifyContentChange(pwd.Password);
        }

        private void NotifyContentChange(string content)
        {
            if (info != null && TargetObject != null)
            {
                info.SetValue(TargetObject, content);
            }
            if (OnContentChanged != null)
            {
                OnContentChanged();
            }
        }

        public void SetContent(string label, string value, bool isPassword = false)
        {
            lab.Content = label;
            if (value == null && TargetObject != null)
            {
                object v = info.GetValue(TargetObject);
                if (v != null)
                {
                    value = v.ToString();
                }
            }
            txt.Text = value;
            pwd.Password = value;
            pwd.PasswordChar = '*';
            if (isPassword)
            {
                txt.Visibility = System.Windows.Visibility.Hidden;
                pwd.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                txt.Visibility = System.Windows.Visibility.Visible;
                pwd.Visibility = System.Windows.Visibility.Hidden;
            }
        }
    }
}
