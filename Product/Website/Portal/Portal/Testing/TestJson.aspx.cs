using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Testing
{
    public partial class TestXml : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			if (Page.IsPostBack)
			{
				string s = Request.Form["box"];
				JavaScriptSerializer ser = new JavaScriptSerializer();
				Person o = ser.Deserialize(s, typeof(Person)) as Person;
				box.Value = ser.Serialize(o);
				
			}
        }
    }
	class Person
	{
		public string name { get; set; }
		public int age { get; set; }
		public Person son;
		public List<Person> children;
	}
}