/*
       Copyright (C) 2008 supesoft.com,All Rights Reserved						    
       File:																		
 				FrameWorkOnlineDataBase.cs                                              			*
       Description:																
 				 在线用户数据库处理类
       Author:																													
 				http://www.supesoft.com												
       Finish DateTime:															
 				2008年10月12日														
       History:																	
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using PmSys.Components;

namespace PmSys
{
    /// <summary>
    /// 在线用户数据库处理类
    /// </summary>
    public class FrameWorkOnlineDataBase : IFrameWorkOnline
    {
        //定时器
        Timer _ClearTimeOutUser;

        //毫秒(10分钟)
        private int runtime = 60000 * Common.OnlineMinute;
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrameWorkOnlineDataBase()
        {
            _ClearTimeOutUser = new Timer(new TimerCallback(statTimer_Elapsed), null, 0, runtime);
        }

        /// <summary>
        /// 定时运行删除操作
        /// </summary>
        /// <param name="o"></param>
        private void statTimer_Elapsed(object o)
        {
            if (_ClearTimeOutUser != null)
            {
                _ClearTimeOutUser.Change(Timeout.Infinite, runtime);
                ClearOnlineUserTimeOut();
                _ClearTimeOutUser.Change(runtime, runtime);

            }
            else
            {
                _ClearTimeOutUser = new Timer(new TimerCallback(statTimer_Elapsed), null, 0, runtime);
            }
        }

        /// <summary>
        /// 插入用户
        /// </summary>
        /// <param name="username">用户名</param>
        public void InsertOnlineUser(string username)
        {
            BusinessFacade.InsertMemberOnline(username, Common.SessionID);
        }

		/// <summary>
		/// 插入用户
		/// </summary>
		/// <param name="username">用户名</param>
		public void UpdateOnlineUser(string username)
		{
			BusinessFacade.UpdateMemberOnline(username, Common.SessionID);
		}

        /// <summary>
        /// 检测用户是否在线
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>是/否</returns>
        public bool OnlineCheck(string username)
        {
            if (BusinessFacade.sys_OnlineDisp(username).OnlineID == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 更新用户最后访问时间
        /// </summary>
        /// <param name="username">用户名</param>
        public void OnlineAccess(string username)
        {
            sys_OnlineTable online = BusinessFacade.sys_OnlineDisp(username);
            online.O_LastTime = DateTime.Now;
            online.O_LastUrl = Common.GetScriptUrl;
            online.DB_Option_Action_ = "Update";
            BusinessFacade.sys_OnlineInsertUpdate(online);
        }
        /// <summary>
        /// 移除在线用户
        /// </summary>
        /// <param name="username">用户名</param>
        public void OnlineRemove(string username)
        {
            sys_OnlineTable online = BusinessFacade.sys_OnlineDisp(username);
            online.DB_Option_Action_ = "Delete";
            BusinessFacade.sys_OnlineInsertUpdate(online);
        }
        /// <summary>
        /// 获得在线用户信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public OnlineUser<string> GetOnlineMember(string username)
        {
            sys_OnlineTable online = BusinessFacade.sys_OnlineDisp(username);
            OnlineUser<string> ou = new OnlineUser<string>();
            ou.U_Guid = online.O_SessionID;
            ou.U_LastIP = online.O_Ip;
            ou.U_LastTime = online.O_LastTime;
            ou.U_LastUrl = online.O_LastUrl;
            ou.U_Name = online.O_UserName;
            ou.U_StartTime = online.O_LoginTime;
            TimeSpan ts = ou.U_LastTime - ou.U_StartTime;
            ou.U_OnlineSeconds = ts.TotalSeconds;
            ou.U_Type = true;

            return ou;
        }

        /// <summary>
        /// 清除超时在线用户
        /// </summary>
        public void ClearOnlineUserTimeOut()
        {
            QueryParam qp = new QueryParam();
            if (Common.GetDBType == "Access")
            {
                qp.Where = string.Format("Where O_LastTime<=#{0}#", Common.FormatDateToString(DateTime.Now.AddMinutes((Common.OnlineMinute * -1))));
            }
            else if (Common.GetDBType == "Oracle")
            {
                qp.Where = string.Format("Where O_LastTime<=to_date('{0}','yyyy-mm-dd HH24:MI:SS')", Common.FormatDateToString(DateTime.Now.AddMinutes((Common.OnlineMinute * -1))));
            }
            else
            {
                qp.Where = string.Format("Where O_LastTime<='{0}'", Common.FormatDateToString(DateTime.Now.AddMinutes((Common.OnlineMinute * -1))));
            }
            qp.PageSize = int.MaxValue;
            int rInt = 0;
            ArrayList lst = BusinessFacade.sys_OnlineList(qp, out rInt);
            if (rInt > 0)
            {
                foreach (sys_OnlineTable var in lst)
                {
                    var.DB_Option_Action_ = "Delete";
                    BusinessFacade.sys_OnlineInsertUpdate(var);
                }
            }
        }
        /// <summary>
        /// 获得在线用户总数
        /// </summary>
        public int GetOnlineUserNum
        {
            get
            {
                int rInt = 0;
                BusinessFacade.sys_OnlineList(new QueryParam(), out rInt);
                return rInt;
            }
        }
        /// <summary>
        /// 获得在线用户名表
        /// </summary>
        /// <param name="pageindex">页码</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="totalnum">记录总数</param>
        /// <returns>列表集合</returns>
        public List<OnlineUser<string>> GetOnlineList(int pageindex, int pagesize, out int totalnum)
        {
            List<OnlineUser<string>> lst = new List<OnlineUser<string>>();
            QueryParam qp = new QueryParam();
            qp.PageIndex = pageindex;
            qp.PageSize = pagesize;
            int RecordCount = 0;
            ArrayList lst1 = BusinessFacade.sys_OnlineList(qp, out RecordCount);
            foreach (sys_OnlineTable var in lst1)
            {
                OnlineUser<string> ou = new OnlineUser<string>();
                ou.U_Guid = var.O_SessionID;
                ou.U_LastIP = var.O_Ip;
                ou.U_LastTime = var.O_LastTime;
                ou.U_LastUrl = var.O_LastUrl;
                ou.U_Name = var.O_UserName;
                ou.U_StartTime = var.O_LoginTime;
                TimeSpan ts = ou.U_LastTime - ou.U_StartTime;
                ou.U_OnlineSeconds = ts.TotalSeconds;
                ou.U_Type = true;
                lst.Add(ou);
            }
            totalnum = RecordCount;
            return lst;
        }


        /// <summary>
        /// 检测当前用户是否在线
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="sessionid">用户唯一标识</param>
        /// <returns>是/否</returns>
        public bool CheckUserInOnline(string username, string sessionid)
        {
            if (BusinessFacade.sys_OnlineDisp(username,sessionid).OnlineID == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 删除在线用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="sessionid">用户sessionid</param>
        public void OnlineRemove(string username, string sessionid)
        {
            sys_OnlineTable so =  BusinessFacade.sys_OnlineDisp(username, sessionid);
            so.DB_Option_Action_ = "Delete";
            BusinessFacade.sys_OnlineInsertUpdate(so);
        }
    }
}
