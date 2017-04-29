using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptEngine.Core
{
    public abstract class BufferedReader<TBuffer, TContent> where TBuffer:new()
    {
		public event Action<BufferedReader<TBuffer, TContent>, TBuffer[]> BufferFullCallback;
		public event Action<BufferedReader<TBuffer, TContent>, TBuffer[]> BufferReadCallback;
		protected bool isStarted;
		protected bool isFinished;
		protected int readCount;
        protected int bufferSize;
        protected int p;
		protected TBuffer[] buffer;

		public TBuffer[] Buffer
		{
			get { return buffer; }
		}
        public BufferedReader(int buffersize)
        {
            bufferSize = buffersize;
			p = buffersize - 1;
			buffer = new TBuffer[bufferSize];
        }
		public virtual void BeginRead()
		{
			isStarted = true;
			isFinished = false;
		}

        public virtual void Read(TContent content)
        {
			if (p + 1 == buffer.Length)
			{
				ShiftLeft();
			}
			buffer[p] = CreateBufferUnit(content);
			if (BufferReadCallback != null)
			{
				BufferReadCallback(this, buffer);
			}
        }

        public virtual void Flush()
        {
            while (p >= 0)
            {
                ShiftLeft();
				buffer[p] = default(TBuffer);
				if (BufferReadCallback != null)
				{
					BufferReadCallback(this, buffer);
				}
                p--;
            }
			p = buffer.Length - 1;
        }

		public virtual void EndRead()
		{
			isStarted = false;
			isFinished = true;
		}
		public abstract TBuffer CreateBufferUnit(TContent content);
		public virtual void ShiftLeft()
		{
			VerifyBuffer();
			for (int i = 0; i < buffer.Length - 1; i++)
			{
				buffer[i] = buffer[i + 1];
			}
		}

		protected void VerifyBuffer()
		{
			if (!EqualityComparer<TBuffer>.Default.Equals(buffer[0], default(TBuffer)) || readCount > 0)
			{
				if (BufferFullCallback != null)
				{
					BufferFullCallback(this, buffer);
				}
			}
		}
    }
}
