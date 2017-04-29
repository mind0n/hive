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
    public partial class Edit : System.Web.UI.Page
    {
        int GroupID = (int)Common.sink("GroupID", MethodType.Get, 255, 1, DataType.Int);
        string CMD = (string)Common.sink("CMD", MethodType.Get, 255, 0, DataType.Str);
        protected void Page_Load(object sender, EventArgs e)
        {
            BindButton();
            if (!Page.IsPostBack)
            {
                OnStart();
            }
        }

        private void OnStart()
        {
            sys_GroupTable gt = BusinessFacade.sys_GroupDisp(GroupID);
            this.G_CName.Text = gt.G_CName;
            if (gt.G_ParentID == 0)
                G_ParentID_Txt.Text = "部门列表";
            else
                G_ParentID_Txt.Text = BusinessFacade.sys_GroupDisp(gt.G_ParentID).G_CName;
            CatListTitle.Text = string.Format("<a href='GroupList.aspx'>部门列表</a>{0}", BusinessFacade.GetGroupTitle(GroupID));
            //判断是否删除
            if (CMD == "Delete")
            {
                FrameWorkPermission.CheckPermissionVoid(PopedomType.Delete);
                
                //删除子分类
                DeleteCat(GroupID);
                //删除当前分类
                BusinessFacade.Update_Table_Fileds("sys_Group", "G_Delete=1", string.Format("GroupID={0}", GroupID));
                





                if (gt.G_ParentID!=0)
                {
                    //更新父类子类数
                    BusinessFacade.Update_Table_Fileds("sys_Group", "G_ChildCount=G_ChildCount-1", string.Format("GroupID={0}", gt.G_ParentID));
                    //更新父类子类排序  
                    int RecordCount = 0;
                    QueryParam qp = new QueryParam();
                    qp.Where = string.Format("Where G_ParentID={0} and G_Delete=0",gt.G_ParentID);
                    qp.Orderfld = "G_Level,G_ShowOrder";
                    qp.OrderType = 0;
                    ArrayList lst = BusinessFacade.sys_GroupList(qp,out RecordCount);
                    RecordCount = 1;
                    foreach (sys_GroupTable var in lst)
                    {
                        BusinessFacade.Update_Table_Fileds("sys_Group", string.Format("G_ShowOrder={0}", RecordCount), string.Format("GroupID={0}", var.GroupID));
                        RecordCount++;
                    }

                }
                EventMessage.MessageBox(1, "操作成功", string.Format("删除部门({0})成功!",gt.G_CName), Icon_Type.OK, Common.GetHomeBaseUrl("GroupList.aspx"),Common.BuildJs);

            }
        }

        private void BindButton()
        {
            HeadMenuButtonItem bi1 = new HeadMenuButtonItem();
            bi1.ButtonPopedom = PopedomType.Delete;
            bi1.ButtonUrl = string.Format("DelData('?CMD=Delete&GroupID={0}')", GroupID);
            bi1.ButtonUrlType = UrlType.JavaScript;
            HeadMenuWebControls1.ButtonList.Add(bi1);

            HeadMenuButtonItem bi0 = new HeadMenuButtonItem();
            bi0.ButtonIcon = "back.gif";
            bi0.ButtonName = "返回";
            bi0.ButtonPopedom = PopedomType.List;
            bi0.ButtonUrl = string.Format("GroupList.aspx?GroupID={0}", GroupID);
            HeadMenuWebControls1.ButtonList.Add(bi0);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FrameWorkPermission.CheckPermissionVoid(PopedomType.Edit);
            sys_GroupTable gt = BusinessFacade.sys_GroupDisp(GroupID);
            gt.G_CName = (string)Common.sink(G_CName.UniqueID,MethodType.Post,50,1,DataType.Str);
            gt.DB_Option_Action_ = "Update";
            BusinessFacade.sys_GroupInsertUpdate(gt);
            EventMessage.MessageBox(1, "操作成功", string.Format("修改部门ID({0})成功!", gt.GroupID), Icon_Type.OK, Common.GetHomeBaseUrl(string.Format("GroupList.aspx?GroupID={0}", GroupID)),Common.BuildJs);
        }

        private void DeleteCat(int GroupID)
        {
            //删除子分类和删除当前分类
            int RecordCount = 0;
            QueryParam qp = new QueryParam();
            qp.Where = string.Format("Where G_ParentID={0} and G_Delete = 0 ", GroupID);
            ArrayList lst = BusinessFacade.sys_GroupList(qp, out RecordCount);
            foreach (sys_GroupTable var in lst)
            {
                BusinessFacade.Update_Table_Fileds("sys_Group", "G_Delete=1", string.Format("GroupID={0}", var.GroupID));
                DeleteCat(var.GroupID);
            }
        }
    }
}
