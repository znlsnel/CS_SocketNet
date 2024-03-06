using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainServer;

namespace MainServer
{ 
    public class GameRoom
	{
		List<ClientSession> _sessions = new List<ClientSession>();
		object _lock = new object();

		public void Broadcast(ClientSession session, string chat)
		{
			S_Chat packet = new S_Chat();
			packet.playerID = session.SessionId;
			packet.chat = chat + $"나는 [{packet.playerID}]야!";
			ArraySegment<byte> segment = packet.Write();

			lock (_lock)
			{
				foreach (ClientSession s in _sessions)
					s.Send(segment);
			} 

		}


		public void Enter(ClientSession session)
		{
			lock (_lock)
			{
				_sessions.Add(session);
				session.Room = this;
			} 
		}
		public void Leave(ClientSession session)
		{
			    lock (this)
			    {
				_sessions.Remove(session);	
			    }

		} 
	}
}
