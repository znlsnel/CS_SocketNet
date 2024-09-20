using DummyClient;
using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;



public class NetworkManager : MonoBehaviour
{
	ServerSession _session = new ServerSession();

	public string ipAddress = "192.168.219.101"; // 기본값 설정
	public int port = 7777; // 기본 포트 설정

	public void Send(ArraySegment<byte> data)
	{
		_session.Send(data);
	}
	  
	  
    void Start() 
    {
		IPAddress[] addresses = Dns.GetHostAddresses(ipAddress);
		if (addresses.Length == 0)
		{
			Debug.LogError("Invalid IP address");
			return;
		} 

		IPAddress ipAddr = addresses[0]; // 첫 번째 IP 주소 사용
		IPEndPoint endPoint = new IPEndPoint(ipAddr, port);
		Connector connector = new Connector();

		connector.Connect(endPoint, () => { return _session; }, 1);
	} 
	 
	void Update()
	{
		List<IPacket> packetList = PacketQueue.Instance.PopAll();
		
		foreach(IPacket packet in packetList)
				PacketManager.Instance.HandlePacket(_session, packet); 
	} 
	  
}

 