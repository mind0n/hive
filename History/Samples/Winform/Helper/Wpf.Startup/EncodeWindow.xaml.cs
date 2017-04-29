using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ULib.Core;
using ULib.Winform.Exceptions;

namespace Wpf.Startup
{
    /// <summary>
    /// Interaction logic for EncodeWindow.xaml
    /// </summary>
    public partial class EncodeWindow : Window
    {
        public EncodeWindow()
        {
            InitializeComponent();
            AllowDrop = true;
            bEncode.PreviewMouseUp += bEncode_PreviewMouseUp;
            box.AllowDrop = true;
        }

        void bEncode_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    Encode();
                }
                else if (e.ChangedButton == MouseButton.Right)
                {
                    Decode();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Handle(ex);
            }
        }

        private void Decode()
        {
            byte[] rlt = null;
            if (encType.SelectedIndex == 0)
            {
                rlt = box.Text.Base64DecodeByteArray();
                File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "output.zip", rlt);
            }
            else
            {
                box.Text = box.Text.AESDecrypt("k:;2n");
            }
        }

        private void Encode()
        {
            if (encType.SelectedIndex == 0)
            {
                box.Text = box.Text.Base64Encode();
            }
            else
            {
                box.Text = box.Text.AESEncrypt("k:;2n");
            }
        }

        private void Window_Drop_1(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] droppedFilePaths =
                    e.Data.GetData(DataFormats.FileDrop, true) as string[];

                foreach (string droppedFilePath in droppedFilePaths)
                {
                    byte[] bytes = File.ReadAllBytes(droppedFilePath);
                    box.Text = Convert.ToBase64String(bytes);
                }
            }
        }
    }
}
