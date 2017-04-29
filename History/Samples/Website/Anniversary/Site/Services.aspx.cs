using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Joy.Server.Web;
using Joy.Server.Data;
using DAL;
using Joy.Server.Authentication;
using DAL.Entities;
using Joy.Core;
using Joy.Server;
using System.Data;
using System.Text;

namespace Site
{
	public partial class Services : ServicePage
	{
		public string SaveMessage(string name, string pwd, string content, int code)
		{
			if (code == AuthCodePage.CurrentValue)
			{
				SqlObject sqlObj = new SqlObject();
				const string sqlGetUser = "select * from tusers where uname like '{0}'";
				const string sqlUpdUser = "update tusers set uname='{0}', upwd='{1}', utext='{0}', ulevel=3 where userid={2}";
				const string sqlGetCategoryByName = "select categoryid from tcategories where caption like '祝福寄语'";
				const string sqlInsArticle = "insert into tarticles(categoryid, userid, caption,visible, content, articletype) values({0}, {1}, '{2}', {4}, '{3}', 3)";
				if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pwd))
				{
					throw new CustomException("用户名或密码不正确");
				}
				name = SqlHelper.MakeSafeFieldValue(name, "str");
				pwd = SqlHelper.MakeSafeFieldValue(pwd, "str");
				content = SqlHelper.MakeSafeFieldValue(content, "str");

				List<User> rlt = D.DB.ExecuteList<User>(string.Format(sqlGetUser, name));
				int cid = D.DB.ExecScalar<int>(sqlGetCategoryByName);
				if (cid <= 0)
				{
					throw new CustomException("类别不存在: 祝福寄语");
				}
				if (rlt == null || rlt.Count < 1)
				{
					int id = D.DB.ExecuteInsert("tusers", "uname", "userid");
					if (id > 0)
					{
						D.DB.Execute(string.Format(sqlUpdUser, name, pwd, id.ToString()));
						D.DB.ExecuteNonQuery(sqlInsArticle, cid.ToString(), id.ToString(), "祝福寄语", content, "False");
					}
				}
				else
				{
					User u = rlt[0];
					if (!string.Equals(u.UPwd, pwd, StringComparison.Ordinal))
					{
						throw new CustomException("用户名或密码不正确");
					}
					D.DB.Execute(string.Format(sqlInsArticle, cid, u.UserId, "祝福寄语", content, u.ULevel < 3 ? "True" : "False"));
				}
				return "祝福提交已成功，需等待管理员审核。";
			}
			throw new CustomException("验证码不对，请重新输入");
		}

		public void AddCache(string cname, string value, string pname)
		{
			const string template = "参数错误：{0}";
			SiteCache c = D.Cache;
			if (string.IsNullOrEmpty(cname))
			{
				throw new IgnorableException(template, "cname");
			}
			if (!string.IsNullOrEmpty(pname))
			{
				c = c.Retrieve(pname);
			}
			SiteCache item = c.Retrieve(cname);
			item.Value = value;
		}
		public string GetTotalCache()
		{
			return Server.UrlEncode(D.Cache.ToXml()).Replace('+', ' ');
		}
		public void SaveCache(string id)
		{
			string sql = null;
			if (D.Cache != null)
			{
				if (string.IsNullOrEmpty(id))
				{
					id = Guid.NewGuid().ToString();
				}
				string content = D.Cache.ToXml();
				DataTable table = D.DB.GetDataTable(string.Format("select * from tCaches where id like '{0}'", id));
				if (table == null || table.Rows.Count < 1)
				{
					sql = "insert into tCaches(id, content) values('{0}', '{1}')";
				}
				else
				{
					sql = "update tCaches set content='{1}' where id like '{0}'";
				}
				D.DB.ExecuteNonQuery(sql, SqlHelper.MakeSafeFieldValue(id, "str"), SqlHelper.MakeSafeFieldValue(content, "str"));
			}
		}
		public string LoadCache(string id)
		{
			string sql = "select * from tCaches ";
			if (!string.IsNullOrEmpty(id))
			{
				sql += string.Concat(" where id like '", SqlHelper.MakeSafeFieldValue(id, "str"), "'");
			}
			DataTable table = D.DB.GetDataTable(sql);
			if (table != null)
			{
				StringBuilder b = new StringBuilder();
				if (table.Rows.Count < 0)
				{
					throw new IgnorableException("缓存为空");
				}
				else if (table.Rows.Count > 1)
				{
					foreach (DataRow r in table.Rows)
					{
						b.Append(r["id"]).Append("=").Append(Server.JsEncode(r["content"])).Append(";");
					}
					return b.ToString();
				}
				else
				{
					DataRow r = table.Rows[0];
					string cache = r["content"] as string;
					if (!string.IsNullOrEmpty(cache))
					{
						D.Cache = cache.FromXml<SiteCache>();
					}
					b.Append(r["id"]).Append("=").Append(Server.JsEncode(r["content"])).Append(";");
					return b.ToString();
				}
			}
			throw new IgnorableException("缓存未找到：{0}", id);
		}
	}
}