using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MainServer
{
	class Packet
	{
		public ushort _size = 12;
		public ushort _packetId = 0;
	}

	class PlayerInfoReq : Packet
	{
		public long playerId;
	}
	class PlayerInfoOk : Packet
	{
		public int hp;
		public int attack;
	}

	public enum PacketId
	{
		PlayerInfoReq = 1,
		PlayerInfoOk = 2,
	}
	 
	public class ClientSession : PacketSession
	{
		static int serverCount = 0;
		public override void OnConnected(EndPoint endPoint)
		{
			Console.WriteLine($"OnConnected : {endPoint}");

			Packet packet = new Packet() { _packetId = 1, _size = 4 };
			ArraySegment<byte> openSegment = SendBufferHelper.Open(4096);
			byte[] buffer = BitConverter.GetBytes(packet._size);
			byte[] buffer2 = BitConverter.GetBytes(packet._packetId);
			Array.Copy(buffer, 0, openSegment.Array, openSegment.Offset, buffer.Length);
			Array.Copy(buffer2, 0, openSegment.Array, openSegment.Offset + buffer.Length, buffer2.Length);
			ArraySegment<byte> sendBuff = SendBufferHelper.close(buffer.Length + buffer2.Length );
			  
			Send(sendBuff);
			//Thread.Sleep(100);
			//Disconnect();
		}
		public override void OnRecvPacket(ArraySegment<byte> buffer)
		{
			ushort count = 0;
			ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
			count += 2;
			ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
			count += 2;
			
			switch ((PacketId)id)
			{
				case PacketId.PlayerInfoReq: 
					{
						long playerId = BitConverter.ToInt64(buffer.Array, buffer.Offset + count);
						count += 8;
						Console.WriteLine($"PlayerInfoReq :{playerId}");
					} 
					break;


			}
			Console.WriteLine($"Size : [{size}] , Id : [{id}] ");

			//	throw new NotImplementedException();
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
