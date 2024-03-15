using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; 

public class UIManager : MonoBehaviour
{ 
        [SerializeField] private InputField _inputName; 
        [SerializeField] private Button _JoinButton;
        [SerializeField] private Canvas _JoinCanvas;

        [SerializeField] private Canvas _ChatCanvas;
	[SerializeField] private InputField _InputChat;
	[SerializeField] private Text _ChatText;

	NetworkManager _networkManager;

	Queue<string> _chating = new Queue<string>();
	 
	string PlayerName;


    // Start is called before the first frame update
    void Start()
    {
                _JoinButton.onClick.AddListener(JoinGame);
                _networkManager = GameObject.Find("NetManager").GetComponent<NetworkManager>(); 

                _JoinCanvas.enabled = true;
		_ChatCanvas.enabled = false;

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
	}

	// Update is called once per frame
	void Update()
    {
		CheckEnter();

    }

        void CheckEnter()
        {
		bool isEnter = Input.GetKeyDown(KeyCode.Return);
		if (isEnter == false || _InputChat.text == "") return;

		C_Chat chat = new C_Chat();
		chat.playerName = PlayerName;
		chat.ChatText = _InputChat.text;
		_networkManager.Send(chat.Write());
		  
		_InputChat.text = ""; 
	} 
	
	public void UpdateChatingText(string name , string text)
	{
		Queue<string> tempChat = new Queue<string>();
		_chating.Dequeue();   
		_chating.Enqueue($"[{name}] : {text}");

		_ChatText.text = ""; 
		foreach (string txt in _chating)
		{
			_ChatText.text += txt + "\n"; 
		}
	}
}
