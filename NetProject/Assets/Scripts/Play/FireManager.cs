using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
        [SerializeField] ParticleSystem _hitEffect;
	
       Vector3 _fireDir = Vector3.zero;
	public bool _isFire = false;
	public float _fireSpeed = 10.0f;  
	public float _fireTime = 5.0f;
	public float _lastFireTime = 5.0f;
	int attackPlayerId = 0;

	private void Start() 
	{
		_hitEffect.Stop();
	}
	void FixedUpdate()
	{  
		UpdateTransform();
	}

	public void Fire(int AttackedPlayerId, Vector3 startOrigin, Vector3 fireDir)
	{
		gameObject.SetActive(true);
		_lastFireTime = 0.0f;
		attackPlayerId = AttackedPlayerId;
		transform.position = startOrigin;	
		_fireDir = fireDir;
		_isFire = true;    
	}   

	
	private void OnCollisionEnter(Collision collision)
	{ 
		Debug.Log($"OnCollisionEnter : {collision.gameObject.name}");
		 
		Player player = collision.gameObject.GetComponent<Player>();
		if (player != null)
		{
			if (player.PlayerId == attackPlayerId)
			{
				Debug.Log("player");

				return;
			}
				//Debug.Log(collision.gameObject.name); 

			_isFire = false;
			gameObject.SetActive(false);
			Debug.Log(collision.gameObject.name);
			ParticleSystem instance = Instantiate(_hitEffect);
			instance.transform.position = collision.transform.position;
			instance.Play();
			return;
		}


		// Damage Send

	}

	private void UpdateTransform()
	{
		if (_isFire == false) return;
		 
		_lastFireTime += Time.fixedDeltaTime;
		if (_lastFireTime > _fireTime)
		{
			_isFire = false;
			_lastFireTime = 0.0f; 
			gameObject.SetActive(false);
			return;
		} 

		transform.Translate(_fireDir * Time.fixedDeltaTime * _fireSpeed, Space.World);

	}
}
