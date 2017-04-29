using System;
using System.Collections.Generic;
using WindowsInput;
using Joy.Core;
using System.Threading;

namespace Joy.Windows.Events
{
    public class EvtFactory
    {
        private static Dictionary<EvtAction, object> evtmappings = new Dictionary<EvtAction, object>();

        static EvtFactory()
        {
            evtmappings[EvtAction.MouseDown] = new MsEvt { State = EvtState.Down };
            evtmappings[EvtAction.MouseUp] = new MsEvt { State = EvtState.Up };
            evtmappings[EvtAction.MouseWheel] = new MsEvt { State = EvtState.Wheel };
            evtmappings[EvtAction.KeyDown] = new KbEvt { State = EvtState.Down };
            evtmappings[EvtAction.KeyUp] = new KbEvt { State = EvtState.Up };
        }

        public void ParseEvents(List<EvtItem> list)
        {
            foreach (var i in list)
            {
                var evt = evtmappings[i.name].Clone<Evt>();
                evt.Parse(i);
                evt.Send();
            }
        }

        public void ParseEvent(string json)
        {
            var i = json.FromJson<EvtItem>();
            var evt = evtmappings[i.name].Clone<Evt>();
            evt.Parse(i);
            evt.Send();
        }

        public void ParseEvents(string json)
        {
            var list = json.FromJson<List<EvtItem>>();
            foreach (var i in list)
            {
                var evt = evtmappings[i.name].Clone<Evt>();
                evt.Parse(i);
                evt.Send();
            }
            //var th = new Thread(new ThreadStart(delegate
            //{
            //    var list = json.FromJson<List<EvtItem>>();
            //    foreach (var i in list)
            //    {
            //        var evt = evtmappings[i.name].Clone<Evt>();
            //        evt.Parse(i);
            //        evt.Send();
            //        Thread.Sleep(1);
            //    }
            //}));
            //th.IsBackground = true;
            //th.Start();
        }
    }

    public class EvtItem
    {
        public EvtAction name;
        public int btn;
        public int x;
        public int y;
        public string key;
        public int cw;
        public int ch;
        public int delta;
        public string[] modifier;
    }

    [Serializable]
    public abstract class Evt
    {
        protected static InputSimulator sim = new InputSimulator();
        public abstract void Parse(EvtItem item);
        public abstract void Send();
    }

    public enum EvtState
    {
        Up,
        Down,
        Wheel
    }

    public enum EvtAction
    {
        KeyDown,
        KeyUp,
        MouseDown,
        MouseUp,
        MouseWheel
    }
}
