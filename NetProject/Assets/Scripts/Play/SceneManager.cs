using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
	// Start is called before the first frame update 
	public UIManager _uiManger;

	public GameObject _fireObject;
	List<GameObject> _fireObjects = new List<GameObject>();
	 

	private void Awake()
	{
		Screen.SetResolution(1000, 700, false);
		_uiManger = GameObject.Find("UIManager").GetComponent<UIManager>();
		PlayerManager.Instnace._sceneManager = this;
	}
	private void Start() 
	{
		for (int i = 0; i < 20; i++)
		{
			GameObject go = Object.Instantiate(_fireObject) as GameObject;
			go.SetActive(false);
			_fireObjects.Add(go);
		} 
	}
	public void TEST(string chat)
	{

		Debug.Log(chat);
	}
	public GameObject GetFireObj(int idx)
	{
		return _fireObjects[idx];
	} 
}
