<%@ Page Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="mylogin.aspx.cs" Inherits="PmSys.web.Manager.mylogin"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
    <asp:GridView ID="GridView1" runat="server" OnSorting="GridView1_Sorting" OnRowCreated="GridView1_RowCreated">
    <Columns>
                                
                <asp:BoundField SortExpression="E_DateTime" HeaderText="时间" DataField="E_DateTime" />                                
                <asp:TemplateField SortExpression="E_IP" HeaderText = "客户端IP">
                    <ItemTemplate>
                        <%#PmSys.Common.GetIPLookUrl((string)Eval("E_IP")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField SortExpression="E_Record" HeaderText="描述" DataField="E_Record" />             
    </Columns>
    </asp:GridView>
    <FrameWorkWebControls:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged">
    </FrameWorkWebControls:AspNetPager>
</asp:Content>