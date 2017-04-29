using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Native.API;

namespace TestDeskWall
{
	class Program
	{
		static bool DesktopEnum(string desktop, IntPtr lParam)
		{
			Console.WriteLine(desktop);
			return true;
		}
		static void Main(string[] args)
		{
			WinAPI.EnumDesktops(IntPtr.Zero, DesktopEnum, IntPtr.Zero);
			Console.ReadKey();
		}
	}
}
