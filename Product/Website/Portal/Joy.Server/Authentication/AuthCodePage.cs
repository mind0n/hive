using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Drawing;

namespace Joy.Server.Authentication
{
	public partial class AuthCodePage : System.Web.UI.Page
	{
		private const string STR_Acode = "acode";
		private static readonly Random r = new Random();
		public static int CurrentValue
		{
			get
			{
				if (HttpContext.Current.Session[STR_Acode] != null)
				{
					return (int)HttpContext.Current.Session[STR_Acode];
				}
				else
				{
					return 0;
				}
			}
			protected set
			{
				HttpContext.Current.Session[STR_Acode] = value;
			}
		}
		public static bool IsValidCode(string code)
		{
			int n;
			if (int.TryParse(code, out n))
			{
				return n == CurrentValue;
			}
			return false;
		}
		public static void Update()
		{
			CurrentValue = r.Next(1000, 9999);
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			Response.Clear();
			Update();
			CreateImage(CurrentValue.ToString());
			Response.End();
		}
		private void CreateImage(string checkCode)
		{
			int iwidth = (int)(checkCode.Length * 11.5);
			System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 24);
			Graphics g = Graphics.FromImage(image);
			Font f = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
			Brush b = new System.Drawing.SolidBrush(Color.White);
			g.Clear(Color.Silver);
			g.DrawString(checkCode, f, b, 3, 5);

			Pen blackPen = new Pen(Color.White, 0);
			Random rand = new Random();
			for (int i = 0; i < 3; i++)
			{
				int y = rand.Next(image.Height);
				int x = rand.Next(5);
				int y2 = rand.Next(image.Height);
				g.DrawLine(blackPen, x, y, image.Width, y2);
			}

			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
			Response.ClearContent();
			Response.ContentType = "image/Jpeg";
			Response.BinaryWrite(ms.ToArray());
			g.Dispose();
			image.Dispose();
		}
	}
}
