using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Entities;
using System.Data;
using System.Data.OleDb;

namespace Site.Admin.AdminDAL
{
    public static class DalHandler
    {
        #region User
        public static DataTable GetAllUsers()
        {
            try
            {
                DataTable dt = OleDbHelper.GetDataTable("select * from [tUsers] order by ulevel, uname");
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int AddUser(UserItem user)
        {
            try
            {
                string comText = "insert into [tUsers] (uname ,upwd ,utext ,ulevel) values ( '"
                    + user.UName + "', '" + user.UPwd + "', '" + user.UText + "', " + user.ULevel + ")";
                int rlt = OleDbHelper.ExecuteNonQuery(comText);
                return rlt;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int UpdateUserById(UserItem user)
        {
            try
            {
                string comText = "update [tUsers] set tUsers.uname='" + user.UName + "', tUsers.upwd='" + user.UPwd + "', tUsers.utext='" + user.UText + "', tUsers.ulevel='" + user.ULevel + "', tUsers.userupdate='" + user.UserUpdate + "' where tUsers.userid=" + user.UserId;
                int rlt = OleDbHelper.ExecuteNonQuery(comText);
                return rlt;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int DeleteUserById(int id)
        {
            try
            {
                string comText = "delete from [tUsers] where tUsers.userid =" + id;
                int rlt = OleDbHelper.ExecuteNonQuery(comText);
                return rlt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Categories
        public static DataTable GetAllCategories()
        {
            try
            {
                DataTable dt = OleDbHelper.GetDataTable("select * from [tCategories] where parentid=0 order by parentid, caption ");
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static OleDbDataReader GetParentIdByCategroyId(string id)
        {
            try
            {
                OleDbDataReader oleDr = OleDbHelper.ExecuteReader("select * from [tCategories] where categoryid = " + id);

                return oleDr;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataTable GetAllCategories2()
        {
            try
            {
                DataTable dt = OleDbHelper.GetDataTable("select * from [tCategories] order by parentid, caption");
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataTable GetChildCategoryByParentId(string id)
        {
            try
            {
                DataTable dt = OleDbHelper.GetDataTable("select * from [tCategories] where parentid =" + id + " order by caption");
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetCategoryById(string id)
        {
            try
            {
                DataTable dt = OleDbHelper.GetDataTable("select * from [tCategories] where categoryid =" + id);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int DeleteCategoryById(int categoryId)
        {
            try
            {
                string comText = "delete from [tCategories] where tCategories.categoryid =" + categoryId;
                int rlt = OleDbHelper.ExecuteNonQuery(comText);
                return rlt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int InsertOrUpdateCategory(DAL.Category category, int flag)
        {
            int rlt = 0;
            string commText = string.Empty;
            if (flag == 0)
            {
                commText = "insert into [tCategories] (caption , parentid , visible ,categoryupdate) values ('"
                        + category.Caption + "'," + category.ParentId + "," + category.Visible + " ,'" + category.CategoryUpdate + "')";
                try
                {
                    rlt = OleDbHelper.ExecuteNonQuery(commText);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else if (flag == 1)
            {
                commText = "update [tCategories] set caption ='"
                        + category.Caption + "',parentid ='" + category.ParentId + "',visible =" + category.Visible + " ,categoryupdate= '" + category.CategoryUpdate +
                        "' where categoryid =" + category.CategoryId;
                try
                {
                    rlt = OleDbHelper.ExecuteNonQuery(commText);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return rlt;
        }
        #endregion

        #region artilces
        public static DataTable GetArtilesDetails(int id = 0)
        {
            try
            {

				string commText;
				if (id == 0)
				{
					commText = "select * from [vArticleCategory] order by articleupdate desc";
				}
				else
				{
					commText = string.Concat("select * from [vArticleCategory] where [tArticles].categoryid =", id, " order by articleupdate desc");
				}
                DataTable dt = OleDbHelper.GetDataTable(commText);
				dt.Columns[1].ColumnName = "Acategoryid";
				dt.Columns[4].ColumnName = "Acaption";
				dt.Columns[8].ColumnName = "Avisible";
				dt.Columns[13].ColumnName = "Ccaption";
				return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static OleDbDataReader GetArtilesDetailsByArticleId(int id)
        {
            try
            {
                string commText = "select * from [vArticleCategory] where [tArticles].articleid =" + id;
                OleDbDataReader oleDr = OleDbHelper.ExecuteReader(commText);
                return oleDr;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int InsertOrUpdateArtile(Article article, int flag)
        {
            int rlt = 0;
            string commText = string.Empty;
            if (flag == 0)
            {
                commText = "insert into [tArticles] (categoryid , userid ,caption , link ,articleupdate , visible , tag ,content) values ('"
                        + article.CategoryId + "'," + article.UserId + ",'" + article.Caption + "','" + article.Link + "','" + article.ArticleUpdate + "',"
                        + article.IsVisible + " ,'" + article.Tag + "','" + article.Content + "')";
                try
                {
                    rlt = OleDbHelper.ExecuteNonQuery(commText);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else if (flag == 1)
            {
                commText = "update [tArticles] set categoryid ="
                        + article.CategoryId + ",userid =" + article.UserId
                        + ",caption ='" + article.Caption 
                        + "',articleupdate ='" + article.ArticleUpdate + "' ,visible= " + article.IsVisible
                        + " ,content= '" + article.Content
                        + "' where articleid =" + article.ArticleId;
                try
                {
                    rlt = OleDbHelper.ExecuteNonQuery(commText);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return rlt;
        }

        public static int DeleteArticleById(int id)
        {
            try
            {
                string comText = "delete from [tArticles] where tArticles.articleid =" + id;
                int rlt = OleDbHelper.ExecuteNonQuery(comText);
                return rlt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region pcmapping
        public static DataTable GetAllPcMapping()
        {
            try
            {
                DataTable dt = OleDbHelper.GetDataTable("select * from [vPageCategories] order by pid, morder, caption");
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int InsertOrUpdatePcMappings(PcMappings pcMappings, int flag)
        {
            int rlt = 0;
            string commText = string.Empty;
            if (flag == 0)
            {
                commText = "insert into [tPcMappings] (categoryid , morder , pid ,[container] ,widgetname ,widgetsettings ,clientvisible , pcupdate) values ("
                        + pcMappings.Categoryid + "," + pcMappings.Morder + ",'" + pcMappings.Pid + "', '" + pcMappings.Container + "', '" + pcMappings.Widgetname + "', '" + pcMappings.Widgetsettings + "',"
                        + pcMappings.Isvisible + " ,'" + pcMappings.Pcupdate + "')";
                try
                {
                    rlt = OleDbHelper.ExecuteNonQuery(commText);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else if (flag == 1)
            {
                commText = "update [tPcMappings] set categoryid ="
                        + pcMappings.Categoryid + ", morder =" + pcMappings.Morder + ", pid ='" + pcMappings.Pid + "', [container] ='" + pcMappings.Container
                        + "', widgetname ='" + pcMappings.Widgetname + "', widgetsettings ='" + pcMappings.Widgetsettings + "', clientvisible =" + pcMappings.Isvisible + " , pcupdate= '" + pcMappings.Pcupdate +
                        "' where pcmid =" + pcMappings.Pcmid;
                try
                {
                    rlt = OleDbHelper.ExecuteNonQuery(commText);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return rlt;
        }

        public static int DeletePcMappingsById(int pcmId)
        {
            try
            {
                string comText = "delete from [tPcMappings] where tPcMappings.pcmid =" + pcmId;
                int rlt = OleDbHelper.ExecuteNonQuery(comText);
                return rlt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion 
 
		public static void Logout()
		{
			HttpContext.Current.Session["UserName"] = null;
			HttpContext.Current.Response.Redirect("login.aspx");
		}
    }
}
