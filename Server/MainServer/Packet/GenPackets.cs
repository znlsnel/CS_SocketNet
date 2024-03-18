
using ServerCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text; 
using System.Threading.Tasks;

public enum PacketId
{
		S_BroadcastEnterGame = 1,	C_FireObjIdxRequest = 2,	S_BroadcastFireObjIdx = 3,	C_Chat = 4,	C_AttackRequest = 5,	S_BroadcastAttack = 6,	C_DamageRequest = 7,	S_BroadcastDamage = 8,	C_UpdateScoreRequest = 9,	S_BroadcastUpdateScore = 10,	S_BroadcastChat = 11,	C_LeaveGame = 12,	S_BroadcastLeaveGame = 13,	S_PlayerList = 14,	C_Move = 15,	S_BroadcastMove = 16,
 
}

public interface IPacket
{
	ushort Protocol { get; }
	void Read(ArraySegment<byte> segment); 
	ArraySegment<byte> Write();
}

 
public class S_BroadcastEnterGame : IPacket
{    
	public int playerId;
	public int teamId;
	public vector3 position { get; set; } = new vector3();
	public vector3 moveDir { get; set; } = new vector3();
	public vector3 destPoint { get; set; } = new vector3(); 

	public ushort Protocol { get { return (ushort)PacketId.S_BroadcastEnterGame; } }
 
	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		ReadOnlySpan<byte> s = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
		count += sizeof(ushort);
		count += sizeof(ushort);
		
		 				
		playerId = BitConverter.ToInt32(s.Slice(count, s.Length - count));
		count += sizeof(int);
		
		 				
		teamId = BitConverter.ToInt32(s.Slice(count, s.Length - count));
		count += sizeof(int);
		
		 				
		position.x = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float);
		position.y = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float);
		position.z = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float); 
		
		 				
		moveDir.x = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float);
		moveDir.y = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float);
		moveDir.z = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float); 
		
		 				
		destPoint.x = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float);
		destPoint.y = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float);
		destPoint.z = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float); 
		
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);
			 
		ushort count = 0; 
		bool success = true;

		count += sizeof(ushort);
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)PacketId.S_BroadcastEnterGame);
		count += sizeof(ushort);

		
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.playerId);
		count += sizeof(int);   
		
		
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.teamId);
		count += sizeof(int);   
		
		
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.position.x);
		count += sizeof(float);   
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.position.y);
		count += sizeof(float);   
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.position.z);
		count += sizeof(float);    
		
		
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.moveDir.x);
		count += sizeof(float);   
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.moveDir.y);
		count += sizeof(float);   
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.moveDir.z);
		count += sizeof(float);    
		
		
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.destPoint.x);
		count += sizeof(float);   
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.destPoint.y);
		count += sizeof(float);   
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.destPoint.z);
		count += sizeof(float);    
		
 
		success &= BitConverter.TryWriteBytes(s, count);


		if (success == false)
			return null;
			 
		return SendBufferHelper.close(count);
	}
}
 
public class C_FireObjIdxRequest : IPacket
{    
	public int requestedPlayerId; 

	public ushort Protocol { get { return (ushort)PacketId.C_FireObjIdxRequest; } }
 
	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		ReadOnlySpan<byte> s = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
		count += sizeof(ushort);
		count += sizeof(ushort);
		
		 				
		requestedPlayerId = BitConverter.ToInt32(s.Slice(count, s.Length - count));
		count += sizeof(int);
		
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);
			 
		ushort count = 0; 
		bool success = true;

		count += sizeof(ushort);
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)PacketId.C_FireObjIdxRequest);
		count += sizeof(ushort);

		
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.requestedPlayerId);
		count += sizeof(int);   
		
 
		success &= BitConverter.TryWriteBytes(s, count);


		if (success == false)
			return null;
			 
		return SendBufferHelper.close(count);
	}
}
 
public class S_BroadcastFireObjIdx : IPacket
{    
	public int requestedPlayerId;
	public int fireObjId; 

	public ushort Protocol { get { return (ushort)PacketId.S_BroadcastFireObjIdx; } }
 
	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		ReadOnlySpan<byte> s = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
		count += sizeof(ushort);
		count += sizeof(ushort);
		
		 				
		requestedPlayerId = BitConverter.ToInt32(s.Slice(count, s.Length - count));
		count += sizeof(int);
		
		 				
		fireObjId = BitConverter.ToInt32(s.Slice(count, s.Length - count));
		count += sizeof(int);
		
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);
			 
		ushort count = 0; 
		bool success = true;

		count += sizeof(ushort);
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)PacketId.S_BroadcastFireObjIdx);
		count += sizeof(ushort);

		
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.requestedPlayerId);
		count += sizeof(int);   
		
		
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.fireObjId);
		count += sizeof(int);   
		
 
		success &= BitConverter.TryWriteBytes(s, count);


		if (success == false)
			return null;
			 
		return SendBufferHelper.close(count);
	}
}
 
