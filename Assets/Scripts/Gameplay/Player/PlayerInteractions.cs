using System;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour {
    
    private EventArchive _eventArchive;
    
    
    internal int passedBlocks = 0;


    private void Awake() {

        _eventArchive = FindFirstObjectByType<EventArchive>();
    }

    private void OnTriggerEnter(Collider other) {

        if(other.CompareTag("BlockShuffleTrigger")) {
            
            passedBlocks++;
            
            _eventArchive.InvokeOnCityBlockPassed(passedBlocks);
        }

        if(other.CompareTag("Car")) {
            
            _eventArchive.InvokeOnCarHitCheck(other.transform);
        }
    }
}