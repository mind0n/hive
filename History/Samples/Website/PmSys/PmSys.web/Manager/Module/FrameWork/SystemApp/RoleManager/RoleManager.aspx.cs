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

namespace PmSys.web.Module.PmSys.RoleManager
{
    public partial class RoleManager : System.Web.UI.Page
    {
        int RoleID = (int)Common.sink("RoleID", MethodType.Get, 255, 0, DataType.Int);
        string CMD = (string)Common.sink("CMD", MethodType.Get, 50, 0, DataType.Str);
        string CMD_Txt = "查看";
        string App_Txt = "角色";
        string All_Title_Txt = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            FrameWorkPermission.CheckPagePermission(CMD);
            BusinessFacade.sys_UserCheckManagerVoid();
            if (RoleID!=0)
                BusinessFacade.sys_Roles_CheckUserVoid(RoleID);
            BindButton();
            if (!Page.IsPostBack)
            {
                OnStart();
            }
        }
        private void OnStart()
        {
            if (CMD != "Look")
            {
                TabOptionItem2.Visible = false;
                TabOptionItem3.Visible = false;
                TabOptionItem4.Visible = false;
                tr_username.Visible = false;
            }
            else {

                if (!FrameWorkPermission.CheckButtonPermission(PopedomType.New))
                    TabOptionItem3.Visible = false;
                //绑定应用列表
                int Recordcount=0;
                QueryParam qp = new QueryParam();
                qp.Where = string.Format("Where A_RoleID = {0}",RoleID);
                qp.OrderType = 0;
                ArrayList lst = BusinessFacade.sys_RoleApplicationList(qp, out Recordcount);
                Repeater1.DataSource = lst;
                Repeater1.DataBind();

                QueryParam qp1 = new QueryParam();
                qp1.OrderType = 0;
                ArrayList lst1 = BusinessFacade.sys_ApplicationsList(qp1, out Recordcount);
                NewAppID.DataSource = lst1;
                NewAppID.DataTextField = "A_AppName";
                NewAppID.DataValueField = "ApplicationID";
                NewAppID.DataBind();
                foreach (sys_RoleApplicationTable var in lst)
                {
                    ListItem ditem1 = this.NewAppID.Items.FindByValue(var.A_ApplicationID.ToString());
                    if (ditem1 != null)
                        NewAppID.Items.Remove(ditem1);
                }

                ListItem item3 = new ListItem();
                item3.Text = "请选择应用";
                item3.Value = "";
                NewAppID.Items.Insert(0, item3);

                BindUserList();
            }

            InputData();

            //Button1.Attributes.Add("Onclick", "javascript:return checkForm(aspnetForm);");
            //Button2.Attributes.Add("Onclick", "javascript:return checkForm(aspnetForm);");
        }

        /// <summary>
        /// 绑定用户列表
        /// </summary>
        private void BindUserList()
        {
            int rInt = 0;
            QueryParam qp = new QueryParam();
            qp.Where = " Where R_RoleID="+RoleID;
            ArrayList lst = BusinessFacade.sys_UserRolesList(qp, out rInt);
            Repeater2.DataSource = lst;
            Repeater2.DataBind();

            //绑定选择用户列表
            qp = new QueryParam();
            qp.Where = "Where U_Status=0";
            rInt = 0;
            ArrayList lst2 = BusinessFacade.sys_UserList(qp, out rInt);
            DropDownList1.DataSource = lst2;
            DropDownList1.DataTextField = "U_LoginName";
            DropDownList1.DataValueField = "UserID";
            DropDownList1.DataBind();
            foreach (sys_UserRolesTable var in lst)
            {
                ListItem ditem1 = this.DropDownList1.Items.FindByValue(var.R_UserID.ToString());
                if (ditem1 != null)
                    DropDownList1.Items.Remove(ditem1);
            }

            ListItem item3 = new ListItem();
            item3.Text = "请选择用户";
            item3.Value = "";
            DropDownList1.Items.Insert(0, item3);
        }

