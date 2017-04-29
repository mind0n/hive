<%@ Page Language="C#" MasterPageFile="~/Manager/MasterPage/PageTemplate.Master" AutoEventWireup="true" CodeBehind="UserManager.aspx.cs" Inherits="PmSys.web.Module.PmSys.UserManager.UserManager"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
        <link rel="stylesheet" type="text/css" href="<%=Page.ResolveUrl("~/") %>Manager/inc/FineMessBox/css/subModal.css" />

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>Manager/inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="<%=Page.ResolveUrl("~/") %>Manager/inc/FineMessBox/js/subModal.js"></script>

    <FrameWorkWebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server"
        HeadOPTxt="用户资料" HeadTitleTxt="用户资料管理">
        <FrameWorkWebControls:HeadMenuButtonItem ButtonName="用户" ButtonPopedom="List" ButtonUrl="Default.aspx"
            ButtonUrlType="Href" ButtonVisible="True" />
    </FrameWorkWebControls:HeadMenuWebControls>
    <FrameWorkWebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem1" runat="server" Tab_Name="用户帐号">
        <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
		<tr>
			<td class="table_body table_body_NoWidth" style="height: 28px" >
                用户名</td>
			<td class="table_none table_none_NoWidth" style="height: 28px" >
                <asp:TextBox ID="U_LoginName" title="请输入用户名~20:!" runat="server" CssClass="text_input"></asp:TextBox>
                <asp:Label ID="U_LoginName_Value" runat="server"></asp:Label></td>
			<td class="table_body table_body_NoWidth" style="height: 28px" >
                密码</td>
			<td class="table_none table_none_NoWidth" style="height: 28px" >
                <asp:TextBox ID="U_Password" TextMode="Password" title="请输入密码~32:!" runat="server" CssClass="text_input"></asp:TextBox>
                <asp:Label ID="U_Password_Value" runat="server" ></asp:Label></td>            
		</tr>
            <tr>
                <td class="table_body table_body_NoWidth" >
                    用户类型</td>
                <td class="table_none table_none_NoWidth" >
                    <asp:DropDownList ID="U_Type" runat="server">
                    <asp:ListItem Value="1">普通用户</asp:ListItem>
                    <asp:ListItem Value="2">管理员</asp:ListItem>
                    <asp:ListItem Value="0">超级用户</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="U_Type_Value" runat="server"></asp:Label></td>
                <td class="table_body table_body_NoWidth" >
                    禁止登陆</td>
                <td class="table_none table_none_NoWidth" >
                    <asp:DropDownList ID="U_Status" runat="server">
                    <asp:ListItem Value="0">正常</asp:ListItem>
                    <asp:ListItem Value="1">禁止</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="U_Status_Value" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth">
                    用户角色</td>
                <td class="table_none table_none_NoWidth" colspan="3">
                    <FrameWorkWebControls:MultiListBox ID="MultiListBox1" runat="server" DataTextField="R_RoleName" DataValueField="RoleID" SelectionMode="Multiple">
                        <FirstListBox><StyleSheet Width="140px" Height="160px" /></FirstListBox>
                        <SecondListBox><StyleSheet Width="140px" Height="160px" /></SecondListBox>
                    </FrameWorkWebControls:MultiListBox>
                    <asp:Label ID="Roles_Value" runat="server" Text=""></asp:Label>
                    (超级用户不受用户角色权限限制,默认所有权限.)
                    </td>
            </tr>
		</table>      
        </FrameWorkWebControls:TabOptionItem>
        <FrameWorkWebControls:TabOptionItem ID="TabOptionItem2" runat="server" Tab_Name="用户资料" Tab_Visible="False">
         <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
            <tr>
                <td class="table_body table_body_NoWidth" >
                    员工编号</td>
                <td class="table_none table_none_NoWidth">
                    <asp:TextBox ID="U_UserNO" title="请输入员工编号~20:" runat="server" CssClass="text_input"></asp:TextBox>
                    <asp:Label ID="U_UserNO_Value" runat="server" Text=""></asp:Label></td>
                <td class="table_body table_body_NoWidth" rowspan="4">
                    照片</td>
                <td class="table_none table_none_NoWidth" rowspan="4">
                    <asp:Image ID="U_PhotoUrl_Value" border=0 runat="server" onclick="javascript:window.open(MaxImgUrl)" style="cursor:pointer" title="点击放大" />
                    
                    <asp:FileUpload ID="U_PhotoUrl" runat="server" contentEditable=false CssClass="text_input" /></td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" >
                    中文名</td>
                <td class="table_none table_none_NoWidth" >
                    <asp:TextBox ID="U_CName" title="请输入中文名~20:" runat="server" CssClass="text_input"></asp:TextBox>
                    <asp:Label ID="U_CName_Value" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" >
                    英文名</td>
                <td class="table_none table_none_NoWidth" >
                    <asp:TextBox ID="U_EName" title="请输入英文名~50:" runat="server" CssClass="text_input"></asp:TextBox>
                    <asp:Label ID="U_EName_Value" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" >
                    部门</td>
                <td class="table_none table_none_NoWidth" >
                        <span id="U_GroupID_Span" runat="server">
                        <input type="hidden" runat="server" name="U_GroupID" id="U_GroupID"><input runat=server name="U_GroupID_Txt" id="U_GroupID_Txt" class="text_input" readonly>
                        <input type=button value="选择部门" id=button3 name="buttonselect" onClick="javascript:ShowDepartID()" class="cbutton">
                        <input type="button" value="清除" onclick="javascript:ClearSelect();" class="cbutton" />
                        </span>
                    <asp:Label ID="U_GroupID_Value" runat="server" Text=""></asp:Label></td>
            </tr>
		<tr>
			<td class="table_body table_body_NoWidth" >
                    性别</td>
			<td class="table_none table_none_NoWidth" >
                <asp:DropDownList ID="U_Sex" runat="server">
                <asp:ListItem Value="1">男</asp:ListItem>
                <asp:ListItem Value="0">女</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="U_Sex_Value" runat="server" Text=""></asp:Label></td>
			<td class="table_body table_body_NoWidth" >
                    出生年月</td>
			<td class="table_none table_none_NoWidth" >
                <asp:TextBox ID="U_BirthDay" title="请输入出生年月~:date" runat="server" CssClass="text_input" onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                <asp:Label ID="U_BirthDay_Value" runat="server" Text=""></asp:Label></td>            
		</tr>
            <tr>
                <td class="table_body table_body_NoWidth" >
                    职称</td>
                <td class="table_none table_none_NoWidth" >
                    <FrameWorkWebControls:FieldWebControls ID="FieldWebControls1" runat="server" F_Key="Title" Field_Name="U_Title" LineNum="2" LineNumOn="True" />
                    <asp:Label ID="U_Title_Value" runat="server" Text=""></asp:Label></td>
                <td class="table_body table_body_NoWidth" >
                    身份证号码
                </td>
                <td class="table_none table_none_NoWidth" >
                    <asp:TextBox ID="U_IDCard" title="请输入身份证号码~30:" runat="server" CssClass="text_input"></asp:TextBox>
                    <asp:Label ID="U_IDCard_Value" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" >
                    家中电话</td>
                <td class="table_none table_none_NoWidth" >
                    <asp:TextBox ID="U_HomeTel" title="请输入家中电话~20:" runat="server" CssClass="text_input"></asp:TextBox>
                    <asp:Label ID="U_HomeTel_Value" runat="server" Text=""></asp:Label></td>
                <td class="table_body table_body_NoWidth" >
                    手机号</td>
                <td class="table_none table_none_NoWidth" >
                    <asp:TextBox ID="U_MobileNo" title="请输入手机号~15:" runat="server" CssClass="text_input"></asp:TextBox>
                    <asp:Label ID="U_MobileNo_Value" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" >
                    公司邮件地址</td>
                <td class="table_none table_none_NoWidth" >
                    <asp:TextBox ID="U_CompanyMail" title="请输入公司邮件地址~100:email" runat="server" CssClass="text_input"></asp:TextBox>
                    <asp:Label ID="U_CompanyMail_Value" runat="server" Text=""></asp:Label></td>
                <td class="table_body table_body_NoWidth" >
                    邮件地址</td>
                <td class="table_none table_none_NoWidth" >
                    <asp:TextBox ID="U_Email" title="请输入邮件地址~100:email" runat="server" CssClass="text_input"></asp:TextBox>
                    <asp:Label ID="U_Email_Value" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" >
                    分机号</td>
                <td class="table_none table_none_NoWidth" >
                    <asp:TextBox ID="U_Extension" title="请输入分机号~10:" runat="server" CssClass="text_input"></asp:TextBox>
                    <asp:Label ID="U_Extension_Value" runat="server" Text=""></asp:Label></td>
                <td class="table_body table_body_NoWidth" >
                    到职日期</td>
                <td class="table_none table_none_NoWidth" >
                    <asp:TextBox ID="U_WorkStartDate" title="请输入到职日期~:date" runat="server" CssClass="text_input" onfocus="javascript:HS_setDate(this);"></asp:TextBox>
                    <asp:Label ID="U_WorkStartDate_Value" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="table_body table_body_NoWidth" >
                    备注说明</td>
                <td class="table_none table_none_NoWidth"  colspan="3">
                    <asp:TextBox ID="U_Remark" title="请输入备注说明~2000:" runat="server" Rows="5" TextMode="MultiLine" CssClass="tex_input"></asp:TextBox>
                    <asp:Label ID="U_Remark_Value" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr id="DispTr" runat="server" visible="false">
                <td class="table_body table_body_NoWidth" >
                    最后访问IP</td>
                <td class="table_none table_none_NoWidth" >
                    <asp:Label ID="U_LastIP_Value" runat="server" Text=""></asp:Label></td>
                <td class="table_body table_body_NoWidth" >
                    最后操作时间</td>
                <td class="table_none table_none_NoWidth" >
                    <asp:Label ID="U_LastDateTime_Value" runat="server" Text=""></asp:Label></td>
            </tr>
		</table>
        </FrameWorkWebControls:TabOptionItem>
        </FrameWorkWebControls:TabOptionWebControls>
    <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" id="PostButton" runat="server">
		<tr>
			<td align="right">
            <asp:Button ID="Button1" runat="server" CssClass="button_bak" Text="确定" OnClick="Button1_Click" />
            <input id="Reset1" class="button_bak" type="reset" value="重填" />
            </td>
        </tr>
    </table>    
        <script language="javascript">
       var MaxImgUrl = "<%=MaxImgUrl %>"; 
        rnd.today=new Date(); 

    rnd.seed=rnd.today.getTime(); 

    function rnd() { 

　　　　rnd.seed = (rnd.seed*9301+49297) % 233280; 

　　　　return rnd.seed/(233280.0); 

    }; 

    function rand(number) { 

　　　　return Math.ceil(rnd()*number); 

    }; 
    
    
    function AlertMessageBox(file_name)
    {

	        if (file_name!=undefined){
	            var ShValues = file_name.split('||');
	            if (ShValues[1]!=0)
	            {
	                document.all.<%=this.U_GroupID_Txt.ClientID %>.value=ShValues[0];
	                document.all.<%=this.U_GroupID.ClientID %>.value=ShValues[1];
	            }            
	        }
	         
    }
      function ShowDepartID()
        {
            showPopWin('选择部门','SelectGroup.aspx?'+rand(10000000), 215, 255, AlertMessageBox,true,true)
        }
       
        

      
        
        function ClearSelect()
        {
   	        document.all.<%=this.U_GroupID_Txt.ClientID %>.value="";
            document.all.<%=this.U_GroupID.ClientID %>.value="";
        }
    </script>
</asp:Content>
