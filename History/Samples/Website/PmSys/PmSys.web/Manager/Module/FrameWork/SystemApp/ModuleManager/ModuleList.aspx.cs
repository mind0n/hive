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

namespace PmSys.web.Module.PmSys.ModuleManager
{
    public partial class ModuleList : System.Web.UI.Page
    {
        int S_ID = (int)Common.sink("S_ID", MethodType.Get, 255, 1, DataType.Int);
        string AppName = (string)Common.sink("AppName", MethodType.Get, 255, 1, DataType.Str);
        public string OrderByListItems_UniqueID;
        protected void Page_Load(object sender, EventArgs e)
        {

            OrderByListItems_UniqueID = OrderByListItems.UniqueID.Replace("$","_");
            Button1.OnClientClick = string.Format("selectAll({0})", OrderByListItems_UniqueID);
            if (!Page.IsPostBack)
            {
                
                //AppName_Txt.Text = AppName;
                AppName = "应用列表->" + AppName;
                TabOptionItem1.Tab_Name = AppName;
                
                BindMenu();

                HeadMenuButtonItem m1 = new HeadMenuButtonItem();
                m1.ButtonName = "模块分类";
                m1.ButtonPopedom = PopedomType.New;
                m1.ButtonUrl = string.Format("moduleManager.aspx?CMD=New&S_ID={0}&ModuleId={1}",S_ID,0);
                HeadMenuWebControls1.ButtonList.Add(m1);

                //判断是否有排序权限
                if (!FrameWorkPermission.CheckButtonPermission(PopedomType.Orderby))
                    TabOptionItem2.Visible = false;
            }
            

            
        }

        public string GetStatus(int ID)
        {
            return BusinessFacade.GetStatus(ID);
        }


        #region "绑定主菜单"
        /// <summary>
        /// 绑定主菜单
        /// </summary>
        private void BindMenu()
        {
            QueryParam qp = new QueryParam();
            qp.Orderfld = " M_Applicationid,M_OrderLevel ";
            qp.OrderType = 0;
            qp.Where = string.Format("Where M_ParentID=0 and M_ApplicationID ={0}", S_ID);
            int RecordCount = 0;
            ArrayList lst = BusinessFacade.sys_ModuleList(qp, out RecordCount);

            GridView1.DataSource = lst;
            GridView1.DataBind();

            //绑定排序
            OrderByListItems.DataSource = lst;
            OrderByListItems.DataTextField = "M_CName";
            OrderByListItems.DataValueField = "ModuleID";
            OrderByListItems.DataBind();
            if (lst.Count>0)
                OrderByListItems.Rows = lst.Count;
        }
        #endregion

        public int GetSuBCount(int ModuleID)
        {
            QueryParam qp = new QueryParam();
            qp.Orderfld = " M_OrderLevel ";
            qp.OrderType = 0;
            qp.Where = string.Format("Where M_ParentID ={0}", ModuleID);
            int RecordCount = 0;
            ArrayList lst = BusinessFacade.sys_ModuleList(qp, out RecordCount);
            return RecordCount;
        }

        #region "绑定子菜单"
        /// <summary>
        /// 绑定子菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ModuleListMain_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            sys_ModuleTable s_Mt = (sys_ModuleTable)e.Item.DataItem;

            QueryParam qp = new QueryParam();
            qp.Orderfld = " M_OrderLevel ";
            qp.OrderType = 0;
            qp.Where = string.Format("Where M_ParentID ={0}", s_Mt.ModuleID);
            int RecordCount = 0;
            ArrayList lst = BusinessFacade.sys_ModuleList(qp, out RecordCount);
            if (lst.Count > 0)
            {
                Repeater LeftSubID = (Repeater)e.Item.FindControl("ModuleListMain_Sub");
                LeftSubID.DataSource = lst;
                LeftSubID.DataBind();
            }
        }
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            FrameWorkPermission.CheckPermissionVoid(PopedomType.Orderby);
            string ItemsList = (string)Common.sink(OrderByListItems.UniqueID, MethodType.Post, 100000, 1, DataType.Str);
            if (ItemsList.Length > 0)
            {
                int RecordCount = 0;
                ArrayList lst = new ArrayList();
                QueryParam qp = new QueryParam();


                string[] ItemsLists = ItemsList.Split(',');
                for (int i = 0; i < ItemsLists.Length; i++)
                {
                    BusinessFacade.Update_Table_Fileds("sys_Module", string.Format("M_OrderLevel='{0}{1}'", Common.FillZero(i.ToString(), 2), "00"), string.Format("ModuleID={0}", ItemsLists[i]));
                    qp.Orderfld = "M_OrderLevel ";
                    qp.OrderType = 0;
                    qp.Where = string.Format("Where M_ParentID={0}", ItemsLists[i]);
                    RecordCount = 0;
                    lst = BusinessFacade.sys_ModuleList(qp, out RecordCount);
                    if (lst.Count > 0)
                    {
                        RecordCount = 1;
                        foreach (sys_ModuleTable var in lst)
                        {
                            BusinessFacade.Update_Table_Fileds("sys_Module", string.Format("M_OrderLevel='{0}{1}'", Common.FillZero(i.ToString(), 2), Common.FillZero(RecordCount.ToString(), 2)), string.Format("ModuleID={0}", var.ModuleID));
                            RecordCount++;
                        }                            
                    }
                }
            }

            EventMessage.MessageBox(1, "操作成功", string.Format("排序{0}模块成功!",AppName), Icon_Type.OK, Common.GetHomeBaseUrl(string.Format("ModuleList.aspx?S_ID={0}&AppName={1}",S_ID,AppName)));
        }
    }
}
