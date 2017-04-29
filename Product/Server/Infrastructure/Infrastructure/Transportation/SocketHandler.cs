using Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
	[Serializable]
	public abstract class SocketHandler
	{
		protected string HandlerName { get; }
		public SocketHandler(string handlerName)
		{
			HandlerName = handlerName;
		}
		public abstract void Handle(Socket accepted);
	}

	[Serializable]
	public class DomainSocketHandler : SocketHandler
	{
		public DomainSocketHandler(string handlerName) : base(handlerName)
		{
		}

		public override void Handle(Socket accepted)
		{
			var si = accepted.DuplicateAndClose(Process.GetCurrentProcess().Id);
			var ad = DomainHelper.Use(HandlerName);
			log.i($"Handling socket in domain: {AppDomain.CurrentDomain.FriendlyName}");
			DomainHelper.Execute(ad, (args) =>
			{
				var info = (SocketInformation)args[0];
				ProcessSocket(info);
			}, si);
		}

		protected virtual void ProcessSocket(SocketInformation info)
		{
			using (var psk = new Socket(info))
			{
				int len = 0;
				var buf = new byte[4096];
				while (true)
				{
					try
					{
						if (psk.Available > 0)
						{
							len = psk.Receive(buf);
							if (len < 1)
							{
								break;
							}
						}
						else
						{
							break;
						}
					}
					catch (Exception ex)
					{
						log.e(ex);
					}
				}
				string response =
					$@"HTTP/1.0 200 OK
Connection:close
Content-Type:text/html; charset=utf-8

<html><body>{AppDomain.CurrentDomain.FriendlyName}</body></html>

";
				UTF8Encoding en = new UTF8Encoding();
				var bytes = en.GetBytes(response);
				psk.Send(bytes);
			}
		}
	}
}
