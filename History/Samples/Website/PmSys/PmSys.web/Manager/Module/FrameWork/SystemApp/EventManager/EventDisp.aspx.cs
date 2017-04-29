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

using PmSys.Components;

namespace PmSys.web.Module.PmSys.EventManager
{
    public partial class EventDisp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OnStart();
            }
        }

        private void OnStart()
        {
            int EventID = (int)Common.sink("EventID", MethodType.Get, 255, 1, DataType.Int);
            sys_EventTable et = BusinessFacade.sys_EventDisp(EventID);
            EventID_Txt.Text = et.EventID.ToString();
            E_A_AppName_Txt.Text = et.E_A_AppName;
            E_DateTime_Txt.Text = et.E_DateTime.ToString();
            E_From_Txt.Text = et.E_From;
            E_IP_Txt.Text = Common.GetIPLookUrl(et.E_IP);
            E_M_Name_Txt.Text = et.E_M_Name;
            E_Record_Txt.Text = et.E_Record;
            E_Type_Txt.Text = MessageBox.Get_Type(et.E_Type);
            E_U_LoginName_Txt.Text = et.E_U_LoginName;
        }
    }
}
