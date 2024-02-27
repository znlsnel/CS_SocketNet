using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DummyClient
{
	class ServerConnector
	{
		Socket _serverSocket;
		Action<Socket> _onAcceptHandler;
		public void Init(IPEndPoint endPoint, Action<Socket> onAcceptHandler)
		{
			_onAcceptHandler += onAcceptHandler;

			_serverSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

			SocketAsyncEventArgs args = new SocketAsyncEventArgs();
			args.RemoteEndPoint = endPoint;
			args.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
			_serverSocket.ConnectAsync(args);
		}


		void OnAcceptCompleted(Object sender, SocketAsyncEventArgs args)
		{
			if (args.SocketError == SocketError.Success)
				_onAcceptHandler.Invoke(args.ConnectSocket);
			else
			{
				Console.WriteLine(args.SocketError.ToString());
				_serverSocket.ConnectAsync(args);
			}
		}

	}
}
