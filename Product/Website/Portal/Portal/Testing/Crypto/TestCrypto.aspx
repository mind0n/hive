<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestCrypto.aspx.cs" Inherits="Portal.Testing.Crypto.TestCrypto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Joy/Joy.js"></script>
    <script type="text/javascript" src="/Joy/Network/Request.js"></script>
    <script type="text/javascript" src="/Joy/Security/AES.js"></script>
    <script type="text/javascript" src="/Joy/Security/Auth.js"></script> 
    <script type="text/javascript">
        joy(function () {
            var pb = joy('pwd');
            var ct = joy('ct');
            var ce = joy('ce');
            var st = joy('st');
            var se = joy('se');
            joy('bce').onclick = function () {
                try {
                    ce.value = joy.encrypt(ct.value, pb.value);
                } catch(e) {
                    alert(e);
                }
            };
            joy('bcd').onclick = function () {
                try {
                    ct.value = joy.decrypt(ce.value, pb.value);
                } catch (e) {
                    alert(e);
                }

            };
            joy('bse').onclick = function () {
                var url = '/Testing/Crypto/TestCrypto.aspx';
                var s = joy.encrypt(st.value, pb.value);
                se.value = s;
                joy.request.send(url, 'p=' + pb.value + '&s=' + s, function (s, c, t) {
                    alert(c);
                });
            };
            joy('bsd').onclick = function () {
                var url = '/Testing/Crypto/TestCrypto.aspx';
                joy.request.send(url, 'p=' + pb.value + '&a=e', function (s, c, t) {
                    //alert(c);
                    st.value = joy.decrypt(c, pb.value);
                });
            };
        });
    </script>
    <style type="text/css">
        .auto-style2 {
            height: 12px;
        }
        .auto-style3 {
            height: 5px;
        }
        #ct {
            height: 246px;
            width: 542px;
        }
        #ce {
            height: 245px;
            width: 496px;
        }
        #st {
            height: 160px;
            width: 538px;
        }
        #se {
            height: 158px;
            width: 503px;
        }
        #pwd {
            width: 494px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <input id="pwd" value="pwd" type="text" />
       <table style="height: 456px; width: 853px">
           <tr><td class="auto-style3">Client:</td><td class="auto-style3"></td><td class="auto-style3"></td></tr>
           <tr><td><textarea id="ct"></textarea></td><td><input id="bce" type="button" value="Encrypt"/><br/><input id="bcd" type="button" value="Decrypt"/></td><td><textarea id="ce"></textarea></td></tr>
           <tr><td class="auto-style2">Server</td><td class="auto-style2"></td><td class="auto-style2"></td></tr>
           <tr><td><textarea id="st"></textarea></td><td><input id="bse" type="button" value="Encrypt"/><br/><input id="bsd" type="button" value="Decrypt"/></td><td><textarea id="se"></textarea></td></tr>
       </table>
    </div>
    </form>
</body>
</html>
