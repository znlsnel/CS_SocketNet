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
			 Console.WriteLine("===========Client===============");
			string host = Dns.GetHostName();
			IPHostEntry ipHost = Dns.GetHostEntry(host);
			//string Ip6Address = "fe80::e03e:ef3d:d51c:74be%7"; 
			//string Ip6Address = "192.168.219.200"; 
			//string IpAddressLaptop = "192.168.219.101";

			//IPAddress ipAddr = IPAddress.Parse(IpAddressLaptop);
			IPAddress ipAddr = ipHost.AddressList[0];


			IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);
			 
			Connector connector = new Connector();
			//connector.Connect(endPoint, () => { return SessionManager.Instance.Generate(); }, 1);   

			Console.WriteLine("이름을 입력해주세요");
			string name = "";
			name = Console.ReadLine();

			connector.Connect(endPoint, () => { return SessionManager.Instance.Generate(name); }, 1);

			while (true)  
			{
				try 
				{
					string chatString = Console.ReadLine(); 
					//connector.Connect(endPoint, () => { return new ServerSession(); });
					Console.SetCursorPosition(0, Console.CursorTop - 1);

					// 현재 줄에 공백을 출력하여 내용을 지웁니다.
					Console.Write(new string(' ', Console.WindowWidth));
					Console.SetCursorPosition(0, Console.CursorTop);
					SessionManager.Instance.SendForEach(chatString);
					 
				}
				catch (Exception ex) {
				    Console.WriteLine(ex);
				}
				//	Thread.Sleep(250);



			}
		}
	}
} 
