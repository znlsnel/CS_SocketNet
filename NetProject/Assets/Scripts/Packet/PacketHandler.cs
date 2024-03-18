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

	public static void S_BroadcastFireObjIdxHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastFireObjIdx pkt = packet as S_BroadcastFireObjIdx;
		ServerSession serverSession = session as ServerSession;

		if (pkt == null || serverSession == null)
			return;

		PlayerManager.Instnace.Fire(pkt);
	}

	public static void S_BroadcastDamageHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastDamage pkt = packet as S_BroadcastDamage;
		ServerSession serverSession = session as ServerSession;

		if (pkt == null || serverSession == null)
			return;

		PlayerManager.Instnace.Damage(pkt);
	}

	public static void S_BroadcastUpdateScoreHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastUpdateScore pkt = packet as S_BroadcastUpdateScore;
		ServerSession serverSession = session as ServerSession;

		if (pkt == null || serverSession == null)
			return;

		PlayerManager.Instnace.UpdateScore(pkt);
	}

	public static void S_BroadcastAttackHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastAttack pkt = packet as S_BroadcastAttack;
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instnace.RequestAttack(pkt);
	}

	public static void S_BroadcastChatHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastChat pkt = packet as S_BroadcastChat;
		ServerSession serverSession = session as ServerSession;

		if (serverSession == null || pkt == null) return;

		PlayerManager.Instnace.Chat(pkt);
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

