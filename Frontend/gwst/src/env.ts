class env{
    private static tsroot:string = "../../../ld-76/DotNet/Web_Solutions/Gdc.Web.SPA";
    static ts(path:string):string{
        return `${env.tsroot}/${path}`;
    }
}