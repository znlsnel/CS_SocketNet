using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using System.Collections.Specialized;

namespace ServerCore
{
	public class Connector
	{

		Func<Session> _sessionFactory;
		object _lock = new object();
		public void Connect(IPEndPoint endPoint, Func<Session> sessionFactory, int count = 1)
		{
			for (int i = 0; i < count; i++)
			{
				Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				_sessionFactory = sessionFactory;
			 

				SocketAsyncEventArgs args = new SocketAsyncEventArgs();
				args.Completed += new EventHandler<SocketAsyncEventArgs>(OnConnectCompleted);
				//args.Completed +=OnConnectCompleted;
				args.RemoteEndPoint = endPoint; 
				args.UserToken = socket;

				RegisterConnect(args);
			}
		}

		void RegisterConnect(SocketAsyncEventArgs args)
		{
			Socket socket = args.UserToken as Socket;
			if (socket == null)
				return;

			bool pending = socket.ConnectAsync(args);

			if (pending == false)
				OnConnectCompleted(null, args);

		}
		static int ConnectedCount = 0;
		void OnConnectCompleted(Object sender, SocketAsyncEventArgs args)
		{
				if (args.SocketError == SocketError.Success)
				{
					Session session = _sessionFactory.Invoke(); 
					session.Start(args.ConnectSocket);
					session.OnConnected(args.RemoteEndPoint); 
				}  
				else 
				{
					Console.WriteLine($"OnConnectedCompleted Fail: {args.SocketError.ToString()}");
				}
		}
	}
}
