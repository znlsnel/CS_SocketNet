using ServerCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
 
namespace DummyClient
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
	
	public class ServerSession : Session
	{ 
		public override void OnConnected(EndPoint endPoint)
		{
			Console.WriteLine($"OnConnected : {endPoint}");

			PlayerInfoReq packet = new PlayerInfoReq() { _size = 4, _packetId = (ushort)PacketId.PlayerInfoReq, playerId = 1001 };
			//for (int i = 0; i < 5; i++) 
			{  
				ArraySegment<byte> sg = SendBufferHelper.Open(4096);

				ushort count = 0;  
				bool success = true;
				count += 2;
				success &= BitConverter.TryWriteBytes(new Span<byte>(sg.Array, sg.Offset+ count, sg.Count - count), packet._packetId);
				count += 2;
				success &= BitConverter.TryWriteBytes(new Span<byte>(sg.Array, sg.Offset + count, sg.Count - count), packet.playerId);
				count += 8;

				success &= BitConverter.TryWriteBytes(new Span<byte>(sg.Array, sg.Offset, sg.Count), count);
				 
				ArraySegment<byte> sendBuff = SendBufferHelper.close(count); 
				  
				if(success)
					Send(sendBuff); 

			}
		}

		public override void OnDisconnected(EndPoint endPoint)
		{
			Console.WriteLine($"OnDisConnected : {endPoint}");

		}

		public override int OnRecv(ArraySegment<byte> buffer)
		{
			string recvData = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
			Console.WriteLine($"[From Server] {recvData}");

			return buffer.Count;
		}

		public override void OnSend(int numOfBytes)
		{
			//	throw new NotImplementedException();
		}
	}
}
