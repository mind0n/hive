using System;
using System.Collections.Generic;

using System.Text;
using Fs.Entities;

namespace Fs.Web.Services
{
	public class ClauseBuilder
	{
		public delegate string MergeHandler(string[] ops, int pos);
		public MergeHandler OnMergingItem;
		public string MergeRlt;
		protected string[] prevList;
		protected string[] cntList;

		public void BuildClause(Node node, string[] ops, int depth)
		{
			if (node == null)
			{
				return;
			}
			foreach (string op in node.Keys)
			{
				if (op.IndexOf('_') != 0)
				{
					ops[depth] = op;
				}
				if (node[op].Values.Count > 0)
				{
					BuildClause(node[op], ops, depth + 1);
				}
				else
				{
					if (depth == cntList.Length - 1)
					{
						MergeClause(ops, cntList);
					}
				}
			}
		}
		protected void MergeClause(string[] ops, string[] mask)
		{
			int diff = -1;
			bool flag = false;
			string item = "";
			if (!string.IsNullOrEmpty(prevList[0]))
			{
				for (int i = 0; i < prevList.Length; i++)
				{
					if (!prevList[i].Equals(ops[i]))
					{
						diff = i;
						break;
					}
				}
			}
			else
			{
				flag = true;
			}
			for (int i = 0; i < ops.Length; i++)
			{
				if (OnMergingItem == null)
				{
					item += ops[i];
				}
				prevList[i] = ops[i];
			}
			if (flag)
			{
				MergeRlt += item;
			}
			else
			{
				MergeRlt += mask[diff] + item;
			}
		}
		public ClauseBuilder(string[] cnlist, string[] pvlist)
		{
			prevList = pvlist;
			cntList = cnlist;
		}
	}
}
