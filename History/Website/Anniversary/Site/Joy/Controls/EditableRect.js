Joy.controls.EditableRect = {
	type: 'EditableRect'
	, editor: 'editbox'
	, tagName: 'div'
	, className: 'editablerect'
	, style: {
		fontSize: '12px'
		, padding: '0px'
		, margin: '0px'
		, fontFamily: 'arial'
        , position: 'relative'
		, overflow: 'hidden'
	}
	, mode: {
		readonly: false
		, editorview: false
		, isediting: false
        , clickedit: false
	}
    , getData: function () {
    	return this.getEditor().getData();
    }
    , setData: function (data) {
    	this.$data = data;
    	this.$label.setData(data);
    }
	, onmouseup: function (evt) {
		if (this.mode.clickedit) {
			this.edit();
		}
	}
    , edit: function () {
    	if (!this.mode || !this.mode.isediting) {
    		if (!this.mode) {
    			this.mode = {};
    		}
    		this.setMode({ isediting: true });
    	}
    }
    , reject: function () {
    	if (this.mode.isediting) {
    		var el = this.getEditor();
    		el.setData(this.$data);
    		this.setMode({ isediting: false });
    	}
    }
    , accept: function () {
    	if (this.mode.isediting) {
    		var el = this.getEditor();
    		el.acceptData(this.$data);
    		this.setMode({ isediting: false });
    	}
    }
    , getEditor: function () {
    	return this['$' + this.editor];
    }
	, setMode: function (mode) {
		Joy.merge(this.mode, mode);
		var editorEl = this['$' + this.editor];
		if (this.mode.isediting) {
			this.$label.style.display = 'none';
			editorEl.disabled = this.mode.readonly;
			editorEl.style.display = '';
			editorEl.setData(this.$data);
		} else {
			this.$label.style.display = '';
			editorEl.style.display = 'none';
			this.$label.setData(this.$data);
		}
	}
	,
	$: [
		{
			controlName: 'DropDown'
			, alias: 'dropdown'
			, style: {
				display: 'none'
			}
			, acceptData: function (data) {
				data.text = this.text;
				data.value = this.value;
			}
		}
		, {
			tagName: 'input'
			, alias: 'editbox'
			, className: 'editbox'
			, style: {
				display: 'none'
			}
			, onfocus: function (evt) {
				this.select();
			}
			, setData: function (data) {
				var value = '';
				if (data) {
					value = (typeof (data) == 'object') ? data.value : data;
				}
				this.value = value;
			}
			, getData: function () {
				return this.value;
			}
            , acceptData: function (data) {
            	data.value = this.value;
            }
		}
		, {
			tagName: 'input'
			, alias: 'checkbox'
			, className: 'checkbox'
			, style: {
				display: 'none'
				, width: '98%'
				, height: '100%'
			}
			, type: 'checkbox'
			, setData: function (data) {
				var value = '';
				if (data) {
					value = (typeof (data) == 'object') ? data.value : data;
				}
				this.value = value;
			}
			, getData: function () {
				return this.value;
			}
            , acceptData: function (data) {
            	data.value = this.value;
            }
		}
		, {
			tagName: 'div'
			, alias: 'label'
			, className: 'label'
			, style: {
				display: ''
			}
			, setData: function (data) {
				var cfg = this.$root.$;
				var value = '';
				if (data) {
					value = (typeof (data) == 'object') ? data.value : data;
				}
				if (cfg && cfg.url && this.$root.$row) {
					if (data) {
						this.innerHTML = '<a href="' + cfg.url.replace(/\{0\}/ig, this.$root.$row.pkv) + '" >' + value + "</a>";
					} else {
						this.innerHTML = '';
					}
				} else {
					this.innerHTML = value;
				}
			}
			, getData: function () {
				return this.innerHTML;
			}
		}
	]
};