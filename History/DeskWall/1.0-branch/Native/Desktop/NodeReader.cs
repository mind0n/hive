using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using Fs;

namespace Dw.Collections
{
	public enum NodeReaderType : int
	{
		Unknown
		, Attribute
		, Node
	}
	public class NodeReader	//<TNode, TValue, TType>
	{
		public bool IsEmpty
		{
			get
			{
				return (nodeContent == null);
			}
		}
		public NodeReader(object targetNode, NodeReaderType type)
		{
			Init(type);
			nodeContent = targetNode;
		}
		protected void Init(NodeReaderType type)
		{
			nodeType = type;
		}
		public virtual NodeReader this[string key]
		{
			get
			{
				return GetChild(key);
			}
		}
		public virtual object Value
		{
			get
			{
				return GetValue();
			}
			set
			{
				SetValue(value);
			}
		}
		public virtual object Children { get { return null; } set { } }
		protected object nodeContent;
		protected NodeReaderType nodeType;
		public virtual void RemoveValue(object value) { throw new NotSupportedException(); }
		public virtual void SetValue(object value) { throw new NotSupportedException(); }
		public virtual void SetValue(string key, object value) { throw new NotSupportedException(); }
		protected virtual object GetChildNode(string key) { return default(object); }
		protected virtual NodeReader GetChild(string key) { throw new NotSupportedException(); }
		public virtual NodeReader GetChildByPath(string path, char splitter)
		{
			string[] keys = path.Split(splitter);
			NodeReader ptr = this;
			foreach (string key in keys)
			{
				ptr = ptr[key];
				if (ptr.IsEmpty)
				{
					break;
				}
			}
			if (ptr != this)
			{
				return ptr;
			}
			else
			{
				return null;
			}
		}
		protected virtual bool SwitchToChild(string key)
		{
			if (IsEmpty)
			{
				return false;
			}
			Dispose();
			object node = GetChildNode(key);
			if (node != null)
			{
				nodeContent = node;
				return true;
			}
			return false;
		}
		protected virtual object GetValue() { throw new NotSupportedException(); }
		protected virtual void Dispose() { }
	}
	public abstract class ExtendableReader : NodeReader
	{
		public ExtendableReader(object targetNode, NodeReaderType type, bool allowAutoExtend) : base(targetNode, type) 
		{
			autoExtend = allowAutoExtend;
		}
		protected bool autoExtend;
		protected abstract object SetChildNode(string key);
	}
	public class RegistryReader : ExtendableReader
	{
		public RegistryReader(object targetNode) : base(targetNode, NodeReaderType.Node, false) { }
		public RegistryReader(object targetNode, NodeReaderType type, bool allowAutoExtend) : base(targetNode, type, allowAutoExtend) { }

		public override void SetValue(string key, object value)
		{
			curtNodePtr.SetValue(key, value);
		}
		public override void RemoveValue(object value)
		{
			try
			{
				curtNodePtr.DeleteValue((string)value);
			}
			catch (Exception err)
			{
				Exceptions.LogOnly(err);
			}
		}
		protected RegistryKey curtNodePtr
		{
			get
			{
				return (RegistryKey)nodeContent;
			}
			set
			{
				nodeContent = value;
			}
		}
		protected override NodeReader GetChild(string key)
		{
			if (!IsEmpty)
			{
				if (key[0] == '$')
				{
					key = key.Substring(1);
					return new RegistryReader(curtNodePtr.GetValue(key), NodeReaderType.Attribute, autoExtend);
				}
				else
				{
					return new RegistryReader(GetChildNode(key), NodeReaderType.Node, autoExtend);
				}
			}
			return new RegistryReader(null, NodeReaderType.Unknown, false);
		}
		protected override object GetChildNode(string key)
		{
			if (!IsEmpty)
			{
				string[] names = curtNodePtr.GetSubKeyNames();
				if (!string.IsNullOrEmpty(key))
				{
					key = key.ToLower();
				}
				foreach (string name in names)
				{
					if (name.ToLower().Equals(key))
					{
						return curtNodePtr.OpenSubKey(name, true);
					}
				}
				if (autoExtend)
				{
					return SetChildNode(key);
				}
			}
			return null;
		}
		protected override object GetValue()
		{
			object rlt;
			if (nodeType == NodeReaderType.Node)
			{
				rlt = curtNodePtr.GetValue("");
			}
			else if (nodeType == NodeReaderType.Attribute)
			{
				return nodeContent;
			}
			return nodeContent;
		}
		protected override object SetChildNode(string key)
		{
			return null;
		}
	}
}
