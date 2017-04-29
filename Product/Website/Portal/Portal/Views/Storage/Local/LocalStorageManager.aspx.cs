using System;
using System.Web.UI;

namespace Portal.Views.Storage.Local
{
    public partial class LocalStorageManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ClientIDMode = ClientIDMode.Static;
        }
    }
}