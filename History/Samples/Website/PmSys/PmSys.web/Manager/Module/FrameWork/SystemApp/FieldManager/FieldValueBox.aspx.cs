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
    public partial class FieldValueBox : System.Web.UI.Page
    {
        string CMD = (string)Common.sink("CMD", MethodType.Get, 50, 1, DataType.Str);
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

            Button2.Attributes.Add("onclick","return doConfirm(this.form);");

            if (CMD == "OrderBy")
            {
                Table_OrderBy_FieldValue.Visible = true;
                string F_Key = (string)Common.sink("F_Key", MethodType.Get, 50, 1, DataType.CharAndNum);
                QueryParam qp = new QueryParam();
                qp.Where = string.Format("where V_F_Key = '{0}'", Common.inSQL(F_Key));
                qp.Orderfld = " V_ShowOrder ";
                qp.OrderType = 0;
                int RecordCount = 0;
                ArrayList lst = BusinessFacade.sys_FieldValueList(qp, out RecordCount);
                OrderByListItems.DataTextField = "V_Text";
                OrderByListItems.DataValueField = "ValueID";
                OrderByListItems.DataSource = lst;
                OrderByListItems.DataBind();

                Button3.OnClientClick = string.Format("selectAll({0})", OrderByListItems.UniqueID);


            }
            else
            {
                int FieldID = (int)Common.sink("FieldID", MethodType.Get, 255, 1, DataType.Int);
                sys_FieldTable ft = BusinessFacade.sys_FieldDisp(FieldID);
                F_Key.Text = ft.F_Key;
                F_CName.Text = ft.F_CName;
                Table_Manager_FieldValue.Visible = true;
                if (CMD == "Edit")
                {
                    int ValueID = (int)Common.sink("ValueID", MethodType.Get, 255, 1, DataType.Int);
                    V_Text_Input.Text = BusinessFacade.sys_FieldValueDisp(ValueID).V_Text;
                    V_Code_Input.Text = BusinessFacade.sys_FieldValueDisp(ValueID).V_Code;
                }
                else
                    Button2.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int FieldID = (int)Common.sink("FieldID", MethodType.Get, 255, 1, DataType.Int);
            sys_FieldTable ft = BusinessFacade.sys_FieldDisp(FieldID);
            string V_Text_Value = (string)Common.sink(V_Text_Input.UniqueID, MethodType.Post, 100, 1, DataType.Str);
            string V_Code_Value = (string)Common.sink(V_Code_Input.UniqueID, MethodType.Post, 100, 0, DataType.Str);
            int ValueID = (int)Common.sink("ValueID", MethodType.Get, 255, 0, DataType.Int);
            sys_FieldValueTable fvt = BusinessFacade.sys_FieldValueDisp(ValueID);
            fvt.V_Text = V_Text_Value;
            fvt.V_Code = V_Code_Value;
            fvt.V_F_Key = ft.F_Key;
            string Messages = "";
            if (CMD == "Edit")
            {
                fvt.DB_Option_Action_ = "Update";
                Messages = string.Format("修改应用字段值(ID:{0})成功!",fvt.ValueID);
            }
            else if (CMD == "New")
            {

                QueryParam qp = new QueryParam();
                qp.Where = string.Format("where V_F_Key = '{0}'", Common.inSQL(ft.F_Key));
                qp.Orderfld = " V_ShowOrder ";
                qp.PageSize = 1;
                int RecordCount = 0;
                ArrayList lst = BusinessFacade.sys_FieldValueList(qp, out RecordCount);
                if (RecordCount == 0)
                    fvt.V_ShowOrder = 0;
                else
                    fvt.V_ShowOrder = ((sys_FieldValueTable)lst[0]).V_ShowOrder + 1;
                fvt.DB_Option_Action_ = "Insert";
                Messages = string.Format("增加应用字段值({0})成功!",fvt.V_Text);
            }

            BusinessFacade.sys_FieldValueInsertUpdate(fvt);
            EventMessage.EventWriteDB(1, Messages);
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(string), "TabJs", "<script language='javascript'>window.returnVal=\"" + Messages + "\";window.parent.hidePopWin(true);</script>");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string ItemsList = (string)Common.sink(OrderByListItems.UniqueID, MethodType.Post, 100000, 1, DataType.Str);
            if (ItemsList.Length > 0)
            {
                string[] ItemsLists = ItemsList.Split(',');
                for (int i = 0; i < ItemsLists.Length; i++)
                {

                    BusinessFacade.Update_Table_Fileds("sys_FieldValue", string.Format("V_ShowOrder={0}", i + 1), string.Format("ValueID={0}", ItemsLists[i]));
                }
            }

            EventMessage.EventWriteDB(1, "排序应用字段值成功!");
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(string), "TabJs", "<script language='javascript'>window.returnVal='排序成功!';window.parent.hidePopWin(true);</script>");
 
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int ValueID = (int)Common.sink("ValueID", MethodType.Get, 255, 1, DataType.Int);
            sys_FieldValueTable ft = new sys_FieldValueTable();
            ft.DB_Option_Action_ = "Delete";
            ft.ValueID = ValueID;
            BusinessFacade.sys_FieldValueInsertUpdate(ft);
            EventMessage.EventWriteDB(1, string.Format("删除应用字段值({0})成功!",ft.V_Text));
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(typeof(string), "TabJs", "<script language='javascript'>window.returnVal='删除成功!';window.parent.hidePopWin(true);</script>");

        }
    }
}
