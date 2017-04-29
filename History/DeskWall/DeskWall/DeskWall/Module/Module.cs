using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Timers;


namespace Dw.Module
{
	public class WindowItem : ModuleBase
	{
		public System.Windows.Forms.Form Window;
	}
	public class PluginItem : ModuleBase
	{
		public Assembly Asm;
		public FunctionModule Module;
		public string BaseDir;
		public new bool IsRegisted
		{
			protected internal set
			{
				_isRegisted = value;
			}
			get
			{
				return _isRegisted;
			}
		}
		public bool IsModuleStuck = false;
		public bool IsTerminating = false;
		public bool IsTerminated = false;
	}
	public interface IStartable
	{
		void Start();
		void Stop();
	}
	public class ModuleBase
	{
		public bool IsRegisted
		{
			get
			{
				return _isRegisted;
			}
		}
		internal bool _isRegisted;
		public string Name
		{
			get
			{
				return _name;
			}
			protected internal set
			{
				_name = value;
			}
		}
		internal string _name;
		public bool IsSingleton
		{
			get
			{
				return _isSingleton;
			}
			protected set
			{
				_isSingleton = value;
			}
		}
		internal bool _isSingleton;
	}
	public abstract class FunctionModule : ModuleBase, IStartable
	{
		public string ConfigFilename
		{
			get
			{
				return _cfgFile;
			}
			set
			{
				_cfgFile = value;
			}
		}protected internal string _cfgFile;
		public string FullConfigFilename
		{
			get
			{
				return ConfigFilePath + ConfigFilename;
			}
		}
		public string ConfigFilePath
		{
		    get
		    {
		        return _cfgPath;
		    }
		    protected internal set
		    {
		        _cfgPath = value;
		    }
		}
		protected internal string _cfgPath;
		public string BaseDir
		{
			get
			{
				return _baseDir;
			}
			internal set
			{
				int pos = value.LastIndexOf('\\');
				_baseDir = value.Substring(0, pos);
				_cfgPath = value; //_baseDir + "\\" + value.Substring(pos + 1, value.Length - pos - 1);
			}
		}
		internal string _baseDir;
		internal Thread BelongThread;
		//protected internal object Creator;

		//private static ModuleBase _instance;
		public bool IsReady = false;
		public bool IsTerminating = false;
		public bool IsTerminated = false;

		protected static object Lock = new object();
		protected static FunctionModule _instance;

		public virtual void Start() { }
		public virtual void Stop() { }
        protected internal virtual void Init() { }
		public void StopModule()
		{
			IsTerminating = true;
			IsReady = false;
			Stop();
			IsTerminated = true;
		}


		protected static T GetInstance<T>() where T : FunctionModule, new()
		{
			if (_instance == null)
			{
				_instance = new T();
			}
			return (T)_instance;
		}

		//protected internal virtual void SetCreator(object creator)
		//{
		//    Creator = creator;
		//}
	}
}
