Joy.controls.DropDown = {
	tagName: 'div'
		, className: 'dropdown'
		, style: { width: '100%', height: '100%', overflow: 'hidden', position:'relative' }
		, showMenu: function (visible) {
			var menu = this.$menu;
			menu.show(this, visible);
		}
		, setData: function (data) {
			this.$menu.setData(data);
		}
		, select: function (item) {
			this.$textbox.innerHTML = item.text;
			this.value = item.value;
		}
		, getData: function () {
			this.$textbox.innerHTML = this.$menu.text;
			return this.$menu.value;
		}
		, onmade: function () {
			var m = {
				controlName: 'SimpleMenu'
			};
			var mel = Joy.make(m);
			mel.$root = this;
			this.$menu = mel;
			document.body.appendChild(mel);
		}
		, $:
		[
			{
				tagName: 'div', className:'dropdown_container', style: { width: '100%', height:'100%', overflow:'hidden', position: 'relative' },
				$:
				[
					{ tagName: 'div', className:'dropdown_box', alias: 'textbox', style: { cursor:'default', overflow:'hidden', width: '100%', height:'100%'},
						onmouseup: function (evt) {
							this.$root.showMenu(true);
						}
					},
					{ tagName: 'div', className:'dropdown_button', style: { cursor:'default', font:'11px verdana',  width: '12px', height: '100%', position: 'absolute', right: '0px', top: '0px', background:'white', borderLeft:'solid 1px gray', textAlign:'center' }, innerHTML: 'v', 
						onmouseup: function (evt) {
							this.$root.showMenu(true);
						}
					}
				]
			}
	//			{ tagName: 'div', style: { width: '100%', tableLayout: 'auto', cursor: 'default' }, $: [{ tagName: 'tbody', $: [{ tagName: 'tr', $:
	//				[
	//					{
	//						tagName: 'td',
	//						style: { cursor: 'default', width: '12px', font: '10px arial', fontWeight: 'bolder', textAlign: 'center' },
	//						innerHTML: 'v',
	//						onmouseup: function (evt) {
	//							this.$root.showMenu(true);
	//						}
	//					},
	//					{
	//						tagName: 'td',
	//						//alias: 'textbox',
	//						style: { overflow: 'hidden', whiteSpace: 'nowrap', display: 'block' },
	//						onmouseup: function (evt) {
	//							this.$root.showMenu(true);
	//						},
	//						$:
	//						[
	//							{ tagName: 'span', alias: 'textbox',
	//								style: { overflow: 'hidden', display: 'block', whiteSpace: 'nowrap'}
	//							}
	//						]
	//					}
	//					 
	//				]
	//			}]
	//			}]
	//			}
		]
};