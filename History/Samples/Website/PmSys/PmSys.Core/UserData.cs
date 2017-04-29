/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				UserData.cs                                              			*
 *      Description:																*
 *				 用户数据存储类                 								    *
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

using PmSys.Components;

namespace PmSys
{
    /// <summary>
    /// 用户数据存储类
    /// </summary>
    public class UserData
    {

        #region "用户资料"
        /// <summary>
        /// 根据用户ID获取用户资料(从缓存中获取)
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        public static sys_UserTable Get_sys_UserTable(int UserID)
        {
            string CacheKey = string.Format("{0}-UserInfo-{1}", Common.Get_WebCacheName, UserID);
			IFrameWorkCache cache = FrameWorkCache.Instance();
            if (cache[CacheKey] != null)
            {
                return (sys_UserTable)FrameWorkCache.Instance()[CacheKey];
            }
            else
            {
                sys_UserTable sUT = BusinessFacade.sys_UserDisp(UserID);
                FrameWorkCache.Instance().Insert(CacheKey, sUT);
                return sUT;
            }
        }

        /// <summary>
        /// 根据用户登陆名,获取用户资料
        /// </summary>
        /// <param name="u_LoginName">用户名</param>
        /// <returns>用户实体类</returns>
        public static sys_UserTable Get_sys_UserTable(string u_LoginName)
        {
            QueryParam qp = new QueryParam();
            qp.Where = string.Format(" Where U_LoginName='{0}' ",u_LoginName);
            int rInt = 0;
            return BusinessFacade.sys_UserList(qp,out rInt)[0] as sys_UserTable;
        }

        /// <summary>
        /// 根据用户ID移除用户资料Cache
        /// </summary>
        /// <param name="UserID"></param>
        public static void MoveUserCache(int UserID)
        {
            FrameWorkCache.Instance().Remove(string.Format("{0}-UserInfo-{1}", Common.Get_WebCacheName, UserID));
        }

        #endregion

        #region "用户权限"
        //private static Cache _UserPermissionCache = HttpRuntime.Cache;
        /// <summary>
        /// 根据用户ID,应用ID,PageCode判断用户是否拥有当前权限
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="P_ApplicationID">应用ID</param>
        /// <param name="P_PageCode">PageCode</param>
        /// <returns></returns>
        public static bool CheckPageCode(int UserID, int P_ApplicationID, string P_PageCode)
        {
            if (Get_sys_UserTable(UserID).U_Type == 0) //判断用户是否为超级用户
                return true;
            bool bBool = false;
            Hashtable UserPermission = Get_UserPermission(UserID);
            if (UserPermission.Count > 0)
            {
                string Key = string.Format("{0}-{1}", P_ApplicationID, P_PageCode);
                if (UserPermission.ContainsKey(Key))
                {
                    bBool = true;
                }
            }
            return bBool;
        }

        /// <summary>
        /// 根据用户ID,应用ID,PageCode,要检测权限数值
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="P_ApplicationID">应用ID</param>
        /// <param name="P_PageCode">PageCode</param>
        /// <param name="CheckPermissionValue">权限值</param>
        /// <returns></returns>
        public static bool CheckPageCode(int UserID, int P_ApplicationID, string P_PageCode, int CheckPermissionValue)
        {
            if (Get_sys_UserTable(UserID).U_Type == 0) //判断用户是否为超级用户
                return true;
            bool bBool = false;
            Hashtable UserPermission = Get_UserPermission(UserID);
            if (UserPermission.Count > 0)
            {
                string Key = string.Format("{0}-{1}", P_ApplicationID, P_PageCode);
                if (UserPermission.ContainsKey(Key))
                {
                    if ((((sys_RolePermissionTable)UserPermission[Key]).P_Value & CheckPermissionValue) == CheckPermissionValue)
                    {
                        bBool = true;
                    }
                }
            }
            return bBool;
        }

        /// <summary>
        /// 获取用户权限Hashtable
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        private static Hashtable Get_UserPermission(int UserID)
        {
            string Key = string.Format("{1}-Permission-{0}", UserID, Common.Get_WebCacheName);
            if (FrameWorkCache.Instance()[Key] != null)
            {
                return (Hashtable)FrameWorkCache.Instance()[Key];
            }
            else
            {

                Hashtable _Temp = Get_sys_RolePermissionTable(UserID);
                FrameWorkCache.Instance().Insert(Key, _Temp);
                return _Temp;
            }
        }

        /// <summary>
        /// 移除用户权限Cache
        /// </summary>
        /// <param name="UserID">用户ID</param>
        public static void Move_UserPermissionCache(int UserID)
        {
            FrameWorkCache.Instance().Remove(string.Format("{1}-Permission-{0}", UserID, Common.Get_WebCacheName));
        }

        /// <summary>
        /// 移除某个角色的用户权限Cache
        /// </summary>
        /// <param name="RoleID"></param>
        public static void Move_RoleUserPermissionCache(int RoleID)
        {
            QueryParam qp = new QueryParam();
            qp.Where = string.Format("Where R_RoleID={0}", RoleID);
            int RecordCount = 0;
            ArrayList lst = BusinessFacade.sys_UserRolesList(qp, out RecordCount);
            foreach (sys_UserRolesTable var in lst)
            {
                Move_UserPermissionCache(var.R_UserID);
            }
        }

        /// <summary>
        /// 根据用户ID,获取用户模块权限列表
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        private static Hashtable Get_sys_RolePermissionTable(int UserID)
        {
            Hashtable PageCodeList = new Hashtable();
            List<sys_RolePermissionTable> List = new List<sys_RolePermissionTable>();
            QueryParam qp = new QueryParam();
            qp.Where = string.Format("Where R_UserID={0}", UserID);
            int RecordCount = 0;
            ArrayList lst = BusinessFacade.sys_UserRolesList(qp, out RecordCount);
            foreach (sys_UserRolesTable var in lst)
            {
                Get_RolesPermission(var.R_RoleID, List);
            }

            for (int i = 0; i < List.Count; i++)
            {
                string Key = string.Format("{0}-{1}", List[i].P_ApplicationID, List[i].P_PageCode);
                if (PageCodeList.ContainsKey(Key))
                {
                    sys_RolePermissionTable Rpt = (sys_RolePermissionTable)PageCodeList[Key];
                    if (Rpt.P_Value != List[i].P_Value)
                    {
                        //PageCodeList[Key] = List[i];
                        Rpt.P_Value = Rpt.P_Value | List[i].P_Value;
                    }
                }
                else
                {
                    PageCodeList.Add(Key, List[i]);
                }
            }

            return PageCodeList;
        }

        /// <summary>
        /// 根据用户角色ID,获取权限列表
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        /// <param name="List">权限列表</param>
        private static void Get_RolesPermission(int RoleID, List<sys_RolePermissionTable> List)
        {
            QueryParam qp = new QueryParam();
            qp.Where = string.Format("Where P_RoleID={0}", RoleID);
            int RecordCount = 0;
            ArrayList lst = BusinessFacade.sys_RolePermissionList(qp, out RecordCount);
            foreach (sys_RolePermissionTable var in lst)
            {
                List.Add(var);
            }
        }
        #endregion

        #region "获取当前登陆用户信息"
        /// <summary>
        /// 获取当前登陆用户信息
        /// </summary>
        public static sys_UserTable GetUserDate
        {
            get {
                return Get_sys_UserTable(Common.Get_UserID);
            }
        }
        #endregion
    }
}