public class C_Chat : IPacket
{    
	public string playerName;
	public string ChatText; 

	public ushort Protocol { get { return (ushort)PacketId.C_Chat; } }
 
	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		ReadOnlySpan<byte> s = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
		count += sizeof(ushort);
		count += sizeof(ushort);
		
		
		ushort playerNameLen = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
		count += sizeof(ushort); 
		  
		this.playerName = Encoding.Unicode.GetString(s.Slice(count, playerNameLen));
		count += playerNameLen; 
		
		
		ushort ChatTextLen = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
		count += sizeof(ushort); 
		  
		this.ChatText = Encoding.Unicode.GetString(s.Slice(count, ChatTextLen));
		count += ChatTextLen; 
		
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);
			 
		ushort count = 0; 
		bool success = true;

		count += sizeof(ushort);
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)PacketId.C_Chat);
		count += sizeof(ushort);

		
		ushort playerNameLen = (ushort)Encoding.Unicode.GetBytes(this.playerName, 0, this.playerName.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), playerNameLen);
		count += sizeof(ushort); 
		count += playerNameLen; 
		
		
		ushort ChatTextLen = (ushort)Encoding.Unicode.GetBytes(this.ChatText, 0, this.ChatText.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), ChatTextLen);
		count += sizeof(ushort); 
		count += ChatTextLen; 
		
 
		success &= BitConverter.TryWriteBytes(s, count);


		if (success == false)
			return null;
			 
		return SendBufferHelper.close(count);
	}
}
 
public class C_AttackRequest : IPacket
{    
	public int playerId; 

	public ushort Protocol { get { return (ushort)PacketId.C_AttackRequest; } }
 
	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		ReadOnlySpan<byte> s = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
		count += sizeof(ushort);
		count += sizeof(ushort);
		
		 				
		playerId = BitConverter.ToInt32(s.Slice(count, s.Length - count));
		count += sizeof(int);
		
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);
			 
		ushort count = 0; 
		bool success = true;

		count += sizeof(ushort);
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)PacketId.C_AttackRequest);
		count += sizeof(ushort);

		
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.playerId);
		count += sizeof(int);   
		
 
		success &= BitConverter.TryWriteBytes(s, count);


		if (success == false)
			return null;
			 
		return SendBufferHelper.close(count);
	}
}
 
public class S_BroadcastAttack : IPacket
{    
	public int playerId; 

	public ushort Protocol { get { return (ushort)PacketId.S_BroadcastAttack; } }
 
	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		ReadOnlySpan<byte> s = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
		count += sizeof(ushort);
		count += sizeof(ushort);
		
		 				
		playerId = BitConverter.ToInt32(s.Slice(count, s.Length - count));
		count += sizeof(int);
		
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);
			 
		ushort count = 0; 
		bool success = true;

		count += sizeof(ushort);
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)PacketId.S_BroadcastAttack);
		count += sizeof(ushort);

		
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.playerId);
		count += sizeof(int);   
		
 
		success &= BitConverter.TryWriteBytes(s, count);


		if (success == false)
			return null;
			 
		return SendBufferHelper.close(count);
	}
}
 
public class C_DamageRequest : IPacket
{    
	public int attackedPlayerId;
	public int damagedPlayerId;
	public int FireObjId; 

	public ushort Protocol { get { return (ushort)PacketId.C_DamageRequest; } }
 
	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		ReadOnlySpan<byte> s = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
		count += sizeof(ushort);
		count += sizeof(ushort);
		
		 				
		attackedPlayerId = BitConverter.ToInt32(s.Slice(count, s.Length - count));
		count += sizeof(int);
		
		 				
		damagedPlayerId = BitConverter.ToInt32(s.Slice(count, s.Length - count));
		count += sizeof(int);
		
		 				
		FireObjId = BitConverter.ToInt32(s.Slice(count, s.Length - count));
		count += sizeof(int);
		
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);
			 
		ushort count = 0; 
		bool success = true;

		count += sizeof(ushort);
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)PacketId.C_DamageRequest);
		count += sizeof(ushort);

		
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.attackedPlayerId);
		count += sizeof(int);   
		
		
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.damagedPlayerId);
		count += sizeof(int);   
		
		
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.FireObjId);
		count += sizeof(int);   
		
 
		success &= BitConverter.TryWriteBytes(s, count);


		if (success == false)
			return null;
			 
		return SendBufferHelper.close(count);
	}
}
 
