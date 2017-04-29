using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Fs.Reflection;
using Fs.Xml;
using Dw.Collections;
using Dw.Module;
using Dw.Window;
using Fs;
using Fs.Core;
using Native.Monitor;
using Microsoft.Win32;


namespace Dw.Module
{
	public class ModuleProperty
	{
		public readonly bool IsSingleton;
	}
	public sealed class DeskWall : FunctionModule
	{
		//public delegate bool OnAdjustNotifyIconMenuItemHandler(MenuMerge mergeType, string text, EventHandler onClick);
		public delegate void OnPluginsLoadHandler(DeskWall sender, PluginItem plugin);
		public delegate void OnPluginsLoadCompleteHandler(DeskWall sender, PluginItem plugin);
		public delegate void OnPluginsStartHandler(DeskWall sender, PluginItem plugin);
		public delegate void OnPluginsStartCompleteHandler(DeskWall sender, PluginItem plugin);
		public delegate void EnumPluginDirectoryHandler(string PluginConfigFileFullPath);
		public delegate bool EnumPluginHandler(PluginItem plugin);

		//public OnAdjustNotifyIconMenuItemHandler OnAdjustNotifyIconMenuItem;
		public OnPluginsLoadHandler OnPluginsLoad;
		public OnPluginsLoadCompleteHandler OnPluginsLoadComplete;
		public OnPluginsStartHandler OnPluginsStart;
		public OnPluginsStartCompleteHandler OnPluginsStartComplete;
		public KeyEventHandler OnKeyDown;
		public KeyEventHandler OnKeyUp;

		public bool EnvironmentExitable = true;
		public string PluginDirectory;

