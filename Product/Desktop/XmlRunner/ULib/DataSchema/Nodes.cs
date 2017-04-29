using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.Controls;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using ULib.Exceptions;
using ULib.Debugging;
using ULib.Executing;
using System.Reflection;
using System.Xml.Linq;

namespace ULib.DataSchema
{
	[Serializable]
    public class Node
    {
		private List<Node> children = new List<Node>();

		public List<Node> Children
		{
			get { return children; }
			set { children = value; }
		}
		public Node this[int index]
		{
			get
			{
				return children[index];
			}
			set
			{
				children[index] = value;
			}
		}
        public virtual string Name { get; set; }

		[Parameter(IsHidden = true)]
		public object Value;

        public bool IsEmpty
        {
            get
            {
                return !HasValue && children.Count < 1;
            }
        }

        public bool HasValue
        {
            get
            {
				return Value != null;
            }
        }

		public void Add(Node node, int index = -1)
		{
			if (!children.Contains(node))
			{
				if (index >= 0 && index < children.Count)
				{
					children.Insert(index, node);
				}
				else
				{
					children.Add(node);
				}
			}
		}
		public void Clear()
		{
			children.Clear();
		}
        public T Child<T>(int index) where T : class
        {
            if (index >= 0 && index < this.children.Count)
            {
                return this[index] as T;
            }
            return default(T);
        }

    }

	public enum CommandStatus
	{
		Wait,
		Executing,
		Error,
		Success
	}

	[Serializable]
	public abstract class CommandNode : Node
    {
		[Parameter( IsReadonly=true)]
        public string Id;

		[Parameter(IsHidden = true)]
		public string Editor;

		[Parameter(IsHidden = true)]
		public bool IsRunOnConfig;

		[Parameter(IsHidden = true)]
		public bool IsRunOnLoad;

        [Parameter(IsHidden = true)]
	    public string OutputId;
	
		public int Index { get; set; }

		[Parameter(IsHidden = true)]
		public bool Visible = true;
		private Exception error;

		public Exception Error
		{
			get { return error; }
			set
			{
				Exception e = value;
				if (e != null)
				{
					while (e.InnerException != null)
					{
						e = e.InnerException;
					}
					error = e;
				}
				else
				{
					error = null;
				}
			}
		}
		public CommandStatus ExecStatus { get; set; }
            
		[XmlIgnore]
        public IOutput Outputer { get; set; }
        protected bool isStopRequested;
        public void Stop()
        {
            isStopRequested = true;
        }
		public virtual void Parse(CommandNode cmd)
		{
			if (cmd != null)
			{
				Type selfType = GetType();
				FieldInfo[] list = cmd.GetType().GetFields();
				foreach (FieldInfo i in list)
				{
					FieldInfo f = selfType.GetField(i.Name);
					if (f != null)
					{
						f.SetValue(this, i.GetValue(cmd));
					}
				}
			}
		}
		public virtual void Parse(XElement xml)
		{
			IEnumerable<XAttribute> attrs = xml.Attributes();
			Type type = this.GetType();
			foreach (XAttribute a in attrs)
			{
				if (string.Equals(a.Name.ToString(), "id", StringComparison.OrdinalIgnoreCase))
				{
					ExecuteParameter par = new ExecuteParameter { Name = a.Value, IsCondition = false, Value = this };
					Executor.Instance.Parameters[a.Value.ToLower()] = par;
				}
				FieldInfo field = type.GetField(a.Name.ToString());
				if (field != null)
				{
					field.SetValue(this, Convert.ChangeType(a.Value, field.FieldType));
				}
			}

		}

        protected void Output(Color color, params string[] msg)
        {
            if (Outputer != null)
            {
                Outputer.WriteMsg("<br />" + string.Concat(msg), true, color.Name, true);
            }
            else
            {
                D.WriteMsg(string.Concat(msg));
            }
        }
        protected void Output(params string[] msg)
        {
            if (Outputer != null)
            {
                Outputer.WriteMsg("<br />" + string.Concat(msg), true);
            }
        }
        protected CommandType NodeType = CommandType.Command;
		protected bool isCancelling;

