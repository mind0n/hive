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

namespace PmSys.web.Module.PmSys.EventManager
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ListBind();
                OnStart();
            }
        }



        public string Get_Type(int E_Type)
        {
            return MessageBox.Get_Type(E_Type);

        }

        private void OnStart()
        {
            BindUserList();
            BindE_ApplicationID();
        }

        private void BindE_ApplicationID()
        {
            QueryParam qp = new QueryParam();
            qp.OrderType = 0;
            int RecordCount = 0;
            ArrayList lst = BusinessFacade.sys_ApplicationsList(qp, out RecordCount);
            E_ApplicationID.DataTextField = "A_AppName";
            E_ApplicationID.DataValueField = "ApplicationID";
            E_ApplicationID.DataSource = lst;
            E_ApplicationID.DataBind();
            E_ApplicationID.Items.Insert(0, new ListItem("不限", ""));
        }

        private void BindUserList()
        {
            QueryParam qp = new QueryParam();
            qp.OrderType = 0;
            int RecordCount = 0;
            ArrayList lst = BusinessFacade.sys_UserList(qp, out RecordCount);
            string stringDel = "";
            foreach (sys_UserTable var in lst)
            {
                stringDel = "";
                if (var.U_Status == 2)
                {
                    stringDel = "己删除";
                }
                E_UserID.Items.Add(new ListItem(var.U_LoginName + "(" + var.U_CName + ")" + stringDel, var.UserID.ToString()));
            }

            E_UserID.Items.Insert(0,new ListItem("不限", ""));
        }

        private void ListBind()
        {

            QueryParam qp = new QueryParam();
            qp.Where = SearchTerms;
            qp.PageIndex = AspNetPager1.CurrentPageIndex;
            qp.PageSize = AspNetPager1.PageSize;
            qp.Orderfld = Orderfld;
            qp.OrderType = OrderType;
            int RecordCount = 0;
            ArrayList lst = BusinessFacade.sys_EventList(qp, out RecordCount);
            GridView1.DataSource = lst;
            GridView1.DataBind();
            this.AspNetPager1.RecordCount = RecordCount;
        }

        protected void Pager_PageChanged(object sender, EventArgs e)
        {
            ListBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            int E_UserID_Value = (int)Common.sink(E_UserID.UniqueID, MethodType.Post, 255, 0, DataType.Int);
            int E_Type_Value = (int)Common.sink(E_Type.UniqueID, MethodType.Post, 255, 0, DataType.Int);
            int E_ApplicationID_Value = (int)Common.sink(E_ApplicationID.UniqueID, MethodType.Post, 255, 0, DataType.Int);
            string E_M_PageCode_Value = (string)Common.sink(E_M_PageCode.UniqueID, MethodType.Post, 6, 0, DataType.Str);
            DateTime? S_E_DateTime_Value = (DateTime?)Common.sink(S_E_DateTime.UniqueID, MethodType.Post, 255, 0, DataType.Dat);
            DateTime? E_E_DateTime_Value = (DateTime?)Common.sink(E_E_DateTime.UniqueID, MethodType.Post, 255, 0, DataType.Dat);
            string E_Record_Value = (string)Common.sink(E_Record.UniqueID, MethodType.Post, 200, 0, DataType.Str);
            string SqlSearch = "Where 1=1 ";

            if (E_UserID_Value != 0)
                SqlSearch = SqlSearch + " and E_UserID = " + E_UserID_Value.ToString();
            if (E_Type_Value!=0)
                SqlSearch = SqlSearch + " and E_Type = " + E_Type_Value.ToString();
            if (E_ApplicationID_Value!=0)
                SqlSearch = SqlSearch + " and E_ApplicationID = " + E_ApplicationID_Value.ToString();
            if (E_M_PageCode_Value!="")
                SqlSearch = SqlSearch + " and E_M_PageCode = '" + Common.inSQL(E_M_PageCode_Value.ToString())+"'";
            if (S_E_DateTime_Value.HasValue && E_E_DateTime_Value.HasValue)
            {
                if (Common.GetDBType == "Access")
                    SqlSearch = SqlSearch + string.Format(" and E_DateTime between #{0} 00:00:00# and #{1} 23:59:59# ", S_E_DateTime_Value.Value.Date.ToShortDateString(), E_E_DateTime_Value.Value.Date.ToShortDateString());
                else if (Common.GetDBType=="Oracle")
                    SqlSearch = SqlSearch + string.Format(" and E_DateTime between to_date('{0} 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('{1} 23:59:59','yyyy-mm-dd HH24:MI:SS') ", S_E_DateTime_Value.Value.Date.ToShortDateString(), E_E_DateTime_Value.Value.Date.ToShortDateString());
                else
                    SqlSearch = SqlSearch + string.Format(" and E_DateTime between '{0} 00:00:00' and '{1} 23:59:59' ", S_E_DateTime_Value.Value.Date.ToShortDateString(), E_E_DateTime_Value.Value.Date.ToShortDateString());

            }
                
            if (E_Record_Value != "")
                SqlSearch = SqlSearch + string.Format(" and E_Record like '%{0}%'",Common.inSQL(E_Record_Value));

            ViewState["SearchTerms"] = SqlSearch;
            TabOptionWebControls1.SelectIndex = 0;
            ListBind();
            


        }

        /// <summary>
        /// 查询条件
        /// </summary>
        private string SearchTerms
        {
            get
            {
                if (ViewState["SearchTerms"] == null)
                    ViewState["SearchTerms"] = " Where 1 = 1";
                return (string)ViewState["SearchTerms"];
            }
            set { ViewState["SearchTerms"] = value; }
        }

        protected void E_ApplicationID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.E_ApplicationID.SelectedValue != "")
            {
                QueryParam qp = new QueryParam();
                qp.OrderType = 0;
                qp.Where = string.Format(" Where M_ApplicationID = {0} and M_ParentID<>0 ", this.E_ApplicationID.SelectedValue);
                int RecordCount = 0;
                ArrayList lst = BusinessFacade.sys_ModuleList(qp, out RecordCount);
                E_M_PageCode.DataTextField = "M_CName";
                E_M_PageCode.DataValueField = "M_PageCode";
                E_M_PageCode.DataSource = lst;
                E_M_PageCode.DataBind();
                E_M_PageCode.Items.Insert(0, new ListItem("不限", ""));
            }
            else {
                E_M_PageCode.Items.Clear();
            }
            TabOptionWebControls1.SelectIndex = 1;
        }

        #region "排序"
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (Orderfld == e.SortExpression)
            {
                if (OrderType == 0)
                {
                    OrderType = 1;
                }
                else
                {
                    OrderType = 0;
                }
            }
            Orderfld = e.SortExpression;
            ListBind();
        }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string Orderfld
        {
            get
            {
                if (ViewState["sortOrderfld"] == null)

                    ViewState["sortOrderfld"] = "EventID";

                return (string)ViewState["sortOrderfld"];
            }
            set
            {
                ViewState["sortOrderfld"] = value;
            }
        }

        /// <summary>
        /// 排序类型 1:降序 0:升序
        /// </summary>
        public int OrderType
        {

            get
            {

                if (ViewState["sortOrderType"] == null)
                    ViewState["sortOrderType"] = 1;

                return (int)ViewState["sortOrderType"];


            }

            set { ViewState["sortOrderType"] = value; }

        }
        /// <summary>
        /// 排序事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                foreach (TableCell var in e.Row.Cells)
                {
                    if (var.Controls.Count > 0 && var.Controls[0] is LinkButton)
                    {
                        string Colume = ((LinkButton)var.Controls[0]).CommandArgument;
                        if (Colume == Orderfld)
                        {

                            LinkButton l = (LinkButton)var.Controls[0];
                            l.Text += string.Format("<img src='{0}' border='0'>", (OrderType == 0) ? Page.ResolveUrl("~/Manager/images/sort_asc.gif") : Page.ResolveUrl("~/Manager/images/sort_desc.gif"));
                            //Image Img = new Image();
                            //SortDirection a = GridView1.SortDirection;
                            //Img.ImageUrl = (a == SortDirection.Ascending) ? "i_p_sort_asc.gif" : "i_p_sort_desc.gif";
                            //var.Controls.Add(Img);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
