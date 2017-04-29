using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Portal.Winform.Controls
{
    [ComVisible(true)]
    public class WebExternal
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void SimulateMouseEvent(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        private const int MOUSE_LEFTDOWN = 0x02;
        private const int MOUSE_LEFTUP = 0x04;
        private const int MOUSE_RIGHTDOWN = 0x08;
        private const int MOUSE_RIGHTUP = 0x10;

        protected WebForm Form;
        public WebExternal(WebForm form)
        {
            Form = form;
        }

        public object GetProcessor(string name)
        {
            return Form.GetProcessor(name);
        }

        public void CaptionMouseUp()
        {
            try
            {
                SimulateMouseEvent(MOUSE_LEFTUP, 0, 0, 0, 0);
            }
            catch
            {
                
            }
        }

        public void CaptionMouseDown()
        {
            ReleaseCapture();
            Message msg = Message.Create(Form.Handle, WM_NCLBUTTONDOWN, (IntPtr)2, IntPtr.Zero);
            Form.Wp(ref msg);
        }

        public void ResizerMouseDown()
        {
            ReleaseCapture();
            Message msg = Message.Create(Form.Handle, WM_NCLBUTTONDOWN, (IntPtr)17, IntPtr.Zero);
            Form.Wp(ref msg);
        }

        public void MaxForm()
        {
            Form.WindowState = Form.WindowState == FormWindowState.Maximized
                ? FormWindowState.Normal
                : FormWindowState.Maximized;
        }

        public void CloseForm()
        {
            Form.Close();
        }

        public void MinForm()
        {
            Form.WindowState = FormWindowState.Minimized;
        }
    }
}
