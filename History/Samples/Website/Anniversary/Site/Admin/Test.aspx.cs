using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;


namespace Site.Admin
{
    public partial class Test : System.Web.UI.Page
    {
        //#region Variables

        //string gvUniqueID = String.Empty;

        //int gvNewPageIndex = 0;

        //int gvEditIndex = -1;

        //string gvSortExpr = String.Empty;

        //private string gvSortDir
        //{
        //    get { return ViewState["SortDirection"] as string ?? "ASC"; }
        //    set { ViewState["SortDirection"] = value; }
        //}

        //#endregion

        ////This procedure returns the Sort Direction
        //private string GetSortDirection()
        //{
        //    switch (gvSortDir)
        //    {
        //        case "ASC":
        //            gvSortDir = "DESC";
        //            break;
        //        case "DESC":
        //            gvSortDir = "ASC";
        //            break;
        //    }
        //    return gvSortDir;
        //}
        ////This procedure prepares the query to bind the child GridView
        //private AccessDataSource ChildDataSource(string strCustometId, string strSort)
        //{
        //    string strQRY = "";
        //    AccessDataSource dsTemp = new AccessDataSource();
        //    dsTemp.DataFile = "~/Northwind.mdb";

        //    strQRY = "SELECT [Orders].[CustomerID],[Orders].[OrderID]," +

        //                            "[Orders].[ShipAddress],[Orders].[Freight],[Orders].[ShipName] FROM [Orders]" +

        //                            " WHERE [Orders].[CustomerID] = '" + strCustometId + "'" +

        //                            "UNION ALL " +

        //                            "SELECT '" + strCustometId + "','','','','' FROM [Orders] WHERE [Orders].[CustomerID] = '" + strCustometId + "'" +

        //                            "HAVING COUNT(*)=0 " + strSort;



        //    dsTemp.SelectCommand = strQRY;

        //    return dsTemp;

        //}
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //}

        //#region GridView1 Event Handlers
        ////This event occurs for each row
        //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    GridViewRow row = e.Row;
        //    string strSort = string.Empty;
        //    // Make sure we aren't in header/footer rows
        //   if (row.DataItem == null)
        //    {
        //        return;
        //   }
        //    //Find Child GridView control
        //    GridView gv = new GridView();
        //    gv = (GridView)row.FindControl("GridView2");
        //    //Check if any additional conditions (Paging, Sorting, Editing, etc) to be applied on child GridView
        //    if (gv.UniqueID == gvUniqueID)
        //    {
        //        gv.PageIndex = gvNewPageIndex;
        //        gv.EditIndex = gvEditIndex;
        //        //Check if Sorting used
        //        if (gvSortExpr != string.Empty)
        //        {
        //            GetSortDirection();

        //            strSort = " ORDER BY " + string.Format("{0} {1}", gvSortExpr, gvSortDir);

        //        }

        //        //Expand the Child grid

        //        ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + ((DataRowView)e.Row.DataItem)["CustomerID"].ToString() + "','one');</script>");

        //    }
        //    //Prepare the query for Child GridView by passing the Customer ID of the parent row

        //    gv.DataSource = ChildDataSource(((DataRowView)e.Row.DataItem)["CustomerID"].ToString(), strSort);

        //    gv.DataBind();
        //    //Add delete confirmation message for Customer

        //    LinkButton l = (LinkButton)e.Row.FindControl("linkDeleteCust");

        //    l.Attributes.Add("onclick", "javascript:return " +

        //    "confirm('Are you sure you want to delete this Customer " +

