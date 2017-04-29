/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				FrameWorkLogin.cs                                        			*
 *      Description:																*
 *				 登陆类         												    *
 *      Author:																		*
 *				Lzppcc														        *
 *				Lzppcc@hotmail.com													*
 *				http://www.supesoft.com												*
 *      Finish DateTime:															*
 *				2007年8月6日														*
 *      History:																	*
 ***********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using PmSys.Components;

namespace PmSys
{
    /// <summary>
    /// 登陆类
    /// </summary>
    public class FrameWorkLogin
    {
        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="LoginName">用户名</param>
        /// <param name="LoginPassWord">密码</param>
        /// <param name="userkey">用户登陆标识</param>
        /// <returns>true/false</returns>
        public bool CheckLogin(string LoginName, string LoginPassWord,string userkey)
        { 
            bool bBool = false;

            QueryParam qp = new QueryParam();
            string a = Common.md5(LoginPassWord, 32);
            qp.Where  = string.Format(" Where U_StatUs=0 and  U_LoginName='{0}' and U_Password='{1}'",Common.inSQL(LoginName),Common.md5(LoginPassWord,32));
            int iInt = 0;
            ArrayList lst = BusinessFacade.sys_UserList(qp, out iInt);
            if (iInt > 0)
            {
                bBool = true;
                sys_UserTable sUT = (sys_UserTable)lst[0];
                string[] U_ExtendFieldArray = (sUT.U_ExtendField+"").Split(',');
                if (U_ExtendFieldArray.Length == 3)
                {
                    Common.MenuStyle = Convert.ToInt32(U_ExtendFieldArray[0]);
                    Common.PageSize = Convert.ToInt32(U_ExtendFieldArray[1]);
                    Common.TableSink = U_ExtendFieldArray[2];
                }
                checkOnline(sUT.U_LoginName.ToLower(), sUT.U_Password, Common.SessionID, userkey);
                Signin(sUT);
            }                
            return bBool;
        }


        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="sUT">用户实体类</param>
        private void Signin(sys_UserTable sUT)
        {

            System.Web.Security.FormsAuthenticationTicket tk = new FormsAuthenticationTicket(1,
                    sUT.UserID.ToString(),
                    DateTime.Now,
                    DateTime.Now.AddMinutes(Common.OnlineMinute),
                    true,
                    "",
                    System.Web.Security.FormsAuthentication.FormsCookiePath
                    );

            string key = System.Web.Security.FormsAuthentication.Encrypt(tk); //得到加密后的身份验证票字串 

            HttpCookie ck = new HttpCookie(System.Web.Security.FormsAuthentication.FormsCookieName, key);
            //ck.Domain = System.Web.Security.FormsAuthentication.CookieDomain;  // 这句话在部署网站后有用，此为关系到同一个域名下面的多个站点是否能共享Cookie
            HttpContext.Current.Response.Cookies.Add(ck);



        }