        /// <summary>
        /// 绑定按钮
        /// </summary>
        private void BindButton()
        {
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
                    Bm0.ButtonUrl = string.Format("?CMD=Look&RoleID={0}", RoleID);
                    Bm0.ButtonIcon = "back.gif";
                    Bm0.ButtonName = "返回";
                    HeadMenuWebControls1.ButtonList.Add(Bm0);
                    HeadMenuButtonItem Bm1 = new HeadMenuButtonItem();
                    Bm1.ButtonPopedom = PopedomType.Delete;
                    Bm1.ButtonUrlType = UrlType.JavaScript;
                    Bm1.ButtonUrl = string.Format("DelData('?CMD=Delete&RoleID={0}')", RoleID);
                    HeadMenuWebControls1.ButtonList.Add(Bm1);
                    break;
                case "Look":
                    HiddenInput();
                    HeadMenuButtonItem Bm2 = new HeadMenuButtonItem();
                    Bm2.ButtonPopedom = PopedomType.Edit;
                    Bm2.ButtonUrl = string.Format("?CMD=Edit&RoleID={0}", RoleID);
                    HeadMenuWebControls1.ButtonList.Add(Bm2);
                    break;
                case "Delete":
                    CMD_Txt = "删除";
                    sys_RolesTable sat = new sys_RolesTable();
                    sat.RoleID = RoleID;
                    sat.DB_Option_Action_ = CMD;
                    BusinessFacade.sys_RolesInsertUpdate(sat);
                    BusinessFacade.sys_RoleApplication_Move(RoleID);
                    BusinessFacade.sys_RolePermission_Move(RoleID);
                    UserData.Move_RoleUserPermissionCache(RoleID);                  

                    EventMessage.MessageBox(1, "操作成功", "删除记录ID:(" + RoleID + ")成功！", Icon_Type.OK, Common.GetHomeBaseUrl("RoleList.aspx"));
                    break;
                case "Move":
                    sys_RoleApplicationTable rt = new sys_RoleApplicationTable();
                    rt.A_ApplicationID = (int)Common.sink("ApplicationID", MethodType.Get, 255, 1, DataType.Int);
                    rt.A_RoleID = RoleID;
                    rt.DB_Option_Action_ = "Delete";
                    BusinessFacade.sys_RoleApplicationInsertUpdate(rt);

                    BusinessFacade.sys_RolePermission_Move(rt.A_RoleID, rt.A_ApplicationID);
                    UserData.Move_RoleUserPermissionCache(RoleID);
                    EventMessage.MessageBox(1, "操作成功", "移除应用ID:(" + rt.A_ApplicationID + ")成功！", Icon_Type.OK, Common.GetHomeBaseUrl(string.Format("RoleManager.aspx?RoleID={0}&CMD=Look", RoleID)));
                    break;
                case "MoveUser":
                    //检测删除权限
                    FrameWorkPermission.CheckPermissionVoid(PopedomType.Delete);
                    int UserID = (int)Common.sink("UserID", MethodType.Get, 255, 1, DataType.Int);
                    UserData.Move_UserPermissionCache(UserID);
                    sys_UserRolesTable su = new sys_UserRolesTable();
                    su.DB_Option_Action_ = "Delete";
                    su.R_RoleID = RoleID;
                    su.R_UserID = UserID;
                    BusinessFacade.sys_UserRolesInsertUpdate(su);
                    EventMessage.MessageBox(1, "操作成功", string.Format("从角色ID:{0}中移除用户ID:{1}成功！",RoleID,UserID), Icon_Type.OK, Common.GetHomeBaseUrl(string.Format("RoleManager.aspx?RoleID={0}&CMD=Look", RoleID)));
                    break;
                    
            }
            All_Title_Txt = CMD_Txt + App_Txt;
            HeadMenuWebControls1.HeadOPTxt = TabOptionItem1.Tab_Name = All_Title_Txt;
        }

        private void InputData()
        {
            if (RoleID == 0)
                return;

            sys_RolesTable SAT = BusinessFacade.sys_RolesDisp(RoleID);
            if (SAT.RoleID == 0)
                return;
            this.R_Description.Text = this.R_Description_Txt.Text = SAT.R_Description;
            this.R_RoleName.Text = this.R_RoleName_Txt.Text = SAT.R_RoleName;
            this.RoleID_Txt.Text = SAT.RoleID.ToString();
            this.R_UserName.Text = BusinessFacade.sys_UserDisp(SAT.R_UserID).U_LoginName;

        }

        private void HiddenDisp()
        {

            this.R_Description_Txt.Visible = false;
            this.R_RoleName_Txt.Visible = false;
        }
        private void HiddenInput()
        {
            this.R_Description.Visible = false;
            this.R_RoleName.Visible = false;
            this.SubmitTr.Visible = false;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            sys_RolesTable sat = BusinessFacade.sys_RolesDisp(RoleID);
            sat.R_Description = (string)Common.sink(R_Description.UniqueID, MethodType.Post, 255, 0, DataType.Str);
            sat.R_RoleName = (string)Common.sink(R_RoleName.UniqueID, MethodType.Post, 50, 1, DataType.Str);

            switch (CMD)
            {
                case "New":
                    if (BusinessFacade.sys_Roles_PK(sat.R_RoleName, 0))
                        EventMessage.MessageBox(1, "数据输入出错", string.Format("存在相同的角色名称({0})", sat.R_RoleName)
                                                , Icon_Type.Error, "history.back()", UrlType.JavaScript);
                    CMD_Txt = "增加";
                    sat.R_UserID = UserData.GetUserDate.UserID;
                    sat.DB_Option_Action_ = "Insert";
                    sat.R_UserID = Common.Get_UserID;
                    break;
                case "Edit":
                    if (BusinessFacade.sys_Roles_PK(sat.R_RoleName, sat.RoleID))
                        EventMessage.MessageBox(1, "数据输入出错", string.Format("存在相同的角色名称({0})", sat.R_RoleName)
                                                , Icon_Type.Error, "history.back()", UrlType.JavaScript);
                    CMD_Txt = "修改";
                    sat.DB_Option_Action_ = "Update";
                    break;
            }
            All_Title_Txt = CMD_Txt + App_Txt;
            BusinessFacade.sys_RolesInsertUpdate(sat);
            EventMessage.MessageBox(1, "操作成功", string.Format("{1}ID({0})成功!", RoleID, All_Title_Txt), Icon_Type.OK, Common.GetHomeBaseUrl("RoleList.aspx"));

        }

        protected void GridView1_RowDataBound(object sender, RepeaterItemEventArgs e)
        {
            Literal AppNameTxt = (Literal)e.Item.FindControl("AppName");
            sys_RoleApplicationTable sr = (sys_RoleApplicationTable)e.Item.DataItem;
            AppNameTxt.Text = BusinessFacade.sys_ApplicationsDisp(sr.A_ApplicationID).A_AppName;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            FrameWorkPermission.CheckPermissionVoid(PopedomType.New);
            sys_RoleApplicationTable rt = new sys_RoleApplicationTable();
            rt.DB_Option_Action_ = "Insert";
            rt.A_ApplicationID = (int)Common.sink(NewAppID.UniqueID, MethodType.Post, 255, 1, DataType.Int);
            rt.A_RoleID = RoleID;
            BusinessFacade.sys_RoleApplicationInsertUpdate(rt);
            EventMessage.MessageBox(1, "操作成功", string.Format("增加角色应用ID({0})成功!", rt.A_ApplicationID), Icon_Type.OK, Common.GetHomeBaseUrl(string.Format("RoleManager.aspx?RoleID={0}&CMD=Look",RoleID)));
        }

        [PopedomTypeAttaible(PopedomType.New)]
        [System.Security.Permissions.PrincipalPermission(System.Security.Permissions.SecurityAction.Demand, Role = "OR")]
        protected void Button3_Click(object sender, EventArgs e)
        {
            int UserID = (int)Common.sink(DropDownList1.UniqueID, MethodType.Post, 255, 1, DataType.Int);
            UserData.Move_UserPermissionCache(UserID);
            sys_UserRolesTable su = new sys_UserRolesTable();
            su.DB_Option_Action_ = "Insert";
            su.R_RoleID = RoleID;
            su.R_UserID = UserID;
            BusinessFacade.sys_UserRolesInsertUpdate(su);
            EventMessage.MessageBox(1, "操作成功", string.Format("从角色ID:{0}中增加用户ID:{1}成功！", RoleID, UserID), Icon_Type.OK, Common.GetHomeBaseUrl(string.Format("RoleManager.aspx?RoleID={0}&CMD=Look", RoleID)));
        }

    }
}
