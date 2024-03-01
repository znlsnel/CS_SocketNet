using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore
{
	public class Connector
	{

		Func<Session> _sessionFactory;
		private Session _currentSession;
		public void Connect(IPEndPoint endPoint, Func<Session> sessionFactory)
		{
			_sessionFactory += sessionFactory;
			 
			Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

			SocketAsyncEventArgs args = new SocketAsyncEventArgs();
			args.Completed += new EventHandler<SocketAsyncEventArgs>(OnConnectCompleted);
			args.RemoteEndPoint = endPoint;
			args.UserToken = socket;

			RegisterConnect(args);
		}

		public Session GetSession() 
		{
			if (_currentSession == null)
				_currentSession = _sessionFactory.Invoke();
			 
			return _currentSession;
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

		void OnConnectCompleted(Object sender, SocketAsyncEventArgs args)
		{
			if (args.SocketError == SocketError.Success)
			{
				Session session = _sessionFactory.Invoke(); 
				session.Start(args.ConnectSocket);
				session.OnConnected(args.RemoteEndPoint); 
				_currentSession = session;
			} 
			else 
			{
				Console.WriteLine($"OnConnectedCompleted Fail: {args.SocketError.ToString()}");
			}
		}

	}
}
