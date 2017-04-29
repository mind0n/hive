using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Lex.Roles
{
	public class MatchResult
	{
		public int Index;
		public bool IsMatch;
		public int Offset;
		public object Input;
	}
	public class RoleUnit<Tc>
	{
		public virtual bool Match(Tc input)
		{
			return false;
		}
	}
	public class Role<Ts, Tc>: List<RoleUnit<Tc>>
		where Ts:IEnumerable<Tc>
	{
		public Action<MatchResult> OnMatch;
		public MatchResult Result = new MatchResult { Index = -1, IsMatch = false, Offset=-1 };
		public Role(Action<MatchResult>callback, int offset, params RoleUnit<Tc>[] units)
		{
			Result.Offset = offset;
			OnMatch = callback;
			AddRange(units);
		}
		public virtual bool Match(Ts input)
		{
			if (Count <= 0)
			{
				if (OnMatch != null)
				{
					Result.IsMatch = true;
					OnMatch(Result);
				}
				return true;
			}
			Result.Input = input;
			RoleUnit<Tc> unit = this[0];
			for (int i = 0; i < input.Count<Tc>() - Count; i++)
			{
				Tc ch = input.ElementAt<Tc>(i);
				if (unit.Match(ch))
				{
					Result.IsMatch = true;
					for (int j = 1; j < Count; j++)
					{
						unit = this[j];
						ch = input.ElementAt<Tc>(i + j);
						if (!unit.Match(ch))
						{
							Result.IsMatch = false;
							break;
						}
					}
					if (Result.IsMatch)
					{
						Result.Index = i;
						if (OnMatch != null)
						{
							OnMatch(Result);
						}
						return true;
					}
				}
			}
			return false;
		}
	}
}
