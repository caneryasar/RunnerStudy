using System;
using System.Data;
using UnityEngine;
using Random = UnityEngine.Random;

public class PointDataGenerator : MonoBehaviour {

    private EventArchive _eventArchive;
    public PointDataContainer dataContainer;

    private void Awake() {

        _eventArchive = FindFirstObjectByType<EventArchive>();

        _eventArchive.OnGetCarInfo += ReturnRandomCar;
        _eventArchive.OnGetPowerColor += ReturnColor;
        
        GenerateColors();
    }

    private CarPropertyHandler ReturnRandomCar() { return dataContainer.carPrefabs[Random.Range(0, dataContainer.carPrefabs.Count)].GetComponent<CarPropertyHandler>(); }


    private void GenerateColors() {
        
        dataContainer.generatedColorMaterials.Clear();

        for(var i = 0; i < 10; i++) {
            
            var r = Random.Range(0.5f, 1f);
            var g = Random.Range(0.5f, 1f);
            var b = Random.Range(0.5f, 1f);
        
            var color = new Color(r, g, b, 1f);

            var mat = new Material(dataContainer.baseMaterial) {
                
                name = $"Color_{i + 1}",
                color = color
            };
            
            dataContainer.generatedColorMaterials.Add(mat);
        }
    }

    private void GenerateAndAddColor(int newIndex) {
        
        var difference = newIndex - dataContainer.generatedColorMaterials.Count;

        for(var i = 0; i < difference; i++) {

            var r = Random.Range(0.5f, 1f);
            var g = Random.Range(0.5f, 1f);
            var b = Random.Range(0.5f, 1f);
        
            var color = new Color(r, g, b, 1f);

            var mat = new Material(dataContainer.baseMaterial) {
                
                name = $"Color_{dataContainer.generatedColorMaterials.Count}",
                color = color
            };
            
            dataContainer.generatedColorMaterials.Add(mat);
        }
    }

    private Material ReturnColor(int point) {
        
        var power = (int)Mathf.Log(point, 2);

        if(power - 1 >= dataContainer.generatedColorMaterials.Count) { GenerateAndAddColor(power); }
        
        return dataContainer.generatedColorMaterials[power - 1];
    }
}