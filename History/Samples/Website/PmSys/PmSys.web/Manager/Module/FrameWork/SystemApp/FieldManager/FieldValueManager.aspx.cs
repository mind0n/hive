using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using PmSys.Components;
using PmSys.WebControls;

namespace PmSys.web.Module.PmSys.FieldManager
{
    public partial class FieldValueManager : System.Web.UI.Page
    {
        public int FieldID = (int)Common.sink("FieldID", MethodType.Get, 50, 0, DataType.Int);
        string CMD = (string)Common.sink("CMD", MethodType.Get, 50, 0, DataType.Str);
        protected void Page_Load(object sender, EventArgs e)
        {
            FrameWorkPermission.CheckPagePermission(CMD);
            if (!IsPostBack)
            {
                OnStart();    
            }
        }

        private void OnStart()
        {

            sys_FieldTable Ft = BusinessFacade.sys_FieldDisp(FieldID);
            F_Key_Txt.Text = Ft.F_Key;
            F_CName_Txt.Text = Ft.F_CName;
            F_Remark_Txt.Text = Ft.F_Remark;

            QueryParam qp = new QueryParam();
            qp.Where = string.Format("where V_F_Key = '{0}'", Common.inSQL(Ft.F_Key));
            qp.Orderfld = " V_ShowOrder ";
            qp.OrderType = 0;
            int RecordCount = 0;
            ArrayList lst = BusinessFacade.sys_FieldValueList(qp, out RecordCount);
            Main_Value.DataSource = lst;
            Main_Value.DataBind();

            if (CMD == "List")
            {
                HeadMenuButtonItem bi1 = new HeadMenuButtonItem();
                bi1.ButtonPopedom = PopedomType.Edit;
                bi1.ButtonName = "应用字段";
                bi1.ButtonUrl = string.Format("?CMD=Edit&FieldID={0}", FieldID);
                HeadMenuWebControls1.ButtonList.Add(bi1);

                HeadMenuButtonItem bi3 = new HeadMenuButtonItem();
                bi3.ButtonPopedom = PopedomType.New;
                bi3.ButtonName = "应用字段值";
                bi3.ButtonUrl = string.Format("showPopWin('应用字段值增加','FieldValueBox.aspx?FieldID={0}&CMD=New&{1}', 300, 160, AlertMessageBox,true);", FieldID, Common.RndNum(7));
                bi3.ButtonUrlType = UrlType.JavaScript;
                HeadMenuWebControls1.ButtonList.Add(bi3);

                HeadMenuButtonItem bi4 = new HeadMenuButtonItem();
                bi4.ButtonPopedom = PopedomType.Edit;
                bi4.ButtonName = "排序应用字段值";
                bi4.ButtonIcon = "Orderby.gif";
                bi4.ButtonUrl = string.Format("showPopWin('应用字段值排序','FieldValueBox.aspx?F_Key={0}&CMD=OrderBy&{1}', 180, 210, AlertMessageBox,true);", Ft.F_Key, Common.RndNum(7));
                bi4.ButtonUrlType = UrlType.JavaScript;
                HeadMenuWebControls1.ButtonList.Add(bi4);
                Disp_Input.Visible = false;



                F_Key_Txt.Visible = true;
                F_CName_Txt.Visible = true;
                F_Remark_Txt.Visible = true;
                F_Key.Visible = false;
                F_CName.Visible = false;
                F_Remark.Visible = false;
            }
            else if (CMD == "Edit")
            {
                F_Key.Text = Ft.F_Key;
                F_CName.Text = Ft.F_CName;
                F_Remark.Text = Ft.F_Remark;

                F_Key.Visible = false;
                F_Key_Txt.Visible = true;

                HeadMenuButtonItem bi5 = new HeadMenuButtonItem();
                bi5.ButtonIcon = "back.gif";
                bi5.ButtonPopedom = PopedomType.List;
                bi5.ButtonName = "返回";
                bi5.ButtonUrl = string.Format("?CMD=List&FieldID={0}", FieldID);
                HeadMenuWebControls1.ButtonList.Add(bi5);

                HeadMenuButtonItem bi2 = new HeadMenuButtonItem();
                bi2.ButtonPopedom = PopedomType.Delete;
                bi2.ButtonName = "应用字段";
                bi2.ButtonUrlType = UrlType.JavaScript;
                bi2.ButtonUrl = string.Format("DelData('?CMD=Delete&FieldID={0}')", FieldID);
                HeadMenuWebControls1.ButtonList.Add(bi2);



                Disp_Sub.Visible = false;
            }
            else if (CMD == "New")
            {
                Disp_Sub.Visible = false;
            }
            else if (CMD == "Delete")
            {

                Ft.DB_Option_Action_ = "Delete";
                BusinessFacade.sys_FieldInsertUpdate(Ft);

                foreach (sys_FieldValueTable var in lst)
                {
                    var.DB_Option_Action_ = "Delete";
                    BusinessFacade.sys_FieldValueInsertUpdate(var);
                }
                EventMessage.MessageBox(1, "操作成功", string.Format("{1}ID({0})成功!", FieldID, "删除应用字段"), Icon_Type.OK, Common.GetHomeBaseUrl("default.aspx"));

            }



        }




        protected void Button1_Click(object sender, EventArgs e)
        {
            string All_Title_Txt = "";
            PopedomType pt = PopedomType.New;
            sys_FieldTable Ft = BusinessFacade.sys_FieldDisp(FieldID);

            string F_CName_Value = (string)Common.sink(F_CName.UniqueID, MethodType.Post, 50, 1, DataType.Str);
            string F_Remark_Value = (string)Common.sink(F_Remark.UniqueID, MethodType.Post, 200, 0, DataType.Str);
            Ft.F_CName = F_CName_Value;
            Ft.F_Remark = F_Remark_Value;
            if (CMD == "New")
            {
                string F_Key_Value = (string)Common.sink(F_Key.UniqueID, MethodType.Post, 50, 1, DataType.CharAndNum);
                Ft.F_Key = F_Key_Value;
                All_Title_Txt = "增加";
                Ft.DB_Option_Action_ = "Insert";
            }
            else if (CMD == "Edit")
            {
                pt = PopedomType.Edit;
                All_Title_Txt = "修改";
                Ft.DB_Option_Action_ = "Update";
            }
            if (BusinessFacade.sys_FieldCheckPK(Ft, pt))
                    EventMessage.MessageBox(1, "操作失败", string.Format("存在相同的值({0})!", Ft.F_Key), Icon_Type.Alert, Common.GetHomeBaseUrl("default.aspx"));    
            BusinessFacade.sys_FieldInsertUpdate(Ft);
            EventMessage.MessageBox(1, "操作成功", string.Format("{1}ID({0})成功!", FieldID, All_Title_Txt), Icon_Type.OK, Common.GetHomeBaseUrl("default.aspx"));
        }
    }
}
