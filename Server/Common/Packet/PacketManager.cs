
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
 
namespace MainServer
{
	class PacketManager
	{
		#region Singleton
		static PacketManager _instance;
		public static PacketManager Instance
		{
			get
			{
				if (_instance == null)
					_instance = new PacketManager();
				return _instance;
			}
		}
		#endregion

		Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>> _onRecv =
			new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>>();
		Dictionary<ushort, Action<PacketSession, IPacket>> _handler = 
			new Dictionary<ushort, Action<PacketSession, IPacket>>();

		 
		public void Register()
		{
			
			_onRecv.Add((ushort)PacketId.PlayerInfoReq, MakePacket<PlayerInfoReq>);
			_handler.Add((ushort)PacketId.PlayerInfoReq, PacketHandler.PlayerInfoReqHandler); 


			_onRecv.Add((ushort)PacketId.Test, MakePacket<Test>);
			_handler.Add((ushort)PacketId.Test, PacketHandler.TestHandler); 


		}


		public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer)
		{
			ushort count = 0;
			ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
			count += 2;
			ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
			count += 2;

			//switch - case 로 함수를 찾는게 아니라 Dictionary로 찾아서 Invoke
			Action<PacketSession, ArraySegment<byte>> action = null;
			if (_onRecv.TryGetValue(id, out action)) 
				action.Invoke(session, buffer); 
		}



		void MakePacket<T>(PacketSession session, ArraySegment<byte> buffer) where T : IPacket, new()
		{
			T pkt = new T();
			pkt.Read(buffer);
			 
			//switch - case 로 함수를 찾는게 아니라 Dictionary로 찾아서 Invoke
			Action<PacketSession, IPacket> action = null;
			if (_handler.TryGetValue(pkt.Protocol, out action))
				action.Invoke(session, pkt);
		}

	}
}
