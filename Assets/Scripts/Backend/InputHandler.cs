using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour {

    private EventArchive _eventArchive;
    
    private InputAction _clickAction;
    private InputAction _attackAction;

    private bool _isPaused = true;


    private void Awake() {

        _eventArchive = FindFirstObjectByType<EventArchive>();
        _eventArchive.OnChangeGameState += state => _isPaused = state;

        _clickAction = InputSystem.actions.FindAction("Click");
        _attackAction = InputSystem.actions.FindAction("Attack");
    }

    private void Update() {
        
        if(_isPaused) { return; }
        
        if(_attackAction.triggered) {
            
            _eventArchive.InvokeOnGetFirstClickPosition(_clickAction.ReadValue<Vector2>());
            // Debug.Log($"Initial Mouse Pos: {_clickAction.ReadValue<Vector2>()}");
        }

        if(_attackAction.IsPressed()) {
            
            _eventArchive.InvokeOnCurrentMousePosition(_clickAction.ReadValue<Vector2>());
            // Debug.Log($"Current Mouse Pos: {_clickAction.ReadValue<Vector2>()}");
        }
        else {
            _eventArchive.InvokeOnCurrentMousePosition(Vector2.zero);
            _eventArchive.InvokeOnGetFirstClickPosition(Vector2.zero);
        }
    }

}