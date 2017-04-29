<%@ page language="C#" autoeventwireup="true" inherits="sysadmin_edit, App_Web_puqoct7y" validaterequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="css/style.css" />
    <link rel="Stylesheet" type="text/css" href="css/sub.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.alerts.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.ui.all.css" />
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/jquery.alerts.js"></script>
    <script type="text/javascript" src="js/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="js/jquery.datepicker.1.8.js"></script>
    <script type="text/javascript" src="js/jquery.ui.core.js"></script>
    <script type="text/javascript" src="js/jquery.ui.widget.js"></script>
    <script src="kindeditor/kindeditor.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function() {
            var endyear = new Date().getYear();
		    <%ShowDateJavaScript(); %>
	    });
	    
	    <%ShowRichEditor(); %>
	</script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div class="bodydiv">
        <div class="map">
            当前位置 >> 信息编辑
        </div>
        <div class="content">
            <fieldset style="width:800px;">
                <legend>信息编辑</legend>
                <table cellpadding="0" cellspacing="0" style="width:100%; text-align:left;">
                    <tr>
                        <td style="width:80px;"></td>
                        <td></td>
                    </tr>
                    <%ShowEditTable(); %>
                    <tr style="height:30px;">
                        <td></td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="提交内容" CssClass="btn" OnClick="btnSubmit_Click" OnClientClick="return chkData()" />
                        </td>
                    </tr>
                </table>
                <script type="text/javascript">
                    function chkData()
                    {
                        <%ResponseCheckJavaScrip(); %>
                        return true;
                    }
                </script>
            </fieldset>
        </div>
    </div>
    </form>
</body>
</html>