<%@ Page Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="PmSys.web.Module.PmSys.OnlineUserManager._default"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
    <FrameWorkWebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server"
        HeadOPTxt="查看在线用户" HeadTitleTxt="在线用户列表">
    </FrameWorkWebControls:HeadMenuWebControls>
    <FrameWorkWebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem1" runat="server" Tab_Name="在线用户列表">
            <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand">
                <Columns>
                    <asp:BoundField DataField="U_Name" HeaderText="用户名" />
                    <asp:BoundField DataField="U_StartTime" HeaderText="进入时间" />
                    <asp:BoundField DataField="U_LastTime" HeaderText="最后访问时间" />
                    <asp:TemplateField HeaderText = "在线时间">
                        <ItemTemplate>
                            <span title="最后访问:<%#Eval("U_LastUrl")%>"><%#string.Format("{0:N2}", (double)Eval("U_OnlineSeconds") / 60)%>(分)</span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText = "IP地址">
                        <ItemTemplate>
                        <%#PmSys.Common.GetIPLookUrl((string)Eval("U_LastIP")) %>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText = "">
                        <ItemTemplate>
                            <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="下线"  CommandName="OutOnline" OnClientClick="return confirm('确认要强制用户下线吗？');"  CommandArgument='<%# Eval("U_Name")%>'></asp:Button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <FrameWorkWebControls:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged">
            </FrameWorkWebControls:AspNetPager>
        </FrameWorkWebControls:TabOptionItem>
    </FrameWorkWebControls:TabOptionWebControls>
</asp:Content>
