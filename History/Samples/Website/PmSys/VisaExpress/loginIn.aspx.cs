using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class loginIn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { lblMessage.Visible = false; 
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Request.Cookies["CheckCode"] == null)
        {
            lblMessage.Text = "您的浏览器设置已被禁用 Cookies，您必须设置浏览器允许使用 Cookies 选项后才能使用本系统。";
            lblMessage.Visible = true;
            return;
        }

        if (String.Compare(Request.Cookies["CheckCode"].Value, checkCodeTextBox.Text, true) != 0)
        {
            lblMessage.Text = "验证码错误，请输入正确的验证码。";
            lblMessage.Visible = true;
            return;
        }
        else
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["VisaManagermentConnectionString"].ConnectionString);
            string selectcmd = @"SELECT [SID], [loginID], [loginPWD], [userName], [userSex] FROM [userInfo] WHERE (([loginID] = @loginID) AND ([loginPWD] = @loginPW))";
            SqlCommand cmd = new SqlCommand(selectcmd, conn);
            cmd.Parameters.Add("loginID", System.Data.SqlDbType.VarChar).Value = userNameTextBox.Text;
            cmd.Parameters.Add("loginPW", System.Data.SqlDbType.VarChar).Value = userPWDTextBox.Text;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    lblMessage.Text = "Successful";
                    Session["userName"] = "admin";
                    Session["loginStatus"] = true;
                }
                else
                {
                    lblMessage.Text = "用户名或密码错误，请重新登录。";
                   
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
            }
        }
    }
}