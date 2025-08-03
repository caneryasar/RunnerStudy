using System;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerCameraHandler : MonoBehaviour {

    private EventArchive _eventArchive;
    
    public CinemachineCamera introCamera;
    public CinemachineCamera mainCamera;

    private void Awake() {

        _eventArchive = FindFirstObjectByType<EventArchive>();
        _eventArchive.OnGameStart += ChangeCamera;
    }

    private void ChangeCamera() {
        
        mainCamera.gameObject.SetActive(true);
        introCamera.gameObject.SetActive(false);
    }

    private void Start() {
        
        introCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
    }
}