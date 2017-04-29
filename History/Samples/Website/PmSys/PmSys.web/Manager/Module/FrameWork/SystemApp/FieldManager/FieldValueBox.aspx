<%@ Page Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="FieldValueBox.aspx.cs" Inherits="PmSys.web.Module.PmSys.FieldManager.FieldValueBox"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="Table_Manager_FieldValue" runat="server" visible="false">
		<tr id="TopTr" runat="server">
			<td class="table_body">
                字段Key</td>
			<td class="table_none">
                <asp:Label ID="F_Key" runat="server" Text=""></asp:Label></td>
		</tr>
            <tr runat="server">
                <td class="table_body">
                    字段名称</td>
                <td class="table_none">
                    <asp:Label ID="F_CName" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr runat="server">
                <td class="table_body">
                    字段值</td>
                <td class="table_none">
                    <asp:TextBox ID="V_Text_Input" title="请输入字段值~100:!" runat="server" CssClass="text_input"></asp:TextBox></td>
            </tr>
            <tr id="Tr1" runat="server">
                <td class="table_body">
                    字段编码</td>
                <td class="table_none">
                    <asp:TextBox ID="V_Code_Input" title="请输入字段编码~100:" runat="server" CssClass="text_input"></asp:TextBox></td>
            </tr>
            <tr runat="server">
                <td colspan="2" align="right" style="height: 26px">
                                    <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />
                                    <asp:Button ID="Button2" runat="server" CssClass="button_bak"  Text="删除" OnClick="Button2_Click"  />
                    <input id="Reset1" class="button_bak" type="reset" value="重填" />
                </td>
            </tr>
		</table>
		
		<table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="Table_OrderBy_FieldValue" runat="server" visible="false">
          <tr class="table_none">
            <td align="center">
                <table>
                <tr>
                    <td style="width: 100px" ><asp:ListBox ID="OrderByListItems"  runat="server" SelectionMode="Multiple" Height="160px" Width="120px"></asp:ListBox></td>
                    <td >
                       	<img border="0" src="<%=Page.ResolveUrl("~/") %>Manager/images/up01.gif" OnClick="javascript:moveUpDown('up','<%=OrderByListItems.ClientID %>');" style="cursor:pointer;">
    			        <p><img border="0" src="<%=Page.ResolveUrl("~/") %>Manager/images/down01.gif" OnClick="javascript:moveUpDown('down','<%=OrderByListItems.ClientID %>');" style="cursor:pointer;">
                    </td>
                </tr>
                </table>
                
            </td>
          </tr>
          <tr><td align="right">
          <asp:Button ID="Button3" runat="server" CssClass="button_bak" Text="确定" OnClick="Button3_Click" />
          </td></tr>
          </table>
</asp:Content>

