<%@ Page Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="ModuleList.aspx.cs" Inherits="PmSys.web.Module.PmSys.ModuleManager.ModuleList"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
    <FrameWorkWebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server"
        HeadOPTxt="应用模块管理" HeadTitleTxt="应用模块管理" HeadHelpTxt="点击模块名称进入管理" ButtonList-Capacity="4">
        <FrameWorkWebControls:HeadMenuButtonItem ButtonName="应用列表" ButtonPopedom="List" ButtonUrl="list.aspx"
            ButtonUrlType="Href" ButtonVisible="True" ButtonIcon="list.gif" />
    </FrameWorkWebControls:HeadMenuWebControls>
    <FrameWorkWebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem1" runat="server" Tab_Name="应用模块管理">
            
            <asp:GridView ID="GridView1" runat="server">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="M_ApplicationID,ModuleID" DataNavigateUrlFormatString="ModuleManager.aspx?S_ID={0}&ModuleID={1}"
                        DataTextField="M_CName" HeaderText="模块分类名称" />
                <asp:BoundField DataField="M_PageCode" HeaderText="模块分类编码" />
                <asp:TemplateField HeaderText = "模块数">
                    <ItemTemplate>
                        <%#GetSuBCount((int)Eval("ModuleID"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="系统模块">
                    <ItemTemplate>
                        <%#GetStatus((int)Eval("M_IsSystem"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="关闭">
                    <ItemTemplate>
                        <%#GetStatus((int)Eval("M_Close"))%>
                    </ItemTemplate>
                </asp:TemplateField>                

            </Columns>
            </asp:GridView>
	            
        
        </FrameWorkWebControls:TabOptionItem>
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem2" runat="server" Tab_Name="模块分类排序">
          <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" >
          <tr class="table_none">
            <td align="center">
                <table >
                <tr>
                    <td> 
                    <asp:ListBox ID="OrderByListItems" runat="server" SelectionMode="Multiple"></asp:ListBox></td>
                    <td>
                       	<img border="0" src="<%=Page.ResolveUrl("~/Manager/") %>images/up01.gif" OnClick="javascript:moveUpDown('up','<%=OrderByListItems_UniqueID %>');" style="cursor:pointer;">
    			        <p><img border="0" src="<%=Page.ResolveUrl("~/Manager/") %>images/down01.gif" OnClick="javascript:moveUpDown('down','<%=OrderByListItems_UniqueID %>');" style="cursor:pointer;">
                    </td>
                </tr>
                </table>
                
            </td>
          </tr>
          <tr><td align="right">
          <asp:Button ID="Button1" runat="server" CssClass="button_bak" OnClick="Button1_Click" Text="确定" />
          </td></tr>
          </table>
       </FrameWorkWebControls:TabOptionItem>
    </FrameWorkWebControls:TabOptionWebControls>
</asp:Content>
