// JScript 文件

/**
* @deprecated 定义窗口高、宽的全局变量
* @author zdz
*/
    var winWidth = 0;
    var winHeight = 0;
/**
* @deprecated 定义获取窗口高宽的空函数
* 
*/
var getWinSize = function(){};

/**
* @deprecated 获取窗口宽度
* 
*/
getWinSize.winWidth = function()
{
    if (window.innerWidth)//for Firefox
    {
       winWidth = window.innerWidth;
    }
    else if((document.body) && (document.body.clientWidth))
    {
       winWidth = document.body.clientWidth;
    }

    //通过深入Document内部对body进行检测，获取窗口大小
    if (document.documentElement && document.documentElement.clientWidth)
    {
       winWidth = document.documentElement.clientWidth;
    }

    return winWidth;
}

/**
* @deprecated 获取窗口高度
* 
*/
getWinSize.winHeight = function()
{
    if (window.innerHeight)//for Firefox
    {
       winHeight = window.innerHeight;
    }
    else if((document.body) && (document.body.clientHeight))
    {
       winHeight = document.body.clientHeight;
    }

    //通过深入Document内部对body进行检测，获取窗口大小
    if (document.documentElement && document.documentElement.clientHeight)
    {
       winHeight = document.documentElement.clientHeight;
       if(window.opera) winHeight = document.body.offsetHeight - 2;  //ziyou修改
    }
    return winHeight;
}

/**
* @deprecated 窗口高宽的设值函数，根据实际业务修改
* @return winWidth winHeight
* 
*/
getWinSize.onresize =function()
{
    var w = getWinSize.winWidth();
    var h = getWinSize.winHeight();
    
    //var leftdiv = document.getElementById("lmenu");
    //var rightdiv = document.getElementById("rfrm");
    
    $("#lmenu").height(h - 80/* - (navigator.userAgent.indexOf("Firefox") > 0 ? 80 : 0)*/);
    $("#rfrm").height(h - 80);
    $("#rfrm").width((w - 160));
    if(w < 1197){
        $("div[class='hm']").attr("class", "hms");
    }
    else {
        $("div[class='hms']").attr("class", "hm");
    }
}

/**
* @deprecated 用window.onload在页面加载的时候加载,window.onresize监听窗口高宽的变化
* 
*/
window.onload = function () 
{ 
    getWinSize.onresize();
    window.onresize = getWinSize.onresize;
}