using MainServer;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
 
namespace DummyClient
{
	public abstract class Packet
	{
		public ushort _size = 12;
		public ushort _packetId = 0;

		
		public abstract ArraySegment<byte> Write();
		public abstract void Read(ArraySegment<byte> s);
	}

	 

	public class ServerSession : PacketSession
	{
		public string _name = "";
		public override void OnConnected(EndPoint endPoint)
		{
			//Console.WriteLine($"OnConnected : {endPoint}");
			Console.WriteLine($"==========연결 성공!=========="); 
		}
		  
		public override void OnDisconnected(EndPoint endPoint)
		{
			Console.WriteLine($"OnDisConnected : {endPoint}");

		}

		public override void OnRecvPacket(ArraySegment<byte> buffer)
		{
			PacketManager.Instance.OnRecvPacket(this, buffer);
		}

		public override void OnSend(int numOfBytes)
		{
			//	throw new NotImplementedException();
		}
	}
}
