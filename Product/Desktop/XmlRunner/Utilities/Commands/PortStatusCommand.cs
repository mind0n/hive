using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Drawing;

namespace Utilities.Commands
{
	public class PortStatusCommand : CommandNode
	{
		public string Ip_Address = "{0}";
		public string Port_Number = "{1}";
		public bool Is_Port_Opened;
        public bool Is_Wait;
		public int Retry_Interval = 1000;

		public override string Name
		{
			get
			{
				return string.Format("Check whether {0}'s port {1} {2} opened (Interval:{3}).", Ip_Address, Port_Number, Is_Port_Opened ? " is " : " is not ", Retry_Interval);
			}
		}
		public override CommandResult Execute(ULib.Controls.CommandTreeViewNode node = null)
		{
			CommandResult result = null;
			while (!isCancelling && (result == null || result.BoolResult != Is_Port_Opened))
			{
				result = CheckPortStatus(Ip_Address, Port_Number);
				if (!result.IsSuccessful)
				{
					break;
                }
                if (!Is_Wait)
                {
                    string content = string.Concat("Port ", Port_Number, " of server ", Ip_Address, result.BoolResult ? " is " : " is not ", "available.");
                    if (result.BoolResult != Is_Port_Opened)
                    {
                        Output(Color.Red, content);
                    }
                    else
                    {
                        Output(Color.Green, content);
                    }
                    break;
                }
				Thread.Sleep(Retry_Interval);
			}
			base.AccomplishCancellation();
			return result;

			//int port = 456; //<--- This is your value
			//bool isAvailable = true;

			//// Evaluate current system tcp connections. This is the same information provided
			//// by the netstat command line application, just in .Net strongly-typed object
			//// form.  We will look through the list, and if our port we would like to use
			//// in our TcpClient is occupied, we will set isAvailable to false.
			//IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
			//TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

			//foreach (TcpConnectionInformation tcpi in tcpConnInfoArray)
			//{
			//    if (tcpi.LocalEndPoint.Port == port)
			//    {
			//        isAvailable = false;
			//        break;
			//    }
			//}

			//// At this point, if isAvailable is true, we can proceed accordingly.
		}

		private static CommandResult CheckPortStatus(string ip, string portnum)
		{
			CommandResult result = new CommandResult();
			string hostname = ip;
			int portno = 0;
			if (int.TryParse(portnum, out portno))
			{
				IPAddress ipa = (IPAddress)Dns.GetHostAddresses(hostname)[0];
				try
				{
					Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					sock.Connect(ipa, portno);
					if (sock.Connected == true)  // Port is in use and connection is successful
					{
						result.BoolResult = true;
					}
					sock.Close();
				}
				catch (System.Net.Sockets.SocketException ex)
				{
					if (ex.ErrorCode == 10061)  // Port is unused and could not establish connection 
					{
						result.BoolResult = false;
					}
					else
					{
						result.LastError = ex;
					}
				}
				catch (Exception ex)
				{
					result.LastError = ex;
				}
			}
			else
			{
				result.LastError = new InvalidCastException();
			}
			return result;
		}

		public override CommandNode Clone()
		{
			PortStatusCommand cmd = new PortStatusCommand { Ip_Address = this.Ip_Address, Is_Port_Opened = this.Is_Port_Opened };
			base.CopyChildren(cmd);
			return cmd;
		}

		public override void Reset()
		{
			base.Reset();
		}
	}
}
