using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using System.IO;
using ULib.Exceptions;
using System.Reflection;
using ULib.Executing.Commands.Common;
using ULib.Output;
using ULib.Forms;
using System.Xml.Linq;
using System.Threading;
using ULib.Executing.Commands.OS;
using System.Diagnostics;
using ULib.NativeSystem;
using ULib.Controls;
using System.Drawing;

namespace ULib.Executing
{
    public class Executor
    {
		public delegate bool StopRunHandler(CommandNode cmd, bool isCompleted = false, CommandResult rlt = null);
		public delegate void RunCmdHandler(CommandNode cmd, bool isCompleted=false, CommandResult rlt = null);
		public delegate void RunHandler(CommandNode runTo, CommandNode runFrom);
        public delegate void RunCompletedHandler(CommandNode runTo, CommandNode runFrom, bool isStopOnError = false);
        public event RunHandler OnRun;
		public event RunCompletedHandler OnRunCompleted;
		public event StopRunHandler OnExecutionStopped;
		public event RunCmdHandler OnRunCmd;
        private static Executor instance = new Executor();
		public static Executor Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Executor();
                }
                return instance;
            }
        }
		public FuncCacheCollection FunctionCommands = new FuncCacheCollection();
		public BeginCommand ExecEntry = new BeginCommand();
		public BeginCommand ConfigEntry = new BeginCommand();
		public BeginCommand LoadEntry = new BeginCommand();

		public ExecuteState ExecState = new ExecuteState();
		public ExecuteState ConfigState = new ExecuteState();
		public ExecuteState LoadState = new ExecuteState();

		public bool FailAndRun = true;
		public ExecuteParameters Parameters = new ExecuteParameters();
		private static Type[] commandTypes;
		private bool isStop;

		public string ParseIdString(string s)
		{
			object rlt = ParseId(s);
			if (rlt != null)
			{
				if (rlt is ArrayCommand)
				{
					return ((ArrayCommand)rlt).Content;
				}
				return rlt.ToString();
			}
			return string.Empty;
		}
		public object ParseId(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return s;
			}
			if (s.StartsWith("$"))
			{
				string r = GetString(s.Substring(1));
                if (r != null)
                {
                    return ParseId(r);
                }
                else
                {
                    return s;
                }
			}
			else
			{
				return s;
			}
		}

		public bool Preview(CommandNode run2cmd = null, ULib.Executing.ExecuteState.RunCmdHandler callback = null)
		{
			do
			{
				try
				{
					if (run2cmd != null && ExecState.CurtCmd == run2cmd)
					{
						return true;
					}
					CommandResult rlt = ExecState.Execute(true, callback);
					if (!rlt.IsSuccessful)
					{
						if (rlt.IsCanceled)
						{
							OutputHandler.Handle("Execution cancelled by user");
							break;
						}
						else if (rlt.IsError)
						{
							ExceptionHandler.Handle(rlt.LastError);
							if (!FailAndRun)
							{
								break;
							}
						}
					}
					if (rlt.ShouldStop)
					{
						bool shouldStop = true;
						if (OnExecutionStopped != null)
						{
							shouldStop = OnExecutionStopped(ExecState.CurtCmd, true, rlt);
						}
						if (shouldStop)
						{
							break;
						}
					}
				}
				catch (Exception e)
				{
					ExceptionHandler.Handle(e);
				}
			} while (ExecState.Next());
			ExecState.Reset();
			return false;
		}

		public void Stop()
		{
			OutputHandler.Handle("Stopping execution, please wait...");
			isStop = true;
            if (ExecState != null && ExecState.CurtCmd != null)
            {
                ExecState.CurtCmd.Cancel();
                ExecState.CurtCmd.Stop();
            }
		}

		private bool isDirty;

		public bool IsDirty
		{
			get { return isDirty; }
		}

		public void RunOnLoad()
		{
			Run(null, null, LoadState);
		}
		public void RunOnConfig()
		{
			Run(null, null, ConfigState);
		}
		public void RunAsync(CommandNode runToCmd = null, CommandNode runFromCmd = null, ExecuteState state = null)
        {
			ThreadStart ts = new ThreadStart(delegate()
			{
				Run(runToCmd, runFromCmd, state);
			});
			Thread th = new Thread(ts);
			th.IsBackground = false;
			th.Start();
		}

		private void Run(CommandNode runToCmd, CommandNode runFromCmd, ExecuteState state)
		{
			if (state == null)
			{
				state = ExecState;
			}
			bool runFromReached = false;
			try
			{
				if (OnRun != null)
				{
					OnRun(runToCmd, runFromCmd);
				}
			}
			catch (Exception ex)
			{
				ExceptionHandler.Handle(ex);
			}
		    bool isStoppedOnError = false;
			do
			{
				try
				{
					if (runFromCmd != null && state.CurtCmd != runFromCmd && !runFromReached)
					{
						continue;
					}
					else if (runFromCmd != null && state.CurtCmd == runFromCmd)
					{
						runFromReached = true;
					}
					isDirty = true;
					CommandResult rlt = state.Execute();
					if (!rlt.IsSuccessful)
					{
						if (rlt.IsCanceled)
						{
							OutputHandler.Handle("Execution cancelled by user");
							break;
						}
						else if (rlt.IsError)
						{
							ExceptionHandler.Handle(rlt.LastError);
							if (!FailAndRun)
							{
							    isStoppedOnError = true;
								break;
							}
						}
					}
					if (rlt.ShouldStop)
					{
						bool shouldStop = true;
						if (OnExecutionStopped != null)
						{
							shouldStop = OnExecutionStopped(ExecState.CurtCmd, true, rlt);
						}
						if (shouldStop)
						{
							break;
						}
					}
					if (runToCmd != null && state.CurtCmd == runToCmd)
					{
						return;
					}
				}
				catch (Exception e)
				{
					ExceptionHandler.Handle(e);
				}
			} while (state.Next() && !isStop);
			OutputHandler.Handle("--- Completed ---", 1);
			try
			{
				if (OnRunCompleted != null)
				{
					OnRunCompleted(runToCmd, runFromCmd, isStoppedOnError);
				}
			}
			catch (Exception ex)
			{
				ExceptionHandler.Handle(ex);
			}
			if (isStop)
			{
				isStop = false;
			}
			state.Reset();
		}

		public string Cache(string file = null)
		{
			try
			{
				if (string.IsNullOrEmpty(file))
				{
					file = parameterBackupFile;
				}
				Parameters.Cache(file);
				string rlt = ExecEntry.ToXml(true);
				return rlt;
			}
			catch (Exception ex)
			{
				ExceptionHandler.Handle(ex);
			}
			return null;
		}
		
		public void Reset()
		{
			FunctionCommands.Clear();
			ExecState.Reset();
			ConfigState.Reset();
			LoadState.Reset();
			if (!string.IsNullOrEmpty(scripts))
			{
				Load(scripts);
			}
			isDirty = false;
		}

		private string scripts;
		private string parameterBackupFile;

		public string Scripts
		{
			get { return scripts; }
		}

		public void LoadFile(string filename, string parameterfile)
		{
			if (File.Exists(filename))
			{
				string xml = File.ReadAllText(filename);
				string par = null;
				if (!string.IsNullOrEmpty(parameterfile) && File.Exists(parameterfile))
				{
					par = File.ReadAllText(parameterfile);
				}
				Load(xml);
			}
		}

		public List<AutoLabelItem> LoadDescription(string xml)
		{
			if (!string.IsNullOrEmpty(xml))
			{
				List<AutoLabelItem> rlt = new List<AutoLabelItem>();
				XDocument doc = XDocument.Parse(xml);
				XElement el = doc.Root;
				foreach (XElement i in el.Elements())
				{
					if (string.Equals(i.Name.ToString(), "descriptions", StringComparison.OrdinalIgnoreCase))
					{
						if (i.HasElements)
						{
							foreach (XNode ii in i.Nodes())
							{
								AutoLabelItem item = new AutoLabelItem();
								item.Text = GetXValue(ii).Trim();
								if (item.Text.StartsWith("\r\n"))
								{
									item.Text = item.Text.Substring(2);
								}
								item.FamilyName = GetXValue(ii, "FamilyName");
								item.DisplayStyle = GetXStyle(ii, "FontStyle");
								item.EmSize = GetXValue<float>(ii, "Size");
								item.Color = GetXValue(ii, "Color");
								rlt.Add(item);
							}
						}
					}
				}
				return rlt;
			}
			return null;
		}

		private static string GetXValue(XNode node, string name = null)
		{
			if (node is XElement)
			{
				XElement ii = (XElement)node;
				if (string.IsNullOrEmpty(name))
				{
					return ii.Value;
				}
				else if (ii.Attribute(name) != null)
				{
					return ii.Attribute(name).Value;
				}
				return string.Empty;
			}
			else if (node is XText)
			{
				return ((XText)node).Value;
			}
			return string.Empty;
		}
		public static FontStyle GetXStyle(XNode node, string name)
		{
			string rlt = GetXValue(node, name);
			if (string.Equals(rlt, "bold", StringComparison.OrdinalIgnoreCase))
			{
				return FontStyle.Bold;
			}
			else if (string.Equals(rlt, "italic", StringComparison.OrdinalIgnoreCase))
			{
				return FontStyle.Italic;
			}
			else if (string.Equals(rlt, "underline", StringComparison.OrdinalIgnoreCase))
			{
				return FontStyle.Underline;
			}
			return FontStyle.Regular;
		}
		private static T GetXValue<T>(XNode node, string name)
		{
			if (node is XElement)
			{
				XElement ii = (XElement)node;
				if (ii.Attribute(name) != null)
				{
					string v = ii.Attribute(name).Value;
					return ConvertValue<T>(v);
				}
				return default(T);
			}
			else if (node is XText && string.Equals(name, "value", StringComparison.OrdinalIgnoreCase))
			{
				return ConvertValue<T>(((XText)node).Value);
			}
			return default(T);
		}

		private static T ConvertValue<T>(string v)
		{
			try
			{
				T rlt = (T)Convert.ChangeType(v, typeof(T));
				return rlt;
			}
			catch
			{
				return default(T);
			}
		}
		public void LoadParameters(string bakfile)
		{
			parameterBackupFile = bakfile;
			if (File.Exists(bakfile))
			{
				Parameters.Load(bakfile);
			}
		}
		public void Load(string xml)
		{
			Parameters.Clear();
			if (!string.IsNullOrEmpty(xml))
			{
				scripts = xml;
				ParseDocument(xml);
				ExecState.Load(ExecEntry);
				ConfigState.Load(ConfigEntry);
				LoadState.Load(LoadEntry);
				//LoadParameters(bakfile);
			}
		}

		public ConfigScreen MakeParamConfigScreen()
		{
			ConfigScreen cfg = new ConfigScreen(Parameters);
			return cfg;
		}
		public ConfigScreen MakeConfigScreen()
		{
			ConfigScreen cfg = new ConfigScreen(this);
			return cfg;
		}
        private void ParseDocument(string xml)
        {
            try
            {
				LoadCommands();
				ConfigEntry.Clear();
				LoadEntry.Clear();
				ExecEntry.Clear();
				XDocument doc = XDocument.Parse(xml);
                XElement el = doc.Root;
				CommandNode parent = ExecEntry;
				ParseNode(el, parent);
            }
            catch (Exception e)
            {
                ExceptionHandler.Handle(e);
            }
        }

		private void LoadCommands()
		{
			Assembly asm = Assembly.GetAssembly(typeof(IfCommand));
			commandTypes = asm.GetTypes();
		}
		public static CommandNode CreateCmd(XElement cmdEl)
		{
			string cmdName = cmdEl.Name + "Command";
			Type type = GetType(cmdName);
			if (type != null)
			{
				CommandNode cmd = Activator.CreateInstance(type) as CommandNode;
				if (cmd != null)
				{
					cmd.Parse(cmdEl);
				}
				return cmd;
			}
			return null;
		}
		private void ParseNode(XElement el, CommandNode parent)
		{
			IEnumerable<XElement> els = el.Elements();
			foreach (XElement i in els)
			{
				if (string.Equals("descriptions", i.Name.ToString(), StringComparison.OrdinalIgnoreCase))
				{
					DescriptionsCommand cmdDescriptions = new DescriptionsCommand();
					cmdDescriptions.Set(i.ToString());
					parent.Add(cmdDescriptions);
					continue;
				}

				CommandNode cmd = CreateCmd(i);

				if (cmd != null)
				{
					cmd.Parse(i);
					CommandAttribute cmdattr = cmd.GetAttribute<CommandAttribute>();
					if (cmd is ImportCommand)
					{
						ImportCommand ic = cmd as ImportCommand;
						XElement root = ic.XmlLoaded();
						ParseNode(root, parent);
					}
					else if (cmd.IsRunOnConfig)
					{
						ConfigEntry.Add(cmd);
					}
					else if (cmd.IsRunOnLoad)
					{
						LoadEntry.Add(cmd);
					}
					else
					{
						parent.Add(cmd);
						if (i.HasElements)
						{
							ParseNode(i, cmd);
						}
					}
				}
			}
		}

		public bool SetVar(string id, ExecuteParameter var)
		{
			if (string.IsNullOrEmpty(id))
			{
				return false;
			}
			Parameters[id.ToLower()] = var;
			return true;
		}
		public bool SetVar(string id, bool isCondition = true, object value = null)
		{
			SetVar(id, new ExecuteParameter { IsCondition = isCondition, Value = value, Name = id });
			return true;
		}
		public ExecuteParameter GetVar(string id, bool autoCreate=false)
		{
			try
			{
				if (id == null)
				{
					return null;
				}
				id = id.ToLower();
				if (Parameters.ContainsKey(id))
				{
					ExecuteParameter par = Parameters[id];
					return par;
				}
				else if (autoCreate)
				{
					ExecuteParameter par = new ExecuteParameter { Name = id };
					SetVar(id, par);
					return par;
				}
			}
			catch (Exception e)
			{
				ExceptionHandler.Handle(e);
			}
			return null;
		}
		public string GetString(string key)
		{
			try
			{
				if (key == null)
				{
					return string.Empty;
				}
				key = key.ToLower();
				if (Parameters.ContainsKey(key))
				{
					ExecuteParameter par = Parameters[key];
					if (par.Value != null && par.Value is CommandNode)
					{
						if (par.Value is SetCommand)
						{
							SetCommand c = (SetCommand)par.Value;
							if (c.Visible)
							{
								return c.Content;
							}
							else
							{
								return c.Value != null?c.Value.ToString():c.Content;
							}
						}
						else if (par.Value is ArrayCommand)
						{
							ArrayCommand c = (ArrayCommand)par.Value;
							if (c.Visible)
							{
								return c.SelectedContent;
							}
							else
							{
								return c.ToString();
							}
						}
					}
					else
					{
						if (par.Value != null)
						{
							return par.Value.ToString();
						}
						else
						{
							return string.Empty;
						}
					}
				}
			}
			catch (Exception e)
			{
				ExceptionHandler.Handle(e);
			}
			return null;
		}
		public object GetObject(string key)
		{
			try
			{
				if (key == null)
				{
					return null;
				}
				key = key.ToLower();
				if (Parameters.ContainsKey(key))
				{
					ExecuteParameter par = Parameters[key];
					return par.Value;
				}
			}
			catch (Exception e)
			{
				ExceptionHandler.Handle(e);
			}
			return null;
		}
		public T Get<T>(string key)
		{
			try
			{
				if (key == null)
				{
					return default(T);
				}
				key = key.ToLower();
				if (Parameters.ContainsKey(key))
				{
					ExecuteParameter par = Parameters[key];
					return (T)par.Value;
				}
			}
			catch (Exception e)
			{
				ExceptionHandler.Handle(e);
			}
			return default(T);
		}
		public static Type GetType(string cmdName)
		{
			foreach (Type i in commandTypes)
			{
				string name = i.Name;
				if (string.Equals(name, cmdName, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			return null;
		}

    }
	public class FuncCacheCollection : Dict<string, FuncCache>
	{
		public void Add(string func, CommandNode cmd)
		{
			if (!this.ContainsKey(func) || this[func] == null)
			{
				this[func] = new FuncCache();
			}
			this[func].Add(cmd);
		}
	}
	public class FuncCache : List<CommandNode>
	{
	}
    public class ExecuteState
    {
		public delegate void RunCmdHandler(CommandNode cmd, bool isCompleted = false, CommandResult rlt = null);
		public event RunCmdHandler OnExecCmd;
        public CmdStack ParentCmds = new CmdStack();
        public PosStack Indexes = new PosStack();
        public int CurtIndex
        {
            get
            {
                return Indexes.Read();
            }
        }
        protected CommandNode ParentCmd
        {
            get
            {
                if (ParentCmds.Count > 0)
                {
                    return ParentCmds[0];
                }
                return null;
            }
        }
        public CommandNode CurtCmd
        {
            get
            {
                if (ParentCmd == null)
                {
                    return null;
                }
                if (CurtIndex >= 0 && CurtIndex < ParentCmd.Children.Count)
                {
                    return ParentCmd[CurtIndex] as CommandNode;
                }
                return null;
            }
        }
		protected CommandNode Entry;
		public void Load(CommandNode entry)
		{
			ParentCmds.Clear();
			ParentCmds.Push(entry);
			Indexes.Clear();
			Indexes.Push(0);
			Entry = entry;
		}


		public CommandResult Execute(bool isPreview = false, RunCmdHandler callback = null)
		{
			CommandResult rlt = new CommandResult();
			CommandNode cmd = CurtCmd;

			if (cmd != null)
			{
				if (!string.IsNullOrEmpty(cmd.Id))
				{
					ExecuteParameter par = Executor.Instance.GetVar(cmd.Id);
					if (par != null && par.IsCommand)
					{
						cmd = (CommandNode)par.Value;
					}
				}
				CommandAttribute cmdAttr = cmd.GetAttribute<CommandAttribute>();
				if (cmdAttr != null)
				{
					if (!cmdAttr.IsExecutable || (!cmdAttr.IsPreviewRun && isPreview))
					{
						if (isPreview && callback != null)
						{
							callback(cmd);
						}
						return rlt;
					}
				}
				try
				{
					if (!isPreview)
					{
						OutputHandler.Handle(cmd.Name);
						cmd.ExecStatus = CommandStatus.Executing;
					}
					CommandAttribute att = cmd.GetAttribute<CommandAttribute>();
					if (isPreview)
					{
						if (callback != null)
						{
							if (att != null && att.IsPreviewRun && cmd.Visible)
							{
								rlt = cmd.Execute(isPreview);
							}
							cmd.Index = CurtIndex;
							callback(cmd);
						}
					}
					else
					{
						if (OnExecCmd != null)
						{
							OnExecCmd(cmd);
						}
						if (att == null || !att.IsSkipAfterStart)
						{
							cmd.Index = CurtIndex;
							rlt = cmd.Execute();
							if (OnExecCmd != null)
							{
								OnExecCmd(cmd, true, rlt);
							}
						}
					}
					if (!isPreview && cmd.ExecStatus != CommandStatus.Error)
					{
						cmd.ExecStatus = CommandStatus.Success;
					}
					if (cmd is IfCommand && rlt.IsSuccessful && rlt.BoolResult)
					{
						Enter(cmd);
					}
				}
				catch (Exception e)
				{
					rlt.LastError = e;
					cmd.ExecStatus = CommandStatus.Error;
					cmd.Error = e;
				}
			}
			return rlt;
		}

		public void Reset()
		{
			ParentCmds.Clear();
			ParentCmds.Push(Entry);
			Indexes.Clear();
			Indexes.Push(0);
		}

		public void Enter(CommandNode cmd)
		{
			ParentCmds.Push(cmd);
			Indexes.Push(-1);
		}

        public bool Next()
        {
			try
			{
				CommandNode next = Get(CurtIndex + 1);
				if (next == null)
				{
					while (ParentCmds.Count > 0)
					{
						Leave();
						next = Get(CurtIndex);
						if (next != null)
						{
							return true;
						}
					}
					return false;
				}
				else
				{
					Increase();
					return true;
				}
			}
			catch (Exception e)
			{
				ExceptionHandler.Handle(e);
				return false;
			}
        }

		private void Leave()
		{
			ParentCmds.Pop();
			Indexes.Pop();
			if (Indexes.Count > 0)
			{
				Indexes[0]++;
			}
		}

		private void Increase()
		{
			if (Indexes.Count > 0)
			{
				Indexes[0]++;
			}
		}
        protected CommandNode Get(int index)
        {
            if (ParentCmd == null)
            {
                return null;
            }
            if (index >= 0 && index < ParentCmd.Children.Count)
            {
                return ParentCmd[index] as CommandNode;
            }
            return null;
        }
    }
    public class PosStack : List<int>
    {
        public void Push(int i)
        {
            this.Insert(0, i);
        }
        public int Read()
        {
            int rlt = -1;
            if (Count > 0)
            {
                rlt = this[0];
            }
            return rlt;
        }
        public int Pop()
        {
            int rlt = Read();
            if (rlt >= 0)
            {
                RemoveAt(0);
            }
            return rlt;
        }
    }
    public class CmdStack : List<CommandNode>
    {
        public void Push(CommandNode cmd)
        {
            this.Insert(0, cmd);
        }
        public CommandNode Read()
        {
            CommandNode rlt = null;
            if (this.Count > 0)
            {
                rlt = this[0];
            }
            return rlt;
        }
        public CommandNode Pop()
        {
            CommandNode rlt = Read();
            if (rlt != null || Count > 0)
            {
                this.RemoveAt(0);
            }
            return rlt;
        }
    }
    public class ExecuteParameter
    {
        public string Name;
        public bool IsCondition;
        public bool IsTrue
        {
            get
            {
                if (IsCondition)
                {
					string v = null;
					if (Value is SetCommand)
					{
						v = ((SetCommand)Value).Content;
					}
					else
					{
						v = (string)Value;
					}
                    return string.Equals("true", v, StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    return Value != null;
                }
            }
        }
		public object Value;
		public bool IsCommand
		{
			get
			{
				return Value is CommandNode;
			}
		}
        public virtual T GetValue<T>()
        {
            return (T)Value;
        }
    }
	public class ExecutorConfigs : Dict<string, CommandNode>
	{
	}
}
