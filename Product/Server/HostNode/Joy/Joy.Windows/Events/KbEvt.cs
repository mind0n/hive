using System;
using System.Collections.Generic;
using WindowsInput.Native;

namespace Joy.Windows.Events
{
    [Serializable]
    public class KbEvt : Evt
    {
        public List<VirtualKeyCode> Modifiers = new List<VirtualKeyCode>();
        public EvtState State;
        public VirtualKeyCode Code;
        public override void Parse(EvtItem item)
        {
            //Modifiers = item.modifier;
            Code = ParseCode(item.key);
            if (item.modifier != null && item.modifier.Length > 0)
            {
                foreach (var i in item.modifier)
                {
                    var vc = ParseCode(i);
                    Modifiers.Add(vc);
                }
            }
        }

        private VirtualKeyCode ParseCode(string code)
        {
            var vc = VirtualKeyCode.APPS;
            VirtualKeyCode.TryParse(code, out vc);
            if (vc == 0)
            {
                VirtualKeyCode.TryParse("VK_" + code, out vc);
            }
            return vc;
        }

        public override void Send()
        {
            if (State == EvtState.Down)
            {
                if (Modifiers.Count > 0)
                {
                    sim.Keyboard.ModifiedKeyStroke(Modifiers, Code);
                }
                else
                {
                    sim.Keyboard.KeyDown(Code);
                }
            }
            else
            {
                sim.Keyboard.KeyUp(Code);
            }
        }
    }
}
