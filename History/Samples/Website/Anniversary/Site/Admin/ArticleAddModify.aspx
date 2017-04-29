<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/default.Master" AutoEventWireup="true"
    CodeBehind="ArticleAddModify.aspx.cs" Inherits="Site.Admin.ArticleAddModify" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="content3" ContentPlaceHolderID="tabplace" runat="server">
	文章添加修改        
</asp:Content>
<asp:Content ID="toolbarwaa" ContentPlaceHolderID="toolbarwaa" runat="server">
	<asp:Button ID="btnAdd" CssClass="btnsave" runat="server"  ValidationGroup="ValidationRequiredFiled" 
		Text="提交" OnClick="btnAdd_Click" />
	<asp:Button ID="btnReset" CssClass="btncancel" runat="server" Text="重置" OnClick="btnReset_Click" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="targetplace" runat="server">
    <div class="contentcontainer">
        <table cellspacing="0" cellpadding="0" class="formeditor wide">
            <tr>
                <td class="label">
                    标　题：
                </td>
                <td class="editor">
                    <asp:TextBox ID="textBoxCaption" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="ValidationRequiredFiled" ControlToValidate="textBoxCaption" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="label">
                    链　接：
                </td>
                <td class="editor">
                    <asp:TextBox ID="txtLink" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="label">
                    标　签：
                </td>
                <td class="editor">
                    <asp:TextBox ID="txtTag" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="label">
                    是否可见：
                </td>
                <td class="editor">
                    <asp:CheckBox ID="checkBoxVisible" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="label">
                    所属类别：
                </td>
                <td class="editor">
					<table><tr><td>
						<asp:DropDownList ID="dropdownlistCategories" runat="server">
						</asp:DropDownList>
					</td><td class="label">
						文章类型:
					</td>
					<td class="editor">
						<asp:DropDownList ID="ddArticleType" runat="server">
						</asp:DropDownList>
					</td></tr></table>
                </td>
            </tr>
            <tr>
                <td class="label">
                    摘　要：
                </td>
                <td class="editor">
                    <asp:TextBox ID="txtBrief" ReadOnly="true" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="label">
                    内　容：
                </td>
                <td class="editor">
                    <FCKeditorV2:FCKeditor ID="FCKeditor2" runat="server" BasePath="Editor/" Height="300px"
                        Width="100%">
                    </FCKeditorV2:FCKeditor>
                </td>
            </tr>
            <tr runat="server" style="display:none;" id="temp" visible="false">
                <td class="label">
                    作　者：
                </td>
                <td class="editor">
                    <asp:Label ID="LabelAuthor" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="content1" ContentPlaceHolderID="msgplace" runat="server">
<p id="errorPlace" runat="server" style=" color:Red"></p>   
</asp:Content>
