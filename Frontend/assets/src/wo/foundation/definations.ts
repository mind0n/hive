interface Element{
    astyle(props:string[]);
}

interface HTMLElement{
	destroyStatus?:Destroyer;
	dispose?();
}

interface Window{
	opr?:any;
	opera?:any;
	StyleMedia?:any;
	chrome?:any;
	CSS?:any;
}

interface Document{
	documentMode?:any;
}

declare var InstallTrigger:any;





