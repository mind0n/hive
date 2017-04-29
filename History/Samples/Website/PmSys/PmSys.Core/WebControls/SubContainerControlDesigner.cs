/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				MyContainerControlDesigner.cs                           			*
 *      Description:																*
 *				 选项卡编辑类       		    								    *
 *      Author:																		*
 *				Lzppcc														        *
 *				Lzppcc@hotmail.com													*
 *				http://www.supesoft.com												*
 *      Finish DateTime:															*
 *				2007年8月6日														*
 *      History:																	*
 ***********************************************************************************/
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.Design;

namespace PmSys.WebControls
{
    /// <summary>
    /// 选项卡编辑类
    /// </summary>
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public class MyContainerControlDesigner : ContainerControlDesigner
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MyContainerControlDesigner()
        {
            base.FrameStyle.Width = Unit.Percentage(100);
        }
    }
}
