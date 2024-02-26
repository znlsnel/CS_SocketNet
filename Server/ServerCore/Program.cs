using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{ 
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("===========Im Server===============");

			// DNS  (Domain Name System)
			// 172.1.2.3 이러한 IP 주소를 www.naver.com 과 같은 형태로 바꿔줌
			string host = Dns.GetHostName();
			IPHostEntry ipHost =  Dns.GetHostEntry(host);
			IPAddress ipAddr =  ipHost.AddressList[0];
			IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

			Socket listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				listenSocket.Bind(endPoint);
				// backing : 최대 대기수
				listenSocket.Listen(10);

				while (true)
				{
					Console.WriteLine("Listening...");

					// client와의 대화는 이 socket을 통해서..
					Socket clientSocket = listenSocket.Accept();

					// client에게 메시지를 받는다
					// 아래 recvBuff는 임시로 만든거
					byte[] recvBuff = new byte[1024];
					int recvBytes = clientSocket.Receive(recvBuff); // recvBuff의 크기를 반환
					string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvBytes);
					Console.WriteLine($"[From Client] {recvData}");

					// client에게 보낸다
					byte[] sendBuff = Encoding.UTF8.GetBytes("hello client! this is server!");
					clientSocket.Send(sendBuff);

					// Client와의 연결을 끊기
					clientSocket.Shutdown(SocketShutdown.Both);
					clientSocket.Close();
				} 
			}
			catch (Exception ex) { 
				Console.WriteLine(ex.ToString());
			}
			 
			
		}

	}
}