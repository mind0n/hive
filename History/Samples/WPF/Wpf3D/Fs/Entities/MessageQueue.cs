using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Fs.Native.Windows;

namespace Fs.Entities
{
	public enum MessageStatus : byte
	{
		NotProcessed,
		ParallelProcessing,
		Processing,
		Processed,
		Canceled
	}
	public class Message
	{
		public virtual bool IsValid
		{
			get
			{
				return vProcessor != null;
			}
		}
		public MessageStatus Status
		{
			get
			{
				return vStatus;
			}
		}protected MessageStatus vStatus = MessageStatus.NotProcessed;
		public bool HasParameters
		{
			get
			{
				return (Parameters != null && Parameters.Keys.Count > 0);
			}
		}
		public bool IsParallel = false;
		public int Priorty;
		public DictParams Parameters;
		protected MessageQueue.MessageDelegate vProcessor;

		public Message(MessageQueue.MessageDelegate processor, DictParams paramlist, int priority, bool parallel)
		{
			Init(processor, paramlist, priority, parallel);
		}
		protected void Init(MessageQueue.MessageDelegate processor, DictParams paramlist, int priority, bool parallel)
		{
			vProcessor = processor;
			Parameters = paramlist;
			IsParallel = parallel;
			Priorty = priority;
			
		}
		public virtual void Process(MessageQueue msgQueue)
		{
			if (IsValid)
			{
				if (IsParallel)
				{
					vStatus = MessageStatus.ParallelProcessing;
				}
				else
				{
					vStatus = MessageStatus.Processing;
				}
				vProcessor(msgQueue, this);
			}
		}
		public virtual void Release()
		{
			vStatus = MessageStatus.Processed;
		}
	}
	public class FrequentMessage : Message
	{
		public event ElapsedEventHandler elapsedProcessor;
		public double Interval
		{
			get
			{
				return vInterval;
			}
		}
		public override bool IsValid
		{
			get
			{
				return elapsedProcessor != null;
			}
		}
		protected ParamTimer msgTimer;
		protected double vInterval = 32;
		public FrequentMessage(ElapsedEventHandler processor, DictParams paramlist, int priority, bool parallel) : base(null, paramlist, priority, parallel)
		{
			elapsedProcessor = processor;
			msgTimer = new ParamTimer(vInterval);
			msgTimer.Param = this;
			msgTimer.DictParams = paramlist;
			msgTimer.Enabled = false;
		}

		public void Process()
		{
			if (IsValid)
			{
				if (IsParallel)
				{
					vStatus = MessageStatus.ParallelProcessing;
				}
				else
				{
					vStatus = MessageStatus.Processing;
				}
				if (elapsedProcessor != null)
				{
					msgTimer.Elapsed += new ElapsedEventHandler(elapsedProcessor);
					msgTimer.Enabled = true;
					msgTimer.Param = this;
					msgTimer.DictParams = Parameters;
					msgTimer.Start();
				}
			}
		}
		public override void Release()
		{
			msgTimer.Stop();
			msgTimer.Enabled = false;
			vStatus = MessageStatus.Processed;
			base.Release();
		}
		public virtual void Cancel()
		{
			Release();
			vStatus = vStatus | MessageStatus.Canceled;
		}
	}
	public class MessageQueue
	{
		public delegate void MessageDelegate(MessageQueue msgObj, Message msg);
		public MessageDelegate MessageOnPeek;
		public List<Message> Messages;
		public double Interval = 100;
		public bool HasMessage
		{
			get
			{
				return Messages != null && Messages.Count > 0;
			}
		}
		protected Timer msgLoopTimer;
		protected int vPos;
		public MessageQueue()
		{
			Messages = new List<Message>();
			vPos = -1;
			msgLoopTimer = new Timer();
			msgLoopTimer.Interval = Interval;
			msgLoopTimer.Elapsed += new ElapsedEventHandler(msgLoopTimer_Elapsed);
			msgLoopTimer.Enabled = false;
		}
		public void Start()
		{
			msgLoopTimer.Enabled = true;
			msgLoopTimer.Start();
		}
		public void Stop()
		{
			msgLoopTimer.Enabled = false;
			msgLoopTimer.Stop();
		}
		public void Wait(Message msg)
		{
			while (msg.Status != MessageStatus.Processed)
			{
				System.Threading.Thread.Sleep(100);
				continue;
			}
		}
		protected Message PeekMessage()
		{
			if (HasMessage)
			{
				Message msg = Messages[0];
				while (msg.Status == MessageStatus.Processing)
				{
					continue;
				}

				if (msg.Status == MessageStatus.Processed || msg.Status == MessageStatus.ParallelProcessing)
				{
					Messages.Remove(msg);
				}
				if (msg.Status == MessageStatus.NotProcessed)
				{
					return msg;
				}
				return null;
			}
			return null;
		}
		protected virtual Message Add(Message msg)
		{
			Messages.Add(msg);
			return msg;
		}
		protected virtual Message Add(MessageDelegate processor, DictParams parameters)
		{
			return Add(processor, parameters, 0, false);
		}
		protected virtual Message Add(MessageDelegate processor, DictParams parameters, bool isParallel)
		{
			return Add(processor, parameters, 0, isParallel);
		}
		protected virtual Message Add(MessageDelegate processor, DictParams parameters, int priority, bool isParallel)
		{
			Message m = new Message(processor, parameters, priority, isParallel);
			int i = GetIndex(priority);
			if (i < 0)
			{
				Messages.Add(m);
			}
			else
			{
				Messages.Insert(i, m);
			}
			return m;
		}
		protected virtual FrequentMessage Add(ElapsedEventHandler processor, DictParams parameters, int priority, bool isParallel)
		{
			FrequentMessage m = new FrequentMessage(processor, parameters, priority, isParallel);
			int i = GetIndex(priority);
			if (i < 0)
			{
				Messages.Add(m);
			}
			else
			{
				Messages.Insert(i, m);
			}
			return m;
		}
		protected virtual FrequentMessage Create(ElapsedEventHandler processor, DictParams parameters, int priority, bool isParallel)
		{
			FrequentMessage m = new FrequentMessage(processor, parameters, priority, isParallel);
			return m;
		}
		protected int GetIndex(int priority)
		{
			for (int i = 0; i < Messages.Count; i++)
			{
				if (Messages[i].Priorty < priority)
				{
					return i;
				}
			}
			return -1;
		}
		protected void ProcessMessage(Message msg)
		{
			if (msg != null)
			{
				msg.Process(this);
			}
		}
		protected void msgLoopTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			msgLoopTimer.Stop();
			ProcessMessage(PeekMessage());
			msgLoopTimer.Start();
		}
	}
}
