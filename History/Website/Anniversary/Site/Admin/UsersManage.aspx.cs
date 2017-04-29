using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAL.Entities;
using Site.Admin.AdminDAL;
using System.Web.Caching;

namespace Site.Admin
{
    public partial class UsersManage : System.Web.UI.Page
    {
        private Cache usersCache = new Cache();
        private Cache ddlSelectedValueCache = new Cache();

        public string DateTimeStr
        {
            get { return DateTime.Now.ToString(); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Write("<script>alert('尚未登陆或超时,请重新登录!');window.location='Login.aspx'</script>");
            }
            if (!IsPostBack)
            {
                BindGridView(false);
            }
        }

        private void BindDropDownListUserType(DropDownList ddl,int selectedInt)
        {
            ddl.Items.Clear();
            ddl.Items.AddRange(new ListItem[] {new ListItem("管理员", "0"), new ListItem("老师", "1"), new ListItem("学生", "2"), new ListItem("游客", "3") });
            ddl.SelectedIndex = selectedInt;
        }


        private DataTable SelectDistinct(DataTable sourceTable, string distinctFiledName)
        {
            DataTable newDt = new DataTable();
            newDt.Columns.Add(distinctFiledName);

            object previousValue = null;
            foreach (DataRow dr in sourceTable.Select("", distinctFiledName))
            {
                if (previousValue == null || !ColumnCompare(previousValue, dr[distinctFiledName]))
                {
                    previousValue = dr[distinctFiledName];
                    newDt.Rows.Add(previousValue);
                }
            }
            return newDt;
        }

        private bool ColumnCompare(object previousValue, object p)
        {
            return (previousValue.Equals(p));
        }

        private void BindGridView(bool isNeedReLoad)
        {
            try
            {
                DataTable dt = null;
                if (Cache["UsersCache"] == null || isNeedReLoad)
                {
                    dt = DalHandler.GetAllUsers();
                    Cache["UsersCache"] = dt;
                }
                else
                {
                    dt = Cache["UsersCache"] as DataTable;
                }
                GridViewUsers.DataSource = dt;
                GridViewUsers.DataBind();
                if (GridViewUsers.FooterRow != null && GridViewUsers.FooterRow.FindControl("ddlUlevelFooter") != null)
                {
                    DropDownList ddl = GridViewUsers.FooterRow.FindControl("ddlUlevelFooter") as DropDownList;
                    BindDropDownListUserType(ddl,-1);
                }

                //add logic to filter usertype is 2 or 3
                #region
                if (Session["UserType"] != null && ((int)Session["UserType"] == 2 || (int)Session["UserType"] == 3))
                {
                    GridViewUsers.Columns[7].Visible = false;
                    GridViewUsers.FooterRow.Visible = false;
                    string userName = string.Empty;
                    if (Session["UserName"] != null)
                    {
                        foreach (GridViewRow row in GridViewUsers.Rows)
                        {
                            userName = (row.FindControl("HiddenFieldUname") as HiddenField).Value.Trim();

                            if (userName.Equals(Session["UserName"].ToString().Trim(), StringComparison.OrdinalIgnoreCase))
                            {
                                row.Visible = true;
                            }
                            else
                            {
                                row.Visible = false;
                            }
                            row.Cells[4].Enabled = false;
                        }
                    }
                    else
                    {
                        // to do sth
                    }
                    btnDel.Enabled = false;
                }
                #endregion
            }
            catch (Exception e)
            {
                errorPlace.InnerText = e.Message;
            }
        }

        protected void GridViewUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewUsers.EditIndex = e.NewEditIndex;
            int selectedIndex = -1 ;
            if (GridViewUsers.Rows[e.NewEditIndex].FindControl("LabelUlevel") != null)
            {
                selectedIndex = Convert.ToInt32((GridViewUsers.Rows[e.NewEditIndex].FindControl("LabelUlevel") as Label).Text) ;
            }
            BindGridView(false);
            if (GridViewUsers.Rows[e.NewEditIndex].FindControl("ddlUlevel") != null)
            {
                DropDownList ddl = GridViewUsers.Rows[e.NewEditIndex].FindControl("ddlUlevel") as DropDownList;
                BindDropDownListUserType(ddl, selectedIndex);
            }
        }

