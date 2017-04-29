<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/default.Master" AutoEventWireup="true"
    CodeBehind="ArticlesManage.aspx.cs" Inherits="Site.Admin.ArticlesManage" %>

<asp:Content ID="content3" ContentPlaceHolderID="tabplace" runat="server">
    文章列表
</asp:Content>
<asp:Content ID="waatoolbar" ContentPlaceHolderID="toolbarwaa" runat="server">
    <asp:Button ID="btnDel" runat="server" CssClass="btndel" Text="删除所选" OnClick="btnDel_Click"
        OnClientClick="javascript:if(!window.confirm('确认删除吗？')){return false;};" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="targetplace" runat="server">
    <style type="text/css">
        /* tooltip */
        #tooltip
        {
            position: absolute;
            border: 1px solid #333;
            background: #f7f5d1;
            padding: 1px;
            color: #333;
            text-align:left;
        }
    </style>
    <script type="text/javascript">
    	var box = document.createElement('div');
    	box.className = 'briefbox';
    	document.body.appendChild(box);
    	function showBrief(obj) {
    		var div = obj.childNodes[1];
    		if (div != null) {
    			box.innerHTML = "文章摘要：<br />" + div.innerHTML;
    			box.style.display = '';
    		}
        }
        function hideBrief() {
        	box.style.display = 'none';
        }
    </script>
    <div class="inputtabcolor" style="height: 100%; width: 100%;">
        <asp:GridView CssClass="gridViewArticlesCss" ID="GridViewArticles" Width="100%" runat="server"
            AutoGenerateColumns="False" EnableModelValidation="True" AllowPaging="True" OnPageIndexChanging="GridViewArticles_PageIndexChanging"
            PagerStyle-CssClass="pager" AllowSorting="true" OnRowDataBound="GridViewArticles_DataBound"
            PageSize="15" EmptyDataText="没有记录!" OnSorting="GridViewArticles_Sorting" PagerSettings-PageButtonCount="10">
            <RowStyle CssClass="oddrow" />
            <AlternatingRowStyle CssClass="evenrow" />
            <HeaderStyle CssClass="header" />
            <FooterStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="<input id='checkAll' onclick='doCheckAllSelectedChange(this)' type='checkbox' />">
                    <ItemTemplate>
                        <asp:CheckBox ID="checkBoxSelect" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="编号" SortExpression="articleid" Visible="false">
                    <ItemTemplate>
                        <asp:Label Width="60px" ID="lblArticleID" Font-Bold="True" ForeColor="#000099" Text='<%# Eval("articleid") %>'
                            runat="server"></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtArticleID" Text='' runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="文章标题（点击修改）">
                    <ItemTemplate>
						<div onmouseover="showBrief(this);" style="text-align:left;">
							<a href="ArticleAddModify.aspx?articleid=<%# Eval("articleid")%>&articlePageStatus=modify"
								style="cursor: pointer;">
								<asp:Label Width="300px" ID="lblArticlesCaption" ToolTip="点击进入文章修改" Text='<%# Eval("Acaption") %>'
									runat="server"></asp:Label>
							</a>
							<div id="divToolTip" class="divbrief" style="display:none;" runat="server">
							<%# Eval("brief") %>
							</div>
							<div id="divContent" class="divbrief" visible="false" runat="server">
							<%# Eval("content") %>
							</div>
							
						</div>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="txtArticlesCaption" Text='<%# Eval("Acaption") %>' runat="server"></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtArticlesCaption" Text='' runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="作者">
                    <ItemTemplate>
                        <asp:Label Width="100px" ID="lbluname" Text='<%# Eval("utext") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lbluname" Text='<%# Eval("utext") %>' runat="server"></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtuname" Text='' runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="文章类别">
                    <ItemTemplate>
						<a href='ArticlesManage.aspx?categoryid=<%# Eval("Acategoryid")%>' style="cursor: pointer;">
							<asp:Label ID="lbltCategoriesCaption" Text='<%# Eval("Ccaption") %>' runat="server"></asp:Label>
						</a>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lbltCategoriesCaption" Text='<%# Eval("Ccaption") %>' runat="server"></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txttCategoriesCaption" Text='' runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="访问次数">
                    <ItemTemplate>
                        <asp:Label Width="100px" ID="lblcounter" Text='<%# Eval("counter") %>' runat="server"
                            Font-Bold="True"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblcounter" Text='<%# Eval("counter") %>' runat="server"></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtcounter" Text='' runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="链接" Visible="false">
                    <ItemTemplate>
                        <asp:Label Width="100px" ID="lbllink" Text='<%# Eval("link") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lbllink" Text='<%# Eval("link") %>' runat="server"></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtlink" Text='' runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="图片地址" Visible="false">
                    <ItemTemplate>
                        <asp:Label Width="100px" ID="lbltag" Text='<%# Eval("tag") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lbltag" Text='<%# Eval("tag") %>' runat="server"></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txttag" Text='' runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="可见性">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkVisible" Enabled="false" Checked='<%# Convert.ToBoolean(Eval("Avisible")) %>'
                            runat="server"></asp:CheckBox>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:CheckBox ID="chkVisible" runat="server" Checked='<%# Convert.ToBoolean(Eval("Avisible")) %>'>
                        </asp:CheckBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:CheckBox ID="chkVisible" runat="server"></asp:CheckBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="最后更新时间" SortExpression="articleupdate">
                    <ItemTemplate>
                        <asp:Label Width="140px" ID="lblLastUpdateTime" Text='<%# Eval("articleupdate") %>'
                            runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblLastUpdateTime" Text='<%# Eval("articleupdate") %>' runat="server"></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtLastUpdateTime" Text='' runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
<asp:Content ID="content1" ContentPlaceHolderID="msgplace" runat="server">
<p id="errorPlace" runat="server"></p>   
</asp:Content>
