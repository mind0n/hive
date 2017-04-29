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
using PmSys.WebControls;

namespace PmSys.web
{
    public partial class UserSet : System.Web.UI.Page
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
            sys_UserTable ut = BusinessFacade.sys_UserDisp(Common.Get_UserID);
            U_LoginName_Txt.Text = ut.U_LoginName;
            string[] U_ExtendFieldArray = (ut.U_ExtendField+"").Split(',');
            if (U_ExtendFieldArray.Length == 3)
            {
                ListItem li = MenuSink.Items.FindByValue(U_ExtendFieldArray[0]);
                if (li != null)
                    li.Selected = true;
                ListItem li1 = PageSize.Items.FindByValue(U_ExtendFieldArray[1]);
                if (li1 != null)
                    li1.Selected = true;

                ListItem li2 = TableSink.Items.FindByValue(U_ExtendFieldArray[2]);
                if (li2 != null)
                    li2.Selected = true;
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string DispTxt = "";
            if (Common.Get_UserID != 0)
            {
                sys_UserTable ut = BusinessFacade.sys_UserDisp(Common.Get_UserID);
                string Old_U_Password_Value = (string)Common.sink(Old_U_Password.UniqueID, MethodType.Post, 20, 0, DataType.Str);
                string New_U_Password_Value = (string)Common.sink(New_U_Password.UniqueID, MethodType.Post, 20, 0, DataType.Str);
                string ReNew_U_Password_Value = (string)Common.sink(ReNew_U_Password.UniqueID, MethodType.Post, 20, 0, DataType.Str);
                int MenuSink_Value = (int)Common.sink(MenuSink.UniqueID, MethodType.Post, 255, 1, DataType.Int);
                int PageSize_Value = (int)Common.sink(PageSize.UniqueID, MethodType.Post, 255, 1, DataType.Int);
                string TableSink_Value = (string)Common.sink(TableSink.UniqueID, MethodType.Post, 255, 1, DataType.Str);
                bool ChangPwdBool = false;

                if (Old_U_Password_Value != "" && New_U_Password_Value != "" && ReNew_U_Password_Value != "")
                {
                    if (New_U_Password_Value != ReNew_U_Password_Value)
                    {
                        DispTxt = "二次输入的密码不相同，请重新输入!";
                    }
                    else if (Common.md5(Old_U_Password_Value, 32) != ut.U_Password)
                        DispTxt = "原密码输入错误，请重新输入!";
                    else
                    {
                        ut.U_Password = Common.md5(New_U_Password_Value, 32);
                        ChangPwdBool = true;
                    }
                        
                }
                ut.U_ExtendField = MenuSink_Value + "," + PageSize_Value+","+TableSink_Value;

                if (DispTxt != "")
                    Common.MessBox(DispTxt);
                else
                {
                    string  titleMessage = string.Format("({0})个人资料设定成功!",ut.U_LoginName);
                    ut.DB_Option_Action_ = "Update";
                    BusinessFacade.sys_UserInsertUpdate(ut);
                    UserData.MoveUserCache(Common.Get_UserID);
                    if (ChangPwdBool)
                    {
                        titleMessage = "密码修改成功,"+titleMessage;
                        FrameWorkLogin.UserOut();
                    }

                    Common.MenuStyle = MenuSink_Value;
                    Common.PageSize = PageSize_Value;
                    Common.TableSink = TableSink_Value;
                    
                    EventMessage.EventWriteDB(1, titleMessage);
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(typeof(string), "TabJs", "<script language='javascript'>window.returnVal='" + titleMessage + "';window.parent.hidePopWin(true);</script>");
                }
            }
        }

    }
}
