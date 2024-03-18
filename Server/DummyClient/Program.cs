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
		string IpAddress = "192.168.219.101";   

		IPAddress ipAddr = IPAddress.Parse(IpAddress); 
		//IPAddress ipAddr = ipHost.AddressList[0]; 


		IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);
			 
		Connector connector = new Connector();

		connector.Connect(endPoint, () => { return SessionManager.Instance.Generate(); }, 10);
		 
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

