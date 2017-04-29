/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				TaboptionItemCollection.cs                	                		*
 *      Description:																*
 *				 选项卡集合        		   							        	    *
 *      Author:																		*
 *				Lzppcc														        *
 *				Lzppcc@hotmail.com													*
 *				http://www.supesoft.com												*
 *      Finish DateTime:															*
 *				2007年8月6日														*
 *      History:																	*
 ***********************************************************************************/
using System;
using System.Reflection;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;

namespace PmSys.WebControls
{
    /// <summary>
    /// 选项卡集合
    /// </summary>
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class TaboptionItemCollection:ControlCollection
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="owner"></param>
        public TaboptionItemCollection(Control owner)
            : base(owner)
        {
        }
        /// <summary>
        /// 增加控件
        /// </summary>
        /// <param name="v"></param>
        public override void Add(Control v)
        {
            if (!(v is TabOptionItem))
            {
                throw new ArgumentException("ViewCollection_must_contain_view");
            }
            base.Add(v);
        }
        /// <summary>
        /// 增加控件
        /// </summary>
        /// <param name="index"></param>
        /// <param name="v"></param>
        public override void AddAt(int index, Control v)
        {
            if (!(v is TabOptionItem))
            {
                throw new ArgumentException("ViewCollection_must_contain_view");
            }
            base.AddAt(index, v);
        }
        /// <summary>
        /// 获取TabOptionItem
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public new TabOptionItem this[int i]
        {
            get
            {
                return (TabOptionItem)base[i];
            }
        }
    }
}
