<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Default.Master" AutoEventWireup="true" CodeBehind="FileUploader.aspx.cs" Inherits="Site.Admin.FileUploader" ValidateRequest="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="msgplace" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="styleplace" runat="server">
	<style type="text/css">
		@import url("<%=Joy.Server.Core.JoyConfig.Instance.RootUrl%>admin/css/upload.css");
		@import url("<%=Joy.Server.Core.JoyConfig.Instance.RootUrl %>Joy/Theme/Default/Viewer.css");
	</style>
	<style type="text/css">
	</style>
	<script type="text/javascript" src="Scripts/Uploader.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="tabplace" runat="server">
	文件上传
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="toolbarpwa" runat="server">
    <input id="btnDelAll" type="button" class="btndel" value="全部删除" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="toolbarwaa" runat="server">
    <input id="btnSave" type="button" class="btnsave" value="保存" />
    <input id="btnReset" type="reset" class="btncancel" value="重置" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="targetplace" runat="server">
    <script type="text/javascript">
        var data = Joy.json('<%=JsonData%>');
    </script>
	<div id="target" class="contentcontainer">
		<div class="fillarea">
			<div class="hfix">
				<div class="vfix">
					<div class="pwa">
						<div id="fviewercontainer">
							<div id="fviewerarea">
					
							</div>
						</div>
					</div>
					<div class="waa">
 						<div id="filelistcontainer">

						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
