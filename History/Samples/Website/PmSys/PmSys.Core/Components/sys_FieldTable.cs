/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				sys_FieldTable.cs                                  		        	*
 *      Description:																*
 *				 应用字段实体类 		            							    *
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
    /// 应用字段实体类
    /// </summary>
    public class sys_FieldTable
    {

        #region "Private Variables"
        private string _DB_Option_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除 
        private int _FieldID = 0;  // 应用字段ID号
        private string _F_Key; //应用字段关键字
        private string _F_CName;  // 应用字段中文说明
        private string _F_Remark;  // 描述说明
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
        /// 应用字段ID号
        /// </summary>
        public int FieldID
        {
            set { this._FieldID = value; }
            get { return this._FieldID; }
        }

        /// <summary>
        /// 应用字段关键字
        /// </summary>
        public string F_Key
        {
            set { this._F_Key = value; }
            get { return this._F_Key; }
        }

        /// <summary>
        /// 应用字段中文说明
        /// </summary>
        public string F_CName
        {
            set { this._F_CName = value; }
            get { return this._F_CName; }
        }

        /// <summary>
        /// 描述说明
        /// </summary>
        public string F_Remark
        {
            set { this._F_Remark = value; }
            get { return this._F_Remark; }
        }

        #endregion
	
	
    }
}
