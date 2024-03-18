
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
  
public class vector3
{ 
	public float x = 0.0f;
	public float y = 0.0f; 
	public float z = 0.0f;
} 

public class PacketManager
{
	#region Singleton
	static PacketManager _instance = new PacketManager(); 
	public static PacketManager Instance { get { return _instance; } }
	#endregion
 
	PacketManager()
	{
		Register(); 
	}

	Dictionary<ushort, Func<PacketSession, ArraySegment<byte>, IPacket>> _makeFunc =
	new Dictionary<ushort, Func<PacketSession, ArraySegment<byte>, IPacket>>();
	Dictionary<ushort, Action<PacketSession, IPacket>> _handler = 
		new Dictionary<ushort, Action<PacketSession, IPacket>>();

		 
	public void Register()
	{
		
			_makeFunc.Add((ushort)PacketId.C_FireObjIdxRequest, MakePacket<C_FireObjIdxRequest>); 
			_handler.Add((ushort)PacketId.C_FireObjIdxRequest, PacketHandler.C_FireObjIdxRequestHandler); 


			_makeFunc.Add((ushort)PacketId.C_Chat, MakePacket<C_Chat>); 
			_handler.Add((ushort)PacketId.C_Chat, PacketHandler.C_ChatHandler); 


			_makeFunc.Add((ushort)PacketId.C_AttackRequest, MakePacket<C_AttackRequest>); 
			_handler.Add((ushort)PacketId.C_AttackRequest, PacketHandler.C_AttackRequestHandler); 


			_makeFunc.Add((ushort)PacketId.C_DamageRequest, MakePacket<C_DamageRequest>); 
			_handler.Add((ushort)PacketId.C_DamageRequest, PacketHandler.C_DamageRequestHandler); 


			_makeFunc.Add((ushort)PacketId.C_UpdateScoreRequest, MakePacket<C_UpdateScoreRequest>); 
			_handler.Add((ushort)PacketId.C_UpdateScoreRequest, PacketHandler.C_UpdateScoreRequestHandler); 


			_makeFunc.Add((ushort)PacketId.C_LeaveGame, MakePacket<C_LeaveGame>); 
			_handler.Add((ushort)PacketId.C_LeaveGame, PacketHandler.C_LeaveGameHandler); 


			_makeFunc.Add((ushort)PacketId.C_Move, MakePacket<C_Move>); 
			_handler.Add((ushort)PacketId.C_Move, PacketHandler.C_MoveHandler); 


	}


	public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer, Action<PacketSession, IPacket> onRecvCallBack = null)
	{
		ushort count = 0;
		ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
		count += 2;
		ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
		count += 2;

		Func<PacketSession, ArraySegment<byte>, IPacket> func = null;
		if (_makeFunc.TryGetValue(id, out func))
		{

			IPacket packet = func.Invoke(session, buffer);
			if (onRecvCallBack != null)
				onRecvCallBack.Invoke(session, packet);
			else
				HandlePacket(session, packet);
		} 
	}



	T MakePacket<T>(PacketSession session, ArraySegment<byte> buffer) where T : IPacket, new()
	{
		T pkt = new T();
		pkt.Read(buffer);

		return pkt;
	}

	public void HandlePacket(PacketSession session, IPacket packet)
	{
		Action<PacketSession, IPacket> action = null;
		if (_handler.TryGetValue(packet.Protocol, out action))
			action.Invoke(session, packet);
	}

}

