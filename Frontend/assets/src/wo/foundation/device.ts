class MobileDevice{
	static get Android ():boolean {
		var r = navigator.userAgent.match(/Android/i);
		if (r) {
			console.log('match Android');
		}
		return r!= null && r.length>0;
	}
	static get BlackBerry():boolean {
		var r = navigator.userAgent.match(/BlackBerry/i);
		if (r) {
			console.log('match Android');
		}
		return r!=null && r.length > 0;
	}
	static get iOS():boolean {
		var r = navigator.userAgent.match(/iPhone|iPad|iPod/i);
		if (r) {
			console.log('match Android');
		}
		return r != null && r.length > 0;
	}
	static get Opera():boolean {
		var r = navigator.userAgent.match(/Opera Mini/i);
		if (r) {
			console.log('match Android');
		}
		return r != null && r.length > 0;
	}
	static get Windows():boolean {
		var r = navigator.userAgent.match(/IEMobile/i);
		if (r) {
			console.log('match Android');
		}
		return r!= null && r.length >0;
	}
	static get any():boolean {
		return (MobileDevice.Android || MobileDevice.BlackBerry || MobileDevice.iOS || MobileDevice.Opera || MobileDevice.Windows);
	}
}

class Browser{
	// Opera 8.0+
	static get isOpera():boolean{
		return (!!window.opr && !!window.opr.addons) || !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
	}
	
	// Firefox 1.0+
	static get isFirefox():boolean{
		return typeof InstallTrigger !== 'undefined';
	}
	// At least Safari 3+: "[object HTMLElementConstructor]"
	static get isSafari():boolean{
		return Object.prototype.toString.call(HTMLElement).indexOf('Constructor') > 0;
	} 
	// Internet Explorer 6-11
	static get isIE():boolean{
		return /*@cc_on!@*/false || !!document.documentMode;
	}
	// Edge 20+
	static get isEdge():boolean{
		return !Browser.isIE && !!window.StyleMedia;
	}
	// Chrome 1+
	static get isChrome():boolean{
		return !!window.chrome && !!window.chrome.webstore;
	}
	// Blink engine detection
	static get isBlink():boolean{
		return (Browser.isChrome || Browser.isOpera) && !!window.CSS;
	}
}

