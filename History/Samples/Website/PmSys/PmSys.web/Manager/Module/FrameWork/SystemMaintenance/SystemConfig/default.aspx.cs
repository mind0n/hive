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

namespace PmSys.web.Module.PmSys.SystemConfig
{
    public partial class _default : System.Web.UI.Page
    {
        string cmd = (string)Common.sink("CMD", MethodType.Get, 255, 0, DataType.Str);
        protected void Page_Load(object sender, EventArgs e)
        {
            FrameWorkPermission.CheckPagePermission(cmd);
            if (!Page.IsPostBack)
            {
                OnStart();
                if (cmd == "Edit")
                {
                    HiddenDisp();
                    HeadMenuWebControls1.ButtonList[0].ButtonVisible = false;
                }
                else
                {
                    HiddenValue();
                    HeadMenuWebControls1.ButtonList[1].ButtonVisible = false;
                }
            }
        }

        private void OnStart()
        {
            SetValue();  
        }

        

        private void SetValue()
        {
            sys_ConfigDataTable dt = FrameSystemInfo.GetSystemInfoTable.S_SystemConfigData;
            tb_S_Name.Text = lb_S_Name.Text = FrameSystemInfo.GetSystemInfoTable.S_Name;
            tb_S_Version.Text = lb_S_Version.Text = FrameSystemInfo.GetSystemInfoTable.S_Version;
            tb_C_UpImgHeight.Text = lb_C_UpImgHeight.Text = dt.C_UpImgHeight.ToString();
            tb_C_UpImgWidth.Text = lb_C_UpImgWidth.Text = dt.C_UpImgWidth.ToString();
            lb_C_RequestLog.Text = ddl_C_RequestLog.SelectedValue= dt.C_RequestLog.ToString();
            tb_C_IPLookUrl.Text = lb_C_IPLookUrl.Text = dt.C_IPLookUrl.ToString();
            tb_C_UploadFileExt.Text = lb_C_UploadFileExt.Text = dt.C_UploadFileExt.ToString();
            tb_C_UploadSizeKb.Text = lb_C_UploadSizeKb.Text = dt.C_UploadSizeKb.ToString();
            tb_C_LoginErrorDisableMinute.Text = lb_C_LoginErrorDisableMinute.Text = dt.C_LoginErrorDisableMinute.ToString();
            tb_C_LoginErrorMaxNum.Text = lb_C_LoginErrorMaxNum.Text = dt.C_LoginErrorMaxNum.ToString();
            lb_C_HttpGZip.Text = ddl_C_HttpGZip.SelectedValue = dt.C_HttpGZip.ToString();
            lb_C_UploadPath.Text = tb_C_UploadPath.Text = dt.C_UploadPath;
            lb_C_CheckUpdate.Text = ddl_C_CheckUpdate.SelectedValue = dt.C_CheckUpdate.ToString();
             

        }

        private void HiddenValue()
        {
            tb_S_Name.Visible = false;
            tb_S_Version.Visible = false;
            tb_C_UpImgHeight.Visible = false;
            tb_C_UpImgWidth.Visible = false;
            ddl_C_RequestLog.Visible = false;
            tb_C_IPLookUrl.Visible = false;
            tb_C_UploadFileExt.Visible = false;
            tb_C_UploadSizeKb.Visible = false;
            tb_C_LoginErrorDisableMinute.Visible = false;
            tb_C_LoginErrorMaxNum.Visible = false;
            ddl_C_HttpGZip.Visible = false;
            tb_C_UploadPath.Visible = false;
            ddl_C_CheckUpdate.Visible = false;
            Button1.Visible = false;
        }

        private void HiddenDisp()
        {
            lb_S_Name.Visible = false;
            lb_S_Version.Visible = false;
            lb_C_UpImgHeight.Visible = false;
            lb_C_UpImgWidth.Visible = false;
            lb_C_RequestLog.Visible = false;
            lb_C_IPLookUrl.Visible = false;
            lb_C_UploadFileExt.Visible = false;
            lb_C_UploadSizeKb.Visible = false;
            lb_C_LoginErrorDisableMinute.Visible = false;
            lb_C_LoginErrorMaxNum.Visible = false;
            lb_C_HttpGZip.Visible = false;
            lb_C_UploadPath.Visible = false;
            lb_C_CheckUpdate.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            sys_SystemInfoTable st = FrameSystemInfo.GetSystemInfoTable;
            st.DB_Option_Action_ = "Update";

            sys_ConfigDataTable dt = new sys_ConfigDataTable();
            dt.C_RequestLog = Convert.ToBoolean((string)Common.sink(ddl_C_RequestLog.UniqueID, MethodType.Post, 5, 1, DataType.Str));
            dt.C_UpImgHeight = (int)Common.sink(tb_C_UpImgHeight.UniqueID, MethodType.Post, 255, 1, DataType.Int);
            dt.C_UpImgWidth = (int)Common.sink(tb_C_UpImgWidth.UniqueID, MethodType.Post, 255, 1, DataType.Int);
            dt.C_IPLookUrl = (string)Common.sink(tb_C_IPLookUrl.UniqueID, MethodType.Post, 255, 1, DataType.Str);
            dt.C_UploadFileExt = (string)Common.sink(tb_C_UploadFileExt.UniqueID, MethodType.Post, 255, 1, DataType.Str);
            dt.C_UploadSizeKb = (int)Common.sink(tb_C_UploadSizeKb.UniqueID, MethodType.Post, 255, 1, DataType.Int);
            dt.C_LoginErrorDisableMinute = (int)Common.sink(tb_C_LoginErrorDisableMinute.UniqueID, MethodType.Post, 255, 1, DataType.Int);
            dt.C_LoginErrorMaxNum = (int)Common.sink(tb_C_LoginErrorMaxNum.UniqueID, MethodType.Post, 255, 1, DataType.Int);
            dt.C_HttpGZip = Convert.ToBoolean((string)Common.sink(ddl_C_HttpGZip.UniqueID, MethodType.Post, 5, 1, DataType.Str));
            dt.C_UploadPath = (string)Common.sink(tb_C_UploadPath.UniqueID, MethodType.Post, 255, 1, DataType.Str);
            dt.C_CheckUpdate = Convert.ToBoolean((string)Common.sink(ddl_C_CheckUpdate.UniqueID, MethodType.Post, 5, 1, DataType.Str));
            st.S_Name = (string)Common.sink(tb_S_Name.UniqueID, MethodType.Post, 255, 1, DataType.Str);
            st.S_Version = (string)Common.sink(tb_S_Version.UniqueID, MethodType.Post, 20, 1, DataType.Str);            
            
            st.S_SystemConfigData = dt;
            BusinessFacade.sys_SystemInfoInsertUpdate(st);
            EventMessage.MessageBox(1, "修改环境配置", "修改环境配置成功!", Icon_Type.OK, Common.GetHomeBaseUrl("default.aspx"));
        }

    }
}
