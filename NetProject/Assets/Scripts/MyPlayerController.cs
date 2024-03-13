using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MyPlayerController : PlayerController
{
        // Start is called before the first frame update 
        float walkSpeed = 10.0f; 
    void Start()
    {  
         InitCtrl();
        Camera.main.GetComponent<CameraController>().InitCamera(gameObject);
    }
         
    // Update is called once per frame
    void Update()
    {
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");

                Vector3 camUp = Camera.main.transform.up; 
                Vector3 camRight = Camera.main.transform.right;
                  
                camUp.y = 0;
                camRight.y = 0;
                camUp.Normalize();  
                camRight.Normalize();
                 
                Vector3 moveDir = transform.position;
		moveDir  = (camUp * vertical + camRight * horizontal);
		moveDir.Normalize();
                 
                _rigidBody.MovePosition(transform.position + moveDir *Time.deltaTime * walkSpeed);
                 
                 
		Vector3 mousePos = Input.mousePosition;
                mousePos.z = Camera.main.nearClipPlane; 

                Vector3 rayStartPos = Camera.main.ScreenToWorldPoint(mousePos);

                mousePos.z = Camera.main.farClipPlane; 
                Vector3 rayEndPos = Camera.main.ScreenToWorldPoint(mousePos);
                Vector3 rayDir = rayEndPos - rayStartPos;
                rayDir.Normalize();

                RaycastHit hitInfo;
                Physics.Raycast(rayStartPos, rayDir, out hitInfo, 20);
                Vector3 point = hitInfo.point;
                 
                Vector3 lookDir = point -  transform.position;
                lookDir.y = 0; 
                lookDir.Normalize();
                point.y = transform.position.y;  

		transform.LookAt(point); 

            //     Debug.Log($"Move Dir : {moveDir.x}, {moveDir.y}, {moveDir.z}");
         //        Debug.Log($"Look Dir : {lookDir.x}, {lookDir.y}, {lookDir.z}");
                  
		_animCtrl.SetDir(moveDir, lookDir);
    }
}
