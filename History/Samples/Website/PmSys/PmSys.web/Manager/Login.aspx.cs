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

//源码下载 www.51aspx.com
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
                //清除在线用户
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
                MBx.M_Title = "强制下线！";
                MBx.M_IconType = Icon_Type.Error;
                MBx.M_Body = "强制下线失败.验证码无效，请确认您输入的验证码有效！";


                if (Session["CheckCode"] == null || OPCode.ToLower() != Session["CheckCode"].ToString())
                {
                    MBx.M_ButtonList.Add(new sys_NavigationUrl("返回", "login.aspx", "点击按钮重新输入验证码！", UrlType.Href, true));
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
                        MBx.M_Body = string.Format("强制帐号{0}下线成功.请重新登陆！", U_LoginName);
                        MBx.M_ButtonList.Add(new sys_NavigationUrl("返回", "login.aspx", "", UrlType.Href, true));
                    }
                    else
                    {
                        MBx.M_Body = "强制下线失败.用户名/密码无效！";
                        MBx.M_ButtonList.Add(new sys_NavigationUrl("返回", "login.aspx", "", UrlType.Href, true));
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
            MBx.M_Title = "登陆出错！";
            MBx.M_IconType = Icon_Type.Error;
            MBx.M_Body = "验证码无效，请确认您输入的验证码有效！";

            //string UserKey = sLoginName + Common.GetIPAddress().Replace(".", "");


            if (FrameWorkLogin.GetLoginUserError(UserKey) > 2 && (Session["CheckCode"] == null || sCode_op != Session["CheckCode"].ToString()))
            {
                MBx.M_WriteToDB = false;
                MBx.M_ButtonList.Add(new sys_NavigationUrl("返回", "login.aspx", "点击按钮重新输入验证码！", UrlType.Href, true));
            }


            else if (!FrameWorkLogin.CheckDisableLoginUser(UserKey))
            {
                MBx.M_Body = string.Format("此用户:{0},IP:{1}登陆出错次数超过系统允许,已经禁止登陆.请联系管理员！", sLoginName, Common.GetIPAddress());
                MBx.M_ButtonList.Add(new sys_NavigationUrl("返回", "login.aspx", "点击按钮返回！", UrlType.Href, true));
                
            }
            else if (new FrameWorkLogin().CheckLogin(sLoginName, sLoginPass,UserKey))
            {
                MBx.M_IconType = Icon_Type.OK;
                MBx.M_Title = "登陆成功！";
                MBx.M_Body = string.Format("欢迎您{0}，成功登入。您的IP为：{1}！", sLoginName, Common.GetIPAddress());
                MBx.M_WriteToDB = false;
                MBx.M_ButtonList.Add(new sys_NavigationUrl("确定", "default.aspx", "点击按钮登陆！", UrlType.Href, true));
                FrameWorkLogin.MoveErrorLoginUser(UserKey);
                //写登入日志
                EventMessage.EventWriteDB(2, MBx.M_Body, UserData.Get_sys_UserTable(sLoginName).UserID);
            }
            else
            {
                MBx.M_Body = string.Format("用户名/密码({0}/{1})错误！", sLoginName, sLoginPass);
                MBx.M_ButtonList.Add(new sys_NavigationUrl("返回", "login.aspx", "点击按钮重新输入！", UrlType.Href, true));
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
