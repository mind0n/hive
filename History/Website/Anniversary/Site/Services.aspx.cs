using System.Globalization;
using DAL;
using DAL.Entities;
using Joy.Core;
using Joy.Server;
using Joy.Server.Authentication;
using Joy.Server.Core;
using Joy.Server.Data;
using Joy.Server.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using Site.Admin;

namespace Site
{
	public partial class Services : ServicePage
	{
		public string ListFiles()
		{
			return string.Empty;
		}

		public string SaveFile()
		{
			HttpContext context = HttpContext.Current;
			context.Response.ContentType = "text/plain";
			context.Response.Clear();
			List<string> urls = ProcessFiles(context);
			string rlt = string.Concat("['", string.Join("','", urls.ToArray()), "']");
			return rlt;
		}

		public string SaveMessage(string name, string pwd, string content, int code)
		{
			if (code == AuthCodePage.CurrentValue)
			{
				//SqlObject sqlObj = new SqlObject();
				const string sqlGetUser = "select * from tusers where uname like '{0}'";
				const string sqlUpdUser = "update tusers set uname='{0}', upwd='{1}', utext='{0}', ulevel=3 where userid={2}";
				const string sqlGetCategoryByName = "select categoryid from tcategories where caption like '祝福寄语'";
				const string sqlInsArticle = "insert into tarticles(categoryid, userid, caption,visible, content, articletype) values({0}, {1}, '{2}', {4}, '{3}', 3)";
				if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pwd))
				{
					throw new CustomException("用户名或密码不正确");
				}
				name = SqlHelper.MakeSafeFieldValue(name);
				pwd = SqlHelper.MakeSafeFieldValue(pwd);
				content = SqlHelper.MakeSafeFieldValue(content);

				List<UserItem> rlt = D.DB.ExecuteList<UserItem>(string.Format(sqlGetUser, name));
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
						D.DB.Execute(string.Format(sqlUpdUser, name, pwd, id.ToString(CultureInfo.InvariantCulture)));
						D.DB.ExecuteNonQuery(sqlInsArticle, cid.ToString(CultureInfo.InvariantCulture), id.ToString(CultureInfo.InvariantCulture), "祝福寄语", content, "False");
					}
				}
				else
				{
					UserItem u = rlt[0];
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
			if (D.Cache != null)
			{
				if (string.IsNullOrEmpty(id))
				{
					id = Guid.NewGuid().ToString();
				}
				string content = D.Cache.ToXml();
				DataTable table = D.DB.GetDataTable(string.Format("select * from tCaches where id like '{0}'", id));
				string sql;
				if (table == null || table.Rows.Count < 1)
				{
					sql = "insert into tCaches(id, content) values('{0}', '{1}')";
				}
				else
				{
					sql = "update tCaches set content='{1}' where id like '{0}'";
				}
				D.DB.ExecuteNonQuery(sql, SqlHelper.MakeSafeFieldValue(id), SqlHelper.MakeSafeFieldValue(content));
			}
		}

		public string LoadCache(string id)
		{
			string sql = "select * from tCaches ";
			if (!string.IsNullOrEmpty(id))
			{
				sql += string.Concat(" where id like '", SqlHelper.MakeSafeFieldValue(id), "'");
			}
			DataTable table = D.DB.GetDataTable(sql);
			if (table != null)
			{
				StringBuilder b = new StringBuilder();
				if (table.Rows.Count < 0)
				{
					throw new IgnorableException("缓存为空");
				}
				if (table.Rows.Count > 1)
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

		private static List<string> ProcessFiles(HttpContext context)
		{
			List<string> urls = new List<string>();
			var cidsel = new SqlObject();
			cidsel.Select("categoryid").From("tCategories").Where("iname", "like", "upload");

			string sql = cidsel.ToString();
			//var tb = D.DB.GetDataTable(sql);
			var list = D.DB.ExecuteValue(sql);
			if (list == null)
			{
				throw new CustomException("无法找到文件上传对应的类别。");
			}
			int cid = (int)list;
			for (int i = 0; i < context.Request.Files.Count; i++)
			{
				FileItem file = ProcessSingleFile(context, urls, i);
				if (file != null)
				{
					file.CategoryId = cid;
					var fins = new SqlObject();
					fins.InsertInto(file);
					sql = fins.ToString();
					D.DB.ExecuteNonQuery(sql);
				}
			}
			return urls;
		}

		private static FileItem ProcessSingleFile(HttpContext context, List<string> urls, int i)
		{
			HttpPostedFile postedFile = context.Request.Files[i];
			byte[] filedata = new byte[postedFile.ContentLength];
			if (filedata.Length > 0)
			{
				postedFile.InputStream.Read(filedata, 0, postedFile.ContentLength);
				string filename = string.Concat(postedFile.FileName, "_", Guid.NewGuid().ToString(), postedFile.FileName.FileExt(true));
				string uploadDir = string.Concat(JoyConfig.Instance.RootUrl, "upload/");
				if (!Directory.Exists(context.Server.MapPath(uploadDir)))
				{
					Directory.CreateDirectory(context.Server.MapPath(uploadDir));
				}
				string fileurl = uploadDir + filename;
				string fullname = HttpContext.Current.Server.MapPath(fileurl);
				if (filedata.LongLength > 0)
				{
					File.WriteAllBytes(fullname, filedata);
					urls.Add(fileurl);
				}

				FileItem entity = new FileItem();
				entity.DisplayName = postedFile.FileName;
				entity.Filename = fileurl;
				UserItem user = Login.LoginUser;
				entity.UserId = user == null ? 0 : user.UserId;
				return entity;
			}
			return null;
		}
	}
}