		public CommandNode()
		{
			ExecStatus = CommandStatus.Wait;
			IsRunOnConfig = false;
			IsRunOnLoad = false;
			CommandSet.Register(this);
		}
        public virtual CommandResult Execute(bool isPreview = false) 
        {
            CommandResult rlt = new CommandResult();
            isStopRequested = false;
            try
            {
                Run(rlt, isPreview);
                if (rlt.IsSuccessful && !string.IsNullOrEmpty(OutputId))
                {
                    rlt.ResultValue = Executor.Instance.SetVar(OutputId, rlt.IsCondition,
                                                               rlt.IsCondition ? rlt.BoolResult : rlt.ResultValue);
                }
            }
            catch (Exception ex)
            {
                rlt.LastError = ex;
            }
            return rlt;
        }
		public virtual void Load() { }
        public virtual void Run(CommandResult rlt, bool isPreview = false) { isStopRequested = false; }
        public virtual XElement XmlLoaded() {
            try
            {
                XElement el = XmlLoaded();
                return el;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
            return null;
        }
        public virtual void XmlLoaded(CommandResult rlt, XElement parent) { }
        public virtual CommandTreeViewNode CreateTreeviewNode()
        {
            CommandTreeViewNode node = new CommandTreeViewNode();
            node.Text = this.Name;
            node.CmdNode = this;
            return node;
        }
		public virtual ActionTreeView CreateTreeview(Action<CommandTreeViewNode, CommandNode> callback, TreeNodeCollection collection = null, ActionTreeView treeView = null)
		{
			if (treeView == null)
			{
				treeView = new ActionTreeView();
			}
			collection = treeView.Nodes;

			foreach (CommandNode node in this.Children)
			{
				if (node != null)
				{
					CommandTreeViewNode treeNode = node.CreateTreeviewNode();
					if (callback != null)
					{
						callback(treeNode, node);
					}
					collection.Add(treeNode);
					if (node.Children.Count > 0)
					{
						node.CreateTreeview(callback, treeNode.Nodes, treeView);
					}
				}
			}
			return treeView;
		}
		public virtual CommandNode Clone()
		{
			Type type = this.GetType();
			ConstructorInfo ci = type.GetConstructor(System.Type.EmptyTypes);
			if (ci != null)
			{
				CommandNode cmd = ci.Invoke(null) as CommandNode;
				if (cmd != null)
				{
					FieldInfo[] list = type.GetFields();
					foreach (FieldInfo i in list)
					{
						if (i.IsPublic && !string.Equals(i.Name, "id", StringComparison.OrdinalIgnoreCase))
						{
							object v = i.GetValue(this);
							i.SetValue(cmd, v);
						}
					}
				}
				return cmd;
			}
			return null;
		}
        public virtual void Clone(CommandNode cmd)
        {
            cmd.Id = Id;
        }
		public virtual void Reset() { AccomplishCancellation(); }
		public virtual void Cancel() { isCancelling = true; }
		public virtual void CopyChildren(CommandNode result)
		{
			foreach (CommandNode i in this.Children)
			{
				if (i != null)
				{
					CommandNode node = i.Clone();
					result.Children.Add(node);
				}
			}
		}
		protected virtual bool AccomplishCancellation()
		{
			if (isCancelling)
			{
				isCancelling = false;
				Reset();
				return true;
			}
			return false;
		}
		protected virtual string GenerateXml() { return null; }
		const string t = "<{0} {1} />";
		const string tc = "<{0} {1}>\r\n{2}\r\n</{0}>";
		public string ToXml(bool addHeader = false)
		{
			string rlt = null;
			string tag = this.GetType().Name.Replace("Command", "");
			List<string> els = new List<string>();
			string attributes = MakeAttributeString();
			rlt = GenerateXml();
			if (rlt == null)
			{
				if (Children.Count < 1)
				{
					rlt = string.Format(t, tag, attributes);
				}
				else
				{
					string innerXml = null;
					for (int i = 0; i < Children.Count; i++)
					{
						CommandNode n = Children[i] as CommandNode;
						if (n != null)
						{
							els.Add(n.ToXml());
						}
					}
					innerXml = string.Join("\r\n", els.ToArray());
					rlt = string.Format(tc, tag, attributes, innerXml);
				}
				if (addHeader)
				{
					rlt = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n" + rlt;
				}
			}
			return rlt;
		}

		private string MakeAttributeString()
		{
			string attributes = null;
			List<string> ats = new List<string>();
			FieldInfo[] list = this.GetType().GetFields();
			foreach (FieldInfo i in list)
			{
				object v = i.GetValue(this);
				if (v != null)
				{
					ats.Add(string.Concat(i.Name, "=\"", v.ToString(), "\""));
				}
			}
			attributes = string.Join(" ", ats.ToArray());
			return attributes;
		}
	}
    public class EmptyCommand : CommandNode
    {
    }
	[Serializable]
	public class BeginCommand : CommandNode
    {
        public void Save(string file, params Type[] extraTypes)
        {
            try
            {
                string content = this.ToXml(extraTypes);
                File.WriteAllText(file, content);
            }
            catch (Exception e)
            {
                ExceptionHandler.Handle(e);
            }
        }

		public override CommandNode Clone()
		{
			return this;
		}
		
		public override void Reset()
		{
			base.Reset();	
		}

	}
    public class CommandResult : Result
    {
		public object ResultValue;
		public bool BoolResult;
		public bool ShouldStop;
		public bool ValidationFailed;
		public bool ErrorOccurred;
        public bool IsCondition;
		public CommandTreeViewNode Next;
    }
    public class CommandParameter
    {
        public string Name;
        public string Value;
    }
    public enum CommandType
    {
        Group,
        Command
    }

	public class CommandSet
	{
		private static readonly List<Type> cmdTypes = new List<Type>();
		public static Type[] SupportedCommands
		{
			get
			{
				return cmdTypes.ToArray();
			}
		}
		public static void Register(CommandNode cmd)
		{
			if (!cmdTypes.Contains(cmd.GetType()))
			{
				cmdTypes.Add(cmd.GetType());
			}
		}
		public static void Register(params Type[] types)
		{
			cmdTypes.AddRange(types);
		}
	}
}
