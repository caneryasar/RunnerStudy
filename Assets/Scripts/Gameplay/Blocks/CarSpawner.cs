using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarSpawner : MonoBehaviour {
    
    private EventArchive _eventArchive;
    
    private List<Transform> _spawnPoints;
    
    private CarPropertyHandler _requestedCarOne;
    private CarPropertyHandler _requestedCarTwo;
    
    private Transform _requestedCarOneTransform;
    private Transform _requestedCarTwoTransform;

    private GameObject _parent;

    private int _spawnIndexOne;
    private int _spawnIndexTwo;

    private void Awake() {
        
        _spawnPoints = new List<Transform>();

        _parent = transform.parent.gameObject;

        for(var i = 0; i < transform.childCount; i++) {  _spawnPoints.Add(transform.GetChild(i)); }

        _eventArchive = FindFirstObjectByType<EventArchive>();

        _eventArchive.OnSpawnCars += CheckAndSpawn;
    }

    private IEnumerator Start() {

        _requestedCarOne = _eventArchive.InvokeOnGetCarInfo();
        _requestedCarTwo = _eventArchive.InvokeOnGetCarInfo();
        
        yield return new WaitForSeconds(.1f);
        
        _requestedCarOneTransform = _eventArchive.InvokeOnCheckForIdleCar(_requestedCarOne.carType);
        _requestedCarTwoTransform = _eventArchive.InvokeOnCheckForIdleCar(_requestedCarTwo.carType);

        yield return new WaitForSeconds(.1f);
        
        Spawn();
    }


    private void CheckAndSpawn(GameObject target) {

        if(target != _parent) { return; }

        StartCoroutine(CheckSpawnRoutine());
    }

    private IEnumerator CheckSpawnRoutine() {
        
        Despawn();
            
        _requestedCarOne = _eventArchive.InvokeOnGetCarInfo();
        _requestedCarTwo = _eventArchive.InvokeOnGetCarInfo();
        
        _requestedCarOneTransform = _eventArchive.InvokeOnCheckForIdleCar(_requestedCarOne.carType);
        _requestedCarTwoTransform = _eventArchive.InvokeOnCheckForIdleCar(_requestedCarTwo.carType);
        GetSpawnPoints();
            
        var spawnPointOne = _spawnPoints[_spawnIndexOne];
        var spawnPointTwo = _spawnPoints[_spawnIndexTwo];

        yield return new WaitForSeconds(.15f);
            
        if(_requestedCarOneTransform) {
                
            _requestedCarOneTransform.gameObject.SetActive(true);
            _requestedCarOneTransform.position = spawnPointOne.position;
        }
        else {
                
            var spawnedCarOne = Instantiate(_requestedCarOne, spawnPointOne.position, Quaternion.identity);
            _requestedCarOneTransform = spawnedCarOne.transform;
        }
            
        if(_requestedCarTwoTransform) {
                
            _requestedCarTwoTransform.gameObject.SetActive(true);
            _requestedCarTwoTransform.position = spawnPointTwo.position;
        }
        else {
                
            var spawnedCarTwo = Instantiate(_requestedCarTwo, spawnPointTwo.position, Quaternion.identity);
            _requestedCarTwoTransform = spawnedCarTwo.transform;
        }
            
        _eventArchive.InvokeOnSetupSpawnedCar(_requestedCarOneTransform, CalculatePointToSpawn());
        _eventArchive.InvokeOnSetupSpawnedCar(_requestedCarTwoTransform, CalculatePointToSpawn());
    }

    private void Spawn() {
        
        GetSpawnPoints();
            
        var spawnPointOne = _spawnPoints[_spawnIndexOne];
        var spawnPointTwo = _spawnPoints[_spawnIndexTwo];

        var spawnedCarOne = Instantiate(_requestedCarOne, spawnPointOne.position, Quaternion.identity);
        _requestedCarOneTransform = spawnedCarOne.transform;        
        var spawnedCarTwo = Instantiate(_requestedCarTwo, spawnPointTwo.position, Quaternion.identity);
        _requestedCarTwoTransform = spawnedCarTwo.transform;

        var carOnePoint = CalculatePointToSpawn();
        _eventArchive.InvokeOnSetupSpawnedCar(_requestedCarOneTransform, carOnePoint);
        
        var carTwoPoint = CalculatePointToSpawn();
        _eventArchive.InvokeOnSetupSpawnedCar(_requestedCarTwoTransform, carTwoPoint);
    }

    private int CalculatePointToSpawn() {
        
        var playerPoint = (int)_eventArchive.InvokeOnGetPlayerPoint();

        var playerPointPower = (int)Mathf.Log(playerPoint, 2);

        var randPower = playerPointPower switch {
            
            1 => Random.Range(1, 4),
            2 => Random.Range(playerPointPower - 1, playerPointPower + 3),
            _ => Random.Range(Random.Range(playerPointPower - 2, playerPointPower), Random.Range(playerPointPower + 1, playerPointPower + 4))
        };

        var point = (int)Mathf.Pow(2, randPower);

        return point;
    }

    private void Despawn() {
        
        _eventArchive.InvokeOnDespawnCars(_requestedCarOneTransform.gameObject, _requestedCarTwoTransform.gameObject);
        
        _requestedCarOne = null;
        _requestedCarTwo = null;
        _requestedCarOneTransform = null;
        _requestedCarTwoTransform = null;
    }

    private void GetSpawnPoints() {
        
        _spawnIndexOne = Random.Range(0, _spawnPoints.Count);
        _spawnIndexTwo = Random.Range(0, _spawnPoints.Count);
        
        if(_spawnIndexOne == _spawnIndexTwo) {

            while(_spawnIndexOne == _spawnIndexTwo) {
                
                _spawnIndexTwo = Random.Range(0, _spawnPoints.Count);
            }
        }

        switch(_spawnIndexOne) {
            
            case 0 or 1: {
                
                while(_spawnIndexTwo is 1 or 0) { _spawnIndexTwo = Random.Range(0, _spawnPoints.Count); }
                break;
            }
            
            case 2 or 3: {
                
                while(_spawnIndexTwo is 3 or 2) { _spawnIndexTwo = Random.Range(0, _spawnPoints.Count); }
                break;
            }
        }
    }
}