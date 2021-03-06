﻿using System;
using System.Collections.Generic;
using System.Data;
using DAL;
using Joy.Core;
using Joy.Server;
using Joy.Server.Data;
using Site.Data;
using System.Reflection;
using Joy.Server.Widgets;
using Joy.Server.Core;

namespace Site.Data
{
	public class PageBase : WebPage
	{
		public string RootUrl
		{
			get
			{
				return JoyConfig.Instance.RootUrl;
			}
		}
		protected override void OnLoad(EventArgs e)
		{
			string sqlArticle = "select top 5 * from vrArticleCategoryMapping where pid like '" + this.PageId + "' and clientvisible=True and categoryid=";
			string sqlUnionArticle = string.Concat(" union ", sqlArticle);
			string sqlCategories = SqlHelper.MakeSelectSql("*", "vPageCategories", string.Format("pid like '{0}' and visible=true", this.GetType().Name), null);
			GetAndRegisterArticlesByPage(sqlArticle, sqlUnionArticle, sqlCategories);
			base.OnLoad(e);
		}
		protected void GetAndRegisterArticlesByPage(string sqlArticle, string sqlUnionArticle, string sqlCategories)
		{
			GridWidget grid = GetArticlesByPage(sqlArticle, sqlUnionArticle, sqlCategories);
			if (grid == null)
			{
				return;
			}
			grid.Split("categoryid", "categoryname", "container", "categoryname", "categoryid", "field=categoryid", "url=content.aspx?cid=", "widgetname", "widgetsettings");
			RegistVariable(this, "categridgroup", grid.ToJson(new JsonSerializer.SerializeCallbackHandler(delegate(MemberInfo mi, string name, object value)
			{
				if (string.Equals("articleupdate", name, StringComparison.OrdinalIgnoreCase))
				{
					if (value is DateTime)
					{
						DateTime dt = (DateTime)value;
						return dt.ToString("yyyy/MM/dd");
					}
				}
				return value;
			})));

		}
		protected GridWidget GetArticlesByPage(string sqlTemplate, string sqlUnionTemplate, string sql)
		{
			ExecuteResult rlt = D.DB.ExecuteReader(sql, new Func<IDataReader, object>(delegate(IDataReader reader)
			{
				List<Category> list = reader.Fill<Category>();
				string[] array = list.ToArray<string>("CategoryId");
				return array;
			}));
			string[] cids = rlt.ObjRlt as string[];
			if (cids == null)
			{
				return null;
			}
			sql = string.Concat(sqlTemplate, string.Join(sqlUnionTemplate, cids), " order by morder, categoryname, articleupdate desc");
			rlt = D.DB.ExecuteReader(sql, new Func<IDataReader, object>(delegate(IDataReader reader)
			{
				GridWidget g = new GridWidget(reader);
				Widget dstyle = g["defaultStyle"] as Widget;
				Widget style = g["styles"] as Widget;
				Widget colCaption = g.Columns("caption");
				colCaption.Use<Widget>("url", string.Concat("content.aspx?aid={0}"));
				colCaption.Use<Widget>("pk", "articleid");
				g["hideCaptions"] = true;
				g["hideFooter"] = true;
				dstyle["display"] = "none";
				style.Use<Widget>("caption").Use<Widget>("display", string.Empty);
				style.Use<Widget>("articleupdate").Use<Widget>("display", string.Empty);
				return g;
			}));
			GridWidget grid = rlt.ObjRlt as GridWidget;
			return grid;
		}
	}
}