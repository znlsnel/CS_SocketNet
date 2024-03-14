using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MyPlayer : Player
{
        // Start is called before the first frame update 
	NetworkManager _network;

	void Start()
	{     
		InitCtrl(); 
		Camera.main.GetComponent<CameraController>().InitCamera(gameObject);
		//StartCoroutine("CoSendPacket");
		_network = GameObject.Find("NetManager").GetComponent<NetworkManager>();
	}
          
    // Update is called once per frame 
    void Update()
    {
		Vector3 moveDir;
		Vector3 lookPoint;

		GetMoveLookDir(out moveDir, out lookPoint);
		
		C_Move move = new C_Move();
		move.position = Utills.MakeVector3(transform.position);
		move.destPoint = Utills.MakeVector3(lookPoint);
		move.moveDir = Utills.MakeVector3(moveDir); 
		_network.Send(move.Write());

		UpdatePosition();
		 
		//TranslatePlayer(transform.position, moveDir, lookPoint);
	}

	void GetMoveLookDir(out  Vector3 moveDir, out Vector3 lookPoint)
        {
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		Vector3 camUp = Camera.main.transform.up;
		Vector3 camRight = Camera.main.transform.right;

		camUp.y = 0;
		camRight.y = 0;
		camUp.Normalize();
		camRight.Normalize();

		moveDir = transform.position;
		moveDir = (camUp * vertical + camRight * horizontal);
		moveDir.Normalize();



		Vector3 mousePos = Input.mousePosition;
		mousePos.z = Camera.main.nearClipPlane;

		Vector3 rayStartPos = Camera.main.ScreenToWorldPoint(mousePos);

		mousePos.z = Camera.main.farClipPlane;
		Vector3 rayEndPos = Camera.main.ScreenToWorldPoint(mousePos);
		Vector3 rayDir = rayEndPos - rayStartPos;
		rayDir.Normalize();

		int mask = LayerMask.GetMask("Terrain");
		RaycastHit hitInfo;
		Physics.Raycast(rayStartPos, rayDir, out hitInfo, 50, mask);
		Vector3 point = hitInfo.point;

		point.y = transform.position.y;
		lookPoint = point;
	}

	
}
