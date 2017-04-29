/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				sys_RolePermissionTable.cs                        		        	*
 *      Description:																*
 *				 角色应用权限实体类           	           						    *
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

namespace PmSys.Components
{
    /// <summary>
    /// 角色应用权限实体类
    /// </summary>
    [Serializable]
    public class sys_RolePermissionTable
    {
        #region "Private Variables"
        private string _DB_Option_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除 
        private int _PermissionID = 0;  // 角色应用权限自动ID
        private int _P_RoleID = 0;  // 角色ID与sys_Roles表中RoleID相
        private int _P_ApplicationID = 0;  // 角色所属应用ID与sys_Applicatio
        private string _P_PageCode; //角色应用中页面权限代码
        private int _P_Value = 0;  // 权限值
        #endregion

        #region "Public Variables"
        /// <summary>
        /// 操作方法 Insert:增加 Update:修改 Delete:删除 
        /// </summary>
        public string DB_Option_Action_
        {
            set { this._DB_Option_Action_ = value; }
            get { return this._DB_Option_Action_; }
        }

        /// <summary>
        /// 角色应用权限自动ID
        /// </summary>
        public int PermissionID
        {
            set { this._PermissionID = value; }
            get { return this._PermissionID; }
        }

        /// <summary>
        /// 角色ID与sys_Roles表中RoleID相
        /// </summary>
        public int P_RoleID
        {
            set { this._P_RoleID = value; }
            get { return this._P_RoleID; }
        }

        /// <summary>
        /// 角色所属应用ID与sys_Applicatio
        /// </summary>
        public int P_ApplicationID
        {
            set { this._P_ApplicationID = value; }
            get { return this._P_ApplicationID; }
        }

        /// <summary>
        /// 角色应用中页面权限代码
        /// </summary>
        public string P_PageCode
        {
            set { this._P_PageCode = value; }
            get { return this._P_PageCode; }
        }

        /// <summary>
        /// 权限值
        /// </summary>
        public int P_Value
        {
            set { this._P_Value = value; }
            get { return this._P_Value; }
        }

        #endregion
    }
}
