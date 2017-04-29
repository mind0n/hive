using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptInterpreter
{
	class StringSegmentReader
	{
		public List<Segment> Segments = new List<Segment>();

		protected string rawScript;
		protected int index;
		protected char[] cache;
		protected int cacheIndex = -1;


		public StringSegmentReader(string script)
		{
			rawScript = script;
			index = 0;
			cache = new char[3];
		}

		public Segment ReadNextSegment()
		{
			if (!string.IsNullOrEmpty(rawScript) && index >= 0 && index < rawScript.Length)
			{
				Segment rlt = new Segment(Segments);

				for (; index <= rawScript.Length; index++)
				{
					if (cacheIndex < 1)
					{
						cacheIndex++;
					}
					if (index == rawScript.Length)
					{
						cache[cacheIndex] = '\0';
					}
					else
					{
						cache[cacheIndex] = rawScript[index];
						if (cacheIndex == 1 && index + 1 < rawScript.Length)
						{
							cache[cacheIndex + 1] = rawScript[index + 1];
							cacheIndex++;
							index++;
						}
					}
					if (rlt.Read(cache, index < rawScript.Length))
					{
						if ((cache[1] == '"' && rlt.SegmentType == SegmentMode.String)
							|| (cacheIndex == 2 && (cache[1] == ' ' && rlt.SegmentType != SegmentMode.String && rlt.SegmentType != SegmentMode.Comment))
							|| (rlt.SegmentType == SegmentMode.Comment && "/*".Equals(rlt.StartOpCode)))
						{
							index++;
							cache[0] = cache[1];
							cache[1] = cache[2];
						}
						break;
					}
					if (cacheIndex == 2)
					{
						cache[0] = cache[1];
						cache[1] = cache[2];
					}
				}

				if (Keywords.Contains(rlt.Content))
				{
					rlt.SegmentType = SegmentMode.Keyword;
				}
				else if (rlt.SegmentType == SegmentMode.NewLine)
				{
					rlt.SegmentType = SegmentMode.Ignore;
				}
				AppendSegment(rlt);
				return rlt;
			}
			else
			{
				Segment rlt = new Segment(null);
				if (index == rawScript.Length && cache[1] != '\0')
				{
					cache[2] = '\0';
					rlt.Read(cache, false);
					index++;
					AppendSegment(rlt);
					return rlt;
				}
				else
				{
					return null;
				}
			}
		}

		private void AppendSegment(Segment rlt)
		{
			if (rlt.SegmentType != SegmentMode.Ignore)
			{
				Segments.Add(rlt);
			}
		}
	}
}
