using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI; 

public class UIManager : MonoBehaviour
{ 
        [SerializeField] private InputField _inputName; 
	[SerializeField] private InputField _InputChat;

        [SerializeField] private Button _JoinButton;

        [SerializeField] private Canvas _JoinCanvas;
        [SerializeField] private Canvas _ChatCanvas;
	[SerializeField] private Canvas _respawnUI;
	[SerializeField] public  Canvas _scoreUI;

	[SerializeField] private Text _ChatText;

	public RespawnUI _respawnManager;
	public ScoreManager _scoreManager;
	
	NetworkManager _networkManager;

	Queue<string> _chating = new Queue<string>();
	 
	string PlayerName;
	public MyPlayer player;

	// Start is called before the first frame update
	private void Awake()
	{
		_networkManager = GameObject.Find("NetManager").GetComponent<NetworkManager>();

		_scoreUI = Instantiate(_scoreUI, Vector3.zero, Quaternion.identity);
		_respawnUI = Instantiate(_respawnUI, Vector3.zero, Quaternion.identity);
		//_ChatCanvas = Instantiate(_ChatCanvas, Vector3.zero, Quaternion.identity);
		_respawnUI.enabled = false; 
		 
		_respawnManager = _respawnUI.GetComponent<RespawnUI>();
		_scoreManager = _scoreUI.GetComponent<ScoreManager>(); 
	}
	void Start()
    { 
                _JoinButton.onClick.AddListener(JoinGame);



		_JoinCanvas.enabled = true;
		_ChatCanvas.enabled = false;
		_InputChat.text = ""; 
		_ChatText.text = "";
		for (int i = 0; i < 16; i++)
		{
			_chating.Enqueue("");
			_ChatText.text +=  " \n"; 
		}

	}
         
	void JoinGame() 
        { 
                if (_inputName.text == "") return;
		 
		PlayerName = _inputName.text; 
                _JoinCanvas.enabled = false;  
		_ChatCanvas.enabled = true;
		player.isSetName = true;
	}

	// Update is called once per frame
	void Update()
	{
		CheckEnter();

	}

        void CheckEnter()
        {
		bool isEnter = Input.GetKeyDown(KeyCode.Return);
		if (isEnter == false) 
			return;
		if(_InputChat.text == "") 
			return;

		C_Chat chat = new C_Chat();
		chat.playerName = PlayerName;
		chat.ChatText = _InputChat.text;
		_networkManager.Send(chat.Write());

		_InputChat.text = "";
		_InputChat.Select();
		_InputChat.ActivateInputField();
	} 
	
	public void UpdateChatingText(string name , string text)
	{
		Queue<string> tempChat = new Queue<string>();
		//text = text.ToString().Substring(0, 10); 
		_chating.Dequeue();    
		_chating.Enqueue($"[{name}] : {text}");

		_ChatText.text = "";
		foreach (string txt in _chating)
		{
			_ChatText.text += txt + "\n"; 
		}
	}
}
