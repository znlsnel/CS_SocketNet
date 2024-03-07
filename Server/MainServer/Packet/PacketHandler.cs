﻿using ServerCore;
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
		public static void C_ChatHandler(PacketSession session, IPacket packet)
		{
			C_Chat chatPacket = packet as C_Chat;
			ClientSession clientSession = session as ClientSession;

			chatPacket.chat = $"[{chatPacket.playerName}]" + chatPacket.chat;
			 
			if (clientSession.Room == null)
				return;
			 
			GameRoom room = clientSession.Room; 
			room.Push(() => room.Broadcast(clientSession, chatPacket.chat));
		}
	}
}  