public class S_BroadcastDamage : IPacket
{    
	public int attackedPlayerId;
	public int damagedPlayerId;
	public int FireObjId; 

	public ushort Protocol { get { return (ushort)PacketId.S_BroadcastDamage; } }
 
	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		ReadOnlySpan<byte> s = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
		count += sizeof(ushort);
		count += sizeof(ushort);
		
		 				
		attackedPlayerId = BitConverter.ToInt32(s.Slice(count, s.Length - count));
		count += sizeof(int);
		
		 				
		damagedPlayerId = BitConverter.ToInt32(s.Slice(count, s.Length - count));
		count += sizeof(int);
		
		 				
		FireObjId = BitConverter.ToInt32(s.Slice(count, s.Length - count));
		count += sizeof(int);
		
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);
			 
		ushort count = 0; 
		bool success = true;

		count += sizeof(ushort);
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)PacketId.S_BroadcastDamage);
		count += sizeof(ushort);

		
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.attackedPlayerId);
		count += sizeof(int);   
		
		
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.damagedPlayerId);
		count += sizeof(int);   
		
		
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.FireObjId);
		count += sizeof(int);   
		
 
		success &= BitConverter.TryWriteBytes(s, count);


		if (success == false)
			return null;
			 
		return SendBufferHelper.close(count);
	}
}
 
public class C_UpdateScoreRequest : IPacket
{    
	public int teamID; 

	public ushort Protocol { get { return (ushort)PacketId.C_UpdateScoreRequest; } }
 
	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		ReadOnlySpan<byte> s = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
		count += sizeof(ushort);
		count += sizeof(ushort);
		
		 				
		teamID = BitConverter.ToInt32(s.Slice(count, s.Length - count));
		count += sizeof(int);
		
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);
			 
		ushort count = 0; 
		bool success = true;

		count += sizeof(ushort);
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)PacketId.C_UpdateScoreRequest);
		count += sizeof(ushort);

		
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.teamID);
		count += sizeof(int);   
		
 
		success &= BitConverter.TryWriteBytes(s, count);


		if (success == false)
			return null;
			 
		return SendBufferHelper.close(count);
	}
}
 
public class S_BroadcastUpdateScore : IPacket
{    
	public int teamID; 

	public ushort Protocol { get { return (ushort)PacketId.S_BroadcastUpdateScore; } }
 
	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		ReadOnlySpan<byte> s = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
		count += sizeof(ushort);
		count += sizeof(ushort);
		
		 				
		teamID = BitConverter.ToInt32(s.Slice(count, s.Length - count));
		count += sizeof(int);
		
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);
			 
		ushort count = 0; 
		bool success = true;

		count += sizeof(ushort);
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)PacketId.S_BroadcastUpdateScore);
		count += sizeof(ushort);

		
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.teamID);
		count += sizeof(int);   
		
 
		success &= BitConverter.TryWriteBytes(s, count);


		if (success == false)
			return null;
			 
		return SendBufferHelper.close(count);
	}
}
 
public class S_BroadcastChat : IPacket
{    
	public string playerName;
	public string ChatText; 

	public ushort Protocol { get { return (ushort)PacketId.S_BroadcastChat; } }
 
	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		ReadOnlySpan<byte> s = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
		count += sizeof(ushort);
		count += sizeof(ushort);
		
		
		ushort playerNameLen = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
		count += sizeof(ushort); 
		  
		this.playerName = Encoding.Unicode.GetString(s.Slice(count, playerNameLen));
		count += playerNameLen; 
		
		
		ushort ChatTextLen = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
		count += sizeof(ushort); 
		  
		this.ChatText = Encoding.Unicode.GetString(s.Slice(count, ChatTextLen));
		count += ChatTextLen; 
		
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);
			 
		ushort count = 0; 
		bool success = true;

		count += sizeof(ushort);
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)PacketId.S_BroadcastChat);
		count += sizeof(ushort);

		
		ushort playerNameLen = (ushort)Encoding.Unicode.GetBytes(this.playerName, 0, this.playerName.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), playerNameLen);
		count += sizeof(ushort); 
		count += playerNameLen; 
		
		
		ushort ChatTextLen = (ushort)Encoding.Unicode.GetBytes(this.ChatText, 0, this.ChatText.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), ChatTextLen);
		count += sizeof(ushort); 
		count += ChatTextLen; 
		
 
		success &= BitConverter.TryWriteBytes(s, count);


		if (success == false)
			return null;
			 
		return SendBufferHelper.close(count);
	}
}
 
