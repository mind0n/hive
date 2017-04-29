using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsInput;

namespace Joy.Windows.Events
{
    [Serializable]
    public class MsEvt : Evt
    {
        public MsEvt(int w = 0, int h = 0)
        {
            if (w == 0)
            {
                w = Screen.PrimaryScreen.Bounds.Right;
            }
            if (h == 0)
            {
                h = Screen.PrimaryScreen.Bounds.Bottom;
            }
            total = new Size(w, h);
        }

        protected Size total;
        public double X;
        public double Y;
        public MouseButton Button;
        public EvtState State;
        public int dlt;
        protected int cw;
        protected int ch;

        public override void Parse(EvtItem item)
        {
            X = item.x;
            Y = item.y;
            cw = item.cw;
            ch = item.ch;
            dlt = item.delta;
            //MouseButton.TryParse(item.btn.ToString(), out Button);
            if (item.btn == 0)
            {
                Button = MouseButton.LeftButton;
            }
            else if (item.btn == 1)
            {
                Button = MouseButton.MiddleButton;
            }
            else if (item.btn == 2)
            {
                Button = MouseButton.RightButton;
            }
        }

        public override void Send()
        {
            var m = sim.Mouse;
            var rx = X/cw*total.Width;
            var ry = Y/ch*total.Height;
            var l = rx/total.Width;
            var t = ry/total.Height;
            m.MoveMouseTo(l*65535, t*65535);
            if (State == EvtState.Wheel)
            {
                sim.Mouse.VerticalScroll(dlt);
            }
            else
            {
                sim.Mouse.XButtonState(Button, State == EvtState.Down);
            }
        }
    }
}
