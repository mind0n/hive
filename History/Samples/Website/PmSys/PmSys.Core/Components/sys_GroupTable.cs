/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				sys_GroupTable.cs                                		        	*
 *      Description:																*
 *				 部门实体类      		            							    *
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
    /// 部门实体类
    /// </summary>
    public class sys_GroupTable
    {

        #region "Private Variables"
        private string _DB_Option_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除 
        private int _GroupID = 0;  // 分类ID号
        private string _G_CName;  // 分类中文说明
        private int _G_ParentID = 0;  // 上级分类ID0:为最高级
        private int _G_ShowOrder = 0;  // 显示顺序
        private int _G_Level = 0;  // 当前分类所在层数
        private int _G_ChildCount = 0;  // 当前分类子分类数
        private int _G_Delete = 0;  // 是否删除1:是0:否
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
        /// 分类ID号
        /// </summary>
        public int GroupID
        {
            set { this._GroupID = value; }
            get { return this._GroupID; }
        }

        /// <summary>
        /// 分类中文说明
        /// </summary>
        public string G_CName
        {
            set { this._G_CName = value; }
            get { return this._G_CName; }
        }

        /// <summary>
        /// 上级分类ID0:为最高级
        /// </summary>
        public int G_ParentID
        {
            set { this._G_ParentID = value; }
            get { return this._G_ParentID; }
        }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int G_ShowOrder
        {
            set { this._G_ShowOrder = value; }
            get { return this._G_ShowOrder; }
        }

        /// <summary>
        /// 当前分类所在层数
        /// </summary>
        public int G_Level
        {
            set { this._G_Level = value; }
            get { return this._G_Level; }
        }

        /// <summary>
        /// 当前分类子分类数
        /// </summary>
        public int G_ChildCount
        {
            set { this._G_ChildCount = value; }
            get { return this._G_ChildCount; }
        }

        /// <summary>
        /// 是否删除1:是0:否
        /// </summary>
        public int G_Delete
        {
            set { this._G_Delete = value; }
            get { return this._G_Delete; }
        }

        #endregion
	
	
    }
}
