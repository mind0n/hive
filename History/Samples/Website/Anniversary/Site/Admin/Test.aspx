<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs"
    Inherits="Site.Admin.Test" %>
    <%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">

        function expandcollapse(obj, row) {
            var div = document.getElementById(obj);
            var img = document.getElementById('img' + obj);
            if (div.style.display == "none") {
                div.style.display = "block";
                if (row == 'alt') {
                    img.src = "minus.gif";
                }
                else {
                    img.src = "minus.gif";
                }
                img.alt = "Close to view other Customers";
            }
            else {
                div.style.display = "none";
                if (row == 'alt') {
                    img.src = "plus.gif";
                }
                else {
                    img.src = "plus.gif";
                }
                img.alt = "Expand to show Orders";
            }
        } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
<%--    <div>
        <asp:GridView ID="GridView1" AllowPaging="True" BackColor="#f1f1f1" AutoGenerateColumns="false"
            DataSourceID="AccessDataSource1" DataKeyNames="CustomerID" Style="z-index: 101;
            left: 8px; position: absolute; top: 32px" ShowFooter="true" Font-Size="Small"
            Font-Names="Verdana" runat="server" GridLines="None" OnRowDataBound="GridView1_RowDataBound"
            OnRowCommand="GridView1_RowCommand" OnRowUpdating="GridView1_RowUpdating" BorderStyle="Outset"
            OnRowDeleting="GridView1_RowDeleting" OnRowDeleted="GridView1_RowDeleted" OnRowUpdated="GridView1_RowUpdated"
            AllowSorting="true">
            <RowStyle BackColor="Gainsboro" />
            <AlternatingRowStyle BackColor="White" />
            <HeaderStyle BackColor="#0083C1" ForeColor="White" />
            <FooterStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <a href="javascript:expandcollapse('div<%# Eval("CustomerID") %>', 'one');">
                            <img id="imgdiv<%# Eval("CustomerID") %>" alt="Click to show/hide Orders for Customer <%# Eval("CustomerID") %>"
                                width="9px" border="0" src="plus.gif" />
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Customer ID" SortExpression="CustomerID">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerID" Text='<%# Eval("CustomerID") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblCustomerID" Text='<%# Eval("CustomerID") %>' runat="server"></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtCustomerID" Text='' runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Company Name" SortExpression="CompanyName">
                    <ItemTemplate>
                        <%# Eval("CompanyName") %></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCompanyName" Text='<%# Eval("CompanyName") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtCompanyName" Text='' runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact Name" SortExpression="ContactName">
                    <ItemTemplate>
                        <%# Eval("ContactName") %></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtContactName" Text='<%# Eval("ContactName") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtContactName" Text='' runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact Title" SortExpression="ContactTitle">
                    <ItemTemplate>
                        <%# Eval("ContactTitle")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtContactTitle" Text='<%# Eval("ContactTitle") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtContactTitle" Text='' runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Address" SortExpression="Address">
                    <ItemTemplate>
                        <%# Eval("Address")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtAddress" Text='<%# Eval("Address") %>' runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtAddress" Text='' runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDeleteCust" CommandName="Delete" runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="linkAddCust" CommandName="AddCustomer" runat="server">Add</asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <tr>
                            <td colspan="100%">
                                <div id="div<%# Eval("CustomerID") %>" style="display: none; position: relative;
                                    left: 15px; overflow: auto; width: 97%">
                                    <asp:GridView ID="GridView2" AllowPaging="True" AllowSorting="true" BackColor="White"
                                        Width="100%" Font-Size="X-Small" AutoGenerateColumns="false" Font-Names="Verdana"
                                        runat="server" DataKeyNames="CustomerID" ShowFooter="true" OnPageIndexChanging="GridView2_PageIndexChanging"
                                        OnRowUpdating="GridView2_RowUpdating" OnRowCommand="GridView2_RowCommand" OnRowEditing="GridView2_RowEditing"
                                        GridLines="None" OnRowUpdated="GridView2_RowUpdated" OnRowCancelingEdit="GridView2_CancelingEdit"
                                        OnRowDataBound="GridView2_RowDataBound" OnRowDeleting="GridView2_RowDeleting"
                                        OnRowDeleted="GridView2_RowDeleted" OnSorting="GridView2_Sorting" BorderStyle="Double"
                                        BorderColor="#0083C1">
                                        <RowStyle BackColor="Gainsboro" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#0083C1" ForeColor="White" />
                                        <FooterStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Order ID" SortExpression="OrderID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOrderID" Text='<%# Eval("OrderID") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblOrderID" Text='<%# Eval("OrderID") %>' runat="server"></asp:Label>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Freight" SortExpression="Freight">
                                                <ItemTemplate>
                                                    <%# Eval("Freight")%></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtFreight" Text='<%# Eval("Freight")%>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtFreight" Text='' runat="server"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shipper Name" SortExpression="ShipName">
                                                <ItemTemplate>
                                                    <%# Eval("ShipName")%></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtShipperName" Text='<%# Eval("ShipName")%>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtShipperName" Text='' runat="server"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ship Address" SortExpression="ShipAddress">
                                                <ItemTemplate>
                                                    <%# Eval("ShipAddress")%></ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtShipAdress" Text='<%# Eval("ShipAddress")%>' runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtShipAdress" Text='' runat="server"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkDeleteCust" CommandName="Delete" runat="server">Delete</asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="linkAddOrder" CommandName="AddOrder" runat="server">Add</asp:LinkButton>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/Northwind.mdb"
            SelectCommand="SELECT [Customers].[CustomerID], [Customers].[CompanyName],[Customers].[ContactName],[Customers].[ContactTitle],[Customers].[Address] FROM [Customers] ORDER BY [Customers].[CustomerID]">
        </asp:AccessDataSource>
    </div>--%>
    <div style="top:700px">
                <FCKeditorV2:FCKeditor ID="FCKeditor2" runat="server" BasePath="Editor/" Height="500px"
                        Width="800Px">
                    </FCKeditorV2:FCKeditor></div>
    </form>
</body>
</html>
