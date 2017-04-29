<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginModule.ascx.cs" Inherits="FSUI.Lib.Server.Ajax.LoginModule" %>


<br /><input id="unamebox" type="text" value="admin" />
<br /><input id="upwdbox" type="password" value="test" />
<br /><img id="codeimg" onmouseover="this.style.cursor='pointer';" onclick="this.src += '#1'" src="" alt="Click to change" /> <input id="codebox" type="text" value="" />
<br /><input id="rmchk" type="checkbox" /><label for="rmchk">Remember me</label>
<br /><input id="signinbtn" type="button" value="Sign in" onclick="bLogin.signIn()" />
<script type="text/javascript">
	var par = {
		processor: '<%=CurtParam.SigninProcessor %>'
		, sredirect: '<%=CurtParam.SuccessRedirect %>'
		, authimgurl: '<%=CurtParam.AuthCodeUrl %>'
		, previousurl: "<%=CurtParam.PreviousUrl %>"
		, elementIds: {
			usernameBox: "unamebox"
			, passwordBox: "upwdbox"
			, authImg: "codeimg"
			, authCodeBox: "codebox"
			, rememberCheckBox: "rmchk"
			, signinBtn: "signinbtn"
		}
	};
	var bLogin = new FC.LoginModule(par);

</script>