public class C_LeaveGame : IPacket
{    
	 

	public ushort Protocol { get { return (ushort)PacketId.C_LeaveGame; } }
 
	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		ReadOnlySpan<byte> s = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
		count += sizeof(ushort);
		count += sizeof(ushort);
		
		
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);
			 
		ushort count = 0; 
		bool success = true;

		count += sizeof(ushort);
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)PacketId.C_LeaveGame);
		count += sizeof(ushort);

		
 
		success &= BitConverter.TryWriteBytes(s, count);


		if (success == false)
			return null;
			 
		return SendBufferHelper.close(count);
	}
}
 
public class S_BroadcastLeaveGame : IPacket
{    
	public int playerId; 

	public ushort Protocol { get { return (ushort)PacketId.S_BroadcastLeaveGame; } }
 
	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		ReadOnlySpan<byte> s = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
		count += sizeof(ushort);
		count += sizeof(ushort);
		
		 				
		playerId = BitConverter.ToInt32(s.Slice(count, s.Length - count));
		count += sizeof(int);
		
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);
			 
		ushort count = 0; 
		bool success = true;

		count += sizeof(ushort);
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)PacketId.S_BroadcastLeaveGame);
		count += sizeof(ushort);

		
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.playerId);
		count += sizeof(int);   
		
 
		success &= BitConverter.TryWriteBytes(s, count);


		if (success == false)
			return null;
			 
		return SendBufferHelper.close(count);
	}
}
 
public class S_PlayerList : IPacket
{    
	 
	public class Player
	{  
		public bool isSelf;
		public int playerId;
		public int teamId;
		public vector3 position { get; set; } = new vector3();
		public vector3 moveDir { get; set; } = new vector3();
		public vector3 destPoint { get; set; } = new vector3();
	 
		public void Read(ReadOnlySpan<byte> s, ref ushort count)
		{ 
			 				
			isSelf = BitConverter.ToBoolean(s.Slice(count, s.Length - count));
			count += sizeof(bool);
			
			 				
			playerId = BitConverter.ToInt32(s.Slice(count, s.Length - count));
			count += sizeof(int);
			
			 				
			teamId = BitConverter.ToInt32(s.Slice(count, s.Length - count));
			count += sizeof(int);
			
			 				
			position.x = BitConverter.ToSingle(s.Slice(count, s.Length - count));
			count += sizeof(float);
			position.y = BitConverter.ToSingle(s.Slice(count, s.Length - count));
			count += sizeof(float);
			position.z = BitConverter.ToSingle(s.Slice(count, s.Length - count));
			count += sizeof(float); 
			
			 				
			moveDir.x = BitConverter.ToSingle(s.Slice(count, s.Length - count));
			count += sizeof(float);
			moveDir.y = BitConverter.ToSingle(s.Slice(count, s.Length - count));
			count += sizeof(float);
			moveDir.z = BitConverter.ToSingle(s.Slice(count, s.Length - count));
			count += sizeof(float); 
			
			 				
			destPoint.x = BitConverter.ToSingle(s.Slice(count, s.Length - count));
			count += sizeof(float);
			destPoint.y = BitConverter.ToSingle(s.Slice(count, s.Length - count));
			count += sizeof(float);
			destPoint.z = BitConverter.ToSingle(s.Slice(count, s.Length - count));
			count += sizeof(float); 
			
		}  
	
		public bool Write(Span<byte> s, ref ushort count)
		{
			bool success = true;
			
			success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.isSelf);
			count += sizeof(bool);   
			
			
			success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.playerId);
			count += sizeof(int);   
			
			
			success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.teamId);
			count += sizeof(int);   
			
			
			 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.position.x);
			count += sizeof(float);   
			 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.position.y);
			count += sizeof(float);   
			 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.position.z);
			count += sizeof(float);    
			
			
			 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.moveDir.x);
			count += sizeof(float);   
			 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.moveDir.y);
			count += sizeof(float);   
			 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.moveDir.z);
			count += sizeof(float);    
			
			
			 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.destPoint.x);
			count += sizeof(float);   
			 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.destPoint.y);
			count += sizeof(float);   
			 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.destPoint.z);
			count += sizeof(float);    
			
					   
			return success;
		}
				 
	
	}    
	public List<Player> players = new List<Player>(); 
	 

	public ushort Protocol { get { return (ushort)PacketId.S_PlayerList; } }
 
	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		ReadOnlySpan<byte> s = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
		count += sizeof(ushort);
		count += sizeof(ushort);
		
		
		ushort playerLen = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
		count += sizeof(ushort);
		
		players.Clear(); 
		for (int i = 0; i < playerLen; i++)
		{
			Player player = new Player();
			player.Read(s, ref count);
			players.Add(player); 
		}
		
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);
			 
		ushort count = 0; 
		bool success = true;

		count += sizeof(ushort);
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)PacketId.S_PlayerList);
		count += sizeof(ushort);

		
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)players.Count);
		count += sizeof(ushort); 
		foreach (Player player in this.players)
		{ 
			success &= player.Write(s, ref count); 
		}
		
 
		success &= BitConverter.TryWriteBytes(s, count);


		if (success == false)
			return null;
			 
		return SendBufferHelper.close(count);
	}
}
 
