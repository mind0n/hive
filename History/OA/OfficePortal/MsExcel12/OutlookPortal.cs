using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestOffice
{
	public class OutlookPortal
	{
		string vto, vcc, vbcc, vsubject, vbody;
		Microsoft.Office.Interop.Outlook.Application oApp;
		Microsoft.Office.Interop.Outlook._NameSpace oNameSpace;
		Microsoft.Office.Interop.Outlook.MAPIFolder oOutboxFolder;
		Microsoft.Office.Interop.Outlook._MailItem oMailItem;
		public OutlookPortal()
		{
			oApp = new Microsoft.Office.Interop.Outlook.Application();
			oNameSpace = oApp.GetNamespace("MAPI");
			oOutboxFolder = oNameSpace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderOutbox);
			oNameSpace.Logon(null, null, false, false);
			oMailItem =	(Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
			
		}
		public void SetValue(string to, string body, string cc, string subject)
		{
			if (!string.IsNullOrEmpty(to))
			{
				oMailItem.To = to;
			}
			if (!string.IsNullOrEmpty(cc))
			{
				oMailItem.CC = cc;
			}
			if (!string.IsNullOrEmpty(subject))
			{
				oMailItem.Subject = subject;
			}
			if (!string.IsNullOrEmpty(body))
			{
				oMailItem.Body = body;
				//oMailItem.
			}
		}
		public void Send()
		{
			oMailItem.Send();
		}
	}
}
