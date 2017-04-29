interface String{
	startsWith(str:string);
	format(...restArgs:any[]);
}

String.prototype.startsWith = function(str:string):boolean{
	return this.indexOf(str)==0;
}
String.prototype.format = function () {
	var args = arguments;
	var s = this;
	if (!args || args.length < 1) {
		return s;
	}
	var r = s;
	for (var i = 0; i < args.length; i++) {
		var reg = new RegExp('\\{' + i + '\\}');
		r = r.replace(reg, args[i]);
	}
	return r;
};