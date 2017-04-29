using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Joy.Core;
using Joy.Core.Encode;

namespace Joy.Windows
{
    public class Wall
    {
        public static string CaptureScreenSec(string Pwd)
        {
            using (var ms = new MemoryStream())
            {
                Wall.CaptureScreen(30, ms);
                var bytes = ms.ToArray();
                var s = Convert.ToBase64String(bytes);
                if (!string.IsNullOrEmpty(s))
                {
                    s = OpenSSL.OpenSSLEncrypt(s, Pwd);
                    s = s.UrlEncode();
                }
                return s;
            }
        }

        public static Bitmap CaptureScreen(int quality = 100, Stream sm = null)
        {
            try
            {
                var totalSize = GetSize();

                var bmp = new Bitmap(totalSize.Width, totalSize.Height,
                    PixelFormat.Format32bppArgb);

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(totalSize.X, totalSize.Y,
                        0, 0, totalSize.Size, CopyPixelOperation.SourceCopy);
                }

                if (quality != 100)
                {
                    bmp = bmp.AdjustQuality(quality);
                }

                if (sm != null)
                {
                    bmp.Save(sm, ImageFormat.Jpeg);
                    bmp.Dispose();
                    bmp = null;
                }
                return bmp;
            }
            catch (Exception ex)
            {
                Error.Handle(ex);
                return null;
            }
        }

        private static Rectangle GetSize()
        {
            Rectangle totalSize = Rectangle.Empty;

            foreach (Screen s in Screen.AllScreens)
                totalSize = Rectangle.Union(totalSize, s.Bounds);
            return totalSize;
        }
    }
}
