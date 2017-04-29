/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				sys_ModuleTable.cs                                		        	*
 *      Description:																*
 *				 模块类           		            							    *
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
    /// 模块类
    /// </summary>
    public class sys_ModuleTable
    {

        #region "Private Variables"
        private string _DB_Option_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除 
        private int _ModuleID = 0;  // 功能模块ID号
        private int _M_ApplicationID = 0;  // 所属应用程序ID
        private int _M_ParentID = 0;  // 所属父级模块ID与ModuleID关联,0为顶级
        private string _M_PageCode; //模块编码Parent为0,则为S00(xx),否则为S00M00(xx)
        private string _M_CName;  // 模块/栏目名称当ParentID为0为模块名称
        private string _M_Directory;  // 模块/栏目目录名
        private string _M_OrderLevel; //当前所在排序级别支持双层99级菜单
        private int _M_IsSystem = 0;  // 是否为系统模块1:是0:否如为系统则无法修改
        private int _M_Close = 0;  // 是否关闭1:是0:否
        private string _M_Icon = ""; //图标
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
        /// 功能模块ID号
        /// </summary>
        public int ModuleID
        {
            set { this._ModuleID = value; }
            get { return this._ModuleID; }
        }

        /// <summary>
        /// 所属应用程序ID
        /// </summary>
        public int M_ApplicationID
        {
            set { this._M_ApplicationID = value; }
            get { return this._M_ApplicationID; }
        }

        /// <summary>
        /// 所属父级模块ID与ModuleID关联,0为顶级
        /// </summary>
        public int M_ParentID
        {
            set { this._M_ParentID = value; }
            get { return this._M_ParentID; }
        }

        /// <summary>
        /// 模块编码Parent为0,则为S00(xx),否则为S00M00(xx)
        /// </summary>
        public string M_PageCode
        {
            set { this._M_PageCode = value; }
            get { return this._M_PageCode; }
        }

        /// <summary>
        /// 模块/栏目名称当ParentID为0为模块名称
        /// </summary>
        public string M_CName
        {
            set { this._M_CName = value; }
            get { return this._M_CName; }
        }

        /// <summary>
        /// 模块/栏目目录名
        /// </summary>
        public string M_Directory
        {

            set
            {
                this._M_Directory = value;
            }
            get
            {

                if (string.IsNullOrEmpty(this._M_Directory))
                    return "";
                else
                    return this._M_Directory;
            }
        }

        /// <summary>
        /// 当前所在排序级别支持双层99级菜单
        /// </summary>
        public string M_OrderLevel
        {
            set { this._M_OrderLevel = value; }
            get { return this._M_OrderLevel; }
        }

        /// <summary>
        /// 是否为系统模块1:是0:否如为系统则无法修改
        /// </summary>
        public int M_IsSystem
        {
            set { this._M_IsSystem = value; }
            get { return this._M_IsSystem; }
        }

        /// <summary>
        /// 是否关闭1:是0:否
        /// </summary>
        public int M_Close
        {
            set { this._M_Close = value; }
            get { return this._M_Close; }
        }

        /// <summary>
        /// 默认显示图标(流行菜单)空为default.gif
        /// </summary>
        public string M_Icon
        {
            get
            {
                if (string.IsNullOrEmpty(this._M_Icon))
                    return "~/manager/images/icon/default.gif";
                else
                    return this._M_Icon;
            }
            set
            {
                this._M_Icon = value;
            }
        }
        #endregion

    }
}
