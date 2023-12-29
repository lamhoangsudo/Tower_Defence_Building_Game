using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Building/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string buildingTypeID;
    public string buildingTypeName;
    public Transform prefab;
    public ResourceGeneratorData resourceGeneratorData;
    public Sprite sprite;
}
