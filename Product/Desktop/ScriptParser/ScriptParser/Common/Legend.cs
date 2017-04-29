using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptParser.Common
{
    public enum TokenType
    {
        Unknown,
		Letter,
        String,
        StringBegin,
        StringEnd,
        Comments,
        CommentBegin,
        CommentEnd,
        Number,
        Operator,
		WhiteSpace,
        Variable,
        Keyword,
        Ignore
    }
    public class Legend
    {
        public char Content;
        public TokenType Type = TokenType.Unknown;
    }
    public class Cache<T> where T:new()
    {
        public delegate bool ShiftHandler(T item);
        public event ShiftHandler OnShiftLeft;
        protected T[] storage;
        protected int head = 0;
        protected int pos = 0;
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= storage.Length)
                {
                    throw new IndexOutOfRangeException(string.Format("Index {1} outside cache size {0}", index, storage.Length));
                }
                int realIndex = (head + index) % storage.Length;
                if (realIndex >= 0 && realIndex < storage.Length)
                {
                    return storage[realIndex];
                }
                throw new IndexOutOfRangeException(string.Format("Real index {1} outside cache size {0}", realIndex, storage.Length));
            }
            set
            {
                if (index < 0 || index >= storage.Length)
                {
                    throw new IndexOutOfRangeException(string.Format("Index {1} outside cache size {0}", index, storage.Length));
                }
                int realIndex = (head + index) % storage.Length;
                if (realIndex >= 0 && realIndex < storage.Length)
                {
                    storage[realIndex] = value;
                }
            }
        }
        public Cache(int size = 32)
        {
            if (size < 1)
            {
                size = 32;
            }
            storage = new T[size];
            if (storage != null)
            {
                //for (int i = 0; i < size; i++)
                //{
                //    storage[i] = new T();
                //}
                head = 0;
                pos = 0;
            }
        }
        public virtual void Add(T item)
        {
            storage[pos] = item;
            pos++;
            pos %= storage.Length;
			if (pos == head)
			{
				ShiftLeft();
			}
        }
        public virtual void ShiftLeft()
        {
			if (OnShiftLeft != null)
			{
				OnShiftLeft(this[0]);
			}
            head++;
            head %= storage.Length;
        }
    }
}
