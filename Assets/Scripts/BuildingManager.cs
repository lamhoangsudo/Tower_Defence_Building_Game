using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private BuildingTypeSO building;
    private BuildingTypeListSO BuildingTypeList;
    private Camera mainCamera;
    private const string stoneHarvester = "SH1";
    private const string woodHarvester = "WH1";
    private void Awake()
    {
        BuildingTypeList = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
    }
    private void Start()
    {
        mainCamera = Camera.main;       
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            building = BuildingTypeList.buildingTypes.Where(bd => bd.buildingTypeID.Equals(stoneHarvester)).FirstOrDefault();
            if (building != null)
            {
                Instantiate(building.prefab, GetMouseWorldPositions(), Quaternion.identity);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            building = BuildingTypeList.buildingTypes.Where(bd => bd.buildingTypeID.Equals(woodHarvester)).FirstOrDefault();
            if (building != null)
            {
                Instantiate(building.prefab, GetMouseWorldPositions(), Quaternion.identity);
            }
        }
    }
    private Vector3 GetMouseWorldPositions()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}
