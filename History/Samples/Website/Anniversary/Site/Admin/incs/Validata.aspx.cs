using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Imaging;

namespace Site.Admin.incs
{
    public partial class Validata : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["code"] = RandNum(4);
            ValidateCode(Session["code"].ToString(), 50, 25, "黑体", 15, "#f2f2f2");   // (随机数,图片宽,图片高,字体,字号，背景)     
        }

        /// <summary>
        /// 该方法用于生成指定位数的随机数
        /// </summary>
        /// <param name="VcodeNum">参数是随机数的位数</param>
        /// <returns>返回一个随机数字符串</returns>
        private string RandNum(int VcodeNum)
        {
            string Vchar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,g,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            string[] VcArray = Vchar.Split(',');//拆分成数组
            string VNum = "";
            int temp = -1;//记录上次随机数值，尽量避避免生产几个一样的随机数

            Random rand = new Random();
            //采用一个简单的算法以保证生成随机数的不同
            for (int i = 0; i < VcodeNum; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(VcArray.Length - 1);
                if (temp != -1 && temp == t)
                {
                    return RandNum(VcodeNum);

                }
                temp = t;
                VNum += VcArray[t];
            }
            return VNum;
        }

        /// <summary>
        /// 生成图片并写入字符
        /// </summary>
        /// <param name="VNum">目标字符</param>
        /// <param name="w">宽</param>
        /// <param name="h">高</param>
        /// <param name="font">字体文件</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="bgColor">图片背景颜色</param>
        private void ValidateCode(string VNum, int w, int h, string font, int fontSize, string bgColor)
        {
            Bitmap Img = new Bitmap(w, h);//生成图像的实例
            Graphics g = Graphics.FromImage(Img);//从Img对象生成新的Graphics对象
            g.Clear(ColorTranslator.FromHtml(bgColor));//背景颜色
            Font f = new Font(font, fontSize);//生成Font类的实例
            SolidBrush s = new SolidBrush(Color.Red);//生成笔刷类的实例
            g.DrawString(VNum, f, s, 1, 1);//将VNum写入图片中
            Img.Save(Response.OutputStream, ImageFormat.Jpeg);//将此图像以Jpeg图像文件的格式保存到流中
            Response.ContentType = "image/Jpeg";
            //回收资源
            g.Dispose();
            Img.Dispose();
            Response.End();
        }

    }
}
