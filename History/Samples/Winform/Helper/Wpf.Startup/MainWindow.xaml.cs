using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ULib.Core.NativeSystem;

namespace Wpf.Startup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Debug.WriteLine("******************************************");
            Native.KeyDown += Instance_KeyDown;
        }

        void Instance_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (KeyboardStatus.Alt && KeyboardStatus.Shift && e.KeyCode == Keys.O)
            {
                EncodeWindow ew = new EncodeWindow();
                ew.Show();
            }
            else if (KeyboardStatus.Alt && KeyboardStatus.Shift && e.KeyCode == Keys.End)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

    }
}
