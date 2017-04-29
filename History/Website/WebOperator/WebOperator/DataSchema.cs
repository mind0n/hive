using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WebOperator
{
	class TrainCollection : List<Train>
	{
	}
	class Train
	{
		public string Name;
		public string Id;
	}
	enum FormDataType
	{
		Input,
		Action,
		Checkbox,
		Script,
		DropDown
	}
	class FormDataItem
	{
		public string Id;
		public string Value;
		public int Index;
		public bool Checked;
		public string Script;
		public FormDataType Type = FormDataType.Input;
		public Action<HtmlElement> Action;
	}
	class FormDataCollection : List<FormDataItem>
	{
		
	}
	class FormData
	{
		public static FormDataCollection QueryForm = new FormDataCollection();
		public static FormDataCollection BookForm = new FormDataCollection();
		public static FormDataCollection LoginForm = new FormDataCollection();
		static FormData()
		{
			DateTime d = DateTime.Now + new TimeSpan(11, 0, 0, 0);
			string text = d.ToString("yyyy-MM-dd");
			LoginForm.Add(new FormDataItem { Id = "Username", Value = "mind0n" });
			LoginForm.Add(new FormDataItem { Id = "password", Value = "332211" });
			LoginForm.Add(new FormDataItem
			{
				Id = "randCode",
				Type = FormDataType.Action,
				Action = new Action<HtmlElement>(delegate(HtmlElement el)
				{
					el.Focus();
				})
			});
			QueryForm.Add(new FormDataItem { Id = "toStationText", Value = "上海" });
			QueryForm.Add(new FormDataItem { Id = "toStation", Value = "SHH" });
			QueryForm.Add(new FormDataItem { Id = "fromStationText", Value = "商丘" });
			QueryForm.Add(new FormDataItem { Id = "fromStation", Value = "SQF" });
			QueryForm.Add(new FormDataItem { Id = "startdatepicker", Value = text });
			BookForm.Add(new FormDataItem { Script = "var el = document.getElementById('showPassengerFilter'); if (el) { var inputs = el.getElementsByTagName('input'); if (inputs) { for (var i in inputs) { $(i).trigger('click'); } } }", Type = FormDataType.Script });
			//ConfirmForm.Add(new FormDataItem { Script = "var passengerJson = [{'first_letter':'MIND0N','isUserSelf':'','mobile_no':'13774352220','old_passenger_id_no':'','old_passenger_id_type_code':'','old_passenger_name':'','passenger_id_no':'310113198610222134','passenger_id_type_code':'1','passenger_id_type_name':'','passenger_name':'袁敏捷','passenger_type':'1','passenger_type_name':'','recordCount':'2'},{'first_letter':'LJ','isUserSelf':'','mobile_no':'15800558628','old_passenger_id_no':'','old_passenger_id_type_code':'','old_passenger_name':'','passenger_id_no':'640121198804121709','passenger_id_type_code':'1','passenger_id_type_name':'','passenger_name':'刘佳','passenger_type':'1','passenger_type_name':'','recordCount':'2'}]; var el = document.getElementById('showPassengerFilter');if (el){var inputs = el.getElementsByTagName('input');if (inputs){for(var i in inputs){click_passenger_ticket(passengerJson, true, i);	}}}", Type = FormDataType.Script });
			
		}
	}
}
