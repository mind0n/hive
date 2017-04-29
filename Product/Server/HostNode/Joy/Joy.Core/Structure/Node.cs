using System.Collections.Generic;

namespace Joy.Core.Structure
{
	public class Node
	{
		protected delegate Node CreateInstanceHandler();
		protected event CreateInstanceHandler childCreateInstance;
		public bool AutoGrow;
		public readonly List<Node> Items = new List<Node>();
		public Node this[int i]
		{
			get
			{
				if (i >= 0 && i < Items.Count)
				{
					return Items[i];
				}
				else
				{
					return null;
				}
			}
			set
			{
				if (i >= 0 && i < Items.Count)
				{
					Items[i] = value;
				}
				else if (AutoGrow && i >= 0)
				{
					for (int n = Items.Count; n < i; n++)
					{
						Items.Add(null);
					}
					Items.Add(value);
				}
			}
		}
		public Node()
		{
		}
		//public void Add<T>(T item) where T : Node
		//{
		//    Items.Add(item);
		//}
		public Node Add()
		{
			Node n = CreateInstance();
			Items.Add(n);
			return n;
		}
		public void AddRange<T>(List<T> list) where T : Node
		{
			for (int i = 0; i < list.Count; i++)
			{
				Items.Add(list[i]);
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
			TextNode n = Add() as TextNode;
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
			ActionNodeData n = Add() as ActionNodeData;
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
