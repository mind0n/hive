using System;
using System.Net;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;

namespace WebOperator
{
	/**/
	/// <summary>
	/// CrMonterNetImg 的摘要说明。
	/// </summary>
	public class Recognizer
	{
		/**/
		/// <summary>
		/// Cookie集合，使用这个cookie集合去获取验证码
		/// </summary>
		private CookieCollection Cookie = null;

		/**/
		/// <summary>
		/// 验证码的地址
		/// </summary>
		private string ImgUrl = "";

		/**/
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="StrUrl">验证码地址</param>
		/// <param name="Cookies">cookie集合</param>
		public Recognizer(string StrUrl, CookieCollection Cookies)
		{
			this.ImgUrl = StrUrl;
			this.Cookie = Cookies;
		}



		private bool _debug = false;
		/**/
		/// <summary>
		/// 设置是否为debug，如果是debug的话，就把当前图片保存到c:.bmp
		/// </summary>
		public bool debug
		{
			set
			{
				_debug = value;
			}
			get
			{
				return _debug;
			}
		}



		/**/
		/// <summary>
		/// 识别主函数
		/// </summary>
		/// <returns></returns>
		public string proc()
		{
			//图形
			Bitmap bmp = null;
			//保存深浅2种颜色
			Color[] cc = new Color[2];
			int[] left = new int[5];//左边的点
			int[] right = new int[5];//右边的点
			int[] top = new int[5];//顶点
			int[] bottom = new int[5];//底点
			string str = "";
			try
			{
				int i = 0;
				// '抓取图片
				while (i != 4)
				{//一直抓到图形中有4个独立的字符

					bmp = this.crImg();
					if (debug)
					{
						bmp.Save("c:/1.bmp");
					}
					cc = this.GetDeepColor(bmp);//得到深浅颜色
					this.FormatImg(bmp, cc);//格式话图形，去掉背景
					Array array1 = this.GetV(bmp);//得到x轴上有黑色点的坐标
					i = this.chkImg(array1, ref left, ref right);//分析字符个数
				}
				//这里分析单个字符的上下左右点，这样就可以确定范围了
				this.getTB(bmp, left, right, ref top, ref bottom);

				//识别出来
				str = this.crImg(bmp, left, right, top, bottom);

				//释放资源
				bmp.Dispose();
			}
			catch (Exception exception2)
			{
				//ProjectData.SetProjectError(exception2);
				Exception exception1 = exception2;
				//ProjectData.ClearProjectError();
			}
			finally
			{//释放资源
				if (bmp != null)
				{
					bmp.Dispose();
				}
			}
			return str;
		}


		#region 其他辅助函数
		/**/
		/// <summary>
		/// 加载验证码
		/// </summary>
		/// <returns>验证码图形</returns>
		private Bitmap crImg()
		{
			Bitmap bmp = null;
			//响应
			HttpWebResponse response = null;
			try
			{
				//请求
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.ImgUrl);

				//这里附加cookie
				request.CookieContainer = new CookieContainer();
				if (this.Cookie != null)
				{
					request.CookieContainer.Add(this.Cookie);
				}
				//得到响应
				response = (HttpWebResponse)request.GetResponse();
				//从响应流直接创建图片
				bmp = new Bitmap(response.GetResponseStream());
			}
			catch
			{
			}
			finally
			{//清理资源
				if (response != null)
				{
					response.Close();
				}
			}
			return bmp;
		}


		/**/
		/// <summary>
		/// "得到垂直投影"，就是得到字符在y轴上有的点
		/// </summary>
		/// <param name="img">图形</param>
		/// <returns>返回这些点所在的array</returns>
		private Array GetV(Bitmap img)
		{
			string str = "";

			for (int i = 0; i <= img.Width - 1; i++)
			{//在图形的x轴上进行循环

				//然后在y轴上找黑点
				for (int h = 0; h <= img.Height - 1; h++)
				{
					Color c = img.GetPixel(i, h);
					if (c.ToArgb() == Color.Black.ToArgb())
					{//如果找到一个黑点的话，就把这个x轴的坐标给记忆下来。
						str = str + "," + i.ToString();
						break;
					}
				}
			}
			if (str.Length > 0)
			{//因为上面采用连接的方式，所以第一个字符可能是“,”，这里去掉
				str = str.Substring(1);
			}
			return str.Split(new char[] { ',' });
		}



