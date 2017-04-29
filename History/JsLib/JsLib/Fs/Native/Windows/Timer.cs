using System;
using System.Collections.Generic;
using System.Text;

namespace Fs.Native.Windows
{
	//public class ParamTimer
	//{
	//    public delegate bool ElapsedHandler(ParamTimer sender, DateTime elapsed);
	//    public ElapsedHandler TimerOnElapsed;
	//    public double Interval;
	//    public object[] Parameters;
	//    protected System.Timers.Timer tmr;
	//    public ParamTimer(double interval, params object[] parlist)
	//    {
	//        Interval = interval;
	//        tmr = new System.Timers.Timer();
	//        tmr.Enabled = true;
	//        tmr.Elapsed += new System.Timers.ElapsedEventHandler(tmr_Elapsed);
	//        Parameters = parlist;
	//    }
	//    public void Start()
	//    {
	//        Start(null);
	//    }
	//    public void Start(params object[] parlist)
	//    {
	//        if (parlist != null)
	//        {
	//            Parameters = parlist;
	//        }
	//        tmr.Start();
	//    }
	//    public void Pause()
	//    {
	//        tmr.Stop();
	//    }
	//    public void Stop()
	//    {
	//        Pause();
	//        Parameters = null;
	//    }
	//    void tmr_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
	//    {
	//        tmr.Stop();
	//        if (TimerOnElapsed != null)
	//        {
	//            if (!TimerOnElapsed(this, e.SignalTime))
	//            {
	//                return;
	//            }
	//            else
	//            {
	//                tmr.Start();
	//            }
	//        }
	//    }
	//}
}
