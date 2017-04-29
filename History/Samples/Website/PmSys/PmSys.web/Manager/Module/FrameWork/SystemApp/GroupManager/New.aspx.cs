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
using PmSys.WebControls;

namespace PmSys.web.Module.PmSys.GroupManager
{
    public partial class New : System.Web.UI.Page
    {
        int GroupID;
        protected void Page_Load(object sender, EventArgs e)
        {
            GroupID = (int)Common.sink("GroupID", MethodType.Get, 255, 1, DataType.Int);
            
            BindButton();
            if (!Page.IsPostBack)
            {
                OnStart();
            }
        }


        private void BindButton()
        {
            HeadMenuButtonItem bi0 = new HeadMenuButtonItem();
            bi0.ButtonIcon = "back.gif";
            bi0.ButtonName = "返回";
            bi0.ButtonPopedom = PopedomType.List;
            bi0.ButtonUrl = string.Format("GroupList.aspx?GroupID={0}", GroupID);
            HeadMenuWebControls1.ButtonList.Add(bi0);


        }

        private void OnStart()
        {
            if (GroupID == 0)
                G_ParentID_Txt.Text = "部门列表";
            else {
                G_ParentID_Txt.Text = BusinessFacade.sys_GroupDisp(GroupID).G_CName;
            }

            CatListTitle.Text = string.Format("<a href='GroupList.aspx'>部门列表</a>{0}",BusinessFacade.GetGroupTitle(GroupID));
            //Button1.Attributes.Add("Onclick", "javascript:return checkForm(aspnetForm);");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            int RecordCount = 0;
            FrameWorkPermission.CheckPermissionVoid(PopedomType.New);
            sys_GroupTable gt = BusinessFacade.sys_GroupDisp(GroupID);
            gt.G_CName = (string)Common.sink(G_CName.UniqueID, MethodType.Post, 50, 1, DataType.Str);
            if (gt.GroupID == 0)
            {
                gt.G_Level = 1;
                BusinessFacade.sys_GroupList(new QueryParam(), out RecordCount);
                gt.G_ShowOrder = RecordCount + 1;
            }
            else
            {
                
                gt.G_Level ++;
                gt.G_ShowOrder = gt.G_ChildCount+1;
                gt.G_ChildCount = 0;
                gt.G_ParentID = GroupID;
            }

            gt.DB_Option_Action_ = "Insert";
            BusinessFacade.sys_GroupInsertUpdate(gt);
            //更新子部门数
            BusinessFacade.Update_Table_Fileds("sys_Group", "G_ChildCount=G_ChildCount+1", string.Format("GroupID={0}", GroupID));
            EventMessage.MessageBox(1, "操作成功", string.Format("增加部门({0})成功!", gt.G_CName), Icon_Type.OK, Common.GetHomeBaseUrl(string.Format("GroupList.aspx?GroupID={0}", GroupID)),Common.BuildJs);
        }
    }
}
