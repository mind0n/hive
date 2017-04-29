/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				FileTxtLogsTable.cs	                                    			*
 *      Description:																*
 *				 文本文件日志实体类   											    *
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
    /// 文本文件日志实体类
    /// </summary>
    public class FileTxtLogsTable : IComparable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Par_LogDateTime"></param>
        /// <param name="Par_LogTxt"></param>
        /// <param name="Par_LogUserIp"></param>
        /// <param name="Par_LogErrorUrl"></param>
        public FileTxtLogsTable(DateTime Par_LogDateTime, string Par_LogTxt, string Par_LogUserIp, string Par_LogErrorUrl)
        {
            this._LogDateTime = Par_LogDateTime;
            this._LogTxt = Par_LogTxt;
            this._LogUserIp = Par_LogUserIp;
            this._LogErrorUrl = Par_LogErrorUrl;
        }

        #region "Private Variables"
        private DateTime _LogDateTime;
        private string _LogTxt;
        private string _LogUserIp;
        private string _LogErrorUrl;
        #endregion

        #region "Public Variables"
        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime LogDateTime
        {
            get {
                return _LogDateTime;
            }
            set {
                _LogDateTime = value;
            }
        }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogTxt
        {
            get {
                return _LogTxt;
            }
            set {
                _LogTxt = value;
            }
        }
        /// <summary>
        /// 日志引发IP地址
        /// </summary>
        public string LogUserIp
        {
            get {
                return _LogUserIp;
            }
            set {
                _LogUserIp = value;
            }
        }
        /// <summary>
        /// 日志引发Url
        /// </summary>
        public string LogErrorUrl
        {
            get {
                return _LogErrorUrl;
            }
            set {
                _LogErrorUrl = value;
            }
        }
        #endregion

        /// <summary>
        /// 重写比较类
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int CompareTo(object o)
        {
            FileTxtLogsTable Two = o as FileTxtLogsTable;
            if (Two._LogDateTime > LogDateTime)
                return 1;
            else if (Two._LogDateTime < LogDateTime)
                return -1;
            else
                return 0;
        }
    }
}
