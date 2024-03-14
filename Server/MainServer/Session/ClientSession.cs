using ServerCore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

public class ClientSession : PacketSession
{
public int SessionId { get; set; }
public GameRoom Room { get; set; }
public vector3 position { get; set; } = new vector3();
public vector3 moveDir { get; set; } = new vector3();
	public vector3 destPoint { get; set; } = new vector3();

         
	static int serverCount = 0;
public override void OnConnected(EndPoint endPoint)
{
        Console.WriteLine($"OnConnected : {endPoint}");
        Program.Room.Push(() => Program.Room.Enter(this));
} 
public override void OnRecvPacket(ArraySegment<byte> buffer)
{
        PacketManager.Instance.OnRecvPacket(this, buffer);
}

public override void OnDisconnected(EndPoint endPoint)
{
                SessionManager.Instance.Remove(this);
                if (Room != null)
                {
                        GameRoom room = Room;
			room.Push(() => { room.Leave(this); });
			Room = null;     
                }
        Console.WriteLine($"OnDisConnected : {endPoint}");

}


public override void OnSend(int numOfBytes)
{
        //	throw new NotImplementedException();
}
}

