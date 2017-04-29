joy.auth = function (url, uname, upwd) {

    function preAuthBack(s, c, t) {
        url = '/auth/Auth';
        var secPar = joy.getCookie(joy.cs);
        //alert(secPar + '\r\n' + s.ctoken);
        try {
            joy('txt').value = 'Original Client Token:\t\t\t' + s.ctoken;
            joy('txt').value += '\r\nClient Token Encrypted Par:\t\t\t' + secPar;
            var dsecPar = joy.decrypt(secPar, s.ctoken);
            //alert(dsecPar);
            var cs = joy.getCookie(joy.cs);
            //alert(cs);
            var dcs = joy.decrypt(cs, s.ctoken);
            //alert(dcs);
            var chsum = joy.encrypt(s.ctoken, dcs);
            joy('txt').value += '\r\nClient TK Decrypted Checksum:\t\t\t' + dcs;
            joy('txt').value += '\r\nPar Encrypted ClientChecksum:\t\t\t' + chsum;
            joy.setCookie(joy.ccs, chsum);
            joy.request.send(url, '', function(s, c, t) {
                window.setTimeout(function() {
                    authBack(s, c, t);
                }, 500);
            }, s);
        } catch(e) {
            alert('Error: '+e);
        }

    }

    function authBack(s, c, t) {
        var r = joy.json(c);
        if (!r.IsAllSuccessful) {
            alert(c);
        }
    }
    if (!url) {
        joy.delCookie(joy.tk);
        joy.delCookie(joy.cs);
        joy.delCookie(joy.ccs);

        return;
    }
    var tmp = Math.floor(Math.random() * 65536241637) + '';
    var secret = joy.encrypt(tmp, upwd);
    joy('box').style.width = '800px';
    joy('box').value = secret;
    joy.setCookie(joy.tk, secret);
    var sender = { ctoken: tmp };
    joy.request.send(url + '/' + uname, 'sec=0', function (s, c, t) {// + secret, function (s, c, t) {
        window.setTimeout(function () {
            preAuthBack(s, c, t);
        }, 500);
    }, sender);
};
