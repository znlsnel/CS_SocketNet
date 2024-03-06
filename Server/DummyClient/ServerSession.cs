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



	public class ServerSession : Session
	{    
		public override void OnConnected(EndPoint endPoint)
		{
			Console.WriteLine($"OnConnected : {endPoint}");

			C_PlayerInfoReq packet = new C_PlayerInfoReq() { playerId = 1021, name = "김우디" };
			packet.skills.Add(new C_PlayerInfoReq.Skill() { id = 1, level = 3, duration = 1.24f });
			packet.skills.Add(new C_PlayerInfoReq.Skill() { id = 2, level = 6, duration = 1.64f });
			packet.skills.Add(new C_PlayerInfoReq.Skill() { id = 3, level = 9, duration = 1.34f });
			packet.skills.Add(new C_PlayerInfoReq.Skill() { id = 4, level = 12, duration = 3.4f });
			packet.skills.Add(new C_PlayerInfoReq.Skill() { id = 5, level = 15, duration = 8.14f });
			packet.skills.Add(new C_PlayerInfoReq.Skill() { id = 6, level = 18, duration = 0.2244f });
			packet.skills[0].attributes.Add(new C_PlayerInfoReq.Skill.Attribute() { att = 1 });
			packet.skills[0].attributes.Add(new C_PlayerInfoReq.Skill.Attribute() { att = 5 });
			packet.skills[0].attributes.Add(new C_PlayerInfoReq.Skill.Attribute() { att = 125 });
			
			  
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
