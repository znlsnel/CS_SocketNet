using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine; 

public class Player : MonoBehaviour
{
        public AnimationController _animCtrl;
	public Rigidbody _rigidBody;

        float walkSpeed = 3.0f;  
	public int PlayerId = 0;
	Vector3 _moveDir;
	 
	protected virtual void InitCtrl()
	{
		_animCtrl = GetComponent<AnimationController>();
		_rigidBody = GetComponent<Rigidbody>();
	}
	// Start is called before the first frame update
	void Start()
        {
                InitCtrl();
        }

    // Update is called once per frame
    void Update()
    {
		UpdatePosition();
	}

	protected void UpdatePosition()
	{ 
		if (_moveDir.magnitude > 0.1)    
		{     
			transform.Translate(_moveDir * Time.deltaTime * walkSpeed, Space.World);
			//_rigidBody.MovePosition(transform.position +  (_moveDir * Time.fixedDeltaTime * walkSpeed));  
			//_rigidBody.MovePosition
			//Debug.Log($"¼Óµµ : {moveDir.magnitude}  ¤§¤§ ;; ");   
		}
	}
	 

	public void TranslatePlayer(Vector3 position, Vector3 moveDir, Vector3 lookPoint)
	{
		if ((transform.position - position).magnitude > 0.5)
			transform.position = position;  
		lookPoint.y = position.y; 
		Vector3 lookDir =  lookPoint - position;
		lookDir.Normalize();
		_moveDir = moveDir;

		transform.LookAt(lookPoint);
		_animCtrl.SetDir(moveDir, lookDir); 
	}

	public void TranslatePlayer(vector3 position, vector3 moveDir, vector3 lookPoint)
	{
		TranslatePlayer(Utills.MakeVector3(position), Utills.MakeVector3(moveDir), Utills.MakeVector3(lookPoint));
	}
}
