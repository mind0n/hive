/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				sys_ApplicationsTable.cs                                 			*
 *      Description:																*
 *				 应用实体类 		            								    *
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
    /// 应用实体类
    /// </summary>
    public class sys_ApplicationsTable
    {

        #region "Private Variables"
        private string _DB_Option_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除 
        private int _ApplicationID = 0;  // 自动ID 1:为系统管理应用
        private string _A_AppName;  // 应用名称
        private string _A_AppDescription;  // 应用介绍
        private string _A_AppUrl; //应用Url地址
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
        /// 自动ID 1:为系统管理应用
        /// </summary>
        public int ApplicationID
        {
            set { this._ApplicationID = value; }
            get { return this._ApplicationID; }
        }

        /// <summary>
        /// 应用名称
        /// </summary>
        public string A_AppName
        {
            set { this._A_AppName = value; }
            get { return this._A_AppName; }
        }

        /// <summary>
        /// 应用介绍
        /// </summary>
        public string A_AppDescription
        {
            set { this._A_AppDescription = value; }
            get { return this._A_AppDescription; }
        }

        /// <summary>
        /// 应用Url地址
        /// </summary>
        public string A_AppUrl
        {
            set { this._A_AppUrl = value; }
            get { return this._A_AppUrl; }
        }

        #endregion
	
	
    }
}
