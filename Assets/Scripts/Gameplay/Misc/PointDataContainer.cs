using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PointDataContainer", menuName = "Scriptable Objects/PointDataContainer")]
public class PointDataContainer : ScriptableObject {

    public Material baseMaterial;
    public List<Material> generatedColorMaterials;
    public List<GameObject> carPrefabs;
}