/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				FieldWebControls.cs	                                    			*
 *      Description:																*
 *				 应用字段控件   												    *
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
using System.Collections;

namespace PmSys.WebControls
{
    /// <summary>
    /// 应用字段控件
    /// </summary>
    [
    AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal),
    ToolboxData("<{0}:FieldWebControls runat=\"server\"> </{0}:FieldWebControls>"),
    Description("应用字段控件")
    ]
    public class FieldWebControls : WebControl
    {
        #region "Private Variables"
        private string _F_Key = null;
        private int _LineNum = 5;
        private bool _LineNumOn = false;
        private FieldDispType _FieldLineDisp = FieldDispType.Select;
        private bool _UseCodeValue = false;
        private bool _Readonly = false;
        private bool _Disabled = false;
        //private string _Field_Name = "";
        //private string _Field_Value = "";
        private string _F_Title = "";
        #endregion

        #region "Public Variables"
        /// <summary>
        /// 是否只读状态
        /// </summary>
        [Description("是否只读状态"), Category("数据"), DefaultValue(false)]
        public bool Readonly
        {
            get
            {
                object m = ViewState["Readonly"];
                return m == null ? _Readonly : (bool)m;
            }
            set
            {
                ViewState["Readonly"] = value;
            }
        }

        /// <summary>
        /// 是否禁止状态
        /// </summary>
        [Description("是否禁止状态"), Category("数据"), DefaultValue(false)]
        public bool Disabled
        {
            get
            {
                object m = ViewState["Disabled"];
                return m == null ? _Disabled : (bool)m;
            }
            set
            {
                ViewState["Disabled"] = value;
            }
        }

        /// <summary>
        /// 设置js检测字符串
        /// </summary>
        [Description("设置js检测字符串"), Category("数据"), DefaultValue("")]
        public string F_Title
        {
            get
            {
                object m = ViewState["F_Title"];
                return m == null ? _F_Title : m.ToString();
            }
            set
            {
                ViewState["F_Title"] = value;
            }
        }

        /// <summary>
        /// 使用字段在表单中显示的名称
        /// </summary>
        [Description("使用字段在表单中显示的名称"), Category("数据"), DefaultValue("")]
        public string Field_Name
        {
            get
            {
                object m = ViewState["Field_Name"];
                return m == null ? F_Key : m.ToString();
            }
            set
            {
                ViewState["Field_Name"] = value;
            }
        }
        /// <summary>
        /// 需要选中的字段值(多个以,号分开)
        /// </summary>
        [Description("使用字段值(多个以,号分开)"), Category("数据"), DefaultValue("")]
        public string Field_Value
        {
            get
            {
                object m = ViewState["Field_Value"];
                return m == null ? "" : m.ToString();
            }
            set
            {
                ViewState["Field_Value"] = value;
            }
        }

        /// <summary>
        /// 应用字段Key
        /// </summary>
        [Description("应用字段Key"), Category("数据"), DefaultValue("")]
        public string F_Key
        {
            get
            {
                object m = ViewState["F_Key"];
                return m == null ? _F_Key : m.ToString();
            }
            set
            {
                ViewState["F_Key"] = value;
            }
        }

        /// <summary>
        /// 每行显示数(在LineNumOn设置为True才正常)
        /// </summary>
        [Description("每行显示数(在LineNumOn设置为True才正常)"), Category("数据"), DefaultValue(5)]
        public int LineNum
        {
            get
            {
                object m = ViewState["LineNum"];
                return m == null ? _LineNum : (int)m;
            }
            set
            {
                ViewState["LineNum"] = value;
            }
        }

        /// <summary>
        /// 是否允许以行方式显示(与LineNum配合使用)
        /// </summary>
        [Description("是否允许以行方式显示(与LineNum配合使用))"), Category("数据"), DefaultValue(false)]
        public bool LineNumOn
        {
            get
            {
                object m = ViewState["LineNumOn"];
                return m == null ? _LineNumOn : (bool)m;
            }
            set
            {
                ViewState["LineNumOn"] = value;
            }
        }

        /// <summary>
        /// 应用字段显示方式
        /// </summary>
        [Description("应用字段显示方式"), Category("数据"), DefaultValue(FieldDispType.Select)]
        public FieldDispType FieldDisp
        {
            get
            {
                object m = ViewState["FieldLineDisp"];
                return m == null ? _FieldLineDisp : (FieldDispType)m;
            }
            set
            {
                ViewState["FieldLineDisp"] = value;
            }
        }
        /// <summary>
        /// 是否使用应用字段编码代替V_ID
        /// </summary>
        [Description("是否使用应用字段编码代替V_ID"), Category("数据"), DefaultValue(false)]
        public bool UseCodeValue
        {
            get
            {
                object m = ViewState["UseCodeValue"];
                return m == null ? _UseCodeValue : (bool)m;
            }
            set
            {
                ViewState["UseCodeValue"] = value;
            }
        }

        #endregion

        /// <summary>
        /// 创建html
        /// </summary>
        /// <returns></returns>
        private string CreateHtml()
        {
            int RecordCount = 0;
            QueryParam qp = new QueryParam();
            qp.Where = string.Format("Where V_F_Key='{0}'", Common.inSQL(this.F_Key));
            qp.Orderfld = " V_ShowOrder ";
            qp.OrderType = 0;
            ArrayList lst = new ArrayList();
            try
            {
                lst = BusinessFacade.sys_FieldValueList(qp, out RecordCount);
            }
            catch
            {
                for (int i = 0; i < 5; i++)
                {
                    sys_FieldValueTable fv = new sys_FieldValueTable();
                    fv.ValueID = i;
                    fv.V_Text = string.Format("测试{0}", i);
                    lst.Add(fv);
                }
            }
            StringBuilder sb = new StringBuilder();
            string SelectOk = ""; //是否选中当前选项
            
            string rString = "";
            if (Disabled)
            {
                rString += " disabled ";
            }
            if (Readonly)
            {
                rString += " readonly ";
            }

            if (FieldDisp == FieldDispType.Select)
            {
                sb.AppendFormat("<select name={0} title=\"{1}\" {2}>", Field_Name, F_Title, rString);
                sb.Append("<option value=\"\">请选择...</option>");
                foreach (sys_FieldValueTable var in lst)
                {
                    if (Common.Check_Char_Is(UseCodeValue==false? var.ValueID.ToString():var.V_Code.Trim(), Field_Value))
                        SelectOk = "selected";
                    else
                        SelectOk = "";
                    sb.AppendFormat("<option value=\"{0}\" {2}>{1}</option>", UseCodeValue==false? var.ValueID.ToString():var.V_Code, var.V_Text, SelectOk);
                }
                sb.Append("</select>");
            }
            else if (FieldDisp == FieldDispType.CheckBox || FieldDisp== FieldDispType.RadioBox)
            {
                if (!LineNumOn)
                {
                    LineNum = 1;
                }
                int Tempi = 1; //当前记录数
                int TdWidth = 100/LineNum; //TD宽度
                int C_nextline = 0; //换行标识

                string InputTypeTxt = "checkbox";

                if (FieldDisp == FieldDispType.RadioBox)
                {
                    InputTypeTxt = "radio";
                }

                sb.Append("<table width=\"100%\" border=\"0\" cellpadding=\"2\" cellspacing=\"1\" ><tr>");

                foreach (sys_FieldValueTable var in lst)
                {
                    if (C_nextline == 1)
                    {
                        sb.Append("</tr>");
                        C_nextline = 0;
                        sb.Append("<tr>");
                    }
                    if (Common.Check_Char_Is(UseCodeValue == false ? var.ValueID.ToString() : var.V_Code.Trim(), Field_Value))
                        SelectOk = "checked";
                    else
                        SelectOk = "";
                    sb.AppendFormat("<td width=\"{5}%\"><input name=\"{0}\" id=\"{0}{6}\" type={4} value=\"{1}\" {3} {7}><label for=\"{0}{6}\">{2}</label></td>", Field_Name, UseCodeValue == false ? var.ValueID.ToString() : var.V_Code.Trim(), var.V_Text, SelectOk, InputTypeTxt, TdWidth, Tempi, rString);

                    //产生换行标识
                    if ((Tempi % LineNum) == 0)
                    {
                        C_nextline = 1;
                    }
                    Tempi++;
                }
                //补齐td
                int LostTDNum = LineNum-lst.Count % LineNum;
                if (LostTDNum != LineNum && LineNum > 0)
                {
                    for (int i = 0; i < LostTDNum; i++)
                    {
                        sb.AppendFormat("<td width=\"{0}%\">&nbsp;</td>",TdWidth);
                    }
                    sb.Append("</tr>");
                }

                sb.Append("</table>");
            }
            return sb.ToString();
        }



        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(CreateHtml());
        }


    }
    /// <summary>
    /// 应用字段显示类型
    /// </summary>
    public enum FieldDispType
    {
        /// <summary>
        /// 以Select方式显示
        /// </summary>
        Select = 1,
        /// <summary>
        /// 以Check方式显示
        /// </summary>
        CheckBox = 2,
        /// <summary>
        /// 以Radio方式显示
        /// </summary>
        RadioBox = 3

    }


}
