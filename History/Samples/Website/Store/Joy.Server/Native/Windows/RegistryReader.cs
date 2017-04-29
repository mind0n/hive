using System;
using System.Collections.Generic;
using System.Text;
using Joy.Server.Collections;
using Microsoft.Win32;
using Joy.Core;

namespace Joy.Server.Native.Windows
{
	public abstract class NodeReaderBasic	//<TNode, TValue, TType>
	{
		public bool IsEmpty
		{
			get
			{
				return (nodeContent == null);
			}
		}
		public NodeReaderBasic(object targetNode, NodeReaderType type)
		{
			Init(targetNode, type);
		}
		protected NodeReaderBasic() { }
		protected void Init(object targetNode, NodeReaderType type)
		{
			nodeType = type;
			nodeContent = targetNode;
		}
		public NodeReaderBasic this[string key]
		{
			get
			{
				return GetChild(key);
			}
		}
		public object Value
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
		//public virtual object Children { get { return null; } set { } }
		protected object nodeContent;
		protected NodeReaderType nodeType;
		public abstract void RemoveValue(object value);
		public virtual void SetValue(object value) { throw new NotImplementedException(); }
		public virtual void SetValue(string key, object value) { throw new NotImplementedException(); }
		protected abstract object GetChildNode(string key);
		protected abstract NodeReaderBasic GetChild(string key);
		public virtual NodeReaderBasic GetChildByPath(string path, char splitter)
		{
			string[] keys = path.Split(splitter);
			NodeReaderBasic ptr = this;
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
		protected abstract object GetValue();
		protected virtual void Dispose() { }
		protected T GetNodeContent<T>()
		{
			return (T)nodeContent;
		}
	}
	public abstract class ExtendableReader : NodeReaderBasic
	{
		public ExtendableReader(object targetNode, NodeReaderType type, bool allowAutoExtend)
			: base(targetNode, type)
		{
			Init(targetNode, type, allowAutoExtend);
		}
		protected void Init(object targetNode, NodeReaderType type, bool allowAutoExtend)
		{
			Init(targetNode, type);
			autoExtend = allowAutoExtend;
		}
		protected ExtendableReader() { }
		protected bool autoExtend;
		protected virtual object SetChildNode(string key) { throw new NotImplementedException(); }
	}
	public class RegistryReader : ExtendableReader
	{
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
		protected override NodeReaderBasic GetChild(string key)
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
	}
}
