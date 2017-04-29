using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows;

namespace Demo.Mef.Extra
{
    // Mark the class as export with the contract Demo.Mef.MenuItems 
    // so it can be matched with the contract in the main app(), this could be used as a filter
    [Export("Demo.Mef.MenuItems", typeof(Button))]
    // This attribute makes MEF create a new instance each time the button is requested
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MessageBoxButton : Button
    {
        public MessageBoxButton()
        {
            this.Content = "PresssMeee";
            this.Click += new System.Windows.RoutedEventHandler(MessageBoxButton_Click);
        }

        void MessageBoxButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("Yes this demo also has Hello world!");
        }
    }
}
