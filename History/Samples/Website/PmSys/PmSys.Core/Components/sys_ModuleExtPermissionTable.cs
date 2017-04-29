/*
    Copyright (C) 2008 supesoft.com,All Rights Reserved
    File:
		    sys_ModuleExtPermissionTable.cs
    Description:
		     模块扩展权限实体类
    Author:
		    Lzppcc
		    Lzppcc@hotmail.com
		    http://www.supesoft.com
    Finish DateTime:
	    2008年11月23日
    History:
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace PmSys.Components
{
    /// <summary>
    /// 模块扩展权限实体类
    /// </summary>
    public class sys_ModuleExtPermissionTable
    {


        #region "Private Variables"
        private string _DB_Option_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除 
        private int _ExtPermissionID = 0;  // 扩展权限ID
        private int _ModuleID = 0;  // 模块ID
        private string _PermissionName;  // 权限名称
        private int _PermissionValue = 0;  // 权限值
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
        /// 扩展权限ID
        /// </summary>
        public int ExtPermissionID
        {
            set { this._ExtPermissionID = value; }
            get { return this._ExtPermissionID; }
        }

        /// <summary>
        /// 模块ID
        /// </summary>
        public int ModuleID
        {
            set { this._ModuleID = value; }
            get { return this._ModuleID; }
        }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName
        {
            set { this._PermissionName = value; }
            get { return this._PermissionName; }
        }

        /// <summary>
        /// 权限值
        /// </summary>
        public int PermissionValue
        {
            set { this._PermissionValue = value; }
            get { return this._PermissionValue; }
        }

        #endregion
	
	
    }
}
