using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;

using PmSys;
using PmSys.Components;


namespace PmSys.web
{
    public partial class Messages : System.Web.UI.Page
    {
        public string ReturnScript = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string CMD =(string) Common.sink("CMD", MethodType.Get, 255, 0, DataType.Str);
            if (!Page.IsPostBack)
            {
                MessageBox MBx = new MessageBox();
                ArrayList lst = new ArrayList();
                if (CMD == "AppError")
                {
                    List<sys_NavigationUrl> ListUrl = new List<sys_NavigationUrl>();
                    ListUrl.Add(new sys_NavigationUrl("确定", "default.aspx", "", UrlType.Href, true));

                    MBx.M_Body = "抱歉，处理您的请求时发生了错误。错误信息已被记录，我们将追踪解决。";
                    MBx.M_Title = "系统错误";
                    MBx.M_IconType = Icon_Type.Error;
                    MBx.M_ButtonList = ListUrl;                    
                }
                else
                {
                    string CookiesKey = string.Format("{0}-MessageValue", Common.Get_CookiesName);
                    //if (Session[string.Format("{0}-MessageValue", Common.Get_CookiesName)] != null)

                    //if (FrameWorkCache.Instance()[CookiesKey]!=null)
                    //{
                        string MessageValue = Request.Cookies[CookiesKey].Value.ToString();
                        //Response.Write(MessageValue);
                        //Response.Write("<br>-------------------------<br>");
                        //Response.Write(Request.Cookies[CookiesKey+"1"].Value.ToString());
                        if (MessageValue != "")
                        {
                            MBx = EventMessage.Deserialize_MessageBox(EventMessage.Deserialize_MessageBox(MessageValue,true));
                        }
                        //MBx = (MessageBox)Session[string.Format("{0}-MessageValue", Common.Get_CookiesName)];
                        //MBx = (MessageBox)FrameWorkCache.Instance()[CookiesKey];

                    //}
                }
                ReturnScript = MBx.M_ReturnScript;
                Header.Title = MBx.M_Title;
                lst.Add(MBx);
                MessageDispBox.DataSource = lst;
                MessageDispBox.DataBind();
            }
        }

        /// <summary>
        /// 根据icon类型返回图片名称
        /// </summary>
        /// <param name="I_type"></param>
        /// <returns></returns>
        public string Get_M_IconType(Icon_Type I_type)
        { 
            switch (I_type)
            {
                case Icon_Type.Error:
                    return "MessageError.gif";
                case Icon_Type.Alert:
                    return "MessageAlert.gif";
                case Icon_Type.OK:
                    return "MessageOk.gif";
            }
            return "MessageAlert.gif";
        }

        public string Get_M_ButtonList(List<sys_NavigationUrl> M_ButtonList)
        {
            string DefaultSelectButtonName = "";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < M_ButtonList.Count; i++)
            {
                string NameKey = string.Format("Message_Button{0}",i);
                string url = "";
                if (M_ButtonList[i].btnDefaultSelect)
                    DefaultSelectButtonName = NameKey;
                switch (M_ButtonList[i].btnType)
                {
                    case UrlType.Href:
                        url = string.Format("JavaScript:window.location.href='{0}';", ResolveClientUrl(M_ButtonList[i].btnUrl));
                        break;
                    case UrlType.JavaScript:
                        url = string.Format("JavaScript:{0}", M_ButtonList[i].btnUrl);
                        break;
                    case UrlType.VBScript:
                        url = string.Format("VBScript:{0}", M_ButtonList[i].btnUrl);
                        break;
                }
                sb.AppendFormat("<input type='button' ID='{3}' class='button_bak' onClick=\"{0}\" value='{1}' title='{2}'> ", url, M_ButtonList[i].btnText, M_ButtonList[i].btnHint,NameKey);
            }

            if (DefaultSelectButtonName != "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "kk", string.Format("document.getElementById('{0}').focus();", DefaultSelectButtonName), true);
            }
            return sb.ToString();
        }
    }
}
