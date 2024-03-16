using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine; 

public class Player : MonoBehaviour
{
        public AnimationController _animCtrl;
	public Rigidbody _rigidBody;
	public CapsuleCollider _capsuleCollider; 
		
        public float walkSpeed = 5.0f;  
	public int PlayerId = 0;
	protected Vector3 _moveDir = new Vector3(1.0f, 0.0f, 0.0f);
	protected Vector3 _lookPoint = new Vector3(1.0f, 0.0f, 0.0f);
	  
	protected virtual void InitCtrl()
	{
		_animCtrl = GetComponent<AnimationController>();
		_rigidBody = GetComponent<Rigidbody>();
		 _capsuleCollider = GetComponent<CapsuleCollider>();
	}
	// Start is called before the first frame update
	private void Awake()
	{
                InitCtrl();

	}
	void Start()
        {
		//_capsuleCollider.On
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("충돌 발생: " + collision.gameObject.name);
		// 예: 특정 함수 호출 
	}

	// Update is called once per frame

	void Update()
    { 
	} 
	
	private void FixedUpdate()
	{
		UpdatePosition();

	}


	protected void UpdatePosition()
	{

		if (_moveDir.magnitude > 0.1)
		{
			_rigidBody.MovePosition(transform.position + (_moveDir * Time.fixedDeltaTime * walkSpeed));
		}
		 
	} 

	private void LateUpdate() 
	{
		Vector3 direction = _lookPoint - transform.position; // 바라보는 방향
		direction.Normalize(); // 방향 정규화
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		transform.rotation = lookRotation;
	}  

	public void TranslatePlayer(Vector3 position, Vector3 moveDir, Vector3 lookPoint)
	{
		if ((transform.position - position).magnitude > Time.fixedDeltaTime * walkSpeed * 3.0f)
		{ 
			transform.position = position;
		}

		lookPoint.y = position.y;
		Vector3 lookDir = lookPoint - position;
		lookDir.Normalize();
		_lookPoint = lookPoint; 
		_moveDir = moveDir; 
		   
		//transform.LookAt(lookPoint);  
		_animCtrl.SetDir(moveDir, lookDir); 
	}

	public void TranslatePlayer(vector3 position, vector3 moveDir, vector3 lookPoint)
	{
		TranslatePlayer(Utills.MakeVector3(position), Utills.MakeVector3(moveDir), Utills.MakeVector3(lookPoint));
	}
}
