using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Building/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string buildingTypeID;
    public string buildingTypeName;
    public Transform prefab;
    public bool ResourceGenerator;
    public ResourceGeneratorData resourceGeneratorData;
    public BuildResourceAmount[] resourceAmount;
    public Sprite sprite;
    public float minConstructionRadius;
    public float maxConstructionRadius;
    public float healthAmount;
    public float timeBuildConstructMax;
    public string GetConstructionResourceNeedString()
    {
        string output = string.Empty;
        foreach (var buildResource in resourceAmount)
        {
            output += buildResource.resource.resourceTypeName + ": " + buildResource.amount + "\n"; 
        }
        return output;
    }
}
