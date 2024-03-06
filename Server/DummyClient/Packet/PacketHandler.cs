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
		public static void S_TestHandler(PacketSession session, IPacket packet)
		{
			S_Test p = (S_Test)packet;
			Console.WriteLine($"testInt :{p.testInt}");
			 
		}
	}
}
