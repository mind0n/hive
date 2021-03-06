﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.OleDb;
using DAL;
using Joy.Server.Core;
using Joy.Server.Authentication;
using Joy.Server.Data;
using DAL.Entities;


namespace Site.Admin
{
    public partial class Login : System.Web.UI.Page
    {
		public string RootUrl
		{
			get
			{
				return JoyConfig.Instance.RootUrl;
			}
		}
		public static bool IsLogin
		{
			get
			{
				return !string.IsNullOrEmpty(HttpContext.Current.Session["UserName"] == null ? null : HttpContext.Current.Session["UserName"].ToString());
			}
		}
	    public static UserItem LoginUser
	    {
		    get
		    {
			    var session = HttpContext.Current.Session;
			    return session["User"] as UserItem;
		    }

	    }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string sUserName = SqlHelper.MakeSafeFieldValue(txtUserName.Text);
            string sPassWord = SqlHelper.MakeSafeFieldValue(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassWord.Text.ToString(), "MD5"));

            //验证码检查
			if (!AuthCodePage.IsValidCode(this.txtChkcode.Text))
			{
				OKInfo.Text = "<font color='red'>验证码错误！！！</font>";
				return;
			}
			string strsql = string.Concat("select * from tUsers where uname='", sUserName, "' and (upwd='", sPassWord, "' or upwd='" + SqlHelper.MakeSafeFieldValue(txtPassWord.Text) + "')");
			string redirectUrl = null;
			D.DB.ExecuteReader(strsql, new Func<IDataReader, object>(delegate(IDataReader dr)
			{
				if (dr.Read())
				{
					UserItem user = new UserItem();
					user.UName = SqlHelper.MakeSafeFieldValue(txtUserName.Text);
					user.ULevel = (int)dr["ulevel"];
					user.UserId = (int) dr["userid"];
					Session["UserName"] = user.UName; //SqlHelper.MakeSafeFieldValue(txtUserName.Text);
					Session["UserType"] = user.ULevel;
					Session["User"] = user;
					OKInfo.Text = "登陆成功！！！";
					redirectUrl = "CategoriesManage.aspx";
				}
				else
				{
					Session["UserName"] = null;
                    Session["UserType"] =null;
					OKInfo.Text = "<font color='red'>用户名或密码错误，登陆不成功！！！</font>";
				}
				return null;
			}));
			if (!string.IsNullOrEmpty(redirectUrl))
			{
				Response.Redirect(redirectUrl);
			}
        }
    }
}