/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				sys_EventTable.cs                                  		        	*
 *      Description:																*
 *				 事件实体类 		            								    *
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
using System.IO;

namespace PmSys.Components
{
    /// <summary>
    /// 事件实体类
    /// </summary>
    public class sys_EventTable
    {


        #region "Private Variables"
        private string _DB_Option_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除 
        private int _EventID = 0;  // 事件ID号
        private string _E_U_LoginName;  // 用户名
        private int _E_UserID = 0;  // 操作时用户ID与sys_Users中UserID
        private DateTime _E_DateTime; // 事件发生的日期及时间
        private int _E_ApplicationID = 0;  // 所属应用程序ID与sys_Applicatio
        private string _E_A_AppName;  // 所属应用名称
        private string _E_M_Name;  // PageCode模块名称与sys_Module相同
        private string _E_M_PageCode; //发生事件时模块名称
        private string _E_From;  // 来源
        private int _E_Type = 0;  // 日记类型,1:操作日记2:安全日志3
        private string _E_IP; //客户端IP地址
        private string _E_Record;  // 详细描述
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
        /// 事件ID号
        /// </summary>
        public int EventID
        {
            set { this._EventID = value; }
            get { return this._EventID; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string E_U_LoginName
        {
            set { this._E_U_LoginName = value; }
            get { return this._E_U_LoginName; }
        }

        /// <summary>
        /// 操作时用户ID与sys_Users中UserID
        /// </summary>
        public int E_UserID
        {
            set { this._E_UserID = value; }
            get { return this._E_UserID; }
        }

        /// <summary>
        /// 事件发生的日期及时间
        /// </summary>
        public DateTime E_DateTime
        {
            set { this._E_DateTime = value; }
            get { return this._E_DateTime; }
        }

        /// <summary>
        /// 所属应用程序ID与sys_Applicatio
        /// </summary>
        public int E_ApplicationID
        {
            set { this._E_ApplicationID = value; }
            get { return this._E_ApplicationID; }
        }

        /// <summary>
        /// 所属应用名称
        /// </summary>
        public string E_A_AppName
        {
            set { this._E_A_AppName = value; }
            get { return this._E_A_AppName; }
        }

        /// <summary>
        /// PageCode模块名称与sys_Module相同
        /// </summary>
        public string E_M_Name
        {
            set { this._E_M_Name = value; }
            get { return this._E_M_Name; }
        }

        /// <summary>
        /// 发生事件时模块名称
        /// </summary>
        public string E_M_PageCode
        {
            set { this._E_M_PageCode = value; }
            get { return this._E_M_PageCode; }
        }

        /// <summary>
        /// 来源
        /// </summary>
        public string E_From
        {
            set { this._E_From = value; }
            get { return this._E_From; }
        }

        /// <summary>
        /// 日记类型,1:操作日记2:安全日志3
        /// </summary>
        public int E_Type
        {
            set { this._E_Type = value; }
            get { return this._E_Type; }
        }

        /// <summary>
        /// 客户端IP地址
        /// </summary>
        public string E_IP
        {
            set { this._E_IP = value; }
            get { return this._E_IP; }
        }

        /// <summary>
        /// 详细描述
        /// </summary>
        public string E_Record
        {
            set { this._E_Record = value; }
            get { return this._E_Record; }
        }

        #endregion
	
	
	
	
    }
}