        //    DataBinder.Eval(e.Row.DataItem, "CustomerID") + "')");
        //}
        ////This event occurs for any operation on the row of the grid

        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{

        //    //Check if Add button clicked

        //    if (e.CommandName == "AddCustomer")
        //    {

        //        try
        //        {

        //            //Get the values stored in the text boxes

        //            string strCompanyName = ((TextBox)GridView1.FooterRow.FindControl("txtCompanyName")).Text;

        //            string strContactName = ((TextBox)GridView1.FooterRow.FindControl("txtContactName")).Text;

        //            string strContactTitle = ((TextBox)GridView1.FooterRow.FindControl("txtContactTitle")).Text;

        //            string strAddress = ((TextBox)GridView1.FooterRow.FindControl("txtAddress")).Text;

        //            string strCustomerID = ((TextBox)GridView1.FooterRow.FindControl("txtCustomerID")).Text;
        //            //Prepare the Insert Command of the DataSource control
        //            string strSQL = "";

        //            strSQL = "INSERT INTO Customers (CustomerID, CompanyName, ContactName, " +

        //                    "ContactTitle, Address) VALUES ('" + strCustomerID + "','" + strCompanyName + "','" +

        //                    strContactName + "','" + strContactTitle + "','" + strAddress + "')";

        //            AccessDataSource1.InsertCommand = strSQL;

        //            AccessDataSource1.Insert();

        //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Customer added successfully');</script>");
        //            //Re bind the grid to refresh the data

        //            GridView1.DataBind();

        //        }

        //        catch (Exception ex)
        //        {

        //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message.ToString().Replace("'", "") + "');</script>");

        //        }

        //    }

        //}
        ////This event occurs on click of the Update button

        //protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{

        //    //Get the values stored in the text boxes

        //    string strCompanyName = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtCompanyName")).Text;

        //    string strContactName = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtContactName")).Text;

        //    string strContactTitle = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtContactTitle")).Text;

        //    string strAddress = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtAddress")).Text;

        //    string strCustomerID = ((Label)GridView1.Rows[e.RowIndex].FindControl("lblCustomerID")).Text;
        //    try
        //    {

        //        //Prepare the Update Command of the DataSource control

        //        string strSQL = "";

        //        strSQL = "UPDATE Customers set CompanyName = '" + strCompanyName + "'" +

        //                 ",ContactName = '" + strContactName + "'" +

        //                 ",ContactTitle = '" + strContactTitle + "'" +

        //                 ",Address = '" + strAddress + "'" +

        //                 " WHERE CustomerID = '" + strCustomerID + "'";

        //        AccessDataSource1.UpdateCommand = strSQL;

        //        AccessDataSource1.Update();

        //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Customer updated successfully');</script>");

        //    }

        //    catch { }

        //}
        ////This event occurs after RowUpdating to catch any constraints while updating

        //protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        //{

        //    //Check if there is any exception while deleting

        //    if (e.Exception != null)
        //    {

        //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");

        //        e.ExceptionHandled = true;

        //    }

        //}
        ////This event occurs on click of the Delete button

        //protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{

        //    //Get the value        

        //    string strCustomerID = ((Label)GridView1.Rows[e.RowIndex].FindControl("lblCustomerID")).Text;

        //    //Prepare the delete Command of the DataSource control

        //    string strSQL = "";

        //    try
        //    {

        //        strSQL = "DELETE from Customers WHERE CustomerID = '" + strCustomerID + "'";

        //        AccessDataSource1.DeleteCommand = strSQL;

        //        AccessDataSource1.Delete();

        //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Customer deleted successfully');</script>");

        //    }

        //    catch { }

        //}

        ////This event occurs after RowDeleting to catch any constraints while deleting

        //protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        //{

        //    //Check if there is any exception while deleting

        //    if (e.Exception != null)
        //    {

        //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");

        //        e.ExceptionHandled = true;

        //    }

        //}

        //#endregion

        //#region GridView2 Event Handlers

        //protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{

        //    GridView gvTemp = (GridView)sender;

        //    gvUniqueID = gvTemp.UniqueID;

        //    gvNewPageIndex = e.NewPageIndex;

        //    GridView1.DataBind();

        //}

        //protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        //{

        //    if (e.CommandName == "AddOrder")
        //    {

        //        try
        //        {

        //            GridView gvTemp = (GridView)sender;

        //            gvUniqueID = gvTemp.UniqueID;

        //            //Get the values stored in the text boxes

        //            string strCustomerID = gvTemp.DataKeys[0].Value.ToString(); //Customer ID is stored as DataKeyNames

        //            string strFreight = ((TextBox)gvTemp.FooterRow.FindControl("txtFreight")).Text;

        //            string strShipperName = ((TextBox)gvTemp.FooterRow.FindControl("txtShipperName")).Text;

        //            string strShipAdress = ((TextBox)gvTemp.FooterRow.FindControl("txtShipAdress")).Text;



        //            //Prepare the Insert Command of the DataSource control

        //            string strSQL = "";

        //            strSQL = "INSERT INTO Orders (CustomerID, Freight, ShipName, " +

        //                    "ShipAddress) VALUES ('" + strCustomerID + "'," + float.Parse(strFreight) + ",'" +

        //                    strShipperName + "','" + strShipAdress + "')";



        //            AccessDataSource1.InsertCommand = strSQL;

        //            AccessDataSource1.Insert();

        //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Order added successfully');</script>");



        //            GridView1.DataBind();

        //        }

        //        catch (Exception ex)
        //        {

        //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message.ToString().Replace("'", "") + "');</script>");

        //        }

        //    }

        //}

        //protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        //{

        //    GridView gvTemp = (GridView)sender;

        //    gvUniqueID = gvTemp.UniqueID;

        //    gvEditIndex = e.NewEditIndex;

        //    GridView1.DataBind();

        //}

        //protected void GridView2_CancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{

        //    GridView gvTemp = (GridView)sender;

        //    gvUniqueID = gvTemp.UniqueID;

        //    gvEditIndex = -1;

        //    GridView1.DataBind();

        //}

        //protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{

        //    try
        //    {

        //        GridView gvTemp = (GridView)sender;

        //        gvUniqueID = gvTemp.UniqueID;

        //        //Get the values stored in the text boxes

        //        string strOrderID = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblOrderID")).Text;

        //        string strFreight = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtFreight")).Text;

        //        string strShipperName = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtShipperName")).Text;

        //        string strShipAdress = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtShipAdress")).Text;

        //        //Prepare the Update Command of the DataSource control

        //        AccessDataSource dsTemp = new AccessDataSource();

        //        dsTemp.DataFile = "~/Northwind.mdb";

        //        string strSQL = "";

        //        strSQL = "UPDATE Orders set Freight = " + float.Parse(strFreight) + "" +

        //                 ",ShipName = '" + strShipperName + "'" +

        //                 ",ShipAddress = '" + strShipAdress + "'" +

        //                 " WHERE OrderID = " + strOrderID;

        //        dsTemp.UpdateCommand = strSQL;

        //        dsTemp.Update();

        //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Order updated successfully');</script>");



        //        //Reset Edit Index

        //        gvEditIndex = -1;

        //        GridView1.DataBind();

        //    }

        //    catch { }

        //}

        //protected void GridView2_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        //{

        //    //Check if there is any exception while deleting

        //    if (e.Exception != null)
        //    {

        //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");

        //        e.ExceptionHandled = true;

        //    }

        //}

        //protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{

        //    GridView gvTemp = (GridView)sender;

        //    gvUniqueID = gvTemp.UniqueID;

        //    //Get the value        

        //    string strOrderID = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblOrderID")).Text;



        //    //Prepare the Update Command of the DataSource control

        //    string strSQL = "";



        //    try
        //    {

        //        strSQL = "DELETE from Orders WHERE OrderID = " + strOrderID;

        //        AccessDataSource dsTemp = new AccessDataSource();

        //        dsTemp.DataFile = "~/Northwind.mdb";

        //        dsTemp.DeleteCommand = strSQL;

        //        dsTemp.Delete();

        //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Order deleted successfully');</script>");

        //        GridView1.DataBind();

        //    }

        //    catch { }

        //}



        //protected void GridView2_RowDeleted(object sender, GridViewDeletedEventArgs e)
        //{

        //    //Check if there is any exception while deleting

        //    if (e.Exception != null)
        //    {

        //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");

        //        e.ExceptionHandled = true;

        //    }

        //}



        //protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        //{

        //    //Check if this is our Blank Row being databound, if so make the row invisible

        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {

        //        if (((DataRowView)e.Row.DataItem)["OrderID"].ToString() == String.Empty) e.Row.Visible = false;

        //    }

        //}



        //protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
        //{

        //    GridView gvTemp = (GridView)sender;

        //    gvUniqueID = gvTemp.UniqueID;

        //    gvSortExpr = e.SortExpression;

        //    GridView1.DataBind();

        //}

        //#endregion

    }
}