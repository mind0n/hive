/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				HeadMenuWebControls.cs	                                   			*
 *      Description:																*
 *				 头部菜单web控件 			    								    *
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
using System.Web.UI.WebControls;
using System.Drawing.Design;
using PmSys.Components;


namespace PmSys.WebControls
{
    /// <summary>
    /// 头部菜单web控件
    /// </summary>
    [
    AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal),
    DefaultProperty("ButtonList"),
    ParseChildren(true, "ButtonList"),
    ToolboxData("<{0}:HeadMenuWebControls runat=\"server\"> </{0}:HeadMenuWebControls>"),
    Description("头部菜单web控件")
    ]
    public class HeadMenuWebControls:WebControl
    {
        /// <summary>
        /// 构造函数,不能少如果用泛型需要初始化
        /// </summary>
        public HeadMenuWebControls()
        {
            _ButtonList = new List<HeadMenuButtonItem>();
        }

        #region "Private Variables"
        private  List<HeadMenuButtonItem> _ButtonList;
        private string HeadMenuTemplateTxt =
        @"<!-- 头部菜单 Start -->
	        <table border='0' cellpadding='0' cellspacing='0' width='100%' align='center'>
              <tr>
                <td class='menubar_title'><img border='0' src='{0}' align='absmiddle' hspace='3' vspace='3'>&nbsp;{1}</td>
                <td class='menubar_readme_text' valign='bottom'><img src='{2}' align='absMiddle' border='0' />&nbsp;{3}</td>
              </tr>
              <tr>
                <td height='27px' class='menubar_function_text'>目前操作功能：{4}</td>
                <td class='menubar_menu_td' align='right'>{5}</td>
              </tr>
              <tr><td height='5px' colspan='2'></td></tr>
            </table>
        <!-- 头部菜单 End -->
        ";
        private string _HeadIconPath = "~/Manager/images/ICON/";
        private string _HeadTitleIcon = "";
        private string _HeadTitleTxt = "标题";
        private string _HeadHelpIcon = "office.gif";
        private string _HeadHelpTxt = "帮助？";
        private string _HeadOPTxt = "";


        private string CreateButtonHtml()
        {
            StringBuilder sb = new StringBuilder();
            if (_ButtonList != null && _ButtonList.Count > 0)
            {
                sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr>");
                string OnUrlJs = "";
                string ButtonIcon = "";
                string ButtonTxt = "";
                int DispButtonNum = 0;
                for (int i = 0; i < _ButtonList.Count; i++)
                {
                    if (_ButtonList[i].ButtonVisible && FrameWorkPermission.CheckButtonPermission(_ButtonList[i].ButtonPopedom))
                    {
                        DispButtonNum++;
                        OnUrlJs = "";
                        ButtonIcon = "";
                        ButtonTxt = "";
                        switch (_ButtonList[i].ButtonUrlType)
                        {
                            case UrlType.Href:
                                OnUrlJs = string.Format("JavaScript:window.location.href='{0}';", _ButtonList[i].ButtonUrl);
                                break;
                            case UrlType.JavaScript:
                                OnUrlJs = string.Format("JavaScript:{0}", _ButtonList[i].ButtonUrl);
                                break;
                            case UrlType.VBScript:
                                OnUrlJs = string.Format("VBScript:{0}", _ButtonList[i].ButtonUrl);
                                break;
                        }
                        if (_ButtonList[i].ButtonIcon != string.Empty)
                        {
                            ButtonIcon = HeadIconPath + _ButtonList[i].ButtonIcon;
                        }
                        else
                        {
                            if (Convert.ToInt32(_ButtonList[i].ButtonPopedom) > 256)
                                ButtonIcon = HeadIconPath + "Custom.gif";
                            else
                                ButtonIcon = HeadIconPath + _ButtonList[i].ButtonPopedom.ToString() + ".gif";
                            switch (_ButtonList[i].ButtonPopedom)
                            {
                                case PopedomType.A:
                                    ButtonTxt = "备用A";
                                    break;
                                case PopedomType.B:
                                    ButtonTxt = "备用B";
                                    break;
                                case PopedomType.Delete:
                                    ButtonTxt = "删除";
                                    break;
                                case PopedomType.Edit:
                                    ButtonTxt = "修改";
                                    break;
                                case PopedomType.List:
                                    ButtonTxt = "列表";
                                    break;
                                case PopedomType.Orderby:
                                    ButtonTxt = "排序";
                                    break;
                                case PopedomType.New:
                                    ButtonTxt = "新增";
                                    break;
                                case PopedomType.Print:
                                    ButtonTxt = "打印";
                                    break;
                                default:
                                    ButtonTxt = "自定义";
                                    break;

                            }
                        }
                        sb.AppendFormat("<td class=\"menubar_button\" id=\"button_{1}\" OnClick=\"{0}\" OnMouseOut=\"javascript:MenuOnMouseOver(this);\" OnMouseOver=\"javascript:MenuOnMouseOut(this);\">", OnUrlJs, i);
                        sb.AppendFormat("<img border=\"0\" align=\"texttop\" src=\"{0}\">&nbsp;", ButtonIcon);
                        sb.AppendFormat("{0}{1}</td>", ButtonTxt, _ButtonList[i].ButtonName);
                    }
                }
                if (DispButtonNum == 0)
                    sb.Append("<td>&nbsp;</td>");
                sb.Append("</tr></table>");
            }
            if (sb.ToString() == string.Empty)
                sb.Append("&nbsp");
            return sb.ToString();
        }


        #endregion

        #region "Public Variables"
        /// <summary>
        /// 读取/设置头部菜单路径
        /// </summary>
        [Description("读取/设置头部菜单路径"), Category("外观"), DefaultValue("~/images/ICON/")]
        public string HeadIconPath
        {
            get
            {
                object m = ViewState["HeadIconPath"];
                return m == null ? ResolveClientUrl(_HeadIconPath) : ResolveClientUrl(m.ToString());
            }
            set
            {
                ViewState["HeadIconPath"] = value;
            }
        }
        /// <summary>
        /// 读取/设置标题Icon图片名(为空,取默认菜单icon)
        /// </summary>
        [Description("读取/设置标题Icon图片名(为空,取默认菜单icon)"), Category("外观"), DefaultValue("default.gif")]
        public string HeadTitleIcon
        {
            get
            {
                object m = ViewState["HeadTitleIcon"];
                return m == null ? "" : string.Format("{0}", m); 
                //return ResolveClientUrl(FrameWorkPermission.GetMenuIcon);
                //if (string.IsNullOrEmpty(_HeadTitleIcon))
                //{
                //    return m == null ? string.Format("{0}", ResolveClientUrl(FrameWorkPermission.GetMenuIcon)) : string.Format("{0}{1}", HeadIconPath, m);
                //}
                //else
                //{
                //    return m == null ? string.Format("{0}{1}", HeadIconPath, _HeadTitleIcon) : string.Format("{0}{1}", HeadIconPath, m);
                //}
            }
            set
            {
                ViewState["HeadTitleIcon"] = value;
            }
        }
        /// <summary>
        /// 读取/设置标题名称
        /// </summary>
        [Description("读取/设置标题名称"), Category("外观"), DefaultValue("标题名称")]
        public string HeadTitleTxt
        {
            get
            {
                object m = ViewState["HeadTitleTxt"];
                return m == null ? _HeadTitleTxt : m.ToString();
            }
            set
            {
                ViewState["HeadTitleTxt"] = value;
            }
        }

        /// <summary>
        /// 读取/设置帮助Icon名称
        /// </summary>
        [Description("读取/设置帮助Icon图片名"), Category("外观"), DefaultValue("office.gif")]
        public string HeadHelpIcon
        {
            get
            {
                object m = ViewState["HeadHelpIcon"];
                return m == null ? _HeadHelpIcon : m.ToString();
                //return m == null ? string.Format("{0}{1}", HeadIconPath, _HeadHelpIcon) : string.Format("{0}{1}", HeadIconPath, m);
            }
            set
            {
                ViewState["HeadHelpIcon"] = value;
            }
        }
        /// <summary>
        /// 读取/设置帮助文字
        /// </summary>
        [Description("读取/设置帮助文字"), Category("外观"), DefaultValue("帮助？")]
        public string HeadHelpTxt
        {
            get
            {
                object m = ViewState["HeadHelpTxt"];
                return m == null ? _HeadHelpTxt : m.ToString();
            }
            set
            {
                ViewState["HeadHelpTxt"] = value;
            }
        }
        /// <summary>
        /// 读取/设置操作说明
        /// </summary>
        [Description("读取/设置操作说明"), Category("外观"), DefaultValue("")]
        public string HeadOPTxt
        {
            get
            {
                object m = ViewState["HeadOPTxt"];
                return m == null ? _HeadOPTxt : m.ToString();
            }
            set
            {
                ViewState["HeadOPTxt"] = value;
            }
        }
        /// <summary>
        /// 按钮集合
        /// </summary>
        [
        Category("Behavior"),
        Description("按钮集合"),
        Editor(typeof(CollectionEditor), typeof(UITypeEditor)),
        PersistenceMode(PersistenceMode.InnerDefaultProperty)
        ]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)] 
        public List<HeadMenuButtonItem> ButtonList
        {
            get
            {
                //object m = ViewState["ButtonList"];
                //return m == null ? _ButtonList : (List<HeadMenuButtonItem>)m;
                return _ButtonList;
            }
            //set
            //{
            //    ViewState["ButtonList"] = value;
            //}
        }

        /// <summary>
        /// 重写RenderContents方法
        /// </summary>
        /// <param name="writer"></param>
        protected override void  Render(HtmlTextWriter writer)
        {
            if (HeadTitleIcon == "")
                _HeadTitleIcon = ResolveClientUrl(FrameWorkPermission.GetMenuIcon);
            else
            {
                _HeadTitleIcon = string.Format("{0}{1}", HeadIconPath, HeadTitleIcon);
            }
            if (HeadHelpIcon == "")
                HeadHelpIcon = _HeadHelpIcon ;
            writer.Write(HeadMenuTemplateTxt, _HeadTitleIcon, HeadTitleTxt, HeadIconPath+HeadHelpIcon, HeadHelpTxt, HeadOPTxt, CreateButtonHtml());
        }
        #endregion
    }
}
