/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				IFrameWorkOnline.cs                                              			*
 *      Description:																*
 *				 在线用户数据接口                 								    *
 *      Author:																		*
 *				http://www.supesoft.com												*
 *      Finish DateTime:															*
 *				2008年10月12日														*
 *      History:																	*
 ***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace PmSys
{
    /// <summary>
    /// 在线用户接口
    /// </summary>
    public interface IFrameWorkOnline
    {
        /// <summary>
        /// 检测当前用户是否在线
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="sessionid">用户唯一标识</param>
        /// <returns>是/否</returns>
        bool CheckUserInOnline(string username, string sessionid);

        /// <summary>
        /// 删除在线用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="sessionid">用户sessionid</param>
        void OnlineRemove(string username, string sessionid);

        /// <summary>
        /// 插入用户
        /// </summary>
        /// <param name="username">用户名</param>
        void InsertOnlineUser(string username);

		/// <summary>
		/// 更新用户
		/// </summary>
		/// <param name="username">用户名</param>
		void UpdateOnlineUser(string username);

        /// <summary>
        /// 检测用户名是否在线
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>是/否</returns>
        bool OnlineCheck(string username);
        /// <summary>
        /// 更新用户最后访问时间
        /// </summary>
        /// <param name="username">用户名</param>
        void OnlineAccess(string username);
        /// <summary>
        /// 移除在线用户
        /// </summary>
        /// <param name="username">用户名</param>
        void OnlineRemove(string username);
        /// <summary>
        /// 获得在线用户信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        OnlineUser<string> GetOnlineMember(string username);
        /// <summary>
        /// 清除超时在线用户
        /// </summary>
        void ClearOnlineUserTimeOut();
        /// <summary>
        /// 获得在线用户总数
        /// </summary>
        int GetOnlineUserNum { get;}
        /// <summary>
        /// 获得在线用户名表
        /// </summary>
        /// <param name="pageindex">页码</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="totalnum">记录总数</param>
        /// <returns>列表集合</returns>
        List<OnlineUser<string>> GetOnlineList(int pageindex, int pagesize, out int totalnum);
    }
}
