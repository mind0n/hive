<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Default.Master" AutoEventWireup="true"
    CodeBehind="CategoriesManage.aspx.cs" Inherits="Site.Admin.CategoriesManage" %>
<asp:Content ID="content3" ContentPlaceHolderID="tabplace" runat="server">
	类别管理
</asp:Content>
<asp:Content ID="toolbarpwa" ContentPlaceHolderID="toolbarpwa" runat="server">
    <asp:Button ID="btnEdit" CssClass="btndel"  runat="server" Text="删除所选" OnClick="btnEdit_Click" />
</asp:Content>
<asp:Content ID="toolbarwaa" ContentPlaceHolderID="toolbarwaa" runat="server">
	<asp:Button ID="btnSave" CssClass="btnsave" ValidationGroup="CaptionValidation" runat="server" Text="提交" OnClick="btnSave_Click" />
	<asp:Button ID="btnCancel" CssClass="btncancel" runat="server" Text="重置" OnClick="btnCancel_Click" />
</asp:Content>
<asp:Content ID="CategoriesManage" ContentPlaceHolderID="targetplace" runat="server">
    <div class="contentcontainer">
        <table class="content">
            <tr>
                <td class="pwa">
                    <div class="gridViewCategoryCss">
                        <asp:GridView ID="CategriesParentGridView" runat="server" AutoGenerateColumns="False"
                            EnableModelValidation="True" OnRowDataBound="CategriesParentGridView_RowDataBound"
                             AllowSorting="true"
                            OnRowCommand="CategriesParentGridView_RowCommand" Width="100%" 
                            onsorting="CategriesParentGridView_Sorting">
                            <RowStyle CssClass="oddrow" />
                            <AlternatingRowStyle BackColor="White" CssClass="evenrow" />
                            <HeaderStyle BackColor="#0083C1" CssClass="headerrow" />
                            <FooterStyle BackColor="White" CssClass="footerrow" />
                            <Columns>
                                <asp:TemplateField HeaderText="<input id='checkAll' onclick='doCheckAllSelectedChange(this)' type='checkbox' />">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="checkBoxSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label Width="60px" Style="cursor: default" ID="lblCategoryID" Text='<%# Eval("categoryid") %>'
                                            runat="server" Font-Bold="True" ForeColor="#000099"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtCategoryID" Text='' runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="类别名称（点击进入文章管理）" SortExpression="caption">
                                    <ItemTemplate>
                                        <a href="ArticlesManage.aspx?categoryid=<%# Eval("categoryid")%>" style="cursor: pointer;">
                                            <asp:Label ID="lblCaption" Text='<%# Eval("caption") %>' runat="server" ToolTip="点击进入文章管理"></asp:Label>
										</a>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="txtCaption" Text='<%# Eval("caption") %>' runat="server"></asp:Label>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtCaption" Text='' runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="类别标识" SortExpression="parentid" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label Width="30px" Style="cursor: default" ID="lblParentId" Text='<%# Eval("parentid") %>'
                                            runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblParentId" Text='<%# Eval("parentid") %>' runat="server"></asp:Label>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtParentId" Text='' runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="是否可见">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkVisible" Width="60" Enabled="false" Checked='<%# Convert.ToBoolean(Eval("visible")) %>'
                                            runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkVisible" runat="server" Checked='<%# Convert.ToBoolean(Eval("visible")) %>'>
                                        </asp:CheckBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:CheckBox ID="chkVisible" runat="server"></asp:CheckBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="最后更新时间" SortExpression="categoryupdate">
                                    <ItemTemplate>
                                        <asp:Label Style="cursor: default" ID="lblLastUpdateTime" Text='<%# Eval("categoryupdate") %>' runat="server"></asp:Label>
                                        <asp:Button ID="btnHiddenPostButton" CommandName="HiddenPostButtonCommand" runat="server" Text="HiddenPostButton" Style="display: none" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="lblLastUpdateTime" Text='<%# Eval("categoryupdate") %>' runat="server"></asp:Label>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtLastUpdateTime" Text='' runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
                <td class="waa">
                    <table class="formeditor" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr style="display:none">
                            <td class="label">
                                ID:
                            </td>
                            <td class="editor">
                                <asp:Label ID="labelID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                所属类别:
                            </td>
                            <td class="editor">
                                <asp:DropDownList ID="dropdownlistParents" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                类别名称:
                            </td>
                            <td class="editor">
                                <asp:TextBox ID="textBoxCaption" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="textBoxCaption"
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
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="content1" ContentPlaceHolderID="msgplace" runat="server">
<p id="errorPlace" runat="server" style=" color:Red"></p>   
</asp:Content>