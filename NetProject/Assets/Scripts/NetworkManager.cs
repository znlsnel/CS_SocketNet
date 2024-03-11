using DummyClient;
using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
	ServerSession _session = new ServerSession();

    void Start()
    {
		Console.WriteLine("===========Client===============");
		string host = Dns.GetHostName();
		IPHostEntry ipHost = Dns.GetHostEntry(host);
		string Ip6Address = "192.168.219.100";

		IPAddress ipAddr = IPAddress.Parse(Ip6Address);

		IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

		Connector connector = new Connector();

		connector.Connect(endPoint, () => { return _session; }, 1);
	}

	void Update()
	{

	} 
}
