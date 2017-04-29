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
using System.Text;

using PmSys;
using PmSys.Components;
using PmSys.WebControls;

namespace PmSys.web.Module.PmSys.GroupManager
{
    public partial class Move : System.Web.UI.Page
    {
        public string TableSink = Common.TableSink;
        public int GroupID = (int)Common.sink("GroupID", MethodType.Get, 255, 1, DataType.Int);
        int ToGroupID = (int)Common.sink("ToGroupID", MethodType.Get, 255, 0, DataType.Int);
        string CMD = (string)Common.sink("CMD", MethodType.Get, 255, 0, DataType.Str);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CMD == "Move")
            {
                FrameWorkPermission.CheckPermissionVoid(PopedomType.Edit);
                MoveCat();
            }
            else {
                OutJs();
            }
            
        }
        private void MoveCat()
        {
            
            sys_GroupTable GT = BusinessFacade.sys_GroupDisp(GroupID);
            sys_GroupTable GTTo = BusinessFacade.sys_GroupDisp(ToGroupID);
            int G_ParentID = GT.G_ParentID;

            //更新移动分类父分类子分类数
            BusinessFacade.Update_Table_Fileds("sys_Group", "G_ChildCount=G_ChildCount-1", string.Format("GroupID={0}", G_ParentID));

            //更新移动分类
            GT.G_ParentID = ToGroupID;
            GT.DB_Option_Action_ = "Update";
            GT.G_ShowOrder = GTTo.G_ChildCount + 1;
            BusinessFacade.sys_GroupInsertUpdate(GT);



            //更新移动到分类
            if (GTTo.GroupID != 0)
            {
                GTTo.G_ChildCount++;
                GTTo.DB_Option_Action_ = "Update";
                BusinessFacade.sys_GroupInsertUpdate(GTTo);
            }

            //排序移动到分类
            BusinessFacade.sys_Group_By(ToGroupID);

            //更新移动分类及子分类层数
            string Sub_GroupID = BusinessFacade.GetGroupCatID(GroupID);
            string[] Sub_GroupIDArray = Sub_GroupID.Split(',');
            for (int i = 0; i < Sub_GroupIDArray.Length; i++)
            {
                BusinessFacade.Update_Table_Fileds("sys_Group", string.Format("G_Level=G_Level+{0}+1-{1}",GTTo.G_Level,GT.G_Level), string.Format("GroupID={0}", Sub_GroupIDArray[i]));
            }

            //排序当前移动分类的父分类的子分类
            BusinessFacade.sys_Group_By(G_ParentID);

            EventMessage.MessageBox(1, "操作成功", string.Format("移动部门({0})成功!", GT.G_CName), Icon_Type.OK, Common.GetHomeBaseUrl(string.Format("GroupList.aspx?GroupID={0}", ToGroupID)),Common.BuildJs  );


        }

        private void OutJs()
        {
            int TotalRecord = 0;
            string strLink = "";
            int intCount = 0;
            
            QueryParam qp = new QueryParam();
            qp.Where = string.Format("Where G_Delete=0 and GroupID not in({0})",BusinessFacade.GetGroupCatID(GroupID));
            qp.Orderfld = "G_Level,G_ShowOrder";
            qp.OrderType = 0;
            ArrayList lst = BusinessFacade.sys_GroupList(qp, out TotalRecord);
            StringBuilder strSB = new StringBuilder();

            strSB.Append("<script language='JavaScript'>\n");
            //strSB.Append("Fold_id='';\n");

            strSB.Append("treeRoot = gFld(\"mainbody\", \"部门列表\", \"\",\"0\")\n");

            foreach (sys_GroupTable x in lst)
            {
                intCount = intCount + 1;
                strLink = x.GroupID.ToString();
                if (x.G_Level == 1)
                {
                    if (x.G_ChildCount == 0)
                    {

                        strSB.AppendFormat("insDoc(treeRoot,gLnk(\"mainbody\",\"{0}\",\"\",\"{1}\"))\n", Common.ReplaceJs(x.G_CName), strLink);
                    }
                    else
                    {
                        strSB.AppendFormat("N{0}=insFld(treeRoot,gFld(\"mainbody\",\"{1}\",\"\",\"{2}\"))\n", x.GroupID, Common.ReplaceJs(x.G_CName), strLink);
                    }
                }
                else
                {
                    if (x.G_ChildCount == 0)
                    {
                        strSB.AppendFormat("insDoc(N{0},gLnk(\"mainbody\",\"{1}\",\"\",\"{2}\"))\n", x.G_ParentID, Common.ReplaceJs(x.G_CName), strLink);
                    }
                    else
                    {
                        strSB.AppendFormat("N{0}=insFld(N{1},gFld(\"mainbody\",\"{2}\",\"\",\"{3}\"))\n", x.GroupID, x.G_ParentID, Common.ReplaceJs(x.G_CName), strLink);
                    }
                }

            }

            strSB.Append("	initializeDocument();\n");
            strSB.Append("</script>\n");
            ShowScript.Text = strSB.ToString();

        }
    }
}
