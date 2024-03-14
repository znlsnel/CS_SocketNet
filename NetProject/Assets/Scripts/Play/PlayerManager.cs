using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static S_PlayerList;

public class PlayerManager 
{
	public MyPlayer _myPlayer;
	Dictionary<int, Player> _players = new Dictionary<int, Player>();        
	public static PlayerManager Instnace { get; } = new PlayerManager();
	UIManager _uiManager;
	// Start is called before the first frame update

	public void EnterGame(S_BroadcastEnterGame packet)
	{
		if (_myPlayer == null)
			return;

		if (_myPlayer.PlayerId == packet.playerId) return;
		 
		Object obj = Resources.Load("Character_1");
		GameObject go = Object.Instantiate(obj) as GameObject;
		 
		 
		Player player = go.AddComponent<Player>();

		player.TranslatePlayer(packet.position, packet.moveDir, packet.destPoint);

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
		Object obj = Resources.Load("Character_1"); 

		foreach (S_PlayerList.Player p in packet.players)
		{
			GameObject go = Object.Instantiate(obj) as GameObject;
			if (p.isSelf)
			{
				MyPlayer myPlayer = go.AddComponent<MyPlayer>();
				_myPlayer = myPlayer;
				_myPlayer.PlayerId = p.playerId; 
				  
				_myPlayer.TranslatePlayer(p.position, p.moveDir, p.destPoint);
			} 
			else
			{
				Player player = go.AddComponent<Player>();
				player.TranslatePlayer(p.position, p.moveDir, p.destPoint);
				player.PlayerId = p.playerId; 
				_players.Add(p.playerId, player); 
			}
		} 
	}
	public void Move(S_BroadcastMove packet)
	{
		if (_myPlayer == null) 
			Debug.Log("ÇÃ·¹À× ¤Ã¾ø´Ù!!");

		if (_myPlayer.PlayerId == packet.playerId)
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
	 
}
 