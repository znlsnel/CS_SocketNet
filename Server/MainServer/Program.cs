using System.Net.Sockets;
using System.Net;
using System.Text;
using ServerCore;

namespace MainServer
{
	public class Massage
	{
		public int _id = 0;
		public string _name = "";
		public string _massage = "";

		public Massage(int id,  string name, string massage) {
			_id = id;
			_name = name;
			_massage = massage;
		}

	}
	public class GameSession : Session
	{
		static int serverCount = 0;
		public override void OnConnected(EndPoint endPoint)
		{
			Console.WriteLine($"OnConnected : {endPoint}");

			Massage msg = new Massage(15, "김우디", "하이 난 김우디");
			ArraySegment<byte> openSegment = SendBufferHelper.Open(4096);
			byte[] buffer = BitConverter.GetBytes(msg._id); 
			byte[] buffer2 = Encoding.UTF8.GetBytes(msg._name);
			byte[] buffer3 = Encoding.UTF8.GetBytes(msg._massage);
			Array.Copy(buffer, 0, openSegment.Array, openSegment.Offset, buffer.Length);
			Array.Copy(buffer2, 0, openSegment.Array, openSegment.Offset + buffer.Length, buffer2.Length);
			Array.Copy(buffer3, 0, openSegment.Array, openSegment.Offset +buffer.Length + buffer2.Length, buffer3.Length);
			ArraySegment<byte> sendBuff = SendBufferHelper.close(buffer.Length + buffer2.Length + buffer3.Length);
			 

			Send(sendBuff);
			Thread.Sleep(100);
			Disconnect();

		}

		public override void OnDisconnected(EndPoint endPoint)
		{
			Console.WriteLine($"OnDisConnected : {endPoint}");

		}

		public override int OnRecv(ArraySegment<byte> buffer)
		{  
			string recvData = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
			Console.WriteLine($"[From Client] {recvData}");
			return buffer.Count;
		}

		public override void OnSend(int numOfBytes)
		{
		//	throw new NotImplementedException();
		}
	}
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
			string IpAddress = "192.168.219.200";

			IPHostEntry ipHost = Dns.GetHostEntry(host);
			//IPAddress ipAddr =  ipHost.AddressList[0];

			IPAddress ipAddr = IPAddress.Parse(IpAddress);
			IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

			_listener.Init(endPoint, () =>{ return new GameSession(); });

			while (true)
			{
				;
			}

		}

	}
}