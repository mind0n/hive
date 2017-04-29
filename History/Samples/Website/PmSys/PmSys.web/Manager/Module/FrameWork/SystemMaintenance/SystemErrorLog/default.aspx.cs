using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PmSys.Components;
using PmSys.WebControls;

namespace PmSys.web.Module.PmSys.SystemErrorLog
{
    public partial class _default : System.Web.UI.Page
    {
        string CMD = (string)Common.sink("CMD", MethodType.Get, 50, 0, DataType.Str);
        protected void Page_Load(object sender, EventArgs e)
        {
            FrameWorkPermission.CheckPagePermission(CMD);   
            if (!Page.IsPostBack)
            {
                FileLogList.DataSource = FileTxtLogs.GetFileList();
                FileLogList.DataTextField = "Value";
                FileLogList.DataValueField = "Value";
                FileLogList.DataBind();
                BindDate();

            }
            if (FileLogList.Items.Count != 0)
            {
                HeadMenuButtonItem item0 = new HeadMenuButtonItem();
                item0.ButtonName = "当前日志文件";
                item0.ButtonPopedom = PopedomType.Delete;
                item0.ButtonUrlType = UrlType.JavaScript;
                item0.ButtonUrl = "DelData('?CMD=Delete');";
                HeadMenuWebControls1.ButtonList.Add(item0);
            }
            if (CMD == "Delete")
                DeleteFile();
        }

        private void DeleteFile()
        {
            string filePath = FileTxtLogs.LogPath + FileLogList.SelectedValue.ToString();
            FileTxtLogs.DeleteFile(filePath);
            EventMessage.MessageBox(1, "删除日志文件", string.Format("删除日志文件({0})成功!",filePath), Icon_Type.OK, Common.GetHomeBaseUrl("default.aspx"));
        }

        private void BindDate()
        {
            List<FileTxtLogsTable> lst = new List<FileTxtLogsTable>();
            lst = FileTxtLogs.GetFileTxtLogs(FileTxtLogs.LogPath+FileLogList.SelectedValue.ToString());

            lst.Sort();

            AspNetPager1.RecordCount = lst.Count;
            ArrayList lists = new ArrayList();
            for (int i = AspNetPager1.StartRecordIndex; i <= AspNetPager1.EndRecordIndex; i++)
            {
                lists.Add(lst[i - 1]);
            }
            

            GridView1.DataSource = lists;
            GridView1.DataBind();
        }

        protected void FileLogList_SelectedIndexChanged(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            BindDate();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindDate();
        }
    }
}
