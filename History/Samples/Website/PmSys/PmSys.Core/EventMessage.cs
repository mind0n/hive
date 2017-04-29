/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				EventMessage.cs	                                           			*
 *      Description:																*
 *				 日志消息类     												    *
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
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using PmSys.Components;

namespace PmSys
{
    /// <summary>
    /// 日志消息类
    /// </summary>
    public class EventMessage
    {

        /// <summary>
        /// 信息提示类
        /// </summary>
        /// <param name="M_Type">类型1:操作日志2:安全日志</param>
        /// <param name="M_Title">标题</param>
        /// <param name="M_Body">内容</param>
        /// <param name="M_IconType">Icon类型</param>
        /// <param name="Url">Url</param>
        public static void MessageBox(int M_Type, string M_Title, string M_Body, Icon_Type M_IconType,string Url)
        {
            MessageBox(M_Type, M_Title, M_Body, M_IconType, Url,UrlType.Href);
        }

        /// <summary>
        /// 信息提示类
        /// </summary>
        /// <param name="M_Type">类型1:操作日志2:安全日志</param>
        /// <param name="M_Title">标题</param>
        /// <param name="M_Body">内容</param>
        /// <param name="M_IconType">Icon类型</param>
        /// <param name="Url">Url</param>
        /// <param name="ReturnScript">执行Script脚本字符串(需加<script></script>)</param>
        public static void MessageBox(int M_Type, string M_Title, string M_Body, Icon_Type M_IconType, string Url, string ReturnScript)
        {
            MessageBox(M_Type, M_Title, M_Body, M_IconType, true, Url, UrlType.Href, ReturnScript);
        }

        /// <summary>
        /// 信息提示类
        /// </summary>
        /// <param name="M_Type">类型1:操作日志2:安全日志</param>
        /// <param name="M_Title">标题</param>
        /// <param name="M_Body">内容</param>
        /// <param name="M_IconType">Icon类型</param>
        /// <param name="Url">Url</param>
        /// <param name="M_UrlType">按钮链接类型</param>
        public static void MessageBox(int M_Type, string M_Title, string M_Body, Icon_Type M_IconType, string Url,UrlType M_UrlType)
        {
            MessageBox(M_Type, M_Title, M_Body, M_IconType, true, Url, M_UrlType,"");
        }

        /// <summary>
        /// 信息提示类
        /// </summary>
        /// <param name="M_Type">类型1:操作日志2:安全日志</param>
        /// <param name="M_Title">标题</param>
        /// <param name="M_Body">内容</param>
        /// <param name="M_IconType">icon类型</param>
        /// <param name="M_WriteToDB">是否写入DB</param>
        /// <param name="Url">链接地址</param>
        /// <param name="M_UrlType">链接类型</param>
        /// <param name="ReturnScript">执行Script脚本字符串(需加<script></script>)</param>
        public static void MessageBox(int M_Type, string M_Title, string M_Body, Icon_Type M_IconType, bool M_WriteToDB, string Url, UrlType M_UrlType,string ReturnScript)
        {
            List<sys_NavigationUrl> M_ButtonList = new List<sys_NavigationUrl>();
            M_ButtonList.Add(new sys_NavigationUrl("确定", Url, "", M_UrlType, true));
            MessageBox(M_Type, M_Title, M_Body, M_IconType, M_WriteToDB, M_ButtonList,ReturnScript);
        }
        /// <summary>
        ///  信息提示
        /// </summary>
        /// <param name="M_Type">类型1:操作日志2:安全日志</param>
        /// <param name="M_Title">标题</param>
        /// <param name="M_Body">内容</param>
        /// <param name="M_IconType">icon类型</param>
        /// <param name="M_WriteToDB">是否写入db</param>
        /// <param name="M_ButtonList">按钮类型</param>
        /// <param name="M_ReturnScript">执行Script脚本字符串(需加<script></script>)</param>
        public static void MessageBox(int M_Type, string M_Title, string M_Body, Icon_Type M_IconType, bool M_WriteToDB, List<sys_NavigationUrl> M_ButtonList, string M_ReturnScript)
        {
            MessageBox mbx = new MessageBox();
            mbx.M_Body = M_Body;
            mbx.M_ButtonList = M_ButtonList;
            mbx.M_IconType = M_IconType;
            mbx.M_Title = M_Title;
            mbx.M_Type = M_Type;
            mbx.M_WriteToDB = M_WriteToDB;
            mbx.M_ReturnScript = M_ReturnScript;
            MessageBox(mbx);
        }

        /// <summary>
        /// 信息提示
        /// </summary>
        /// <param name="MBx">信息提示类</param>
        public static void MessageBox(MessageBox MBx)
        {
            if (MBx.M_WriteToDB)
            {
                EventWriteDB(MBx.M_Type, MBx.M_Body);
            }
            if (MBx.M_ButtonList.Count > 0)
            {
                System.Web.HttpContext.Current.Response.Cookies[string.Format("{0}-MessageValue",Common.Get_CookiesName)].Value = Serializable_MessageBox(Serializable_MessageBox(MBx),true);
                //System.Web.HttpContext.Current.Response.Cookies[string.Format("{0}-MessageValue1", Common.Get_CookiesName)].Value = Serializable_MessageBox(Serializable_MessageBox(MBx), false);
                //System.Web.HttpContext.Current.Session[string.Format("{0}-MessageValue", Common.Get_CookiesName)] = MBx;
                //FrameWorkCache.Instance().Insert(string.Format("{0}-MessageValue", Common.Get_CookiesName), MBx);
                System.Web.HttpContext.Current.Response.Redirect(string.Format("~/Manager/Messages.aspx?OPID={0}",Common.RndNum(5)));
            }            
        }

