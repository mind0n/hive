/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				sys_RoleApplicationTable.cs                        		        	*
 *      Description:																*
 *				 角色应用实体类           	           							    *
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
    /// 角色应用实体类
    /// </summary>
    public class sys_RoleApplicationTable
    {

        #region "Private Variables"
        private string _DB_Option_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除 
        private int _A_RoleID = 0;  // 角色ID与sys_Roles中RoleID相关
        private int _A_ApplicationID = 0;  // 应用ID与sys_Applications中Appl
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
        /// 角色ID与sys_Roles中RoleID相关
        /// </summary>
        public int A_RoleID
        {
            set { this._A_RoleID = value; }
            get { return this._A_RoleID; }
        }

        /// <summary>
        /// 应用ID与sys_Applications中Appl
        /// </summary>
        public int A_ApplicationID
        {
            set { this._A_ApplicationID = value; }
            get { return this._A_ApplicationID; }
        }

        #endregion
	
	
    }
}
