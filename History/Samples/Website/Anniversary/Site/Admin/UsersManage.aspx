<%@ Page Language="C#" MasterPageFile="~/Admin/Default.Master" AutoEventWireup="true"
    CodeBehind="UsersManage.aspx.cs" Inherits="Site.Admin.UsersManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="tabplace" runat="server">
    用户管理
</asp:Content>
<asp:Content ID="waatoolbar" ContentPlaceHolderID="toolbarwaa" runat="server">
    <asp:Button ID="btnDel" runat="server" Text="删除所选" OnClick="btnDel_Click" OnClientClick="javascript:if(!window.confirm('确认删除吗？')){return false;};"
        CssClass="btndel" />
</asp:Content>
<asp:Content ID="UserManage" ContentPlaceHolderID="targetplace" runat="server">
        <div class="contentcontainer">
            <asp:GridView Width="100%" ID="GridViewUsers" runat="server" AutoGenerateColumns="False"
                EnableModelValidation="True" ShowFooter="true" OnRowEditing="GridViewUsers_RowEditing"
                OnRowUpdating="GridViewUsers_RowUpdating" OnRowCancelingEdit="GridViewUsers_RowCancelingEdit"
                OnRowDataBound="GridViewUsers_RowDataBound" OnRowCommand="GridViewUsers_RowCommand">
                <RowStyle BackColor="#dbe7f6" />
                <AlternatingRowStyle BackColor="White" />
                <HeaderStyle BackColor="#0083C1" ForeColor="White" Height="30" />
                <FooterStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="<input id='checkAll' onclick='doCheckAllSelectedChange(this)' type='checkbox' />" ItemStyle-Width="12">
                        <ItemTemplate>
                            <asp:CheckBox ID="selectCheckBox" runat="server" />
                            <asp:HiddenField ID="userIdHiddenField" runat="server" Value='<%#Eval("userid")%>' />
                            <asp:HiddenField ID="HiddenFieldUname" runat="server" Value='<%#Eval("uname")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="用户名" ItemStyle-Width="120">
                        <ItemTemplate>
                            <asp:Label ID="LabelUserName" runat="server" Text='<%#Eval("uname") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxUserName" Width="100%" runat="server" Text='<%#Eval("uname") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TextBoxUserNameFooter" Width="100%" runat="server" Text=''></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="密码" ItemStyle-Width="180">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text="******"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxPwd" Width="100%" TextMode="Password" runat="server" Text='<%#Eval("upwd") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TextBoxPwdFooter" Width="100%" TextMode="Password" runat="server"
                                Text=''></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="描述信息" ItemStyle-Width="180">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("utext") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="textBoxUtext" Width="100%" TextMode="SingleLine" runat="server"
                                Text='<%#Eval("utext") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox Width="100%" ID="textBoxUtextFooter" runat="server" Text="">
                            </asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="用户等级" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="LabelUlevel" runat="server" Text='<%#Eval("ulevel") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlUlevel" runat="server"></asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlUlevelFooter" runat="server"></asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="最后一次更新时间" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%#Eval("userupdate") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%= DateTimeStr %>'></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ItemStyle-Width="80" ShowEditButton="True" EditText="更改" CancelText="取消"
                        UpdateText="保存" />
                    <asp:TemplateField ItemStyle-Width="40">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkDelete" CommandName="DeleteUser" CommandArgument='<%# Eval("userid") %>'
                                runat="server" OnClientClick="javascript:if(!window.confirm('确认删除吗？')){return false;};">删除</asp:LinkButton>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="linkAdd" CommandName="AddUser" runat="server">添加</asp:LinkButton>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
</asp:Content>
<asp:Content ID="ep" ContentPlaceHolderID="msgplace" runat="server">
<div runat="server" ID="errorPlace">
游客：不能登录后台，只能提交祝福寄语<br />
学生：不能管理其他用户账户 &nbsp;&nbsp; 管理员：不受限制。
</div>
</asp:Content>