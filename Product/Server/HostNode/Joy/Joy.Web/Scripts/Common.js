function Task(par, method, callback, sender) {
    var o = {
        isCompleted: false,
        rlt: null,
        execute: function (host) {
            this.rlt = method(par, host);
            this.isCompleted = true;
            if (callback) {
                callback(this, sender);
            }
        }
    };
    return o;
}
function Queue() {
    var n = 0;
    var q = new Array();
    q.add = function (o) {
        q[q.length] = o;
    };
    q.addTask = function (par, method, init) {
        n++;
        var t = Task(par, method, function (task, sender) {
            //for (var i = 0; i < sender.length; i++) {
            //    if (!sender[i].isCompleted) {
            //        return;
            //    }
            //}
            n--;
            if (n == 0 && sender.oncompleted) {
                sender.oncompleted();
            }
        }, this);
        this.add(t);
        if (init) {
            init(par);
        }
        t.index = this.length - 1;
    };
    q.dequeue = function (size) {
        if (this.length < 1) {
            return null;
        }
        if (!size) {
            size = this.length;
        }
        var x = null;
        for (var i = 0; i < size; i++) {
            x = this[0];
            this.splice(0, 1);
            x.execute(this);
        }
        return x;
    };
    return q;
}