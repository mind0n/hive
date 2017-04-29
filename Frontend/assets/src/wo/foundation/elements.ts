Element.prototype.astyle = function actualStyle(props) {
	var el = this;
	var compStyle = window.getComputedStyle(el, null);
	for (var i = 0; i < props.length; i++) {
		var style = compStyle[props[i]];
		if (style != null) {
			return style;
		}
	}
	return null;
};

class Destroyer{
	disposing:boolean;
	destroying:boolean;
	static container:HTMLElement = document.createElement("div");
	static destroy(target:HTMLElement){
		if (!target.destroyStatus){
			target.destroyStatus = new Destroyer();
		}
		if (target.dispose && !target.destroyStatus.disposing){
			target.destroyStatus.disposing = true;
			target.dispose();
		}
		if (!target.destroyStatus.destroying){
			target.destroyStatus.destroying = true;
			Destroyer.container.appendChild(target);
			for(let i in target){
				if (i.startsWith('$')){
					var tmp = target[i];
					if (tmp instanceof HTMLElement){
						target[i] = null;
						tmp = null;
					}else{
						delete target[i];
					}
				}
			}
			Destroyer.container.innerHTML = '';
		}
	}
}
