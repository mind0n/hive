using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CSharpLoader
{
	unsafe public class DllNative
	{
		[DllImport(@"MatrixCubeApp.dll")]
		public static extern long InitDirect3D(void * hWnd);
		[DllImport(@"MatrixCubeApp.dll")]
		public static extern void Render();
		[DllImport(@"MatrixCubeApp.dll")]
		public static extern void CleanUpDirect3D();
	}
}
