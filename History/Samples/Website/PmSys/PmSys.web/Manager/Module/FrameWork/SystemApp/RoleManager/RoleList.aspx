<%@ Page Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="RoleList.aspx.cs" Inherits="PmSys.web.Module.PmSys.RoleManager.RoleList"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
    <FrameWorkWebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadOPTxt="角色列表" HeadTitleTxt="角色资料管理" ButtonList-Capacity="4" HeadHelpTxt="点击角色名称进入角色管理">
        <FrameWorkWebControls:HeadMenuButtonItem ButtonName="角色" ButtonPopedom="New" ButtonUrlType="Href"
            ButtonVisible="True" ButtonUrl="RoleManager.aspx?CMD=New" />
    </FrameWorkWebControls:HeadMenuWebControls>
    <FrameWorkWebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem1" runat="server" Tab_Name="角色列表">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns=false>
                <Columns>
                    <asp:HyperLinkField HeaderText="角色名称" DataTextField="R_RoleName" DataNavigateUrlFields="RoleID" DataNavigateUrlFormatString="RoleManager.aspx?RoleID={0}&CMD=Look" />                
                    <asp:BoundField HeaderText="角色介绍" DataField="R_Description"/>
                    <asp:TemplateField HeaderText ="所属用户">
                        <ItemTemplate>
                            <%#PmSys.BusinessFacade.sys_UserDisp((int)Eval("R_UserID")).U_LoginName %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <FrameWorkWebControls:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged">
            </FrameWorkWebControls:AspNetPager>
        </FrameWorkWebControls:TabOptionItem>
    </FrameWorkWebControls:TabOptionWebControls>
</asp:Content>