public class C_Move : IPacket
{    
	public vector3 position { get; set; } = new vector3();
	public vector3 moveDir { get; set; } = new vector3();
	public vector3 destPoint { get; set; } = new vector3(); 

	public ushort Protocol { get { return (ushort)PacketId.C_Move; } }
 
	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		ReadOnlySpan<byte> s = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
		count += sizeof(ushort);
		count += sizeof(ushort);
		
		 				
		position.x = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float);
		position.y = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float);
		position.z = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float); 
		
		 				
		moveDir.x = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float);
		moveDir.y = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float);
		moveDir.z = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float); 
		
		 				
		destPoint.x = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float);
		destPoint.y = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float);
		destPoint.z = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float); 
		
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);
			 
		ushort count = 0; 
		bool success = true;

		count += sizeof(ushort);
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)PacketId.C_Move);
		count += sizeof(ushort);

		
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.position.x);
		count += sizeof(float);   
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.position.y);
		count += sizeof(float);   
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.position.z);
		count += sizeof(float);    
		
		
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.moveDir.x);
		count += sizeof(float);   
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.moveDir.y);
		count += sizeof(float);   
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.moveDir.z);
		count += sizeof(float);    
		
		
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.destPoint.x);
		count += sizeof(float);   
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.destPoint.y);
		count += sizeof(float);   
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.destPoint.z);
		count += sizeof(float);    
		
 
		success &= BitConverter.TryWriteBytes(s, count);


		if (success == false)
			return null;
			 
		return SendBufferHelper.close(count);
	}
}
 
public class S_BroadcastMove : IPacket
{    
	public int playerId;
	public vector3 position { get; set; } = new vector3();
	public vector3 moveDir { get; set; } = new vector3();
	public vector3 destPoint { get; set; } = new vector3(); 

	public ushort Protocol { get { return (ushort)PacketId.S_BroadcastMove; } }
 
	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		ReadOnlySpan<byte> s = new ReadOnlySpan<byte>(segment.Array, segment.Offset, segment.Count);
		count += sizeof(ushort);
		count += sizeof(ushort);
		
		 				
		playerId = BitConverter.ToInt32(s.Slice(count, s.Length - count));
		count += sizeof(int);
		
		 				
		position.x = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float);
		position.y = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float);
		position.z = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float); 
		
		 				
		moveDir.x = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float);
		moveDir.y = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float);
		moveDir.z = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float); 
		
		 				
		destPoint.x = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float);
		destPoint.y = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float);
		destPoint.z = BitConverter.ToSingle(s.Slice(count, s.Length - count));
		count += sizeof(float); 
		
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		Span<byte> s = new Span<byte>(segment.Array, segment.Offset, segment.Count);
			 
		ushort count = 0; 
		bool success = true;

		count += sizeof(ushort);
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), (ushort)PacketId.S_BroadcastMove);
		count += sizeof(ushort);

		
		success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.playerId);
		count += sizeof(int);   
		
		
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.position.x);
		count += sizeof(float);   
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.position.y);
		count += sizeof(float);   
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.position.z);
		count += sizeof(float);    
		
		
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.moveDir.x);
		count += sizeof(float);   
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.moveDir.y);
		count += sizeof(float);   
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.moveDir.z);
		count += sizeof(float);    
		
		
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.destPoint.x);
		count += sizeof(float);   
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.destPoint.y);
		count += sizeof(float);   
		 success &= BitConverter.TryWriteBytes(s.Slice(count, s.Length - count), this.destPoint.z);
		count += sizeof(float);    
		
 
		success &= BitConverter.TryWriteBytes(s, count);


		if (success == false)
			return null;
			 
		return SendBufferHelper.close(count);
	}
}


