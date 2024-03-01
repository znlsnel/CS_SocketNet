using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServerCore;
  
namespace DummyClient
{  
	public class GameSession : Session
	{
		static int _dummyID = 0;
		public override void OnConnected(EndPoint endPoint)
		{ 
			Console.WriteLine($"OnConnected : {endPoint}");
			byte[] sendBuffer = Encoding.UTF8.GetBytes($"Hello Server! this is Client[{_dummyID++}]!");
			Send(sendBuffer);


		}

		public override void OnDisconnected(EndPoint endPoint)
		{
			Console.WriteLine($"OnDisConnected : {endPoint}");

		}

		public override int OnRecv(ArraySegment<byte> buffer)
		{
			string recvData = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
			Console.WriteLine($"[From Server] {recvData}");
			 
			return buffer.Count;
		}

		public override void OnSend(int numOfBytes)
		{
			//	throw new NotImplementedException();
		}
	}

	class Program
	{
		static ServerConnector _connector = new ServerConnector();
		static void OnConnectCompleted(Socket serverSocket)
		{ 
			try  
			{
				ClientSession session = new ClientSession();
				session.Start(serverSocket);

				// 서버로 보내기
				//byte[] sendBuffer = Encoding.UTF8.GetBytes($"Hello Server! this is Client[{_dummyID++}]!");
				//session.Send(sendBuffer);  
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
			 
			Connector connector = new Connector();
			connector.Connect(endPoint, () => { return new GameSession(); });   

			      
			while (true) 
			{
				connector.Connect(endPoint, () => { return new GameSession(); });
				Thread.Sleep(100);
				//string chatString = Console.ReadLine();
				//byte[] chatData = Encoding.UTF8.GetBytes(chatString);
				//Console.SetCursorPosition(0, Console.CursorTop - 1);

				//// 현재 줄에 공백을 출력하여 내용을 지웁니다.
				//Console.Write(new string(' ', Console.WindowWidth));
				//Console.SetCursorPosition(0, Console.CursorTop);

				//Session session = connector.GetSession();
				//session.Send(chatData);

			
			} 
		}
	}
} 
