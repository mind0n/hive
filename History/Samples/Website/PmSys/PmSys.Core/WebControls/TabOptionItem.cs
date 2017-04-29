/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				TabOptionItem.cs                           	                		*
 *      Description:																*
 *				 选项卡        		    							        	    *
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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Specialized;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Drawing.Design;

namespace PmSys.WebControls
{
    /// <summary>
    /// 选项卡
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Designer(typeof(MyContainerControlDesigner))]
    [ParseChildren(false)]
    [PersistChildren(true)]
    public class TabOptionItem:Control
    {
        #region "Private Variables"
        private string _Tab_Name;
        private bool _Tab_Visible = true;
        #endregion

        #region "Public Variables"
        /// <summary>
        /// 选项卡名称
        /// </summary>
        public string Tab_Name
        {
            get {
                return _Tab_Name;
            }
            set {
                _Tab_Name = value;
            }
        }
        /// <summary>
        /// 选项卡是否显示
        /// </summary>
        public bool Tab_Visible
        {
            get {
                return _Tab_Visible;
            }
            set {
                _Tab_Visible = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TabOptionItem()
            : this(String.Empty, true)
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_Tab_Name"></param>
        /// <param name="_Tab_Visible"></param>
        public TabOptionItem(string _Tab_Name,bool _Tab_Visible)
        {
            this._Tab_Name = _Tab_Name;
            this._Tab_Visible = _Tab_Visible;
        }
        #endregion
    }
}
