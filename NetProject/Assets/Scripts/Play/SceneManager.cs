using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
	// Start is called before the first frame update 
	public UIManager _uiManger;



	private void Awake()
	{
		Screen.SetResolution(1000, 700, false);
		_uiManger = GameObject.Find("UIManager").GetComponent<UIManager>();
	}

}
