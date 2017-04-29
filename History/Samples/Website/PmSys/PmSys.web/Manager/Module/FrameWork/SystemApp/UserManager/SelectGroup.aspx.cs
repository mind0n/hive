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

namespace PmSys.web.Module.PmSys.UserManager
{
    public partial class SelectGroup : System.Web.UI.Page
    {
        public string TableSink = Common.TableSink;
        protected void Page_Load(object sender, EventArgs e)
        {
            OutJs();
        }

        private void OutJs()
        {
            int TotalRecord = 0;
            string strLink = "";
            int intCount = 0;
            QueryParam qp = new QueryParam();
            qp.Where = "Where G_Delete=0";
            qp.Orderfld = "G_Level,G_ShowOrder";
            qp.OrderType = 0;
            ArrayList lst = BusinessFacade.sys_GroupList(qp, out TotalRecord);
            StringBuilder strSB = new StringBuilder();

            strSB.Append("<script language='JavaScript'>\n");
            strSB.Append("Fold_id='';\n");

            strSB.Append("treeRoot = gFld(\"mainbody\", \"部门列表\", \"0\",\"0\")\n");

            foreach (sys_GroupTable x in lst)
            {
                intCount = intCount + 1;
                //strLink = "GroupList.aspx?GroupID=" + x.GroupID.ToString();
                strLink = x.GroupID.ToString();
                if (x.G_Level == 1)
                {
                    if (x.G_ChildCount == 0)
                    {

                        strSB.AppendFormat("insDoc(treeRoot,gLnk(\"mainbody\",\"{0}\",\"\",{1}))\n", Common.ReplaceJs(x.G_CName), strLink);
                    }
                    else
                    {
                        strSB.AppendFormat("N{0}=insFld(treeRoot,gFld(\"mainbody\",\"{1}\",\"\",{2}))\n", x.GroupID, Common.ReplaceJs(x.G_CName), strLink);
                    }
                }
                else
                {
                    if (x.G_ChildCount == 0)
                    {
                        strSB.AppendFormat("insDoc(N{0},gLnk(\"mainbody\",\"{1}\",\"\",{2}))\n", x.G_ParentID, Common.ReplaceJs(x.G_CName), strLink);
                    }
                    else
                    {
                        strSB.AppendFormat("N{0}=insFld(N{1},gFld(\"mainbody\",\"{2}\",\"\",{3}))\n", x.GroupID, x.G_ParentID, Common.ReplaceJs(x.G_CName), strLink);
                    }
                }

            }

            strSB.Append("	initializeDocument();\n");
            strSB.Append("</script>\n");
            ShowScript.Text = strSB.ToString();
        }
    }
}
