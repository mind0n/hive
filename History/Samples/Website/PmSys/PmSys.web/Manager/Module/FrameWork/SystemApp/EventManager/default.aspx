<%@ Page Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="PmSys.web.Module.PmSys.EventManager._default"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
    <FrameWorkWebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadOPTxt="事件日志列表" HeadTitleTxt="事件日志管理">
    </FrameWorkWebControls:HeadMenuWebControls>
    <FrameWorkWebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem1" runat="server" Tab_Name="日志列表">
            <asp:GridView ID="GridView1" runat="server" OnSorting="GridView1_Sorting" OnRowCreated="GridView1_RowCreated">
                <Columns>
                <asp:HyperLinkField SortExpression="EventID" HeaderText="ID" DataTextField="EventID" DataNavigateUrlFields="EventID" DataNavigateUrlFormatString="EventDisp.aspx?EventID={0}&CMD=Look" />
                <asp:TemplateField SortExpression="E_U_LoginName" HeaderText = "用户名">
                    <ItemTemplate>
                        <span title="访问地址:<%#Eval("E_From") %>"><%#Eval("E_U_LoginName")%></span>
                    </ItemTemplate>
                </asp:TemplateField>
                                
                
                <asp:TemplateField SortExpression="E_IP" HeaderText = "客户端IP">
                    <ItemTemplate>
                        <%#PmSys.Common.GetIPLookUrl((string)Eval("E_IP")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField SortExpression="E_Type" HeaderText = "事件类型">
                    <ItemTemplate>
                        <span title="<%#Eval("E_Record") %>"><%#Get_Type((int)Eval("E_Type"))%></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField SortExpression="E_A_AppName" HeaderText="应用名称" DataField="E_A_AppName"/>
                <asp:BoundField SortExpression="E_M_Name" HeaderText="模块名称" DataField="E_M_Name"/>
                <asp:BoundField SortExpression="E_DateTime" HeaderText="时间" DataField="E_DateTime" />                
                
                </Columns>
            </asp:GridView>
            <FrameWorkWebControls:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="Pager_PageChanged">
            </FrameWorkWebControls:AspNetPager>
        </FrameWorkWebControls:TabOptionItem>
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem2" runat="server" Tab_Name="查询">
        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
		<tr>
			<td class="table_body table_body_NoWidth" >
                用户名</td>
			<td class="table_none table_none_NoWidth" >
                <asp:DropDownList ID="E_UserID" runat="server">
                </asp:DropDownList></td>
			<td class="table_body table_body_NoWidth" >
                日志类型</td>
			<td class="table_none table_none_NoWidth" >
                <asp:DropDownList ID="E_Type" runat="server">
                    <asp:ListItem Value="">不限</asp:ListItem>
                    <asp:ListItem Value="1">操作日志</asp:ListItem>
                    <asp:ListItem Value="2">安全日志</asp:ListItem>
                    <asp:ListItem Value="3">访问日志</asp:ListItem>
                </asp:DropDownList></td>            
		</tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 28px">
                    应用/模块</td>
                <td class="table_none table_none_NoWidth" style="height: 28px">
                    <asp:DropDownList ID="E_ApplicationID" AutoPostBack="true" runat="server" OnSelectedIndexChanged="E_ApplicationID_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="E_M_PageCode" runat="server">
                    </asp:DropDownList></td>
                <td class="table_body table_body_NoWidth" style="height: 28px">
                    时间</td>
                <td class="table_none table_none_NoWidth" style="height: 28px">
                    <asp:TextBox ID="S_E_DateTime" title="请输入开始日期~:date" onfocus="javascript:HS_setDate(this);" Columns="10" runat="server" CssClass="text_input"></asp:TextBox>~<asp:TextBox
                        ID="E_E_DateTime" title="请输入结束日期~:date" onfocus="javascript:HS_setDate(this);" Columns="10" runat="server" CssClass="text_input"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" style="height: 28px">
                    详细描述</td>
                <td class="table_none table_none_NoWidth" colspan="3" style="height: 28px">
                    <asp:TextBox ID="E_Record" runat="server" Columns="50" CssClass="text_input"></asp:TextBox></td>
            </tr>
				<tr><td colspan="4" align="right">
            <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="查询" OnClick="Button1_Click" /></td></tr>
		</table>
        </FrameWorkWebControls:TabOptionItem>
    </FrameWorkWebControls:TabOptionWebControls>  
</asp:Content>
