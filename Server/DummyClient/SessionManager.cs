using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


class SessionManager
{
	static SessionManager _session = new SessionManager();
	public static SessionManager Instance { get { return _session; } }

	List<ServerSession> _sessions = new List<ServerSession>();
	object _lock = new object();

	Random _random = new Random();
	public void SendForEach(string massage = "")
	{
		lock (_lock)
		{ 
			foreach (ServerSession session in _sessions)
			{ 
				C_Move movePacket = new C_Move();
				movePacket.posX = _random.Next(-50,50);
				movePacket.posY = 0;
				movePacket.posZ = _random.Next(-50, 50);
				session.Send(movePacket.Write());
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

