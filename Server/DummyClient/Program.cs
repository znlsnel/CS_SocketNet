using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace DummyClient
{
	class Program
	{ 
		static void Main(string[] args)
		{
			 Console.WriteLine("===========Im Client===============");
			string host = Dns.GetHostName();
			IPHostEntry ipHost = Dns.GetHostEntry(host);
			//string Ip6Address = "fe80::e03e:ef3d:d51c:74be%7"; 
			string Ip6Address = "192.168.219.200"; 
			 
			//	IPAddress ipAddr = ipHost.AddressList[0];
			IPAddress ipAddr = IPAddress.Parse(Ip6Address);
			 
			IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

			int dummyID = 0;
			while (true)
			{
				Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				 
				try
				{
					socket.Connect(endPoint);
					//Console.WriteLine($"Connected To {socket.RemoteEndPoint.ToString()}");

					// 서버로 보내기
					byte[] sendBuffer = Encoding.UTF8.GetBytes($"Hello Server! this is Client[{dummyID++}]!");
					int sendBytes = socket.Send(sendBuffer);
					 
					// 서버에게 받기
					byte[] recvBuff = new byte[1024];
					int recvBuffers = socket.Receive(recvBuff);
					string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvBuffers);
					Console.WriteLine($"[From Server] {recvData}");

				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}

				Thread.Sleep(100); 
			}

        }
	}
}
