using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using ServerCore;
  
class Program 
{
		 
	static void Main(string[] args)
	{ 
			Console.WriteLine("===========Client===============");
		string host = Dns.GetHostName();
		IPHostEntry ipHost = Dns.GetHostEntry(host);
		//string Ip6Address = "fe80::e03e:ef3d:d51c:74be%7"; 
		string Ip6Address = "192.168.219.100"; 
		//string IpAddressLaptop = "192.168.219.101";

		//IPAddress ipAddr = IPAddress.Parse(Ip6Address);
		IPAddress ipAddr = ipHost.AddressList[0];


		IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);
			 
		Connector connector = new Connector();
		//connector.Connect(endPoint, () => { return SessionManager.Instance.Generate(); }, 1);   

		connector.Connect(endPoint, () => { return SessionManager.Instance.Generate(); }, 0);
		 
		while (true)  
		{
			try 
			{
				SessionManager.Instance.SendForEach();
			}
			catch (Exception ex) {
				Console.WriteLine(ex);
			}
			Thread.Sleep(250);



		}
	}
}

