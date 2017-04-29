using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PmSys.Web.Manager.Module.FrameWork
{
    public class PageBase : System.Web.UI.Page
    {
       public string S_ID = (string)Common.sink("S_ID", MethodType.Get, 255, 0, DataType.Str);
       public string CMD = (string)Common.sink("CMD", MethodType.Get, 50, 0, DataType.Str);
       public string CMD_Txt = "查看";
       public string App_Txt = "应用";
       public string All_Title_Txt = "";

        public PageBase()
        {
            
        }
        #region 重载方法

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!IsPostBack)
            {
                FrameWorkPermission.CheckPagePermission(CMD);
            }
            base.OnLoad(e);
        }
        #endregion
    }
}