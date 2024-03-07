using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DummyClient
{
	class SessionManager
	{
		static SessionManager _session = new SessionManager();
		public static SessionManager Instance { get { return _session; } }

		List<ServerSession> _sessions = new List<ServerSession>();
		object _lock = new object();

		public void SendForEach(string massage = "")
		{
			lock (_lock)
			{
				foreach (ServerSession session in _sessions)
				{ 
					
					C_Chat chatPacket = new C_Chat();
					chatPacket.chat = massage == "" ? $"Hello Server !" : massage;
					chatPacket.playerName = session._name;
					 
					ArraySegment<byte> segment = chatPacket.Write();
					 
					session.Send(segment); 
				}
			}
		} 
		public ServerSession Generate(string name = "")
		{
			lock (_lock)
			{
				ServerSession session = new ServerSession();
				session._name = name;
				_sessions.Add(session);
				return session;
			}
		}
	}
}
