<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/default.Master" AutoEventWireup="true"
    CodeBehind="PcMappingManage.aspx.cs" Inherits="Site.Admin.PcMappingManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="tabplace" runat="server">
显示配置管理	
</asp:Content>
<asp:Content ID="toolbarpwa" ContentPlaceHolderID="toolbarpwa" runat="server">
    <asp:Button ID="btnDel" CssClass="btndel" runat="server" Text="删除所选" OnClick="btnDel_Click" />
</asp:Content>
<asp:Content ID="toolbarwaa" ContentPlaceHolderID="toolbarwaa" runat="server">
    <asp:Button ID="btnSave" CssClass="btnsave" ValidationGroup="CaptionValidation" runat="server" Text="提交" OnClick="btnSave_Click" />
    <asp:Button ID="btnCancel" CssClass="btncancel" runat="server" Text="重置" OnClick="btnCancel_Click" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="targetplace" runat="server">
    <div class="contentcontainer">
		<table class="content"><tr><td class="pwa">
                        <asp:GridView ID="PcMappingGridView" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                            OnRowDataBound="PcMappingGridView_RowDataBound" OnRowCommand="PcMappingGridView_RowCommand"
                            Width="100%" AllowPaging="True"
							PageSize="15" 
							PagerStyle-CssClass="pager"
                            OnPageIndexChanging="PcMappingGridView_PageIndexChanging" AllowSorting="True" 
                            onsorting="PcMappingGridView_Sorting">
                            <RowStyle CssClass="oddrow" />
                            <AlternatingRowStyle CssClass="evenrow" />
                            <HeaderStyle CssClass="header" />
                            <FooterStyle CssClass="pager" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="24" HeaderText="<input id='checkAll' onclick='doCheckAllSelectedChange(this)' type='checkbox' />">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="checkBoxSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label Style="cursor: default" ID="lblPcmID" Text='<%# Eval("pcmid") %>'
                                            runat="server" Font-Bold="True" ForeColor="#000099"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="类别名称" SortExpression="caption">
                                    <ItemTemplate>
                                        <asp:Label Width="80px" ID="lblCaption" Text='<%# Eval("caption") %>' runat="server"></asp:Label>
                                        <asp:HiddenField ID="hiddentCatagoryId" runat="server" Value='<%# Eval("categoryid")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="显示顺序">
                                    <ItemTemplate>
                                        <asp:Label Width="30px" Style="cursor: default" ID="lblmorder" Text='<%# Eval("morder") %>'
                                            runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="页面标识">
                                    <ItemTemplate>
                                        <asp:Label Width="70px" Style="cursor: default" ID="lblpid" Text='<%# Eval("pid") %>'
                                            runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="目标容器">
                                    <ItemTemplate>
                                        <asp:Label Width="70px" Style="cursor: default" ID="lblcontainer" Text='<%# Eval("container") %>'
                                            runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="widgetname" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label Width="70px" Style="cursor: default" ID="lblwidgetname" Text='<%# Eval("widgetname") %>'
                                            runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="widgetsettings" Visible="false">
                                    <ItemTemplate>
                                        <p style="text-align: left">
                                            <asp:Label Width="70px" Style="cursor: default;" ID="lblwidgetsettings" Text='<%# Eval("widgetsettings") %>'
                                                runat="server"></asp:Label></p>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="是否可见">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkVisible" Width="60" Enabled="false" Checked='<%# Convert.ToBoolean(Eval("clientvisible")) %>'
                                            runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="最后更新时间" SortExpression="pcupdate">
                                    <ItemTemplate>
                                        <asp:Label Width="140px" Style="cursor: default" ID="lblLastUpdateTime" Text='<%# Eval("pcupdate") %>'
                                            runat="server"></asp:Label>
                                        <asp:Button ID="btnHiddenPostButton" CommandName="HiddenPostButtonCommand" runat="server"
                                            Text="HiddenPostButton" Style="display: none" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
					<td class="waa">
                <table class="formeditor" cellspacing="0" cellpadding="0" border="0">
                    <tr style="display:none">
                        <td class="label">
                            PcmID:
                        </td>
                        <td class="editor">
                            <asp:Label ID="labelID" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            选择类别:
                        </td>
                        <td class="editor">
                            <asp:DropDownList ID="dropdownlistCategories" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            显示顺序:
                        </td>
                        <td class="editor short">
                            <asp:TextBox ID="textBoxMorder" runat="server" Text="0"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                                ControlToValidate="textBoxMorder" runat="server" ErrorMessage="必须数字" 
                                ValidationExpression="^[0-9]+$" ValidationGroup="CaptionValidation"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            所属页面:
                        </td>
                        <td class="editor">
                            <asp:TextBox ID="textBoxPid" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="textBoxPid"
                                runat="server" ErrorMessage="*" ValidationGroup="CaptionValidation"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            目标容器:
                        </td>
                        <td class="editor">
                            <asp:TextBox ID="textBoxContainer" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="textBoxContainer"
                                runat="server" ErrorMessage="*" ValidationGroup="CaptionValidation"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            脚本控件名称:
                        </td>
                        <td class="editor">
                            <asp:TextBox ID="textBoxWidgetname" runat="server" Text="Grid"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="textBoxWidgetname"
                                runat="server" ErrorMessage="*" ValidationGroup="CaptionValidation"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            脚本控件配置:
                        </td>
                        <td class="editor">
                            <asp:TextBox ID="textBoxWidgetsettings" runat="server" Text="{}"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="textBoxWidgetsettings"
                                runat="server" ErrorMessage="*" ValidationGroup="CaptionValidation"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            是否可见:
                        </td>
                        <td class="editor">
                            <asp:CheckBox ID="checkBoxIsVisible" runat="server" />
                        </td>
                    </tr>
                </table>
					</td>
					</tr></table>
    </div>
</asp:Content>
<asp:Content ID="content3" ContentPlaceHolderID="msgplace" runat="server">
<p id="errorPlace" runat="server" style=" color:Red"></p>   
</asp:Content>