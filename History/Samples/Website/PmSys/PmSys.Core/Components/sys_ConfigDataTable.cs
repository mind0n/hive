/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				sys_ConfigDataTable.cs                                  			*
 *      Description:																*
 *				 系统配置表 		            								    *
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
    /// 系统配置表
    /// </summary>
    [Serializable]
    public class sys_ConfigDataTable
    {
		public static sys_ConfigDataTable DefaultConfigDataTable
		{
			get
			{
				sys_ConfigDataTable t = new sys_ConfigDataTable();
				t.C_CheckUpdate = false;
				t.C_HttpGZip = false;
				t.C_IPLookUrl = "localhost";
				t.C_LoginErrorDisableMinute = 30;
				t.C_LoginErrorMaxNum = 5;
				t.C_RequestLog = false;
				t.C_UpImgHeight = 120;
				t.C_UpImgWidth = 180;
				t.C_UploadPath = "/Upload";
				t.C_UploadSizeKb = 1000000;
				return t;
			}
		}
        #region "Private Variables"
        private int _C_UpImgHeight = 120;
        private int _C_UpImgWidth = 180;
        private string _C_UploadPath = "/Manager/Public/";
        private int _C_UploadSizeKb = 1024;
        private string _C_UploadFileExt = "zip,rar,doc,jpg,png,gif,bmp,swf";
        private bool _C_RequestLog = false;
        private string _C_IPLookUrl = "http://www.yahoo.cn/s?p={0}";
        private int _C_LoginErrorDisableMinute = 30;
        private int _C_LoginErrorMaxNum = 5;
        private bool _C_HttpGZip = false;
        private bool _C_CheckUpdate = true;
        #endregion

        #region "Public Variables"
        /// <summary>
        /// 是否自动检测有新版本更新
        /// </summary>
        public bool C_CheckUpdate
        {
            get {
                return _C_CheckUpdate;
            }
            set {
                _C_CheckUpdate = value;
            }
        }
        /// <summary>
        /// 是否允许GZip压缩输出网页
        /// </summary>
        public bool C_HttpGZip
        {
            get {
                return _C_HttpGZip;
            }

            set {
                _C_HttpGZip = value;
            }
        }

        /// <summary>
        /// 同一IP同一帐号登入出错,禁止登入时间(默认30)分,设定此值需要重启动应用程序
        /// </summary>
        public int C_LoginErrorDisableMinute
        {
            get {
                return _C_LoginErrorDisableMinute;
            }
            set {
                _C_LoginErrorDisableMinute = value;
            }
        }
        /// <summary>
        /// 同一IP同一帐号登入允许出错次数默认为5
        /// </summary>
        public int C_LoginErrorMaxNum
        {
            get {
                return _C_LoginErrorMaxNum;
            }
            set {
                _C_LoginErrorMaxNum = value;
            }
        }

        /// <summary>
        /// 上传文件扩展名 多个以,号分开
        /// </summary>
        public string C_UploadFileExt
        {
            get {
                return _C_UploadFileExt;
            }
            set {
                _C_UploadFileExt = value;
            }
        }

        /// <summary>
        /// 上传文件大小(Kb为单位)
        /// </summary>
        public int C_UploadSizeKb
        {
            get {
                return _C_UploadSizeKb;
            }
            set {
                _C_UploadSizeKb = value;
            }
        }
        
        /// <summary>
        /// 上传文件路径
        /// </summary>
        public string C_UploadPath
        {
            get {
                return _C_UploadPath;
            }
            set {
                _C_UploadPath = value;
            }
        }
        
        /// <summary>
        /// 上传图片缩图高度 默认120
        /// </summary>
        public int C_UpImgHeight
        {
            get {
                return _C_UpImgHeight;
            }
            set {
                _C_UpImgHeight = value;
            }
        }
        
        /// <summary>
        /// 上传图片缩图宽度 默认 180
        /// </summary>
        public int C_UpImgWidth
        {
            get {
                return _C_UpImgWidth;
            }
            set {
                _C_UpImgWidth = value;
            }
        }

        /// <summary>
        /// 是否启用户访问日志 默认为false
        /// </summary>
        public bool C_RequestLog
        {
            get {
                return _C_RequestLog;
            }
            set {
                _C_RequestLog = value;
            }
        }

        /// <summary>
        /// IP地址查询链接
        /// </summary>
        public string C_IPLookUrl
        {
            get {
                return _C_IPLookUrl;
            }
            set {
                _C_IPLookUrl = value;
            }
        }
        #endregion
    }
}
