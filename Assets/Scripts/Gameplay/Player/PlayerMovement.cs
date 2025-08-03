using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour {
    
    private EventArchive _eventArchive;
    
    public GameObject player;

    public float horizontalMoveAmount;
    public float fwdMovementSpeed;
    
    public enum PlayerPos { LEFT, RIGHT }
    public PlayerPos playerPos;

    private float _movementSpeedFactor = 1;
    
    private bool _isPaused = true;
    private bool _isAlive = true;
    
    private Vector2 _swipeStart;
    private Vector2 _swipeCurrent;

    private readonly float ScreenWidth = Screen.width;


    private void Awake() {

        _eventArchive = FindFirstObjectByType<EventArchive>();
        _eventArchive.OnChangeGameState += state => _isPaused = state;
        _eventArchive.OnGetFirstClickPosition += startPosition => _swipeStart = startPosition;
        _eventArchive.OnCurrentMousePosition += currentPosition => _swipeCurrent = currentPosition;
        _eventArchive.OnGameEnd += () => _isAlive = false;
        _eventArchive.OnPowerChange += power => _movementSpeedFactor = power * 3;

        playerPos = PlayerPos.RIGHT;
    }
    
    private void Update() {
        
        if(!_isAlive) { return; }
        
        if(_isPaused) { return; }
        
        transform.position += transform.forward * ((fwdMovementSpeed + Mathf.Clamp(_movementSpeedFactor, 3, fwdMovementSpeed * 2f)) * Time.deltaTime);

        if(_swipeStart == _swipeCurrent) { return; }
        var currentSwipeDistance = _swipeCurrent.x - _swipeStart.x;


        if(!(Mathf.Abs(currentSwipeDistance) >= ScreenWidth * .2f)) { return; }
        
        if(currentSwipeDistance > 0) {
            
            if(!playerPos.Equals(PlayerPos.LEFT)) { return; }
            
            player.transform.DOLocalMove(Vector3.right * horizontalMoveAmount, .25f).SetEase(Ease.InOutSine);
            player.transform.DOLocalRotate(Vector3.up * 30f, .15f)
                .OnComplete(() => player.transform.DOLocalRotate(Vector3.zero, .1f));

            playerPos = PlayerPos.RIGHT;
        }
        else {
            
            if(!playerPos.Equals(PlayerPos.RIGHT)) { return; }
            
            player.transform.DOLocalMove(Vector3.right * -horizontalMoveAmount, .25f).SetEase(Ease.InOutSine);
            player.transform.DOLocalRotate(Vector3.up * -30f, .15f)
                .OnComplete(() => player.transform.DOLocalRotate(Vector3.zero, .1f));
                        
            playerPos = PlayerPos.LEFT;
        }
    }
    
}