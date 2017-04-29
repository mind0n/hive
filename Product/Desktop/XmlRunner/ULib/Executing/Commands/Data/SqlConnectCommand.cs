using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using ULib.Output;
using System.Windows.Forms;
using ULib.Controls;
using System.Data.SqlClient;
using ULib.Exceptions;

namespace ULib.Executing.Commands.Data
{
	[Command(IsConfigurable = true)]
	public class SqlConnectCommand : UICommandNode
    {
		[ParameterAttribute(IsRequired=true)]
        public string Server;
        public string User;
        public string Pwd;
        public bool IsTrusted;
		public override string Name
		{
			get
			{
				return string.Format("Sql connection to {0} with {1}", Server, IsTrusted ? "windows authentication" : "user " + User);
			}
			set
			{
				base.Name = value;
			}
		}

        public override CommandNode Clone()
        {
            return new SqlConnectCommand {Id=Id, Server = Server, User = User, Pwd = Pwd, IsTrusted = IsTrusted };
        }
		public override void OnGenerateCompleted(AutoSettingPanel sender)
		{
			TextBox userEd = Get<TextBox>("user");
			TextBox pwdEd = Get<TextBox>("pwd");
			CheckBox isTrusted = Get<CheckBox>("istrusted");
			isTrusted.CheckedChanged += new EventHandler(isTrusted_CheckedChanged);
			sender.AddButton("Test Connection", onTestConnectionClick);
			userEd.Enabled = !isTrusted.Checked;
			pwdEd.Enabled = !isTrusted.Checked;
		}

		void onTestConnectionClick(object sender, EventArgs e)
		{
			using (SqlConnection conn = ExecuteSqlCommand.GetConnection(this.Id))
			{
				try
				{
					conn.Open();
					MessageBox.Show("Version: " + conn.ServerVersion, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
					conn.Close();
				}
				catch (Exception ex)
				{
					ExceptionHandler.Handle(ex);
				}
			}
		}

		void isTrusted_CheckedChanged(object sender, EventArgs e)
		{
			TextBox userEd = Get<TextBox>("user");
			TextBox pwdEd = Get<TextBox>("pwd");
			CheckBox isTrusted = sender as CheckBox;
			if (userEd != null)
			{
				userEd.Enabled = !isTrusted.Checked;
			}
			if (pwdEd != null)
			{
				pwdEd.Enabled = !isTrusted.Checked;
			}
		}
    }
}
