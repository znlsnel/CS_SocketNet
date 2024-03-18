using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
 
 
class PacketHandler 
{ 
	public static void C_LeaveGameHandler(PacketSession session, IPacket packet)
	{  
		ClientSession clientSession = session as ClientSession;
			 
		if (clientSession.Room == null)
			return; 
			   
		GameRoom room = clientSession.Room; 
		room.Push(() => room.Leave(clientSession));
	}

	public static void C_AttackRequestHandler(PacketSession session, IPacket packet)
	{
		ClientSession clientSession = session as ClientSession;
		if (clientSession.Room == null)
			return;

		GameRoom room = clientSession.Room; 
		room.Push(() => room.Attack(clientSession));
	}

	public static void C_UpdateScoreRequestHandler(PacketSession session, IPacket packet)
	{
		C_UpdateScoreRequest pkt = packet as C_UpdateScoreRequest;
		ClientSession clientSession = session as ClientSession;
		if (clientSession.Room == null || pkt == null)
			return; 

		GameRoom room = clientSession.Room;
		room.Push(() => room.UpdateScore(pkt));
	} 


	public static void C_DamageRequestHandler(PacketSession session, IPacket packet)
	{
		C_DamageRequest pkt = packet as C_DamageRequest;
		ClientSession clientSession = session as ClientSession;
		if (clientSession.Room == null || pkt == null)
			return;

		GameRoom room = clientSession.Room;
		room.Push(() => room.Damage(pkt));  
	}
	public static void C_FireObjIdxRequestHandler(PacketSession session, IPacket packet)
	{
		C_FireObjIdxRequest pkt = packet as C_FireObjIdxRequest;
		ClientSession clientSession = session as ClientSession;
		if (clientSession.Room == null || pkt == null)
			return;
		 
		GameRoom room = clientSession.Room;
		room.Push(() => room.FireObjIdx(pkt));

	}
	
	public static void C_ChatHandler(PacketSession session, IPacket packet)
	{
		C_Chat chatPacket = packet as C_Chat;
		ClientSession clientSession = session as ClientSession;

		if (clientSession.Room == null || chatPacket == null)
			return;
		 
		GameRoom room = clientSession.Room;
		room.Push(() => room.Chating(chatPacket)); 
		 


	}
	public static void C_MoveHandler(PacketSession session, IPacket packet)
	{
		C_Move movePacket = packet as C_Move;
		ClientSession clientSession = session as ClientSession;
		 
	//	Console.WriteLine($"Pos : {movePacket.destPoint.x}, {movePacket.destPoint.y}, {movePacket.destPoint.z}");
		if (clientSession.Room == null) 
			return;    
		    
		GameRoom room = clientSession.Room;
		room.Push(() => room.Move(clientSession, movePacket));
	}  
}

