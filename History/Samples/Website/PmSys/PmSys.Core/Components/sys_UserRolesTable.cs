/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				sys_UserRolesTable.cs                             		        	*
 *      Description:																*
 *				 用户所属角色实体类            	           						    *
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
    /// 用户所属角色实体类
    /// </summary>
    public class sys_UserRolesTable
    {

        #region "Private Variables"
        private string _DB_Option_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除 
        private int _R_UserID = 0;  // 用户ID与sys_User表中UserID相关
        private int _R_RoleID = 0;  // 用户所属角色ID与Sys_Roles关联
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
        /// 用户ID与sys_User表中UserID相关
        /// </summary>
        public int R_UserID
        {
            set { this._R_UserID = value; }
            get { return this._R_UserID; }
        }

        /// <summary>
        /// 用户所属角色ID与Sys_Roles关联
        /// </summary>
        public int R_RoleID
        {
            set { this._R_RoleID = value; }
            get { return this._R_RoleID; }
        }

        #endregion
	
	
    }
}
