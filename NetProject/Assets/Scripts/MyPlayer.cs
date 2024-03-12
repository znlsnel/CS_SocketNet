using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : Player
{
	// Start is called before the first frame update
	NetworkManager _network;
    void Start()
    {
		StartCoroutine("CoSendPacket");
		_network = GameObject.Find("NetManager").GetComponent<NetworkManager>();
    } 

    // Update is called once per frame
    void Update()
    {
         
    } 

	System.Random _random = new System.Random();
	IEnumerator CoSendPacket()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.25f); 

			C_Move movePacket = new C_Move();
			movePacket.posX = _random.Next(-5, 5);
			movePacket.posY = 0; 
			movePacket.posZ = _random.Next(-5, 5);
			_network.Send(movePacket.Write()); 
		}
	}
}