        /// <summary>
        /// 检测在线用户表
        /// </summary>
        /// <param name="U_LoginName">用户名</param>
        /// <param name="U_Password">密码</param>
        /// <param name="SessionID">用户sessionID</param>
        /// <param name="userkey">用户Key</param>
        private void checkOnline(string U_LoginName, string U_Password, string SessionID, string userkey)
        {
            if (FrameWorkOnline.Instance().OnlineCheck(U_LoginName))
            {
				FrameWorkOnline.Instance().UpdateOnlineUser(U_LoginName);
				//HttpContext.Current.Session["CheckCode"] = Common.RndNum(4);
				////OnlineUser<int> ou =  FrameWorkPermission.UserOnlineList.GetValue(U_LoginName);
				//OnlineUser<string> ou = FrameWorkOnline.Instance().GetOnlineMember(U_LoginName);
				//MessageBox MBx = new MessageBox();
				//MBx.M_Type = 2;
				//MBx.M_Title = "重复登陆!";
				//MBx.M_IconType = Icon_Type.Alert;

				//MBx.M_Body = string.Format("您的用户名({0})已经于({1}),从({2})IP登陆在本系统.在线时间:{3}分.", U_LoginName, ou.U_StartTime, ou.U_LastIP, (ou.U_OnlineSeconds/60).ToString("N2"));
				//MBx.M_ButtonList.Add(new sys_NavigationUrl("返回", "default.aspx", "", UrlType.Href, true));
				//MBx.M_ButtonList.Add(new sys_NavigationUrl("强制下线", string.Format("Login.aspx?CMD=OutOnline&OPCode={0}&U_LoginName={1}&U_Password={2}&SessionID={3}", HttpContext.Current.Session["CheckCode"].ToString(), U_LoginName, U_Password, U_LoginName), "", UrlType.Href, false));
				//FrameWorkLogin.MoveErrorLoginUser(userkey);
				//EventMessage.MessageBox(MBx);
            }
            else
            {
                FrameWorkOnline.Instance().InsertOnlineUser(U_LoginName);
                //OnlineUser<int> us = new OnlineUser<int>();
                //us.U_Guid = UserID;
                //us.U_Name = U_LoginName;
                //us.U_Type = true;
                //us.U_LastUrl = Common.GetScriptUrl;
                //FrameWorkPermission.UserOnlineList.InsertUser(us.U_Guid, us);
            }
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        public static void UserOut()
        {
            //FrameWorkPermission.UserOnlineList.Remove(Common.Get_UserID);
            //写退出日志
            EventMessage.EventWriteDB(2, "用户退出", Common.Get_UserID);
            //退出操作
            FrameWorkOnline.Instance().OnlineRemove(UserData.GetUserDate.U_LoginName,Common.SessionID);
            UserData.Move_UserPermissionCache(Common.Get_UserID);
            UserData.MoveUserCache(Common.Get_UserID);
            HttpContext.Current.Response.Cookies["MenuStyle"].Expires = DateTime.Now.AddDays(-30);
            HttpContext.Current.Response.Cookies["PageSize"].Expires = DateTime.Now.AddDays(-30);
            HttpContext.Current.Response.Cookies["TableSink"].Expires = DateTime.Now.AddDays(-30);
            System.Web.Security.FormsAuthentication.SignOut();


        }

        #region "用户登陆出错次数检测"
        private static CacheOnline<string, LoginErrorUser> _LoginErrorList = new CacheOnline<string, LoginErrorUser>(FrameSystemInfo.GetSystemInfoTable.S_SystemConfigData.C_LoginErrorDisableMinute);

        /// <summary>
        /// 获得用户登陆出错次数
        /// </summary>
        public static int GetLoginUserError(string UserKey)
        {
            int errorcount = 0;
            LoginErrorUser LUser = _LoginErrorList.GetValue(UserKey);
            if (LUser != null)
            {

                errorcount = LUser.ErrorCount;
                    
            }
            return errorcount;
        }

        /// <summary>
        /// 检测用户是否可以登陆
        /// </summary>
        /// <param name="UserKey">用户UserKey</param>
        /// <returns>true可以登陆,false不可登陆</returns>
        public static bool CheckDisableLoginUser(string UserKey)
        {
            bool rBool = false;
            LoginErrorUser LUser = _LoginErrorList.GetValue(UserKey);
            if (LUser == null)
            {
                LUser = new LoginErrorUser();
                LUser.U_Guid = UserKey;
                LUser.U_Type = true;
                LUser.ErrorCount = 1;
                LUser.U_Name = UserKey;
                _LoginErrorList.InsertUser(UserKey, LUser);
                rBool = true;
            }
            else {
                if (LUser.ErrorCount < FrameSystemInfo.GetSystemInfoTable.S_SystemConfigData.C_LoginErrorMaxNum)
                {
                    LUser.ErrorCount++;
                    return true;
                }
                else {
                    _LoginErrorList.Access(UserKey);
                }
            }
            return rBool;
        }

        /// <summary>
        /// 移除登陆列表中用户
        /// </summary>
        /// <param name="UserKey">用户key</param>
        /// <returns>true成功,false失败</returns>
        public static bool MoveErrorLoginUser(string UserKey)
        {
            bool rBool = false;
            _LoginErrorList.Remove(UserKey);
            return rBool;            
        }
        



        /// <summary>
        /// 用户登陆出错类
        /// </summary>
        public class LoginErrorUser : OnlineUser<string>
        {
            /// <summary>
            /// 用户登陆出错次数
            /// </summary>
            public int ErrorCount;
        }
        #endregion
    }
}
