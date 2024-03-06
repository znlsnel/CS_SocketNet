using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MainServer
{
	class PacketHandler 
	{
		public static void C_PlayerInfoReqHandler(PacketSession session, IPacket packet)
		{
			C_PlayerInfoReq p = packet as C_PlayerInfoReq; 
			Console.WriteLine($"PlayerInfoReq :{p.playerId}");
			Console.WriteLine($"[{p.name}] ");
			foreach (C_PlayerInfoReq.Skill skill in p.skills)
			{
				Console.WriteLine($"Skill ID : ({skill.id}), Skill Level : ({skill.level}), Skill Duration : ({skill.duration})");
				foreach (C_PlayerInfoReq.Skill.Attribute at in skill.attributes)
					Console.WriteLine($"ATT : [{at.att}]");
			}
		} 
	}
} 
