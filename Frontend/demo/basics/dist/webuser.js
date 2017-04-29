"use strict";
var Demo;
(function (Demo) {
    var WebUser = (function () {
        function WebUser(userId, pwd) {
            this.UserId = userId;
            this.Pwd = pwd;
        }
        return WebUser;
    }());
    Demo.WebUser = WebUser;
})(Demo = exports.Demo || (exports.Demo = {}));
//# sourceMappingURL=webuser.js.map