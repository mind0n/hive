using System;
using System.Collections.Generic;
using System.Text;

namespace Fs.Entities
{
	public class BasicNode
	{
		public delegate object ConnectionHandler(BasicNode origin, BasicNode target);
		public ConnectionHandler NodeOnDisconnect;
		public ConnectionHandler NodeOnConnect;
		public static byte Successful = 0;
		public static byte CancelOperation = 1;
		public static byte ErrNodeListFull = 128;
		public static string [] Messages = new string[] {"Successful", "Node list full"};
		public int NodeCount
		{
			get {
				if (vNodes != null)
				{
					return vNodes.Length;
				}
				return 0;
			}
		}
		protected BasicNode[] vNodes;
		public BasicNode(int nodeListSize)
		{
			vNodes = new BasicNode[nodeListSize];
		}
		protected BasicNode() { }
		public byte Connect(BasicNode target)
		{
			int i = GetNodeIndex(null);
			byte rlt;
			if (NodeOnDisconnect != null)
			{
				if ((byte)NodeOnDisconnect(this, target) == CancelOperation)
				{
					return CancelOperation;
				}
			}
			if (i < 0)
			{
				return ErrNodeListFull;
			}
			else
			{
				vNodes[i] = target;
				return Successful;
			}
		}
		public void Disconnect(BasicNode target)
		{
			int i = GetNodeIndex(target);
			if (NodeOnDisconnect != null)
			{
				if ((byte)NodeOnDisconnect(this, target) == CancelOperation)
				{
					return;
				}
			}
			if (i >= 0)
			{
				vNodes[i] = null;
			}
		}
		public int GetNodeIndex(BasicNode target)
		{
			for (int i = 0; i < NodeCount; i++)
			{
				if (vNodes[i] == target)
				{
					return i;
				}
			}
			return -1;
		}
		public BasicNode GetNodeByIndex(int index)
		{
			if (0 <= index && index < NodeCount)
			{
				return vNodes[index];
			}
			return null;
		}
		protected virtual BasicNode CreateNode()
		{
			return new BasicNode();
		}
	}

}