		/**/
		/// <summary>
		/// 查找单个字符中图形中最上和最下的黑点。
		/// </summary>
		/// <param name="Img">图形</param>
		/// <param name="IntS">四个字符的左边点</param>
		/// <param name="IntE">四个字符的右边点</param>
		/// <param name="T">传出参数：保存四个字符顶点</param>
		/// <param name="B">传出参数：保存四个字符底点</param>
		/// <returns></returns>
		private object getTB(Bitmap Img, int[] IntS, int[] IntE, ref int[] T, ref int[] B)
		{
			//用来计数，最多四个数字。
			int i = 0;
			while (true)
			{//一个循环处理一个图形

				for (int w = IntS[i]; w <= IntE[i]; w++)
				{//对其中的一个字符从左到右开始找
					int h = 0;
					do
					{//然后在上面指定的范围内查找最上黑点和最下黑点的位置
						Color c1 = Img.GetPixel(w, h);
						if (c1.ToArgb() == Color.Black.ToArgb())
						{
							if (T[i] == 0)
							{
								T[i] = h;
							}
							if (h < T[i])
							{
								T[i] = h;
							}
							if (h > B[i])
							{
								B[i] = h;
							}
						}
						h++;
					}
					while (h <= 0x13);
				}


				i++;
				if (i > 3)
				{
					return null;
				}
			}




		}

		/**/
		/// <summary>
		/// 得到深色、浅色--深色的RGB刚好相差30
		/// </summary>
		/// <param name="Img"></param>
		/// <returns></returns>
		private Color[] GetDeepColor(Bitmap Img)
		{
			Color c = Color.White;//深色
			Color c1 = Color.White;//浅色
			Color[] cc = new Color[3];
			if (Img == null)
			{//检查图片是否有效
				return null;
			}

			for (int w = 0; w <= Img.Width - 1; w++)
			{//从做到右开始找。
				int h = 4;
				do
				{//经过观察，y方向4-6坐标的三行绝对可以找到这2种颜色

					Color temp = Img.GetPixel(w, h);// 当前色
					if (temp.R < 0xff)
					{//'有颜色的时候才在处理
						if (c.R > 0)
						{
							//用找到的颜色和当前颜色比较
							//'如果找到的颜色比当前颜色要小
							// '那么 c就是深色
							// 'c1就是浅色
							if (c.R < temp.R)
							{
								c1 = temp;
							}
							if (c.R > temp.R)
							{
								c1 = c;
								c = temp;
							}
						}
						else
						{
							c = temp;
						}
						if (c1.ToArgb() > 0)
						{
							break;
						}
					}
					h++;
				}
				while (h <= 6);
			}
			cc[0] = c;
			cc[1] = c1;
			return cc;
		}

		/**/
		/// <summary>
		/// 去背景，单色显示
		/// </summary>
		/// <param name="Img"></param>
		/// <param name="Cc"></param>
		/// <returns></returns>
		private object FormatImg(Bitmap Img, Color[] Cc)
		{
			object obj = null;

			//循环处理每一点
			for (int w = 0; w <= Img.Width - 1; w++)
			{
				for (int h = 0; h <= Img.Height - 1; h++)
				{
					//如果当前颜色是深色的话，就转化成黑色
					//否则就转化成白色
					Color c = Img.GetPixel(w, h);
					if (c.ToArgb() == Cc[1].ToArgb())
					{
						Img.SetPixel(w, h, Color.White);
					}
					if (c.ToArgb() == Cc[0].ToArgb())
					{
						Img.SetPixel(w, h, Color.Black);
					}
				}
			}
			return obj;
		}


