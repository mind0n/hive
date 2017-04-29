using Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
	/// <summary>
	/// Listening on port or connect remote port
	/// </summary>
	public class PassiveNode : IDisposable
	{
		protected bool isending;
		protected AddressFamily afamily;
		protected SocketType stype;
		protected ProtocolType ptype;

		public PassiveNode(AddressFamily af = AddressFamily.InterNetwork, SocketType st = SocketType.Stream, ProtocolType pt = ProtocolType.Tcp)
		{
			afamily = af;
			stype = st;
			ptype = pt;
		}

		public void Listen(dynamic settings)
		{
			var port = settings.Port;
			var threads = settings.Threads;
			var htype = Type.GetType(settings.Handler);
			if (htype == null)
			{
				return;
			}
			if (port > 0 && port < 65536)
			{
				for (int i = 0; i < threads; i++)
				{
					Thread th = new Thread(new ThreadStart(() =>
					{
						using (var listenSocket = new Socket(afamily, stype, ptype))
						{
							try
							{
								IPEndPoint iep = new IPEndPoint(IPAddress.Any, port);
								listenSocket.Bind(iep);
								listenSocket.Listen(port);

								log.i($"Thread {Thread.CurrentThread.ManagedThreadId} listening on port {port}");

								while (!isending)
								{
									Socket ask = null;
									try
									{
										var handler = new DomainSocketHandler(GetType().FullName);
										ask = listenSocket.Accept();
										handler.Handle(ask);

									}
									catch (Exception ex)
									{
										log.e(ex.ToString());
									}
								}
							}
							catch (Exception ex)
							{
								log.e(ex);
							}
						}
					}));
					th.Start();
				}
			}
		}

		public void Dispose()
		{
			isending = true;
		}

	}
}
