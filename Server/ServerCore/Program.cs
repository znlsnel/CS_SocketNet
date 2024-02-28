using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ServerCore
{  

	class Program
	{
		static Listener _listener = new Listener();
		static int count = 0;
		static void OnAcceptHandler(Socket clientSocket)
		{

			try
			{
				Session session = new Session(); 
				session.Start(clientSocket);
				 
				// client에게 보낸다 
				byte[] sendBuff = Encoding.UTF8.GetBytes($"hello client! this is server{count++}!");
				session.Send(sendBuff);
				 
				//Thread.Sleep(10);
				session.Disconnect();
				
			}
			catch (Exception ex)
			{
				//Console.WriteLine(ex.ToString());
				Console.Write("o ");
			}


		}

		static void Main(string[] args)
		{
			Console.WriteLine("===========Im Server===============");
			 
			// DNS  (Domain Name System)
			// 172.1.2.3 이러한 IP 주소를 www.naver.com 과 같은 형태로 바꿔줌
			string host = Dns.GetHostName();
			//string IpAddress = "fe80::e03e:ef3d:d51c:74be%7";
			string IpAddress = "192.168.219.200";

			IPHostEntry ipHost =  Dns.GetHostEntry(host);
			//IPAddress ipAddr =  ipHost.AddressList[0];
			   
			IPAddress ipAddr = IPAddress.Parse(IpAddress);
			IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

			_listener.Init(endPoint, OnAcceptHandler);

			while (true)
			{
				;
			}

		}

	}
}