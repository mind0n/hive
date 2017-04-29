page.categridgroup = 
    { 
        defaultEditor:"editbox",
        defaultStyle:{ 
            width:"70px",display:"none" 
        }, 
        styles:{ 
            caption:{ 
                display:"" 
            },
            articleupdate:{ 
                display:"" 
            } 
        },
        cols:
            [ 
                { field:"articleid",caption:"articleid",type:"Int32",datatype:"INTEGER" },
                { field:"categoryid",caption:"categoryid",type:"Int32",datatype:"INTEGER" },
                { field:"userid",caption:"userid",type:"Int32",datatype:"INTEGER" },
                { field:"caption",caption:"caption",type:"String",datatype:"VARCHAR",url:"content.aspx?aid={0}",pk:"articleid" },
                { field:"link",caption:"link",type:"String",datatype:"VARCHAR" },
                { field:"articleupdate",caption:"articleupdate",type:"DateTime",datatype:"DATETIME" },
                { field:"tag",caption:"tag",type:"String",datatype:"LONGCHAR" },
                { field:"categoryname",caption:"categoryname",type:"String",datatype:"VARCHAR" },
                { field:"container",caption:"container",type:"String",datatype:"VARCHAR" },
                { field:"morder",caption:"morder",type:"Int32",datatype:"INTEGER" },
                { field:"pid",caption:"pid",type:"String",datatype:"VARCHAR" },
                { field:"widgetname",caption:"widgetname",type:"String",datatype:"VARCHAR" },
                { field:"widgetsettings",caption:"widgetsettings",type:"String",datatype:"LONGCHAR" },
                { field:"clientvisible",caption:"clientvisible",type:"Boolean",datatype:"BIT" } 
            ],
        hideCaptions:"True",
        hideFooter:"True",
        dataset:{ 
            通知公告:{ 
                container:"leftpanel",
                categoryname:"通知公告",
                categoryid:2,
                field:"categoryid",
                url:"content.aspx?cid=",
                widgetname:"Grid",
                widgetsettings:"{}",
                rows:
                    [ 
                        { articleid:8,categoryid:2,userid:1,caption:"article9",link:{  },articleupdate:"2012/06/25",tag:{  },categoryname:"通知公告",container:"leftpanel",morder:1,pid:"index_aspx",widgetname:"Grid",widgetsettings:"{}",clientvisible:"True" },
                        { articleid:6,categoryid:2,userid:1,caption:"article6",link:{  },articleupdate:"2012/06/24",tag:{  },categoryname:"通知公告",container:"leftpanel",morder:1,pid:"index_aspx",widgetname:"Grid",widgetsettings:"{}",clientvisible:"True" },
                        { articleid:5,categoryid:2,userid:1,caption:"article5",link:{  },articleupdate:"2012/06/24",tag:{  },categoryname:"通知公告",container:"leftpanel",morder:1,pid:"index_aspx",widgetname:"Grid",widgetsettings:"{}",clientvisible:"True" },
                        { articleid:4,categoryid:2,userid:1,caption:"article4",link:{  },articleupdate:"2012/06/24",tag:{  },categoryname:"通知公告",container:"leftpanel",morder:1,pid:"index_aspx",widgetname:"Grid",widgetsettings:"{}",clientvisible:"True" },
                        { articleid:12,categoryid:2,userid:1,caption:"article12",link:{  },articleupdate:"2012/06/24",tag:{  },categoryname:"通知公告",container:"leftpanel",morder:1,pid:"index_aspx",widgetname:"Grid",widgetsettings:"{}",clientvisible:"True" } 
                    ] 
            },
            图片新闻:{ 
                container:"topimg",
                categoryname:"图片新闻",
                categoryid:17,
                field:"categoryid",
                url:"content.aspx?cid=",
                widgetname:"AdSwitcher",
                widgetsettings:"{hidetitle:true}",
                rows:
                    [ 
                        { articleid:38,categoryid:17,userid:1,caption:"图片新闻1",link:{  },articleupdate:"2012/07/14",tag:"Themes/Default/Images/newsimg1.jpg",categoryname:"图片新闻",container:"topimg",morder:1,pid:"index_aspx",widgetname:"AdSwitcher",widgetsettings:"{hidetitle:true}",clientvisible:"True" },
                        { articleid:39,categoryid:17,userid:1,caption:"图片新闻2",link:{  },articleupdate:"2012/07/14",tag:"Themes/Default/Images/newsimg2.jpg",categoryname:"图片新闻",container:"topimg",morder:1,pid:"index_aspx",widgetname:"AdSwitcher",widgetsettings:"{hidetitle:true}",clientvisible:"True" } 
                    ] 
            },
            校庆新闻:{ 
                container:"leftpanel",
                categoryname:"校庆新闻",
                categoryid:1,
                field:"categoryid",
                url:"content.aspx?cid=",
                widgetname:"Grid",
                widgetsettings:"{autoHide:true}",
                rows:
                    [ 
                        { articleid:47,categoryid:1,userid:1,caption:"article1 - the name of article 1 is really very long to test the index grid layout",link:"",articleupdate:"2012/07/29",tag:"",categoryname:"校庆新闻",container:"leftpanel",morder:2,pid:"index_aspx",widgetname:"Grid",widgetsettings:"{autoHide:true}",clientvisible:"True" },
                        { articleid:3,categoryid:1,userid:1,caption:"article3",link:{  },articleupdate:"2012/06/24",tag:{  },categoryname:"校庆新闻",container:"leftpanel",morder:2,pid:"index_aspx",widgetname:"Grid",widgetsettings:"{autoHide:true}",clientvisible:"True" },
                        { articleid:7,categoryid:1,userid:1,caption:"ok",link:{  },articleupdate:"2012/06/24",tag:{  },categoryname:"校庆新闻",container:"leftpanel",morder:2,pid:"index_aspx",widgetname:"Grid",widgetsettings:"{autoHide:true}",clientvisible:"True" },
                        { articleid:9,categoryid:1,userid:1,caption:"article8",link:{  },articleupdate:"2012/06/24",tag:{  },categoryname:"校庆新闻",container:"leftpanel",morder:2,pid:"index_aspx",widgetname:"Grid",widgetsettings:"{autoHide:true}",clientvisible:"True" },
                        { articleid:11,categoryid:1,userid:1,caption:"article11",link:{  },articleupdate:"2012/06/24",tag:{  },categoryname:"校庆新闻",container:"leftpanel",morder:2,pid:"index_aspx",widgetname:"Grid",widgetsettings:"{autoHide:true}",clientvisible:"True" } 
                    ] 
            },
            友情链接:{ 
                container:"rightpanel",
                categoryname:"友情链接",
                categoryid:14,
                field:"categoryid",
                url:"content.aspx?cid=",
                widgetname:"ImgList",
                widgetsettings:"{}",
                rows:
                    [ 
                        { articleid:13,categoryid:14,userid:1,caption:"机械学院",link:"http://jixie.sdju.edu.cn/",articleupdate:"2012/06/30",tag:"Themes/Default/Images/il_jxxy.jpg",categoryname:"友情链接",container:"rightpanel",morder:3,pid:"index_aspx",widgetname:"ImgList",widgetsettings:"{}",clientvisible:"True" } 
                    ] 
            },
            光影电机:{ 
                container:"leftpanel",
                categoryname:"光影电机",
                categoryid:7,
                field:"categoryid",
                url:"content.aspx?cid=",
                widgetname:"AdSwitcher",
                widgetsettings:"{player:'VideoPlayer',isImgBtn:true,disableAutoPlay:true}",
                rows:
                    [ 
                        { articleid:40,categoryid:7,userid:1,caption:"视频资料2",link:"http://player.youku.com/player.php/sid/XNDI3NjQzNTg4/v.swf",articleupdate:"2012/07/15",tag:"Themes/Default/Images/videoicon2.jpg",categoryname:"光影电机",container:"leftpanel",morder:4,pid:"index_aspx",widgetname:"AdSwitcher",widgetsettings:"{player:'VideoPlayer',isImgBtn:true,disableAutoPlay:true}",clientvisible:"True" },
                        { articleid:14,categoryid:7,userid:1,caption:"视频资料1",link:"http://player.youku.com/player.php/sid/XNDIyMTUzODQ0/v.swf",articleupdate:"2012/07/02",tag:"Themes/Default/Images/videoicon1.jpg",categoryname:"光影电机",container:"leftpanel",morder:4,pid:"index_aspx",widgetname:"AdSwitcher",widgetsettings:"{player:'VideoPlayer',isImgBtn:true,disableAutoPlay:true}",clientvisible:"True" } 
                    ] 
            } 
        } 
    };
page.catemenugroup = 
    [ 
        { CategoryId:1,Caption:"校庆新闻",ParentId:0,Visible:"True" },
        { CategoryId:3,Caption:"院情院史",ParentId:0,Visible:"True" },
        { CategoryId:4,Caption:"办学成果",ParentId:0,Visible:"True" },
        { CategoryId:5,Caption:"校友风采",ParentId:0,Visible:"True" },
        { CategoryId:7,Caption:"光影电机",ParentId:0,Visible:"True" },
        { CategoryId:6,Caption:"祝福寄语",ParentId:0,Visible:"True" },
        { CategoryId:9,Caption:"教授风采",ParentId:4,Visible:"True" },
        { CategoryId:10,Caption:"教学成果",ParentId:4,Visible:"True" },
        { CategoryId:11,Caption:"科研成果",ParentId:4,Visible:"True" },
        { CategoryId:12,Caption:"育人成果",ParentId:4,Visible:"True" },
        { CategoryId:13,Caption:"大学生科创",ParentId:4,Visible:"True" } 
    ] 
