using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Joy.Server.Entities
{
	//public enum MessageStatus : byte
	//{
	//    NotProcessed,
	//    Processing,
	//    Processed
	//}
	//public class Message
	//{
	//    public bool IsValid
	//    {
	//        get
	//        {
	//            return vProcessor != null;
	//        }
	//    }
	//    public MessageStatus Status
	//    {
	//        get
	//        {
	//            return vStatus;
	//        }
	//    }protected MessageStatus vStatus = MessageStatus.NotProcessed;
	//    public bool HasParameters
	//    {
	//        get
	//        {
	//            return (Parameters != null && Parameters.Keys.Count > 0);
	//        }
	//    }
	//    public Dictionary<string, object> Parameters;
	//    protected MessageQueue.MessageDelegate vProcessor;
	//    public Message(MessageQueue.MessageDelegate processor, Dictionary<string, object> paramlist)
	//    {
	//        vProcessor = processor;
	//        Parameters = paramlist;
	//    }
	//    public void Process(MessageQueue msgQueue)
	//    {
	//        if (IsValid)
	//        {
	//            vStatus = MessageStatus.Processing;
	//            vProcessor(msgQueue, this);
	//        }
	//    }
	//}
	//public class MessageQueue
	//{
	//    public delegate void MessageDelegate(MessageQueue msgObj, Message msg);
	//    public MessageDelegate MessageOnPeek;
	//    public List<Message> Messages;
	//    public double Interval = 32;
	//    public bool HasMessage
	//    {
	//        get
	//        {
	//            return vPos >= 0 && vPos < Messages.Count;
	//        }
	//    }
	//    protected Timer tmr;
	//    protected int vPos;
	//    public MessageQueue()
	//    {
	//        Messages = new List<Message>();
	//        vPos = -1;
	//        tmr = new Timer();
	//        tmr.Interval = Interval;
	//        tmr.Elapsed += new ElapsedEventHandler(tmr_Elapsed);
	//        tmr.Enabled = false;
	//    }
	//    public Message PeekMessage()
	//    {
	//        if (HasMessage)
	//        {
	//            Message msg = Messages[vPos];
	//            while (msg.Status == MessageStatus.Processing)
	//            {
	//                continue;
	//            }

	//            if (msg.Status == MessageStatus.Processed)
	//            {
	//                Messages.Remove(msg);
	//            }
	//            if (msg.Status == MessageStatus.NotProcessed)
	//            {
	//                return msg;
	//            }
	//            return null;
	//        }
	//        return null;
	//    }
	//    public void ProcessMessage(Message msg)
	//    {
	//        if (msg != null)
	//        {
	//            msg.Process(this);
	//        }
	//    }
	//    public void Add(MessageDelegate processor)
	//    {
	//        Message m = new Message(processor, null);
	//        Messages.Add(m);
	//    }
	//    public void Start()
	//    {
	//        tmr.Enabled = true;
	//        tmr.Start();
	//    }
	//    public void Stop()
	//    {
	//        tmr.Enabled = false;
	//        tmr.Stop();
	//    }
	//    void tmr_Elapsed(object sender, ElapsedEventArgs e)
	//    {
	//        tmr.Stop();
	//        ProcessMessage(PeekMessage());
	//        tmr.Start();
	//    }
	//}
}
