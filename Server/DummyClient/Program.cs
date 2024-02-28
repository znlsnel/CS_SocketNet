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
		static ServerConnector _connector = new ServerConnector();
		static int _dummyID = 0;
		static void OnConnectCompleted(Socket serverSocket)
		{ 
			try  
			{
				ClientSession session = new ClientSession();
				session.Start(serverSocket);

				// 서버로 보내기
				byte[] sendBuffer = Encoding.UTF8.GetBytes($"Hello Server! this is Client[{_dummyID++}]!");
				session.Send(sendBuffer);  
				//serverSocket.SendA
			}
			catch (Exception ex)
			{
				//Console.WriteLine(ex.ToString());
				Console.Write("o "); 
			} 

		}

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
			 
			   
			while (true)
			{
				//_connector.Init(endPoint, OnConnectCompleted);
				try 
				{ 
					Socket serverSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
					serverSocket.Connect(endPoint);

					byte[] sendBuffer = Encoding.UTF8.GetBytes($"Hello Server! this is Client[{_dummyID++}]!");
					serverSocket.Send(sendBuffer);

					byte[] recvBuff = new byte[1024];
					int recvBytes = serverSocket.Receive(recvBuff);
					string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvBytes);

					Console.WriteLine($"[From Server] {recvData}");
				}
				catch (Exception ex)
				{ 
					Console.WriteLine(ex.ToString());
				}

				Thread.Sleep(1); 
			}

        }
	}
}
