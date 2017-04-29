<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Storage/Local/WebFormUI.Master" AutoEventWireup="true" CodeBehind="LocalStorageManager.aspx.cs" Inherits="Portal.Views.Storage.Local.LocalStorageManager" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .main{
        position: absolute;
        top: 24px;
        left: 0;
        right: 0;
        bottom: 0;
    }
    .main .category {
        position: absolute;
        left: 0;
        top: 0;
        width: 150px;
        bottom: 0;
        border-right: solid 1px #f3f3f3;
    }
    .main .category #addtblarea{
        position: absolute;
        top: 2px;
        left: 0;
        right: 0;
        height: 24px;
        line-height: 24px;
        background: white;
        border: solid 1px #f3f3f3;
    }
    .main .category #addtblarea input {
        background: white;
    }
    .main .category #tblist {
        position: absolute;
        top: 26px;
        left: 0;
        right: 0;
        bottom: 0;
    }
    .main .category #tblist canvas {
    }
    .main .category #tblist #tblist_area {
        position: absolute;
        top: 12px;
        left: 12px;
        right: 12px;
        bottom: 4px;
    }
    .main .category #tblist .grid .caption {
        
    }
    .main .category #tblist .grid .caption .header_cell .header_cell_content {
        line-height: 24px;
        font: 14px arial;
        font-weight: bold;
    }
    .main .category #tblist .grid .body .body_table .Name .label {
        line-height: 18px;
    }
    .main .category #tblist .grid .body .body_table {
        width: 100%;
    }
    .main .category #tblist .grid .body .body_table tr {
        background: white;
    }
    .main .category #tblist .grid .body .body_table tr:hover {
        background: #b0e0e6;
    }
    .main .category #tblist .grid .body .body_table tr td {
        cursor: default;
    }
    .main .details {
        position: absolute;
        left: 150px;
        top: 0;
        right: 0;
        bottom: 0;
    }
    .main .details canvas {
        z-index: 10;
	    width: 100%;
	    height: 100%;
    }
    </style>
    <script type="text/javascript">
	    var rcfg = {
		    cols: {
		    	ObjectKey: { type: 'string', hidden: true },
		    	uname: { type: 'string', text: 'Username' },
		    	upwd: { type: 'string', text: 'Password', mask: '*****' }
		    }
	    };
	    
	    var rdata = {
	        rows: [
	            { ObjectKey: 'a', uname: 'admin', upwd: 'pwd' },
	            { ObjectKey: 'b', uname: 'user', upwd: 'pwd2' }
	        ]
	    };

	    var borderWidth = [0, 0, 0, 0];
	    var Cell = {
		    layout: {
			    w: 100,
			    h: 'extend',
			    valign: 'middle',
			    align: 'left',
			    padding: 0,
			    offset:[0, 4, 0, 4]
		    },
		    theme: {
			    color: 'black',
			    backcolor: 'lightblue'
		    },
		     setData: function (data) {
			    this.text = data;
		    },
		    text: 'Hello, world!'
	    };

	    var Header = {
	        display: 'line',
	        hcols: null,
	        layout: {
	            w: 300,
	            h: 28,
	            padding: 0
	        },
	        theme: {
	            bordercolor: 'blue',
	            borderWidth: borderWidth
	        },
	        init: function (s) {
	            if (s.cols) {
	                this.hcols = j2d.ChildBox(this.$root);
	                for (var i in s.cols) {
	                    var col = s.cols[i];
	                    var b = j2d().createBox(Cell, this);
	                    b.$root = this.$root ? this.$root : this;
	                    b.init(col);
	                    if (!col.hidden) {
	                        b.setData(col.text ? col.text : i);
	                        this.$.addBox(b);
	                    } else {
	                        this.hcols.addBox(b);
	                    }
	                }
	            }
	        }
	    };
	    
	    var Row = {
	        display: 'line',
	        hcols: null, //joy.List(),
		    layout: {
			    w: 300,
			    h: 28,
			    padding: 0
		    },
		    theme: {
			    bordercolor: 'blue',
			    borderWidth: borderWidth
		    },
		    setData: function (data) {
		        if (!this.hcols) {
		            this.hcols = j2d.ChildBox(this.$root);
		        }
		        var s = this.$schema;
		        if (s.cols) {
		            for (var i in s.cols) {
		                var col = s.cols[i];
		                var b = j2d().createBox(Cell, this);
		                b.$root = this.$root ? this.$root : this;
		                b.init(col);
		                if (!col.hidden) {
		                    b.setData(data[i]);
		                    this.$.addBox(b);
		                } else {
		                    this.hcols.addBox(b);
		                }
		            }
		        }
		    }
	    };
	    
	    var Rows = {
		    layout: {
			    x: 40,
			    y: 40,
			    w: 350,
			    h: 150,
			    padding:0
		    },
		    theme: {
			    color: 'black',
			    backcolor: 'lightblue'
		    },
		    setData: function (data) {
		        var s = this.$schema;
			    for (var i = 0; i < data.length;i++) {
			        var rd = data[i];
			        var b = j2d().createBox(Row, this);
			        b.$root = this.$root ? this.$root : this;
			        b.init(s);
			        b.setData(rd);
			        this.$.addBox(b);
			    }
		    },
		    init:function(cfg) {
		        this.$schema = cfg;
		        var h = j2d().createBox(Header, this);
		        h.$root = this;
		        h.init(rcfg);
		        this.$.addBox(h);
		    }
	    };

	    function updateTblist() {
		    //var json = service.GetEntry();
		    var json = '{"Users":{"Name":"Users","ObjectKey":"5d77a484-cc5a-4614-940d-e2818aef8a1a"},"Roles":{"Name":"Roles","ObjectKey":"8850914f-f388-483f-9b33-0f3480d21ee8"},"Articles":{"Name":"Articles","ObjectKey":"ab180c21-db40-43f3-b68e-84e2e04d32ac"},"Tags":{"Name":"Tags","ObjectKey":"998fd8ea-4760-48ca-98e2-fd06a9d5bda6"}}';
		    var entry = joy.fromJson(json);
		    var griddata = { rows: [] };
		    for (var i in entry) {
			    griddata.rows[griddata.rows.length] = entry[i];
		    }
		    tbGrid.setData(griddata);
		    tbGrid.update();
		    tbGrid.append(joy('tblist_area'));
	    }

	    function render() {
		    var b = j2d('cvs').createBox(Rows);
		    b.init(rcfg);
		    b.setData(rdata);
		    b.render();
	    }
	    var gridconfig = {
		    hideTitle: true,
		    hideFooter: true,
		    rowcfg: {
			    onmouseup: function (e) {
				    var evt = event || e;
				    var el = evt.srcElement || evt.target;
				    if (e.button == 1) {
					    service.DropGroup(el.innerHTML);
					    updateTblist();
				    } else {
                        
				    }
			    }
		    },
		    cols:
		    [
			    { field: 'Name', caption: 'Groups', type: 'string' }
		    ]
	    };
	    
	    var tbGrid = joy.make('Grid');
	    tbGrid.init(gridconfig);
	    joy(function () {
		    if (external) {
			    try {
				    window.service = external.GetProcessor('storage');
			    } catch (e) {
				    window.service = {};
			    }
		    } else {
			    window.service = {};
		    }
		    var d = joy.make('DropDown');
		    d.init({
			    btnText: '+',
			    $dropbtn: {
				    onmouseup: function () {
					    service.AddGroup(d.getData());
					    updateTblist();
					    d.clearData();
				    }
			    }
		    });
		    d.append(joy('addtblarea'));
		    updateTblist();
		    joy('cvs').resize();
		    render();
		    window.onresize = function() {
			    joy('cvs').resize();
			    render();
		    };
	    });

    </script>
</asp:Content>
<asp:Content ID="caption" ContentPlaceHolderID="caption" runat="server">
    Local Storage Manager
</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="content" runat="server">
    <div class="main">
        <div id="category" class="category">
            <div id="addtblarea"></div>
            <div id="tblist">
                <div id="tblist_area"></div>
            </div>
        </div>
        <div class="details">
            <canvas id="cvs" width="2000" height="2000"></canvas>
        </div>
    </div>
</asp:Content>
