using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FormTestRoom
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void btnRun_Click(object sender, EventArgs e)
		{
			string filename = @"c:\test.wav";
			byte[] bs = File.ReadAllBytes(filename);
			ASCIIEncoding en = new ASCIIEncoding();
			//string output = bs.ToHexString();
			tMy.Text = bs.Length + "\r\n";
			//tMy.Text += output.Length.ToString() + "\r\n";
			string output = Convert.ToBase64String(bs);
			tMy.Text += output.Length.ToString();
			tSys.Text = output;
			byte[] decode = Convert.FromBase64String(output);
			//byte b = 111;
			//MessageBox.Show(b.ToHexString());
			//MessageBox.Show(Ext.HexDict.Count.ToString());
		}
	}
	public static class Ext
	{
		public static byte[] ByteHex = new byte[] { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 65, 66, 67, 68, 69, 70 };
		public static List<char> HexDict 
			= new List<char>() 
			{	'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 
				'A', 'B', 'C', 'D', 'E', 'F', 'G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
				'a', 'b', 'c', 'd', 'e', 'f', 'g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
				'~','@','#','$','%','^','&','*','(',')','_','+','{','}','|',':','"','<','>','?','`','-','=','[',']','\\',';','/','.',',',' '
			};
		static Ext()
		{
		}
		public static string ToHexString(this byte b)
		{
			byte hi = (byte)(b >> 4);
			byte lo = (byte)(b & 0x0f);
			StringBuilder rlt = new StringBuilder();
			rlt.Append(HexDict[hi]);
			rlt.Append(HexDict[lo]);
			return rlt.ToString();
		}
		public static string ToHexString(this byte[] bytes)
		{
			StringBuilder rlt = new StringBuilder();
			foreach (byte b in bytes)
			{
				rlt.Append(b.ToHexString());
			}
			return rlt.ToString();
		}
	}
}
