<%@ Page Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="PmSys.web.Module.PmSys.SystemErrorLog._default"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
    <FrameWorkWebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" ButtonList-Capacity="4" HeadOPTxt="查看" HeadTitleTxt="系统出错日志">
    </FrameWorkWebControls:HeadMenuWebControls>
    <FrameWorkWebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem1" runat="server" Tab_Name="出错日志列表" Tab_Visible="False">
            <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
                <tr>
                    <td class="table_title">
                        日志文件选择
                        <asp:DropDownList ID="FileLogList" AutoPostBack="true" runat="server" OnSelectedIndexChanged="FileLogList_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server">
                <Columns>
                    <asp:BoundField HeaderText="时间" DataField="LogDateTime"/>
                    <asp:BoundField HeaderText="内容" ItemStyle-HorizontalAlign="left" HtmlEncode="true" DataField="LogTxt"/>
                    <asp:TemplateField HeaderText = "用户IP">
                        <ItemTemplate>
                            <span title="<%#Eval("LogErrorUrl")%>"><%#Eval("LogUserIp")%></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>
            </asp:GridView>
            <FrameWorkWebControls:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged">
            </FrameWorkWebControls:AspNetPager>
        </FrameWorkWebControls:TabOptionItem>
    </FrameWorkWebControls:TabOptionWebControls>
</asp:Content>
