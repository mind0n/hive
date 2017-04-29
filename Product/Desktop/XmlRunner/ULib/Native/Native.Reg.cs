using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ULib.Native
{
	/// <summary>
	/// Use windows API to operate registry.
	/// </summary>
	public partial class Native
	{
		public static UIntPtr HKEY_CLASSES_ROOT = (UIntPtr)0x80000000;
		public static UIntPtr HKEY_CURRENT_USER = (UIntPtr)0x80000001;
		public static UIntPtr HKEY_LOCAL_MACHINE = (UIntPtr)0x80000002;
		public static UIntPtr HKEY_USERS = (UIntPtr)0x80000003;
		public static UIntPtr HKEY_CURRENT_CONFIG = (UIntPtr)0x80000005;

		// 关闭64位（文件系统）的操作转向 35         
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

		// 开启64位（文件系统）的操作转向 38         
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

		// 获取操作Key值句柄 42         
		[DllImport("Advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern uint RegOpenKeyEx(UIntPtr hKey, string lpSubKey, uint ulOptions, int samDesired, out IntPtr phkResult);

		//关闭注册表转向（禁用特定项的注册表反射） 45         
		[DllImport("Advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern long RegDisableReflectionKey(IntPtr hKey);

		//使能注册表转向（开启特定项的注册表反射） 48         
		[DllImport("Advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern long RegEnableReflectionKey(IntPtr hKey);

		//获取Key值（即：Key值句柄所标志的Key对象的值） 51         
		[DllImport("Advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int RegQueryValueEx(IntPtr hKey, string lpValueName, int lpReserved, out uint lpType, System.Text.StringBuilder lpData, ref uint lpcbData);

		/// <summary>
		/// Get 64-bit registry value.
		/// </summary>
		/// <param name="parentKeyName"></param>
		/// <param name="subKeyName"></param>
		/// <param name="keyName"></param>
		/// <returns></returns>
		public static string Get64BitValue(UIntPtr hKey, string subKeyName, string keyName)
		{
			int KEY_QUERY_VALUE = (0x0001);
			int KEY_WOW64_64KEY = (0x0100);
			int KEY_ALL_WOW64 = (KEY_QUERY_VALUE | KEY_WOW64_64KEY);
			try
			{

				//声明将要获取Key值的句柄 87                
				IntPtr pHKey = IntPtr.Zero;
				//记录读取到的Key值 90                
				StringBuilder result = new StringBuilder("".PadLeft(1024));
				uint resultSize = 1024;
				uint lpType = 0;
				//关闭文件系统转向  95                
				IntPtr oldWOW64State = new IntPtr();
				if (Wow64DisableWow64FsRedirection(ref oldWOW64State))
				{
					//获得操作Key值的句柄 99                    
					RegOpenKeyEx(hKey, subKeyName, 0, KEY_ALL_WOW64, out pHKey);
					//关闭注册表转向（禁止特定项的注册表反射）102                     
					RegDisableReflectionKey(pHKey);
					//获取访问的Key值105                    
					RegQueryValueEx(pHKey, keyName, 0, out lpType, result, ref resultSize);
					//打开注册表转向（开启特定项的注册表反射）108                     
					RegEnableReflectionKey(pHKey);
				}
				//打开文件系统转向112                 
				Wow64RevertWow64FsRedirection(oldWOW64State);
				//返回Key值115                
				return result.ToString().Trim();
			}
			catch (Exception ex)
			{
				return null;
			}
		}

	}
}
