using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace Joy.Core
{
	public enum NodeReaderType : int
	{
		Unknown
		, Value
		, Attribute
		, Node
	}
	public abstract class NodeReaderBase
	{
		public delegate bool EnumChildContentHandler(object item);
		public bool IsInvalid
		{
			get
			{
				return vNodeContent == null;
			}
		}
		public bool IsEmpty
		{
			get
			{
				return IsInvalid || Value == null;
			}
		}
		public object Value
		{
			get
			{
				return GetChildContent(null);
			}
			set
			{
				SetChildContent(null, value);
			}
		}
		public NodeReaderBase this[string key]
		{
			get
			{
				return SwitchToChild(key);
			}
		}
		protected NodeReaderBase nodePtr;
		protected bool AllowAutoExtend;
		protected NodeReaderType vNodeType;
		protected object vNodeContent;
		public T Duplicate<T>() where T : NodeReaderBase, new()
		{
			T rlt = new T();
			rlt.Load(vNodeContent, vNodeType, AllowAutoExtend);
			return rlt;
		}
		public T NodeContent<T>()
		{
			return (T)vNodeContent;
		}
		public void Load(object content, NodeReaderType type, bool autoExtend)
		{
			Dispose(null);
			vNodeContent = content;
			vNodeType = type;
			AllowAutoExtend = autoExtend;
		}
		public virtual void Dispose(params object[] parlist)
		{
			vNodeContent = null;
			vNodeType = NodeReaderType.Unknown;
			AllowAutoExtend = false;
		}
		public virtual bool RemoveChild(string key)
		{
			if (!IsInvalid && vNodeType == NodeReaderType.Node)
			{
				return RemoveChildContent(key);
			}
			return false;
		}
		public virtual object SetValue(string key, object value)
		{
			return SetValue(key, value, true);
		}
		public virtual object SetValue(string key, object value, bool overwrite)
		{
			if (vNodeType == NodeReaderType.Value)
			{
				vNodeContent = value;
				return value;
			}
			else
			{
				return SetChildContent(key, value, overwrite);
			}
		}
		public virtual object AddValue(string key, object value)
		{
			return SetValue(key, value, false);
		}
		public virtual bool Exist(string key, object value)
		{
			if (EnumChild(delegate(object child)
			{
				NodeReaderBase item = (NodeReaderBase)child;
				if (item.GetContentKeyString().Equals(key))
				{
					if (value != null)
					{
						if (item.ValueEquals(value))
						{
							return false;
						}
						return true;
					}
					return false;
				}
				return true;
			}) != null)
			{
				return true;
			}
			return false;
		}
		public virtual T GetValue<T>(string key)
		{
			if (vNodeType == NodeReaderType.Value || string.IsNullOrEmpty(key))
			{
				return NodeContent<T>();
			}
			else
			{
				return (T)GetChildContent(key);
			}
		}
		protected abstract NodeReaderBase CreateInstance(object content);
		protected abstract int GetChildCount();
		protected abstract object GetChildContent(int index);
		protected abstract object GetChildContent(string key);
		protected abstract object SetChildContent(string key, object value, bool overwrite);
		protected abstract object AddChildContent(string key, object value);
		protected abstract bool RemoveChildContent(string key);
		protected abstract bool ValueEquals(object value);
		protected abstract string GetContentKeyString();
		protected abstract NodeReaderType DetectNodeTypeFromKeyName(string key);
		protected NodeReaderBase SwitchToChild(string key)
		{
			object content = this.GetChildContent(key);
			if (content == null && AllowAutoExtend)
			{
				content = ExtendChild(key);
			}
			Load(content, DetectNodeTypeFromKeyName(key), AllowAutoExtend);
			return this;
		}
		protected virtual object SetChildContent(string key, object value)
		{
			return SetChildContent(key, value, true);
		}
		protected virtual object EnumChild(EnumChildContentHandler callback)
		{
			object rlt;
			if (callback != null)
			{
				for (int i = 0; i < GetChildCount(); i++)
				{
					rlt = CreateInstance(GetChildContent(i));
					if (!callback(rlt))
					{
						return rlt;
					}
				}
				return null;
			}
			return null;
		}
		protected virtual object ExtendChild(string key)
		{
			vNodeContent = SetChildContent(key, null);
			return vNodeContent;
		}
	}
}
