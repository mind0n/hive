using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PmSys.Components;
using PmSys.WebControls;

namespace PmSys.Web.Manager.Module.FrameWork.Projects
{
    public partial class ProjectManager : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                OnStart();
            }
        }

        private void OnStart()
        {
            InputData();
            switch (CMD)
            {
                case "New":
                    CMD_Txt = "增加";
                    HiddenDisp();
                    TopTr.Visible = false;
                    break;
                case "Edit":
                    CMD_Txt = "修改";
                    HiddenDisp();
                    HeadMenuButtonItem Bm0 = new HeadMenuButtonItem();
                    Bm0.ButtonPopedom = PopedomType.List;
                    Bm0.ButtonUrl = string.Format("?CMD=Look&S_ID={0}", S_ID);
                    Bm0.ButtonIcon = "back.gif";
                    Bm0.ButtonName = "返回";
                    HeadMenuWebControls1.ButtonList.Add(Bm0);
                    HeadMenuButtonItem Bm1 = new HeadMenuButtonItem();
                    Bm1.ButtonPopedom = PopedomType.Delete;
                    Bm1.ButtonUrlType = UrlType.JavaScript;
                    Bm1.ButtonUrl = string.Format("DelData('?CMD=Delete&S_ID={0}')", S_ID);
                    HeadMenuWebControls1.ButtonList.Add(Bm1);
                    break;
                case "Look":
                    HiddenInput();
                    HeadMenuButtonItem Bm2 = new HeadMenuButtonItem();
                    Bm2.ButtonPopedom = PopedomType.Edit;
                    Bm2.ButtonUrl = string.Format("?CMD=Edit&S_ID={0}", S_ID);
                    HeadMenuWebControls1.ButtonList.Add(Bm2);
                    break;
                case "Delete":
                    CMD_Txt = "删除";
                    PM_ProjectsTable sat = new PM_ProjectsTable();
                    sat.ProjectId = S_ID.ToString();
                    sat.DB_Option_Action_ = CMD;
                    BusinessFacade.PM_ProjectsTableInsertUpdate(sat);

                    EventMessage.MessageBox(1, "操作成功", "删除记录ID:(" + S_ID + ")成功！", Icon_Type.OK, Common.GetHomeBaseUrl("ProjectList.aspx"));
                    break;
            }
            All_Title_Txt = CMD_Txt + App_Txt;
            HeadMenuWebControls1.HeadOPTxt = TabOptionItem1.Tab_Name = All_Title_Txt;
        }

        private void InputData()
        {
            if (string.IsNullOrEmpty(S_ID))
                return;

            PM_ProjectsTable PJT = BusinessFacade.PM_ProjectsDisp(S_ID);
            if (PJT != null)
            {
                labelProjectNo.Text = PJT.ProjectNo;
                labelProjectName.Text = textBoxProjectName.Text = PJT.ProjectName;
                labelProjectStatus.Text = DropDownListProjectStatus.SelectedValue = PJT.ProjectStatus.ToString();
                labelProjectStartTime.Text = textBoxProjectStartTime.Text = PJT.ProjectStartTime.ToShortDateString();
                labelProjectDuration.Text = textBoxProjectDuration.Text = PJT.ProjectDuration.ToString();
                labelProjectBrief.Text = textProjectBrief.Text = PJT.ProjectBrief.ToString();
                labelProjectComments.Text = textProjectComments.Text = PJT.ProjectComments;
                labelProjectIcon.Text = textProjectIcon.Text = PJT.ProjectIcon;
            }
        }

        private void HiddenDisp()
        {
            this.labelProjectName.Visible = false;
            this.labelProjectStatus.Visible = false;
            this.labelProjectStartTime.Visible = false;
            this.labelProjectDuration.Visible = false;
            this.labelProjectBrief.Visible = false;
            this.labelProjectComments.Visible = false;
            this.labelProjectIcon.Visible = false;
        }
        private void HiddenInput()
        {
            this.textBoxProjectName.Visible = false;
            this.DropDownListProjectStatus.Visible = false;
            this.textBoxProjectStartTime.Visible = false;
            this.textBoxProjectDuration.Visible = false;
            this.textProjectBrief.Visible = false;
            this.textProjectComments.Visible = false;
            this.textProjectIcon.Visible = false;
        }

        protected void ButtonOK_Click(object sender, EventArgs e)
        {
            PM_ProjectsTable sat = new PM_ProjectsTable();
            sat.ProjectId = S_ID.ToString();
            sat.ProjectNo = string.Format("No{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
            sat.ProjectName = (string)Common.sink(textBoxProjectName.UniqueID, MethodType.Post, 50, 1, DataType.Str);
            sat.ProjectStatus =Convert.ToInt32(DropDownListProjectStatus.SelectedValue);
            sat.ProjectStartTime = (DateTime)Common.sink(textBoxProjectStartTime.UniqueID, MethodType.Post, 100, 1, DataType.Dat);
            sat.ProjectDuration =(int)Common.sink(textBoxProjectDuration.UniqueID, MethodType.Post, 50, 1, DataType.Int);
            sat.ProjectBrief = (string)Common.sink(textProjectBrief.UniqueID, MethodType.Post, 500, 0, DataType.Str);
            sat.ProjectComments = (string)Common.sink(textProjectComments.UniqueID, MethodType.Post, 500, 0, DataType.Str);
            sat.ProjectIcon = (string)Common.sink(textProjectIcon.UniqueID, MethodType.Post, 100, 0, DataType.Str);
            switch (CMD)
            {
                case "New":
                    CMD_Txt = "增加";
                    sat.DB_Option_Action_ = "Insert";
                    break;
                case "Edit":
                    CMD_Txt = "修改";
                    sat.DB_Option_Action_ = "Update";
                    break;
            }
            All_Title_Txt = CMD_Txt + App_Txt;
            BusinessFacade.PM_ProjectsTableInsertUpdate(sat);
            EventMessage.MessageBox(1, "操作成功", string.Format("{1}ID({0})成功!", S_ID, All_Title_Txt), Icon_Type.OK, Common.GetHomeBaseUrl("ProjectList.aspx"));

        }
    }
}