		internal XReader ConfigReader
		{
			get
			{
				XReader xr = new XReader(FullConfigFilename);
				return xr;
			}
		}
		private bool LockKeyboard = false;
		internal MainForm MainWindow;
		private static StrongArray Plugins;
		private static StrongArray Windows;
		public void TryExit()
		{
			IsTerminating = true;
			foreach (PluginItem pi in Plugins)
			{
				UnRegistPlugin(pi);
			}
			CleanUnregistedPlugins();
			Terminate();
		}
		public void Terminate()
		{
			if (MainWindow != null)
			{
				((MainForm)MainWindow).CloseWindow();
			}
			if (EnvironmentExitable)
			{
				if (Application.AllowQuit)
				{
					Application.Exit();
				}
				Environment.Exit(0);
			}
		}
		public void SetAutoRun()
		{
			string disableAutoRun = ConfigReader["Config"]["Registry"]["$DisableAutoRun"].Value;
			string allowSetAutoRun = ConfigReader["Config"]["Registry"]["$AllowSetAutoRun"].Value;
			string autoExit = ConfigReader["Config"]["Registry"]["$AutoExit"].Value;
			if (!string.IsNullOrEmpty(autoExit))
			{
				Environment.Exit(0);
				return;
			}
			if (!string.IsNullOrEmpty(allowSetAutoRun) && allowSetAutoRun.ToLower().Equals("true"))
			{
				RegistryReader rlm = new RegistryReader(Registry.LocalMachine);
				rlm = (RegistryReader)rlm.GetChildByPath(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", '\\');
				if (!string.IsNullOrEmpty(disableAutoRun) && disableAutoRun.ToLower().Equals("true"))
				{
					rlm.RemoveValue(Application.ProductName);
				}
				else
				{
					rlm.SetValue(Application.ProductName, Application.ExecutablePath);
				}
			}

		}
		public override void Start()
		{
			bool isUnique;
			Mutex unique = new Mutex(true, "DeskWall - " + Environment.MachineName, out isUnique);
			if (!isUnique)
			{
				Terminate();
			}
		}
		public override void Stop()
		{
			
		}
		public bool RegistWindow(string name, Form window, ModuleBase module)
		{
			WindowItem wi = new WindowItem();
			wi.Name = name;
			wi.Window = window;
			Windows.Add(wi);
			return true;
		}
		public bool RegistStuckPlugin(PluginItem pl)
		{
			int i = Plugins.IndexOf(pl);
			if (i >= 0)
			{
				PluginItem pi = (PluginItem)Plugins[i];
				pi.IsModuleStuck = true;
				return true;
			}
			return false;
		}
		public bool RegistPlugin(PluginItem pl)
		{
			bool rlt;
			ModuleBase module = pl.Module;
			rlt = EnumPluginItem(delegate(PluginItem existedPl)
			{
				ModuleBase existedModule = existedPl.Module;
				if (module.Name.Equals(existedModule.Name))
				{
					if (existedModule.IsSingleton)
					{
						return false;
					}
				}
				return true;
			});
			Plugins.Add(pl);
			pl.IsRegisted = true;
			//* Debug
			Logger.Log("Registing: " + pl.Module.Name + " " + pl.IsRegisted);
			//*/
			return rlt;
		}
		public bool EnumPluginItem(EnumPluginHandler EnumPluginItemCallback)
		{
			bool rlt = true;
			foreach (object obj in Plugins)
			{
				PluginItem pl = (PluginItem)obj;
				rlt = EnumPluginItemCallback(pl);
				if (!rlt)
				{
					break;
				}
			}
			return rlt;
		}
		public FunctionModule GetModuleByName(string moduleName)
		{
			PluginItem pi = GetPluginByModuleName(moduleName);
			if (pi != null)
			{
				return pi.Module;
			}
			return null;
		}
		public static DeskWall GetInstance()
		{
			if (_instance == null)
			{
				_instance = new DeskWall();
			}
			return (DeskWall)_instance;
		}
		public static ModuleBase GetModuleInstanceByTypeName(string TypeName, string DllFullPath)
		{
			Assembly asm = Assembly.LoadFrom(DllFullPath);
			return GetModuleInstanceByTypeName(TypeName, asm, false);
		}
		public static ModuleBase CreateModuleInstanceByTypeName(string TypeName, Assembly asm)
		{
			return GetModuleInstanceByTypeName(TypeName, asm, true);
		}
		public static ModuleBase GetModuleInstanceByTypeName(string TypeName, Assembly asm, bool createNew)
		{
			ModuleBase newModule;
			//Type t = asm.GetType(TypeName);

			foreach (PluginItem p in Plugins)
			{
				ModuleBase existedModule = p.Module;
				if (existedModule.GetType().ToString().Equals(TypeName))
				{
					if (existedModule.IsSingleton)
					{
						if (createNew)
						{
							return null;
						}
						else
						{
							return existedModule;
						}
					}
					else
					{
						break;
					}
				}
			}
			Type t = asm.GetType(TypeName);
			ConstructorInfo ci = t.GetConstructor(new Type[] { });
			if (ci != null)
			{
				newModule = (ModuleBase)asm.CreateInstance(TypeName);
			}
			else
			{
				newModule = (ModuleBase)t.InvokeMember("GetInstance", BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static, null, null, null);
			}
			return newModule;
		}
		private PluginItem GetPluginByModuleName(string pluginName)
		{
			PluginItem rlt = null;
			EnumPluginItem(delegate(PluginItem pl)
			{
				if (pl.Module.Name.Equals(pluginName))
				{
					rlt = pl;
					return false;
				}
				return true;
			});
			return rlt;
		}
		private void ReadConfiguration()
		{
			XReader xr = new XReader(FullConfigFilename);
			BaseDir = AppDomain.CurrentDomain.BaseDirectory;
			PluginDirectory = AppDomain.CurrentDomain.BaseDirectory + xr["Config"]["Plugins"]["$Directory"].Value;
			if (PluginDirectory[PluginDirectory.Length - 1] != '\\')
			{
				PluginDirectory += '\\';
			}

			LoadPlugins();
		}
		private void DirectoryOnEnum(string path)
		{
			XReader xr;
			string rlt = path;
			if (File.Exists(path))
			{
				string typName;
				Assembly asm;
				xr = new XReader(path);
				try
				{
					//instanceType = xr["Plugin"]["$Instance"].Value;
					asm = Assembly.LoadFrom(PluginDirectory + xr.Reset()["Plugin"]["$File"].Value);
					typName = xr.Reset()["Plugin"]["$Type"].Value;
					//						ModuleBase module = (ModuleBase)asm.CreateInstance(typName);
					FunctionModule module = (FunctionModule)CreateModuleInstanceByTypeName(typName, asm);
					if (module == null)
					{
						return;
					}
					module.BaseDir = path;
					//module.ConfigFileFullPath = path + "\\Plugin.Config";
					PluginItem pl = new PluginItem();
					pl.Asm = asm;
					pl.Module = module;
					bool isRegisted = RegistPlugin(pl);
					if (OnPluginsLoad != null)
					{
						//OnPluginsLoad(this, pl);
						ClassHelper.AsyncInvokeDelegates(OnPluginsLoad, DelegateInvoke_PreInvokeCallback, DelegateInvoke_PostInvokeCallback, new object[] { this, pl });
					}
				}
				catch (Exception e)
				{
					//MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Exceptions.Log(e);
				}
			}
		}

		private void LoadPlugins()
		{
			Process p = new Process();
			ProcessStartInfo psi = new ProcessStartInfo();
			psi.CreateNoWindow = true;
			psi.ErrorDialog = false;
			EnumPluginDirectory(DirectoryOnEnum);
			Thread.Sleep(2000);
			if (OnPluginsLoadComplete != null)
			{
				ClassHelper.AsyncInvokeDelegates(OnPluginsLoadComplete, DelegateInvoke_PreInvokeCallback, DelegateInvoke_PostInvokeCallback, new object[] { this, null });
			}
			//StartPlugins();
		}
		private void DelegateInvoke_PreInvokeCallback(object sender, ThreadResults results, object[] pars)
		{
			FunctionModule m = (FunctionModule)sender;
			m.BelongThread = results.Thread;
            Logger.Log("Thread " + sender.ToString() + " started");
		}
		private void DelegateInvoke_PostInvokeCallback(object sender, ThreadResults results, object[] pars)
		{
			//* Debug
			if (results.Err != null)
			{
				Logger.Log(results.Err.ToString());
			}
			else
			{
				Logger.Log("Thread " + sender.ToString() + " ended");
			}
			//*/
		}
		//private void StartPlugins()
		//{
		//    foreach (PluginItem pl in Plugins)
		//    {
		//        StartPlugin(pl);
		//        //* Debug
		//        Logger.Log(pl.Module.Name + " is starting...");
		//        //*/
		//    }
		//    foreach (PluginItem pl in Plugins)
		//    {
		//        pl.Module.StartComplete();
		//        //* Debug
		//        Logger.Log(pl.Module.Name + " is running...");
		//        //*/
		//    }
		//}
		private void StartPlugin(PluginItem pi)
		{
			ThreadStart ts = new ThreadStart(delegate()
			{
				pi.Module.Start();
				UnRegistPlugin(pi);
			});

			Thread th = new Thread(ts);
			th.SetApartmentState(ApartmentState.STA);
			th.Start();
		}
		private void CleanUnregistedPlugins()
		{
			for (int i = Plugins.Count - 1; i >= 0; i--)
			{
				PluginItem pi = (PluginItem)Plugins[i];
				if (!pi.IsRegisted)
				{
					//* Debug
					Logger.Log("Removing " + pi.Module.Name + " from plugin list");
					//*/
					Plugins.Remove(pi);
				}
			}
		}
		private void StopModule(PluginItem pi)
		{
			pi.IsTerminating = true;
			Thread gentleExit = new Thread(new ThreadStart(delegate()
			{
				System.Timers.Timer tmr = new System.Timers.Timer();
				tmr.Interval = 2000;
				tmr.Enabled = true;
				tmr.Elapsed += delegate(object sender, ElapsedEventArgs e)
				{
					tmr.Stop();
					if (!pi.IsTerminated && pi.IsTerminating)
					{
						//* Debug
						Logger.Log("Stuck module detected: " + pi.Module.Name);
						//*/
						RegistStuckPlugin(pi);
					}
				};
				tmr.Start();
				pi.Module.StopModule();
				pi.IsTerminated = true;
			}));
			gentleExit.Start();
		}
		private void UnRegistPlugin(PluginItem pi)
		{
			if (Plugins.Contains(pi))
			{
				StopModule(pi);
				pi.IsRegisted = false;
			}
			//* Debug
			Logger.Log(pi.Module.Name + " unregisted");
			//*/
		}
		private void EnumPluginDirectory(EnumPluginDirectoryHandler DirectoryCallBack)
		{
			DirectoryInfo[] pluginDirInfo = new DirectoryInfo(PluginDirectory).GetDirectories();
			foreach (DirectoryInfo dir in pluginDirInfo)
			{
				DirectoryCallBack(dir.FullName + "\\Plugin.Config");
			}
		}
		private void StartCmdPrompt()
		{
			// 实例一个Process类,启动一个独立进程
			Process p = new Process();

			// 设定程序名
			p.StartInfo.FileName = "cmd.exe";
			p.StartInfo.Arguments = "/k cd c:\\";
			// 关闭Shell的使用
			p.StartInfo.UseShellExecute = false;
			// 重定向标准输入
			//p.StartInfo.RedirectStandardInput = true;
			// 重定向标准输出
			//p.StartInfo.RedirectStandardOutput = true;
			//重定向错误输出
			//p.StartInfo.RedirectStandardError = true;
			// 设置不显示窗口
			//p.StartInfo.CreateNoWindow = true;
			p.Start();
		}

		private void HookManager_KeyUp(object sender, KeyEventArgs e)
		{
			if (OnKeyUp != null)
			{
				OnKeyUp(sender, e);
			}
		}
		private void HookManager_KeyDown(object sender, KeyEventArgs e)
		{
			bool swc = false;
			if (e.KeyCode == Keys.R && KeyboardStatus.Alt && KeyboardStatus.Shift)
			{
				StartCmdPrompt();
			}
			else if (KeyboardStatus.Shift && KeyboardStatus.Ctrl && KeyboardStatus.Alt && e.KeyCode == Keys.End)
			{
				Terminate();
			}
			else if (e.KeyCode == Keys.Back && KeyboardStatus.Ctrl && KeyboardStatus.Alt)
			{
				LockKeyboard = false;
				swc = true;
			}
			else if (e.KeyCode == Keys.End && KeyboardStatus.Ctrl && KeyboardStatus.Alt)
			{
				LockKeyboard = false;
			}
			else if (e.KeyCode == Keys.F12)
			{
				SetAutoRun();
				LockKeyboard = true;
			}
			else if (e.KeyCode == Keys.F4)
			{
				MainWindow.Opacity = 0.01;
				MainWindow.Update();
			}
			else if (e.KeyCode == Keys.F5)
			{
				MainWindow.SwitchImage();
				MainWindow.Opacity = 1;
				MainWindow.Update();
			}
			if (OnKeyDown != null)
			{
				OnKeyDown(sender, e);
				if (e.Handled)
				{
					LockKeyboard = true;
				}
			}
			e.Handled = LockKeyboard;
			if (LockKeyboard)
			{
				MainWindow.LockScreen();
			}
			else
			{
				if (!swc)
				{
					MainWindow.UnlockScreen();
				}
				else
				{
					MainWindow.HideScreen();
				}
			}
		}
		#region Constructors
		private static new DeskWall _instance = new DeskWall();
		private DeskWall()
		{
			HookManager.KeyDown += HookManager_KeyDown;
			HookManager.KeyUp += HookManager_KeyUp;
		}
        protected internal override void Init()
        {
			Plugins = new StrongArray();
			Windows = new StrongArray();
			ReadConfiguration();
        }
		#endregion
	}

}
