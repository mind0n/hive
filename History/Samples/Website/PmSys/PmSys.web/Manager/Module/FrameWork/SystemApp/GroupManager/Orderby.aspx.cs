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
    public partial class Orderby : System.Web.UI.Page
    {
        int GroupID = (int)Common.sink("GroupID", MethodType.Get, 255, 1, DataType.Int);
        public string OrderByListItems_UniqueID;
        protected void Page_Load(object sender, EventArgs e)
        {
            OrderByListItems_UniqueID = OrderByListItems.UniqueID.Replace("$","_");
            Button2.OnClientClick = string.Format("selectAll({0})", OrderByListItems_UniqueID);
            OnBindButton();
            if (!Page.IsPostBack)
            {
                OnStart();
            }
        }
        private void OnBindButton()
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
            int RecordCount = 0;
            QueryParam qp = new QueryParam();
            qp.Orderfld = "G_Level,G_ShowOrder";
            qp.OrderType = 0;
            qp.Where = string.Format("Where G_ParentID={0} and G_Delete= 0",GroupID);
            ArrayList lst = BusinessFacade.sys_GroupList(qp, out RecordCount);
            OrderByListItems.DataSource = lst;
            OrderByListItems.DataTextField = "G_CName";
            OrderByListItems.DataValueField = "GroupID";
            OrderByListItems.DataBind();
            if (lst.Count > 0)
                OrderByListItems.Rows = lst.Count;

            CatListTitle.Text = string.Format("<a href='GroupList.aspx'>部门列表</a>{0}", BusinessFacade.GetGroupTitle(GroupID));

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            FrameWorkPermission.CheckPermissionVoid(PopedomType.Orderby);
            string ItemsList = (string)Common.sink(OrderByListItems.UniqueID, MethodType.Post, 100000, 1, DataType.Str);
            if (ItemsList.Length > 0)
            {
                string[] ItemsLists = ItemsList.Split(',');
                for (int i = 0; i < ItemsLists.Length; i++)
                {

                    BusinessFacade.Update_Table_Fileds("sys_Group", string.Format("G_ShowOrder={0}", i+1), string.Format("GroupID={0}", ItemsLists[i]));
                }
            }

            EventMessage.MessageBox(1, "操作成功", string.Format("排序子部门成功!"), Icon_Type.OK, Common.GetHomeBaseUrl(string.Format("GroupList.aspx?GroupID={0}", GroupID)),Common.BuildJs);

        }
    }
}
