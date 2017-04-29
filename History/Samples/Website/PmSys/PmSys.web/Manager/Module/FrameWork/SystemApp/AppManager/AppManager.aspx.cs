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

using PmSys;
using PmSys.Components;
using PmSys.WebControls;

namespace PmSys.web.Module.PmSys.AppManager
{
    public partial class AppManager : System.Web.UI.Page
    {
        int S_ID = (int)Common.sink("S_ID", MethodType.Get, 255, 0, DataType.Int);
        string CMD = (string)Common.sink("CMD", MethodType.Get, 50, 0, DataType.Str);
        string CMD_Txt = "查看";
        string App_Txt = "应用";
        string All_Title_Txt = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FrameWorkPermission.CheckPagePermission(CMD);
                OnStart();
            }

        }

        private void OnStart()
        {

            InputData();
            switch (CMD)
            {
                case "New":
                    CMD_Txt = "增加";
                    HiddenDisp();
                    TopTr.Visible = false;
                    break;
                case "Edit":
                    CMD_Txt = "修改";
                    HiddenDisp();
                    HeadMenuButtonItem Bm0 = new HeadMenuButtonItem();
                    Bm0.ButtonPopedom = PopedomType.List;
                    Bm0.ButtonUrl = string.Format("?CMD=Look&S_ID={0}", S_ID);
                    Bm0.ButtonIcon = "back.gif";
                    Bm0.ButtonName = "返回";
                    HeadMenuWebControls1.ButtonList.Add(Bm0);
                    HeadMenuButtonItem Bm1 = new HeadMenuButtonItem();
                    Bm1.ButtonPopedom = PopedomType.Delete;
                    Bm1.ButtonUrlType = UrlType.JavaScript;
                    Bm1.ButtonUrl = string.Format("DelData('?CMD=Delete&S_ID={0}')", S_ID);
                    HeadMenuWebControls1.ButtonList.Add(Bm1);
                    break;
                case "Look":
                    HiddenInput();
                    HeadMenuButtonItem Bm2 = new HeadMenuButtonItem();
                    Bm2.ButtonPopedom = PopedomType.Edit;
                    Bm2.ButtonUrl = string.Format("?CMD=Edit&S_ID={0}", S_ID);
                    HeadMenuWebControls1.ButtonList.Add(Bm2);
                    break;
                case "Delete":
                    CMD_Txt = "删除";
                    sys_ApplicationsTable sat = new sys_ApplicationsTable();
                    sat.ApplicationID = S_ID;
                    sat.DB_Option_Action_ = CMD;
                    if (sat.ApplicationID != 1)
                    {
                        //删除应用表
                        BusinessFacade.sys_ApplicationsInsertUpdate(sat);
                        //删除模块表
                        BusinessFacade.sys_Module_DeleteFormAppID(S_ID);
                        //删除角色应用权限表
                        BusinessFacade.sys_RolePermission_DeleteFromAppid(S_ID);
                        //删除角色应用表
                        BusinessFacade.sys_RoleApplication_DeleteFormAppid(S_ID);
                        
                    }
                    EventMessage.MessageBox(1, "操作成功", "删除记录ID:(" + S_ID + ")成功！", Icon_Type.OK, Common.GetHomeBaseUrl("list.aspx"));
                    break;
            }
            All_Title_Txt = CMD_Txt + App_Txt;
            HeadMenuWebControls1.HeadOPTxt = TabOptionItem1.Tab_Name = All_Title_Txt;
            //Button1.Attributes.Add("Onclick", "javascript:return checkForm(aspnetForm);");
            
        }

        private void InputData()
        {
            if (S_ID == 0)
                return;

            sys_ApplicationsTable SAT =  BusinessFacade.sys_ApplicationsDisp(S_ID);
            if (SAT.ApplicationID == 0)
                return;
            this.A_AppDescription.Text = this.A_AppDescription_Txt.Text = SAT.A_AppDescription;
            this.A_AppName.Text = this.A_AppName_Txt.Text = SAT.A_AppName;
            this.A_AppUrl.Text = this.A_AppUrl_Txt.Text = SAT.A_AppUrl;
            this.ApplicationID_Txt.Text = SAT.ApplicationID.ToString();

        }

        private void HiddenDisp()
        {
            
            this.A_AppName_Txt.Visible = false;
            this.A_AppDescription_Txt.Visible = false;
            this.A_AppUrl_Txt.Visible = false;
        }
        private void HiddenInput()
        {
            this.A_AppDescription.Visible = false;
            this.A_AppName.Visible = false;
            this.A_AppUrl.Visible = false;
            this.SubmitTr.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            sys_ApplicationsTable sat = new sys_ApplicationsTable();
            sat.ApplicationID = S_ID;
            sat.A_AppDescription = (string)Common.sink(A_AppDescription.UniqueID, MethodType.Post, 200, 0, DataType.Str);
            sat.A_AppName = (string)Common.sink(A_AppName.UniqueID, MethodType.Post, 50, 1, DataType.Str);
            sat.A_AppUrl = (string)Common.sink(A_AppUrl.UniqueID, MethodType.Post, 50, 0, DataType.Str);

            switch (CMD)
            {
                case "New":
                    CMD_Txt = "增加";
                    sat.DB_Option_Action_ = "Insert";
                    break;
                case "Edit":
                    CMD_Txt = "修改";
                    sat.DB_Option_Action_ = "Update";
                    break;
            }
            All_Title_Txt = CMD_Txt + App_Txt;
            BusinessFacade.sys_ApplicationsInsertUpdate(sat);
            EventMessage.MessageBox(1, "操作成功", string.Format("{1}ID({0})成功!", S_ID, All_Title_Txt), Icon_Type.OK, Common.GetHomeBaseUrl("list.aspx"));
        }
    }
}
