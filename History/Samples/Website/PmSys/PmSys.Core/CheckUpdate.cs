/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				CheckUpdate.cs	                                        			*
 *      Description:																*
 *				 检测更新类   													    *
 *      Author:																		*
 *				Lzppcc														        *
 *				Lzppcc@hotmail.com													*
 *				http://www.supesoft.com												*
 *      Finish DateTime:															*
 *				2007年8月6日														*
 *      History:																	*
 ***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

using PmSys.Components;
using System.Reflection;
using System.Diagnostics;


namespace PmSys
{
    /// <summary>
    /// 检测更新类
    /// </summary>
    public class CheckUpdate
    {
        #region "Private Variables"
        private  bool _CheckOk = false;
        private  bool _CheckNewVer = false;
        private static bool _CheckLicensed = true;
        private  int  _CheckCount = 0;
        private  sys_FrameWorkInfoTable _CheckReturn = new sys_FrameWorkInfoTable();
        #endregion

        #region "Public Variables"
        /// <summary>
        /// Licensed是否正确
        /// </summary>
        public static bool CheckLicensed
        {
            get {
                return _CheckLicensed;
            }
            set {
                _CheckLicensed = value;
            }
        }

        /// <summary>
        /// 是否完成检测
        /// </summary>
        public  bool CheckOk
        {
            get {
                return _CheckOk;
            }
            set {
                _CheckOk = value;
            }
        }
        /// <summary>
        /// 是否有最新版本
        /// </summary>
        public  bool CheckNewVer
        {
            get {
                return _CheckNewVer;
            }
            set {
                _CheckNewVer = value;
            }
        }
        /// <summary>
        /// 远程返回当前最新版本内容
        /// </summary>
        public  sys_FrameWorkInfoTable CheckReturn
        {
            get {
                return _CheckReturn;
            }
            set {
                _CheckReturn = value;
            }
        }
        /// <summary>
        /// 检测次数
        /// </summary>
        public int CheckCount
        {
            get {
                return _CheckCount;
            }
        }
        #endregion

        /// <summary>
        /// 发送接收返回方法
        /// </summary>
        public  void SendDataWeb()
        {
            CheckOk = true;
            if (_CheckCount < 3)
            {
                _CheckCount++;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(FrameSystemInfo.GetSystemInfoTable.S_FrameWorkInfo.S_UpdateUrl);
                request.Timeout = 6000;
                request.Method = "POST";
                byte[] byteArray = FrameSystemInfo.Serializable_sys_FrameWorkInfoTable(FrameSystemInfo.GetSystemInfoTable.S_FrameWorkInfo);
                request.ContentLength = byteArray.Length;
                try
                {
                    bool a = request.HaveResponse;

                    Stream dataStream = request.GetRequestStream();

                    //if (dataStream.CanWrite)
                    //{
                    //    SendStateObject state = new SendStateObject();
                    //    state.rq = request;
                    //    state.str = dataStream;
                    //     dataStream.BeginWrite(byteArray, 0, byteArray.Length, new AsyncCallback(SendCallBack), state);
                    //}
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    //// Close the Stream object.
                    dataStream.Close();
                    //// Get the response.
                    WebResponse response = request.GetResponse();
                    //// Display the status.

                    ////Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                    dataStream = response.GetResponseStream();

                    sys_FrameWorkInfoTable Webfi = new BinaryFormatter().Deserialize(dataStream) as sys_FrameWorkInfoTable;
                    //CheckOk = true;
                    CheckReturn = Webfi;
                    if (Convert.ToDecimal(FrameSystemInfo.GetSystemInfoTable.S_FrameWorkInfo.S_Version.Replace(".", "")) < Convert.ToDecimal(Convert.ToDecimal(Webfi.S_Version.Replace(".", ""))))
                    {
                        CheckNewVer = true;
                    }
                    //更新Licensed验证字段
                    if (Webfi.S_Licensed == "False")
                        _CheckLicensed = false;

                    //sys_FrameWorkInfoTable Webfi = new BinaryFormatter().Deserialize(dataStream) as sys_FrameWorkInfoTable;

                    //CheckOk = true;
                    //CheckReturn = Webfi;
                    //if (Convert.ToDecimal(FrameSystemInfo.GetSystemInfoTable.S_FrameWorkInfo.S_Version.Replace(".", "")) < Convert.ToDecimal(Webfi.S_Version.Replace(".", "")))
                    //{
                    //    CheckNewVer = true;
                    //}
                    dataStream.Close();
                    response.Close();
                    request.Abort();
                }
                catch 
                {
                    request.Abort();
                    //if (ex.InnerException != null) ex = ex.InnerException;
                    //if (Common.DispError == false)
                    //    FileTxtLogs.WriteLog(ex.Source + " thrown " + ex.GetType().ToString() + "<br />" + ex.Message.Replace("\r", "").Replace("\n", "<br />") + "<br />" + ex.StackTrace.Replace("\r", "").Replace("\n", "<br />"));
                }
            }

        }

        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="rs"></param>
        public  void SendCallBack(IAsyncResult rs)
        {
            if (rs.IsCompleted)
            {
                SendStateObject state = rs.AsyncState as SendStateObject;

                WebRequest rq = state.rq;
                try
                {
                WebResponse response = rq.GetResponse();
                Stream dataStream = state.str;
                dataStream = response.GetResponseStream();

                    sys_FrameWorkInfoTable Webfi = new BinaryFormatter().Deserialize(dataStream) as sys_FrameWorkInfoTable;
                    //CheckOk = true;
                    CheckReturn = Webfi;
                    if (Convert.ToDecimal(FrameSystemInfo.GetSystemInfoTable.S_FrameWorkInfo.S_Version.Replace(".","")) < Convert.ToDecimal(Convert.ToDecimal(Webfi.S_Version.Replace(".",""))))
                    {
                        CheckNewVer = true;
                    }
                    //更新Licensed验证字段
                    if (Webfi.S_Licensed == "False")
                        _CheckLicensed = false;
                }
                //catch (Exception ex)
                //{
                //    if (ex.InnerException != null) ex = ex.InnerException;
                //    if (Common.DispError == false)
                //        FileTxtLogs.WriteLog(ex.Source + " thrown " + ex.GetType().ToString() + "<br />" + ex.Message.Replace("\r", "").Replace("\n", "<br />") + "<br />" + ex.StackTrace.Replace("\r", "").Replace("\n", "<br />"));
                //}
                finally
                {
                    rq.Abort();
                    //response.Close();
                    //dataStream.Close();
                }
            }
        }

        /// <summary>
        /// 回调参数对象
        /// </summary>
        public class SendStateObject
        {
            /// <summary>
            /// 数据类
            /// </summary>
            public Stream str;
            /// <summary>
            /// 发送请求
            /// </summary>
            public WebRequest rq;
        }
        /// <summary>
        /// 获取最新版本信息
        /// </summary>
        public static string GetNewVerInfo
        {
            get {
                string NewTxt = "<font color=red>{0}</font>";
				Assembly asm = Assembly.GetExecutingAssembly();
				FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
				return string.Format(NewTxt, fvi.ProductVersion);
            }
        }
    }
}
