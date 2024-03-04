using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using ServerCore;
  
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
			//string Ip6Address = "192.168.219.200"; 
			string IpAddressLaptop = "192.168.219.101";

			IPAddress ipAddr = IPAddress.Parse(IpAddressLaptop);

			IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);
			 
			Connector connector = new Connector();
			connector.Connect(endPoint, () => { return new ServerSession(); });   
			 
			      
			while (true)  
			{ 
			//	connector.Connect(endPoint, () => { return new ServerSession(); });
				//Thread.Sleep(100);
				//string chatString = Console.ReadLine(); 
				//Console.SetCursorPosition(0, Console.CursorTop - 1);

				//// 현재 줄에 공백을 출력하여 내용을 지웁니다.
				//Console.Write(new string(' ', Console.WindowWidth));
				//Console.SetCursorPosition(0, Console.CursorTop);

				//Session session = connector.GetSession();

				//Packet msg = new Packet(10, 53, "김우디");
				//ArraySegment<byte> openSegment = SendBufferHelper.Open(4096);
				//byte[] buffer = BitConverter.GetBytes(msg._size);
				//byte[] buffer2 = BitConverter.GetBytes(msg._packetId);
				//byte[] buffer3 = Encoding.UTF8.GetBytes(msg._name);

				//Array.Copy(buffer, 0, openSegment.Array, openSegment.Offset, buffer.Length);
				//Array.Copy(buffer2, 0, openSegment.Array, openSegment.Offset + buffer.Length, buffer2.Length);
				//Array.Copy(buffer3, 0, openSegment.Array, openSegment.Offset + buffer.Length + buffer2.Length, buffer3.Length);
				//ArraySegment<byte> sendBuff = SendBufferHelper.close(msg._size);



				//session.Send(sendBuff);


			}
		}
	}
} 
