using ServerCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
	  
	class PlayerInfoReq : Packet
	{
		public long playerId;
		public string name;

		public PlayerInfoReq()
		{
			this._packetId = (ushort)PacketId.PlayerInfoReq; 
		}
		
		public override void Read(ArraySegment<byte> segment)
		{
			ReadOnlySpan<byte> s = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
			ushort count = 0;
			//ushort size = BitConverter.ToUInt16(s.Array, s.Offset);
			count += sizeof(ushort);
			//ushort id = BitConverter.ToUInt16(s.Array, s.Offset + count);
			count += sizeof(ushort);
			this.playerId = BitConverter.ToInt64(s.Slice(count, s.Length - count));
			count += sizeof(long);

			ushort nameLen = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
			count += sizeof(ushort);

			name = Encoding.Unicode.GetString(s.Slice(count, nameLen));
			count += nameLen;
		}

		public override ArraySegment<byte> Write()
		{ 
			ArraySegment<byte> segment = SendBufferHelper.Open(4096);
			Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);
			 
			ushort count = 0; 
			bool success = true;

			count += sizeof(ushort);
			success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)PacketId.PlayerInfoReq);
			count += sizeof(ushort);
			success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.playerId);
			count += sizeof(long); 

			// string을 넣고 string의 길이를 반환함 
			ushort nameLen = (ushort)Encoding.Unicode.GetBytes(this.name, 0, this.name.Length, segment.Array, segment.Offset + count + sizeof(ushort));
			success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), nameLen);
			count += sizeof(ushort); 
			count += nameLen;

			success &= BitConverter.TryWriteBytes(s, count);


			if (success == false)
				return null;
			 
			return SendBufferHelper.close(count);
		}
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

			PlayerInfoReq packet = new PlayerInfoReq() { playerId = 1021, name = "김우디" };
			//for (int i = 0; i < 5; i++) 
			{ 
				ArraySegment<byte> s =  packet.Write();
				  
				if(s != null)  
					Send(s); 

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