        protected void GridViewUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                GridViewRow row = GridViewUsers.Rows[rowIndex];
                HiddenField hiddfield = row.FindControl("userIdHiddenField") as HiddenField;
                if (hiddfield != null && !string.IsNullOrEmpty(hiddfield.Value))
                {
                    DAL.Entities.UserItem user = new UserItem();
                    user.UserId = Convert.ToInt32(hiddfield.Value);
                    user.UName = (row.Cells[1].Controls[1] as TextBox).Text;
                    user.UPwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((row.Cells[2].Controls[1] as TextBox).Text, "MD5");
                    user.ULevel = Convert.ToInt32((row.Cells[4].Controls[1] as DropDownList).SelectedValue);
                    user.UText = (row.Cells[3].Controls[1] as TextBox).Text;
                    user.UserUpdate = DateTime.Now;
                    int rlt = DalHandler.UpdateUserById(user);
                    if (rlt == 1)
                    {
                        RefreshCacheUsers(rowIndex, user);
                        GridViewUsers.EditIndex = -1;
                        BindGridView(true);
                    }
                }
                else
                {
                    BindGridView(true);
                }
            }
            catch (Exception ex)
            {
                errorPlace.InnerText = ex.Message;
            }
        }

        private void RefreshCacheUsers(int rowIndex, DAL.Entities.UserItem user)
        {
            DataTable dt = Cache["UsersCache"] as DataTable;
            if (dt != null)
            {
                //0 means after adding a user into db , others mean update operation 
                if (rowIndex == 0)
                {
                    DataRow row = dt.NewRow();
                    row["uname"] = user.UName;
                    row["upwd"] = user.UPwd;
                    row["userupdate"] = user.UserUpdate;
                    row["utext"] = user.UText;
                    row["ulevel"] = user.ULevel;
                    dt.Rows.Add(row);
                }
                else
                {
                    dt.Rows[rowIndex]["uname"] = user.UName;
                    dt.Rows[rowIndex]["upwd"] = user.UPwd;
                    dt.Rows[rowIndex]["userupdate"] = user.UserUpdate;
                    dt.Rows[rowIndex]["utext"] = user.UText;
                    dt.Rows[rowIndex]["ulevel"] = user.ULevel;
                }
            }
            else
            {
                dt = DalHandler.GetAllUsers();
                Cache["UsersCache"] = dt;
            }
        }

        protected void GridViewUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewUsers.EditIndex = -1;
            BindGridView(false);
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            if (!DelCheck(GridViewUsers))
            {
                return;
            }
            foreach (GridViewRow row in GridViewUsers.Rows)
            {
                if ((row.FindControl("selectCheckBox") as CheckBox).Checked)
                {
                    int userId = Convert.ToInt32((row.FindControl("userIdHiddenField") as HiddenField).Value);
                    try
                    {
                        int rlt = DalHandler.DeleteUserById(userId);
                        if (rlt == 1)
                        {
                            DataTable dt = Cache["UsersCache"] as DataTable;
                            dt.Rows[row.RowIndex].Delete();
                            BindGridView(false);
                        }
                    }
                    catch (Exception ex)
                    {
                        errorPlace.InnerText = ex.Message;
                    }
                }
            }

        }

        private bool DelCheck(GridView gridView)
        {
            if (gridView == null)
            {
                return false;
            }
            int checkNum = 0;
            foreach (GridViewRow row in gridView.Rows)
            {
                if ((row.FindControl("selectCheckBox") as CheckBox).Checked)
                {
                    checkNum++;
                }
            }
            if (checkNum == 0)
            {
                return false;
            }
            return true;
        }

        protected void GridViewUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
        }

        protected void GridViewUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddUser")
            {
                if (GridViewUsers.FooterRow != null)
                {
                    DAL.Entities.UserItem user = new UserItem();
                    user.UName = (GridViewUsers.FooterRow.FindControl("TextBoxUserNameFooter") as TextBox).Text.Trim();
                    user.UPwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((GridViewUsers.FooterRow.FindControl("TextBoxPwdFooter") as TextBox).Text.Trim(), "MD5");
                    user.ULevel = Convert.ToInt32((GridViewUsers.FooterRow.FindControl("ddlUlevelFooter") as DropDownList).SelectedValue);
                    user.UText = (GridViewUsers.FooterRow.FindControl("textBoxUtextFooter") as TextBox).Text;
                    user.UserUpdate = DateTime.Now;
                    try
                    {
                        int rlt = DalHandler.AddUser(user);
                        if (rlt == 1)
                        {
                            RefreshCacheUsers(0, user);
                            BindGridView(true);
                        }
                        else
                        {
                            //...
                        }
                    }
                    catch (Exception ex)
                    {
                        errorPlace.InnerHtml = ex.Message;
                    }
                }
            }
            else if (e.CommandName == "DeleteUser")
            {
                if (e.CommandArgument != null)
                {
                    int userId = Convert.ToInt32(e.CommandArgument);
                    try
                    {
                        int rlt = DalHandler.DeleteUserById(userId);
                        if (rlt == 1)
                        {
                            DataTable dt = Cache["UsersCache"] as DataTable;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row["userid"].ToString().Equals(userId.ToString(), StringComparison.OrdinalIgnoreCase))
                                {
                                    row.Delete();
                                    break;
                                }
                            }
                            BindGridView(false);
                        }
                    }
                    catch (Exception ex)
                    {
                        errorPlace.InnerText = ex.Message;
                    }
                }
            }

        }

        protected override void OnPreRender(EventArgs e)
        {
            string CheckAllSelectedChange = @"<script type='text/javascript'>
    function doCheckAllSelectedChange(checkBox) {
        var checkAll = checkBox;
        if (checkAll != null) {
var gridView=" + GridViewUsers.ClientID + @";
            if (gridView) {
                var inputs = gridView.getElementsByTagName('input');
                for (var i = 0; i < inputs.length; i++) {
                    if (inputs[i].type == 'checkbox' && inputs[i].name.indexOf('selectCheckBox')>0) {
                        inputs[i].checked = checkBox.checked;
                    }
                }
            }
        }
    }
</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "CheckAllSelectedChange", CheckAllSelectedChange);
            base.OnPreRender(e);
        }
    }
}