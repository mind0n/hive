
function topMenuInit(completed) {
    if (completed) {
        var area = Joy('topmenu');
        var treenode = new Joy.data.treenode(page.catemenugroup, menuCfg);
        var menu = Joy.make('Menu');
        menu.init(treenode);
        menu.setData(treenode);
        menu.showIn(area);
    } else {
        var counter = Joy('counter');
        var d = new Date('2013/10/3');
        var r = Joy.dateDiff(d);
        if (r >= 0) {
            var s = '倒计时 _ 天';
            counter.innerHTML = s.replace(/_/, r);
        } else {
            window.setInterval(function () {
                var now = new Date();
                counter.innerHTML = Joy.dateString(now, 'yyyy-MM-dd HH:mm:ss');
            }, 500);
        }
    }

}