        /// <summary>
        /// 写入日志到DB
        /// </summary>
        /// <param name="E_Type">日志类型：日记类型,1:操作日记2:安全日志</param>
        /// <param name="E_Record">日志内容</param>
        public static void EventWriteDB(int E_Type, string E_Record)
        {
            EventWriteDB(E_Type, E_Record, Common.Get_UserID);
        }

        /// <summary>
        /// 写入日志到DB
        /// </summary>
        /// <param name="E_Type">日志类型：日记类型,1:操作日记2:安全日志</param>
        /// <param name="E_Record">日志内容</param>
        /// <param name="userid">关联用户id</param>
        public static void EventWriteDB(int E_Type, string E_Record, int userid)
        {
            sys_EventTable s_Et = new sys_EventTable();
            s_Et.E_DateTime = DateTime.Now;
            s_Et.E_From = Common.GetScriptUrl;
            s_Et.E_IP = Common.GetIPAddress();
            s_Et.E_Record = E_Record;
            s_Et.E_UserID = userid;
            s_Et.E_U_LoginName = UserData.Get_sys_UserTable(s_Et.E_UserID).U_LoginName;
            s_Et.E_Type = E_Type;
            s_Et.DB_Option_Action_ = "Insert";
            Permission PMI = FrameWorkPermission.Get_Permission;
            if (PMI != null)
            {
                s_Et.E_ApplicationID = PMI.ApplicationID;
                s_Et.E_A_AppName = BusinessFacade.sys_ApplicationsDisp(PMI.ApplicationID).A_AppName;
                s_Et.E_M_PageCode = PMI.PageCode;
                s_Et.E_M_Name = BusinessFacade.sys_ModuleDisp(PMI.ApplicationID, PMI.PageCode).M_CName;
            }
            BusinessFacade.sys_EventInsertUpdate(s_Et);
        }

        /// <summary>
        /// 序列化MessageBox类
        /// </summary>
        /// <param name="MBx">MessageBox类</param>
        /// <returns>字符数组</returns>
        public static byte[] Serializable_MessageBox(MessageBox MBx)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            byte[] b;
            formatter.Serialize(ms, MBx);
            ms.Position = 0;
            b = new byte[ms.Length];
            ms.Read(b, 0, b.Length);
            ms.Close();
            return b;
        }

        /// <summary>
        /// 将字节数组转为ASCII字符
        /// </summary>
        /// <param name="MessageArray">字节数组</param>
        /// <returns></returns>
        public static string Serializable_MessageBox(byte[] MessageArray)
        {
            return Convert.ToBase64String(MessageArray);
        }

        /// <summary>
        /// 将字节数组转为ASCII字符(是否进行Gzip压缩)
        /// </summary>
        /// <param name="MessageArray">字节数组</param>
        /// <param name="isgzip">是否启用gzip</param>
        /// <returns>返回字符</returns>
        public static string Serializable_MessageBox(byte[] MessageArray, bool isgzip)
        {
            if (isgzip)
            {
                MemoryStream ms = new MemoryStream();
                Stream s = new GZipStream(ms, CompressionMode.Compress);
                s.Write(MessageArray, 0, MessageArray.Length);
                s.Close();
                MessageArray = (byte[])ms.ToArray();
                ms.Dispose();
                ms.Close();
                s.Dispose();
            }
            return Serializable_MessageBox(MessageArray);
        }
        /// <summary>
        /// 反序列化MessageBox类
        /// </summary>
        /// <param name="BytArray">字节内容</param>
        /// <returns></returns>
        public static MessageBox Deserialize_MessageBox(byte[] BytArray)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            ms.Write(BytArray, 0, BytArray.Length);
            ms.Position = 0;
            MessageBox MBx = (MessageBox)formatter.Deserialize(ms);
            return MBx;            
        }

        /// <summary>
        /// 将字符按ASCII转为字节数组
        /// </summary>
        /// <param name="Messages">字符</param>
        /// <returns>字节流</returns>
        public static byte[] Deserialize_MessageBox(string Messages)
        {
            return Convert.FromBase64String(Messages);
        }

        /// <summary>
        /// 将字符按ASCII转为字节数组(是否进行Gzip压缩)
        /// </summary>
        /// <param name="Messages">字符</param>
        /// <param name="isgzip">字符是否进行过gzip压缩</param>
        /// <returns>字节流</returns>
        public static byte[] Deserialize_MessageBox(string Messages,bool isgzip)
        {
            byte[] BytArray  =  Deserialize_MessageBox(Messages);
            if (isgzip)
            {
                MemoryStream ms = new MemoryStream();
                GZipStream s = new GZipStream(new MemoryStream(BytArray), CompressionMode.Decompress);
                
                byte[] bytes = new byte[4096];
                int n;
                while ((n = s.Read(bytes, 0, bytes.Length)) != 0)
                {
                    ms.Write(bytes, 0, n);
                }

                s.Close();
                BytArray = (byte[])ms.ToArray();
                ms.Dispose();
                ms.Close();
                s.Dispose();
            }
            return BytArray;
        }
    }
}
