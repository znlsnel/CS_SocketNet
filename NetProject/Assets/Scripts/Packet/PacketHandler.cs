using DummyClient;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
 
 
class PacketHandler
{
	public static void S_BroadcastEnterGameHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastEnterGame pkt = packet as S_BroadcastEnterGame;
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instnace.EnterGame(pkt); 
	}
	public static void S_TESTHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastEnterGame pkt = packet as S_BroadcastEnterGame;
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instnace.EnterGame(pkt);
	} 

	public static void S_BroadcastLeaveGameHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastLeaveGame pkt = packet as S_BroadcastLeaveGame;
		ServerSession serverSession = session as ServerSession;
		  
		PlayerManager.Instnace.LeaveGame(pkt);
	}
	 
	public static void S_PlayerListHandler(PacketSession session, IPacket packet)
	{
		S_PlayerList pkt = packet as S_PlayerList;
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instnace.Add(pkt); 
	}
	public static void S_BroadcastMoveHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastMove pkt = packet as S_BroadcastMove;
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instnace.Move(pkt); 
	}
}

