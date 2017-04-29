Joy.Controls.ListView = {
    init: function (cfg) {
        this.$header = Joy.make('ListViewHeader');
        this.$headerarea.appendChild(this.$header);
        this.$header.init(cfg);
        this.$body = Joy.make('ListViewBody');
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
Joy.Controls.ListViewHeader = {
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
Joy.Controls.ListViewBody = {
    tagName: 'table',
    init: function (cfg) {

    },
    setData: function (data) {

    },
    update: function () {

    }
};
Joy.Controls.ListViewRow = {
    tagName: 'tr',
    init: function (cfg) {

    },
    setData: function (data) {

    },
    update: function () {

    }
};
