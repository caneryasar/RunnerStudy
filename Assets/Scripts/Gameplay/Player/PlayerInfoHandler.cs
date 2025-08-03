using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInfoHandler : MonoBehaviour {

    private EventArchive _eventArchive;
    
    public MeshRenderer playerRenderer;
    public int colorIndex;
    public TextMeshProUGUI pointText;

    private int _scorePower;
    
    private float _health = 100f;
    private int _currentScore = 2;

    private int _lastHitPoint;


    private void Awake() {

        _eventArchive = FindFirstObjectByType<EventArchive>();
        _eventArchive.OnGetPlayerPoint += () => _currentScore;
        _eventArchive.OnCarSendInfo += GetHitCarInfo;
        _eventArchive.OnGameStart += OpenCanvas;

        pointText.gameObject.SetActive(false);

        _scorePower = (int)Mathf.Log(_currentScore, 2);
    }

    private void OpenCanvas() { pointText.gameObject.SetActive(true); }

    private void GetHitCarInfo(int getHitPoint) {
        
        _lastHitPoint = getHitPoint;
        
        if(_lastHitPoint == _currentScore) { IncreasePoint(); }
        else { ReduceHealth(_lastHitPoint); }
    }


    private void Start() {
        
        GetSetVisuals();
    }

    private void ReduceHealth(int hitPoint) {
        
        var currentHealth = _health;
        
        var percent = 1 - (float)hitPoint / _currentScore;
        
        if(percent * 100 < 0) { 
            
            _eventArchive.InvokeOnPlayerHealthChange(0);
            _eventArchive.InvokeOnChangeGameState(true);
            _eventArchive.InvokeOnGameEnd();
            return;
        }
        
        var deduction = currentHealth * percent;
        
        currentHealth -= deduction;

        var remainder = currentHealth % 10;

        _health = remainder < 5 ? currentHealth - remainder : currentHealth + 10 - remainder;
        
        _eventArchive.InvokeOnPlayerHealthChange((int)_health);

        if(_health is not 0) { return; }
        
        _eventArchive.InvokeOnPlayerHealthChange(0);
        _eventArchive.InvokeOnChangeGameState(true);
        _eventArchive.InvokeOnGameEnd();
    }

    private void IncreasePoint() {

        _scorePower++;
        
        _currentScore = (int)Mathf.Pow(2, _scorePower);
        
        _eventArchive.InvokeOnPowerChange(_scorePower);
        
        GetSetVisuals();
    }

    private void GetSetVisuals() {
        
        var powerMat = _eventArchive.InvokeOnGetPowerColor(_currentScore);
        
        playerRenderer.material = powerMat;
        
        pointText.outlineColor = powerMat.color;
        
        var newFormat = MiscHelper.FormatScore(_currentScore);
        
        pointText.text = newFormat;
        
        _eventArchive.InvokeOnPlayerPointChange(_currentScore);
    }
}