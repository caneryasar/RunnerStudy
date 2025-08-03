using System;
using TMPro;
using UnityEngine;

public class InGameInfoHandler : MonoBehaviour {

    private EventArchive _eventArchive;
    
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI totalScoreText;

    public TextMeshProUGUI endGameText;

    public GameObject startPanel;
    public GameObject mainPanel;
    public GameObject infoPanel;
    public GameObject endPanel;

    private int _topScore = 2;

    private int _currentScore = 0;
    
    private void Awake() {

        _eventArchive = FindFirstObjectByType<EventArchive>();
        

        _eventArchive.OnGameEnd += GameOverUI;
        _eventArchive.OnPlayerHealthChange += UpdateHealth;
        _eventArchive.OnPlayerPointChange += UpdatePoint;
    }

    private void Start() {

        startPanel.SetActive(true);
        mainPanel.SetActive(false);
        infoPanel.SetActive(false);
        endPanel.SetActive(false);

        if(!PlayerPrefs.HasKey("PlayerScore")) { return; }
        _topScore = PlayerPrefs.GetInt("PlayerScore", 2);
        
        UpdatePoint(_topScore);
    }

    private void UpdatePoint(int score) {

        _currentScore = score;

        totalScoreText.text = $"CURRENT SCORE: {MiscHelper.FormatScore(_currentScore)}\n\nTOP SCORE: {MiscHelper.FormatScore(_topScore)}";
        
        if(_currentScore >= _topScore) { 
            
            _topScore = _currentScore;
            var updateFormat = MiscHelper.FormatScore(_topScore);
            scoreText.text = $"TOP SCORE : {updateFormat}";
            
            return;
        }
        
        var newFormat = MiscHelper.FormatScore(_topScore);
        scoreText.text = $"TOP SCORE : {newFormat}";
    }

    private void UpdateHealth(int health) {
        
        if(health < 0) { health = 0; }
        
        healthText.text = $"{health}";
    }

    private void GameOverUI() {
        
        PlayerPrefs.SetInt("PlayerScore", _topScore);
        
        endGameText.text = $"TOTAL SCORE: {MiscHelper.FormatScore(_currentScore)}\n\nTOP SCORE: {MiscHelper.FormatScore(_topScore)}\n\nPLAY AGAIN?";
        
        startPanel.SetActive(false);
        mainPanel.SetActive(false);
        infoPanel.SetActive(false);
        endPanel.SetActive(true);
    }
}