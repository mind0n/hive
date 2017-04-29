using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Net;
using System.IO;
using System.Management;
using System.Drawing;
using System.Drawing.Imaging;
using Joy.Common;
using Joy.Core.Logging;

namespace Joy.Server.Net
{
	public class TransferResult
	{
		public string Result = string.Empty;
		public Encoding ContentEncoding = Encoding.Default;
		public string ContentType = "text/html";
		public bool Success = false;
		public HttpResponse RemoteResponse;
		public void Respond()
		{
			HttpResponse response = HttpContext.Current.Response;
			if (response != null && Success)
			{
				response.ContentEncoding = ContentEncoding;
				response.ContentType = ContentType;
				if (!string.IsNullOrEmpty(Result))
				{
					response.BinaryWrite(ContentEncoding.GetBytes(Result));
				}
			}
		}
		public void ImageRespond()
		{
			HttpResponse response = HttpContext.Current.Response;
			response.ClearHeaders();
			response.ClearContent();
			
		}
	}
	public class NetHelper
	{
		/// <summary>
		/// Get MAC address of the computer.
		/// </summary>
		/// <returns></returns>
		public static string GetActiveLocalMacAddress()
		{
			ManagementClass mc;
			ManagementObjectCollection moc;
			mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
			moc = mc.GetInstances();
			string str = "";
			foreach (ManagementObject mo in moc)
			{
				if ((bool)mo["IPEnabled"] == true)
				{
					str = mo["MacAddress"].ToString();
				}
			}
			return str.Replace(":", "");
		}
		public static string ResolveUrl(string url)
		{
			if (!string.IsNullOrEmpty(url) && url.IndexOf(':') < 0)
			{
				Uri uri = HttpContext.Current.Request.Url;
				url = uri.Scheme + "://" + uri.Authority + url;
			}
			return url;
		}
		public static TransferResult Transfer(string url)
		{
			return Transfer(url, null);
		}
		/// <summary>
		/// Access specified url.
		/// </summary>
		/// <returns></returns>
		public static TransferResult Transfer(string url, CookieContainer cookieContainer)
		{
			TransferResult rlt = new TransferResult();
			try
			{
				url = ResolveUrl(url);
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
				if (cookieContainer != null)
				{
					request.CookieContainer = cookieContainer;
				}
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
				rlt.Result = reader.ReadToEnd();
				rlt.ContentEncoding = reader.CurrentEncoding;
				rlt.ContentType = response.ContentType;
				rlt.Success = true;
			}
			catch (Exception ex)
			{
				Exceptions.LogOnly(ex);
				rlt.Success = false;
			}
			return rlt;
		}
		public static Stream BinaryTransfer(string url)
		{
			return BinaryTransfer(url, null);
		}
		public static Stream BinaryTransfer(string url, CookieContainer cookieContainer)
		{
			//TransferResult rlt = new TransferResult();
			try
			{
				url = ResolveUrl(url);
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
				if (cookieContainer != null)
				{
					request.CookieContainer = cookieContainer;
				}
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				Stream stream = response.GetResponseStream();
				return stream;
				//rlt.Result = reader.ReadToEnd();
				//rlt.ContentEncoding = reader.CurrentEncoding;
				//rlt.ContentType = response.ContentType;
				//rlt.Success = true;
			}
			catch (Exception ex)
			{
				Exceptions.LogOnly(ex);
				return null;
			}
		}
		//public static MemoryStream TransferImage(string url)
		//{
		//    Image img;
		//    MemoryStream rlt = new MemoryStream();
		//    //TransferResult rlt = new TransferResult();
		//    try
		//    {
		//        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
		//        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
		//        Stream stream = response.GetResponseStream();
		//        img = Image.FromStream(stream);
		//        img.Save(rlt, ImageFormat.Png);

		//        //rlt.Result = reader.ReadToEnd();
		//        //rlt.ContentEncoding = reader.CurrentEncoding;
		//        //rlt.ContentType = response.ContentType;
		//        //rlt.Success = true;
		//    }
		//    catch (Exception ex)
		//    {
		//        Exceptions.LogOnly(ex);
		//        rlt = null;
		//    }
		//    return rlt;
		//}
	}
}
