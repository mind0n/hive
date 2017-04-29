using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Site.Admin.AdminDAL;
using DAL.Entities;

namespace Site.Admin
{
    public partial class PcMappingManage : System.Web.UI.Page
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
                ViewState["SortOrder"] = "pid";
                ViewState["OrderDire"] = "ASC";
                BindGridViewData();
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
                dropdownlistCategories.DataValueField = "categoryid";
                dropdownlistCategories.DataTextField = "caption";
                dropdownlistCategories.Items.Clear();
                dropdownlistCategories.DataSource = dt;
                dropdownlistCategories.DataBind();
            }
            catch (Exception ex)
            {
                errorPlace.InnerText = ex.Message;
            }
        }

        private void SetGridViewStyle()
        {
        }

        protected void PcMappingGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            Button btnHiddenPostButton = e.Row.FindControl("btnHiddenPostButton") as Button;
            if (btnHiddenPostButton != null)
            {
                e.Row.Attributes["onclick"] = String.Format("javascript:document.getElementById('{0}').click()", btnHiddenPostButton.ClientID);
                e.Row.Attributes["style"] = "cursor:default";
                e.Row.Attributes["title"] = "单击选择当前行";
            }
        }

        private void BindGridViewData()
        {
            try
            {
                DataTable dt = DalHandler.GetAllPcMapping();
                dt.Columns["tPcMappings.categoryid"].ColumnName = "categoryid";
                string sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
                dt.DefaultView.Sort = sort;
                PcMappingGridView.DataSource = dt;
                PcMappingGridView.DataBind();
            }
            catch (Exception ex)
            {
                errorPlace.InnerText = ex.Message;
            }
        }

        protected void PcMappingGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "HiddenPostButtonCommand":
                    Control cmdControl = e.CommandSource as Control;
                    GridViewRow row = cmdControl.NamingContainer as GridViewRow;
                    string lblPcmID = (row.FindControl("lblPcmID") as Label).Text;
                    if (!string.IsNullOrEmpty(lblPcmID))
                    {
                        labelID.Text = lblPcmID;
                        dropdownlistCategories.SelectedValue = (row.FindControl("hiddentCatagoryId") as HiddenField).Value;
                        textBoxMorder.Text = (row.FindControl("lblmorder") as Label).Text;
                        textBoxPid.Text = (row.FindControl("lblpid") as Label).Text;
                        textBoxContainer.Text = (row.FindControl("lblcontainer") as Label).Text;
                        textBoxWidgetname.Text = (row.FindControl("lblwidgetname") as Label).Text;
                        textBoxWidgetsettings.Text = (row.FindControl("lblwidgetsettings") as Label).Text;
                        checkBoxIsVisible.Checked = (row.FindControl("chkVisible") as CheckBox).Checked;
                        pageStatus = PageStatus.Modify;

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
            textBoxMorder.Text = "0";
            textBoxPid.Text = "";
            textBoxContainer.Text = "";
            textBoxWidgetname.Text = "Grid";
            textBoxWidgetsettings.Text = "{}";
            dropdownlistCategories.SelectedIndex = 0;
            checkBoxIsVisible.Checked = true;
            pageStatus = PageStatus.Add;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            PcMappings pcMappings = new PcMappings();
            pcMappings.Categoryid = Convert.ToInt32(dropdownlistCategories.SelectedValue);
            pcMappings.Pid = textBoxPid.Text.Trim();
            pcMappings.Container = textBoxContainer.Text.Trim();
            pcMappings.Morder =string.IsNullOrEmpty(textBoxMorder.Text) ? 0 : Convert.ToInt32(textBoxMorder.Text.Trim());
            pcMappings.Widgetname = textBoxWidgetname.Text.Trim();
            pcMappings.Widgetsettings = textBoxWidgetsettings.Text.Trim();
            pcMappings.Isvisible = checkBoxIsVisible.Checked;
            pcMappings.Pcupdate = DateTime.Now;
            if (pageStatus == PageStatus.Add)
            {
                try
                {
                    int rlt = DalHandler.InsertOrUpdatePcMappings(pcMappings, 0);
                }
                catch (Exception ex)
                {
                    errorPlace.InnerText = ex.Message;
                }

            }
            else if (pageStatus == PageStatus.Modify)
            {
                pcMappings.Pcmid = Convert.ToInt32(labelID.Text);
                try
                {
                    pcMappings.Pcmid = Convert.ToInt32(labelID.Text);
                    int rlt = DalHandler.InsertOrUpdatePcMappings(pcMappings, 1);
                }
                catch (Exception ex)
                {
                    errorPlace.InnerText = ex.Message;
                }
            }
            BindGridViewData();
            BindDropDownList();
            ReSetControlsStatus();
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            if (!DelCheck(PcMappingGridView))
            {
                return;
            }
            foreach (GridViewRow row in PcMappingGridView.Rows)
            {
                if ((row.FindControl("checkBoxSelect") as CheckBox).Checked)
                {
                    int pcmId = Convert.ToInt32((row.FindControl("lblPcmID") as Label).Text);
                    try
                    {
                        int rlt = DalHandler.DeletePcMappingsById(pcmId);
                        if (rlt == 1)
                        {
                            BindGridViewData();
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

        protected void PcMappingGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PcMappingGridView.PageIndex = e.NewPageIndex;
            BindGridViewData();
        }

        protected void PcMappingGridView_Sorting(object sender, GridViewSortEventArgs e)
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
            BindGridViewData();
        }

        protected override void OnPreRender(EventArgs e)
        {
            string CheckAllSelectedChange = @"<script type='text/javascript'>
    function doCheckAllSelectedChange(checkBox) {
        var checkAll = checkBox;
        if (checkAll != null) {
var gridView=" + PcMappingGridView.ClientID + @";
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