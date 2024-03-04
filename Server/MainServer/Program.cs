﻿using System.Net.Sockets;
using System.Net;
using System.Text;
using ServerCore;

namespace MainServer
{


	class Program
	{
		static Listener _listener = new Listener();
		static int count = 0;
		 

		static void Main(string[] args)
		{
			Console.WriteLine("===========Im Server===============");

			// DNS  (Domain Name System)
			// 172.1.2.3 이러한 IP 주소를 www.naver.com 과 같은 형태로 바꿔줌
			string host = Dns.GetHostName();
			//string IpAddress = "fe80::e03e:ef3d:d51c:74be%7";
		//	string IpAddressPC = "192.168.219.200";
			string IpAddressLaptop = "192.168.219.101";
			
			IPHostEntry ipHost = Dns.GetHostEntry(host);
			//IPAddress ipAddr =  ipHost.AddressList[0];

			IPAddress ipAddr = IPAddress.Parse(IpAddressLaptop);
			IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);
			 
			_listener.Init(endPoint, () =>{ return new ClientSession(); });
			 
			while (true)
			{
				;
			}

		}

	}
}