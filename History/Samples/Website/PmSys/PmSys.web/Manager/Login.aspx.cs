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

using PmSys;
using PmSys.Components;

//Դ������ www.51aspx.com
namespace PmSys.web
{
    public partial class Login : System.Web.UI.Page
    {
        public string FrameName;
        public string FrameNameVer;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //��������û�
                FrameWorkOnline.Instance().ClearOnlineUserTimeOut();
            }

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("default.aspx");
            }
            FrameName = FrameSystemInfo.GetSystemInfoTable.S_Name;
            FrameNameVer = FrameSystemInfo.GetSystemInfoTable.S_Version;

            //Button1.Attributes.Add("Onclick", "javascript:return checkForm(ctl00);");
            string CMD = (string)Common.sink("CMD", MethodType.Get, 255, 0, DataType.Str);
            if (CMD == "OutOnline")
            {
                string U_LoginName = (string)Common.sink("U_LoginName", MethodType.Get, 20, 1, DataType.Str);
                string U_Password = (string)Common.sink("U_Password", MethodType.Get, 32, 32, DataType.Str);
                string OPCode = (string)Common.sink("OPCode", MethodType.Get, 4, 4, DataType.Str);

                MessageBox MBx = new MessageBox();
                MBx.M_Type = 2;
                MBx.M_Title = "ǿ�����ߣ�";
                MBx.M_IconType = Icon_Type.Error;
                MBx.M_Body = "ǿ������ʧ��.��֤����Ч����ȷ�����������֤����Ч��";


                if (Session["CheckCode"] == null || OPCode.ToLower() != Session["CheckCode"].ToString())
                {
                    MBx.M_ButtonList.Add(new sys_NavigationUrl("����", "login.aspx", "�����ť����������֤�룡", UrlType.Href, true));
                }
                else
                {
                    QueryParam qp = new QueryParam();
                    qp.Where = string.Format(" Where U_StatUs=0 and  U_LoginName='{0}' and U_Password='{1}'", Common.inSQL(U_LoginName), Common.inSQL(U_Password));
                    int iInt = 0;
                    ArrayList lst = BusinessFacade.sys_UserList(qp, out iInt);
                    if (iInt > 0)
                    {
                        //FrameWorkPermission.UserOnlineList.RemoveUserName(U_LoginName.ToLower());
                        string sessionid = (string)Common.sink("SessionID", MethodType.Get, 24, 0, DataType.Str);
                        FrameWorkOnline.Instance().OnlineRemove(sessionid);
                        MBx.M_IconType = Icon_Type.OK;
                        MBx.M_Body = string.Format("ǿ���ʺ�{0}���߳ɹ�.�����µ�½��", U_LoginName);
                        MBx.M_ButtonList.Add(new sys_NavigationUrl("����", "login.aspx", "", UrlType.Href, true));
                    }
                    else
                    {
                        MBx.M_Body = "ǿ������ʧ��.�û���/������Ч��";
                        MBx.M_ButtonList.Add(new sys_NavigationUrl("����", "login.aspx", "", UrlType.Href, true));
                    }

                }
                Session["CheckCode"] = Common.RndNum(4);
                EventMessage.MessageBox(MBx);


            }
            
            if (FrameWorkLogin.GetLoginUserError(UserKey) <= 2)
            {
                Logincode_op.Src = "images/Logon/Logon_7no.gif";
                inputcode_op.Visible = false;
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MessageBox MBx = new MessageBox();
            MBx.M_Type = 2;
            MBx.M_Title = "��½����";
            MBx.M_IconType = Icon_Type.Error;
            MBx.M_Body = "��֤����Ч����ȷ�����������֤����Ч��";

            //string UserKey = sLoginName + Common.GetIPAddress().Replace(".", "");


            if (FrameWorkLogin.GetLoginUserError(UserKey) > 2 && (Session["CheckCode"] == null || sCode_op != Session["CheckCode"].ToString()))
            {
                MBx.M_WriteToDB = false;
                MBx.M_ButtonList.Add(new sys_NavigationUrl("����", "login.aspx", "�����ť����������֤�룡", UrlType.Href, true));
            }


            else if (!FrameWorkLogin.CheckDisableLoginUser(UserKey))
            {
                MBx.M_Body = string.Format("���û�:{0},IP:{1}��½�����������ϵͳ����,�Ѿ���ֹ��½.����ϵ����Ա��", sLoginName, Common.GetIPAddress());
                MBx.M_ButtonList.Add(new sys_NavigationUrl("����", "login.aspx", "�����ť���أ�", UrlType.Href, true));
                
            }
            else if (new FrameWorkLogin().CheckLogin(sLoginName, sLoginPass,UserKey))
            {
                MBx.M_IconType = Icon_Type.OK;
                MBx.M_Title = "��½�ɹ���";
                MBx.M_Body = string.Format("��ӭ��{0}���ɹ����롣����IPΪ��{1}��", sLoginName, Common.GetIPAddress());
                MBx.M_WriteToDB = false;
                MBx.M_ButtonList.Add(new sys_NavigationUrl("ȷ��", "default.aspx", "�����ť��½��", UrlType.Href, true));
                FrameWorkLogin.MoveErrorLoginUser(UserKey);
                //д������־
                EventMessage.EventWriteDB(2, MBx.M_Body, UserData.Get_sys_UserTable(sLoginName).UserID);
            }
            else
            {
                MBx.M_Body = string.Format("�û���/����({0}/{1})����", sLoginName, sLoginPass);
                MBx.M_ButtonList.Add(new sys_NavigationUrl("����", "login.aspx", "�����ť�������룡", UrlType.Href, true));
            }
            Session["CheckCode"] = Common.RndNum(4);
            EventMessage.MessageBox(MBx);
        }

        string UserKey
        {
            get
            {
                //return sLoginName + Common.GetIPAddress().Replace(".", "");
                return Common.GetIPAddress().Replace(".", "");
            }
        }
        string sLoginName
        {
            get
            {
                return this.LoginName.Text.Trim();
            }
        }
        string sLoginPass
        {
            get
            {
                return this.LoginPass.Text.Trim();
            }
        }
        string sCode_op
        {
            get
            {
                return this.code_op.Text.Trim();
            }
        }

    }
}
