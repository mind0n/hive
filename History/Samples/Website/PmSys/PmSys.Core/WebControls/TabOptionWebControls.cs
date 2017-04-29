/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				TabOptionWebControls.cs                  	                		*
 *      Description:																*
 *				 选项卡Web控件     		   							        	    *
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
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Drawing.Design;
using PmSys.Components;
using System.Collections.Specialized;

namespace PmSys.WebControls
{
    /// <summary>
    /// 选项卡Web控件
    /// </summary>
    [Designer(typeof(MyContainerControlDesigner))]
    [ControlBuilder(typeof(MultiViewControlBuilder))]
    [
    Description("选项卡Web控件"),
    ToolboxData("<{0}:TabOptionWebControls runat=\"server\"></{0}:TabOptionWebControls>"),
    ParseChildren(typeof(TabOptionItem)),
    AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), 
    AspNetHostingPermission(SecurityAction.InheritanceDemand, 
        Level = AspNetHostingPermissionLevel.Minimal)]
    public class TabOptionWebControls : Control
    {
        #region "Private Variables"
        private string _TabHtml =
        @"<!-- 选项卡 Start -->
        <TABLE cellSpacing=0 cellPadding=0 width='100%' align=center border=0>   
        <TBODY>   
	        <TR>     
		        <TD style='PADDING-LEFT: 2px; HEIGHT: 22px' background='{0}tab_top_bg.gif'>
			        <TABLE cellSpacing=0 cellPadding=0 border=0>
				        <TBODY><TR>
                                    <!--按钮　Start-->
                                    {1}
                                    <!--按钮 End-->
						        </TR>
				        </TBODY>
			        </TABLE>
		        </TD>
	        </TR>
	        <TR>
	        <TD bgColor=#ffffff>           
		        <TABLE cellSpacing=0 cellPadding=0 width='100%' border=0>
		        <TBODY>
		        <TR>
			        <TD width=1 background='{0}tab_bg.gif'><IMG  height=1 src='{0}tab_bg.gif'  width=1></TD>
			        <TD style='PADDING-RIGHT: 15px; PADDING-LEFT: 15px; PADDING-BOTTOM: 15px; PADDING-TOP: 15px; HEIGHT: 100px' vAlign=top>
        ";
        private string _TabHtmlEnd =
        @"
			        </TD>
			        <TD width=1 background='{0}tab_bg.gif'><IMG height=1 src='{0}tab_bg.gif'  width=1></TD>
		        </TR>
		        </TBODY>
		        </TABLE>
	        </TD>
	        </TR>
	        <TR>
		        <TD background='{0}tab_bg.gif' bgColor='#ffffff'><IMG height=1 src='{0}tab_bg.gif' width='1'></TD>
	        </TR>
        </TBODY>
        </TABLE>
        <!--选项卡 End-->
        ";
        private string _TabButton =
        @"<TD {4}>
	        <TABLE height=22 cellSpacing=0 cellPadding=0 border=0>
		        <TBODY>
                       <TR>
				         <TD width=3><IMG id=tabImgLeft__{1} height=22 src='{0}tab_unactive_left.gif'  width=3></TD>
				         <TD class=tab id=tabLabel__{1} onclick='javascript:tabClick({1},{2})' background='{0}tab_unactive_bg.gif' UNSELECTABLE='on'>{3}</TD>
				         <TD width=3><IMG id=tabImgRight__{1} height=22 src='{0}tab_unactive_right.gif' width=3></TD>
			           </TR>
		        </TBODY>
	        </TABLE>
        </TD>";
        private string _TabImagesPath = "~/Manager/images/Menu/";
        private string TabButton;
        /// <summary>
        /// 生成选项卡
        /// </summary>
        private void CreateTabHtml()
        {
            StringBuilder sbTabButton = new StringBuilder();
            if (TaboptionItems.Count > 0)
            {
                string IsDisp = "";
                for (int i = 0; i < TaboptionItems.Count; i++)
                {
                    IsDisp = "";
                    if (TaboptionItems[i].Visible == false)
                        IsDisp = "style='display:none'";
                    sbTabButton.AppendFormat(_TabButton, TabImagesPath, i, TaboptionItems.Count, TaboptionItems[i].Tab_Name, IsDisp);                    
                }
            }
            TabButton = sbTabButton.ToString();
        }

        private string _TabJs =
        @"<script language='javascript'>
        function tabClick(idx,count) {1}
          for (i_tr = 0; i_tr < count; i_tr++) {1}
            if (i_tr == idx) {1}
              var tabImgLeft = document.getElementById('tabImgLeft__' + idx);
              var tabImgRight = document.getElementById('tabImgRight__' + idx);
              var tabLabel = document.getElementById('tabLabel__' + idx);
              var tabContent = document.getElementById('tabContent__' + idx);

              tabImgLeft.src = '{0}tab_active_left.gif';
              tabImgRight.src = '{0}tab_active_right.gif';
              tabLabel.style.backgroundImage = ""url({0}tab_active_bg.gif)"";
              tabContent.style.visibility = 'visible';
              tabContent.style.display = 'block';
              continue;
            {2}
            var tabImgLeft = document.getElementById('tabImgLeft__' + i_tr);
            var tabImgRight = document.getElementById('tabImgRight__' + i_tr);
            var tabLabel = document.getElementById('tabLabel__' + i_tr);
            var tabContent = document.getElementById('tabContent__' + i_tr);

            tabImgLeft.src = '{0}tab_unactive_left.gif';
            tabImgRight.src = '{0}tab_unactive_right.gif';
            tabLabel.style.backgroundImage = ""url({0}tab_unactive_bg.gif)"";
            tabContent.style.visibility = 'hidden';
            tabContent.style.display = 'none';
          {2}
          document.getElementById('{5}').value=idx;
        {2}
        tabClick({3},{4});
       </script>";

        private string _HiddenInputName = Common.Get_WebCacheName + "SelectIndex";
        private string _HiddenSelectIndex = "<input type='hidden' ID='{0}' name='{0}' value='{1}'>";

        #endregion

        #region "Public Variables"
        /// <summary>
        /// 选择/设置选项卡
        /// </summary>
        public int SelectIndex
        {
            get {

                object m = ViewState["SelectIndex"];
                return m == null ? 0 : Convert.ToInt32(m);
            }
            set {
                if (value < 0 || value >= TaboptionItems.Count)
                {
                    value = 0;
                }
                ViewState["SelectIndex"] = value;
            }
        }
        /// <summary>
        /// 读取/设置选项卡图片路径
        /// </summary>
        [Description("读取/设置选项卡图片路径"), Category("外观"), DefaultValue("~/images/Menu/")]
        public string TabImagesPath
        {
            get
            {
                object m = ViewState["TabImagesPath"];
                return m == null ? ResolveClientUrl(_TabImagesPath) : ResolveClientUrl(m.ToString());
            }
            set
            {
                ViewState["TabImagesPath"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 重写RenderContents方法
        /// </summary>
        /// <param name="writer"></param>
        protected override void  Render(HtmlTextWriter writer)
        {
            CreateTabHtml();
            writer.Write(_TabHtml, TabImagesPath, TabButton);
            writer.Write(_HiddenSelectIndex, _HiddenInputName, SelectIndex);
            for (int i = 0; i < TaboptionItems.Count; i++)
            {
                writer.Write("<!--内容框Start-->");
                writer.Write("<DIV id='tabContent__{0}' style='display:none'>", i);
                TaboptionItems[i].RenderControl(writer);
                writer.Write("</DIV>");
                writer.Write("<!--内容框End-->");
            }
            writer.Write(_TabHtmlEnd, TabImagesPath);
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(string), "TabJs", string.Format(_TabJs, TabImagesPath, "{", "}", SelectIndex, TaboptionItems.Count,_HiddenInputName));
        }

        /// <summary>
        /// 重写增加控件方法
        /// </summary>
        /// <param name="obj"></param>
        protected override void AddParsedSubObject(object obj)
        {
            if (obj is TabOptionItem)
            {
                this.Controls.Add((TabOptionItem)obj);
            }
            else if (!(obj is LiteralControl))
            {
                throw new HttpException(string.Format("MultiView_cannot_have_children_of_type", new object[] { obj.GetType().Name }));
            }
        }

        /// <summary>
        /// 重写加载事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            if (HttpContext.Current.Request.Form[_HiddenInputName] != null)
                this.SelectIndex = Convert.ToInt32(HttpContext.Current.Request.Form[_HiddenInputName]);
            base.OnInit(e);
        }

        /// <summary>
        /// 重写创建子控件集合
        /// </summary>
        /// <returns></returns>
        protected override ControlCollection CreateControlCollection()
        {
            return new TaboptionItemCollection(this);
        }

        

        /// <summary>
        /// 获取子控件集合
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerDefaultProperty), Browsable(false)]
        public virtual TaboptionItemCollection TaboptionItems
        {
            get
            {
                return (TaboptionItemCollection)this.Controls;
            }
        }

    }
}
