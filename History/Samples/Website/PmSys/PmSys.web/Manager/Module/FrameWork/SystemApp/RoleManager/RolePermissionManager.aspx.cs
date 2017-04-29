using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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
    public partial class RolePermissionManager : System.Web.UI.Page
    {
        int RoleID = (int)Common.sink("RoleID", MethodType.Get, 255, 1, DataType.Int);
        int ApplicationID = (int)Common.sink("ApplicationID", MethodType.Get, 255, 1, DataType.Int);
        string CMD = (string)Common.sink("CMD", MethodType.Get, 255, 1, DataType.Str);
        protected void Page_Load(object sender, EventArgs e)
        {
            FrameWorkPermission.CheckPagePermission(CMD);
            BusinessFacade.sys_UserCheckManagerVoid();
            BusinessFacade.sys_Roles_CheckUserVoid(RoleID);
            BindButton();
            if (!Page.IsPostBack)
            {
                OnStart();
                OnDisp();
            }
        }


        /// <summary>
        /// 获得当前角色所属用户ID
        /// </summary>
        private int GetRoleUserID
        {
            get {
                return BusinessFacade.sys_RolesDisp(RoleID).R_UserID;
            }
        }

        private void OnStart()
        {
            sys_RolesTable srt = BusinessFacade.sys_RolesDisp(RoleID);
            RoleName_Txt.Text = srt.R_RoleName;
            AppName_Txt.Text = BusinessFacade.sys_ApplicationsDisp(ApplicationID).A_AppName;
            RoleUser_Txt.Text = BusinessFacade.sys_UserDisp(srt.R_UserID).U_LoginName;
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void OnDisp()
        {
            QueryParam qp = new QueryParam();
            qp.Orderfld = " M_Applicationid,M_OrderLevel ";
            qp.OrderType = 0;
            qp.Where = string.Format("Where M_Close=0 and M_ParentID=0 and M_ApplicationID ={0}", ApplicationID);
            int RecordCount = 0;
            ArrayList lst = BusinessFacade.sys_ModuleList(qp, out RecordCount);
            Module_Main.DataSource = lst;
            Module_Main.DataBind();
        }

        private void BindButton()
        {
            HeadMenuButtonItem item0 = new HeadMenuButtonItem();
            item0.ButtonName = "角色资料";
            item0.ButtonIcon = "look.gif";
            item0.ButtonPopedom = PopedomType.List;
            item0.ButtonUrl = string.Format("RoleManager.aspx?RoleID={0}&CMD=Look",RoleID);
            HeadMenuWebControls1.ButtonList.Add(item0);
            if (CMD == "Look")
            {
                HeadMenuButtonItem item1 = new HeadMenuButtonItem();
                item1.ButtonName = "角色权限";
                item1.ButtonPopedom = PopedomType.Edit;
                item1.ButtonUrl = string.Format("?RoleID={0}&ApplicationID={1}&CMD=Edit",RoleID,ApplicationID);
                HeadMenuWebControls1.ButtonList.Add(item1);
            }
            if (CMD == "Edit")
            {
                ButtonTr_End.Visible = true;
                HeadMenuButtonItem item2 = new HeadMenuButtonItem();
                item2.ButtonName = "返回";
                item2.ButtonIcon = "back.gif";
                item2.ButtonPopedom = PopedomType.List;
                item2.ButtonUrl = "history.back(-1);";
                item2.ButtonUrlType = UrlType.JavaScript;
                HeadMenuWebControls1.ButtonList.Add(item2);
            }
        }

        #region "绑定数据事件"
        protected void Module_Main_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            sys_ModuleTable s_Mt = (sys_ModuleTable)e.Item.DataItem;
            QueryParam qp = new QueryParam();
            qp.Orderfld = " M_OrderLevel ";
            qp.OrderType = 0;
            qp.Where = string.Format("Where M_Close=0 and M_ParentID ={0}", s_Mt.ModuleID);
            int RecordCount = 0;
            ArrayList lst = BusinessFacade.sys_ModuleList(qp, out RecordCount);
            if (lst.Count > 0)
            {
                Repeater Module_Sub = (Repeater)e.Item.FindControl("Module_Sub");
                Module_Sub.DataSource = lst;
                Module_Sub.DataBind();
            }
        }

        /// <summary>
        /// 获得模块的权限集合
        /// </summary>
        /// <param name="moduleID">模块ID</param>
        /// <returns>返回模块权限列表</returns>
        public List<sys_PermissionItem> GetPermissionList(int moduleID)
        {
            List<sys_PermissionItem> lst = new List<sys_PermissionItem>();
            lst.Add(new sys_PermissionItem("查看", 2, false));
            lst.Add(new sys_PermissionItem("新增", 4, false));
            lst.Add(new sys_PermissionItem("修改", 8, false));
            lst.Add(new sys_PermissionItem("删除", 16, false));
            lst.Add(new sys_PermissionItem("排序", 32, false));
            lst.Add(new sys_PermissionItem("打印", 64, false));
            lst.Add(new sys_PermissionItem("备用A", 128, false));
            lst.Add(new sys_PermissionItem("备用B", 256, false));
            int recordcount=0;
            QueryParam qp = new QueryParam();
            qp.OrderType = 0;
            qp.Where = string.Format(" Where ModuleID={0}",moduleID);
            ArrayList ExtPermissionlst = BusinessFacade.sys_ModuleExtPermissionList(qp, out recordcount);
            if (recordcount > 0)
            {
                foreach (sys_ModuleExtPermissionTable var in ExtPermissionlst)
                {
                    lst.Add(new sys_PermissionItem(var.PermissionName,var.PermissionValue,false));
                }
            }
            return lst;
        }


        /// <summary>
        /// 获得权限显示文本
        /// </summary>
        /// <param name="pagecode">模块Pagecode</param>
        /// <param name="permissionName">权限名称</param>
        /// <param name="permissionValue">权限值</param>
        /// <param name="allow">是否允许</param>
        /// <returns>显示文本</returns>
        public void FormatPermission(List<sys_PermissionItem> lst, string pagecode, int userpermissionValue)
        {


            string rightString = "";
            string wrongString = "";
            string dispString = "";
            string SelectTxt = "";
            string DisableCheckbox = "";

            foreach (sys_PermissionItem var in lst)
            {
                rightString = string.Format("<img src='{0}' align='absbottom'>{1}({2})", ResolveClientUrl("~/Manager/images/allow.gif"), var.PermissionName,var.PermissionValue);
                wrongString = string.Format("<img src='{0}'  align='absbottom'>{1}({2})", ResolveClientUrl("~/Manager/images/disable.gif"), var.PermissionName,var.PermissionValue);
                dispString = "";
                SelectTxt = "";
                DisableCheckbox = "";

                var.Allow = (userpermissionValue & var.PermissionValue) == var.PermissionValue;
                    
                    
                //判断当前角色所属用户是否享有当前权限
                if (!UserData.CheckPageCode(GetRoleUserID, ApplicationID, pagecode, var.PermissionValue))
                {
                    DisableCheckbox = "disabled";
                }
                else
                {
                    DisableCheckbox = "";
                }
                if (var.Allow)
                {
                    dispString = rightString;
                    SelectTxt = "checked";
                }
                else
                {
                    dispString = wrongString;
                    SelectTxt = "";
                }
                if (CMD == "Edit")
                {
                    dispString = string.Format("<input type=checkbox id='PageCode{0}' name='PageCode{0}' value='{1}'  {2} {3}>{4}", pagecode, var.PermissionValue, SelectTxt, DisableCheckbox, var.PermissionName);
                }
                var.DispTxt = dispString;
            }
        }

        protected void Module_Sub_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            sys_ModuleTable s_Mt = (sys_ModuleTable)e.Item.DataItem;
            //获得当前设定角色权限
            sys_RolePermissionTable s_Rp = BusinessFacade.sys_RolePermissionDisp(RoleID, ApplicationID, s_Mt.M_PageCode);

            DataList dlist = (DataList)e.Item.FindControl("PermissionList");
            if (dlist != null)
            {

                List<sys_PermissionItem> lst = GetPermissionList(s_Mt.ModuleID);
                FormatPermission(lst, s_Mt.M_PageCode, s_Rp.P_Value);
                dlist.DataSource = lst;
                dlist.DataBind();
            }

            #region "原有代码"
            /*
            string rightString = string.Format("<img src='{0}'>",ResolveClientUrl("~/Manager/images/right.gif"));
            string wrongString = string.Format("<img src='{0}'>",ResolveClientUrl("~/Manager/images/wrong.gif"));
            string dispString = "";
            string SelectTxt = "";
            string DisableCheckbox = "";
            int TempStep = 1;
            for (int i = 0; i < 8; i++)
            {
                TempStep = TempStep + TempStep;
                Literal li = (Literal)e.Item.FindControl(string.Format("Lab{0}_Txt",TempStep));
                if (li != null)
                {
                    //判断当前角色所属用户是否享有当前权限
                    if (!UserData.CheckPageCode(Common.Get_UserID, ApplicationID, s_Mt.M_PageCode, TempStep))
                    {
                        DisableCheckbox = "disabled";
                    }
                    else
                    {
                        DisableCheckbox = "";
                    }
                    if ((s_Rp.P_Value & TempStep) == TempStep)
                    {
                        dispString = rightString;
                        SelectTxt = "checked";
                    }
                    else
                    {
                        dispString = wrongString;
                        SelectTxt = "";
                    }
                    if (CMD == "Edit")
                    {
                        dispString = string.Format("<input type=checkbox id='PageCode{0}' name='PageCode{0}' value='{1}'  {2} {3}>", s_Mt.M_PageCode, TempStep, SelectTxt, DisableCheckbox);
                    }
                    li.Text = dispString;
                }
            }
            */
            #endregion

        }
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            BusinessFacade.sys_RolePermission_Move(RoleID, ApplicationID);
            string TempPageCodeString = "";
            string[] ArrayInt;
            int PageCodeValue = 0;
            sys_RolePermissionTable s_Rt = new sys_RolePermissionTable();
            s_Rt.DB_Option_Action_ = "Insert";
            s_Rt.P_ApplicationID = ApplicationID;
            s_Rt.P_RoleID = RoleID;
            foreach (string var in Request.Form)
            {
                if (var.Length > 8)
                {

                    TempPageCodeString = var.Substring(0, 8);
                    if (TempPageCodeString == "PageCode")
                    {
                        PageCodeValue = 0;
                        TempPageCodeString = var.Substring(8, var.Length - 8);
                        ArrayInt = Request.Form[var].Split(',');
                        for (int i = 0; i < ArrayInt.Length; i++)
                        {
                            //判断当前用户是否享有当前权限
                            if (UserData.CheckPageCode(GetRoleUserID, ApplicationID, TempPageCodeString, Convert.ToInt32(ArrayInt[i])))
                            {
                                PageCodeValue = PageCodeValue + Convert.ToInt32(ArrayInt[i]);
                            }
                        }
                        s_Rt.P_PageCode = TempPageCodeString;
                        s_Rt.P_Value = PageCodeValue;
                        BusinessFacade.sys_RolePermissionInsertUpdate(s_Rt);
                        
                    }

                }
            }
            UserData.Move_RoleUserPermissionCache(RoleID);

            UpdateRolesUserToRulesPermission(RoleID, ApplicationID);

            EventMessage.MessageBox(1, "操作成功", string.Format("修改角色({0})应用({1})权限成功！", RoleID, ApplicationID), Icon_Type.OK, Common.GetHomeBaseUrl(string.Format("RolePermissionManager.aspx?RoleID={0}&ApplicationID={1}&CMD=Look",RoleID,ApplicationID)));

        }

        /// <summary>
        /// 修改角色相关用户创建的角色权限
        /// </summary>
        /// <param name="roleid">修改角色权限</param>
        /// <param name="applicationid">应用ID</param>
        protected void UpdateRolesUserToRulesPermission(int roleid, int applicationid)
        {

            ArrayList lst = BusinessFacade.sys_UserRolesList(roleid);
            foreach (sys_UserRolesTable var in lst)
            {
                foreach (sys_RolesTable varroles in BusinessFacade.sys_RolesList(var.R_UserID))
                {
                    UpdateUserRolesPermission(var.R_UserID, varroles.RoleID, applicationid);
                }
            }

        }

        /// <summary>
        /// 更新用户创建角色权限
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="roleid">角色Id</param>
        /// <param name="applicationid">应用ID</param>
        protected void UpdateUserRolesPermission(int userid, int roleid, int applicationid)
        {
            ArrayList lst = BusinessFacade.sys_RolePermission_GetList(applicationid, roleid);
            int Temp_P_Value = 0;
            foreach (sys_RolePermissionTable var in lst)
            {
                Temp_P_Value = 0;
                for (int i = 2 ; i <= var.P_Value;)
                {
                    if ((var.P_Value & i) ==i)
                    {
                        if (UserData.CheckPageCode(userid, applicationid, var.P_PageCode, i))
                        {
                            Temp_P_Value = Temp_P_Value + i;
                        }
                    }
                    i = i * 2;
                }
                var.P_Value = Temp_P_Value;
                var.DB_Option_Action_ = "Update";
                BusinessFacade.sys_RolePermissionInsertUpdate(var);
                UserData.Move_RoleUserPermissionCache(roleid);
            }
        }
    }
}
