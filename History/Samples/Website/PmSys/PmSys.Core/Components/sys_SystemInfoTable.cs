/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				sys_SystemInfoTable.cs                             		        	*
 *      Description:																*
 *				 系统信息类                 	           						    *
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
    /// 系统信息类
    /// </summary>
    [Serializable]
    public class sys_SystemInfoTable
    {

        #region "Private Variables"
        private string _DB_Option_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除 
        private int _SystemID = 0;  // 自动ID
		private string _S_Name = "签证管理系统";  // 系统名称
        private string _S_Version = "1.0.7";  // 版本号
        private string _S_Licensed = ""; //序列号
        private sys_ConfigDataTable _S_SystemConfigData = new sys_ConfigDataTable(); //系统配置信息
        private sys_FrameWorkInfoTable _S_FrameWorkInfo = new sys_FrameWorkInfoTable();

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
        public int SystemID
        {
            set { this._SystemID = value; }
            get { return this._SystemID; }
        }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string S_Name
        {
            set { this._S_Name = value; }
            get { return this._S_Name; }
        }

        /// <summary>
        /// 版本号
        /// </summary>
        public string S_Version
        {
            set { this._S_Version = value; }
            get { return this._S_Version; }
        }

        /// <summary>
        /// 序列号
        /// </summary>
        public string S_Licensed
        {
            set { 
                this._S_Licensed = value;
                _S_FrameWorkInfo.S_Licensed = value;
            }
            get { return this._S_Licensed; }
        }	

        /// <summary>
        /// 系统配置信息
        /// </summary>
        public sys_ConfigDataTable S_SystemConfigData
        {
            get{
                return _S_SystemConfigData;
            }
            set{
                _S_SystemConfigData = value;
            }
        }
        /// <summary>
        /// 系统版本信息
        /// </summary>
        public sys_FrameWorkInfoTable S_FrameWorkInfo
        {
            get {
                return _S_FrameWorkInfo;
            }
            set {
                _S_FrameWorkInfo = value;
            }
        }
        #endregion
	
    }    
}
