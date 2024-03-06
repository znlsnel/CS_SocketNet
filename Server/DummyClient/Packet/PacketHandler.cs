using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyClient
{
	class PacketHandler
	{
		public static void S_ChatHandler(PacketSession session, IPacket packet)
		{ 
			S_Chat chatPacket = (S_Chat)packet;
			ServerSession serverSession = session as ServerSession;

			//if (chatPacket.playerID == 1) 
				Console.WriteLine(chatPacket.chat);
		}
	}
}
