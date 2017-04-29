/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				sys_PermissionItem.cs                               		        	*
 *      Description:																*
 *				 权限Item           		           							    *
 *      Author:																		*
 *				Lzppcc														        *
 *				Lzppcc@hotmail.com													*
 *				http://www.supesoft.com												*
 *      Finish DateTime:															*
 *				2008年11月28日														*
 *      History:																	*
 ***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace PmSys.Components
{
    /// <summary>
    /// 权限Item实体类
    /// </summary>
    public class sys_PermissionItem
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="namevalue"></param>
        /// <param name="allowvalue"></param>
        public sys_PermissionItem(string name,int namevalue,bool allowvalue)
        {
            _Allow = allowvalue;
            _PermissionName = name;
            _PermissionValue = namevalue;
        }

        private string _PermissionName;

        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName
        {
            get { return _PermissionName; }
            set { _PermissionName = value; }
        }

        private int _PermissionValue;

        /// <summary>
        /// 权限值
        /// </summary>
        public int PermissionValue
        {
            get { return _PermissionValue; }
            set { _PermissionValue = value; }
        }
                
        private bool _Allow;

        /// <summary>
        /// 是否允许当前权限
        /// </summary>
        public bool Allow
        {
            get { return _Allow; }
            set { _Allow = value; }
        }

        private string _DispTxt;

        /// <summary>
        /// 生成显示文本
        /// </summary>
        public string DispTxt
        {
            get { return _DispTxt; }
            set { _DispTxt = value; }
        }

    }
}
