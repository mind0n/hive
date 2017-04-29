/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				FrameWorkPermission.cs                                     			*
 *      Description:																*
 *				 权限检测         												    *
 *      Author:																		*
 *				Lzppcc														        *
 *				Lzppcc@hotmail.com													*
 *				http://www.supesoft.com												*
 *      Finish DateTime:															*
 *				2007年8月6日														*
 *      History:																	*
 ***********************************************************************************/
using System;
using System.Web;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Compression;
using System.Text;
using PmSys.Components;
using System.Web.Security;

namespace PmSys
{
    /// <summary>
    /// 权限检测
    /// </summary>
    public class FrameWorkPermission : IHttpModule
    {
        /// <summary>
        /// 读取配置状态
        /// </summary>
        public static bool InitStat = false;
        internal delegate void DelegateCheckUpdate();

		/// <summary>
        /// 应用启动时间
        /// </summary>
        public static DateTime AppStartTime;


        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="app"></param>
        public void Init(HttpApplication app)
        {
            app.BeginRequest += new EventHandler(app_HttpGZip);
            app.Error += new EventHandler(app_Error);
            app.AuthenticateRequest += new EventHandler(app_AuthMethod);
            app.AcquireRequestState += new EventHandler(app_Auth);
            AppStartTime = DateTime.Now;
        }

        /// <summary>
        /// HttpGZip压缩
        /// </summary>
        /// <param name="ob"></param>
        /// <param name="ex"></param>
        private void app_HttpGZip(object ob, EventArgs ex)
        {            
            //判断是否在manager目录里
            if (Common.GetCharInStringCount("/manager/", Common.GetScriptUrl.ToLower()) > 0)
            {
                if ((InitStat) && (FrameSystemInfo.GetSystemInfoTable.S_SystemConfigData.C_HttpGZip))
                {
                    HttpApplication ap = ob as HttpApplication;
                    if (Common.GetScriptNameExt.ToLower() == "aspx" && ap.Request.Headers["Accept-encoding"] != null && ap.Request.Headers["Accept-encoding"].Contains("gzip"))
                    {
                        ap.Response.Filter = new GZipStream(ap.Response.Filter, CompressionMode.Compress);
                        ap.Response.AppendHeader("Content-encoding", "gzip");
                        ap.Response.AppendHeader("Vary", "Content-encoding");
                        //ap.Response.Write("HTTP Compression Enabled (GZip)");
                    }
                }
            }
        }

        /// <summary>
        /// 设置方法属性权限检测数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void app_AuthMethod(object sender, EventArgs e)
        {
            //判断是否在manager目录里,并且文件名为aspx
            if (Common.GetScriptNameExt.ToLower() == "aspx" && Common.GetCharInStringCount("/manager/", Common.GetScriptUrl.ToLower()) > 0)
            {
                //检测方法权限设置
                HttpApplication App = (HttpApplication)sender;
                string cookieName = FormsAuthentication.FormsCookieName;
                HttpCookie authCookie = App.Context.Request.Cookies[cookieName];

                if (null == authCookie)
                {
                    // 沒有驗證 Cookie。
                    return;
                }

                FormsAuthenticationTicket authTicket = null;
                try
                {
                    authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                }
                catch (Exception ex)
                {
                    // 記錄例外狀況詳細資料 (為簡單起見已省略)
                    FileTxtLogs.WriteLog(ex.ToString());
                    return;
                }

                if (null == authTicket)
                {
                    // Cookie 無法解密。
                    return;
                }

                // 建立 Identity 物件
                FormsIdentity id = new FormsIdentity(authTicket);
                App.Context.User = new PermissionPrincipal(id);
            }
        }

