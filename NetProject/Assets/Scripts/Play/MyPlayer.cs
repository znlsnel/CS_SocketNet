using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MyPlayer : Player
{
	float _attackCoolTime = 0.5f;
	float _lastAttackTime = 0.0f;
	bool _isAttackable = true;

	// Start is called before the first frame update 
	public bool isSetName = false; 
	public NetworkManager _network;
	private void Awake()
	{
		InitCtrl();
		Camera.main.GetComponent<CameraController>().InitCamera(gameObject);
		//StartCoroutine("CoSendPacket");
		_network = GameObject.Find("NetManager").GetComponent<NetworkManager>();


	}
	void Start()
	{
		handTransform = _animCtrl._animator.GetBoneTransform(HumanBodyBones.RightHand);
		_scene._uiManger.player = this;
		 
	}

	// Update is called once per frame 
	void Update()
	{
		if (isSetName == false)
			return;

		Vector3 moveDir;
		Vector3 lookPoint;

		GetMoveLookDir(out moveDir, out lookPoint);

		C_Move move = new C_Move();
		move.position = Utills.MakeVector3(transform.position);
		move.destPoint = Utills.MakeVector3(lookPoint);
		move.moveDir = Utills.MakeVector3(moveDir);

		
		_network.Send(move.Write());

		RequestAttack();
		UpdateCoolTimes();
	}

	void FixedUpdate()
	{
		if (isSetName == false)
			return; 
		UpdatePosition();
	}

	void UpdateCoolTimes()
	{
		if (_isAttackable == false)
		{
			_lastAttackTime += Time.deltaTime;
			if (_lastAttackTime > _attackCoolTime)
			{
				_isAttackable = true;
				_lastAttackTime = 0.0f;
			}
		}
	}

	void RequestAttack() 
	{ 
		if (_isAttackable == false) 
			return;

		if (Input.GetAxis("Fire1") <= 0)
			return;
		_isAttackable = false;
		C_AttackRequest chat = new C_AttackRequest();
		chat.playerId = PlayerId;
		_network.Send(chat.Write());

		// send Attack!
	}

	void GetMoveLookDir(out Vector3 moveDir, out Vector3 lookPoint)
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		Vector3 camUp = Camera.main.transform.up;
		Vector3 camRight = Camera.main.transform.right;

		camUp.y = 0;
		camRight.y = 0;
		camUp.Normalize();
		camRight.Normalize();

		moveDir = (camUp * vertical + camRight * horizontal);
		moveDir.Normalize();
		// Debug.Log($"move Dir : {moveDir.x},  {moveDir.y} ,  {moveDir.z}");



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

	public override void OnHit()
	{
		if (_hp <= 0) return;

		_hp--;
		if (_hp == 0)
		{
			_animCtrl._isDeath = true;
				_scene._uiManger._respawnManager.OnDie(this);
		}
	}



}
