/*
       Copyright (C) 2008 supesoft.com,All Rights Reserved						    
       File:																		
 				FrameWorkOnlineCache.cs                                              			*
       Description:																
 				 在线用户缓存处理类
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

using PmSys.Components;

namespace PmSys
{
    /// <summary>
    /// 在线用户缓存处理类
    /// </summary>
    public class FrameWorkOnlineCache:IFrameWorkOnline
    {

        /// <summary>
        /// 删除在线用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="sessionid">用户sessionid</param>
        public void OnlineRemove(string username, string sessionid)
        {
            UserOnlineList.Remove(sessionid);
        }

        /// <summary>
        /// 检测当前用户是否在线
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="sessionid">用户唯一标识</param>
        /// <returns>是/否</returns>
        public bool CheckUserInOnline(string username, string sessionid)
        {
            return UserOnlineList.CheckKeyOnline(sessionid);
        }

        /// <summary>
        /// 插入用户
        /// </summary>
        /// <param name="username">用户名</param>
        public void InsertOnlineUser(string username)
        {
            OnlineUser<string> us = new OnlineUser<string>();
            us.U_Guid = Common.SessionID;
            us.U_Name = username;
            us.U_Type = true;
            us.U_LastUrl = Common.GetScriptUrl;
            UserOnlineList.InsertUser(us.U_Guid, us);
        }

		public void UpdateOnlineUser(string username)
		{
			OnlineUser<string> us = new OnlineUser<string>();
			us.U_Guid = Common.SessionID;
			us.U_Name = username;
			us.U_Type = true;
			us.U_LastUrl = Common.GetScriptUrl;
			UserOnlineList.InsertUser(us.U_Guid, us);
		}

        /// <summary>
        /// 缓存列表
        /// </summary>
        private CacheOnline<string, OnlineUser<string>> UserOnlineList
        {
            get {
                object o = FrameWorkCache.Instance()["FrameWorkCache_OnlineList"];
                if (o == null)
                {
                    CacheOnline<string, OnlineUser<string>> _OnlineCache = new CacheOnline<string, OnlineUser<string>>(Common.OnlineMinute);
                    FrameWorkCache.Instance().Insert("FrameWorkCache_OnlineList", _OnlineCache);
                    return _OnlineCache;
                }
                else
                {
                    return o as CacheOnline<string, OnlineUser<string>>;
                }
            }
        }

        /// <summary>
        /// 检测用户是否在线
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>是/否</returns>
        public bool OnlineCheck(string username)
        {
            return UserOnlineList.CheckMemberOnline(username.ToLower());
        }
        /// <summary>
        /// 更新用户最后访问时间
        /// </summary>
        /// <param name="username">用户名</param>
        public void OnlineAccess(string username)
        {
            UserOnlineList.Access(Common.SessionID, Common.GetScriptUrl);
        }
        /// <summary>
        /// 移除在线用户
        /// </summary>
        /// <param name="username">用户名</param>
        public void OnlineRemove(string username)
        {
            UserOnlineList.RemoveUserName(username);
        }
        /// <summary>
        /// 获得在线用户信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public OnlineUser<string> GetOnlineMember(string username)
        {
            return UserOnlineList.GetValue(username);
        }

        /// <summary>
        /// 清除超时在线用户
        /// </summary>
        public void ClearOnlineUserTimeOut()
        {
            
        }
        /// <summary>
        /// 获得在线用户总数
        /// </summary>
        public int GetOnlineUserNum
        {
            get
            {
                return UserOnlineList.MemberCount;
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
            totalnum = UserOnlineList.MemberCount;
            for (int i = (pageindex-1)*pagesize; i < pagesize*pagesize; i++)
            {
                if (i >= UserOnlineList.GetList.Count)
                    break;
                lst.Add(UserOnlineList.GetList[i]);
            }
            return lst;
        }

    }
}
