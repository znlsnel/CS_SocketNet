using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerCore;


public class GameRoom : IJobQueue 
{
	List<ClientSession> _sessions = new List<ClientSession>();

	List<ClientSession> _teamRed = new List<ClientSession>();
	List<ClientSession> _teamGreen = new List<ClientSession>();
	List<ClientSession> _teamBlue = new List<ClientSession>();
	 
	JobQueue _jobQueue = new JobQueue();
	List<ArraySegment<byte>> _pendingList = new List<ArraySegment<byte>>();
	
	
	public void Push(Action job) 
	{ 
		_jobQueue.Push(job);
	}

	public void Flush() 
	{
		foreach (ClientSession s in _sessions)
			s.Send(_pendingList);
		 
	//	Console.WriteLine($"Flushed [{_pendingList.Count}] items");
		_pendingList.Clear();
	}

	public void Broadcast(ArraySegment<byte> segment)
	{ 
		_pendingList.Add(segment);
	}  
		 
	public void Enter(ClientSession session)
	{ 
		// 플레이어 추가

		int redCount = _teamRed.Count;
		int greenCount = _teamGreen.Count;
		int blueCount = _teamBlue.Count;  
		
		if (redCount <= greenCount && redCount <= blueCount)
		{
			session.TeamId = 0;
			_teamRed.Add(session);
		}
		else if (greenCount <= redCount && greenCount <= blueCount)
		{ 
			session.TeamId = 1;
			_teamGreen.Add(session);
		}
		else 
		{
			session.TeamId = 2;
			_teamBlue.Add(session); 
		}

		_sessions.Add(session);

		session.Room = this;
			
		// 신입생한테 모든 플레이어 목록 전송
		S_PlayerList players = new S_PlayerList();
		foreach(ClientSession s in _sessions)
		{
			players.players.Add(new S_PlayerList.Player()
			{
				isSelf = (s == session),
				playerId = s.SessionId,
				teamId = s.TeamId, 
				position = s.position,
				moveDir = s.moveDir,
				destPoint = s.destPoint,
			});
		}
			 
		session.Send(players.Write()); 
		// 신입생 입장을 모두에게 알림

		S_BroadcastEnterGame enter = new S_BroadcastEnterGame();
		enter.playerId = session.SessionId;
		enter.teamId = session.TeamId; 
		enter.position = new vector3();
		enter.moveDir = new vector3();
		enter.destPoint= new vector3();
		 

		Broadcast(enter.Write());
			 
			  
	}
	public void Leave(ClientSession session)
	{
		_sessions.Remove(session);
		_teamRed.Remove(session);
		_teamGreen.Remove(session); 
		_teamBlue.Remove(session);

			S_BroadcastLeaveGame leave = new S_BroadcastLeaveGame();
		leave.playerId = session.SessionId;
		Broadcast(leave.Write()); 
	} 
	 
	public void Move(ClientSession session, C_Move packet)
	{
		S_BroadcastMove move = new S_BroadcastMove();
		move.playerId = session.SessionId;
		move.position= packet.position;
		move.moveDir = packet.moveDir;
		move.destPoint = packet.destPoint;
		 
		Broadcast(move.Write());  
	}

	public void Chating(C_Chat packet)
	{
		S_BroadcastChat chat = new S_BroadcastChat();
		chat.playerName = packet.playerName;
		chat.ChatText = packet.ChatText;

		Broadcast(chat.Write());
	}
	 
	public void Attack(ClientSession session)
	{
		S_BroadcastAttack s = new S_BroadcastAttack(); 
		s.playerId = session.SessionId;

		Broadcast(s.Write());
	}
	public void Damage(C_DamageRequest packet) 
	{
		S_BroadcastDamage s = new S_BroadcastDamage();
		s.attackedPlayerId = packet.attackedPlayerId;
		s.damagedPlayerId = packet.damagedPlayerId;
		s.FireObjId = packet.FireObjId; 
		Broadcast(s.Write());
	}

	static int fireObjId = 0;
	public void FireObjIdx(C_FireObjIdxRequest packet)
	{ 
		S_BroadcastFireObjIdx s = new S_BroadcastFireObjIdx();
		s.requestedPlayerId = packet.requestedPlayerId;
		s.fireObjId = fireObjId % 20;
		fireObjId++; 
		Broadcast(s.Write());
	}
	public void UpdateScore(C_UpdateScoreRequest packet)
	{
		S_BroadcastUpdateScore s = new S_BroadcastUpdateScore();
		s.teamID = packet.teamID;
		Broadcast(s.Write()); 
	}
	
}
