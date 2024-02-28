using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DummyClient
{
	class ClientSession
	{
		Socket _socket;
		int _disconnected = 0;
		 
		public void Start (Socket socket)
		{
			_socket = socket;

			SocketAsyncEventArgs recvArgs = new SocketAsyncEventArgs();
			recvArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnRecvCompleted);
			recvArgs.SetBuffer(new byte[1024], 0, 1024);
			RegisterRecv(recvArgs);
		}
		public void Send(byte[] data)
		{
			SocketAsyncEventArgs sendArgs = new SocketAsyncEventArgs();
			sendArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnSendCompleted);
			sendArgs.SetBuffer(data, 0, data.Length);

			RegisterSend(sendArgs);
		}

		public void Disconnect()
		{
			if (Interlocked.Exchange(ref _disconnected, 1) == 1)
				return;

			_socket.Shutdown(SocketShutdown.Both);
			_socket.Close();
		}

		#region 네트워크 통신
		void RegisterRecv(SocketAsyncEventArgs args)
		{
			bool pending = _socket.ReceiveAsync(args);
			if (pending == false)
			{
				OnRecvCompleted(null, args);
			}
		}
		void RegisterSend(SocketAsyncEventArgs args)
		{
			bool pending = _socket.SendAsync(args);
			if (pending == false)
			{
				OnSendCompleted(null, args);
			}
		}
		void OnSendCompleted(object sender, SocketAsyncEventArgs args)
		{
			if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
			{
				try
				{

				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
			}
		}

		void OnRecvCompleted(object sender, SocketAsyncEventArgs args)
		{
			if (args.SocketError == SocketError.Success)
			{
				try
				{
					string recvData = Encoding.UTF8.GetString(args.Buffer, args.Offset, args.BytesTransferred);
					Console.WriteLine($"[From Server] {recvData}"); 
					 
					RegisterRecv(args);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.ToString());
				}

			}
			else
			{
				// TODO Disconnect
			}

		}
		#endregion

	}
}