        /// <summary>
        /// 处理认证成功事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void app_Auth(object sender, EventArgs e)
        {
            //判断是否在manager目录里
            if (Common.GetCharInStringCount("/manager/", Common.GetScriptUrl.ToLower()) > 0)
            {
                //判断文件名是否为aspx
                if (Common.GetScriptNameExt.ToLower() == "aspx")
                {
                    //判断
                    if (Common.Get_UserID != 0)
                    {
                        //判断在线用户

                        //if (UserOnlineList.CheckMemberOnline(UserData.GetUserDate.U_LoginName.ToLower(), Common.Get_UserID))
                        //{
                        //    UserOnlineList.Access(Common.Get_UserID, Common.GetScriptUrl);
                        //}
                        //string abc = Common.GetSessionID;

                        if (FrameWorkOnline.Instance().CheckUserInOnline(UserData.GetUserDate.U_LoginName,Common.SessionID))
                        {
                            FrameWorkOnline.Instance().OnlineAccess(UserData.GetUserDate.U_LoginName);
                        }
                        else
                        {
                            if (Common.OnlineMinute != 0)
                            {
                                FrameWorkLogin.UserOut();
                                MessageBox MBx = new MessageBox();
                                MBx.M_Type = 2;
                                MBx.M_Title = "没有登陆!";
                                MBx.M_IconType = Icon_Type.Error;
                                MBx.M_Body = "您已经被系统强制退出！";
                                MBx.M_ButtonList.Add(new sys_NavigationUrl("返回", "~/Manager/default.aspx", "", UrlType.Href, true));
                                EventMessage.MessageBox(MBx);
                            }
                        }

                        //检测权限
                        if (!Check_Permission)
                        {
                            MessageBox MBx = new MessageBox();
                            MBx.M_Type = 2;
                            MBx.M_Title = "权限出错";
                            MBx.M_IconType = Icon_Type.Error;
                            MBx.M_Body = "无权访问当前页面！";
                            MBx.M_ButtonList.Add(new sys_NavigationUrl("返回", "history.back();", "", UrlType.JavaScript, true));
                            EventMessage.MessageBox(MBx);
                        }

                        //更新当前用户最后访问记录
                        if (Common.GetDBType == "Oracle")
                            BusinessFacade.Update_Table_Fileds("sys_User", string.Format("U_LastIP='{0}',U_LastDateTime=to_date('{1}','yyyy-mm-dd HH24:MI:SS')", Common.GetIPAddress(), DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")), string.Format("UserID={0}", Common.Get_UserID));
                        else
                            BusinessFacade.Update_Table_Fileds("sys_User", string.Format("U_LastIP='{0}',U_LastDateTime='{1}'", Common.GetIPAddress(), DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")), string.Format("UserID={0}", Common.Get_UserID));

                        //写访问日志
                        if (FrameSystemInfo.GetSystemInfoTable.S_SystemConfigData.C_RequestLog)
                            EventMessage.EventWriteDB(3, "访问网页");
                    }
                }
            }
        }

        /// <summary>
        /// 处理出错日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void app_Error(object sender, EventArgs e)
        {
            HttpApplication ap = sender as HttpApplication;
            Exception ex = ap.Server.GetLastError();
            if (ex is HttpException)
            {
                HttpException hx = (HttpException)ex;
                if (hx.GetHttpCode() == 404)
                {
                    string page = ap.Request.PhysicalPath;
                    FileTxtLogs.WriteLog(string.Format("文件不存在:{0}", ap.Request.Url.AbsoluteUri));
                    return;
                }
            }
            if (ex.InnerException != null) ex = ex.InnerException;
            FileTxtLogs.WriteLog("访问路径:"+Common.GetScriptUrl+"<br>"+ex.Source + " thrown " + ex.GetType().ToString() + "<br />" + ex.Message.Replace("\r", "").Replace("\n", "<br />") + "<br />" + ex.StackTrace.Replace("\r", "").Replace("\n", "<br />"));
            if (!Common.DispError)
            {
                if (Common.GetScriptName.ToLower().IndexOf("manager/messages.aspx")<0)
                {
                    ap.Response.Redirect("~/Manager/Messages.aspx?CMD=AppError");
                }
            }


        }

        /// <summary>
        /// 检测权限
        /// </summary>
        private bool Check_Permission
        {
            get
            {
                Permission Pis = Get_Permission;
                if (Pis == null)
                    return true;
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    int UserID = Common.Get_UserID;
                    if (UserData.Get_sys_UserTable(UserID).U_Type == 0) //如为超级用户
                        return true;
                    //检测页面文件名访问权限
                    PermissionItem PsItem = Get_PermissionItem(Pis.ItemList);
                    if (PsItem == null)
                        return false;
                    if (string.IsNullOrEmpty(Common.GetScriptNameQueryString))
                        return UserData.CheckPageCode(UserID, Pis.ApplicationID, Pis.PageCode, PsItem.Item_Value);
                    else {
                        if (!UserData.CheckPageCode(UserID, Pis.ApplicationID, Pis.PageCode, PsItem.Item_Value))
                        {
                            return false;
                        }
                        else { 
                            //检测页面文件url权限
                            PsItem = Get_UrlPermissionItem(Pis.ItemList);
                            if (PsItem == null)
                                return true;
                            return UserData.CheckPageCode(UserID, Pis.ApplicationID, Pis.PageCode, PsItem.Item_Value);
                        }
                    }
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 获取当前访问网页文件名格式: ,文件名,
        /// </summary>
        public static string Get_Script_Name
        {
            get
            {
                string Script_Name = Common.GetScriptName;
                Script_Name = Script_Name.Substring(Script_Name.LastIndexOf("/") + 1);
                return string.Format(",{0},", Script_Name);
            }
        }

        /// <summary>
        /// 获取当前目录下权限配置集合
        /// </summary>
        public static Permission Get_Permission
        {
            get
            {
                return (Permission)ConfigurationManager.GetSection("Permission");
            }
        }

        /// <summary>
        /// 获取当前面页所属的PermissionItem
        /// </summary>
        /// <param name="List">权限列表</param>
        /// <returns></returns>
        public static PermissionItem Get_PermissionItem(List<PermissionItem> List)
        {
            PermissionItem PI = null;
            foreach (PermissionItem var in List)
            {
                if (var.Item_FileList.IndexOf(Get_Script_Name.ToLower()) >= 0)
                {
                    return var;
                }
            }
            return PI;
        }

        /// <summary>
        /// 获取当前面页Url所属的PermissionItem
        /// </summary>
        /// <param name="List">权限</param>
        /// <returns>权限值</returns>
        public static PermissionItem Get_UrlPermissionItem(List<PermissionItem> List)
        {
            PermissionItem PI = null;
            foreach (PermissionItem var in List)
            {
                if (var.Item_FileList.IndexOf(","+Common.GetScriptNameUrl.ToLower()+",") >= 0)
                {
                    return var;
                }
            }
            return PI;
        }

        /// <summary>
        /// 检测权限
        /// </summary>
        /// <param name="PT"></param>
        /// <returns></returns>
        public static bool CheckButtonPermission(PopedomType PT)
        {
            Permission Pis = Get_Permission;
            if (Pis == null)
                return true;
            return UserData.CheckPageCode(Common.Get_UserID, Pis.ApplicationID, Pis.PageCode, (int)PT);

        }

        /// <summary>
        /// 获得当前页面的菜单Icon
        /// </summary>
        public static string GetMenuIcon
        {
            get {
                Permission Pis = Get_Permission;
                if (Pis == null)
                    return "~/manager/images/icon/default.gif";
                else
                {
                    return BusinessFacade.sys_ModuleDisp(Pis.ApplicationID, Pis.PageCode).M_Icon;
                }
            }
        }

        /// <summary>
        /// 检测权限(出提示框)
        /// </summary>
        /// <param name="PT"></param>
        public static void CheckPermissionVoid(PopedomType PT)
        {
            if (!CheckButtonPermission(PT))
            {
                EventMessage.MessageBox(2, "禁止访问", "无权限执行当前操作!", Icon_Type.Error, "history.back();", UrlType.JavaScript);
            }
        }

        /// <summary>
        /// 根据CMD检测权限
        /// </summary>
        /// <param name="CMD">CMD值</param>
        public static void CheckPagePermission(string CMD)
        {
            switch (CMD)
            {
                case "List":
                    CheckPermissionVoid(PopedomType.List);
                    break;
                case "New":
                    CheckPermissionVoid(PopedomType.New);
                    break;
                case "Edit":
                    CheckPermissionVoid(PopedomType.Edit);
                    break;
                case "OrderBy":
                    CheckPermissionVoid(PopedomType.Edit);
                    break;
                case "Move":
                    CheckPermissionVoid(PopedomType.Delete);
                    break;
                case "Delete":
                    CheckPermissionVoid(PopedomType.Delete);
                    break;
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {

        }
    }
}
