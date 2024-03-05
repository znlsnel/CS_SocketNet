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

			PlayerInfoReq packet = new PlayerInfoReq() { playerId = 141, name = "김더지" };

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
			ushort count = 0;
			ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
			count += 2;
			ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
			count += 2;
			
			switch ((PacketId)id)
			{ 
				case PacketId.PlayerInfoReq: 
					{
						PlayerInfoReq p = new PlayerInfoReq();
						p.Read(buffer);
						Console.WriteLine($"PlayerInfoReq :{p.playerId}");
						Console.WriteLine($"[{p.name}] ");
						foreach(PlayerInfoReq.Skill skill in p.skills)
						{
							Console.WriteLine($"Skill ID : ({skill.id}), Skill Level : ({skill.level}), Skill Duration : ({skill.duration})");
							foreach (PlayerInfoReq.Skill.Attribute at in  skill.attributes)
								Console.WriteLine($"ATT : [{at.att}]");
						}
						 
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
