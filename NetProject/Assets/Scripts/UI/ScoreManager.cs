using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
        [SerializeField] Text _redTeamScoreText;
        [SerializeField] Text _greenTeamScoreText;
        [SerializeField] Text _blueTeamScoreText;

        int _redTeamScore  = 0;
        int _blueTeamScore = 0;
        int _greenTeamScore = 0;

    void Start() 
    {
		        _redTeamScoreText.text = $"{_redTeamScore}";
		_greenTeamScoreText.text = $"{_greenTeamScore}";
		_blueTeamScoreText.text = $"{_blueTeamScore}";

	}

	// Update is called once per frame
	void Update() 
    {

    }
         
       public void UpdateScore(int teamId)
        {
                if (teamId == 0)
                { 
                        _redTeamScore++;
		        _redTeamScoreText.text = $"{_redTeamScore}";
                }
                else if (teamId == 1)
                {
                        _greenTeamScore++;
		        _greenTeamScoreText.text = $"{_greenTeamScore}";
		} 
                else
                {
                        _blueTeamScore++; 
		        _blueTeamScoreText.text = $"{_blueTeamScore}";
                }
	}
}
