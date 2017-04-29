/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				HeadMenuButtonItem.cs	                                   			*
 *      Description:																*
 *				 菜单按钮   				    								    *
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
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using PmSys.Components;

namespace PmSys.WebControls
{
    /// <summary>
    /// 菜单按钮
    /// </summary>
    [TypeConverter(typeof(TypeConverter))]
    public class HeadMenuButtonItem
    {
        #region "Private Variables"
        private string _ButtonName;
        private string _ButtonUrl;
        private PopedomType _ButtonPopedom;
        private UrlType _ButtonUrlType = UrlType.Href ;
        private string _ButtonIcon;
        private bool _ButtonVisible = true;
        #endregion

        #region "Public Variables"
        /// <summary>
        /// 构造函数
        /// </summary>
        public HeadMenuButtonItem()
            : this(String.Empty, String.Empty, PopedomType.List, UrlType.Href, string.Empty, true)
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_ButtonName">按钮名称</param>
        /// <param name="_ButtonUrl">按钮链接</param>
        /// <param name="_ButtonPopedom">按钮所属权限</param>
        /// <param name="_ButtonUrlType">按钮链接类型</param>
        /// <param name="_ButtonIcon">按钮Icon</param>
        /// <param name="_ButtonVisible">是否显示</param>
        public HeadMenuButtonItem(string _ButtonName, string _ButtonUrl, PopedomType _ButtonPopedom, 
            UrlType _ButtonUrlType, string _ButtonIcon, bool _ButtonVisible
            )
        {
            this._ButtonIcon = _ButtonIcon;
            this._ButtonName = _ButtonName;
            this._ButtonPopedom = _ButtonPopedom;
            this._ButtonUrl = _ButtonUrl;
            this._ButtonUrlType = _ButtonUrlType;
            this._ButtonVisible = _ButtonVisible;
        }


        
        /// <summary>
        /// 按钮名称
        /// </summary>
        [
        Category("Behavior"),
        DefaultValue(""),
        Description("按钮名称"),
        NotifyParentProperty(true),
        ]
        public string ButtonName
        {
            get {
                return _ButtonName;
            }
            set {
                _ButtonName = value;
            }
        }
        /// <summary>
        /// 按钮链接
        /// </summary>
        [
        Category("Behavior"),
        DefaultValue(""),
        Description("按钮链接"),
        NotifyParentProperty(true),
        ]
        public string ButtonUrl
        {
            get {
                return _ButtonUrl;
            }
            set {
                _ButtonUrl = value;
            }
        }
        /// <summary>
        /// 按钮所属权限
        /// </summary>
        [
        Category("Behavior"),
        DefaultValue(""),
        Description("按钮所属权限"),
        NotifyParentProperty(true),
        ]
        public PopedomType ButtonPopedom
        {
            get {
                return _ButtonPopedom;
            }
            set {
                _ButtonPopedom = value;
            }
        }
        /// <summary>
        /// 按钮链接类型 默认为url
        /// </summary>
        [
        Category("Behavior"),
        DefaultValue(""),
        Description("按钮链接类型 默认为url"),
        NotifyParentProperty(true),
        ]
        public UrlType ButtonUrlType
        {
            get {
                return _ButtonUrlType;
            }
            set {
                _ButtonUrlType = value;
            }
        }
        /// <summary>
        /// 按钮Icon 默认为空
        /// </summary>
        [
        Category("Behavior"),
        DefaultValue(""),
        Description("按钮Icon 默认为空"),
        NotifyParentProperty(true),
        ]
        public string ButtonIcon
        {
            get {
                return _ButtonIcon;
            }
            set {
                _ButtonIcon = value;
            }
        }
        /// <summary> 
        /// 按钮是否显示 默认为true
        /// </summary>

        [
        Category("Behavior"),
        DefaultValue(""),
        Description("按钮是否显示 默认为true"),
        NotifyParentProperty(true),
        ]
        public bool ButtonVisible
        {
            get {
                return _ButtonVisible;
            }
            set {
                _ButtonVisible = value;
            }
        }
        #endregion
    }


}
