using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class RespawnUI : MonoBehaviour
{
	[SerializeField] private Canvas _baseCanvas;
	[SerializeField] private Animator _respawnUIAnim;
	[SerializeField] private Text _resapwnUIText;
	MyPlayer _player;
	int respawnInitCount = 5;

	bool isDie = false;
	float timer = 0.0f;
	int respawnCount = 0;

	// Start is called before the first frame update
	void Start()
	{
		_resapwnUIText.text = "";
		_baseCanvas.enabled = false; 
	}

	    // Update is called once per frame
	void Update()
	{
		UpdateCountText();
	}

	public void OnDie(MyPlayer player)
	{
		isDie = true;
		_player = player;
		_baseCanvas.enabled = true;
		_resapwnUIText.text = $"{respawnInitCount}";
		_respawnUIAnim.Play("RespawnCountUIAnim");
		respawnCount = respawnInitCount;
	}

	void OnRespawn() 
	{
		_baseCanvas.enabled = false;
		isDie = false;
		_player.OnRespawn();
	}

	void UpdateCountText()
	{
		if (isDie == false) 
			return;

		timer += Time.deltaTime;

		if (timer > 1.0f)
		{
			_resapwnUIText.text = $"{respawnCount--}";
			_respawnUIAnim.Play("RespawnCountUIAnim");
			timer = 0.0f;
		}

		if (respawnCount == 0)
		{
			OnRespawn();
		}
	}
}
