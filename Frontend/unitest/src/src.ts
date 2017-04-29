class AjaxUtil{
    protected xhr = new XMLHttpRequest();
    protected onsuccess:Function;
    protected onerror:Function;
    
    constructor(success:Function, error:Function){
        this.onsuccess = success;
        this.onerror = error;
        this.xhr.onreadystatechange = function(){
            if (this.xhr.readyState == 4 && this.xhr.status == 200){
                this.onsuccess(this.xhr.responseText);
            }else if (this.xhr.readState == 4){
                this.onerror(this.xhr.responseText);
            }
        }
    }
    
    protected send(type:string, url:string, data?:any, header?:any, isSync?:boolean){
        this.xhr.open(type, url, !isSync);
        if (header){
            for(let i in header){
                this.xhr.setRequestHeader(i, header[i]);
            }
        }
        let args:any = null;
        if (data){
            args = '';
            for(let i in data){
                args += i + '=' + encodeURIComponent(data[i]) + '&';
            }
        }
        this.xhr.send(args);
    }
    
    get(url:string, isSync?:boolean){
        return this.send("GET", url, isSync);
    }

    post(url:string, isSync?:boolean){
        return this.send("POST", url, isSync);
    }
}

export default function ajax(success:Function, error:Function):AjaxUtil{
    let rlt = new AjaxUtil(success, error);
    return rlt;
}