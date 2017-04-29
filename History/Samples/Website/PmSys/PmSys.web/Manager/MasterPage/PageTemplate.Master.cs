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
namespace PmSys.web.MasterPage
{
    public partial class PageTemplate : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            // Define an HtmlLink control.
            HtmlLink TableLinkCss = new HtmlLink();
            TableLinkCss.Href = Page.ResolveUrl("~/Manager/css/table/" + Common.TableSink + "/Css.css");
            TableLinkCss.Attributes.Add("rel", "stylesheet");
            TableLinkCss.Attributes.Add("type", "text/css");

            // Add the HtmlLink to the Head section of the page.
            Page.Header.Controls.Add(TableLinkCss);



            HtmlGenericControl CheckLinkJs = new HtmlGenericControl("script");

            CheckLinkJs.Attributes.Add("type", "text/javascript");
            CheckLinkJs.Attributes.Add("src", Page.ResolveUrl("~/Manager/js/checkform.js"));
            CheckLinkJs.Attributes.Add("charset", "utf-8");
            Page.Header.Controls.Add(CheckLinkJs);



            HtmlGenericControl dateLinkJs = new HtmlGenericControl("script");
            dateLinkJs.Attributes.Add("src", Page.ResolveUrl("~/Manager/js/date/date.js"));
            dateLinkJs.Attributes.Add("type", "text/javascript");
            dateLinkJs.Attributes.Add("charset", "utf-8");
            Page.Header.Controls.Add(dateLinkJs);

            Page.Header.Controls[2].Visible = false;


        }
    }
}
