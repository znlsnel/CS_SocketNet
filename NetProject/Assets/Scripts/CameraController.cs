using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
        // Start is called before the first frame update
        GameObject _player;
        void Start()
        {

        }
        
        // Update is called once per frame
        void Update()
        {
                if (_player == null)
                        return;
                 
                 
	}
         
	private void LateUpdate() 
	{ 
		transform.position = _player.transform.position + new Vector3(-7.0f, 10.0f, 0.0f);
		//transform.LookAt(_player.transform.position); 
	} 
         
	public void InitCamera(GameObject player)
        {
                _player = player; 
		transform.position = _player.transform.position + new Vector3(-7.0f, 10.0f, 0.0f);
		transform.LookAt(_player.transform.position);
	}
}
