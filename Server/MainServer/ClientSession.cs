using ServerCore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MainServer
{
	public abstract class Packet
	{
		public ushort _size = 12;
		public ushort _packetId = 0;

		public abstract ArraySegment<byte> Write();
		public abstract void Read(ArraySegment<byte> s);
	} 
	  
	public class ClientSession : PacketSession
	{
		static int serverCount = 0;
		public override void OnConnected(EndPoint endPoint)
		{
			Console.WriteLine($"OnConnected : {endPoint}");

			C_PlayerInfoReq packet = new C_PlayerInfoReq() { playerId = 141, name = "김더지" };

			//ArraySegment<byte> openSegment = SendBufferHelper.Open(4096);
			//byte[] buffer = BitConverter.GetBytes(packet.size);
			//byte[] buffer2 = BitConverter.GetBytes(packet._packetId);
			//Array.Copy(buffer, 0, openSegment.Array, openSegment.Offset, buffer.Length);
			//Array.Copy(buffer2, 0, openSegment.Array, openSegment.Offset + buffer.Length, buffer2.Length);
			//ArraySegment<byte> sendBuff = SendBufferHelper.close(buffer.Length + buffer2.Length );
			    
			//Send(sendBuff);
			//Thread.Sleep(100);
			//Disconnect(); 
		}
		public override void OnRecvPacket(ArraySegment<byte> buffer)
		{
			PacketManager.Instance.OnRecvPacket(this, buffer);
		}

		public override void OnDisconnected(EndPoint endPoint)
		{
			Console.WriteLine($"OnDisConnected : {endPoint}");

		}


		public override void OnSend(int numOfBytes)
		{
			//	throw new NotImplementedException();
		}
	}
}
