using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
            Topmost = true;
            ckFile.IsChecked = true;
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

        int base64type = 1;
        int urltype = 0;
        int encrypt = 2;

        private void Encode()
        {
            if (encType.SelectedIndex == urltype)
            {
                box.Text = HttpUtility.UrlEncode(box.Text);
            }
            else if (encType.SelectedIndex == base64type)
            {
                box.Text = box.Text.Base64Encode();
            }
            else
            {
                box.Text = box.Text.AESEncrypt("k:;2n");
            }
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "encoded.txt", box.Text);
        }
        
        private void Decode()
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "predecoded.txt", box.Text);
            if (encType.SelectedIndex == urltype)
            {
                if (ckFile.IsChecked.Value)
                {
                    string b = HttpUtility.UrlDecode(box.Text);
                    b = b.Replace(' ', '+');
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "decoded.txt", b);
                    Save2File(b);
                }
                else
                {
                    box.Text = HttpUtility.UrlDecode(box.Text);
                }

            }
            else if (encType.SelectedIndex == base64type)
            {
                if (ckFile.IsChecked.Value)
                {
                    byte[] rlt = box.Text.Base64DecodeByteArray();
                    Save2File(rlt);
                }
                else
                {
                    string rlt = box.Text.Base64DecodeString();
                    box.Text = rlt;
                }
            }
            else
            {
                if (ckFile.IsChecked.Value)
                {
                    string rlt = box.Text.AESDecrypt("k:;2n");
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "decoded.txt", rlt);
                    Save2File(rlt);
                }
                else
                {
                    box.Text = box.Text.AESDecrypt("k:;2n");
                }
            }
        }

        private static void Save2File(string content)
        {
            byte[] rlt = Convert.FromBase64String(content);
            Save2File(rlt);
        }

        private static void Save2File(byte[] bytes)
        {
            File.WriteAllBytes(GetOutputName(), bytes);
        }

        private static string GetOutputName()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "output.zip";
        }



        private void Window_Drop_File(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] droppedFilePaths =
                    e.Data.GetData(DataFormats.FileDrop, true) as string[];
                box.Text = string.Empty;
                foreach (string droppedFilePath in droppedFilePaths)
                {
                    byte[] bytes = File.ReadAllBytes(droppedFilePath);
                    box.Text = Convert.ToBase64String(bytes);
                    Clipboard.SetData(DataFormats.Text, box.Text);
                }
                Encode();
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "encoded.txt", box.Text);
            }
        }
    }
}
