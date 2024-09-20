using System.Net.Sockets;
using System.Net;
using System.Text;
using ServerCore;



class Program
{
	static Listener _listener = new Listener();
	public static GameRoom Room = new GameRoom();
	static int count = 0;
		 
	static void FlushRoom()
	{ 
		Room.Push(() => Room.Flush());  
		JobTimer.Instance.Push(FlushRoom, 1);
	} 
		   
	static void Main(string[] args) 
	{ 
		Console.WriteLine("===========Server===============");

		string Ip6Address = "192.168.219.101"; 
		  
		IPAddress ipAddr = IPAddress.Parse(Ip6Address); 
		IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777); 
			 
		_listener.Init(endPoint, () =>{ return SessionManager.Instance.Generate(); });

		JobTimer.Instance.Push(FlushRoom);
		while (true)
			JobTimer.Instance.Flush();
	}
}
