using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = ("ScriptableObject/Building/BuildingTypeList"))]
public class BuildingTypeListSO : ScriptableObject
{
    public List<BuildingTypeSO> buildingTypes;
}
