/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				sys_OnlineTable.cs                               		        	*
 *      Description:																*
 *				 按钮类型           		           							    *
 *      Author:																		*
 *				Lzppcc														        *
 *				Lzppcc@hotmail.com													*
 *				http://www.supesoft.com												*
 *      Finish DateTime:															*
 *				2008年3月22日														*
 *      History:																	*
 ***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace PmSys.Components
{
    /// <summary>
    /// 在线人员实体类
    /// </summary>
	public class sys_OnlineTable
	{


        #region "Private Variables"
        private string _DB_Option_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除 
        private int _OnlineID = 0;  // 自动ID
        private string _O_SessionID; //用户SessionID
        private string _O_UserName;  // 用户名
        private string _O_Ip; //用户IP地址
        private DateTime _O_LoginTime = DateTime.MaxValue; // 登陆时间
        private DateTime _O_LastTime = DateTime.MaxValue; // 最后访问时间
        private string _O_LastUrl;  // 最后请求网站
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
        /// 自动ID
        /// </summary>
        public int OnlineID
        {
            set { this._OnlineID = value; }
            get { return this._OnlineID; }
        }

        /// <summary>
        /// 用户SessionID
        /// </summary>
        public string O_SessionID
        {
            set { this._O_SessionID = value; }
            get { return this._O_SessionID; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string O_UserName
        {
            set { this._O_UserName = value; }
            get { return this._O_UserName; }
        }

        /// <summary>
        /// 用户IP地址
        /// </summary>
        public string O_Ip
        {
            set { this._O_Ip = value; }
            get { return this._O_Ip; }
        }

        /// <summary>
        /// 登陆时间
        /// </summary>
        public DateTime O_LoginTime
        {
            set { this._O_LoginTime = value; }
            get { return this._O_LoginTime; }
        }

        /// <summary>
        /// 最后访问时间
        /// </summary>
        public DateTime O_LastTime
        {
            set { this._O_LastTime = value; }
            get { return this._O_LastTime; }
        }

        /// <summary>
        /// 最后请求网站
        /// </summary>
        public string O_LastUrl
        {
            set { this._O_LastUrl = value; }
            get { return this._O_LastUrl; }
        }

        #endregion
	
	
		
	}
}
