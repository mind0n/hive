<%@ Page Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="Orderby.aspx.cs" Inherits="PmSys.web.Module.PmSys.GroupManager.Orderby"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
    <FrameWorkWebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server"
        HeadOPTxt="部门排序" HeadTitleTxt="部门资料管理">
    </FrameWorkWebControls:HeadMenuWebControls>
    <FrameWorkWebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem1" runat="server" Tab_Name="排序">
          <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" >
          <tr class="table_title">
            <td>
                <asp:Label ID="CatListTitle" runat="server"></asp:Label>
            </td>
          </tr>
          <tr class="table_none">
            <td align="center">
                <table >
                <tr>
                    <td><asp:ListBox ID="OrderByListItems" runat="server" SelectionMode="Multiple"></asp:ListBox></td>
                    <td>
                       	<img border="0" src="<%=Page.ResolveUrl("~/") %>Manager/images/up01.gif" OnClick="javascript:moveUpDown('up','<%=OrderByListItems_UniqueID %>');" style="cursor:pointer;">
    			        <p><img border="0" src="<%=Page.ResolveUrl("~/") %>Manager/images/down01.gif" OnClick="javascript:moveUpDown('down','<%=OrderByListItems_UniqueID %>');" style="cursor:pointer;">
                    </td>
                </tr>
                </table>
                
            </td>
          </tr>
          <tr><td align="right">
          <asp:Button ID="Button2" runat="server" CssClass="button_bak" Text="确定" OnClick="Button2_Click" />
          </td></tr>
          </table>
        </FrameWorkWebControls:TabOptionItem>
    </FrameWorkWebControls:TabOptionWebControls>
</asp:Content>
