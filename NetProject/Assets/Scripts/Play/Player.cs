using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;

public class Player : MonoBehaviour
{
        public AnimationController _animCtrl;
	public Rigidbody _rigidBody;
	protected SceneManager _scene;	
	 
        public float walkSpeed = 5.0f;  
	public int PlayerId = 0; 
	public int TeamId = 0; 
	protected Vector3 _moveDir = new Vector3(0.0f, 0.0f, 0.0f);
	public Vector3 _lookPoint = new Vector3(1.0f, 0.0f, 0.0f);

	public int _maxHp = 3; 
	public int _hp = 3;
	public Transform handTransform;
	protected virtual void InitCtrl()
	{
		_animCtrl = GetComponent<AnimationController>();
		_rigidBody = GetComponent<Rigidbody>();
		_scene = GameObject.Find("GameScene").GetComponent<SceneManager>();
	

	}

	// Start is called before the first frame update
	private void Awake()
	{
                InitCtrl();

	}

	private void Start()
	{
		handTransform = _animCtrl._animator.GetBoneTransform(HumanBodyBones.RightHand);
	}
	private void FixedUpdate()
	{

		UpdatePosition();

	}
	float lastDeathTime = 0.0f;
	private void Update()
	{

		if (_animCtrl._isDeath)
		{
			lastDeathTime += Time.deltaTime;
			if (lastDeathTime > 5.0f)
			{
				_animCtrl._isDeath = false;
				OnRespawn();
				lastDeathTime = 0.0f; 
			}
		}	
	} 


	protected void UpdatePosition()
	{
		if ( _animCtrl._isDeath == false) 
		{ 
			if (_moveDir.magnitude > 0.1)
				_rigidBody.MovePosition(transform.position + (_moveDir * Time.fixedDeltaTime * walkSpeed));

			Vector3 direction = _lookPoint - transform.position; // 바라보는 방향
			direction.Normalize(); // 방향 정규화
			Quaternion lookRotation = Quaternion.LookRotation(direction);
			transform.rotation = lookRotation;
		}
		 
	}



	public void Fire(int fireObjIdx)
	{
		GameObject fireObj = _scene.GetFireObj(fireObjIdx);
		FireManager fireM = fireObj.GetComponent<FireManager>();
		Vector3 fireDir = _lookPoint - transform.position;
		fireDir.Normalize(); 
		  
		fireM.Fire(this, handTransform.position, fireDir, fireObjIdx);
	}

	public void OnAttack()
	{
		//_animCtrl
		_animCtrl.PlayAttackAnimatio();
	}

	public virtual void OnHit()
	{
		if (_hp <= 0) return;
		Debug.Log("HIT");
		_hp--; 
		if(_hp == 0)
		{ 
			_animCtrl._isDeath = true;
		}
	}

	public void OnRespawn() 
	{
		_hp = _maxHp;
		_animCtrl._isDeath = false;
		_animCtrl._animator.speed = 1;
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
