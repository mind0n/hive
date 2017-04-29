/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				MessageBox.cs	                                           			*
 *      Description:																*
 *				 信息提示实体类     											    *
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
    /// 信息提示实体类
    /// </summary>
    [Serializable]
    public class MessageBox
    {

        #region "Private Variables"
        private string _M_Title = "信息提示标题";
        private string _M_Body = "信息提示内容";
        private Icon_Type _M_IconType = Icon_Type.OK;
        private List<sys_NavigationUrl> _M_ButtonList = new List<sys_NavigationUrl>();
        private int _M_Type =1;
        private bool _M_WriteToDB = true;
        private string _M_ReturnScript ;
        #endregion

        #region "Public Variables"
        /// <summary>
        /// 信息提示标题
        /// </summary>
        public string M_Title
        {
            get {
                return _M_Title;
            }
            set {
                _M_Title = value;
            }
        }

        /// <summary>
        /// 信息提示内容
        /// </summary>
        public string M_Body
        {
            get {
                return _M_Body;
            }
            set {
                _M_Body = value;
            }
        }

        /// <summary>
        /// 信息提示图标类型
        /// </summary>
        public Icon_Type M_IconType
        {
            get {
                return _M_IconType;
            }
            set {
                _M_IconType = value;
            }
        }

        /// <summary>
        /// 按钮列表
        /// </summary>
        public List<sys_NavigationUrl> M_ButtonList
        {
            get {
                return _M_ButtonList;
            }
            set {
                _M_ButtonList = value;
            }
        }
        /// <summary>
        /// 信息类型: 1操作日志,2安全日志,3访问日志 (默认1)
        /// </summary>
        public int M_Type
        {
            get
            {
                return _M_Type;
            }
            set {
                _M_Type = value;
            }
        }

        /// <summary>
        /// 是否需要写入当前日志数据库,默认为true
        /// </summary>
        public bool M_WriteToDB
        {
            get {
                return _M_WriteToDB;
            }
            set {
                _M_WriteToDB = value;
            }
        }

        /// <summary>
        /// 执行Script脚本字符串(需加<script></script>)
        /// </summary>
        public string M_ReturnScript
        {
            get {
                return _M_ReturnScript;
            }
            set {
                _M_ReturnScript = value;
            }
        }

        #endregion

        /// <summary>
        /// 根据日志ID返回日志类型
        /// </summary>
        /// <param name="E_Type">日志ID</param>
        /// <returns></returns>
        public static string Get_Type(int E_Type)
        {
            if (E_Type == 1)
                return "操作日志";
            else if (E_Type == 2)
                return "安全日志";
            else if (E_Type == 3)
                return "访问日志";
            else
                return "未知类型" + E_Type.ToString();
        }
    }

    #region "提示框图标类型"

    /// <summary>
    /// 提示Icon类型
    /// </summary>
    public enum Icon_Type : byte
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        OK,         
        /// <summary>
        /// 操作标示
        /// </summary>
        Alert,       
        /// <summary>
        /// 操作失败
        /// </summary>
        Error
    }
    #endregion



}
