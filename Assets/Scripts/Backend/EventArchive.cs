using System;
using UnityEngine;

public class EventArchive : MonoBehaviour {
    
    private MiscHelper _miscHelper;

    private void Awake() { _miscHelper = new MiscHelper(); }

    public event Action<Vector2> OnGetFirstClickPosition;
    public void InvokeOnGetFirstClickPosition(Vector2 pos) { OnGetFirstClickPosition?.Invoke(pos); }
    
    public event Action<Vector2> OnCurrentMousePosition;
    public void InvokeOnCurrentMousePosition(Vector2 pos) { OnCurrentMousePosition?.Invoke(pos); }
    
    public event Action<bool> OnChangeGameState;
    public void InvokeOnChangeGameState(bool state) { OnChangeGameState?.Invoke(state); }

    public event Action OnGameStart;
    public void InvokeOnGameStart() { OnGameStart?.Invoke(); }
    
    public event Action OnGameEnd;
    public void InvokeOnGameEnd() { OnGameEnd?.Invoke(); }

    public event Action<int> OnCityBlockPassed;
    public void InvokeOnCityBlockPassed(int count) { OnCityBlockPassed?.Invoke(count); }
    
    public event Action<GameObject> OnSpawnCars; 
    public void InvokeOnSpawnCars(GameObject cityBlock) { OnSpawnCars?.Invoke(cityBlock); }
    
    public event Action<GameObject, GameObject> OnDespawnCars; 
    public void InvokeOnDespawnCars(GameObject car1, GameObject car2) { OnDespawnCars?.Invoke(car1, car2); }
    
    public event Func<CarPropertyHandler> OnGetCarInfo;
    public CarPropertyHandler InvokeOnGetCarInfo() { return OnGetCarInfo?.Invoke(); }

    public event Func<int, Transform> OnCheckForIdleCar;
    public Transform InvokeOnCheckForIdleCar(int type) { return OnCheckForIdleCar?.Invoke(type); }

    public event Func<int, Material> OnGetPowerColor;
    public Material InvokeOnGetPowerColor(int power) { return OnGetPowerColor?.Invoke(power); }

    public event Func<int> OnGetPlayerPoint;
    public int? InvokeOnGetPlayerPoint() { return OnGetPlayerPoint?.Invoke(); }

    public event Action<Transform, int> OnSetupSpawnedCar;
    public void InvokeOnSetupSpawnedCar(Transform car, int point) { OnSetupSpawnedCar?.Invoke(car, point); }

    
    public event Action<Transform> OnCarHitCheck;
    public void InvokeOnCarHitCheck(Transform car) { OnCarHitCheck?.Invoke(car); }
    
    public event Action<int> OnCarSendInfo;
    public void InvokeOnCarSendInfo(int point) { OnCarSendInfo?.Invoke(point); }

    public event Action<int> OnPlayerHealthChange;
    public void InvokeOnPlayerHealthChange(int value) { OnPlayerHealthChange?.Invoke(value); }
    
    public event Action<int> OnPlayerPointChange;
    public void InvokeOnPlayerPointChange(int value) { OnPlayerPointChange?.Invoke(value); }

    public event Action<int> OnPowerChange;
    public void InvokeOnPowerChange(int value) { OnPowerChange?.Invoke(value); }

    public event Action OnGameRestart;
    public void InvokeOnGameRestart() { OnGameRestart?.Invoke(); }

    public event Action OnGameQuit;
    public void InvokeOnGameQuit() { OnGameQuit?.Invoke(); }

}