/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				sys_FieldValueTable.cs                             		        	*
 *      Description:																*
 *				 应用字段值实体类 		            							    *
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
    /// 应用字段值实体类
    /// </summary>
    public class sys_FieldValueTable
    {

        #region "Private Variables"
        private string _DB_Option_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除 
        private int _ValueID = 0;  // 索引ID号
        private string _V_F_Key; //与sys_Field表中F_Key字段关联
        private string _V_Text;  // 中文说明
        private int _V_ShowOrder = 0;  // 同级显示顺序
        private string _V_Code; //编码
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
        /// 索引ID号
        /// </summary>
        public int ValueID
        {
            set { this._ValueID = value; }
            get { return this._ValueID; }
        }

        /// <summary>
        /// 与sys_Field表中F_Key字段关联
        /// </summary>
        public string V_F_Key
        {
            set { this._V_F_Key = value; }
            get { return this._V_F_Key; }
        }

        /// <summary>
        /// 中文说明
        /// </summary>
        public string V_Text
        {
            set { this._V_Text = value; }
            get { return this._V_Text; }
        }

        /// <summary>
        /// 同级显示顺序
        /// </summary>
        public int V_ShowOrder
        {
            set { this._V_ShowOrder = value; }
            get { return this._V_ShowOrder; }
        }

        /// <summary>
        /// 编码
        /// </summary>
        public string V_Code
        {
            set {
                _V_Code = value;
            }
            get{
                return _V_Code; 
            }
        }
        #endregion
	
	
    }
}