		/**/
		/// <summary>
		/// 识别图形的字符
		/// </summary>
		/// <param name="Img">图形</param>
		/// <param name="IntS">计算出来的左边点</param>
		/// <param name="Inte">计算出来的上面点</param>
		/// <param name="T">计算出来的上面点</param>
		/// <param name="B">计算出来的顶点</param>
		/// <returns>字符</returns>
		private string crImg(Bitmap Img, int[] IntS, int[] Inte, int[] T, int[] B)
		{
			string str = "";
			int i = 0;
			do
			{//依次处理4个字符

				//获取当前范围：四个构造函数分别是x,y ，宽和高，已经可以确定字符的范围了。
				Rectangle r = new Rectangle(IntS[i], T[i], (Inte[i] - IntS[i]) + 1, (B[i] - T[i]) + 1);
				//裁减出来的图形
				Bitmap bmp = Img.Clone(r, Img.PixelFormat);
				//识别并保存出来
				str = str + this.crImg(bmp);
				//释放当前图片资源
				bmp.Dispose();

				//处理下一个。
				i++;
			}
			while (i <= 3);
			return str;
		}






		/**/
		/// <summary>
		/// 检测当前的图片是否符合社识别条件，通过分析字符数量
		/// </summary>
		/// <param name="Va"></param>
		/// <param name="IntS">ref：用来保存四个字符的左边点</param>
		/// <param name="IntE">ref：用来保存四个字符的右边点</param>
		/// <returns>字符数量</returns>
		private int chkImg(Array Va, ref int[] intS, ref int[] intE)
		{
			int IntRang = 0; //一共找到多少段

			string[] str = (string[])Va;
			intS = new int[] { -1, -1, -1, -1, -1, -1, -1 };  //保存段的开始数字,多申请几个，避免出问题
			intE = new int[] { -1, -1, -1, -1, -1, -1, -1 };  //保存段的结束数字,多申请几个，避免出问题

			for (int i = 0; i <= str.Length - 2; i++)
			{
				int intCur = int.Parse(str[i]);//当前坐标
				int IntPrew = int.Parse(str[i + 1]);//下一个坐标
				if ((IntPrew - intCur) == 1)
				{//如果2个数字相差一，表示这个数字还是连续的

					if (intS[IntRang] == -1)
					{//如果已经找到结尾了
						intS[IntRang] = intCur;
					}
					if (i == (str.Length - 2))
					{ //如果到了倒数第二数字，最后一个数字就是结束了
						intE[IntRang] = IntPrew;
					}
				}
				else
				{
					intE[IntRang] = intCur;
					IntRang++;
				}
			}
			return (IntRang + 1);
		}


		/**/
		/// <summary>
		/// 识别小图片中的数字
		/// </summary>
		/// <param name="Img">小图形</param>
		/// <returns>识别出来的字符</returns>
		private string crImg(Bitmap Img)
		{
			if (Img != null)
			{//检查图片
				Color c;


				if ((Img.Width == 6) & (Img.Height == 9))
				{//宽6 高9的一定是1
					return "1";
				}


				if ((Img.Width == 9) & (Img.Height == 12))
				{//宽9 高12的一定是4
					return "4";
				}

				if ((Img.Width == 8) & (Img.Height == 9))
				{//如果宽是8 高是9的话

					//找特殊点
					Color c2 = Img.GetPixel(0, 1);
					if (c2.ToArgb() == Color.Black.ToArgb())
					{//如果0，1是黑点的话，就是2，否则就是0
						return "2";
					}
					return "0";
				}

				if ((Img.Width == 8) & (Img.Height == 11))
				{//宽8  高11
					c = Img.GetPixel(2, 0);
					if (c.ToArgb() == Color.Black.ToArgb())
					{//也同样找特殊点来区分2.0
						return "8";
					}
					return "6";
				}
				if ((Img.Width == 8) & (Img.Height == 12))
				{//同上的方法区分3.5.7.9
					//都可以使用特殊点来区分
					c = Img.GetPixel(5, 2);
					if (c.ToArgb() == Color.Black.ToArgb())
					{
						return "3";
					}
					c = Img.GetPixel(2, 4);
					if (c.ToArgb() == Color.Black.ToArgb())
					{
						return "5";
					}
					c = Img.GetPixel(0, 0);
					if (c.ToArgb() == Color.Black.ToArgb())
					{
						return "7";
					}
					c = Img.GetPixel(0, 5);
					if (c.ToArgb() == Color.Black.ToArgb())
					{
						return "9";
					}
				}
			}
			return " ";
		}
		#endregion

	}
}
