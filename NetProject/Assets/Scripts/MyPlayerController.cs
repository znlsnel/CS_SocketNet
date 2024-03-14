using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MyPlayerController : PlayerController
{
        // Start is called before the first frame update 
        float walkSpeed = 1.5f;
	
    void Start()
    {   
         InitCtrl();
        Camera.main.GetComponent<CameraController>().InitCamera(gameObject);
    }
          
    // Update is called once per frame 
    void Update()
    {
		Vector3 moveDir;
		Vector3 lookDir; 
		Vector3 lookPoint;
		GetMoveLookDir(out moveDir, out lookDir, out lookPoint);

		//_rigidBody.MovePosition(transform.position +( moveDir * Time.fixedDeltaTime * 1.0f));
		if (moveDir.magnitude > 0.1) 
			transform.Translate(moveDir * Time.fixedDeltaTime * walkSpeed, Space.World);  
		transform.LookAt(lookPoint); 
		_animCtrl.SetDir(moveDir, lookDir); 
	} 

	void GetMoveLookDir(out  Vector3 moveDir, out Vector3 lookDir, out Vector3 lookPoint)
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

		lookDir = point - transform.position; 
		lookDir.y = 0;
		lookDir.Normalize();
		point.y = transform.position.y;
		lookPoint = point;
		 
	}
}
