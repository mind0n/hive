using System;
using System.Diagnostics;
using ScriptEngine.Debug;

namespace ScriptEngine.Core
{
    public abstract class Condition<T>
    {
        public delegate bool ValidateCallbackHandler(T[] buffer, T prev, int index, bool ismatch);
        public event ValidateCallbackHandler ValidateCallback;
        public abstract bool Validate(T[] buffer, int index);
        public Role<T> Role;
        public bool NotifyCallback(T[] buffer, T prev, int index, bool isMatch)
        {
            if (ValidateCallback != null)
            {
                return ValidateCallback(buffer, prev, index, isMatch);
            }
            return true;
        }
		public abstract void Resolve(Role<T> role, T[] buffer, int offset);
    }
	public abstract class Role<T>
	{
		public delegate bool MatchCallbackHandler(Role<T> role, T[] buffer, int index);
		public event MatchCallbackHandler MatchCallback;
		public bool IsMatch
		{
			get { return isMatch; }
		}
		public readonly string Name;
		public int StartIndex;
        protected bool isMatch;
        protected Condition<T>[] conditions;
        public Role(string name, int startIndex, params Condition<T>[] conditions)
        {
			Name = name;
            this.conditions = conditions;
            if (conditions != null)
            {
                foreach (Condition<T> condition in conditions)
                {
                    condition.Role = this;
                }
            }
			this.StartIndex = startIndex;
        }
		public virtual bool Validate(T[] buffer)
		{
			if (buffer.Length < 1)
			{
				isMatch = false;
				return false;
			}
			isMatch = true;
			for (int j = 0; j < conditions.Length; j++)
			{
				Condition<T> condition = conditions[j];
				if (!condition.Validate(buffer, StartIndex + j))
				{
					isMatch = false;
					break;
				}
			}
			if (isMatch)
			{
				if (MatchCallback == null || MatchCallback(this, buffer, StartIndex))
				{
					for (int j = 0; j < conditions.Length; j++)
					{
						Condition<T> condition = conditions[j];
						condition.Resolve(this, buffer, j);
					}
				}
				return true;
			}
			isMatch = false;
			return false;
		}

	}
    public class LexCondition : Condition<Lex>
    {

        public bool IsMatch
        {
            get { return isMatch; }
        }
        public char? Content;
        public ElementType? Type;
		public ElementType? ResolveType;
		protected bool isMatch;
        public override bool Validate(Lex[] buffer, int index)
        {
            Lex target = buffer[index];
			if (target == null)
			{
				return false;
			}
			D.WriteLine("(" + target.Content + "," + target.Type + ")<-(" + Type + "," + ")");
			Lex prev = null;
            if (index > 0)
            {
                prev = buffer[index - 1];
            }
            isMatch = false;
            if (Content != null && Content.Value != target.Content)
            {
                return false;
            }
            if (Type != null && Type.Value != target.Type)
            {
                return false;
            }
			D.WriteLine(isMatch.ToString());
            isMatch = NotifyCallback(buffer, prev, index, true);
            return isMatch;
        }

		public override void Resolve(Role<Lex> role, Lex[] buffer, int offset)
		{
			if (ResolveType.HasValue)
			{
				buffer[role.StartIndex + offset].Type = ResolveType.Value;
			}
		}
    }
	public class LexRole : Role<Lex>
	{
        public LexRole(string name, int startIndex, params LexCondition[] conditions) : base(name, startIndex, conditions) { }
	}
}
