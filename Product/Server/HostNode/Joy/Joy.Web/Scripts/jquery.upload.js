(function($) {
    var fileEls = [];
    //var req = new XMLHttpRequest();
    $.userControls.Uploader = {
        tag: 'form',
        className: 'uploadform',
        method: 'post',
        enctype: 'multipart/form-data',
        init: function() {
            this.$inputfile.name = this.baseid;
            this.$inputfile.id = this.baseid;
        },
        val: function(data) {

        },
        clearFiles: function() {
            this.$progresses.innerHTML = '';

        },
        addFile: function() {
            var id = this.baseid + '_' + fileEls.length;
            var ui = $.use('UploadItem');
            ui.$frm = this;
            ui.id = '_' + $.uid;
            ui.name = ui.id;
            fileEls.push(ui);
            this.$progresses.appendChild(ui);
            return ui;
        },
        $: [{
                tag: 'a',
                onclick: function() {
                    $(this.$root.$inputfile).click();
                },
                $: 'Upload'
            }, {
                tag: 'input',
                alias: 'inputfile',
                multiple: true,
                style: {
                    visibility: 'hidden'
                },
                onchange: function() {
                    var item = this;

                    var files = item.files;
                    var paths = item.value.split(', ');
                    item.$root.clearFiles();
                    var q = Queue();
                    for (var i = 0; i < files.length; i++) {
                        var o = {};
                        o.file = files[i];
                        o.path = paths[i];
                        o.root = this.$root;
                        var m = function(par, queue) {
                            var fd = new FormData();
                            var url = par.root.action;
                            var ui = par.ui;
                            fd.append(par.path, par.file);
                            try {
                                var req = ui.req;
                                req.onload = function(e) {
                                    return queue.dequeue(1);
                                };
                                req.open('POST', url, true);
                                req.send(fd);
                            } catch(e) {
                                debugger;
                                return false;
                            }
                        };
                        var u = function(par, queue) {
                            var ui = par.root.addFile();
                            par.ui = ui;
                        };
                        q.addTask(o, m, u);
                    }
                    q.dequeue(item.$root.maxthreads);
                },
                type: 'file'
            }, { tag: 'div', alias: 'progresses', className: 'progresses' }]
    };
    $.userControls.UploadItem = {
        tag: 'div',
        req: null,
        className: 'uploaditem',
        init: function() {
            this.req = new XMLHttpRequest();
            var r = this.req;
            var entity = r.upload;
            if (!entity) {
                entity = r;
                console.log('UE unavailable');
            }
            entity.ui = this;
            entity.onprogress = function(e) {
                this.ui.val(e.loaded, e.total);
            };
            entity.onload = function(e) {
                this.ui.val(100);
            };
        },
        val: function(prg, total) {
            console.log(prg + '/' + total);
            if (total) {
                prg = parseInt(prg / total * 100);
            }
            this.$progress.style.width = prg + '%';
            this.$caption.innerHTML = prg;
        },
        $: [{ alias: 'caption', tag: 'div', className: 'captionarea', $: 'Caption' }, { tag: 'div', className: 'progressarea', $: [{ tag: 'div', alias: 'progress', className: 'bar' }] }]
    };
})(jQuery);