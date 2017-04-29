<%@ Page Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="UserSet.aspx.cs" Inherits="PmSys.web.UserSet"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
		    <tr>
			    <td class="table_body">
                    用户名</td>
			    <td class="table_none" >
                    <asp:Label ID="U_LoginName_Txt" runat="server"></asp:Label></td>
		    </tr>
            <tr>
                <td class="table_body">
                    原始密码</td>
                <td class="table_none">
                    <asp:TextBox ID="Old_U_Password" TextMode="Password" title="请输入原始密码~20:" runat="server" CssClass="text_input"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="table_body">
                    新密码</td>
                <td class="table_none">
                    <asp:TextBox ID="New_U_Password" TextMode="Password" title="请输入新密码~20:" runat="server" CssClass="text_input"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="table_body" nowrap>
                    重新输入新密码</td>
                <td class="table_none">
                    <asp:TextBox ID="ReNew_U_Password" TextMode="Password" title="请重新输入新密码~20:" runat="server" CssClass="text_input"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="table_body">
                    菜单样式</td>
                <td class="table_none">
                    <asp:DropDownList ID="MenuSink" runat="server">
                        <asp:ListItem Value="0">经典</asp:ListItem>
                        <asp:ListItem Value="1">流行(仅限IE)</asp:ListItem>
                        <asp:ListItem Value="2">朴素</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="table_body">
                    表格样式</td>
                <td class="table_none">
                    <asp:DropDownList ID="TableSink" runat="server">
                        <asp:ListItem Value="default">经典</asp:ListItem>
                        <asp:ListItem Value="blue">深蓝</asp:ListItem>
                        <asp:ListItem Value="red">深红</asp:ListItem>
                        <asp:ListItem Value="green">深绿</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>            
            <tr>
                <td class="table_body" nowrap>
                    页码设定(每页/笔)</td>
                <td class="table_none">
                    
                    <asp:DropDownList ID="PageSize" runat="server">
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>40</asp:ListItem>
                        <asp:ListItem>80</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                                    <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />
                    <input id="Reset1" class="button_bak" type="reset" value="重填" />
                </td>
            </tr>
		</table>
</asp:Content>
