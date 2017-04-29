joy.Controls.ListView = {
    init: function (cfg) {
        this.$header = joy.make('ListViewHeader');
        this.$headerarea.appendChild(this.$header);
        this.$header.init(cfg);
        this.$body = joy.make('ListViewBody');
        this.$bodyarea.appendChild(this.$body);
        this.$body.$ListView = this;
        this.$header.$ListView = this;
    },
    setData: function (data) {
        if (data.rows) {
            this.$body.setData(data);
        }
    },
    update:function(){

    },
    tagName: 'div',
    className: 'grid',
    $:
	[
		{ tagName: 'div', alias: 'titlearea', className: 'title' },
        { tagName: 'div', alias: 'headerarea', className: 'caption' },
        { tagName: 'div', alias: 'bodyarea', className: 'body' },
        { tagName: 'div', alias: 'footarea', className: 'foot', $: 'footer' }
	]
};
joy.Controls.ListViewHeader = {
    tagName: 'table',
    init: function (cfg) {

    },
    setData: function (data) {

    },
    update: function () {

    },
    $:
        [
            { tagName: 'tbody', alias: 'body' }
        ]
};
joy.Controls.ListViewBody = {
    tagName: 'table',
    init: function (cfg) {

    },
    setData: function (data) {

    },
    update: function () {

    }
};
joy.Controls.ListViewRow = {
    tagName: 'tr',
    init: function (cfg) {

    },
    setData: function (data) {

    },
    update: function () {

    }
};
