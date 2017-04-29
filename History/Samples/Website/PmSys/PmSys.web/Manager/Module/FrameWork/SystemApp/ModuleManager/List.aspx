<%@ Page Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true"
    Codebehind="List.aspx.cs" Inherits="PmSys.web.Module.PmSys.ModuleManager.List"
     %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
    <FrameWorkWebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server"
        HeadHelpTxt="点击应用名称进入模块管理" HeadOPTxt="应用模块管理" HeadTitleTxt="应用模块管理">
    </FrameWorkWebControls:HeadMenuWebControls>
    <FrameWorkWebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem1" runat="server" Tab_Name="应用列表">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="应用ID" DataField="ApplicationID" />
                    <asp:HyperLinkField DataNavigateUrlFields="ApplicationID,A_AppName" DataNavigateUrlFormatString="ModuleList.aspx?S_ID={0}&AppName={1}"
                        DataTextField="A_AppName" HeaderText="应用名称" />
                    <asp:BoundField DataField="A_AppDescription" HeaderText="应用介绍" />
                    <asp:TemplateField HeaderText="模块分类">
                        <ItemTemplate>
                            <%#GetCatCount((int)Eval("ApplicationID"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <FrameWorkWebControls:AspNetPager ID="Pager" runat="server" OnPageChanged="Pager_PageChanged">
            </FrameWorkWebControls:AspNetPager>
        </FrameWorkWebControls:TabOptionItem>
    </FrameWorkWebControls:TabOptionWebControls>
</asp:Content>
