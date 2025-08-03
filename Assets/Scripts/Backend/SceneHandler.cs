using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {

    private EventArchive _eventArchive;

    private bool _isGameStarted;

    private void Awake() {

        _eventArchive = FindFirstObjectByType<EventArchive>();
        _eventArchive.OnGameStart += () => _isGameStarted = true;
        _eventArchive.OnGameRestart += () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _eventArchive.OnGameQuit += Application.Quit;
    }

    private void OnApplicationFocus(bool hasFocus) {
        
        if(!_isGameStarted) { return; }

        _eventArchive.InvokeOnChangeGameState(!hasFocus);
    }

}