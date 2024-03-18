using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static S_PlayerList;

public class PlayerManager 
{
	public MyPlayer _myPlayer;
	Dictionary<int, Player> _players = new Dictionary<int, Player>();        
	public static PlayerManager Instnace { get; } = new PlayerManager();
	UIManager _uiManager;

	public SceneManager _sceneManager;

	public void EnterGame(S_BroadcastEnterGame packet)
	{
		Debug.Log("EnterGame!"); 

		if (_myPlayer &&_myPlayer.PlayerId == packet.playerId) return;

		Object obj = Resources.Load($"Character_{packet.teamId + 1}");
		 
		GameObject go = Object.Instantiate(obj) as GameObject;

		  
		Player player = go.AddComponent<Player>();
		player.TeamId = packet.teamId;
		player.PlayerId = packet.playerId;
		//player.TranslatePlayer(packet.position, packet.moveDir, packet.destPoint);

		_players.Add(packet.playerId, player);
	} 
	public void LeaveGame(S_BroadcastLeaveGame packet) 
	{
		if (_myPlayer.PlayerId == packet.playerId)
		{
			GameObject.Destroy(_myPlayer.gameObject);
			_myPlayer = null;
		}
		else
		{ 
			Player player = null;
			if (_players.TryGetValue(packet.playerId, out player))
			{
				GameObject.Destroy(player.gameObject);
				_players.Remove(packet.playerId); 
			}

		}
	}

	public void Add(S_PlayerList packet)
	{
		Debug.Log("ADD");

		foreach (S_PlayerList.Player p in packet.players)
		{
			Object obj = Resources.Load($"Character_{p.teamId + 1}");
			 
			GameObject go = Object.Instantiate(obj) as GameObject;
			if (p.isSelf)
			{
				MyPlayer myPlayer = go.AddComponent<MyPlayer>();
				_myPlayer = myPlayer;
				_myPlayer.PlayerId = p.playerId;
				_myPlayer.TeamId = p.teamId;
				_players.Add(_myPlayer.PlayerId, _myPlayer); 
				//_myPlayer.TranslatePlayer(p.position, p.moveDir, p.destPoint);
			} 
			else
			{
				Player player = go.AddComponent<Player>();
				//player.TranslatePlayer(p.position, p.moveDir, p.destPoint);
				player.TeamId = p.teamId;
				player.PlayerId = p.playerId;
				_players.Add(p.playerId, player); 
			}
		} 
	}
	public void Move(S_BroadcastMove packet)
	{
		if (_myPlayer == null) 
			Debug.Log("ÇÃ·¹À× ¤Ã¾ø´Ù!!"); 
		 
		if (_myPlayer && _myPlayer.PlayerId == packet.playerId)
		{
			_myPlayer.TranslatePlayer(packet.position, packet.moveDir, packet.destPoint);
		 
		}
		else
		{ 
			Player player = null;
			if (_players.TryGetValue(packet.playerId, out player))
			{
				player.TranslatePlayer(packet.position, packet.moveDir, packet.destPoint);

			}
		}	
	}
	 
	public void Chat(S_BroadcastChat packet)
	{
		if (_uiManager == null) 
			_uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
		 
		_uiManager.UpdateChatingText(packet.playerName, packet.ChatText); 
	}

	public void RequestAttack(S_BroadcastAttack packet)
	{
		Player player;
		if (_players.TryGetValue(packet.playerId, out player))
		{
			player.OnAttack();
		}
		
	}
	public void Fire(S_BroadcastFireObjIdx packet)
	{
		Player player;
		if (_players.TryGetValue(packet.requestedPlayerId, out player))
		{ 

			player.Fire(packet.fireObjId);
		}

	}
	public void Damage(S_BroadcastDamage packet)
	{
		
		FireManager fm = _sceneManager.GetFireObj(packet.FireObjId).GetComponent<FireManager>();

		Player attacker = null;
		Player damagedPlayer = null;
		_players.TryGetValue(packet.attackedPlayerId, out attacker);
		_players.TryGetValue(packet.damagedPlayerId, out damagedPlayer);

		if (attacker != null) 
		{
			fm.OnHit(attacker, damagedPlayer);
		}
		 

	} 
	public void UpdateScore(S_BroadcastUpdateScore packet)
	{
		_sceneManager._uiManger._scoreManager.UpdateScore(packet.teamID);

	}
		
}
 