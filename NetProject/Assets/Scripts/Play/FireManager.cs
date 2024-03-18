using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
        [SerializeField] ParticleSystem _hitEffect;
	
       Vector3 _fireDir = Vector3.zero;
	Player _attacker;
	public bool _isFire = false;
	public float _fireSpeed = 5.0f;  
	public float _fireTime = 5.0f; 
	public float _lastFireTime = 5.0f;
	int attackPlayerId = 0;
	int IDX = -1;

	private void Start() 
	{
		_hitEffect.Stop(); 
	}
	void Update()
	{   
		UpdateTransform();
	}

	public void Fire(Player attacker, Vector3 startOrigin, Vector3 fireDir, int fireObjIdx)
	{
		_attacker = attacker;
		gameObject.SetActive(true);
		_lastFireTime = 0.0f;
		attackPlayerId = attacker.PlayerId;
		transform.position = startOrigin;	
		_fireDir = fireDir;
		_isFire = true; 
		IDX = fireObjIdx;
	}   

	
	private void OnCollisionEnter(Collision collision)
	{ 
		Debug.Log($"OnCollisionEnter : {collision.gameObject.name}");

		MyPlayer mp = _attacker as MyPlayer;
		if (mp == null) 
			return;

		int targetID = -1;
		Player player = collision.gameObject.GetComponent<Player>();
		if (player != null)
		{
			if (player.PlayerId == attackPlayerId)
				return; 
			
			targetID = player.PlayerId; 
		}
		C_DamageRequest chat = new C_DamageRequest();
		chat.attackedPlayerId = attackPlayerId;
		chat.damagedPlayerId = targetID;
		 
		chat.FireObjId = IDX;
		MyPlayer p = _attacker as MyPlayer;
		p._network.Send(chat.Write());

	}

	public void OnHit(Player attacker, Player damagedPlayer)
	{ 
		_isFire = false;
		gameObject.SetActive(false);
		ParticleSystem instance = Instantiate(_hitEffect);
		instance.Play();
		instance.transform.position = transform.position; 
		if (damagedPlayer != null)
		{
			instance.transform.position = damagedPlayer.transform.position + new Vector3(0, 1.0f, 0);
			if (damagedPlayer._hp == 0)
				return;

			damagedPlayer.OnHit();
			if (damagedPlayer._hp == 0)
			{

				MyPlayer mp = _attacker as MyPlayer;
				if (mp != null)
				{
					C_UpdateScoreRequest chat = new C_UpdateScoreRequest();
					chat.teamID = attacker.TeamId;
					mp._network.Send(chat.Write()); 
				}

			}
		}
	}


	private void UpdateTransform()
	{
		if (_isFire == false) return;
		 
		_lastFireTime += Time.deltaTime;
		if (_lastFireTime > _fireTime)
		{
			_isFire = false;
			_lastFireTime = 0.0f;  
			gameObject.SetActive(false);
			return; 
		} 

		transform.Translate(_fireDir * Time.deltaTime * _fireSpeed, Space.World);

	}
}
