using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Joy.Core.DataSchema
{
	public class Node
	{
		protected delegate Node CreateInstanceHandler();
		protected event CreateInstanceHandler childCreateInstance;
		public bool AutoGrow;
		public readonly List<Node> Children = new List<Node>();
		public Node this[int i]
		{
			get
			{
				if (i >= 0 && i < Children.Count)
				{
					return Children[i];
				}
				else
				{
					return null;
				}
			}
			set
			{
				if (i >= 0 && i < Children.Count)
				{
					Children[i] = value;
				}
				else if (AutoGrow && i >= 0)
				{
					for (int n = Children.Count; n < i; n++)
					{
						Children.Add(null);
					}
					Children.Add(value);
				}
			}
		}
		public Node()
		{
		}
		//public void Add<T>(T item) where T : Node
		//{
		//	Children.Add(item);
		//}
		public void Add(Node item)
		{
			Children.Add(item);
		}
		public T New<T>() where T : Node
		{
			T rlt = CreateInstance() as T;
			return rlt;
		}
		public Node NewNode()
		{
			Node n = CreateInstance();
			Children.Add(n);
			return n;
		}
		public void AddRange<T>(List<T> list) where T : Node
		{
			for (int i = 0; i < list.Count; i++)
			{
				Children.Add(list[i]);
			}
		}
		protected T CreateInstance<T>() where T : Node, new()
		{
			return new T();
		}
		public virtual Node CreateInstance()
		{
			if (childCreateInstance != null)
			{
				return childCreateInstance();
			}
			return null;
		}

	}
	public class TextNode : Node
	{
		public string Name { get; set; }
		public string Text { get; set; }
		public TextNode() : base()
		{
			childCreateInstance += new CreateInstanceHandler(TextNodeCreateInstance);
		}

		public TextNode(string content)
			: this()
		{
			Text = content;
		}

		public TextNode Add(string text)
		{
			TextNode n = NewNode() as TextNode;
			if (n != null)
			{
				n.Text = text;
			}
			return n as TextNode;
		}

		protected Node TextNodeCreateInstance()
		{
			return CreateInstance<TextNode>();
		}
	}
	public class ActionNodeData : TextNode
	{
		public string TypeName;
		public bool IsDefault;

		public ActionNodeData() : base()
		{
			childCreateInstance -= TextNodeCreateInstance;
			childCreateInstance += new CreateInstanceHandler(ActionNodeDataCreateInstance);
		}

		public ActionNodeData(string typeName, string text) : this()
		{
			TypeName = typeName;
			Text = text;
		}

		public ActionNodeData Add(string typename, string text)
		{
			ActionNodeData n = NewNode() as ActionNodeData;
			n.TypeName = typename;
			n.Text = text;
			return n;
		}

		Node ActionNodeDataCreateInstance()
		{
			return CreateInstance<ActionNodeData>();
		}
	}
}
