using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Web;
using System.Web.UI;
using System.IO;

using System.Drawing.Drawing2D;
using Joy.Server.Web.Authentication;
using Joy.Server.Drawing;
using Joy.Core;

namespace Joy.Server.Authentication
{
	public class AuthCode
	{
		public static string GetRandomAuthCode(int intLength, Point Size, out Bitmap pic)
		{
			Bitmap b = new Bitmap(Size.X, Size.Y);
			Graphics g = Graphics.FromImage(b);
			StringBuilder s = new StringBuilder();
			//合法随机显示字符列表
			string strLetters = "!@#$%&*?1234567890!@#$%&*?abcdefghijklmnopqrstuvwxyz!@#$%&*?ABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%&*?国家统计局昨天在官方网站公布《住宅销售价格统计调查方案》征求意见稿，并从即日起至9月30日公开征求公众意见。此次出台的征求意见稿，被视作国家统计局对此前屡遭质疑的房价数据的回应和改进。2010年2月25日，国家统计局发布报告称“2009年70个大中城市房屋销售价格上涨1.5%”，被公众戏称点错小数点。此后，有关进行空置房调查的呼声也不断高涨。昨天发布的《住宅销售价格统计调查方案》征求意见稿，主要从“数据获取方式”和“数据发布的引导方式”两个方面着手改进房价统计，其内容涉及房价的数据来源、调查方式、汇总方式、计算方式，并拟发布与“股指”相类似的房地产价格的定级指数。";

			try
			{
				//HttpContext context = HttpContext.Current;

				////设置输出流图片格式
				//context.Response.ContentType = "image/gif";

				g.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#EDF2F7")), 0, 0, 70, 27);
				Font font = new Font(FontFamily.GenericSerif, 24, FontStyle.Italic, GraphicsUnit.Pixel);
				Random r = new Random();


				//将随机生成的字符串绘制到图片上
				for (int i = 0; i < intLength; i++)
				{
					s.Append(strLetters.Substring(r.Next(0, strLetters.Length - 1), 1));

					g.DrawString(s[s.Length - 1].ToString(), font, new SolidBrush(ColorTranslator.FromHtml("#2265b2")), 4 + i * 14, r.Next(0, 1));
				}
				//                b.Save(context.Response.OutputStream, ImageFormat.Gif);
			}
			catch (Exception e)
			{
				Exceptions.LogOnly(e);
				s.Append(e.Message);
			}
			pic = b;
			return s.ToString();
		}
		public static string GetRamdomAuthCode(int intLength)
		{
			string rlt;
			StringBuilder s = new StringBuilder();
			string strLetters = "2345678ABCDEFGHJKLMNPQRSTUVWXYZ";
			Random r = new Random();
			for (int i = 0; i < intLength; i++)
			{
				s.Append(strLetters.Substring(r.Next(0, strLetters.Length - 1), 1));
			}
			rlt = s.ToString();
			Authenticator.AuthCode = rlt;
			return rlt;
		}
		public static Bitmap GetStringBitmap(string content, Font font, Color color, Size size)
		{
			int x = 0, y = 0;
			Graphics gmp = Graphics.FromImage(new Bitmap(1, 1));
			if (size == Size.Empty)
			{
				size = gmp.MeasureString(content, font).ToSize();
			}
			Bitmap pic = new Bitmap(size.Width, size.Height);
			//OffsetImage oi = OffsetImage.New(pic);

			Graphics g = Graphics.FromImage(pic);
			Random r = new Random();
			RotateMatrix rm;
			Brush fill;
			//g.FillRectangle(Brushes.Wheat, 0, 0, pic.Width, pic.Height);
			foreach (char c in content)
			{
				string ch = new string(c, 1);
				int width = (int)gmp.MeasureString(ch, font).Width;
				int height = (int)gmp.MeasureString(ch, font).Height;

				float angle = r.Next(-35, 35);
				fill = new LinearGradientBrush(new Point(x, y), new Point(pic.Width / 2, pic.Height / 2), color, Color.Wheat);
				rm = RenderHelper.Rotate(angle, width / 2, height / 2, x, y);

				x += width / 5 * 3;
				y = r.Next(-pic.Height / 7, pic.Height / 7);

				OffsetImage oi = OffsetImage.New(new Bitmap(size.Width, size.Height));
				RenderHelper.DrawString(ref oi, ch, font, fill, rm);
				RenderHelper.DrawImage(g, oi);
				oi.Dispose();
				//g.Transform = rm.Matrix;
				//g.DrawString(new string(c, 1), font, fill, new PointF(x, y));
				fill.Dispose();
			}
			gmp.Dispose();
			g.Dispose();
			return pic;
		}
		public static MemoryStream GetBitmapStream(Bitmap bitmap, ImageFormat format)
		{
			MemoryStream rlt = new MemoryStream();
			bitmap.Save(rlt, format);
			bitmap.Dispose();
			return rlt;
		}
		public static byte[] MakeAuthCode(int length, Color color, Font font, Size size)
		{
			MemoryStream rlt = GetBitmapStream(GetStringBitmap(GetRamdomAuthCode(length), font, color, size), ImageFormat.Png);
			string s = Authenticator.AuthCode;
			return rlt.ToArray();
		}
		public static byte[] MakeAuthCode(int length, Color color, int fontSize, Size size)
		{
			Font font = new Font(FontFamily.GenericSerif, fontSize, FontStyle.Italic, GraphicsUnit.Pixel);
			return MakeAuthCode(length, color, font, size);
		}
		public static byte[] MakeAuthCode(int length, Color color, int fontSize)
		{
			Font font = new Font(FontFamily.GenericSerif, fontSize, FontStyle.Italic, GraphicsUnit.Pixel);
			return MakeAuthCode(length, color, font, Size.Empty);
		}
		public static byte[] MakeAuthCode(int length, Color color)
		{
			Font font = new Font(FontFamily.GenericSerif, 24, FontStyle.Italic, GraphicsUnit.Pixel);
			return MakeAuthCode(length, color, font, Size.Empty);
		}
		public static byte[] MakeAuthCode(int length)
		{
			return MakeAuthCode(length, Color.Maroon);
		}
	}
}
