using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float maxDistance = 1.0f;   
	public float cameraSpeed = 5.0f;
	public Vector3 cameraArmPos = new Vector3(-5.0f, 7.0f, 0.0f);  
	// Start is called before the first frame update
	GameObject _player;
        void Start()
        {
		 
        } 
         
        // Update is called once per frame
        void Update()
        {
		   


		//transform.LookAt(_player.transform.position); 
		 
	}
	
	private void LateUpdate() 
	{
		if (_player == null)
			return;

		Vector3 nextCameraPos = _player.transform.position + cameraArmPos;
		transform.position = nextCameraPos;
		//Vector3 moveDIr = nextCameraPos - transform.position;
		//float moveDirLength = moveDIr.magnitude; 
		 
		//if (moveDirLength < 0.1f) 
		//	return;

		//moveDIr.Normalize();

		//float speed = cameraSpeed;
		//if (moveDirLength > maxDistance)
		//	speed = 5.0f;  
		//	//speed = cameraSpeed + (moveDirLength - maxDistance) * 3;

		//transform.Translate(moveDIr * Time.deltaTime * speed, Space.World);


	}

	public void InitCamera(GameObject player)
        {
                _player = player;  
		transform.position = _player.transform.position + cameraArmPos;
		transform.LookAt(_player.transform.position);
	}
}
