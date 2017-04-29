/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				sys_RolesTable.cs                               		        	*
 *      Description:																*
 *				 角色实体类                 	           						    *
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
    /// 角色实体类
    /// </summary>
    public class sys_RolesTable
    {
        #region "Private Variables"
        private string _DB_Option_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除 
        private int _RoleID = 0;  // 角色ID自动ID
        private int _R_UserID = 0;  // 角角所属用户ID
        private string _R_RoleName;  // 角色名称
        private string _R_Description;  // 角色介绍
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
        /// 角色ID自动ID
        /// </summary>
        public int RoleID
        {
            set { this._RoleID = value; }
            get { return this._RoleID; }
        }

        /// <summary>
        /// 角角所属用户ID
        /// </summary>
        public int R_UserID
        {
            set { this._R_UserID = value; }
            get { return this._R_UserID; }
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string R_RoleName
        {
            set { this._R_RoleName = value; }
            get { return this._R_RoleName; }
        }

        /// <summary>
        /// 角色介绍
        /// </summary>
        public string R_Description
        {
            set { this._R_Description = value; }
            get { return this._R_Description; }
        }

        #endregion	
    }
}
