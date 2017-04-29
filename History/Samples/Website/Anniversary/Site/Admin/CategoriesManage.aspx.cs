using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Site.Admin.AdminDAL;
using DAL;
using System.Data.OleDb;

namespace Site.Admin
{
    enum PageStatus
    {
        Add,
        Modify,
        Del,
        None
    }
    public partial class CategoriesManage : System.Web.UI.Page
    {
        private static PageStatus pageStatus;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Write("<script>alert('尚未登陆或超时,请重新登录!');window.location='Login.aspx'</script>");
            }
            if (!IsPostBack)
            {
                ViewState["SortOrder"] = "caption";
                ViewState["OrderDire"] = "ASC";
                BindGridViewParentData();
                BindDropDownList();
                pageStatus = PageStatus.Add;
            }
            SetGridViewStyle();
        }

        private void BindDropDownList()
        {
            try
            {
                DataTable dt = DalHandler.GetAllCategories2();
                dropdownlistParents.DataValueField = "categoryid";
                dropdownlistParents.DataTextField = "caption";
                dropdownlistParents.Items.Clear();
                dropdownlistParents.SelectedIndex = -1;
                dropdownlistParents.DataSource = dt;
                dropdownlistParents.DataBind();
                ListItem item = new ListItem("- 根类别 -", "0");
                dropdownlistParents.Items.Insert(0, item);
            }
            catch (Exception ex)
            {
                errorPlace.InnerText = ex.Message;
            }
        }

        private void SetGridViewStyle()
        {
            CategriesParentGridView.Columns[1].ItemStyle.Width = 9;
        }

        protected void CategriesParentGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;

            e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#cccccc'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");

            Button btnHiddenPostButton = e.Row.FindControl("btnHiddenPostButton") as Button;
            if (btnHiddenPostButton != null)
            {
                e.Row.Attributes["onclick"] = String.Format("javascript:document.getElementById('{0}').click()", btnHiddenPostButton.ClientID);
                e.Row.Attributes["style"] = "cursor:default";
                e.Row.Attributes["title"] = "单击选择当前行";
            }
            // Make sure we aren't in header/footer rows
            //if (row.DataItem == null)
            //{
            //    return;
            //}
            //Find Child GridView control
            //GridView gv = new GridView();

            //gv = (GridView)row.FindControl("GridViewChild");

            //ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + ((DataRowView)e.Row.DataItem)["categoryid"].ToString() + "','none');</script>");
            ////Prepare the query for Child GridView by passing the Customer ID of the parent row
            //gv.Columns[0].ItemStyle.Width = 9;
            //gv.DataSource = BindGridVeiwChildData(((DataRowView)e.Row.DataItem)["categoryid"].ToString());
            //gv.DataBind();
        }

        protected void gvChild_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "HiddenPostButtonChildCommand":
                    Control cmdControl = e.CommandSource as Control;
                    GridViewRow row = cmdControl.NamingContainer as GridViewRow;
                    string categoryId = (row.FindControl("lblCategoryID") as Label).Text;
                    //DataTable dt = DalHandler.GetCategoryById(categoryId);
                    labelID.Text = categoryId;
                    textBoxCaption.Text = (row.FindControl("lblCaption") as Label).Text;
                    dropdownlistParents.SelectedValue = (row.FindControl("lblParentId") as Label).Text;
                    checkBoxIsVisible.Checked = (row.FindControl("chkVisible") as CheckBox).Checked;
                    pageStatus = PageStatus.Modify;
                    break;
            }
        }

        protected void gvChild_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            Button btnHiddenPostButton = e.Row.FindControl("btnHiddenPostChildButton") as Button;
            if (btnHiddenPostButton != null)
            {
                e.Row.Cells[0].Attributes["onclick"] = String.Format("javascript:document.getElementById('{0}').click()", btnHiddenPostButton.ClientID);
                e.Row.Attributes["style"] = "cursor:pointer";
                e.Row.Attributes["title"] = "单击选择当前行";
            }
        }

        private DataTable BindGridVeiwChildData(string parentId)
        {
            DataTable dt = null;
            try
            {
                dt = DalHandler.GetChildCategoryByParentId(parentId);
            }
            catch (Exception ex)
            {
                errorPlace.InnerText = ex.Message;
            }
            return dt;
        }

        private void BindGridViewParentData()
        {
            try
            {
                DataTable dt = DalHandler.GetAllCategories2();
                string sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
                dt.DefaultView.Sort = sort;
                CategriesParentGridView.DataSource = dt;
                CategriesParentGridView.DataBind();
            }
            catch (Exception ex)
            {
                errorPlace.InnerText = ex.Message;
            }
        }

        protected void CategriesParentGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "HiddenPostButtonCommand":
                    Control cmdControl = e.CommandSource as Control;
                    GridViewRow row = cmdControl.NamingContainer as GridViewRow;
                    string categoryId = (row.FindControl("lblCategoryID") as Label).Text;
                    //DataTable dt = DalHandler.GetCategoryById(categoryId);
                    if (!string.IsNullOrEmpty(categoryId))
                    {
                        labelID.Text = categoryId;
                        textBoxCaption.Text = (row.FindControl("lblCaption") as Label).Text;
                        OleDbDataReader dr = DalHandler.GetParentIdByCategroyId(categoryId);
                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                dropdownlistParents.SelectedValue = dr["parentid"].ToString();
                                break;
                            }
                            checkBoxIsVisible.Checked = (row.FindControl("chkVisible") as CheckBox).Checked;
                            pageStatus = PageStatus.Modify;
                        }
                        else
                        {
                            // throw sth
                        }
                    }
                    else
                    {
                        //throw sth
                    }

                    break;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ReSetControlsStatus();
            pageStatus = PageStatus.Add;
        }

        private void ReSetControlsStatus()
        {
            labelID.Text = "";
            textBoxCaption.Text = "";
            dropdownlistParents.SelectedIndex = 0;
            checkBoxIsVisible.Checked = false;
            pageStatus = PageStatus.Add;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.Caption = textBoxCaption.Text.Trim();
            category.ParentId = Convert.ToInt32(dropdownlistParents.SelectedValue);
            category.Visible = checkBoxIsVisible.Checked;
            category.CategoryUpdate = DateTime.Now;
            if (pageStatus == PageStatus.Add)
            {
                try
                {
                    int rlt = DalHandler.InsertOrUpdateCategory(category, 0);
                }
                catch (Exception ex)
                {
                    errorPlace.InnerText = ex.Message;
                }

            }
            else if (pageStatus == PageStatus.Modify)
            {
                try
                {
                    category.CategoryId = Convert.ToInt32(labelID.Text);
                    int rlt = DalHandler.InsertOrUpdateCategory(category, 1);
                }
                catch (Exception ex)
                {
                    errorPlace.InnerText = ex.Message;
                }
            }
            BindGridViewParentData();
            BindDropDownList();
            ReSetControlsStatus();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (!DelCheck(CategriesParentGridView))
            {
                return;
            }
            foreach (GridViewRow row in CategriesParentGridView.Rows)
            {
                if ((row.FindControl("checkBoxSelect") as CheckBox).Checked)
                {
                    int categoryId = Convert.ToInt32((row.FindControl("lblCategoryID") as Label).Text);
                    try
                    {
                        int rlt = DalHandler.DeleteCategoryById(categoryId);
                        if (rlt == 1)
                        {
                            BindGridViewParentData();
                            BindDropDownList();
                            ReSetControlsStatus();
                        }
                        else
                        {
                            //do sth                        
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
                if ((row.FindControl("checkBoxSelect") as CheckBox).Checked)
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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ReSetControlsStatus();
            pageStatus = PageStatus.Add;
        }

        protected void CategriesParentGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sPage = e.SortExpression;
            if (ViewState["SortOrder"].ToString() == sPage)
            {
                if (ViewState["OrderDire"].ToString() == "Desc")
                    ViewState["OrderDire"] = "ASC";
                else
                    ViewState["OrderDire"] = "Desc";
            }
            else
            {
                ViewState["SortOrder"] = e.SortExpression;
            }
            BindGridViewParentData();
        }

        protected override void OnPreRender(EventArgs e)
        {
            string CheckAllSelectedChange = @"<script type='text/javascript'>
    function doCheckAllSelectedChange(checkBox) {
        var checkAll = checkBox;
        if (checkAll != null) {
var gridView=" + CategriesParentGridView.ClientID + @";
            if (gridView) {
                var inputs = gridView.getElementsByTagName('input');
                for (var i = 0; i < inputs.length; i++) {
                    if (inputs[i].type == 'checkbox' && inputs[i].name.indexOf('checkBoxSelect')>0) {
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
