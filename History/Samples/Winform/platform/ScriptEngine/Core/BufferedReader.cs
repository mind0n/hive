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
		private bool isFlushing;

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
			isFlushing = false;
			if (p + 1 == buffer.Length)
			{
				ShiftLeft();
			}
			else
			{
				p++;
			}
			buffer[p] = CreateBufferUnit(content);
			if (BufferReadCallback != null)
			{
				BufferReadCallback(this, buffer);
			}
        }

        public virtual void Flush()
        {
			isFlushing = true;
            while (p >= 0)
            {
                ShiftLeft();
				buffer[p] = default(TBuffer);
				if (BufferReadCallback != null)
				{
					BufferReadCallback(this, buffer);
				}
                p--;
				if (p < 0 && BufferFullCallback != null)
				{
					BufferFullCallback(this, buffer);
				}
			}
			p = buffer.Length - 1;
        }

		public void RemoveIgnore(int index)
		{
			int i;
			for (i = index; i < buffer.Length - 1; i++)
			{
				buffer[i] = buffer[i + 1];
			}
			buffer[buffer.Length - 1] = default(TBuffer);
			p--;
			if (BufferReadCallback != null)
			{
				BufferReadCallback(this, buffer);
			}
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
				if (isFlushing && buffer[i] == null)
				{
					break;
				}
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
